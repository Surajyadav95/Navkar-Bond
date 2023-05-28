

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
            LoadReport()
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

        Response.Write(hw)
        Response.End()
    End Sub

    
    Private Sub LoadReport()
        Try
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/BondNotice2.rdlc")

            'GPNO = "1"
            ds = db.sub_GetDataSets("USP_BONDNOTICE_PRINT_NOTICE '" & Session("UserId_BondCFS") & "'")

            Dim Importer As String = Trim(ds.Tables(0).Rows(0)("Importer") & "")
            Dim ImporterAddress As String = Trim(ds.Tables(0).Rows(0)("ImporterAddress") & "")
            Dim Description As String = Trim(ds.Tables(0).Rows(0)("Description") & "")
            Dim BQty As String = Trim(ds.Tables(0).Rows(0)("BQty") & "")
            Dim boeno As String = Trim(ds.Tables(0).Rows(0)("boeno") & "")
            Dim boedate As String = Trim(ds.Tables(0).Rows(0)("boedate") & "")

            Dim BondNo As String = Trim(ds.Tables(0).Rows(0)("BondNo") & "")
            Dim bonddate As String = Trim(ds.Tables(0).Rows(0)("bonddate") & "")
            Dim BondExpDate As String = Trim(ds.Tables(0).Rows(0)("BondExpDate") & "")
            Dim CHA As String = Trim(ds.Tables(0).Rows(0)("CHA") & "")
            Dim BDuty As String = Trim(ds.Tables(0).Rows(0)("BDuty") & "")
            Dim con_Name As String = Trim(ds.Tables(1).Rows(0)("con_Name") & "")
            Dim PrintDate As String = Trim(ds.Tables(0).Rows(0)("PrintDate") & "")

            Dim p1 As New ReportParameter("Importer", Importer)
            Dim p2 As New ReportParameter("ImporterAddress", ImporterAddress)
            Dim p3 As New ReportParameter("Description", Description)
            Dim p4 As New ReportParameter("BQty", BQty)
            Dim p5 As New ReportParameter("boeno", boeno)
            Dim p6 As New ReportParameter("boedate", boedate)

            Dim p7 As New ReportParameter("BondNo", BondNo)
            Dim p8 As New ReportParameter("bonddate", bonddate)
            Dim p9 As New ReportParameter("BondExpDate", BondExpDate)

            Dim p10 As New ReportParameter("BDuty", BDuty)
            Dim p11 As New ReportParameter("CHA", CHA)
            Dim p12 As New ReportParameter("con_Name", con_Name)
            Dim p13 As New ReportParameter("PrintDate", PrintDate)

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13})

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
