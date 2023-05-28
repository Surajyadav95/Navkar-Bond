

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

    'Dim lr As New Microsoft.Reporting.WebForms.LocalReport
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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/ExportAssessmentPrint.rdlc")

            Dim assessno As String = Request.QueryString("assessno")
            Dim workyear As String = Request.QueryString("workyear")
           
 
            strSql = ""
            strSql += "USP_EXPORT_PRINT_WEB '" & assessno & "','" & workyear & "'"
            ds = db.sub_GetDataSets(strSql)
            
            dt2 = ds.Tables(0)
            Dim datasource As New ReportDataSource("Exportheader", dt2)
            dt3 = ds.Tables(1)
            Dim datasource1 As New ReportDataSource("ExportSB", dt3)
            dt4 = ds.Tables(2)
            Dim datasource2 As New ReportDataSource("ExportChar", dt4)
            dt5 = ds.Tables(3)
            Dim datasource3 As New ReportDataSource("ExportHan", dt5)
            dt6 = ds.Tables(4)
            Dim datasource4 As New ReportDataSource("ExportContainer", dt6)
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)
            ReportViewer1.LocalReport.DataSources.Add(datasource1)

            ReportViewer1.LocalReport.DataSources.Add(datasource2)
            ReportViewer1.LocalReport.DataSources.Add(datasource3)
            ReportViewer1.LocalReport.DataSources.Add(datasource4)

            deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"

            bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

            Response.ClearContent()

            Response.ClearHeaders()

            Response.ContentType = "application/pdf"

            Response.BinaryWrite(bytes)

            File.WriteAllBytes("D:\NavkarCFS\Navkar-Bond\Report_Bond\Cht.pdf", bytes)

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
