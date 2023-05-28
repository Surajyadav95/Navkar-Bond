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
    Dim agentID, AgentIDEdit As String
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


            If Not (Request.QueryString("AgentIDEdit") = "") Then
                agentID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("AgentIDEdit")))
                strSql = ""
                strSql = "USP_Edit_Agent '" & agentID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblagentID.Text = Trim(dt.Rows(0)("agentID") & "")
                    lblcode.Text = Trim(dt.Rows(0)("agentCode") & "")
                    lblagentname.Text = Trim(dt.Rows(0)("agentName") & "")
                    txtcode.Text = Trim(dt.Rows(0)("agentCode") & "")
                    txtagentname.Text = Trim(dt.Rows(0)("agentName") & "")
                    txtaddress.Text = Trim(dt.Rows(0)("Address") & "")
                    txtcity.Text = Trim(dt.Rows(0)("City") & "")
                    ddlstste.SelectedValue = Trim(dt.Rows(0)("state") & "")
                    txtcontactper.Text = Trim(dt.Rows(0)("ContactPerson") & "")
                    txtperdestinston.Text = Trim(dt.Rows(0)("Designation") & "")
                    txtnumber1.Text = Trim(dt.Rows(0)("ContactNo1") & "")
                    txtnumber2.Text = Trim(dt.Rows(0)("ContactNo2") & "")
                    txtfax.Text = Trim(dt.Rows(0)("FaxNumber") & "")
                    txtmobile.Text = Trim(dt.Rows(0)("CellNumber") & "")
                    txtemail.Text = Trim(dt.Rows(0)("eMailIDs") & "")
                    txtledger.Text = Trim(dt.Rows(0)("TallyLedgerName") & "")
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")
                    txtstorageweeks.Text = Trim(dt.Rows(0)("StorageWeeks") & "")
                    txtinsuranceweeks.Text = Trim(dt.Rows(0)("InsuranceWeeks") & "")
                    txtsize20perarea.Text = Trim(dt.Rows(0)("Size20perArea") & "")
                    txtsize40perarea.Text = Trim(dt.Rows(0)("Size40perArea") & "")
                    txt45perarea.Text = Trim(dt.Rows(0)("Size45perArea") & "")

                End If
                Panel1.Enabled = True
                Panel2.Enabled = True
                Panel3.Enabled = True
                Panel4.Enabled = True
                btnSave.Text = "Update"
            End If
            If Not (Request.QueryString("AgentIDView") = "") Then
                agentID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("AgentIDView")))
                strSql = ""
                strSql = "USP_Edit_Agent '" & agentID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtcode.Text = Trim(dt.Rows(0)("agentCode") & "")
                    lblcode.Text = Trim(dt.Rows(0)("agentCode") & "")
                    lblagentname.Text = Trim(dt.Rows(0)("agentName") & "")
                    txtagentname.Text = Trim(dt.Rows(0)("agentName") & "")
                    txtaddress.Text = Trim(dt.Rows(0)("Address") & "")
                    txtcity.Text = Trim(dt.Rows(0)("City") & "")
                    ddlstste.SelectedValue = Trim(dt.Rows(0)("state") & "")
                    txtcontactper.Text = Trim(dt.Rows(0)("ContactPerson") & "")
                    txtperdestinston.Text = Trim(dt.Rows(0)("Designation") & "")
                    txtnumber1.Text = Trim(dt.Rows(0)("ContactNo1") & "")
                    txtnumber2.Text = Trim(dt.Rows(0)("ContactNo2") & "")
                    txtfax.Text = Trim(dt.Rows(0)("FaxNumber") & "")
                    txtmobile.Text = Trim(dt.Rows(0)("CellNumber") & "")
                    txtemail.Text = Trim(dt.Rows(0)("eMailIDs") & "")
                    txtledger.Text = Trim(dt.Rows(0)("TallyLedgerName") & "")
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")
                    txtstorageweeks.Text = Trim(dt.Rows(0)("StorageWeeks") & "")
                    txtinsuranceweeks.Text = Trim(dt.Rows(0)("InsuranceWeeks") & "")
                    txtsize20perarea.Text = Trim(dt.Rows(0)("Size20perArea") & "")
                    txtsize40perarea.Text = Trim(dt.Rows(0)("Size40perArea") & "")
                    txt45perarea.Text = Trim(dt.Rows(0)("Size45perArea") & "")
                End If
                Panel1.Enabled = False
                Panel2.Enabled = False
                Panel3.Enabled = False
                Panel4.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False
            End If

        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Get_Dropdown_Agent_list")
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
                    strSql += "SELECT Count(*) FROM Agent WHERE agentCode='" & Trim(txtcode.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Agent code already exists .');", True)
                        txtcode.Focus()
                        Exit Sub
                    End If
                End If
                If Trim(txtagentname.Text) <> Trim(lblagentname.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM Agent WHERE agentName='" & Trim(txtagentname.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Agent name already exists .');", True)
                        txtagentname.Focus()
                        Exit Sub
                    End If
                End If

                strSql = ""
                strSql += " USP_Update_Agent '" & Trim(lblagentID.Text & "") & "','" & Replace(Trim(txtcode.Text & ""), "'", "''") & "','" & Replace(Trim(txtagentname.Text & ""), "'", "''") & "','" & Replace(Trim(txtaddress.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtcity.Text & ""), "'", "''") & "','" & Trim(ddlstste.SelectedValue & "") & "','" & Replace(Trim(txtcontactper.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtperdestinston.Text & ""), "'", "''") & "','" & Trim(txtnumber1.Text & "") & "','" & Trim(txtnumber2.Text & "") & "',"
                strSql += "'" & Trim(txtfax.Text & "") & "','" & Trim(txtmobile.Text & "") & "','" & Replace(Trim(txtemail.Text & ""), "'", "''") & "','" & Replace(Trim(txtledger.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "','" & Trim(txtstorageweeks.Text & "") & "','" & Trim(txtinsuranceweeks.Text & "") & "',"
                strSql += "'" & Trim(txtsize20perarea.Text & "") & "','" & Trim(txtsize40perarea.Text & "") & "','" & Trim(txt45perarea.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record updated successfully for agent ID " & lblagentID.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            Else
                strSql = ""
                strSql += "USP_Agent_Name '" & Replace(Trim(txtcode.Text & ""), "'", "''") & "','" & Replace(Trim(txtagentname.Text & ""), "'", "''") & "'"
                ds = db.sub_GetDataSets(strSql)
                If (ds.Tables(0).Rows.Count > 0) Then
                    txtcode.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Agent code already exists .');", True)
                    txtcode.Focus()
                    Exit Sub
                End If
                If (ds.Tables(1).Rows.Count > 0) Then
                    txtagentname.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Agent name already exists .');", True)
                    txtagentname.Focus()
                    Exit Sub
                End If

                strSql = ""
                strSql += " USP_SaveimpAgentDetails '" & Replace(Trim(txtcode.Text & ""), "'", "''") & "','" & Replace(Trim(txtagentname.Text & ""), "'", "''") & "','" & Replace(Trim(txtaddress.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtcity.Text & ""), "'", "''") & "','" & Trim(ddlstste.SelectedValue & "") & "','" & Replace(Trim(txtcontactper.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtperdestinston.Text & ""), "'", "''") & "','" & Trim(txtnumber1.Text & "") & "','" & Trim(txtnumber2.Text & "") & "',"
                strSql += "'" & Trim(txtfax.Text & "") & "','" & Trim(txtmobile.Text & "") & "','" & Replace(Trim(txtemail.Text & ""), "'", "''") & "','" & Replace(Trim(txtledger.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "','" & Trim(txtstorageweeks.Text & "") & "','" & Trim(txtinsuranceweeks.Text & "") & "',"
                strSql += "'" & Trim(txtsize20perarea.Text & "") & "','" & Trim(txtsize40perarea.Text & "") & "','" & Trim(txt45perarea.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully agent ID " & dt.Rows(0)("agentID") & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
