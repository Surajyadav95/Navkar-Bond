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
    Dim ImporterID, ImporterIDView As String
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


            If Not (Request.QueryString("ImporterIDEdit") = "") Then
                ImporterID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("ImporterIDEdit")))
                strSql = ""
                strSql = "USP_Edit_Importer '" & ImporterID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblImporterID.Text = Trim(dt.Rows(0)("ImporterID") & "")
                    lblcode.Text = Trim(dt.Rows(0)("ImporterCode") & "")
                    lblagentname.Text = Trim(dt.Rows(0)("ImporterName") & "")
                    txtcode.Text = Trim(dt.Rows(0)("ImporterCode") & "")
                    txtImportername.Text = Trim(dt.Rows(0)("ImporterName") & "")
                    txtaddress.Text = Trim(dt.Rows(0)("ImpAddress") & "")
                    txtcity.Text = Trim(dt.Rows(0)("ImpCity") & "")
                    ddlstste.SelectedValue = Trim(dt.Rows(0)("state") & "")
                    txtcontactper.Text = Trim(dt.Rows(0)("ImpAuthPerson") & "")
                    txtperdestinston.Text = Trim(dt.Rows(0)("ImpAuthPersonDesig") & "")
                    txtnumber1.Text = Trim(dt.Rows(0)("ImpTelI") & "")
                    txtnumber2.Text = Trim(dt.Rows(0)("ImpTelII") & "")
                    txtfax.Text = Trim(dt.Rows(0)("ImpFax") & "")
                    txtmobile.Text = Trim(dt.Rows(0)("ImpCellNo") & "")
                    txtemail.Text = Trim(dt.Rows(0)("ImpEMail") & "")
                    txtpan.Text = Trim(dt.Rows(0)("ImpPANNo") & "")
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")

                End If
                Panel1.Enabled = True
                Panel2.Enabled = True

                btnSave.Text = "Update"
            End If
            If Not (Request.QueryString("ImporterIDView") = "") Then
                ImporterID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("ImporterIDView")))
                strSql = ""
                strSql = "USP_Edit_Importer '" & ImporterID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblImporterID.Text = Trim(dt.Rows(0)("ImporterID") & "")
                    lblcode.Text = Trim(dt.Rows(0)("ImporterCode") & "")
                    lblagentname.Text = Trim(dt.Rows(0)("ImporterName") & "")
                    txtcode.Text = Trim(dt.Rows(0)("ImporterCode") & "")
                    txtImportername.Text = Trim(dt.Rows(0)("ImporterName") & "")
                    txtaddress.Text = Trim(dt.Rows(0)("ImpAddress") & "")
                    txtcity.Text = Trim(dt.Rows(0)("ImpCity") & "")
                    ddlstste.SelectedValue = Trim(dt.Rows(0)("state") & "")
                    txtcontactper.Text = Trim(dt.Rows(0)("ImpAuthPerson") & "")
                    txtperdestinston.Text = Trim(dt.Rows(0)("ImpAuthPersonDesig") & "")
                    txtnumber1.Text = Trim(dt.Rows(0)("ImpTelI") & "")
                    txtnumber2.Text = Trim(dt.Rows(0)("ImpTelII") & "")
                    txtfax.Text = Trim(dt.Rows(0)("ImpFax") & "")
                    txtmobile.Text = Trim(dt.Rows(0)("ImpCellNo") & "")
                    txtemail.Text = Trim(dt.Rows(0)("ImpEMail") & "")
                    txtpan.Text = Trim(dt.Rows(0)("ImpPANNo") & "")
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")

                End If
                Panel1.Enabled = False
                Panel2.Enabled = False
                txtremarks.ReadOnly = True
                chkisActive.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False
            End If

        End If
    End Sub
    Protected Sub Filldropdown()

        Try
            dt = db.sub_GetDatatable("USP_Get_Dropdown_Importer_list")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If

            ds = db.sub_GetDataSets("USP_Agent_Ledger_Fill")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlstste.DataSource = ds.Tables(0)
                ddlstste.DataTextField = "state"
                ddlstste.DataValueField = "State_ID"
                ddlstste.DataBind()
                ddlstste.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            'If (ds.Tables(1).Rows.Count > 0) Then
            '    ddlLedger.DataSource = ds.Tables(1)
            '    ddlLedger.DataTextField = "LedgerName"
            '    ddlLedger.DataValueField = "LedgerID"
            '    ddlLedger.DataBind()
            '    ddlLedger.Items.Insert(0, New ListItem("--Select--", 0))
            'End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If (btnSave.Text = "Update") Then

                If Trim(txtcode.Text) <> Trim(lblcode.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM Importer WHERE ImporterCode='" & Trim(txtcode.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Importer code already exists .');", True)
                        txtcode.Focus()
                        Exit Sub
                    End If
                End If
                If Trim(txtImportername.Text) <> Trim(lblagentname.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM Importer WHERE ImporterName='" & Trim(txtImportername.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Importer name already exists .');", True)
                        txtImportername.Focus()
                        Exit Sub
                    End If
                End If

                strSql = ""
                strSql += " USP_Update_Importer '" & Trim(lblImporterID.Text & "") & "','" & Replace(Trim(txtcode.Text & ""), "'", "''") & "','" & Replace(Trim(txtImportername.Text & ""), "'", "''") & "','" & Replace(Trim(txtaddress.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtcity.Text & ""), "'", "''") & "','" & Trim(ddlstste.SelectedValue & "") & "','" & Replace(Trim(txtcontactper.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtperdestinston.Text & ""), "'", "''") & "','" & Trim(txtnumber1.Text & "") & "','" & Trim(txtnumber2.Text & "") & "',"
                strSql += "'" & Trim(txtfax.Text & "") & "','" & Trim(txtmobile.Text & "") & "','" & Replace(Trim(txtemail.Text & ""), "'", "''") & "','" & Trim(txtpan.Text & "") & "',"
                strSql += "'" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record updated successfully for Importer ID " & lblImporterID.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            Else

                strSql = ""
                strSql += "USP_Importer_Name '" & Replace(Trim(txtcode.Text & ""), "'", "''") & "','" & Replace(Trim(txtImportername.Text & ""), "'", "''") & "'"
                ds = db.sub_GetDataSets(strSql)
                If (ds.Tables(0).Rows.Count > 0) Then
                    txtcode.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Importer code already exists .');", True)
                    Exit Sub
                End If
                If (ds.Tables(1).Rows.Count > 0) Then
                    txtImportername.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Importer name already exists .');", True)
                    Exit Sub
                End If

                strSql = ""
                strSql += " USP_ImporterCode_Insert_Save '" & Replace(Trim(txtcode.Text & ""), "'", "''") & "','" & Replace(Trim(txtImportername.Text & ""), "'", "''") & "','" & Replace(Trim(txtaddress.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtcity.Text & ""), "'", "''") & "','" & Trim(ddlstste.SelectedValue & "") & "','" & Replace(Trim(txtcontactper.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtperdestinston.Text & ""), "'", "''") & "','" & Trim(txtnumber1.Text & "") & "','" & Trim(txtnumber2.Text & "") & "',"
                strSql += "'" & Trim(txtfax.Text & "") & "','" & Trim(txtmobile.Text & "") & "','" & Replace(Trim(txtemail.Text & ""), "'", "''") & "','" & Trim(txtpan.Text & "") & "',"
                strSql += "'" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully for Importer ID " & dt.Rows(0)("ImporterID") & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()

            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
