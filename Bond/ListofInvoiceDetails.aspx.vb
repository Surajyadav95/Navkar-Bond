Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel
Imports Outlook = Microsoft.Office.Interop.Outlook

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9 As DataTable
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
    Dim strSubject As String = ""
    Dim strBodyText As String = ""
    Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    Dim deviceInfo As String

    Dim bytes As Byte()
    Dim streamids As String() = Nothing

    Dim mimeType As String = Nothing

    Dim encoding As String = Nothing

    Dim extension As String = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-7)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
            btnsearch_Click(sender, e)
        End If
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_NOC_Assessment_summary '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00") & "',"
            strSql += "'" & Trim(ddlcriteria.SelectedValue & "") & "','" & Trim(txtAssessno.Text & "") & "','" & Trim(txtnocno.Text & "") & "','" & Trim(txtgstname.Text & "") & "','" & Trim(ddlinvoicetype.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdSummary.DataSource = dt
            grdSummary.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdSummary.DataSource = dt
        grdSummary.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            txtAssessno.Text = ""
            txtnocno.Text = ""
            txtgstname.Text = ""
            ddlinvoicetype.SelectedValue = 0
            If ddlcriteria.SelectedValue = 0 Then
                divassessno.Attributes.Add("style", "display:none")
                divnocno.Attributes.Add("style", "display:none")
                divgstname.Attributes.Add("style", "display:none")
                divinvtype.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 1 Then
                divassessno.Attributes.Add("style", "display:block")
                divnocno.Attributes.Add("style", "display:none")
                divgstname.Attributes.Add("style", "display:none")
                divinvtype.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 2 Then
                divassessno.Attributes.Add("style", "display:none")
                divnocno.Attributes.Add("style", "display:block")
                divgstname.Attributes.Add("style", "display:none")
                divinvtype.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 3 Then
                divassessno.Attributes.Add("style", "display:none")
                divnocno.Attributes.Add("style", "display:none")
                divgstname.Attributes.Add("style", "display:block")
                divinvtype.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 4 Then
                divassessno.Attributes.Add("style", "display:none")
                divnocno.Attributes.Add("style", "display:none")
                divgstname.Attributes.Add("style", "display:none")
                divinvtype.Attributes.Add("style", "display:block")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim AssessNo As String = lnkcancel.CommandArgument
            Dim WorkYear As String = grdSummary.DataKeys(row.RowIndex).Value.ToString()
            Dim str As String = ""
            Dim dtACk As New DataTable
            strSql = ""
            strSql = "USP_Validation_ACK_Status '" & AssessNo & "' "
            dtACk = db.sub_GetDatatable(strSql)
            If dtACk.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' " + dtACk.Rows(0)("msg") + ".');", True)
                Exit Sub
            End If
            strSql = ""
            strSql = "select IsInvLocked from bond_assessM where InvoiceNo='" & AssessNo & "' and WorkYear='" & WorkYear & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows(0)(0) = True) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified invoice has been locked. Cannot cancel.');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += "UPDATE Bond_assessm set iscancel=1,cancelledby='" & Session("UserId_BondCFS") & "',CancelledOn=getdate() where InvoiceNo='" & AssessNo & "' and WorkYear='" & WorkYear & "'"
            db.sub_ExecuteNonQuery(strSql)
            btnsearch_Click(sender, e)
            lblsession.Text = "Assessment Cancelled Successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkmail_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim AssessNo As String = lnkcancel.CommandArgument
            Dim WorkYear As String = grdSummary.DataKeys(row.RowIndex).Value.ToString()
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/BondAssessmentPrint.rdlc")

            'Dim assessno As String = Request.QueryString("AssessNo")
            'Dim workyear As String = Request.QueryString("WorkYear")
            ''assessno = "182"
            ''workyear = "2018-19"
            strSql = ""
            strSql += "USP_NOC_Assessment_Print_details '" & assessno & "','" & workyear & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then


                dt = ds.Tables(1)
                Dim Con_name As String = dt.Rows(0)("con_Name")
                Dim addressI As String = dt.Rows(0)("AddressI")
                Dim addressII As String = Trim(dt.Rows(0)("AddressII") & "")
                Dim BankName As String = dt.Rows(0)("BankName")
                Dim AccountNo As String = dt.Rows(0)("AccountNo")
                Dim BranchName As String = dt.Rows(0)("BranchName")
                Dim IFSCCode As String = dt.Rows(0)("IFSCCode")
                Dim GSTINComp As String = dt.Rows(0)("GSTIN")
                Dim CINNo As String = dt.Rows(0)("CINNo")
                Dim PANNo As String = dt.Rows(0)("PANNo")
                Dim ConFor As String = dt.Rows(0)("Con_For")
                Dim NoteI As String = dt.Rows(0)("NoteI")
                Dim NoteII As String = dt.Rows(0)("NoteII")
                Dim Swiftcode As String = dt.Rows(0)("Swiftcode")
                Dim MICROCode As String = dt.Rows(0)("MICROCode")

                dt1 = ds.Tables(0)

                Dim Assessnoprint As String = Trim(dt1.Rows(0)("AssessNo") & "")
                Dim assessdate As String = Trim(dt1.Rows(0)("AssessDate") & "")
                Dim GSTNAME As String = Trim(dt1.Rows(0)("GSTName") & "")
                Dim GSTADD As String = Trim(dt1.Rows(0)("GSTAddress") & "")
                Dim GSTIN As String = Trim(dt1.Rows(0)("GSTIn_uniqID") & "")
                Dim STATE As String = Trim(dt1.Rows(0)("State") & "")
                Dim STATECODE As String = Trim(dt1.Rows(0)("state_Code") & "")
                Dim CHAName As String = Trim(dt1.Rows(0)("CHAName") & "")
                Dim ImporterName As String = Trim(dt1.Rows(0)("ImporterName") & "")
                Dim NOCNo As String = Trim(dt1.Rows(0)("NOCNo") & "")
                Dim ValidUptoDate As String = Trim(dt1.Rows(0)("ValidUptoDate") & "")
                Dim IGMNo As String = Trim(dt1.Rows(0)("IGMNo") & "")
                Dim NOCDate As String = Trim(dt1.Rows(0)("NOCDate") & "")
                Dim BOENo As String = Trim(dt1.Rows(0)("BOENo") & "")
                Dim BOEDate As String = Trim(dt1.Rows(0)("BOEDate") & "")
                Dim Area As String = Trim(dt1.Rows(0)("Area") & "")
                Dim Qty As String = Trim(dt1.Rows(0)("Qty") & "")
                Dim Value As String = Trim(dt1.Rows(0)("Value") & "")
                Dim Duty As String = Trim(dt1.Rows(0)("duty") & "")

                Dim Weight As String = Trim(dt1.Rows(0)("Weight") & "")
                Dim UserName As String = Trim(dt1.Rows(0)("UserName") & "")
                Dim Today As String = Trim(dt1.Rows(0)("Today") & "")
                Dim SGST As String = Val(dt1.Rows(0)("sgst"))
                Dim CGST As String = Val(dt1.Rows(0)("cgst"))
                Dim IGST As String = Val(dt1.Rows(0)("igst"))
                Dim NetTotal As String = dt1.Rows(0)("NetTotal")
                Dim GrandTotal As String = dt1.Rows(0)("GrandTotal")
                Dim amtinword As String = dt1.Rows(0)("amtinword")

                Dim CargoType As String = Trim(dt1.Rows(0)("cargotype") & "")
                Dim CargoDescrp As String = Trim(dt1.Rows(0)("CargoDescrp") & "")
                Dim T20 As String = dt1.Rows(0)("T20")
                Dim T40 As String = dt1.Rows(0)("T40")
                Dim DeliveryType As String = dt1.Rows(0)("DeliveryType")
                Dim BondNo As String = Trim(dt1.Rows(0)("BondNo") & "")
                Dim BondDate As String = Trim(dt1.Rows(0)("BondDate") & "")
                Dim EXBOENo As String = Trim(dt1.Rows(0)("ExBoeNo") & "")
                Dim EXBOEDate As String = Trim(dt1.Rows(0)("ExBoedate") & "")

                Dim LastAssessNo As String = Trim(dt1.Rows(0)("LastAssessNo") & "")
                Dim LastAssessNo1 As String
                If LastAssessNo = "0" Then
                    LastAssessNo1 = ""
                Else
                    LastAssessNo1 = "Previous Invoice No : " + LastAssessNo
                End If

                Dim Remarks As String = Trim(dt1.Rows(0)("Remarks") & "")

                dt2 = ds.Tables(2)
                Dim datasource As New ReportDataSource("DataSet1", dt2)
                dt3 = ds.Tables(3)
                Dim datasource1 As New ReportDataSource("DataSet2", dt3)

                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.DataSources.Add(datasource)
                ReportViewer1.LocalReport.DataSources.Add(datasource1)

                Dim p1 As New ReportParameter("Con_Name", Con_name)
                Dim p2 As New ReportParameter("AddressI", addressI)
                Dim p3 As New ReportParameter("AddressII", addressII)
                Dim p4 As New ReportParameter("BankName", BankName)
                Dim p5 As New ReportParameter("AccountNo", AccountNo)
                Dim p6 As New ReportParameter("BranchName", BranchName)
                Dim p7 As New ReportParameter("IFSCCOde", IFSCCode)
                Dim p8 As New ReportParameter("GSTINComp", GSTINComp)
                Dim p9 As New ReportParameter("CINNo", CINNo)
                Dim p10 As New ReportParameter("PANNo", PANNo)

                Dim p11 As New ReportParameter("AssessNo", Assessnoprint)
                Dim p12 As New ReportParameter("AssessDate", assessdate)
                Dim p13 As New ReportParameter("GSTNAME", GSTNAME)
                Dim p14 As New ReportParameter("GST_address", GSTADD)
                Dim p15 As New ReportParameter("GSTIN", GSTIN)
                Dim p16 As New ReportParameter("STATE", STATE)
                Dim p17 As New ReportParameter("STATECODE", STATECODE)
                Dim p18 As New ReportParameter("CHAName", CHAName)
                Dim p19 As New ReportParameter("ImporterName", ImporterName)
                Dim p20 As New ReportParameter("NOCNo", NOCNo)
                Dim p21 As New ReportParameter("ValidUptoDate", ValidUptoDate)
                Dim p22 As New ReportParameter("IGMNo", IGMNo)
                Dim p23 As New ReportParameter("NOCDate", NOCDate)
                Dim p24 As New ReportParameter("BOENo", BOENo)
                Dim p25 As New ReportParameter("Value", Value)
                Dim p26 As New ReportParameter("BOEDate", BOEDate)
                Dim p27 As New ReportParameter("Area", Area)
                Dim p28 As New ReportParameter("Qty", Qty)
                Dim p29 As New ReportParameter("Weight", Weight)
                Dim p30 As New ReportParameter("UserName", UserName)
                Dim p31 As New ReportParameter("Today", Today)
                Dim p32 As New ReportParameter("SGST", SGST)
                Dim p33 As New ReportParameter("CGST", CGST)
                Dim p34 As New ReportParameter("IGST", IGST)
                Dim p35 As New ReportParameter("NetTotal", NetTotal)
                Dim p36 As New ReportParameter("GrandTotal", GrandTotal)
                Dim p37 As New ReportParameter("amtinword", amtinword)
                Dim p38 As New ReportParameter("ConFor", ConFor)

                Dim p39 As New ReportParameter("NoteI", NoteI)
                Dim p40 As New ReportParameter("NoteII", NoteII)
                Dim p41 As New ReportParameter("Duty", Duty)
                Dim p42 As New ReportParameter("CargoType", CargoType)
                Dim p43 As New ReportParameter("CargoDescrp", CargoDescrp)
                Dim p44 As New ReportParameter("T20", T20)
                Dim p45 As New ReportParameter("T40", T40)
                Dim p46 As New ReportParameter("DeliveryType", DeliveryType)
                Dim p47 As New ReportParameter("BondNo", BondNo)
                Dim p48 As New ReportParameter("BondDate", BondDate)
                Dim p49 As New ReportParameter("EXBOENo", EXBOENo)
                Dim p50 As New ReportParameter("EXBOEDate", EXBOEDate)

                Dim p51 As New ReportParameter("Swiftcode", Swiftcode)
                Dim p52 As New ReportParameter("MICROCode", MICROCode)

                Dim p53 As New ReportParameter("LastAssessNo1", LastAssessNo1)
                Dim p54 As New ReportParameter("Remarks", Remarks)

                Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54})
                '''''''''''''''''''Print Finish


                ''''''''''Mail Body'''''''''''''



                'strSql = ""
                'strSql = "USP_getElement '" & lblticketno.Text & "'"
                'dtTempI = db.sub_GetDatatable(strSql)
                'strDesc = ""



                'For ispID As Integer = 0 To dtSPID.Rows.Count - 1
                strSql = ""
                strSql = "USP_MAIL_INVOICE_EMAILID  '" & Trim(AssessNo) & "','" & Trim(WorkYear) & "'"
                ds = db.sub_GetDataSets(strSql)

                Dim dtTemp As New DataTable
                strText = ""
                strText = "<font face='Calibri' color='black' size=3>A Invoice, &lt; " & Trim(ds.Tables(1).Rows(0)("InvoiceNo")) & " &gt; has been generated.</font> <br>"

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
                strtable += "&lt; Bond &gt; </font>"

                strtable += "</table>"
                strtable += "</body></html>"
                strtable += "</table>"
                'If dtSPID.Rows.Count > 0 Then
                
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        strMailTo = Trim(ds.Tables(0).Rows(i)("Email")) + ";" + strMailTo
                    Next
                    strToIDs = strMailTo

                End If
                'strToIDs = "durga@phoenixkreations.com;"
                strSubject = "Bond: Invoice [" & Trim(ds.Tables(1).Rows(0)("InvoiceNo")) & "] "
                strBodyText = strtable & "<br><br>"
                'End If
                If (strToIDs = ";;") Then
                    Exit Sub
                End If
                ' Exit Sub
                strCcIDs = ""
                ' am.Sub_mail(strToIDs, strCcIDs, strSubject, strBodyText)

                'Next

                ''''''Mail Connection''''''''''



                Dim strmaildomain As String = ""
                'Dim strCcIDs As String = ""
                Dim intPortNo As Integer
                Dim strFrom As String = ""
                Dim strMailPswrd As String = ""
                Dim mm As New MailMessage
                Dim varSplitTo() As String
                '    Dim userid As String = "4"
                strmaildomain = "mail.phoenixkreations.in"
                intPortNo = 25
                strFrom = "aampleERP@phoenixkreations.in"
                strMailPswrd = "kanshu0909"
                Dim mailAddress As New MailAddress(strFrom, "Bond")
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
                mm.Bcc.Add("aampleERP@phoenixkreations.in")
                mm.Subject = strSubject
                mm.Body = strBodyText
                mm.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "BondAssessmentPrint.pdf")))
                mm.IsBodyHtml = True

                mm.From = mailAddress
                Dim smtpClient As New SmtpClient(strmaildomain, intPortNo)
                Dim credentials As New System.Net.NetworkCredential(strFrom, strMailPswrd)
                smtpClient.EnableSsl = False
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = credentials
                smtpClient.Send(mm)
            End If
            'Dim strfilename As String = ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "BondAssessmentPrint.pdf")
            'Dim oApp As Outlook.Application = New Outlook.Application()
            'Dim mail As Outlook.MailItem = CType(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
            'mail.To = ""
            ''mail.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "BondAssessmentPrint.pdf")))
            'mail.Attachments.Add(strfilename, Outlook.OlAttachmentType.olByValue, 1, "BondAssessmentPrint")
            'mail.Display(True)
            lblsession1.Text = "Mailed Successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel1.Update()
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

    Protected Sub btnGeneratePDF_Click(sender As Object, e As EventArgs)
        Try
        
            For Each row In grdSummary.Rows
          

                strSql = ""
                strSql += "USP_NOC_Assessment_Print_details '" & Val(CType(row.FindControl("lblassessNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "'"
                ds = db.sub_GetDataSets(strSql)
                dt = ds.Tables(1)
                Dim Con_name As String = Trim(dt.Rows(0)("con_Name") & "")
                Dim addressI As String = Trim(dt.Rows(0)("AddressI") & "")
                Dim addressII As String = Trim(dt.Rows(0)("AddressII") & "")
                Dim BankName As String = Trim(dt.Rows(0)("BankName") & "")
                Dim AccountNo As String = Trim(dt.Rows(0)("AccountNo") & "")
                Dim BranchName As String = Trim(dt.Rows(0)("BranchName") & "")
                Dim IFSCCode As String = Trim(dt.Rows(0)("IFSCCode") & "")
                Dim GSTINComp As String = Trim(dt.Rows(0)("GSTIN") & "")
                Dim CINNo As String = Trim(dt.Rows(0)("CINNo") & "")
                Dim PANNo As String = Trim(dt.Rows(0)("PANNo") & "")
                Dim ConFor As String = Trim(dt.Rows(0)("Con_For") & "")
                Dim NoteI As String = Trim(dt.Rows(0)("NoteI") & "")
                Dim NoteII As String = Trim(dt.Rows(0)("NoteII") & "")
                Dim NoteIII As String = Trim(dt.Rows(0)("NoteIII") & "")
                Dim noteiv As String = Trim(dt.Rows(0)("noteiv") & "")
                Dim LCNO As String = Trim(dt.Rows(0)("LCNO") & "")

                Dim Swiftcode As String = Trim(dt.Rows(0)("Swiftcode") & "")
                Dim MICROCode As String = Trim(dt.Rows(0)("MICROCode") & "")

                dt1 = ds.Tables(0)

                Dim Assessnoprint As String = Trim(dt1.Rows(0)("AssessNo") & "")
                Dim assessdate As String = Trim(dt1.Rows(0)("AssessDate") & "")
                Dim GSTNAME As String = Trim(dt1.Rows(0)("GSTName") & "")
                Dim GSTADD As String = Trim(dt1.Rows(0)("GSTAddress") & "")
                Dim GSTIN As String = Trim(dt1.Rows(0)("GSTIn_uniqID") & "")
                Dim STATE As String = Trim(dt1.Rows(0)("State") & "")
                Dim STATECODE As String = Trim(dt1.Rows(0)("state_Code") & "")
                Dim CHAName As String = Trim(dt1.Rows(0)("CHAName") & "")
                Dim ImporterName As String = Trim(dt1.Rows(0)("ImporterName") & "")
                Dim NOCNo As String = Trim(dt1.Rows(0)("NOCNo") & "")
                Dim ValidUptoDate As String = Trim(dt1.Rows(0)("ValidUptoDate") & "")
                Dim IGMNo As String = Trim(dt1.Rows(0)("IGMNo") & "")
                Dim IGMItemNo As String = Trim(dt1.Rows(0)("IGMItemNo") & "")

                Dim NOCDate As String = Trim(dt1.Rows(0)("NOCDate") & "")
                Dim NOCExpiryDate As String = Trim(dt1.Rows(0)("NOCExpiryDate") & "")

                Dim BOENo As String = Trim(dt1.Rows(0)("BOENo") & "")
                Dim BOEDate As String = Trim(dt1.Rows(0)("BOEDate") & "")
                Dim Area As String = Trim(dt1.Rows(0)("Area") & "")
                Dim Qty As String = Trim(dt1.Rows(0)("Qty") & "")
                Dim Value As String = Trim(dt1.Rows(0)("Value") & "")
                Dim Duty As String = Trim(dt1.Rows(0)("duty") & "")

                Dim Weight As String = Trim(dt1.Rows(0)("Weight") & "")
                Dim UserName As String = Trim(dt1.Rows(0)("UserName") & "")
                Dim Today As String = Trim(dt1.Rows(0)("Today") & "")
                Dim SGST As String = Val(dt1.Rows(0)("sgst"))
                Dim CGST As String = Val(dt1.Rows(0)("cgst"))
                Dim IGST As String = Val(dt1.Rows(0)("igst"))
                Dim NetTotal As String = dt1.Rows(0)("NetTotal")
                Dim GrandTotal As String = dt1.Rows(0)("GrandTotal")
                Dim amtinword As String = dt1.Rows(0)("amtinword")

                Dim CargoType As String = Trim(dt1.Rows(0)("cargotype") & "")
                Dim CargoDescrp As String = Trim(dt1.Rows(0)("CargoDescrp") & "")
                Dim T20 As String = dt1.Rows(0)("T20")
                Dim T40 As String = dt1.Rows(0)("T40")
                Dim DeliveryType As String = dt1.Rows(0)("DeliveryType")
                Dim BondNo As String = Trim(dt1.Rows(0)("BondNo") & "")
                Dim BondDate As String = Trim(dt1.Rows(0)("BondDate") & "")
                Dim EXBOENo As String = Trim(dt1.Rows(0)("ExBoeNo") & "")
                Dim EXBOEDate As String = Trim(dt1.Rows(0)("ExBoedate") & "")
                Dim DeliveredQty As String = Trim(dt1.Rows(0)("DeliveredQty") & "")
                Dim EXBondDate As String = Trim(dt1.Rows(0)("EXBondDate") & "")
                Dim storagespace As String = Trim(dt1.Rows(0)("storagespace") & "")

                ''Dim Remarks As String = Trim(dt1.Rows(0)("Remarks") & "")

                Dim LastAssessNo As String = Trim(dt1.Rows(0)("LastAssessNo") & "")
                Dim LastAssessNo1 As String
                If LastAssessNo = "0" Then
                    LastAssessNo1 = ""
                Else
                    LastAssessNo1 = "Previous Invoice No : " + LastAssessNo
                End If
                Dim Remarks As String = Trim(dt1.Rows(0)("Remarks") & "")
                Dim TaxNote As String = Trim(dt1.Rows(0)("Note") & "")
                Dim InvoiceDate As String = Trim(dt1.Rows(0)("InvoiceDate") & "")
                Dim InBOENo As String = Trim(dt1.Rows(0)("InBOENo") & "")

                dt2 = ds.Tables(2)
                Dim datasource As New ReportDataSource("DataSet1", dt2)
                dt3 = ds.Tables(4)
                Dim datasource1 As New ReportDataSource("DataSet2", dt3)
                dt4 = ds.Tables(5)
                Dim datasource2 As New ReportDataSource("DataSet3", dt4)

                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.DataSources.Add(datasource)
                ReportViewer1.LocalReport.DataSources.Add(datasource1)
                ReportViewer1.LocalReport.DataSources.Add(datasource2)

                Dim p1 As New ReportParameter("Con_Name", Con_name)
                Dim p2 As New ReportParameter("AddressI", addressI)
                Dim p3 As New ReportParameter("AddressII", addressII)
                Dim p4 As New ReportParameter("BankName", BankName)
                Dim p5 As New ReportParameter("AccountNo", AccountNo)
                Dim p6 As New ReportParameter("BranchName", BranchName)
                Dim p7 As New ReportParameter("IFSCCOde", IFSCCode)
                Dim p8 As New ReportParameter("GSTINComp", GSTINComp)
                Dim p9 As New ReportParameter("CINNo", CINNo)
                Dim p10 As New ReportParameter("PANNo", PANNo)

                Dim p11 As New ReportParameter("AssessNo", Assessnoprint)
                Dim p12 As New ReportParameter("AssessDate", assessdate)
                Dim p13 As New ReportParameter("GSTNAME", GSTNAME)
                Dim p14 As New ReportParameter("GST_address", GSTADD)
                Dim p15 As New ReportParameter("GSTIN", GSTIN)
                Dim p16 As New ReportParameter("STATE", STATE)
                Dim p17 As New ReportParameter("STATECODE", STATECODE)
                Dim p18 As New ReportParameter("CHAName", CHAName)
                Dim p19 As New ReportParameter("ImporterName", ImporterName)
                Dim p20 As New ReportParameter("NOCNo", NOCNo)
                Dim p21 As New ReportParameter("ValidUptoDate", ValidUptoDate)
                Dim p22 As New ReportParameter("IGMNo", IGMNo)
                Dim p23 As New ReportParameter("NOCDate", NOCDate)
                Dim p24 As New ReportParameter("BOENo", BOENo)
                Dim p25 As New ReportParameter("Value", Value)
                Dim p26 As New ReportParameter("BOEDate", BOEDate)
                Dim p27 As New ReportParameter("Area", Area)
                Dim p28 As New ReportParameter("Qty", Qty)
                Dim p29 As New ReportParameter("Weight", Weight)
                Dim p30 As New ReportParameter("UserName", UserName)
                Dim p31 As New ReportParameter("Today", Today)
                Dim p32 As New ReportParameter("SGST", SGST)
                Dim p33 As New ReportParameter("CGST", CGST)
                Dim p34 As New ReportParameter("IGST", IGST)
                Dim p35 As New ReportParameter("NetTotal", NetTotal)
                Dim p36 As New ReportParameter("GrandTotal", GrandTotal)
                Dim p37 As New ReportParameter("amtinword", amtinword)
                Dim p38 As New ReportParameter("ConFor", ConFor)

                Dim p39 As New ReportParameter("NoteI", NoteI)
                Dim p40 As New ReportParameter("NoteII", NoteII)
                Dim p41 As New ReportParameter("Duty", Duty)
                Dim p42 As New ReportParameter("CargoType", CargoType)
                Dim p43 As New ReportParameter("CargoDescrp", CargoDescrp)
                Dim p44 As New ReportParameter("T20", T20)
                Dim p45 As New ReportParameter("T40", T40)
                Dim p46 As New ReportParameter("DeliveryType", DeliveryType)
                Dim p47 As New ReportParameter("BondNo", BondNo)
                Dim p48 As New ReportParameter("BondDate", BondDate)
                Dim p49 As New ReportParameter("EXBOENo", EXBOENo)
                Dim p50 As New ReportParameter("EXBOEDate", EXBOEDate)

                Dim p51 As New ReportParameter("Swiftcode", Swiftcode)
                Dim p52 As New ReportParameter("MICROCode", MICROCode)

                Dim p53 As New ReportParameter("LastAssessNo1", LastAssessNo1)
                Dim p54 As New ReportParameter("Remarks", Remarks)
                Dim p55 As New ReportParameter("DeliveredQty", DeliveredQty)
                Dim p56 As New ReportParameter("EXBondDate", EXBondDate)
                Dim p57 As New ReportParameter("NoteIII", NoteIII)
                Dim p58 As New ReportParameter("IGMItemNo", IGMItemNo)
                Dim p59 As New ReportParameter("storagespace", storagespace)
                Dim p60 As New ReportParameter("noteiv", noteiv)
                Dim p61 As New ReportParameter("NOCExpiryDate", NOCExpiryDate)
                Dim p62 As New ReportParameter("TaxNote", TaxNote)
                Dim p63 As New ReportParameter("LCNO", LCNO)
                Dim p64 As New ReportParameter("InvoiceDate", InvoiceDate)
                Dim p65 As New ReportParameter("InBOENo", InBOENo)

                Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65})
                ' Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33})

                '    'Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1})
                '    'lblerror.Text = "loaded3"
                'End If
                'Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1})
                'lblError.Text = "loaded3"
                '' Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4})

                'ReportViewer1.LocalReport.Refresh()
                deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"

                bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

                Response.ClearContent()

                Response.ClearHeaders()

                Response.ContentType = "application/pdf"

                Response.BinaryWrite(bytes)

                File.WriteAllBytes("D:\trackerweb\Navkar-Bond\Bond_PDF\AssessMentPrint_" & Val(CType(row.FindControl("lblassessNo"), Label).Text) & ".pdf", bytes)

                'Response.Flush()

                'Response.Close()
            Next
            'btnsearch_Click(sender, e)
            'lblsession.Text = "Assessment Cancelled Successfully"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            'UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
