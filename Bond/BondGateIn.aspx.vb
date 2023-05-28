Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
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
            'db.sub_ExecuteNonQuery("Delete from Temp_generate_ssr Where UserID=" & Session("UserId_BondCFS") & "")
            txtgateDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            btnsearch_Click(sender, e)
        End If
    End Sub
  


    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "usp_Fill_Bond_Gate'" & Trim(txtboeNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = "SELECT isnull(MAX(GateInNo),0)+1   as GateInNo FROM Bond_GateIn_M WITH (XLOCK)"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtgateno.Text = dt1.Rows(0)("GateInNo")
                'intgateinid = dt1.Rows(0)("GateInNo")
            
            End If
            strSql = ""
            strSql = "usp_insert_BondGateIn_M'" & Trim(txtgateno.Text & "") & "','" & Trim(txtboeNo.Text & "") & "','" & Convert.ToDateTime(Trim(txtgateDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)

            For Each row In grdcontainer.Rows
                If CType(row.FindControl("chkright"), CheckBox).Checked = True Then


                    strSql = ""
                    strSql += "usp_insert_BondGateIn_D'" & Trim(txtgateno.Text & "") & "','" & Trim(CType(row.FindControl("lblcontainerno"), Label).Text) & "','" & Val(CType(row.FindControl("lblsize"), Label).Text) & "',"
                    strSql += "'" & Val(CType(row.FindControl("lbltypeid"), Label).Text) & "','" & Trim(CType(row.FindControl("lblnocno"), Label).Text) & "','" & Trim(CType(row.FindControl("txttrailerno"), TextBox).Text) & "',"
                    strSql += "'" & Val(CType(row.FindControl("txtsealno"), TextBox).Text) & "'"
                    db.sub_ExecuteNonQuery(strSql)
                End If
            Next
            lblSession.Text = "Record saved successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub chkright_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dblchkcount As Double = 0
            For Each row In grdcontainer.Rows
                If CType(row.FindControl("chkright"), CheckBox).Checked = True Then
                    dblchkcount += 1
                End If
            Next
            'If dblchkcount = grdcontainer.Rows.Count Then
            '    chkSelectAll.Checked = True
            'Else
            '    chkSelectAll.Checked = False
            'End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
