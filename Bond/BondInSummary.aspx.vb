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
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-7)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")

            btnsearch_Click(sender, e)
        End If
    End Sub
   Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)

        If (ddlCategory.SelectedValue = "1") Then
            divBond.Attributes.Add("style", "display:block")
            txtDeposNo.Text = ""

        Else
            divBond.Attributes.Add("style", "display:None")
        End If

        If (ddlCategory.SelectedValue = "2") Then
            divDepos.Attributes.Add("style", "display:block")
            ddlBondType.SelectedValue = 0
            txtDeposNo.Text = ""
        Else
            divDepos.Attributes.Add("style", "display:None")
        End If

        If (ddlCategory.SelectedValue = "3") Then
            divDepos.Attributes.Add("style", "display:block")
            ddlBondType.SelectedValue = 0
            txtDeposNo.Text = ""

        Else
            textbondn.Attributes.Add("style", "display:None")
        End If

    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_Bond_IN_Summary '" & Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy 23:59:00") & "',"
            strSql += "'" & Trim(ddlCategory.SelectedValue & "") & "','" & Trim(ddlBondType.SelectedValue & "") & "','" & Trim(txtDeposNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdSummary.DataSource = dt
            grdSummary.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdSummary.DataSource = dt
        grdSummary.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub
    Protected Sub btnprint_Click(sender As Object, e As EventArgs)
        Try
            For Each row In grdSummary.Rows
                Dim NOCNo As RadioButton = CType(row.FindControl("redbutton"), RadioButton)
                Dim NOCNo1 As String = CType(row.FindControl("lblNOCNo"), Label).Text.ToString()

                If NOCNo.Checked = True Then
                    txtNOCNo.Text = NOCNo1
                    'Response.Redirect("../Report_Bond/BondEx_logo_print.aspx?NOCNo=" + NOCNo1)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BondInPrint", "javascript:BondInPrint();", True)
                    Exit Sub
                End If

            Next

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
End Class
