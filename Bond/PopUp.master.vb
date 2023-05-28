
Partial Class Summary_PopUp
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Convert.ToInt32(Session("UserId_BondCFS")) = 0 Then
            Response.Redirect("../Bond/Common/SessionExpired.aspx")
      
        End If

    End Sub
End Class

