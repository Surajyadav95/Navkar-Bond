﻿Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim WHID, WHIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'getItemList()
            db.sub_ExecuteNonQuery("Delete from Temp_NOC_List_TruckIn Where User_ID=" & Session("UserId_BondCFS") & "")
            btnSave_Click(sender, e)
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " NOC_list_truck_in '" & ddlSearchCriteria.SelectedValue & "','" & Trim(txtsearch.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdNOCList.DataSource = dt
            grdNOCList.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        'grdcontainer.DataSource = dt
        'grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub
    Protected Sub lnkselect_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim Auto_Id As String = lnkRemove.CommandArgument

            strSql = ""
            strSql += "insert into Temp_NOC_List_TruckIn (NOCNo,userid,AddedOn) Values('" & Auto_Id & "','" & Session("UserId_BondCFS") & "',Getdate())"
            db.sub_ExecuteNonQuery(strSql)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:callparentfunction();", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
