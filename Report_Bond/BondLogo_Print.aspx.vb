

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

            If Not (Request.QueryString("NOCNo") = "" Or (Request.QueryString("WorkYear") = "")) Then
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

        Response.Write(hw)
        Response.End()
    End Sub
    Private Sub LoadReport()
        Try
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/BondLogo.rdlc")

            Dim NOCNo As String = Request.QueryString("NOCNo")
            Session("Workyear") = Request.QueryString("WorkYear")

            'NOCNo = "7"
            ds = db.sub_GetDataSets("USP_NOC_print_New '" & NOCNo & "'")

            Dim NOCDate As String = Trim(ds.Tables(0).Rows(0)("NocD") & "")
            Dim ImporterName As String = Trim(ds.Tables(0).Rows(0)("ImporterName") & "")
            Dim BOENo As String = Trim(ds.Tables(0).Rows(0)("BOENo") & "")
            Dim BOEDate As String = Trim(ds.Tables(0).Rows(0)("Boe") & "")
            Dim IGMNo As String = Trim(ds.Tables(0).Rows(0)("IGMNo") & "")
            Dim ITEMNO As String = Trim(ds.Tables(0).Rows(0)("itemno") & "")
            Dim cargotype As String = Trim(ds.Tables(0).Rows(0)("cargotype") & "")
            Dim IGMDate As String = Trim(ds.Tables(0).Rows(0)("IGMD") & "")
            If IGMNo = "" Then
                IGMDate = ""
            End If

            Dim StorageSpace As String = Trim(ds.Tables(0).Rows(0)("StorageSpace") & "")
            Dim ExPDate As String = Trim(ds.Tables(0).Rows(0)("Expiry") & "")
            Dim CHAName As String = Trim(ds.Tables(0).Rows(0)("CHAName") & "")
            Dim Qty As String = Trim(ds.Tables(0).Rows(0)("Qty") & "")
            ''Dim Unit As String = Trim(ds.Tables(0).Rows(0)("Unit") & "")
            Dim Days As String = Trim(ds.Tables(0).Rows(0)("Days") & "")
            Dim GrossWt As String = Trim(ds.Tables(0).Rows(0)("GrossWt") & "")
            Dim duty As String = Trim(ds.Tables(0).Rows(0)("duty") & "")
            Dim Commodity As String = Trim(ds.Tables(0).Rows(0)("Commodity") & "")
            Dim Value As String = Trim(ds.Tables(0).Rows(0)("Value") & "")
            Dim con_Name As String = Trim(ds.Tables(1).Rows(0)("con_Name") & "")
            Dim AddressI As String = Trim(ds.Tables(1).Rows(0)("AddressI") & "")
            Dim AddressII As String = Trim(ds.Tables(1).Rows(0)("AddressII") & "")
            Dim WRCode As String = Trim(ds.Tables(0).Rows(0)("WRCode") & "")

            Dim p1 As New ReportParameter("NOCNo", NOCNo)
            Dim p2 As New ReportParameter("NocD", NOCDate)
            Dim p3 As New ReportParameter("ImporterName", ImporterName)
            Dim p4 As New ReportParameter("BOENo", BOENo)
            Dim p5 As New ReportParameter("Boe", BOEDate)
            Dim p6 As New ReportParameter("IGMNo", IGMNo)
            Dim p7 As New ReportParameter("IGMD", IGMDate)
            Dim p8 As New ReportParameter("StorageSpace", StorageSpace)
            Dim p9 As New ReportParameter("Expiry", ExPDate)
            Dim p10 As New ReportParameter("CHAName", CHAName)
            Dim p11 As New ReportParameter("Qty", Qty)
            ' Dim p12 As New ReportParameter("Unit", Unit)
            Dim p12 As New ReportParameter("Days", Days)
            Dim p13 As New ReportParameter("GrossWt", GrossWt)
            Dim p14 As New ReportParameter("duty", duty)
            Dim p15 As New ReportParameter("Commodity", Commodity)
            Dim p16 As New ReportParameter("Value", Value)
            Dim p17 As New ReportParameter("con_Name", con_Name)
            Dim p18 As New ReportParameter("AddressI", AddressI)
            Dim p19 As New ReportParameter("AddressII", AddressII)
            Dim p20 As New ReportParameter("WRCode", WRCode)
            Dim p21 As New ReportParameter("ITEMNO", ITEMNO)
            Dim p22 As New ReportParameter("cargotype", cargotype)



            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22})

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
