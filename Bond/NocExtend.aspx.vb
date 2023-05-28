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
            txtvalidupto.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtExtend.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtnoc.Focus()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
     
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "usp_noc_extend'" & Trim(txtnoc.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtvalidupto.Text = Convert.ToDateTime(Trim(dt.Rows(0)("ExpiryDate") & "")).ToString("yyyy-MM-dd")
                txtExtend.Text = Convert.ToDateTime(Trim(dt.Rows(0)("ExpiryDate") & "")).ToString("yyyy-MM-dd")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('NOC No not found!');", True)
                txtnoc.Text = ""
                txtnoc.Focus()
                Exit Sub
            End If
            txtweeks.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtweeks_TextChanged(sender As Object, e As EventArgs)
        Try
            If Trim(txtweeks.Text & "") <> "" Then
                txtExtend.Text = Convert.ToDateTime(DateAdd("d", (Val(txtweeks.Text) * 7 - 1), Convert.ToDateTime(txtvalidupto.Text).ToString("dd-MM-yyyy"))).ToString("yyyy-MM-dd")

            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "Update_Extend_noc'" & Trim(txtnoc.Text & "") & "','" & Convert.ToDateTime(Trim(txtExtend.Text & "")).ToString("yyyy-MM-dd") & "'"
            dt = db.sub_GetDatatable(strSql)
            lblSession.Text = "You have successfully Extended NOC"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
