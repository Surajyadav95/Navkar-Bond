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
            txtgatepassno.Focus()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function  
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try

            strSql = ""
            strSql = "select * from bondgatepass  where GPNo='" & Trim(txtgatepassno.Text & "") & "' and IsCancel=1"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Gate Pass No is already cancelled');", True)
                Control_Clear(sender, e)
                Exit Sub
            End If

            strSql = ""
            strSql = "usp_cancel_gate_pass '" & Trim(txtgatepassno.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                'txtBondno.Text = dt.Rows(0)("BondNo")
                txtbondate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Gate pass Date") & "")).ToString("dd-MM-yyyy")
                txtcha.Text = dt.Rows(0)("CHAName")
                txtimporter.Text = dt.Rows(0)("ImporterName")
                txtcancelremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Gate Pass no not found');", True)
                Control_Clear(sender, e)
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Control_Clear(sender As Object, e As EventArgs)
        Try
            txtgatepassno.Text = ""
            'txtBondno.Text = ""
            txtbondate.Text = ""
            txtcha.Text = ""
            txtimporter.Text = ""
            txtcancelremarks.Text = ""
          
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btncancel_Click(sender As Object, e As EventArgs)
        Try
            'strSql = ""
            'strSql = " select * from bondgatepass where GPNo='" & Trim(txtgatepassno.Text & "") & "' and iscancel=0 "
            'dt = db.sub_GetDatatable(strSql)
            'If dt.Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Gate Pass in Holder 10 Days Is Not Allow Noc No');", True)
            '    txtgatepassno.Focus()
            '    Control_Clear(sender, e)
            '    Exit Sub
            'End If

            lblquoteApprove.Text = "Are you sure to  Cancel ?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btncancelyes_ServerClick(sender As Object, e As EventArgs)
        strSql = ""
        strSql = " usp_update_cancel_gate_pass'" & Replace(Trim(txtcancelremarks.Text & ""), "'", "''") & "','" & Trim(txtgatepassno.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        lblSession.Text = "Gate Pass No cancelled successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Control_Clear(sender, e)
    End Sub
End Class
