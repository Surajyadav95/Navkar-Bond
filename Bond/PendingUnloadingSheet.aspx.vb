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
            db.sub_ExecuteNonQuery("delete from Temp_Loading_Entry where USERID=" & Session("UserId_BondCFS") & "")
            getItemList()
        End If
    End Sub
    Protected Sub getItemList()
        Try
            strSql = ""
            strSql += "USP_PENDING_UNLOADING_SHEETS '" & ddlSearchCriteria.SelectedValue & "','" & Trim(txtSearch.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdpendinggrnlist.DataSource = dt
            grdpendinggrnlist.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim Auto_Id As String = lnkRemove.CommandArgument

            db.sub_ExecuteNonQuery("insert into Temp_Loading_Entry(UnloadingNo,UserID) Values('" & Auto_Id & "'," & Session("UserId_BondCFS") & ")")

            Dim page As String = "index.aspx"            
            ClientScript.RegisterStartupScript(page.GetType(), "OpenList", "<script>callparentfunction(); </script>")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        getItemList()
    End Sub
End Class
