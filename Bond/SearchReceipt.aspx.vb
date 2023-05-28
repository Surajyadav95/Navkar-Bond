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
    Dim TariffID, TariffIDView As String
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
            txtWorkYear.Text = Session("Workyear")
            filldetails()
            Filldropdown()

        End If
    End Sub  
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("exec get_sp_table 'bond_assessM','AssessNo','','AssessNo'")
            If (dt.Rows.Count > 0) Then
                ddlReceiptNo.DataSource = dt
                ddlReceiptNo.DataTextField = "AssessNo"
                ddlReceiptNo.DataValueField = "AssessNo"
                ddlReceiptNo.DataBind()
                ddlReceiptNo.Items.Insert(0, New ListItem("--Select--", 0))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
   
   Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            Dim lastChar As String = ""
            If ddlSearch.SelectedValue = "amount" Then
                lastChar = ddlReceiptNo.SelectedValue.Substring(ddlReceiptNo.SelectedValue.Length - 2)
            End If
            strSql = ""
            strSql = "get_Sp_bond_Receipt'" & ddlReceiptNo.SelectedValue & "','" & txtWorkYear.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtReceiptAmount.Text = Trim(dt.Rows(0)("GrandTotal") & "")
                txtAssessmentNo.Text = Trim(dt.Rows(0)("AssessNo") & "")
                txtReceiptdate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BondDate") & "")).ToString("yyyy-MM-dd")
                txtAssessmentDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("AssessDate") & "")).ToString("yyyy-MM-dd")

                '''' txtAssessType.Text = Trim(dt.Rows(0)("Assesstype") & "")
                txtbondnumber.Text = Trim(dt.Rows(0)("BondNo") & "")
                ''txtIGMNo.Text = Trim(dt.Rows(0)("IGMNo") & "")
                ''txtItemNo.Text = Trim(dt.Rows(0)("ItemNo") & "")
                txtCha.Text = Trim(dt.Rows(0)("CHAName") & "")
                txtImporter.Text = Trim(dt.Rows(0)("ImporterName") & "")
                Call filldetails()
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub filldetails()
 
        strSql = ""
        strSql = "usp_fill_grid_receipt'" & Trim(txtsearch.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        grdgodown.DataSource = dt
        grdgodown.DataBind()
    End Sub

    
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try

            strSql = ""
            strSql += "select * from Temp_Receipt_Search where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtsearch.Text = Trim(dt.Rows(0)("RECEPITNO") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
