Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim NOCNo, NOCNoEdit As String
    Dim ed As New clsEncodeDecode
    Public Sub grid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from Temp_Container Where UniqueID=" & Session("UserId_BondCFS") & "")
            'txtnocDate.Attributes("type") = "date"
            'txtnocDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            'txtbedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            'txtigmDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtnocDate.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm")
            txtbedate.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm")
            txtigmDate.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm")

            Filldropdown()
            Container(sender, e)
            If Not (Request.QueryString("NOCNoEdit") = "") Then
                NOCNo = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("NOCNoEdit")))
                strSql = ""
                strSql = "USP_Edit_NOC '" & NOCNo & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                Container(sender, e)
                If (dt.Rows.Count > 0) Then
                    txtnoc.Text = Trim(dt.Rows(0)("NOCNo") & "")
                    txtnocDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Noc") & "")).ToString("yyyy-MM-ddTHH:mm")
                    ddlbond.SelectedValue = Trim(dt.Rows(0)("BondType") & "")
                    txtbe.Text = Trim(dt.Rows(0)("BOENo") & "")
                    txtbedate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Boe") & "")).ToString("yyyy-MM-ddTHH:mm")
                    txtigm.Text = Trim(dt.Rows(0)("IGMNo") & "")
                    Txtitem.Text = Trim(dt.Rows(0)("ItemNo") & "")
                    txtigmDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("IGMD") & "")).ToString("yyyy-MM-ddTHH:mm")
                    txtweek.Text = Trim(dt.Rows(0)("Weeks") & "")
                    txtday.Text = Trim(dt.Rows(0)("Days") & "")
                    txtexpirydate.Text = Trim(dt.Rows(0)("Expiry") & "")
                    ddlCustomer.SelectedValue = Trim(dt.Rows(0)("CustID") & "")
                    ddlimport.SelectedValue = Trim(dt.Rows(0)("ImporterId") & "")
                    ddlcha.SelectedValue = Trim(dt.Rows(0)("CHAID") & "")
                    txtcommodity.Text = Trim(dt.Rows(0)("commodity") & "")
                    ddlstatus.SelectedValue = Trim(dt.Rows(0)("Status") & "")
                    ddlcargo.SelectedValue = Trim(dt.Rows(0)("Cargotype") & "")
                    txtqty.Text = Trim(dt.Rows(0)("QTY") & "")
                    ddlunit.SelectedValue = Trim(dt.Rows(0)("Unit") & "")
                    txtgrosskgs.Text = Trim(dt.Rows(0)("GrossWt") & "")
                    txtstorsqm.Text = Trim(dt.Rows(0)("StorageSpace") & "")
                    txtvalue.Text = Trim(dt.Rows(0)("Value") & "")
                    txtduty.Text = Trim(dt.Rows(0)("duty") & "")
                    txtcontact.Text = Trim(dt.Rows(0)("contactno") & "")
                    txtinsweeks.Text = Trim(dt.Rows(0)("InsuranceWeeks") & "")
                    txtinsdays.Text = Trim(dt.Rows(0)("InsuranceDays") & "")
                    txtinsexpdate.Text = Trim(dt.Rows(0)("InsuranceExpiry") & "")

                End If
                Panel2.Enabled = True
                Panel3.Enabled = True
                btnSave.Text = "Modify"
            End If
            If Not (Request.QueryString("NOCNoView") = "") Then
                NOCNo = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("NOCNoView")))
                strSql = ""
                strSql = "USP_Edit_NOC '" & NOCNo & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                Container(sender, e)
                If (dt.Rows.Count > 0) Then
                    txtnoc.Text = Trim(dt.Rows(0)("NOCNo") & "")
                    txtnocDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Noc") & "")).ToString("yyyy-MM-ddTHH:mm")
                    ddlbond.SelectedValue = Trim(dt.Rows(0)("BondType") & "")
                    txtbe.Text = Trim(dt.Rows(0)("BOENo") & "")
                    txtbedate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Boe") & "")).ToString("yyyy-MM-ddTHH:mm")
                    txtigm.Text = Trim(dt.Rows(0)("IGMNo") & "")
                    Txtitem.Text = Trim(dt.Rows(0)("ItemNo") & "")
                    txtigmDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("IGMD") & "")).ToString("yyyy-MM-ddTHH:mm")
                    txtweek.Text = Trim(dt.Rows(0)("Weeks") & "")
                    txtday.Text = Trim(dt.Rows(0)("Days") & "")
                    txtexpirydate.Text = Trim(dt.Rows(0)("Expiry") & "")
                    ddlCustomer.SelectedValue = Trim(dt.Rows(0)("CustID") & "")
                    ddlimport.SelectedValue = Trim(dt.Rows(0)("ImporterId") & "")
                    ddlcha.SelectedValue = Trim(dt.Rows(0)("CHAID") & "")
                    txtcommodity.Text = Trim(dt.Rows(0)("commodity") & "")
                    ddlstatus.SelectedValue = Trim(dt.Rows(0)("Status") & "")
                    ddlcargo.SelectedValue = Trim(dt.Rows(0)("Cargotype") & "")
                    txtqty.Text = Trim(dt.Rows(0)("QTY") & "")
                    ddlunit.SelectedValue = Trim(dt.Rows(0)("Unit") & "")
                    txtgrosskgs.Text = Trim(dt.Rows(0)("GrossWt") & "")
                    txtstorsqm.Text = Trim(dt.Rows(0)("StorageSpace") & "")
                    txtvalue.Text = Trim(dt.Rows(0)("Value") & "")
                    txtduty.Text = Trim(dt.Rows(0)("duty") & "")
                    txtcontact.Text = Trim(dt.Rows(0)("contactno") & "")
                    txtinsweeks.Text = Trim(dt.Rows(0)("InsuranceWeeks") & "")
                    txtinsdays.Text = Trim(dt.Rows(0)("InsuranceDays") & "")
                    txtinsexpdate.Text = Trim(dt.Rows(0)("InsuranceExpiry") & "")
                End If
                Panel2.Enabled = False
                Panel3.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False
            End If
            ddlbond.Focus()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Get_Dropdown_Noc_list")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If
            ds = db.sub_GetDataSets("USP_Fill_Noc_list")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlCustomer.DataSource = ds.Tables(0)
                ddlCustomer.DataTextField = "agentName"
                ddlCustomer.DataValueField = "agentID"
                ddlCustomer.DataBind()
                ddlCustomer.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If (ds.Tables(1).Rows.Count > 0) Then
                ddlimport.DataSource = ds.Tables(1)
                ddlimport.DataTextField = "ImporterName"
                ddlimport.DataValueField = "ImporterID"
                ddlimport.DataBind()
                ddlimport.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If (ds.Tables(2).Rows.Count > 0) Then
                ddlcha.DataSource = ds.Tables(2)
                ddlcha.DataTextField = "CHAName"
                ddlcha.DataValueField = "CHAID"
                ddlcha.DataBind()
                ddlcha.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            ddlunit.DataSource = ds.Tables(3)
            ddlunit.DataTextField = "Package"
            ddlunit.DataValueField = "CodeID"
            ddlunit.DataBind()
            ddlunit.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtbe_TextChanged(sender As Object, e As EventArgs)
        Try
            dt = db.sub_GetDatatable("USP_Noc_Be'" & Trim(txtbe.Text & "") & "'")
            If (dt.Rows.Count > 0) Then
                txtbe.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('BE No is already generated.');", True)
                Exit Sub
                'UpdatePanel9.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If Val(textqt.Text) = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid qty');", True)
                textqt.Text = ""
                textqt.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql = "USP_Insert '" & Trim(txtcontainer.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                txtcontainer.Text = ""
                textqt.Text = ""
                ddlsize.SelectedValue = "0"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No already Added .');", True)
                Exit Sub
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                If Val(txtqty.Text) < (Val(ds.Tables(1).Rows(0)("Qty")) + Val(textqt.Text)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Containers qty should not be greater than total noc qty');", True)
                    textqt.Text = ""
                    textqt.Focus()
                    Exit Sub
                End If
            End If
            strSql = ""
            strSql = "USP_Insert_Container "
            strSql += "'" & Trim(txtcontainer.Text & "") & "','" & Trim(textqt.Text & "") & "','" & Trim(ddlsize.SelectedItem.Text) & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            Container(sender, e)
            txtcontainer.Text = ""
            textqt.Text = ""
            ddlsize.SelectedValue = "0"
            up_grid.Update()
            btnAdd.Text = "Add"
            btnAdd.Attributes.Add("Class", "btn btn-primary")
        Catch ex As Exception
            btnAdd.Text = "Add"
            btnAdd.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Container(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            dt = db.sub_GetDatatable("USP_Select_Temp_Container '" & Session("UserId_BondCFS") & "'")
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            Dim strContainer As String = "", intcontainerA As Integer = 0, intcontainerB As Integer = 0, intcontainerC As Integer = 0
            For i = 0 To dt.Rows.Count - 1
                strContainer = Trim(dt.Rows(i)("Container_Size"))
                If InStr(strContainer, "20") > 0 Then
                    intcontainerA += 1
                ElseIf InStr(strContainer, "40") > 0 Then
                    intcontainerB += 1
                ElseIf InStr(strContainer, "45") > 0 Then
                    intcontainerC += 1
                End If
            Next
            lblA.Text = intcontainerA
            lblB.Text = intcontainerB
            lblC.Text = intcontainerC
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            dt = db.sub_GetDatatable("USP_Delete_Container '" & AutoID & "','" & Session("UserId_BondCFS") & "'")
            Container(sender, e)
            If (dt.Rows.Count > 0) Then
            End If
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If (btnSave.Text = "Modify") Then
                strSql = ""
                strSql += "USP_Update_NOC '" & Trim(txtnoc.Text & "") & "','" & Convert.ToDateTime(Trim(txtnocDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlbond.SelectedItem.Text & "") & "','" & Trim(txtbe.Text & "") & "','" & Convert.ToDateTime(Trim(txtbedate.Text & "")).ToString("yyyy-MM-dd") & "',"
                strSql += "'" & Trim(txtigm.Text & "") & "','" & Trim(Txtitem.Text & "") & "','" & Convert.ToDateTime(Trim(txtigmDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtday.Text & "") & "',"
                strSql += "'" & Convert.ToDateTime(Trim(txtexpirydate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlCustomer.SelectedValue & "") & "','" & Trim(ddlimport.SelectedValue & "") & "',"
                strSql += "'" & Trim(ddlcha.SelectedValue & "") & "','" & Trim(txtcommodity.Text & "") & "','" & Trim(ddlstatus.SelectedItem.Text & "") & "','" & Trim(ddlcargo.SelectedItem.Text & "") & "',"
                strSql += "'" & Trim(txtqty.Text & "") & "','" & Trim(ddlunit.SelectedValue & "") & "','" & Trim(txtgrosskgs.Text & "") & "','" & Trim(txtstorsqm.Text & "") & "','" & Trim(txtvalue.Text & "") & "','" & Trim(txtduty.Text & "") & "','" & Trim(txtcontact.Text & "") & "','" & Session("UserId_BondCFS") & "',"
                strSql += "'" & Trim(txtinsdays.Text & "") & "','" & Convert.ToDateTime(Trim(txtinsexpdate.Text & "")).ToString("yyyy-MM-dd") & "'"
                dt = db.sub_GetDatatable(strSql)
                Container_dates(sender, e)
                btnSave.Text = "Modify"
                btnSave.Attributes.Add("Class", "btn btn-primary")
                lblSession.Text = "Record modified successfully for NOC No " & txtnoc.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel5.Update()
            Else
                strSql = ""
                strSql += "USP_Insert_NOC '" & Convert.ToDateTime(Trim(txtnocDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlbond.SelectedItem.Text & "") & "','" & Trim(txtbe.Text & "") & "','" & Convert.ToDateTime(Trim(txtbedate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                strSql += "'" & Trim(txtigm.Text & "") & "','" & Trim(Txtitem.Text & "") & "','" & Convert.ToDateTime(Trim(txtigmDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtday.Text & "") & "',"
                strSql += "'" & Convert.ToDateTime(Trim(txtexpirydate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlCustomer.SelectedValue & "") & "','" & Trim(ddlimport.SelectedValue & "") & "',"
                strSql += "'" & Trim(ddlcha.SelectedValue & "") & "','" & Trim(txtcommodity.Text & "") & "','" & Trim(ddlstatus.SelectedItem.Text & "") & "','" & Trim(ddlcargo.SelectedItem.Text & "") & "',"
                strSql += "'" & Trim(txtqty.Text & "") & "','" & Trim(ddlunit.SelectedValue & "") & "','" & Trim(txtgrosskgs.Text & "") & "','" & Trim(txtstorsqm.Text & "") & "','" & Trim(txtvalue.Text & "") & "','" & Trim(txtduty.Text & "") & "','" & Trim(txtcontact.Text & "") & "', "
                strSql += "'" & Trim(lblA.Text & "") & "','" & Trim(lblB.Text & "") & "','" & Trim(lblC.Text & "") & "','" & Session("UserId_BondCFS") & "',"
                strSql += "'" & Trim(txtinsdays.Text & "") & "','" & Convert.ToDateTime(Trim(txtinsexpdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"

                dt = db.sub_GetDatatable(strSql)
                txtnoc.Text = dt.Rows(0)("NOCNo")
                Container_dates(sender, e)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary")
                lblSession.Text = "Record saved successfully for NOC No " & dt.Rows(0)("NOCNo") & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel5.Update()
            End If
        Catch ex As Exception
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Container_dates(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_NOC_dets '" & txtnoc.Text & "'"
            db.sub_ExecuteNonQuery(strSql)
            db.sub_ExecuteNonQuery("USP_INSERT_NOC_Dets '" & txtnoc.Text & "','" & Session("UserId_BondCFS") & "'")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtweek_TextChanged(sender As Object, e As EventArgs)
        Try
            'Dim bedate As DateTime = Convert.ToDateTime(txtbedate.Text)
            If txtnocDate.Text <> "" Then
                If txtweek.Text <> "" Then
                    txtday.Text = Val(txtweek.Text) * 7
                    'Dim strdate As Date = DateTime.ParseExact(txtbedate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    'Dim Diff As Date = DateAdd("d", (Val(txtweek.Text) * 7 - 1), strdate)
                    'Dim strdiffdate As Date = Format(Diff, "dd-MM-yyyy")
                    txtexpirydate.Text = Convert.ToDateTime(DateAdd("d", (Val(txtweek.Text) * 7 - 1), Convert.ToDateTime(txtnocDate.Text).ToString("dd-MM-yyyy HH:mm"))).ToString("dd-MM-yyyy HH:mm")
                    'txtexpirydate.Text = strdiffdate
                    txtinsweeks.Focus()
                End If
            End If
            'UpdatePanel4.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub txtinsweeks_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtnocDate.Text <> "" Then
                If txtinsweeks.Text <> "" Then
                    txtinsdays.Text = Val(txtinsweeks.Text) * 7
                    txtinsexpdate.Text = Convert.ToDateTime(DateAdd("d", (Val(txtinsweeks.Text) * 7 - 1), Convert.ToDateTime(txtnocDate.Text).ToString("dd-MM-yyyy HH:mm"))).ToString("dd-MM-yyyy HH:mm")
                    'txtexpirydate.Text = strdiffdate
                    ddlcha.Focus()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlCustomer_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            txtweek.Text = ""
            txtday.Text = ""
            txtexpirydate.Text = ""
            txtinsweeks.Text = ""
            txtinsdays.Text = ""
            txtinsexpdate.Text = ""
            If ddlCustomer.SelectedValue <> 0 Then
                strSql = ""
                strSql += "select isnull(StorageWeeks,0) StorageWeeks,Isnull(InsuranceWeeks,0) InsuranceWeeks from agent where agentID=" & ddlCustomer.SelectedValue & ""
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0)("StorageWeeks")) <> 0 Then
                        txtweek.Text = Val(dt.Rows(0)("StorageWeeks"))
                        txtweek_TextChanged(sender, e)
                    Else
                        txtweek.Text = ""
                    End If
                    If Val(dt.Rows(0)("InsuranceWeeks")) <> 0 Then
                        txtinsweeks.Text = Val(dt.Rows(0)("InsuranceWeeks"))
                        txtinsweeks_TextChanged(sender, e)
                    Else
                        txtinsweeks.Text = ""
                    End If
                End If
            End If
            ddlCustomer.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
