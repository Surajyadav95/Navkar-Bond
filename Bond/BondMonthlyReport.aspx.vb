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
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            btnSave_Click(sender, e)
        End If
    End Sub
    Protected Sub Filldropdown()
        Try

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_BOE_BOND_STATEMENT '" & Convert.ToDateTime(Trim(txtfromDate.Text)).ToString("yyyy-MM-dd 23:59:00") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdMonthlyStatement.DataSource = dt
            grdMonthlyStatement.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdMonthlyStatement_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            If (e.Row.RowType = DataControlRowType.Header) Then
                Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)

                Dim cell As New TableHeaderCell()
                cell.Text = ""
                cell.ColumnSpan = 8
                cell.Style.Add("text-align", "center")
                row.Controls.Add(cell)


                cell = New TableHeaderCell()
                cell.Text = "Opening Balance"
                cell.ColumnSpan = 4
                cell.Style.Add("text-align", "center")
                row.Controls.Add(cell)

                cell = New TableHeaderCell()
                cell.ColumnSpan = 4
                cell.Text = "EX BOND"
                cell.Style.Add("text-align", "center")
                'cell.HorizontalAlign = HorizontalAlign.Center
                row.Controls.Add(cell)

                cell = New TableHeaderCell()
                cell.ColumnSpan = 5
                cell.Text = "Balance"
                cell.Style.Add("text-align", "center")
                row.Controls.Add(cell)
 
                'row.BackColor = ColorTranslator.FromHtml("#3AC0F2")
                'row.BackColor = Color.Yellow
                e.Row.Parent.Controls.AddAt(0, row)
                e.Row.BackColor = Color.Yellow
            End If
            If Not (e.Row.RowType = DataControlRowType.Header) And Not (e.Row.RowType = DataControlRowType.Footer) Then

                Dim row1 As New GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Normal)
                Dim cell1 As New TableHeaderCell()
                cell1.Text = ""
                cell1.ColumnSpan = 4
                cell1.Style.Add("text-align", "center")
                row1.Controls.Add(cell1)
                cell1 = New TableHeaderCell()
                cell1.Text = ""
                cell1.ColumnSpan = 4
                cell1.Style.Add("text-align", "center")
                row1.Controls.Add(cell1)
                cell1 = New TableHeaderCell()
                cell1.Text = ""
                cell1.ColumnSpan = 2
                cell1.Style.Add("text-align", "center")
                row1.Controls.Add(cell1)
                cell1 = New TableHeaderCell()
                cell1.Text = ""
                cell1.ColumnSpan = 3
                cell1.Style.Add("text-align", "center")
                row1.Controls.Add(cell1)
                cell1 = New TableHeaderCell()
                cell1.Text = ""
                cell1.ColumnSpan = 4
                cell1.Style.Add("text-align", "center")
                row1.Controls.Add(cell1)
                cell1 = New TableHeaderCell()
                cell1.Text = ""
                cell1.ColumnSpan = 4
                cell1.Style.Add("text-align", "center")
                row1.Controls.Add(cell1)
                e.Row.Parent.Controls.AddAt(1, row1)
                'grdMonthlyStatement.HeaderRow.Parent.Controls.AddAt(0, row)

            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
