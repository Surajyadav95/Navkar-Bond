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
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-7)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
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
                ddlimpor.DataSource = ds.Tables(1)
                ddlimpor.DataTextField = "ImporterName"
                ddlimpor.DataValueField = "ImporterID"
                ddlimpor.DataBind()
                ddlimpor.Items.Insert(0, New ListItem("--Select--", 0))
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
            ddlimpor.SelectedValue = 0
            ddlcustomer.SelectedValue = 0
            ddlBondType.SelectedValue = 0
            ddlWarehouse.SelectedValue = 0
            If (ddlCategory.SelectedValue = "1") Then
                divCHA.Attributes.Add("style", "display:block")
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                ddlBondType.SelectedValue = 0
            Else
                divCHA.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "2") Then
                divImpoeter.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                ddlBondType.SelectedValue = 0
            Else
                divImpoeter.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "3") Then
                divCustomer.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlBondType.SelectedValue = 0
            Else
                divCustomer.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "4") Then
                divBond.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divBond.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "5") Then
                divNoc.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divNoc.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "6") Then
                divNoc.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divcargo.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "7") Then
                divWarehouse.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
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
            strSql = ""
            strSql += " USP_gate_IN_REGISTER '" & Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy 23:59:00 ") & "',"
            strSql += "'" & Trim(ddlCategory.SelectedValue & "") & "','" & Trim(ddlcha.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlimpor.SelectedValue & "") & "','" & Trim(ddlcustomer.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlBondType.SelectedValue & "") & "','" & Trim(txtNoc.Text & "") & "','" & Trim(ddlWarehouse.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()

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
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(btnsearch, btnsearch.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += " USP_gate_IN_REGISTER '" & Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy 23:59:00 ") & "',"
            strSql += "'" & Trim(ddlCategory.SelectedValue & "") & "','" & Trim(ddlcha.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlimpor.SelectedValue & "") & "','" & Trim(ddlcustomer.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlBondType.SelectedValue & "") & "','" & Trim(txtNoc.Text & "") & "','" & Trim(ddlWarehouse.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)

            strSql = ""
            strSql += " USP_gate_IN_REGISTER_Not_Stuff '" & Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy 23:59:00 ") & "',"
            strSql += "'" & Trim(ddlCategory.SelectedValue & "") & "','" & Trim(ddlcha.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlimpor.SelectedValue & "") & "','" & Trim(ddlcustomer.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlBondType.SelectedValue & "") & "','" & Trim(txtNoc.Text & "") & "','" & Trim(ddlWarehouse.SelectedValue & "") & "'"
            dt1 = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Bond In Register" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    wb.Worksheets.Add(dt1, "Bond In Container" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
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
                        .Cell(2, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(3, 1).Value = "BOND IN Details"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Font.FontSize = 17
                    End With
                    With wb.Worksheets(1)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt1.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(4)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(3).Height = 20
                        .Cell(2, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(3, 1).Value = "BOND IN Details"
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
                    Response.AddHeader("content-disposition", "attachment;filename=BondInRegister" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
