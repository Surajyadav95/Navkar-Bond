﻿Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Configuration

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserIDPRE_Bond") Is Nothing Then
        '    Session("UserIDPRE_Bond") = Request.Cookies("UserIDPRE_Bond").Value
        '    'Session("Dept") = Request.Cookies("Dept").Value
        '    Session("UserNamePRE_Bond") = Request.Cookies("UserNamePRE_Bond").Value
        '    ''Session("PROFILEURL") = Request.Cookies("PROFILEURL").Value
        '    'Session("Location") = Request.Cookies("Location").Value
        '    ''Session("LOcationId") = Request.Cookies("LOcationId").Value
        '    'Session("ID") = Response.Cookies("ID").Value
        '    'Session("CompID") = Response.Cookies("CompID").Value
        '    'Session("Workyear") = Response.Cookies("Workyear").Value
        'End If
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-7)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
            Filldropdown()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_Customer_DAR_fillDropDown")

            ddlcustomer.DataSource = ds.Tables(2)
            ddlcustomer.DataTextField = "CustomerName"
            ddlcustomer.DataValueField = "Cust_ID"
            ddlcustomer.DataBind()
            ddlcustomer.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            txtNOCNo.Text = ""
            ddlcustomer.SelectedValue = 0            
            If Trim(ddlCategory.SelectedValue) = 1 Then
                divCustomer.Attributes.Add("style", "display:None")
                divNOC.Attributes.Add("style", "display:block")
            ElseIf Trim(ddlCategory.SelectedValue) = 2 Then
                divCustomer.Attributes.Add("style", "display:block")
                divNOC.Attributes.Add("style", "display:None")
            Else
                divCustomer.Attributes.Add("style", "display:None")
                divNOC.Attributes.Add("style", "display:None")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_Profitability_report_new '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00") & "',"
            strSql += "'" & Trim(ddlCategory.SelectedValue) & "','" & Trim(txtNOCNo.Text & "") & "','" & Trim(ddlcustomer.SelectedValue) & "'"
            dt9 = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If dt9.Rows.Count > 0 Then
                dt9.Columns.Remove("NOCNo1")
                dt9.Columns.Remove("Sr No1")
                dt9.Columns.Remove("NOCNo")
                dt9.Columns.Remove("NOCNo2")
                dt9.Columns.Remove("ssrtype")
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt9, "Profitability")
                    Dim Excelno As String = "", Excelno1 As String = "", Excelno2 As String = "", Excelno3 As String = "", Excelno4 As String = ""
                    Excelno = db.GetExcelColumnName(dt9.Columns.Count)
                    Excelno1 = db.GetExcelColumnName(dt9.Columns("Area Occupied in Sq. Mtr").Ordinal + 2)
                    Excelno2 = db.GetExcelColumnName(dt9.Columns("Total Revenue").Ordinal + 1)
                    Excelno3 = db.GetExcelColumnName(dt9.Columns("Total Revenue").Ordinal + 2)
                    Excelno4 = db.GetExcelColumnName(dt9.Columns("Total Cost").Ordinal + 1)

                    With wb.Worksheets(0)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(5)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("" & Excelno1 & "5:" & Excelno2 & "5").Merge()
                        .Range("" & Excelno3 & "5:" & Excelno4 & "5").Merge()
                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30

                        .Cell(5, dt9.Columns("Area Occupied in Sq. Mtr").Ordinal + 2).Value = "Revenue"
                        .Cell(5, dt9.Columns("Area Occupied in Sq. Mtr").Ordinal + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(5, dt9.Columns("Area Occupied in Sq. Mtr").Ordinal + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                        .Cell(5, dt9.Columns("Total Revenue").Ordinal + 2).Value = "Cost"
                        .Cell(5, dt9.Columns("Total Revenue").Ordinal + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(5, dt9.Columns("Total Revenue").Ordinal + 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                        .Cell(3, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(3, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(5, dt9.Columns("Total Revenue").Ordinal + 2).Style.Fill.BackgroundColor = XLColor.LightBlue
                        .Cell(5, dt9.Columns("Area Occupied in Sq. Mtr").Ordinal + 2).Style.Fill.BackgroundColor = XLColor.Pink
                        For i = 1 To dt9.Columns.Count
                            .Cell(dt9.Rows.Count + 7, i).Style.Fill.BackgroundColor = XLColor.Yellow
                        Next
                        Dim dblCount As Double = 0, dblQty As Double = 0, dblValue As Double = 0, dblDuty As Double = 0, dblAreaOcc As Double = 0, dblTotRevenue As Double = 0, dblTotCost As Double = 0, dblMargin As Double = 0
                        For i = 1 To dt9.Rows.Count
                            If Not dt9.Rows(i - 1)("NOC No") = "" Then
                                dblCount += 1
                                .Cell(i + 6, 1).Value = dblCount
                            End If
                            dblQty += Val(dt9.Rows(i - 1)("Qty") & "")
                            dblValue += Val(dt9.Rows(i - 1)("Value") & "")
                            dblDuty += Val(dt9.Rows(i - 1)("Duty") & "")
                            dblAreaOcc += Val(dt9.Rows(i - 1)("Area Occupied in Sq. Mtr") & "")
                            dblTotRevenue += Val(dt9.Rows(i - 1)("Total Revenue") & "")
                            dblTotCost += Val(dt9.Rows(i - 1)("Total Cost") & "")
                            dblMargin += Val(dt9.Rows(i - 1)("Margin") & "")
                        Next
                        .Cell(dt9.Rows.Count + 7, dt9.Columns("Qty").Ordinal + 1).Value = dblQty
                        .Cell(dt9.Rows.Count + 7, dt9.Columns("Value").Ordinal + 1).Value = dblValue
                        .Cell(dt9.Rows.Count + 7, dt9.Columns("Duty").Ordinal + 1).Value = dblDuty
                        .Cell(dt9.Rows.Count + 7, dt9.Columns("Area Occupied in Sq. Mtr").Ordinal + 1).Value = dblAreaOcc
                        .Cell(dt9.Rows.Count + 7, dt9.Columns("Total Revenue").Ordinal + 1).Value = dblTotRevenue
                        .Cell(dt9.Rows.Count + 7, dt9.Columns("Total Cost").Ordinal + 1).Value = dblTotCost
                        .Cell(dt9.Rows.Count + 7, dt9.Columns("Margin").Ordinal + 1).Value = dblMargin
                        .Cell(dt9.Rows.Count + 7, dt9.Columns("NOC No").Ordinal + 1).Value = "Total"
                        .Cell(dt9.Rows.Count + 7, dt9.Columns("Ex Bond Dated").Ordinal + 1).Value = "Total"
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=Profitability" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No record found!');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
