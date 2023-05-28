

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

            If Not (Request.QueryString("GPNO") = "" Or (Request.QueryString("WorkYear") = "")) Then
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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/GateLogo.rdlc")

            Dim GPNO As String = Request.QueryString("GPNO")
            Session("Workyear") = Request.QueryString("WorkYear")

            'GPNO = "1"
            ds = db.sub_GetDataSets("USP_Gate_print_New " & GPNO & "")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(2))
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)

            Dim GATEPASSDate As String = Trim(ds.Tables(0).Rows(0)("GPDATE") & "")
            Dim NOCNO As String = Trim(ds.Tables(0).Rows(0)("NocNo") & "")
            Dim BONDNO As String = Trim(ds.Tables(0).Rows(0)("BONDNO") & "")
            Dim BOENO As String = Trim(ds.Tables(0).Rows(0)("BOENO") & "")
            Dim BONDDATE As String = Trim(ds.Tables(0).Rows(0)("BONDDATE") & "")
            Dim MANIFESTEDQTY As String = Trim(ds.Tables(0).Rows(0)("TOTALQTY") & "")
            Dim BALANCEWT As String = Trim(ds.Tables(0).Rows(0)("BALANCEWT") & "")
            Dim BALANCEQTY As String = Trim(ds.Tables(0).Rows(0)("BALANCEQTY") & "")
            Dim GROSSWEIGH As String = Trim(ds.Tables(0).Rows(0)("TOTALWT") & "")
            Dim IMPORTER As String = Trim(ds.Tables(0).Rows(0)("IMPORTERNAME") & "")
            Dim CHA As String = Trim(ds.Tables(0).Rows(0)("CHA") & "")
            Dim DESCRIPTION As String = Trim(ds.Tables(0).Rows(0)("DESCRIPTION") & "")
            Dim EXBOENo As String = Trim(ds.Tables(0).Rows(0)("EXBOENO") & "")
            Dim EXBOEDate As String = Trim(ds.Tables(0).Rows(0)("EXBOEDATE") & "")
            Dim DELIVEREDQty As String = Trim(ds.Tables(0).Rows(0)("DELIVERDQTY") & "")
            Dim con_Name As String = Trim(ds.Tables(1).Rows(0)("con_Name") & "")
            Dim AddressI As String = Trim(ds.Tables(1).Rows(0)("AddressI") & "")
            Dim AddressII As String = Trim(ds.Tables(1).Rows(0)("AddressII") & "")

            Dim DELIVEREDWt As String = Trim(ds.Tables(0).Rows(0)("DELIVEREDWT") & "")
            Dim SignCHA As String = Trim(ds.Tables(1).Rows(0)("GatePass_SignCHA") & "")
            Dim BondInCharge As String = Trim(ds.Tables(1).Rows(0)("GatePass_BondInCharge") & "")
            Dim Accountant As String = Trim(ds.Tables(1).Rows(0)("GatePass_Accountant") & "")
            Dim Security As String = Trim(ds.Tables(1).Rows(0)("GatePass_Security") & "")

            Dim p1 As New ReportParameter("GPNO", GPNO)
            Dim p2 As New ReportParameter("GPDATE", GATEPASSDate)
            Dim p3 As New ReportParameter("NocNo", NOCNO)
            Dim p4 As New ReportParameter("BONDNO", BONDNO)
            Dim p5 As New ReportParameter("BOENO", BOENO)
            Dim p6 As New ReportParameter("BONDDATE", BONDDATE)
            Dim p7 As New ReportParameter("TOTALQTY", MANIFESTEDQTY)
            Dim p8 As New ReportParameter("TOTALWT", GROSSWEIGH)
            Dim p9 As New ReportParameter("BALANCEQTY", BALANCEQTY)

            Dim p10 As New ReportParameter("BALANCEWT", BALANCEWT)
            Dim p11 As New ReportParameter("IMPORTER", IMPORTER)
            Dim p12 As New ReportParameter("CHA", CHA)
            Dim p13 As New ReportParameter("DESCRIPTION", DESCRIPTION)

            Dim p14 As New ReportParameter("EXBOENo", EXBOENo)
            Dim p15 As New ReportParameter("EXBOEDate", EXBOEDate)
            Dim p16 As New ReportParameter("DELIVERDQTY", DELIVEREDQty)
            Dim p17 As New ReportParameter("con_Name", con_Name)
            Dim p18 As New ReportParameter("AddressI", AddressI)
            Dim p19 As New ReportParameter("DELIVEREDWT", DELIVEREDWt)
            Dim p20 As New ReportParameter("SignCHA", SignCHA)
            Dim p21 As New ReportParameter("BondInCharge", BondInCharge)
            Dim p22 As New ReportParameter("Accountant", Accountant)
            Dim p23 As New ReportParameter("Security", Security)
            Dim p24 As New ReportParameter("AddressII", AddressII)



            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24})

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
