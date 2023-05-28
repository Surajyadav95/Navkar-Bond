

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

            If Not (Request.QueryString("LSheetNo") = "") Then
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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/LoadingSheetPrint.rdlc")

            Dim SheetNo As String = Request.QueryString("LSheetNo")

            ds = db.sub_GetDataSets("USP_LOADING_SHEET_PRINT " & SheetNo & "")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(1))
            Dim datasource1 As New ReportDataSource("DataSet2", ds.Tables(2))

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)
            ReportViewer1.LocalReport.DataSources.Add(datasource1)


            Dim EntryDate As String = Trim(ds.Tables(0).Rows(0)("EntryDate") & "")
            Dim BOENO As String = Trim(ds.Tables(0).Rows(0)("BOENO") & "")
            Dim BONDNO As String = Trim(ds.Tables(0).Rows(0)("BONDNO") & "")
            Dim AGENTNAME As String = Trim(ds.Tables(0).Rows(0)("AGENTNAME") & "")
            Dim IMPORTERNAME As String = Trim(ds.Tables(0).Rows(0)("IMPORTERNAME") & "")
            Dim CHANAME As String = Trim(ds.Tables(0).Rows(0)("CHANAME") & "")
            Dim EXBOENO As String = Trim(ds.Tables(0).Rows(0)("EXBOENO") & "")
            Dim EXBOEDATE As String = Trim(ds.Tables(0).Rows(0)("EXBOEDATE") & "")
            Dim UNLOADING_SHEET_NO As String = Trim(ds.Tables(0).Rows(0)("UNLOADING_SHEET_NO") & "")
            Dim WHNAME As String = Trim(ds.Tables(0).Rows(0)("WHNAME") & "")            
            Dim QTY As String = Trim(ds.Tables(0).Rows(0)("QTY") & "")
            Dim Grosswt As String = Trim(ds.Tables(0).Rows(0)("Grosswt") & "")
            Dim deliveredvalue As String = Trim(ds.Tables(0).Rows(0)("deliveredvalue") & "")
            Dim deliveredduty As String = Trim(ds.Tables(0).Rows(0)("deliveredduty") & "")
            Dim areareleased As String = Trim(ds.Tables(0).Rows(0)("areareleased") & "")
            Dim noc_no As String = Trim(ds.Tables(0).Rows(0)("noc_no") & "")
            Dim username As String = Trim(ds.Tables(0).Rows(0)("username") & "")


            Dim con_Name As String = Trim(ds.Tables(3).Rows(0)("con_Name") & "")
            Dim AddressI As String = Trim(ds.Tables(3).Rows(0)("AddressI") & "")
            Dim AddressII As String = Trim(ds.Tables(3).Rows(0)("AddressII") & "")
            Dim Con_For As String = Trim(ds.Tables(3).Rows(0)("Con_For") & "")



            Dim p1 As New ReportParameter("SheetNo", SheetNo)
            Dim p2 As New ReportParameter("EntryDate", EntryDate)
            Dim p3 As New ReportParameter("BOENO", BOENO)
            Dim p4 As New ReportParameter("BONDNO", BONDNO)
            Dim p5 As New ReportParameter("AGENTNAME", AGENTNAME)
            Dim p6 As New ReportParameter("IMPORTERNAME", IMPORTERNAME)
            Dim p7 As New ReportParameter("CHAName", CHANAME)
            Dim p8 As New ReportParameter("EXBOENO", EXBOENO)
            Dim p9 As New ReportParameter("EXBOEDATE", EXBOEDATE)
            Dim p10 As New ReportParameter("UNLOADING_SHEET_NO", UNLOADING_SHEET_NO)
            Dim p11 As New ReportParameter("WHNAME", WHNAME)

            Dim p19 As New ReportParameter("con_Name", con_Name)
            Dim p20 As New ReportParameter("AddressI", AddressI)
            Dim p21 As New ReportParameter("AddressII", AddressII)
            Dim p22 As New ReportParameter("Con_For", Con_For)

            Dim p23 As New ReportParameter("QTY", QTY)
            Dim p24 As New ReportParameter("Grosswt", Grosswt)
            Dim p25 As New ReportParameter("deliveredvalue", deliveredvalue)
            Dim p26 As New ReportParameter("deliveredduty", deliveredduty)
            Dim p27 As New ReportParameter("areareleased", areareleased)
            Dim p28 As New ReportParameter("noc_no", noc_no)
            Dim p29 As New ReportParameter("username", username)




            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29})

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
