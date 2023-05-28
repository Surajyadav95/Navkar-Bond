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
            'txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
            Filldropdown()
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
            ds = db.sub_GetDataSets("USP_Fill_gate_BOND_IN_register")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlcha.DataSource = ds.Tables(0)
                ddlcha.DataTextField = "CHAName"
                ddlcha.DataValueField = "CHAID"
                ddlcha.DataBind()
                ddlcha.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddlimporter.DataSource = ds.Tables(1)
                ddlimporter.DataTextField = "ImporterName"
                ddlimporter.DataValueField = "ImporterID"
                ddlimporter.DataBind()
                ddlimporter.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If (ds.Tables(2).Rows.Count > 0) Then
                ddlcustomer.DataSource = ds.Tables(2)
                ddlcustomer.DataTextField = "CustomerName"
                ddlcustomer.DataValueField = "Cust_ID"
                ddlcustomer.DataBind()
                ddlcustomer.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            ddlWarehouse.DataSource = ds.Tables(3)
            ddlWarehouse.DataTextField = "warehouse_code"
            ddlWarehouse.DataValueField = "warehouse_code"
            ddlWarehouse.DataBind()
            ddlWarehouse.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlcha.SelectedValue = 0
            ddlimporter.SelectedValue = 0
            ddlcustomer.SelectedValue = 0
            ddlBondType.SelectedValue = 0
            ddlWarehouse.SelectedValue = 0
            If (ddlCategory.SelectedValue = "1") Then
                divCHA.Attributes.Add("style", "display:block")
                ddlimporter.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                ddlBondType.SelectedValue = 0
            Else
                divCHA.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "2") Then
                divImporter.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                ddlBondType.SelectedValue = 0
            Else
                divImporter.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "3") Then
                divCustomer.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimporter.SelectedValue = 0
                ddlBondType.SelectedValue = 0
            Else
                divCustomer.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "4") Then
                divBond.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimporter.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divBond.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "5") Then
                divNoc.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimporter.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divNoc.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "6") Then
                divNoc.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimporter.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divcargo.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "7") Then
                divWarehouse.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimporter.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divWarehouse.Attributes.Add("style", "display:None")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            Dim Criteria As String = "", Name As String = ""
            Criteria = Trim(ddlCategory.SelectedItem.Text & "")
            If ddlCategory.SelectedValue = 1 Then
                Name = ddlcha.SelectedValue
            ElseIf ddlCategory.SelectedValue = 2 Then
                Name = ddlimporter.SelectedValue
            ElseIf ddlCategory.SelectedValue = 3 Then
                Name = ddlcustomer.SelectedValue
            ElseIf ddlCategory.SelectedValue = 4 Then
                Name = ddlBondType.SelectedValue
            ElseIf ddlCategory.SelectedValue = 5 Then
                Name = txtNoc.Text
            ElseIf ddlCategory.SelectedValue = 6 Then
                Name = txtNoc.Text
            ElseIf ddlCategory.SelectedValue = 7 Then
                Name = ddlWarehouse.SelectedValue
            End If
            strSql = ""
            strSql += " Get_sp_TotalBondStock_new '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 23:59:00 ") & "',"
            strSql += "'" & Trim(Criteria & "") & "','" & Trim(Name & "") & "'"            
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
            Dim Criteria As String = "", Name As String = ""
            Dim dbltotdwellweeks As Double = 0, dbltotbalarea As Double = 0, dbltotbalweight As Double = 0, dbltotbalqty As Double = 0, dbltotbalvalue As Double = 0, dbltotbalduty As Double = 0, dbltotvalduty As Double = 0

            Criteria = Trim(ddlCategory.SelectedItem.Text & "")
            If ddlCategory.SelectedValue = 1 Then
                Name = ddlcha.SelectedValue
            ElseIf ddlCategory.SelectedValue = 2 Then
                Name = ddlimporter.SelectedValue
            ElseIf ddlCategory.SelectedValue = 3 Then
                Name = ddlcustomer.SelectedValue
            ElseIf ddlCategory.SelectedValue = 4 Then
                Name = ddlBondType.SelectedValue
            ElseIf ddlCategory.SelectedValue = 5 Then
                Name = txtNoc.Text
            ElseIf ddlCategory.SelectedValue = 6 Then
                Name = txtCargo.Text
            ElseIf ddlCategory.SelectedValue = 7 Then
                Name = ddlWarehouse.SelectedValue
            End If
            strSql = ""
            strSql += " Get_sp_TotalBondStock_new '" & Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy 00:00:00 ") & "',"
            strSql += "'" & Trim(Criteria & "") & "','" & Trim(Name & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Cargo Inventory" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    For i = 0 To dt.Rows.Count - 1
                        dbltotdwellweeks += Val(dt.Rows(i)("Dwell Weeks"))
                        dbltotbalarea += Val(dt.Rows(i)("Balance Area"))
                        dbltotbalweight += Val(dt.Rows(i)("Balance Weight(KGS)"))
                        dbltotbalqty += Val(dt.Rows(i)("Balance Qty"))
                        dbltotbalvalue += Val(dt.Rows(i)("Balance Value"))
                        dbltotbalduty += Val(dt.Rows(i)("Balance Duty"))
                        dbltotvalduty += Val(dt.Rows(i)("Total value & Duty"))
                    Next
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(4)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(3).Height = 20
                        .Cell(2, 1).Value = "As On: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy")
                        .Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(3, 1).Value = "Cargo Inventory Details"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Font.FontSize = 17
                        .Cell(dt.Rows.Count + 6, 15).Value = "Total"
                        For i = 1 To dt.Columns.Count
                            .Cell(dt.Rows.Count + 6, i).Style.Fill.BackgroundColor = XLColor.Yellow
                        Next
                        .Cell(dt.Rows.Count + 6, 16).Value = dbltotdwellweeks
                        .Cell(dt.Rows.Count + 6, 17).Value = dbltotbalarea
                        .Cell(dt.Rows.Count + 6, 18).Value = dbltotbalweight
                        .Cell(dt.Rows.Count + 6, 19).Value = dbltotbalqty
                        .Cell(dt.Rows.Count + 6, 21).Value = dbltotbalvalue
                        .Cell(dt.Rows.Count + 6, 22).Value = dbltotbalduty
                        .Cell(dt.Rows.Count + 6, 23).Value = dbltotvalduty
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=CargoInventory" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
