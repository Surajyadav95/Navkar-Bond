Imports System.Data
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
            db.sub_ExecuteNonQuery("Delete from Temp_Bond_Gate_Search Where UserID=" & Session("UserId_BondCFS") & "")
            btnSave_Click(sender, e)
        End If
    End Sub
  
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " IN_Bond_gate_Search '" & ddlSearchCriteria.SelectedValue & "','" & Trim(txtsearch.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkselect_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim DblExTallyNo As String = lnkRemove.CommandArgument

            strSql = ""
            strSql = "usp_check_recipt '" & DblExTallyNo & "' "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then

            Else

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Receipt not generate');", True)
                Exit Sub
            End If
            strSql = ""
            strSql = "usp_check_DateInBond '" & DblExTallyNo & "' "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invoice validity is expired. Cannot proceed.');", True)
                Exit Sub
            Else

                
            End If
            strSql = ""
            strSql += "USP_INSERT_Temp_Bond_Gate_Search '" & DblExTallyNo & "','" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:callparentfunction();", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
