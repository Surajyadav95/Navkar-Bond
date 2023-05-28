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
            lblFuelRegNo.Text = Request.QueryString("FuelRegID")
            'lblWorkYear.Text = Request.QueryString("WorkYear")
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "update fuelm set IsCancel=1,CancelledBy=" & Session("UserId_BondCFS") & ",CancelledOn=GETDATE(),remark='" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "' where FuelRegID='" & Trim(lblFuelRegNo.Text & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            lblFuelRegNo.Text = ""
            txtRemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Fuel Master cancelled successfully');", True)
            Dim page As String = "index.aspx"
            ClientScript.RegisterStartupScript(page.GetType(), "OpenList", "<script>callparentfunction(); </script>")
        Catch ex As Exception
            'lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

End Class
