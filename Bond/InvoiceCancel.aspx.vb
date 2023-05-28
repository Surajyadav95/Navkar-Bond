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
            lblInvoiceNo.Text = Request.QueryString("AssessNo")
            lblWorkYear.Text = Request.QueryString("WorkYear")
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_Validation_Lock_Status_CreditNote '" & lblInvoiceNo.Text & "','" & lblWorkYear.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' " + dt.Rows(0)("msg") + ".');", True)
                Exit Sub
            Else
                strSql = ""
                strSql += "update CreditNoteM set IsCancel=1,CancelledBy=" & Session("UserId_BondCFS") & ",CancelledOn=GETDATE(),Cancelled_Remarks='" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "' where CreditNoteNo='" & Trim(lblInvoiceNo.Text & "") & "'   and WorkYear='" & Trim(lblWorkYear.Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
                lblInvoiceNo.Text = ""
                lblWorkYear.Text = ""
                txtRemarks.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Credit Note cancelled successfully');", True)
                Dim page As String = "CreditNoteSummary.aspx"
                ClientScript.RegisterStartupScript(page.GetType(), "OpenList", "<script>callparentfunction(); </script>")
            End If
        Catch ex As Exception
            'lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

End Class
