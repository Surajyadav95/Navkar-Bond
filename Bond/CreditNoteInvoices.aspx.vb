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
            'db.sub_ExecuteNonQuery("Delete from Temp_Noc_Search Where UserID=" & Session("UserId_BondCFS") & "")
            btnSave_Click(sender, e)
            FillGrid()
        End If
    End Sub    
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_INVOICES_FOR_CREDIT_NOTES " & Val(Request.QueryString("Category") & "") & "," & Val(ddlcriteria.SelectedValue & "") & ",'" & Trim(txtsearc.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub FillGrid()
        Try
            strSql = ""
            strSql += "select distinct assessno,Workyear,InvoiceNo,GSTNAME,GSTADDRESS from Temp_Credit_Note Where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            GridView1.DataSource = dt
            GridView1.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkselect_Click(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim Auto_Id As String = lnkRemove.CommandArgument

            Dim GSTName As String = "", GSTAddress As String = "", blResult As Boolean = False
            For Each row In GridView1.Rows
                GSTName = Trim(CType(row.FindControl("lblGSTName"), Label).Text)
                GSTAddress = Trim(CType(row.FindControl("lblAddress"), Label).Text)
                blResult = True
                Exit For
            Next
            'If blResult = False Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Select atleast one invoice to proceed.');", True)
            '    Exit Sub            
            'End If
            If GSTName <> "" Then
                If Not (GSTName = Trim(CType(gr.FindControl("lblGSTName"), Label).Text) And GSTAddress = Trim(CType(gr.FindControl("lblAddress"), Label).Text)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Selected invoice are of different Party or location. Cannot proceed');", True)
                    Exit Sub
                End If
            End If            
            strSql = ""
            strSql += "select * from temp_credit_note Where UserID=" & Session("UserId_BondCFS") & " and AssessNo='" & Trim(CType(gr.FindControl("lblAssessNo"), Label).Text) & "' and WorkYear='" & Trim(CType(gr.FindControl("lblWorkyear"), Label).Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invoice already added.');", True)
                Exit Sub
            End If            
            strSql = ""
            strSql += "USP_INSERT_INTO_TEMP_CREDIT_NOTE_FOR_MULTIPLE_INVOICES " & Val(Request.QueryString("Category") & "") & ",'" & Trim(CType(gr.FindControl("lblAssessNo"), Label).Text) & "','" & Trim(CType(gr.FindControl("lblWorkyear"), Label).Text) & "','" & Trim(CType(gr.FindControl("lblInvoiceNo"), Label).Text) & "','" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            txtsearc.Text = ""
            If ddlcriteria.SelectedValue = 0 Then
                divSearchText.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 1 Then
                divSearchText.Attributes.Add("style", "display:block")
            ElseIf ddlcriteria.SelectedValue = 2 Then
                divSearchText.Attributes.Add("style", "display:block")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            'Dim GSTName As String = "", GSTAddress As String = "", blResult As Boolean = False
            ''strSql = ""
            ''strSql += "select * from temp_credit_note Where UserID=" & Session("UserId_BondCFS") & " and AssessNo='" & Trim(CType(row.FindControl("lblAssessNo"), Label).Text) & "' and WorkYear='" & Trim(CType(row.FindControl("lblAssessNo"), Label).Text) & "'"
            ''dt = db.sub_GetDatatable(strSql)

            'For Each row In grdcontainer.Rows
            '    If CType(row.FindControl("CheckBox1"), CheckBox).Checked = True Then
            '        GSTName = Trim(CType(row.FindControl("lblGSTName"), Label).Text)
            '        GSTAddress = Trim(CType(row.FindControl("lblAddress"), Label).Text)
            '        blResult = True
            '        Exit For
            '    End If
            'Next
            'If blResult = False Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Select atleast one invoice to proceed.');", True)
            '    Exit Sub
            'Else
            '    db.sub_ExecuteNonQuery("Delete from temp_credit_note Where UserID=" & Session("UserId_BondCFS") & "")
            'End If
            'For Each row In grdcontainer.Rows
            '    If CType(row.FindControl("CheckBox1"), CheckBox).Checked = True Then
            '        If Not (GSTName = Trim(CType(row.FindControl("lblGSTName"), Label).Text) And GSTAddress = Trim(CType(row.FindControl("lblAddress"), Label).Text)) Then
            '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Selected invoices are of different Party or location. Cannot proceed');", True)
            '            CType(row.FindControl("CheckBox1"), CheckBox).Focus()
            '            Exit Sub
            '        End If                    
            '    End If
            'Next
            'For Each row In grdcontainer.Rows
            '    If CType(row.FindControl("CheckBox1"), CheckBox).Checked = True Then
            '        strSql = ""
            '        strSql += "USP_INSERT_INTO_TEMP_CREDIT_NOTE_FOR_MULTIPLE_INVOICES " & Val(Request.QueryString("Category") & "") & ",'" & Trim(CType(row.FindControl("lblAssessNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblWorkyear"), Label).Text) & "','" & Trim(CType(row.FindControl("lblInvoiceNo"), Label).Text) & "','" & Session("UserId_BondCFS") & "'"
            '        db.sub_ExecuteNonQuery(strSql)
            '    End If
            'Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:callparentfunction();", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class