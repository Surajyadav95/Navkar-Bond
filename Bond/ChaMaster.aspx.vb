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
    Dim CHANo, CHANoEdit As String
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

            If Not (Request.QueryString("CHANoEdit") = "") Then
                CHANo = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("CHANoEdit")))
                strSql = ""
                strSql = "USP_Edit_CHA'" & CHANo & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtchano.Text = Trim(dt.Rows(0)("CHANo") & "")
                    lblCHano.Text = Trim(dt.Rows(0)("chacode") & "")
                    lblChaname.Text = Trim(dt.Rows(0)("CHAName") & "")
                    txtchac.Text = Trim(dt.Rows(0)("chacode") & "")
                    txtchaname.Text = Trim(dt.Rows(0)("CHAName") & "")
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
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")

                End If
                Panel1.Enabled = True
                Panel2.Enabled = True
                btnSave.Text = "Update"
            End If
            If Not (Request.QueryString("CHANoView") = "") Then
                CHANo = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("CHANoView")))
                strSql = ""
                strSql = " USP_Edit_CHA'" & CHANo & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtchano.Text = Trim(dt.Rows(0)("CHANo") & "")
                    txtchac.Text = Trim(dt.Rows(0)("chacode") & "")
                    txtchaname.Text = Trim(dt.Rows(0)("CHAName") & "")
                    lblCHano.Text = Trim(dt.Rows(0)("chacode") & "")
                    lblChaname.Text = Trim(dt.Rows(0)("CHAName") & "")
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
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")

                End If
                Panel1.Enabled = False
                Panel2.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False
                txtremarks.ReadOnly = True
                chkisActive.Enabled = False
            End If
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Get_Dropdown_Cha_list")
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
                If Trim(txtchac.Text) <> Trim(lblCHano.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM CHA WHERE chacode='" & Trim(txtchac.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('CHA code already exists .');", True)
                        txtchac.Focus()
                        Exit Sub
                    End If
                End If
                If Trim(txtchaname.Text) <> Trim(lblChaname.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM CHA  WHERE CHAName='" & Trim(txtchaname.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('CHA name already exists .');", True)
                        txtchaname.Focus()
                        Exit Sub
                    End If
                End If

                strSql = ""
                strSql += "USP_UPDATE_CHA '" & Trim(txtchano.Text & "") & "','" & Trim(txtchac.Text & "") & "','" & Trim(txtchaname.Text & "") & "','" & Trim(txtaddress.Text & "") & "',"
                strSql += "'" & Trim(txtcity.Text & "") & "','" & Trim(ddlstste.SelectedValue & "") & "','" & Trim(txtcontactper.Text & "") & "',"
                strSql += "'" & Trim(txtperdestinston.Text & "") & "','" & Trim(txtnumber1.Text & "") & "','" & Trim(txtnumber2.Text & "") & "',"
                strSql += "'" & Trim(txtfax.Text & "") & "','" & Trim(txtmobile.Text & "") & "','" & Trim(txtemail.Text & "") & "',"
                strSql += "'" & Trim(txtremarks.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record updated successfully for CHA No " & txtchano.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            Else
                strSql = ""
                strSql += "USP_CHA_Name '" & Replace(Trim(txtchac.Text & ""), "'", "''") & "','" & Replace(Trim(txtchaname.Text & ""), "'", "''") & "'"
                ds = db.sub_GetDataSets(strSql)
                If (ds.Tables(0).Rows.Count > 0) Then
                    txtchac.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('CHA code already exists .');", True)
                    Exit Sub
                End If
                If (ds.Tables(1).Rows.Count > 0) Then
                    txtchaname.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('CHA name already exists .');", True)
                    Exit Sub
                End If
                strSql = ""
                strSql += " USP_SaveCHADetails '" & Trim(txtchano.Text & "") & "','" & Replace(Trim(txtchac.Text & ""), "'", "''") & "','" & Replace(Trim(txtchaname.Text & ""), "'", "''") & "','" & Replace(Trim(txtaddress.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtcity.Text & ""), "'", "''") & "','" & Trim(ddlstste.SelectedValue & "") & "','" & Replace(Trim(txtcontactper.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtperdestinston.Text & ""), "'", "''") & "','" & Trim(txtnumber1.Text & "") & "','" & Trim(txtnumber2.Text & "") & "',"
                strSql += "'" & Trim(txtfax.Text & "") & "','" & Trim(txtmobile.Text & "") & "','" & Replace(Trim(txtemail.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "','" & Trim(txtchano.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully CHA No " & dt.Rows(0)("chano") & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
