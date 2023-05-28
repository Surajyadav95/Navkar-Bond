

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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/BondNoc.rdlc")

            Dim NOCNo As String = Request.QueryString("NOCNo")
            Session("Workyear") = Request.QueryString("WorkYear")

            'NOCNo = "3"
            ds = db.sub_GetDataSets("USP_NOC_details_NocNo '" & NOCNo & "'")

            Dim NOCDate As String = Trim(ds.Tables(0).Rows(0)("Date") & "")
            Dim ImporterName As String = Trim(ds.Tables(0).Rows(0)("ImporterName") & "")
            Dim BOENo As String = Trim(ds.Tables(0).Rows(0)("BOENo") & "")
            Dim BOEDate As String = Trim(ds.Tables(0).Rows(0)("BDate") & "")
            Dim IGMNo As String = Trim(ds.Tables(0).Rows(0)("IGMNo") & "")
            Dim IGMDate As String = Trim(ds.Tables(0).Rows(0)("IGDate") & "")
            If IGMNo = "" Then
                IGMDate = ""
            End If
            Dim StorageSpace As String = Trim(ds.Tables(0).Rows(0)("StorageSpace") & "")
            Dim ExPDate As String = Trim(ds.Tables(0).Rows(0)("ExPDate") & "")
            Dim CHAName As String = Trim(ds.Tables(0).Rows(0)("CHAName") & "")

            Dim ware_house_code As String = Trim(ds.Tables(1).Rows(0)("ware_house_code") & "")
            Dim con_Name As String = Trim(ds.Tables(1).Rows(0)("con_Name") & "")
            Dim AddressI As String = Trim(ds.Tables(1).Rows(0)("AddressI") & "")
            Dim AddressII As String = Trim(ds.Tables(1).Rows(0)("AddressII") & "")


            Dim p1 As New ReportParameter("NOCNo", NOCNo)
            Dim p2 As New ReportParameter("Date", NOCDate)
            Dim p3 As New ReportParameter("ImporterName", ImporterName)
            Dim p4 As New ReportParameter("BOENo", BOENo)
            Dim p5 As New ReportParameter("BDate", BOEDate)
            Dim p6 As New ReportParameter("IGMNo", IGMNo)
            Dim p7 As New ReportParameter("IGDate", IGMDate)
            Dim p8 As New ReportParameter("StorageSpace", StorageSpace)
            Dim p9 As New ReportParameter("ExPDate", ExPDate)
            Dim p10 As New ReportParameter("CHAName", CHAName)
            Dim p11 As New ReportParameter("ware_house_code", ware_house_code)
            Dim p12 As New ReportParameter("con_Name", con_Name)
            Dim p13 As New ReportParameter("AddressI", AddressI)
            Dim p14 As New ReportParameter("AddressII", AddressII)


            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14})

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
