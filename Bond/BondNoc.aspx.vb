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
    Public Sub Clear(sender As Object, e As EventArgs)
        Try
            db.sub_ExecuteNonQuery("Delete from Temp_Container Where UniqueID=" & Session("UserId_BondCFS") & "")
            'txtnocDate.Attributes("type") = "date"
            txtnocDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtbedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtigmDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            Container(sender, e)        
            ddlbond.SelectedValue = 0
            txtbe.Text = ""
            txtigm.Text = ""
            Txtitem.Text = ""
            txtweek.Text = ""
            txtday.Text = ""
            txtexpirydate.Text = ""
            ddlCustomer.SelectedValue = 0
            ddlimport.SelectedValue = 0
            ddlcha.SelectedValue = 0
            txtcommodity.Text = ""
            ddlstatus.SelectedValue = 0
            ddlcargo.SelectedValue = 0
            txtqty.Text = ""
            ddlunit.SelectedValue = 0
            txtgrosskgs.Text = ""
            txtstorsqm.Text = ""
            txtvalue.Text = ""
            txtduty.Text = ""
            txtcontact.Text = ""
            txtinsweeks.Text = ""
            txtinsdays.Text = ""
            txtinsexpdate.Text = ""
            ddlimport.SelectedValue = 0
            ddlWarehouse.SelectedValue = 0
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from Temp_Container Where UniqueID=" & Session("UserId_BondCFS") & "")
            'txtnocDate.Attributes("type") = "date"
            txtnocDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtbedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtigmDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            'txtnocDate.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm")
            'txtbedate.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm")
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
                    txtnocDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Noc") & "")).ToString("yyyy-MM-dd")
                    ddlbond.SelectedValue = Trim(dt.Rows(0)("BondType") & "")
                    txtbe.Text = Trim(dt.Rows(0)("BOENo") & "")
                    txtbedate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Boe") & "")).ToString("yyyy-MM-dd")
                    txtigm.Text = Trim(dt.Rows(0)("IGMNo") & "")
                    Txtitem.Text = Trim(dt.Rows(0)("ItemNo") & "")
                    txtigmDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("IGMD") & "")).ToString("yyyy-MM-dd")
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
                    ddlimport.SelectedValue = Trim(dt.Rows(0)("ImporterId") & "")
                    ddlWarehouse.SelectedValue = Trim(dt.Rows(0)("WR_CODE") & "")
                    ddlCargoCategory.SelectedValue = Trim(dt.Rows(0)("CargoCategory") & "")

                End If

                Panel2.Enabled = True
                Panel3.Enabled = True
                PanelWarehouse.Enabled = False
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
                    txtnocDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Noc") & "")).ToString("yyyy-MM-dd")
                    ddlbond.SelectedValue = Trim(dt.Rows(0)("BondType") & "")
                    txtbe.Text = Trim(dt.Rows(0)("BOENo") & "")
                    txtbedate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Boe") & "")).ToString("yyyy-MM-dd")
                    txtigm.Text = Trim(dt.Rows(0)("IGMNo") & "")
                    Txtitem.Text = Trim(dt.Rows(0)("ItemNo") & "")
                    txtigmDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("IGMD") & "")).ToString("yyyy-MM-dd")
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
                    ddlimport.SelectedValue = Trim(dt.Rows(0)("ImporterId") & "")
                    ddlWarehouse.SelectedValue = Trim(dt.Rows(0)("WR_CODE") & "")
                    ddlCargoCategory.SelectedValue = Trim(dt.Rows(0)("CargoCategory") & "")

                End If
                Panel2.Enabled = False
                Panel3.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False
            End If
            ddlWarehouse.Focus()
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

            ddlWarehouse.DataSource = ds.Tables(5)
            ddlWarehouse.DataTextField = "Warehouse_code"
            ddlWarehouse.DataValueField = "Warehouse_code"
            ddlWarehouse.DataBind()
            ddlWarehouse.Items.Insert(0, New ListItem("--Select--", 0))

            ddlType.DataSource = ds.Tables(6)
            ddlType.DataTextField = "Containertype"
            ddlType.DataValueField = "ContainertypeID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("--Select--", 0))

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
            If ds.Tables(1).Rows.Count > 0 Then
                If Val(txtgrosskgs.Text) < (Val(ds.Tables(1).Rows(0)("Weight")) + Val(txtWeight.Text)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Weight should not be greater than total noc Weight');", True)
                    txtWeight.Text = ""
                    txtWeight.Focus()
                    Exit Sub
                End If
            End If
            strSql = ""
            strSql = "USP_Insert_Container "
            strSql += "'" & Trim(txtcontainer.Text & "") & "','" & Trim(textqt.Text & "") & "','" & Trim(ddlsize.SelectedItem.Text) & "','" & Session("UserId_BondCFS") & "'"
            strSql += "," & Val(ddlType.SelectedValue) & ",'" & Trim(txtWeight.Text & "") & "'"
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
            up_grid.Update()
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
                strSql += "'" & Trim(ddlcha.SelectedValue & "") & "','" & Replace(Trim(txtcommodity.Text & ""), "'", "''") & "','" & Trim(ddlstatus.SelectedItem.Text & "") & "','" & Trim(ddlcargo.SelectedItem.Text & "") & "',"
                strSql += "'" & Trim(txtqty.Text & "") & "','" & Trim(ddlunit.SelectedValue & "") & "','" & Trim(txtgrosskgs.Text & "") & "','" & Trim(txtstorsqm.Text & "") & "','" & Trim(txtvalue.Text & "") & "','" & Trim(txtduty.Text & "") & "','" & Trim(txtcontact.Text & "") & "','" & Session("UserId_BondCFS") & "',"
                strSql += "'" & Trim(txtinsdays.Text & "") & "','" & Convert.ToDateTime(Trim(txtinsexpdate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlCargoCategory.SelectedValue & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                Container_dates(sender, e)
                btnSave.Text = "Modify"
                btnSave.Attributes.Add("Class", "btn btn-primary")
                Clear(sender, e)
                lblSession.Text = "Record modified successfully for NOC No " & txtnoc.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel5.Update()
            Else
                strSql = ""
                strSql += "USP_Insert_NOC '" & Convert.ToDateTime(Trim(txtnocDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlbond.SelectedItem.Text & "") & "','" & Trim(txtbe.Text & "") & "','" & Convert.ToDateTime(Trim(txtbedate.Text & "")).ToString("yyyy-MM-dd") & "',"
                strSql += "'" & Trim(txtigm.Text & "") & "','" & Trim(Txtitem.Text & "") & "','" & Convert.ToDateTime(Trim(txtigmDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtday.Text & "") & "',"
                strSql += "'" & Convert.ToDateTime(Trim(txtexpirydate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlCustomer.SelectedValue & "") & "','" & Trim(ddlimport.SelectedValue & "") & "',"
                strSql += "'" & Trim(ddlcha.SelectedValue & "") & "','" & Replace(Trim(txtcommodity.Text & ""), "'", "''") & "','" & Trim(ddlstatus.SelectedItem.Text & "") & "','" & Trim(ddlcargo.SelectedItem.Text & "") & "',"
                strSql += "'" & Trim(txtqty.Text & "") & "','" & Trim(ddlunit.SelectedValue & "") & "','" & Trim(txtgrosskgs.Text & "") & "','" & Trim(txtstorsqm.Text & "") & "','" & Trim(txtvalue.Text & "") & "','" & Trim(txtduty.Text & "") & "','" & Trim(txtcontact.Text & "") & "', "
                strSql += "'" & Trim(lblA.Text & "") & "','" & Trim(lblB.Text & "") & "','" & Trim(lblC.Text & "") & "','" & Session("UserId_BondCFS") & "',"
                strSql += "'" & Trim(txtinsdays.Text & "") & "','" & Convert.ToDateTime(Trim(txtinsexpdate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlWarehouse.SelectedValue & "") & "','" & Trim(ddlCargoCategory.SelectedValue & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                txtnoc.Text = dt.Rows(0)("NOCNo")
                Container_dates(sender, e)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary")
                Clear(sender, e)
                lblSession.Text = "Record saved successfully for NOC No " & txtnoc.Text & ""
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
                    txtexpirydate.Text = Convert.ToDateTime(DateAdd("d", (Val(txtweek.Text) * 7 - 1), Convert.ToDateTime(txtnocDate.Text).ToString("dd-MM-yyyy"))).ToString("dd-MM-yyyy")
                    'txtexpirydate.Text = strdiffdate
                    txtinsweeks.Focus()
                End If
            End If
            updatepanel2.Update()
            updatepanel4.Update()
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
                    txtinsexpdate.Text = Convert.ToDateTime(DateAdd("d", (Val(txtinsweeks.Text) * 7 - 1), Convert.ToDateTime(txtnocDate.Text).ToString("dd-MM-yyyy"))).ToString("dd-MM-yyyy")
                    'txtexpirydate.Text = strdiffdate
                    ddlcha.Focus()
                End If
            End If
            updatepanel2.Update()
            updatepanel4.Update()
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
                strSql += "select isnull(StorageWeeks,0) StorageWeeks,Isnull(InsuranceWeeks,0) InsuranceWeeks from bond_tariffmaster where custID=" & ddlCustomer.SelectedValue & ""
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
    Protected Sub SaveOk_ServerClick(sender As Object, e As EventArgs)
        Try
            lblPrintQue.Text = "Do you wish to print?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtigm_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtigm.Text <> "" And Txtitem.Text <> "" Then
                strSql = ""
                strSql += "USP_IGM_ITEM_NO_TEXT_CHANGED '" & Trim(txtigm.Text & "") & "','" & Trim(Txtitem.Text & "") & "','" & Session("UserId_BondCFS") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                If dt1.Rows.Count > 0 Then
                    ddlcha.SelectedValue = Val(dt1.Rows(0)("CHAID"))
                    ddlimport.SelectedValue = Val(dt1.Rows(0)("Importerid"))
                    ddlCustomer.SelectedValue = Val(dt1.Rows(0)("AgentID"))
                    ddlCustomer_SelectedIndexChanged(sender, e)
                    txtqty.Text = Trim(dt1.Rows(0)("IGMQty"))
                    txtgrosskgs.Text = Trim(dt1.Rows(0)("CargoWt"))
                    txtcommodity.Text = Trim(dt1.Rows(0)("IGM_GoodsDesc"))
                    Container(sender, e)
                End If
            End If
            updatepanel2.Update()
            updatepanel3.Update()
            Txtitem.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Txtitem_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtigm.Text <> "" And Txtitem.Text <> "" Then
                strSql = ""
                strSql += "USP_IGM_ITEM_NO_TEXT_CHANGED '" & Trim(txtigm.Text & "") & "','" & Trim(Txtitem.Text & "") & "','" & Session("UserId_BondCFS") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                If dt1.Rows.Count > 0 Then
                    ddlcha.SelectedValue = Val(dt1.Rows(0)("CHAID"))
                    ddlimport.SelectedValue = Val(dt1.Rows(0)("Importerid"))
                    ddlCustomer.SelectedValue = Val(dt1.Rows(0)("AgentID"))
                    ddlCustomer_SelectedIndexChanged(sender, e)
                    txtqty.Text = Trim(dt1.Rows(0)("IGMQty"))
                    txtgrosskgs.Text = Trim(dt1.Rows(0)("CargoWt"))
                    txtcommodity.Text = Trim(dt1.Rows(0)("IGM_GoodsDesc"))
                    txtbe.Text = Trim(dt1.Rows(0)("BOENo"))
                    txtbedate.Text = Convert.ToDateTime((dt1.Rows(0)("BOEDate"))).ToString("yyyy-MM-dd")
                    Container(sender, e)
                End If
            End If
            updatepanel2.Update()
            updatepanel3.Update()
            updatepanelnew.Update()
            txtigmDate.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlWarehouse_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlbond.SelectedValue = 0
            strSql = ""
            strSql += "select BNDTYPE from warehousem where Warehouse_code='" & Trim(ddlWarehouse.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddlbond.SelectedValue = Trim(dt.Rows(0)("BNDTYPE"))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
