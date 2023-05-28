Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim dtSPID As New DataTable
    Dim dtTempI As New DataTable
    Dim strDesc As String = ""
    Dim strtable As String = ""
    Dim strText As String = ""
    Dim strMailTo As String = ""
    Dim strCcIDs As String = ""
    Dim strToIDs As String = ""
    Dim strBCCIDs As String = ""

    Dim strSubject As String = ""
    Dim strBodyText As String = ""
    Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    Dim streamids As String() = Nothing

    Dim mimeType As String = Nothing

    Dim encoding As String = Nothing

    Dim extension As String = Nothing

    Dim deviceInfo As String

    Dim bytes As Byte()

    Dim lr As New Microsoft.Reporting.WebForms.LocalReport
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")

            btnSave_Click(sender, e)
        End If
    End Sub
    Public Sub grid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub

    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_VGM_REPORT_DATE_WISE '" & Convert.ToDateTime(Trim(txtfromDate.Text)).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text)).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtContainerNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub

    Protected Sub lnkmail_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim Auto_Id As String = lnkRemove.CommandArgument

            Dim SLIPID As String = ed.Decrypt(HttpUtility.UrlDecode(Auto_Id))

            strSql = ""
            strSql += "USP_VALIDATION_VGM_MAILS '" & SLIPID & "'"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Email IDs not present for this Exporter');", True)
                Exit Sub
            End If
            LoadReport(SLIPID, sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub LoadReport(SLIPID As String, sender As Object, e As EventArgs)
        Try
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/VGMRDLC.rdlc")

            'Dim VGMNo As String = ed.Decrypt(HttpUtility.UrlDecode(SLIPID))

            dt = db.sub_GetDatatable("USP_VGM_PRINT '" & SLIPID & "'")
            If dt.Rows.Count > 0 Then
                Dim ShipperName As String = Trim(dt.Rows(2)("Particulars") & "")
                Dim WDate As String = Convert.ToDateTime(Trim(dt.Rows(9)("Particulars") & "")).ToString("dd-MM-yyyy")

                Dim p1 As New ReportParameter("ShipperName", ShipperName)
                Dim p2 As New ReportParameter("WDate", WDate)

                Dim datasource As New ReportDataSource("DataSet1", dt)

                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.DataSources.Add(datasource)

                Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2})
            End If

            strSql = ""
            strSql += "USP_VALIDATION_VGM_MAILS '" & SLIPID & "'"
            dtSPID = db.sub_GetDatatable(strSql)
            If dtSPID.Rows.Count > 0 Then
                strText = ""
                strText = "<font face='Calibri' color='black' size=3>A VGM, &lt; " & Trim(dtSPID.Rows(0)("SlipNo")) & " &gt; has been generated at " & Trim(dtSPID.Rows(0)("Yard")) & " for container No " & Trim(dtSPID.Rows(0)("ContainerNo")) & "</font> <br>"

                strtable += "<html><body>"
                strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black' Style='width: 100%; height: auto;'>  "
                strtable += "<tr bgcolor='white'><font face='Calibri' color='black' size='3'>"
                strtable += "<td style='padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'>Greetings, <br><br>" & strText & "<br>"
                strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black' Style='width: 100%; height: auto;'>  "
                strtable = strtable & "<font face='Calibri' color='black' size=3>Please find attachment. </font> "
                strtable += "<br>"
                strtable += "<font face='Calibri' color='black' size=3>"

                strtable += "<br>"
                strtable += "<font face='Calibri' color='black' size=3>Best Regards,<br>"
                ' strtable += "[" & Trim(dtSPID.Rows(ispID)("Person Name")) & "]</font>"
                strtable += "&lt; " & Trim(dtSPID.Rows(0)("Regards")) & " &gt; </font>"

                strtable += "</table>"
                strtable += "</body></html>"
                strtable += "</table>"
                strCcIDs = ""
                strBCCIDs = ""
                For i = 0 To dtSPID.Rows.Count - 1
                    strMailTo = Trim(dtSPID.Rows(i)("MailTo")) + ";" + strMailTo
                    If Trim(dtSPID.Rows(i)("MailCC")) <> "" Then
                        strCcIDs = Trim(dtSPID.Rows(i)("MailCC")) + ";" + strCcIDs
                    End If
                    If Trim(dtSPID.Rows(i)("MailBCC")) <> "" Then
                        strBCCIDs = Trim(dtSPID.Rows(i)("MailBCC")) + ";" + strBCCIDs
                    End If
                Next
                strToIDs = strMailTo
                If (strToIDs = ";;") Then
                    Exit Sub
                End If

                strSubject = "VGM for [" & Trim(dtSPID.Rows(0)("ContainerNo")) & "] "
                strBodyText = strtable & "<br><br>"
                Dim strmaildomain As String = ""
                'Dim strCcIDs As String = ""
                Dim intPortNo As Integer
                Dim strFrom As String = ""
                Dim strMailPswrd As String = ""
                Dim mm As New MailMessage
                Dim varSplitTo() As String

                strmaildomain = Trim(dtSPID.Rows(0)("SMTPServer"))
                intPortNo = Val(dtSPID.Rows(0)("SMTPServerPort"))
                strFrom = Trim(dtSPID.Rows(0)("MailFromID"))
                strMailPswrd = Trim(dtSPID.Rows(0)("UserPassword"))
                Dim mailAddress As New MailAddress(strFrom)
                If Trim(strToIDs) <> "" Then
                    varSplitTo = Split(strToIDs, ";")
                    For Each Toid As String In varSplitTo
                        If Trim(Toid) <> "" And Len(Trim(Toid)) >= 5 Then
                            If InStr(Toid, "@") > 0 Then
                                mm.To.Add(Trim(Toid))
                            End If
                        End If
                    Next
                End If
                If Trim(strCcIDs) <> "" Then
                    varSplitTo = Split(strCcIDs, ";")
                    For Each Toid As String In varSplitTo
                        If Trim(Toid) <> "" And Len(Trim(Toid)) >= 5 Then
                            If InStr(Toid, "@") > 0 Then
                                mm.CC.Add(Trim(Toid))
                            End If
                        End If
                    Next
                End If
                If Trim(strBCCIDs) <> "" Then
                    varSplitTo = Split(strBCCIDs, ";")
                    For Each Toid As String In varSplitTo
                        If Trim(Toid) <> "" And Len(Trim(Toid)) >= 5 Then
                            If InStr(Toid, "@") > 0 Then
                                mm.Bcc.Add(Trim(Toid))
                            End If
                        End If
                    Next
                End If
                mm.Subject = strSubject
                mm.Body = strBodyText
                mm.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "VGM_" & Trim(dtSPID.Rows(0)("ContainerNo")) & ".pdf")))
                mm.IsBodyHtml = True
                mm.From = mailAddress
                Dim smtpClient As New SmtpClient(strmaildomain, intPortNo)
                Dim credentials As New System.Net.NetworkCredential(strFrom, strMailPswrd)
                smtpClient.EnableSsl = True
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = credentials
                smtpClient.Send(mm)
                strSql = ""
                strSql = " UPDATE VGM_D SET IsMailSend=1 WHERE SlipId='" & SLIPID & "'"
                db.sub_ExecuteNonQuery(strSql)

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Mail Sent successfully');", True)
                btnSave_Click(Sender, e)
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Function ExportReportToPDF(path As String, reportName As String) As String

        Dim bytes As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)
        Dim filename As String = path & reportName
        Using fs = New System.IO.FileStream(filename, System.IO.FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
        End Using

        Return filename
    End Function
End Class
