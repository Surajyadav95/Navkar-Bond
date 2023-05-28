

Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Net.Mail

Partial Class Report_Estimation_Default
    Inherits System.Web.UI.Page
    Dim db As New dbOperation_bond_general
    ' Dim dt, dt1 As DataTable
    Dim strSql, strSql1 As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9 As DataTable
    ' Dim db As New dbOperation
    Dim ds, ds1, ds2, ds3 As DataSet
    Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    Dim streamids As String() = Nothing
    Dim mimeType As String = Nothing
    Dim encoding As String = Nothing
    Dim encoding1 As String = Nothing
    Dim extension As String = Nothing
    Dim filename As String
    Dim deviceInfo As String
    Dim strPDFFile As String
    Dim bytes As Byte()
    Dim dtpfrom As String
    Dim dtpto As String
    Dim SmtpServer As New SmtpClient()
    Dim mail As New MailMessage()
    'Dim lr As New Microsoft.Reporting.WebForms.LocalReport
    Private Sub PrintData(ByVal strTINo As String)
        Try
            btnExport_Click()
        Catch ex As Exception
            MsgBox("Error in procedure: " & System.Reflection.MethodBase.GetCurrentMethod.Name & vbCrLf & ex.Message.ToString)
        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Session("UserIDPRE") Is Nothing Then
        '    Session("UserIDPRE") = Request.Cookies("UserIDPRE").Value           
        'End If
        If Not IsPostBack Then
            txtvaldate.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy 08:00")
            'LoadReport()
           
            dtpfrom = DateTime.Now.ToString("dd-MM-yyyy 00:00:00")
            dtpto =  DateTime.Now.ToString("dd-MM-yyyy 23:59:59")   
            sub_Bond_InvoiceAutoMail()

        End If
    End Sub
    Protected Sub btnExport_Click()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim sw As New StringWriter()
        Dim hw As New HtmlTextWriter(sw)
        '  pnlPerson.RenderControl(hw)
        Dim sr As New StringReader(sw.ToString())
        'Dim pdfDoc As New XDocument(New Rectangle(288.0F, 144.0F), 10.0F, 10.0F, 30.0F, 30.0F)
        'pdfDoc.SetPageSize(PageSize.A4)
        'Dim htmlparser As New HTMLWorker(pdfDoc)
        'PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
        'pdfDoc.Open()
        'htmlparser.Parse(sr)
        'pdfDoc.Close()
        Response.Write(hw)
        Response.End()
    End Sub

    Private Sub LoadReport(ByVal strinvoice As String, ByVal strDate As Object, ByVal strDeliveryType As String, ByVal strinworkyear As String, ByVal strInvNo As String)
        Try
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/AutoMailBond.rdlc")

            Dim assessno As String = Request.QueryString("AssessNo")
            Dim workyear As String = Request.QueryString("WorkYear")
            ''assessno = "182"
            ''workyear = "2018-19"
            strSql = ""
            strSql += "USP_NOC_Assessment_Print_details '" & strinvoice & "','" & strinworkyear & "'"
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
            Dim SignedQRcode As String = Trim(dt1.Rows(0)("SignedQRcode") & "")
            Dim AckNo As String = Trim(dt1.Rows(0)("AckNo") & "")
            Dim Irn As String = Trim(dt1.Rows(0)("Irn") & "")
            Dim Ackdt As String = Trim(dt1.Rows(0)("Ackdt") & "")
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
            Dim p66 As New ReportParameter("SignedQRcode", SignedQRcode)
            Dim p67 As New ReportParameter("AckNo", AckNo)
            Dim p68 As New ReportParameter("Irn", Irn)
            Dim p69 As New ReportParameter("Ackdt", Ackdt)

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69})
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
            File.WriteAllBytes("D:\NavkarCFS\Navkar-Bond\ReportPDF\" & strinvoice & ".pdf", bytes)
            'File.WriteAllBytes("D:\NavkarCFS\Navkar-Bond\ReportPDF\BondAssessmentPrint.pdf", bytes)

            'Response.Flush()

            'Response.Close()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub


    Private Sub sub_Bond_InvoiceAutoMail()

        Dim dblno As Double
        Dim dblch As Double
        Dim stremaiid As String
        Dim strHeader As String
        Dim strFooter As String
        Dim strBody As String
        Dim strAddress As String
        Dim strDept As String
        Dim strTable As String
        Dim strText As String
        Dim strsmtpserver As String
        Dim strport As String
        Dim stremailfrom As String
        Dim strDate As String
        Dim dblnoofassessno As Double
        Dim dblgrandtotal As String
        Dim strpartyname As String
        Dim Strassess As String
        stremaiid = ""
        strHeader = ""
        strFooter = ""
        strBody = ""
        strAddress = ""
        Dim mm As New MailMessage
        Dim strAllPDFFile = ""
        Dim strAllInvoice = ""

        Dim dtDets As New DataTable
        strSql = " Execute USP_Party_Auto_Mail_INV '" & "Bond" & "' "
        dtDets = db.sub_GetDatatable(strSql)
        For i As Integer = 0 To dtDets.Rows.Count - 1
            Try
                dblnoofassessno = 0
                dblgrandtotal = 0
                strpartyname = ""
                strAllPDFFile = ""
                strHeader = ""
                strFooter = ""
                strAllInvoice = ""
                Dim strInvWorkyear = ""
                strAllPDFFile = ""
                strAllInvoice = ""
                Strassess = ""
                strFooter = ""
                strHeader = ""
                strHeader = ""
                strFooter = ""
                strAllInvoice = ""
                strInvWorkyear = ""
                strAllPDFFile = ""
                strAllInvoice = ""
                strAllPDFFile = ""
                strAllInvoice = ""
                strInvWorkyear = ""
                strAllPDFFile = ""
                strAllInvoice = ""

                Strassess = ""
                strAllPDFFile = ""
                strAllInvoice = ""
                strAllInvoice = ""

                Dim Dtfetch As New DataTable
                strSql = "Execute USP_INVOICE_FOR_AUTO_MAIL '" & Trim(dtDets.Rows(i)("Party")) & "', '" & Trim(dtDets.Rows(i)("Party_ID")) & "', '" & Convert.ToDateTime(Trim(dtpfrom)).ToString("dd-MMM-yyyy HH:mm") & "','" & Convert.ToDateTime(Trim(dtpto)).ToString("dd-MMM-yyyy HH:mm") & "', '" & "Bond" & "' "
                Dtfetch = db.sub_GetDatatable(strSql)
                If Dtfetch.Rows.Count > 0 Then
                    For F As Integer = 0 To Dtfetch.Rows.Count - 1
                        strpartyname = Trim(Dtfetch.Rows(F)("PartyName")) & "."
                        ' Call frmReports.sub_CleanOutViewer()
                        Call LoadReport(Val(Dtfetch.Rows(F)("AssessNo")), Format(Dtfetch.Rows(F)("assessDate"), "dd-MMM-yyyy"), Trim(Dtfetch.Rows(F)("assesstype")), Trim(Dtfetch.Rows(F)("workyear")), Trim(Dtfetch.Rows(F)("AssessNo")))
                        If Trim(strAllInvoice) = "" Then
                            strAllInvoice = Trim(Dtfetch.Rows(F)("AssessNo"))
                        Else
                            strAllInvoice = strAllInvoice & "," & Trim(Dtfetch.Rows(F)("AssessNo"))
                        End If
                        strInvWorkyear = Trim(Dtfetch.Rows(F)("WorkYear"))


                        If Trim(strAllPDFFile) = "" Then
                            strAllPDFFile = strPDFFile
                        Else
                            strAllPDFFile = strAllPDFFile & "," & strPDFFile
                        End If
                        Strassess = "'" & Trim(Dtfetch.Rows(F)("AssessNo")) & "'" & "," & Strassess


                    Next
                End If
                ' for mail
                'for sending mail
                If strAllInvoice <> "" Then

                    Dim dtMail As New DataTable, dtTemp As New DataTable, dtToID As New DataTable
                    Dim dblMailEntryID As Double = 0
                    Dim strMailDomain As String = "", strMailAdrs As String = "", strMailPswrd As String = "", intPortNo As Integer = 0
                    Dim strFrom As String = "", strMail_ToIDs As String = "", strMail_CcIDs As String = "", strMail_BccIDs As String = ""
                    Dim strToIDs As String = "", strCcIDs As String = "", strBccIDs As String = ""
                    Dim strAttachFilePath As String = "", strAttachFilePath_Org As String = "", strSubject As String = "", strBodyText As String = ""
                    Dim strsubjuct As String = ""

                    Dim varSplitTo() As String, varSplitCc() As String, varSplitBcc() As String
                    Dim ssl As Boolean = True
                    Dim lblid As String = ""
                    strSql = ""
                    strSql = "SELECT MailProcess, SMTPServer, SMTPServerPort, MailFromID, UserId, UserPassword, MailTo, MailCC,MailBCC "
                    strSql += " from mail_SETTINGS WHERE  Mailprocess='Bond'"
                    dtTemp = db.sub_GetDatatable(strSql)
                    strSql = ""

                    If dtTemp.Rows.Count > 0 Then
                        'lblid.Text = ""
                        lblid = Trim(dtDets.Rows(i)("Party_ID"))
                        strMailDomain = Trim(dtTemp.Rows(0)("smtpserver") & "")
                        intPortNo = Val(dtTemp.Rows(0)("SMTPServerPort") & "")
                        strFrom = Trim(dtTemp.Rows(0)("MailFromID") & "")
                        strMailPswrd = Trim(dtTemp.Rows(0)("userpassword") & "")
                        strMail_ToIDs = Trim(dtDets.Rows(i)("send_to") & "")
                        strMail_CcIDs = Trim(dtTemp.Rows(0)("MailCC") & "")
                        '  strMail_CcIDs = Trim(dtTemp.Rows(0)("MailCC") & "") & ";" & Trim(dtDets.Rows(i)("send_cc") & "")
                        strMail_BccIDs = Trim(dtTemp.Rows(0)("MailBCC") & "")
                        SmtpServer.Credentials = New  _
                        Net.NetworkCredential(strFrom, strMailPswrd)
                        SmtpServer.Port = intPortNo
                        SmtpServer.Host = strMailDomain
                        SmtpServer.EnableSsl = True
                        mail = New MailMessage()
                        mail.From = New MailAddress(strFrom, "Do_Not_Reply | Navkar Terminals")
                        'mail to 
                        varSplitTo = Split(strMail_ToIDs, ";")
                        For Each Toid As String In varSplitTo
                            If Trim(Toid) <> "" And Len(Trim(Toid)) >= 5 Then
                                If InStr(Toid, "@") > 0 Then
                                    mail.To.Add(Trim(Toid))
                                End If
                            End If
                        Next
                        'mail cc
                        If Trim(strMail_CcIDs) <> "" Then
                            varSplitCc = Split(strMail_CcIDs, ";")
                            For Each Ccid As String In varSplitCc
                                If Trim(Ccid) <> "" And Len(Trim(Ccid)) >= 5 Then
                                    If InStr(Ccid, "@") > 0 Then
                                        mail.CC.Add(Trim(Ccid))
                                    End If
                                End If
                            Next
                        End If
                        'mail BCC
                        If Trim(strMail_BccIDs) <> "" Then
                            varSplitCc = Split(strMail_BccIDs, ";")
                            For Each bCcid As String In varSplitCc
                                If Trim(bCcid) <> "" And Len(Trim(bCcid)) >= 5 Then
                                    If InStr(bCcid, "@") > 0 Then
                                        mail.Bcc.Add(Trim(bCcid))
                                    End If
                                End If
                            Next
                        End If
                        Try

                            strFooter = ""
                            strHeader = ""
                            strHeader = strHeader & "<font face='Verdana' color='black' size=2>Dear " & strpartyname & " </font> <br><br>"
                            strHeader = strHeader & "<font face='Verdana' color='black' size=2>Encl. invoices generated on " & Convert.ToDateTime(Trim(dtpfrom)).ToString("dd-MMM-yyyy HH:mm") & " fyi pl.</font><br><br>"

                            strHeader = strHeader & "<font face='Verdana' color='black' size=2>The subject Invoice/s is/are system generated, any dispute related to this </font>"
                            strHeader = strHeader & "<font face='Verdana' color='black' size=2>invoice/s should be forwarded to Bondbilling@navkarcfs.com/accounts@navkarcfs.com compulsorily with brief </font>"
                            strHeader = strHeader & "<font face='Verdana' color='black' size=2>reason within 3 days from receipt of this email.</font><br>"

                            strHeader = strHeader & "<font face='Verdana' color='black' size=2></font><BR>"
                            strHeader = strHeader & "<font face='Verdana' color='black' size=2>Thanks & Best Regards,</font><br>"
                            strHeader = strHeader & "<font face='Verdana' color='black' size=2>Export billing,</font><br>"
                            strFooter = strFooter & "<font face='Verdana' color='black' size=3>NAVKAR CORPORATION LTD</font><br>"
                            'strFooter = strFooter & "<font face='Verdana' color='black' size=2>Sales Office: ,</font><br>"
                            ''   strFooter = strFooter & "<font face='Verdana' color='black' size=2> survey No.44/1, 44/1/2 Village Tumb, Taluka Umbergoan, District Valsad, Gujarat, Pin No-396150.</font><br>"

                            'strFooter = strFooter & "<font face='Verdana' color='BLUE' size=2>Email ID     :- </font><br>"
                            strFooter = strFooter & "<font face='Verdana' color='black' size=2>Tel: +02143662525 - Ext. 522/523/524/316 </font><br>"
                            '  strFooter = strFooter & "<font face='Verdana' color='black' size=2>Web         :- http://</font><br>"
                            '  strFooter = strFooter & "<font face='Verdana' color='black' size=2>Tracking    :-Check Container tracking online </font><br>"
                            strFooter = strFooter & "<font face='Verdana' color='black' size=2>--------------------------------------------------------</font><br>"
                            strFooter = strFooter & "<font face='Verdana' color='black' size=2>--------------------------------------------------------</font><br>"

                            strFooter = strFooter & "<font face='arial' color='red' size=2>  ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & " </font> <br>"
                            strFooter = strFooter & "<font face='arial' color='red' size=2> <b>                    This is system generated message. If you have any queries please reply to  exportbilling@navkarcfs.com/accounts@navkarcfs.com" & "</b> </font> <br>"
                            strFooter = strFooter & "<font face='arial' color='red' size=2>  ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & " </font> <br> <br>"

                            mail.Subject = strsubjuct & "CFS YARD-III INVOICE: " & strpartyname & ""

                            ' mail.Subject = strsubjuct & "CFS YARD-II INVOICE: " & strpartyname & ""
                            'End If
                            mail.IsBodyHtml = True
                            mail.Body = strHeader & strFooter

                            ' Dim mailAttachment As New System.Net.Mail.Attachment(Trim(strAllPDFFile))
                            'attaching invoices copy of PDF
                            ''mail.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/ReportPDF/"), "" & filename & ".pdf")))
                            Dim arrayfile As New ArrayList
                            If Trim(strAllInvoice) <> "" Then
                                Dim varSplitFileName() As String
                                Dim intSBCount As Integer
                                varSplitFileName = Split(strAllInvoice, ",")
                                If Trim(strAllInvoice) <> "" Then
                                    varSplitCc = Split(strAllInvoice, ",")
                                    For Each Ccid As String In varSplitCc


                                        mail.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("filename"), "")))
                                        'End If

                                    Next
                                End If
                            End If


                            'If Trim(strAllInvoice) <> "" Then
                            '    Dim varSplitFileName() As String
                            '    Dim intSBCount As Integer
                            '    varSplitFileName = Split(strAllInvoice, ",")
                            '    If Trim(strAllInvoice) <> "" Then
                            '        varSplitCc = Split(strAllInvoice, ",")
                            '        For Each Ccid As String In varSplitCc
                            '            If Trim(strAllInvoice) <> "" And Len(Trim(strAllInvoice)) >= 5 Then
                            '                ' If InStr(strAllPDFFile, ",") > 0 Then
                            '                mail.Attachments.Add(New Attachment(Ccid))
                            '                'End If
                            '            End If
                            '        Next
                            '    End If
                            'End If

                            SmtpServer.Send(mail)
                            mail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess
                            strSql = ""
                            strSql = "INSERT INTO Invoice_AutoMailDets VALUES('" & "EXPORT" & "', '" & Convert.ToDateTime(Trim(dtpfrom)).ToString("dd-MMM-yyyy HH:mm") & "' , '" & Trim(dtDets.Rows(i)("Party_ID")) & "' , " & dblnoofassessno & " ," & Val(dblgrandtotal) & ")"
                            db.sub_ExecuteNonQuery(strSql)

                            Strassess = Mid(Strassess, 1, Len(Strassess) - 1)
                            strSql = ""
                            strSql = "UPDATE Bond_assessm SET IsSendMail=1  WHERE assessno in (" & Strassess & ") and IsSendMail=0"
                            db.sub_ExecuteNonQuery(strSql)
                            Strassess = ""
                        Catch ex As Exception

                            strSql = ""
                            strSql = "INSERT INTO AutoMailLog_Failed VALUES ('" & "Bond Invoice" & "','" & Trim(lblid) & "','" & Convert.ToDateTime(Trim(dtpfrom)).ToString("dd-MMM-yyyy HH:mm") & "','" & Replace(Err.Description, "'", "''") & "')"
                            db.sub_ExecuteNonQuery(strSql)
                            Continue For
                        End Try
                    End If
                End If
            Catch ex As Exception
                ''Call ErrorLog(Err.Number, "" & Me.Name & ", " & System.Reflection.MethodBase.GetCurrentMethod.Name & "", Err.Description)
                'strSql = ""
                'strSql = "INSERT INTO AutoMailLog_Failed VALUES ('" & "Bond Invoice" & "','" & Val(lblid) & "','" & Format(Now, "yyyy-MM-dd HH:mm") & "','" & Replace(Err.Description, "'", "''") & "')"
                'db.sub_ExecuteNonQuery(strSql)
                'Continue For
            End Try
            strHeader = ""
            strFooter = ""
            strAllInvoice = ""

            strAllPDFFile = ""
            strAllInvoice = ""
            Strassess = ""
            strFooter = ""
            strHeader = ""
            strHeader = ""
            strFooter = ""
            strAllInvoice = ""

            strAllPDFFile = ""
            strAllInvoice = ""
            strAllPDFFile = ""
            strAllInvoice = ""

            strAllPDFFile = ""
            strAllInvoice = ""

            Strassess = ""
            strAllPDFFile = ""
            strAllInvoice = ""
            strAllInvoice = ""
        Next
    End Sub
    Private Function ExportReportToPDF(path As String, reportName As String) As String

        Dim bytes As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, mimeType, encoding1, extension, streamids, warnings)
        filename = path & reportName
        Using fs = New System.IO.FileStream(filename, System.IO.FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
        End Using

        Return filename
    End Function
End Class
