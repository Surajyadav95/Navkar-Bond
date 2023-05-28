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
    Public Sub Fillgrid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-7)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
            btnsearch_Click(sender, e)
        End If
    End Sub
    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        If (ddlCategory.SelectedValue = "1") Then
            divBondtype.Attributes.Add("style", "display:block")
            txtDeposNo.Text = ""

        Else
            divBondtype.Attributes.Add("style", "display:None")
        End If

        If (ddlCategory.SelectedValue = "2") Then
            divDepos.Attributes.Add("style", "display:block")
            ddltype.SelectedValue = 0
            txtDeposNo.Text = ""
        Else
            divDepos.Attributes.Add("style", "display:None")
        End If

        If (ddlCategory.SelectedValue = "3") Then
            divDepos.Attributes.Add("style", "display:block")
            ddltype.SelectedValue = 0
            txtDeposNo.Text = ""

        Else
            textbondn.Attributes.Add("style", "display:None")
        End If

    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_Bond_Ex_Summary '" & Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy 00:00:00") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy 23:59:00") & "',"
            strSql += "'" & Trim(ddlCategory.SelectedValue & "") & "','" & Trim(ddltype.SelectedValue & "") & "','" & Trim(txtDeposNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnprint_Click(sender As Object, e As EventArgs)
        Try
            For Each row In grdcontainer.Rows
                Dim NOCNo As RadioButton = CType(row.FindControl("redbutton"), RadioButton)
                Dim NOCNo1 As String = CType(row.FindControl("lblbondexno"), Label).Text.ToString()

                If NOCNo.Checked = True Then
                    txtNOCNo.Text = NOCNo1
                    'Response.Redirect("../Report_Bond/BondEx_logo_print.aspx?NOCNo=" + NOCNo1)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BondExPrint", "javascript:BondExPrint();", True)
                End If

            Next

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
