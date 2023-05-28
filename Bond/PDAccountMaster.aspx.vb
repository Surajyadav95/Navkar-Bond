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
    Dim EntryID, EntryIDView As String
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
            Filldropdown()

            If Not (Request.QueryString("EntryIDView") = "") Then
                EntryID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("EntryIDView")))
                strSql = ""
                strSql = "USP_View_Importer'" & EntryID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblentryID.Text = Trim(dt.Rows(0)("EntryID") & "")
                    ddlAgency.SelectedItem.Text = Trim(dt.Rows(0)("AgencyName") & "")
                    txtpdcode.Text = Trim(dt.Rows(0)("PDCode") & "")
                    txtPDAccount.Text = Trim(dt.Rows(0)("AccountName") & "")
                    txtopnning.Text = Trim(dt.Rows(0)("OpeningBal") & "")
                    txtCredit.Text = Trim(dt.Rows(0)("CreditAmount") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")

                End If

                Panel2.Enabled = False

                chkisActive.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False
            End If

        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Get_PD_account_List")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If

            ds = db.sub_GetDataSets("USE_Fill_PD_Account")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlAgency.DataSource = ds.Tables(0)
                ddlAgency.DataTextField = "agentName"
                ddlAgency.DataValueField = "agentID"
                ddlAgency.DataBind()
                ddlAgency.Items.Insert(0, New ListItem("--Select--", 0))
            End If
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
            strSql += "USP_Account_Name '" & Replace(Trim(txtpdcode.Text & ""), "'", "''") & "','" & Replace(Trim(txtPDAccount.Text & ""), "'", "''") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                txtpdcode.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('PD code already exists .');", True)
                Exit Sub
            End If
            If (ds.Tables(1).Rows.Count > 0) Then
                txtPDAccount.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' PD account name already exists .');", True)
                Exit Sub
            End If

            strSql = ""
            strSql += "USP_insert_Bond_PDAccount '" & Trim(ddlAgency.SelectedItem.Text & "") & "','" & Replace(Trim(txtpdcode.Text & ""), "'", "''") & "','" & Replace(Trim(txtPDAccount.Text & ""), "'", "''") & "',"
            strSql += "'" & Trim(txtopnning.Text & "") & "','" & Trim(txtCredit.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            lblSession.Text = "Record saved successfully for Entry ID " & dt.Rows(0)("EntryID") & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
