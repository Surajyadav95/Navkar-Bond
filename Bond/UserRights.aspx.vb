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

            Filldropdown()
            'btnShow_Click(sender, e)
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_get_DropdownFor_User")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlUser.DataSource = ds.Tables(0)
                ddlUser.DataTextField = "Name"
                ddlUser.DataValueField = "UserID"
                ddlUser.DataBind()
                ddlUser.Items.Insert(0, New ListItem("--Select--", "0"))
            End If
            If (ds.Tables(1).Rows.Count > 0) Then
                ddlMenu.DataSource = ds.Tables(1)
                ddlMenu.DataTextField = "menudept"
                ddlMenu.DataValueField = "MenudeptID"
                ddlMenu.DataBind()
                ddlMenu.Items.Insert(0, New ListItem("--Select--", "0"))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If ddlUser.SelectedValue = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select username first');", True)
                Exit Sub
            End If
            dt = db.sub_GetDatatable("USP_user_rights_show '" & Trim(ddlMenu.SelectedValue & "") & "','" & Trim(ddlUser.SelectedValue & "") & "','" & Trim(txtseach.Text & "") & "'")
            grduserdata.DataSource = dt
            grduserdata.DataBind()
            If grduserdata.Rows.Count > 0 Then
                btnupdate.Attributes.Add("style", "display:block")
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnupdate_Click(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In grduserdata.Rows
                Dim hfEntryid As HiddenField = DirectCast(row.FindControl("hfEntryid"), HiddenField)
                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                strSql = ""
                strSql += "USP_insert_into_userrights " & Trim(hfEntryid.Value & "") & "," & Trim(chkright.Checked & "") & "," & Trim(ddlUser.SelectedValue & "") & ""
                db.sub_ExecuteNonQuery(strSql)

            Next
            lblSession.Text = "Record updated successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
