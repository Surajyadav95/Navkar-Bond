﻿

Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO

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

    Dim extension As String = Nothing

    Dim deviceInfo As String

    Dim bytes As Byte()

    Dim lr As New Microsoft.Reporting.WebForms.LocalReport
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
            If Not (Request.QueryString("AssessNo") = "" Or (Request.QueryString("WorkYear") = "")) Then
                LoadReport()
            End If
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

    
    Private Sub LoadReport()
        Try


            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/BondNOCAssessmentPrint.rdlc")

            Dim assessno As String = Request.QueryString("AssessNo")
            Dim workyear As String = Request.QueryString("WorkYear")
            'assessno = "181"
            'workyear = "2018-19"
            strSql = ""
            strSql += "USP_NOC_Assessment_Print_details '" & assessno & "','" & workyear & "'"
            ds = db.sub_GetDataSets(strSql)
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

            Dim CargoType As String = dt1.Rows(0)("cargotype")
            Dim CargoDescrp As String = dt1.Rows(0)("CargoDescrp")
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
            dt2 = ds.Tables(2)
            Dim datasource As New ReportDataSource("DataSet1", dt2)

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)

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

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53})
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

            Response.Flush()

            Response.Close()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    '    Protected Sub btnshow_Click()
    '        Try

    '            Dim dbljono As Double = 0, dblIGMQty As Double = 0, dblGPQty As Double = 0, strcontainer As String = ""
    '            Dim strperc As String = ""
    '            Dim intOutCount As Double, dblwt As Double = 0, strdays As String = ""
    '            strSql += "delete temp_pi_import where userid=" & Session("UserIDPRE") & ""
    '            db.sub_ExecuteNonQuery(strSql)

    '            strSql = ""
    '            strSql += "USP_Show_Button_SP '" & Trim(txtigm.Text) & "','" & Trim(txtitem.Text) & "'," & Session("ID") & ""
    '            ds = db.sub_GetDataSets(strSql)

    '            If ds.Tables(1).Rows.Count > 0 Then
    '                '  txtblno.Text = Trim(ds.Tables(1).Rows(0)("IGM_BLNo") & "")
    '                For i = 0 To ds.Tables(1).Rows.Count - 1
    '                    dbljono = Trim(ds.Tables(1).Rows(i)("JONo"))
    '                    strcontainer = Trim(ds.Tables(1).Rows(i)("ContainerNo"))
    '                    dblwt = Trim(ds.Tables(1).Rows(i)("IGM_MT_Wt"))

    '                    strSql = ""
    '                    strSql += "USP_Show_Button_containers '" & Trim(txtigm.Text) & "','" & Trim(txtitem.Text) & "','" & dbljono & "','" & strcontainer & "','" & Convert.ToDateTime(txtvaldate.Text & "").ToString("yyyy/MM/dd") & "'"
    '                    ds1 = db.sub_GetDataSets(strSql)
    '                    'If Trim(ddlItemGroup.SelectedItem.Text & "") = "Destuff" Then
    '                    '    dblIGMQty = Val(ds1.Tables(0).Rows(0)(0))
    '                    '    dblGPQty = Val(ds1.Tables(1).Rows(0)(0))
    '                    '    If dblIGMQty - dblGPQty = 0 Then
    '                    '        GoTo nextrecord
    '                    '    End If
    '                    'End If
    '                    'If ds1.Tables(2).Rows.Count > 0 Then
    '                    '    If Trim(ds1.Tables(2).Rows(0)("IsSC") & "") = True Then
    '                    '        strperc = "SC"
    '                    '    ElseIf Trim(ds1.Tables(2).Rows(0)("IsCE") & "") = True Then
    '                    '        strperc = Trim(ds1.Tables(2).Rows(0)("DestuffPerc") & "")
    '                    '    End If
    '                    'End If
    '                    'If Trim(ddlItemGroup.SelectedItem.Text & "") <> "LCL" Then
    '                    '    intOutCount = Trim(ds1.Tables(3).Rows(0)(0) & "")
    '                    'Else
    '                    '    intOutCount = 0
    '                    'End If
    '                    If intOutCount = 0 Then
    '                        If ds1.Tables(4).Rows.Count > 0 Then
    '                            'strdays = DateDiff("D", Convert.ToDateTime(ds1.Tables(4).Rows(0)("InDate1") & "").ToString("dd-MMM-yyyy HH:mm"), Convert.ToDateTime(txtvaldate.Text & "").ToString("dd-MMM-yyyy HH:mm"))
    '                            strSql = ""
    '                            strSql += "USP_insert_into_temp_pi_import " & dbljono & ",'" & strcontainer & "','" & Trim(ds1.Tables(4).Rows(0)("Size") & "") & "',"
    '                            strSql += "'" & Trim(ds1.Tables(4).Rows(0)("Cargotype") & "") & "','" & Trim(ds1.Tables(4).Rows(0)("InDate") & "") & "','" & dblwt & "','" & ds1.Tables(4).Rows(0)("days") & "','" & strperc & "'," & Trim(ds1.Tables(4).Rows(0)("IsScan") & "") & ",'" & Trim(ds1.Tables(4).Rows(0)("ScanType") & "") & "'," & Session("UserIDPRE") & ""
    '                            db.sub_ExecuteNonQuery(strSql)
    '                        End If
    '                        'Else
    '                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container is imported');", True)
    '                        '    Exit Sub
    '                    End If
    'nextrecord:
    '                Next
    '            Else
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found');", True)
    '                Exit Sub
    '            End If



    '        Catch ex As Exception
    '            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '        End Try
    '    End Sub
End Class
