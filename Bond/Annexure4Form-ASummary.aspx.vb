﻿Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt10 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(7)).ToString("yyyy-MM-dd")            
            btnsearch_Click(sender, e)

        End If
    End Sub
    Public Sub grid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Filldropdown()
        Try
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_Annexure_Form_A '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub

    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim dbltotdwellweeks As Double = 0, dbltotbalarea As Double = 0, dbltotbalweight As Double = 0, dbltotbalqty As Double = 0, dbltotbalvalue As Double = 0, dbltotbalduty As Double = 0, dbltotvalduty As Double = 0

            strSql = ""
            strSql += " USP_Annexure_Form_A '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00") & "'"
            dt = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Annexure 4 Form A " & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                   
                    With wb.Worksheets(0)
                        Dim Excelno As String = "", Excelno1 As String = "", Excelno2 As String = "", Excelno3 As String = "", Excelno4 As String = "", Excelno5 As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        Excelno1 = db.GetExcelColumnName(dt.Columns("Shortage").Ordinal + 2)
                        Excelno2 = db.GetExcelColumnName(dt.Columns("Shortage").Ordinal + 1)
                        Excelno3 = db.GetExcelColumnName(dt.Columns("Date and time of removal").Ordinal + 2)
                        Excelno4 = db.GetExcelColumnName(dt.Columns("Quantity cleared").Ordinal + 1)
                        Excelno5 = db.GetExcelColumnName(dt.Columns("Remarks").Ordinal + 1)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(5)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno2 & "5").Merge()
                        .Range("" & Excelno1 & "5:" & Excelno3 & "5").Merge()
                        .Range("" & Excelno4 & "5:" & Excelno5 & "5").Merge()
                        
                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(3).Height = 20

                        .Cell(5, 1).Value = "Receipts"
                        .Cell(5, 1).Style.Fill.BackgroundColor = XLColor.Pink
                        .Cell(5, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(5, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                        .Cell(5, dt.Columns("Sample drawn by government agencies").Ordinal + 1).Value = "Handling and storage"
                        .Cell(5, dt.Columns("Sample drawn by government agencies").Ordinal + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(5, dt.Columns("Sample drawn by government agencies").Ordinal + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(5, dt.Columns("Sample drawn by government agencies").Ordinal + 1).Style.Fill.BackgroundColor = XLColor.Aqua

                        .Cell(5, dt.Columns("Quantity cleared").Ordinal + 1).Value = "Removal"
                        .Cell(5, dt.Columns("Quantity cleared").Ordinal + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(5, dt.Columns("Quantity cleared").Ordinal + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(5, dt.Columns("Quantity cleared").Ordinal + 1).Style.Fill.BackgroundColor = XLColor.Yellow


                        .Cell(2, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(3, 1).Value = "Annexure 4 Form A Details"
                        .Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Font.FontSize = 17
                        
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=Annexure4FormA" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        'getdatewiseWO()
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No record found!');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
