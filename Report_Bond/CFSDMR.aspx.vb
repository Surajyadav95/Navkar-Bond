Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Security
Imports System.IO
Imports System.Xml
Imports System.Security.Cryptography.X509Certificates
Imports Microsoft.Reporting.WebForms
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel

Partial Class FG_DailyActivityPDF
    Inherits System.Web.UI.Page
    Dim db, db1, db2, db3 As New dbOperation_bond_general
    'Dim db1 As New dbOperation_OpuusDAR
    Dim dt, dt1, dtprod, dtPO, dtSample, dtReplace, dtPOGrp, dtmachine, dtProPulse, dtRejection, dtClGraph, dtDept, dtmachinedowngraph,
        dtGRN, dtQc, dtProj As DataTable
    Dim ds, dtqtly, dtCust, dtPI, dtPIGraph, dtWOGraph, dtPIMomthly, dtWOMonGraph, dtWOYearGraph, dtTIGraph,
        dtTIGraphM, dtTIGraphY, dsProduc, dsExpPI, dsExpWO, dsExpTI, dtFuelDetails, dtPodetails, dsgraph, dsPIConv, dsTarget, dsPIExp, dsWoExp, dsTIExp, dsTargetCo, dsFireGraph As DataSet
    Dim ed As New clsEncodeDecode
    Dim dtSPID As New DataTable
    Dim dtTempI As New DataTable
    Dim strDesc As String = ""
    Dim strtable As String = ""
    Dim encoding1 As String = Nothing
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
    Dim ToDate As String
    Dim lr As New Microsoft.Reporting.WebForms.LocalReport
    Private Sub PrintData(ByVal strTINo As String)
        Try
            ' btnExport_Click()
        Catch ex As Exception
            lblerror.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Not (Request.QueryString("Receptno") = "") Then
            ToDate = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            LoadReport()
            'End If
        End If
    End Sub

    Private Sub LoadReport()
        Try
            Dim strsql As String = ""
            Dim SLIPID As String
            Dim ds1, ds2, ds3, ds4, ds5, ds6, ds7, ds8, ds9, ds10, ds11, ds12, ds13 As DataSet
            '  Dim ReciptNo As String = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("Receptno")))
            ' Dim userid As Integer = "98"
            Dim FromDate As String = Request.QueryString("FromDate")
            Dim ToDate As String = Request.QueryString("ToDate")

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/CFSDMRPdf1.rdlc")


            '  dtqtly = db.sub_GetDataSets("USP_Graph_QualityComplaint ")

            ds = db.sub_GetDataSets("USP_CFS_Billing_DMR")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(0))

            ds1 = db.sub_GetDataSets("USP_CFS_Billing_DMR_NCL_I")

            Dim datasource1 As New ReportDataSource("NCL1", ds1.Tables(0))

            ds2 = db.sub_GetDataSets("USP_CFS_Billing_DMR_NCL_II")

            Dim datasource2 As New ReportDataSource("NCL2", ds2.Tables(0))

            ds3 = db.sub_GetDataSets("USP_CFS_Billing_DMR_NCL_III")

            Dim datasource3 As New ReportDataSource("NCL3", ds3.Tables(0))

            ds4 = db.sub_GetDataSets("USP_CFS_Billing_DMR_LastSixMonths")

            Dim datasource4 As New ReportDataSource("Combined", ds4.Tables(0))
            Dim datasource8 As New ReportDataSource("YardTotal", ds4.Tables(1))

            ds5 = db.sub_GetDataSets("USP_CFS_Billing_DMR_LastSixMonths_Yard_I")

            Dim datasource5 As New ReportDataSource("YardI", ds5.Tables(0))
            Dim datasource9 As New ReportDataSource("YardTotaLI", ds5.Tables(1))

            ds6 = db.sub_GetDataSets("USP_CFS_Billing_DMR_LastSixMonths_Yard_II")

            Dim datasource6 As New ReportDataSource("YardII", ds6.Tables(0))
            Dim datasource10 As New ReportDataSource("YardTotaLII", ds6.Tables(1))

            ds7 = db.sub_GetDataSets("USP_CFS_Billing_DMR_LastSixMonths_Yard_III")

            Dim datasource7 As New ReportDataSource("YardIII", ds7.Tables(0))
            Dim datasource11 As New ReportDataSource("YardTotaLIII", ds7.Tables(1))

            ds8 = db.sub_GetDataSets("USP_CFS_Billing_DMR_Collection_D")

            Dim datasource12 As New ReportDataSource("ColloctionAll", ds8.Tables(0))

            ds9 = db.sub_GetDataSets("USP_CFS_Billing_DMR_Collection_D_I")

            Dim datasource13 As New ReportDataSource("ColloctionI", ds9.Tables(0))

            ds10 = db.sub_GetDataSets("USP_CFS_Billing_DMR_Collection_D_II")

            Dim datasource14 As New ReportDataSource("ColloctionII", ds10.Tables(0))

            ds11 = db.sub_GetDataSets("USP_CFS_Billing_DMR_Collection_D_III")

            Dim datasource15 As New ReportDataSource("ColloctionIII", ds11.Tables(0))

            ds12 = db.sub_GetDataSets("USP_CFS_DMR_OS_KDM_WISE")

            Dim datasource16 As New ReportDataSource("Outstanding", ds12.Tables(0))


            ds13 = db.sub_GetDataSets("USP_CFS_DMR_OS_KDM_WISE")

            Dim datasource17 As New ReportDataSource("kdm", ds13.Tables(1))
            Dim InvoiceDate As String = Trim(ds.Tables(0).Rows(0)("Date") & "")
            Dim Dates As String = Trim(ds.Tables(0).Rows(0)("Dates") & "")
            Dim DateT As String = Trim(ds11.Tables(0).Rows(0)("DateT") & "")

            dsFireGraph = db.sub_GetDataSets("USP_OUTStanding_Statement_all_Summary'" & 0 & "','" & DateT & "'")
            dsPIConv = db.sub_GetDataSets("USP_CFS_OPS_DMR_Yard_I")
            Dim datasource18 As New ReportDataSource("OutstandingReport", dsFireGraph.Tables(0))

            Dim datasource19 As New ReportDataSource("Description", dsPIConv.Tables(0))
            Dim datasource20 As New ReportDataSource("Description1", dsPIConv.Tables(1))
            Dim datasource21 As New ReportDataSource("Description2", dsPIConv.Tables(2))
            Dim datasource22 As New ReportDataSource("Description3", dsPIConv.Tables(3))
            Dim datasource23 As New ReportDataSource("Description31", dsPIConv.Tables(4))
            Dim datasource24 As New ReportDataSource("Description311", dsPIConv.Tables(5))

            dsTargetCo = db.sub_GetDataSets("USP_CFS_OPS_DMR_Yard_II")

            Dim datasource25 As New ReportDataSource("Description4", dsTargetCo.Tables(0))
            Dim datasource26 As New ReportDataSource("Description11", dsTargetCo.Tables(1))
            Dim datasource27 As New ReportDataSource("Description21", dsTargetCo.Tables(2))
            Dim datasource28 As New ReportDataSource("Description32", dsTargetCo.Tables(3))
            Dim datasource29 As New ReportDataSource("Description312", dsTargetCo.Tables(4))
            Dim datasource30 As New ReportDataSource("Description3111", dsTargetCo.Tables(5))

            dtWOMonGraph = db.sub_GetDataSets("USP_CFS_OPS_DMR")

            Dim datasource31 As New ReportDataSource("Description41", dtWOMonGraph.Tables(0))
            Dim datasource32 As New ReportDataSource("Description111", dtWOMonGraph.Tables(1))
            Dim datasource33 As New ReportDataSource("Description211", dtWOMonGraph.Tables(2))
            Dim datasource34 As New ReportDataSource("Description321", dtWOMonGraph.Tables(3))
            Dim datasource35 As New ReportDataSource("Description3121", dtWOMonGraph.Tables(4))
            Dim datasource36 As New ReportDataSource("Description31111", dtWOMonGraph.Tables(5))

            dtPodetails = db.sub_GetDataSets("USP_ICD_PO_DMR")

            dtFuelDetails = db.sub_GetDataSets("USP_ICD_Fuel_DMR")
            dtqtly = db.sub_GetDataSets("USP_DAR_CREDIT_summary")
            dtCust = db.sub_GetDataSets("USP_DAR_CREDIT_summary_I")
            dtPI = db.sub_GetDataSets("USP_DAR_CREDIT_summary_II")
            dtPIGraph = db.sub_GetDataSets("USP_DAR_CREDIT_summary_III")
            dsExpTI = db.sub_GetDataSets("USP_ICD_DAR")
            Dim AutoID As String = "999999"
            Dim Mail As String = "All"
            dtWOGraph = db.sub_GetDataSets("USP_AGING_CUST_WISE_OS_Summary '" & AutoID & "','" & DateT & "'")
            Dim datasource37 As New ReportDataSource("PoDetails", dtPodetails.Tables(0))
            Dim datasource38 As New ReportDataSource("FuelDetails", dtFuelDetails.Tables(0))
            Dim datasource39 As New ReportDataSource("CombinedCredit", dtqtly.Tables(0))
            Dim datasource40 As New ReportDataSource("CombinedCredit1", dtCust.Tables(0))
            Dim datasource41 As New ReportDataSource("CombinedCredit11", dtPI.Tables(0))
            Dim datasource42 As New ReportDataSource("CombinedCredit12", dtPIGraph.Tables(0))
            Dim datasource43 As New ReportDataSource("OutStaindingCFS", dtWOGraph.Tables(0))

            Dim datasource44 As New ReportDataSource("ICDDAR", dsExpTI.Tables(0))
            Dim datasource45 As New ReportDataSource("ThisMonthICD", dsExpTI.Tables(1))
            Dim datasource46 As New ReportDataSource("JanMonthICD", dsExpTI.Tables(2))
            Dim datasource47 As New ReportDataSource("lastMonth24MonthICD", dsExpTI.Tables(3))
            Dim datasource48 As New ReportDataSource("HoursMonthICD12", dsExpTI.Tables(4))
            Dim datasource49 As New ReportDataSource("PortpendencyMonthICD", dsExpTI.Tables(5))
            Dim datasource50 As New ReportDataSource("INVENTORYICD", dsExpTI.Tables(6))
            Dim datasource51 As New ReportDataSource("Last24HoursDeliverycfs", dsExpTI.Tables(7))
            Dim MonthName As String = Trim(dtWOMonGraph.Tables(6).Rows(0)("MonthName") & "")
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)
            ReportViewer1.LocalReport.DataSources.Add(datasource1)
            ReportViewer1.LocalReport.DataSources.Add(datasource2)
            ReportViewer1.LocalReport.DataSources.Add(datasource3)
            ReportViewer1.LocalReport.DataSources.Add(datasource4)
            ReportViewer1.LocalReport.DataSources.Add(datasource5)
            ReportViewer1.LocalReport.DataSources.Add(datasource6)
            ReportViewer1.LocalReport.DataSources.Add(datasource7)
            ReportViewer1.LocalReport.DataSources.Add(datasource8)
            ReportViewer1.LocalReport.DataSources.Add(datasource9)
            ReportViewer1.LocalReport.DataSources.Add(datasource10)
            ReportViewer1.LocalReport.DataSources.Add(datasource11)
            ReportViewer1.LocalReport.DataSources.Add(datasource12)
            ReportViewer1.LocalReport.DataSources.Add(datasource13)
            ReportViewer1.LocalReport.DataSources.Add(datasource14)
            ReportViewer1.LocalReport.DataSources.Add(datasource15)
            ReportViewer1.LocalReport.DataSources.Add(datasource16)
            ReportViewer1.LocalReport.DataSources.Add(datasource17)
            ReportViewer1.LocalReport.DataSources.Add(datasource18)
            ReportViewer1.LocalReport.DataSources.Add(datasource19)
            ReportViewer1.LocalReport.DataSources.Add(datasource20)
            ReportViewer1.LocalReport.DataSources.Add(datasource21)
            ReportViewer1.LocalReport.DataSources.Add(datasource22)
            ReportViewer1.LocalReport.DataSources.Add(datasource23)
            ReportViewer1.LocalReport.DataSources.Add(datasource24)
            ReportViewer1.LocalReport.DataSources.Add(datasource25)
            ReportViewer1.LocalReport.DataSources.Add(datasource26)
            ReportViewer1.LocalReport.DataSources.Add(datasource27)
            ReportViewer1.LocalReport.DataSources.Add(datasource28)
            ReportViewer1.LocalReport.DataSources.Add(datasource29)
            ReportViewer1.LocalReport.DataSources.Add(datasource30)
            ReportViewer1.LocalReport.DataSources.Add(datasource31)
            ReportViewer1.LocalReport.DataSources.Add(datasource32)
            ReportViewer1.LocalReport.DataSources.Add(datasource33)
            ReportViewer1.LocalReport.DataSources.Add(datasource34)
            ReportViewer1.LocalReport.DataSources.Add(datasource35)
            ReportViewer1.LocalReport.DataSources.Add(datasource36)
            ReportViewer1.LocalReport.DataSources.Add(datasource37)
            ReportViewer1.LocalReport.DataSources.Add(datasource38)
            ReportViewer1.LocalReport.DataSources.Add(datasource39)
            ReportViewer1.LocalReport.DataSources.Add(datasource40)
            ReportViewer1.LocalReport.DataSources.Add(datasource41)
            ReportViewer1.LocalReport.DataSources.Add(datasource42)
            ReportViewer1.LocalReport.DataSources.Add(datasource43)
            ReportViewer1.LocalReport.DataSources.Add(datasource44)
            ReportViewer1.LocalReport.DataSources.Add(datasource45)
            ReportViewer1.LocalReport.DataSources.Add(datasource46)
            ReportViewer1.LocalReport.DataSources.Add(datasource47)
            ReportViewer1.LocalReport.DataSources.Add(datasource48)
            ReportViewer1.LocalReport.DataSources.Add(datasource49)
            ReportViewer1.LocalReport.DataSources.Add(datasource50)
            ReportViewer1.LocalReport.DataSources.Add(datasource51)
            Dim p1 As New ReportParameter("InvoiceDate", InvoiceDate)
            Dim p2 As New ReportParameter("Dates", Dates)
            Dim p3 As New ReportParameter("DateT", DateT)
            Dim P4 As New ReportParameter("MonthName", MonthName)

            ' btnExport_Click()
            'ReportViewer1.LocalReport.Refresh()
            ' lr.ReportPath = "Report1.rdlc"
            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, P4})

            Dim Proses As String = "M-DMR"
            strsql = ""
            strsql += "USP_VALIDATION_DMR_MAILS '" & Proses & "'"
            dtSPID = db.sub_GetDatatable(strsql)
            If dtSPID.Rows.Count > 0 Then
                strText = ""
                strText = "<font face='Segoe UI' color='black' size=2>A DMR, &lt; " & Trim(dtSPID.Rows(0)("Mailprocess")) & " &gt; has been generated at " & Trim(dtSPID.Rows(0)("Yard")) & "</font> <br>"


                strtable += "<html><body>"
                strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black' style='width: 100%; height: auto;'>  "
                strtable += "<tr bgcolor='white'><font face='Segoe UI' color='black' size='2'>"
                strtable += "<td style='padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'>Greetings from tracker,"
                strtable += "<br>"
                strtable += "<br>"
                strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black' style='width: 100%; height: auto;'>  "
                strtable = strtable & "<font face='Segoe UI' color='black' size=2>Please find attached <b>NCL Daily Activity Report </b> for " & Trim(dtSPID.Rows(0)("Result")) & " " & Trim(dtSPID.Rows(0)("Date")) & ". </font> "
                strtable += "<br>"
                strtable += "<br>"
                strtable = strtable & "<font face='Segoe UI' color='black' size=2>This is an auto generated e-mail. Please do not reply to this email.</font> "
                strtable += "<br>"
                strtable = strtable & "<font face='Segoe UI' color='black' size=2>Please contact your System Administrator OR your tracker team, in case of any issues or comments.</font> "
                strtable += "<br>"
                strtable += "<font face='calibri' color='black' size=3>"

                strtable += "<br>"
                strtable += "<font face='Segoe UI' color='black' size=2>Best Regards .<br>"
                ' strtable += "[" & trim(dtspid.rows(ispid)("person name")) & "]</font>"
                'strtable += "&lt; " & Trim(dtSPID.Rows(0)("regards")) & " &gt; </font>"


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

                strSubject = "" & Trim(dtSPID.Rows(0)("strSubject")) & ""
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
                mm.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "" & Trim(dtSPID.Rows(0)("File Name")) & ".pdf")))
                mm.IsBodyHtml = True
                mm.From = mailAddress
                Dim smtpClient As New SmtpClient(strmaildomain, intPortNo)
                Dim credentials As New System.Net.NetworkCredential(strFrom, strMailPswrd)
                smtpClient.EnableSsl = True
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = credentials
                smtpClient.Send(mm)


                '*******************************************for separate sending  +
                mm.To.Clear()
                mm.CC.Clear()
                strToIDs = ""

                strsql = ""
                strsql = "select MailTo from Mail_Settings where MailProcess='separateMIS'"
                dtDept = db.sub_GetDatatable(strsql)
                strToIDs = Trim(dtDept.Rows(0)("MailTo"))
                If Trim(strToIDs) <> "" Then
                    varSplitTo = Split(strToIDs, ";")
                    For Each toidnew As String In varSplitTo
                        If Trim(toidnew) <> "" And Len(Trim(toidnew)) >= 5 Then
                            If InStr(toidnew, "@") > 0 Then
                                mm.To.Add(Trim(toidnew))
                            End If
                        End If
                    Next
                End If
                mm.Subject = strSubject
                mm.Body = strBodyText
                'mm.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/cfsdmr/"), "" & Trim(dtSPID.Rows(0)("file name")) & ".PDF")))
                mm.IsBodyHtml = True
                mm.From = mailAddress
                smtpClient.EnableSsl = True
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = credentials
                smtpClient.Send(mm)

                'sending separate mail end
                mm.To.Clear()
                mm.CC.Clear()
                strToIDs = ""

                strsql = ""
                strsql = "select MailTo from Mail_Settings where MailProcess='separateMISII'"
                dtDept = db.sub_GetDatatable(strsql)
                strToIDs = Trim(dtDept.Rows(0)("MailTo"))
                If Trim(strToIDs) <> "" Then
                    varSplitTo = Split(strToIDs, ";")
                    For Each toidnew As String In varSplitTo
                        If Trim(toidnew) <> "" And Len(Trim(toidnew)) >= 5 Then
                            If InStr(toidnew, "@") > 0 Then
                                mm.To.Add(Trim(toidnew))
                            End If
                        End If
                    Next
                End If
                mm.Subject = strSubject
                mm.Body = strBodyText
                'mm.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/cfsdmr/"), "" & Trim(dtSPID.Rows(0)("file name")) & ".PDF")))
                mm.IsBodyHtml = True
                mm.From = mailAddress
                smtpClient.EnableSsl = True
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = credentials
                smtpClient.Send(mm)
                '**********************************

                mm.To.Clear()
                mm.CC.Clear()
                strToIDs = ""

                strsql = ""
                strsql = "select MailTo from Mail_Settings where MailProcess='separateMISIII'"
                dtDept = db.sub_GetDatatable(strsql)
                strToIDs = Trim(dtDept.Rows(0)("MailTo"))
                If Trim(strToIDs) <> "" Then
                    varSplitTo = Split(strToIDs, ";")
                    For Each toidnew As String In varSplitTo
                        If Trim(toidnew) <> "" And Len(Trim(toidnew)) >= 5 Then
                            If InStr(toidnew, "@") > 0 Then
                                mm.To.Add(Trim(toidnew))
                            End If
                        End If
                    Next
                End If
                mm.Subject = strSubject
                mm.Body = strBodyText
                'mm.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/cfsdmr/"), "" & Trim(dtSPID.Rows(0)("file name")) & ".PDF")))
                mm.IsBodyHtml = True
                mm.From = mailAddress
                smtpClient.EnableSsl = True
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = credentials
                smtpClient.Send(mm)

                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Mail Sent successfully');", True)
                'btnSave_Click(Sender, e)
            End If
            deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"

            bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

            Response.ClearContent()

            Response.ClearHeaders()

            Response.ContentType = "application/pdf"

            Response.BinaryWrite(bytes)

            Response.Flush()

            Response.Close()
        Catch ex As Exception
            lblerror.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
    Private Function ExportReportToPDF(path As String, reportName As String) As String

        Dim bytes As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, mimeType, encoding1, extension, streamids, warnings)
        Dim filename As String = path & reportName
        Using fs = New System.IO.FileStream(filename, System.IO.FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
        End Using

        Return filename
    End Function
End Class
