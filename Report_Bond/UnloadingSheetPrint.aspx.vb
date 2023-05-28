

Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO

Partial Class Report_Estimation_Default
    Inherits System.Web.UI.Page
    Dim db As New dbOperation_bond_general

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
        If Not IsPostBack Then
            txtvaldate.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy 08:00")
            ''LoadReport()

            If Not (Request.QueryString("SheetNo") = "") Then
                LoadReport()
            End If
        End If
    End Sub
    Protected Sub btnExport_Click()
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf")
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Dim sw As New StringWriter()
        'Dim hw As New HtmlTextWriter(sw)
        ''  pnlPerson.RenderControl(hw)
        'Dim sr As New StringReader(sw.ToString())

        'Response.Write(hw)
        'Response.End()
    End Sub

    
    Private Sub LoadReport()
        Try


            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/UnloadingSheetPrint.rdlc")

            Dim SheetNo As String = Request.QueryString("SheetNo")

            ds = db.sub_GetDataSets("USP_UNLOADING_SHEET_PRINT " & SheetNo & "")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(1))
            Dim datasource1 As New ReportDataSource("DataSet2", ds.Tables(2))

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)
            ReportViewer1.LocalReport.DataSources.Add(datasource1)


            Dim EntryDate As String = Trim(ds.Tables(0).Rows(0)("EntryDate") & "")
            Dim BE_NO As String = Trim(ds.Tables(0).Rows(0)("BE_NO") & "")
            Dim BE_Date As String = Trim(ds.Tables(0).Rows(0)("BE_Date") & "")
            Dim WEEKS As String = Trim(ds.Tables(0).Rows(0)("WEEKS") & "")
            Dim EXPIRY_DATE As String = Trim(ds.Tables(0).Rows(0)("EXPIRY_DATE") & "")
            Dim ImporterName As String = Trim(ds.Tables(0).Rows(0)("ImporterName") & "")
            Dim CHAName As String = Trim(ds.Tables(0).Rows(0)("CHAName") & "")
            Dim agentName As String = Trim(ds.Tables(0).Rows(0)("agentName") & "")
            Dim BondNo As String = Trim(ds.Tables(0).Rows(0)("BondNo") & "")
            Dim BondDate As String = Trim(ds.Tables(0).Rows(0)("BondDate") & "")
            Dim Qty As String = Trim(ds.Tables(0).Rows(0)("Qty") & "")
            Dim Package As String = Trim(ds.Tables(0).Rows(0)("Package") & "")
            Dim AreaOccp As String = Trim(ds.Tables(0).Rows(0)("AreaOccp") & "")
            Dim Grosswt As String = Trim(ds.Tables(0).Rows(0)("Grosswt") & "")
            Dim value As String = Trim(ds.Tables(0).Rows(0)("value") & "")
            Dim Duty As String = Trim(ds.Tables(0).Rows(0)("Duty") & "")
            Dim WHName As String = Trim(ds.Tables(0).Rows(0)("WHName") & "")
            Dim noc_no As String = Trim(ds.Tables(0).Rows(0)("noc_no") & "")
            Dim equipment_name As String = Trim(ds.Tables(0).Rows(0)("equipment_name") & "")
            Dim surveyorName As String = Trim(ds.Tables(0).Rows(0)("surveyorName") & "")
            Dim trailername As String = Trim(ds.Tables(0).Rows(0)("trailername") & "")
            Dim username As String = Trim(ds.Tables(0).Rows(0)("username") & "")

            Dim con_Name As String = Trim(ds.Tables(3).Rows(0)("con_Name") & "")
            Dim AddressI As String = Trim(ds.Tables(3).Rows(0)("AddressI") & "")
            Dim AddressII As String = Trim(ds.Tables(3).Rows(0)("AddressII") & "")
            Dim Con_For As String = Trim(ds.Tables(3).Rows(0)("Con_For") & "")



            Dim p1 As New ReportParameter("SheetNo", SheetNo)
            Dim p2 As New ReportParameter("BE_NO", BE_NO)
            Dim p3 As New ReportParameter("BE_Date", BE_Date)
            Dim p4 As New ReportParameter("WEEKS", WEEKS)
            Dim p5 As New ReportParameter("EXPIRY_DATE", EXPIRY_DATE)
            Dim p6 As New ReportParameter("ImporterName", ImporterName)
            Dim p7 As New ReportParameter("CHAName", CHAName)
            Dim p8 As New ReportParameter("agentName", agentName)
            Dim p9 As New ReportParameter("BondNo", BondNo)
            Dim p10 As New ReportParameter("EntryDate", EntryDate)
            Dim p11 As New ReportParameter("BondDate", BondDate)
            Dim p12 As New ReportParameter("Qty", Qty)
            Dim p13 As New ReportParameter("Package", Package)
            Dim p14 As New ReportParameter("AreaOccp", AreaOccp)
            Dim p15 As New ReportParameter("Grosswt", Grosswt)
            Dim p16 As New ReportParameter("value", value)
            Dim p17 As New ReportParameter("Duty", Duty)
            Dim p18 As New ReportParameter("WHName", WHName)

            Dim p19 As New ReportParameter("con_Name", con_Name)
            Dim p20 As New ReportParameter("AddressI", AddressI)
            Dim p21 As New ReportParameter("AddressII", AddressII)
            Dim p22 As New ReportParameter("Con_For", Con_For)

            Dim p23 As New ReportParameter("noc_no", noc_no)
            Dim p24 As New ReportParameter("equipment_name", equipment_name)
            Dim p25 As New ReportParameter("surveyorName", surveyorName)
            Dim p26 As New ReportParameter("trailername", trailername)
            Dim p27 As New ReportParameter("username", username)



            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27})

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


End Class
