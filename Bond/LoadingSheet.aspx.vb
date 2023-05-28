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
    Dim Entry, EntryIDEdit As String
    Dim ed As New clsEncodeDecode
    Public Sub grid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
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
        '    Session("Workyear") = Request.Cookies("Workyear").Value

        'End If
        If Not IsPostBack Then
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            db.sub_ExecuteNonQuery("Delete from Temp_Container_Ex_Loading Where UniqueID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Godown_Ex_Loading Where UniqueID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Loading_Entry Where UserID=" & Session("UserId_BondCFS") & "")


            txtExBedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")

            Filldropdown()
            Container(sender, e)
            Godown(sender, e)

            If Not (Request.QueryString("EntryIDEdit") = "") Then
                Entry = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("EntryIDEdit")))
                strSql = ""
                strSql = "USP_Edit_BondEx '" & Entry & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                'Container(sender, e)
                Godown(sender, e)

                If (dt.Rows.Count > 0) Then
                    txtentry.Text = Trim(dt.Rows(0)("EntryID") & "")
                    txtnocn.Text = Trim(dt.Rows(0)("NOCNo") & "")
                    txtEx.Text = Trim(dt.Rows(0)("ExBoeNo") & "")
                    txtExBedate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Date") & "")).ToString("yyyy-MM-dd")
                    txtqtydeliv.Text = Trim(dt.Rows(0)("Qty") & "")
                    txtgrosswdeli.Text = Trim(dt.Rows(0)("GrossWt") & "")
                    ddlunit.SelectedValue = Trim(dt.Rows(0)("Unit") & "")
                    textrelea.Text = Trim(dt.Rows(0)("AreaReleased") & "")
                    txtvalu.Text = Trim(dt.Rows(0)("DeliveredValue") & "")
                    'txtbalduty.Text = Trim(dt.Rows(0)("Duty") & "")
                    txtdut.Text = Trim(dt.Rows(0)("DeliveredDuty") & "")
                    ddlEquipment.SelectedValue = Trim(dt.Rows(0)("EquipmentType") & "")
                    ddlSurveyor.SelectedValue = Trim(dt.Rows(0)("SurveyorName") & "")
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    ddlEquipmentNo.SelectedValue = Trim(dt.Rows(0)("equipmentno") & "")

                    Bond_Ex(sender, e)
                End If
                Panel5.Enabled = True
                Panel3.Enabled = True
                Panel2.Enabled = True
                txtnocn.ReadOnly = True
                Button2.Text = "Modify"
            End If

            If Not (Request.QueryString("EntryIDView") = "") Then
                Entry = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("EntryIDView")))
                strSql = ""
                strSql = "USP_Edit_BondEx '" & Entry & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                'Container(sender, e)
                Godown(sender, e)

                If (dt.Rows.Count > 0) Then
                    txtentry.Text = Trim(dt.Rows(0)("EntryID") & "")
                    txtnocn.Text = Trim(dt.Rows(0)("NOCNo") & "")
                    txtEx.Text = Trim(dt.Rows(0)("ExBoeNo") & "")
                    txtExBedate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Date") & "")).ToString("yyyy-MM-dd")
                    txtqtydeliv.Text = Trim(dt.Rows(0)("Qty") & "")
                    ddlunit.SelectedValue = Trim(dt.Rows(0)("Unit") & "")
                    txtgrosswdeli.Text = Trim(dt.Rows(0)("GrossWt") & "")
                    textrelea.Text = Trim(dt.Rows(0)("AreaReleased") & "")
                    txtvalu.Text = Trim(dt.Rows(0)("DeliveredValue") & "")
                    'txtbalduty.Text = Trim(dt.Rows(0)("Duty") & "")
                    txtdut.Text = Trim(dt.Rows(0)("DeliveredDuty") & "")
                    ddlEquipment.SelectedValue = Trim(dt.Rows(0)("EquipmentType") & "")
                    ddlSurveyor.SelectedValue = Trim(dt.Rows(0)("SurveyorName") & "")
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    ddlEquipmentNo.SelectedValue = Trim(dt.Rows(0)("equipmentno") & "")

                    Bond_Ex(sender, e)
                End If
                Panel1.Enabled = False
                Panel5.Enabled = False
                Panel4.Enabled = False
                Panel3.Enabled = False
                Panel2.Enabled = False
                txtnocn.ReadOnly = True
                Button2.Text = "View"
                Button2.Visible = False
                btnClear.Visible = False

            End If

            ddlware_SelectedIndexChanged(sender, e)
            txtUnloadingSheetNo.Focus()

        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Get_Dropdown_Bond_Ex_list_Loading")
            If dt.Rows.Count > 0 Then
                rptNOLIst.DataSource = dt
                rptNOLIst.DataBind()
            End If

            ds = db.sub_GetDataSets("USP_Fill_Ex_list")
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

            If (ds.Tables(3).Rows.Count > 0) Then
                ddlware.DataSource = ds.Tables(3)
                ddlware.DataTextField = "WHName"
                ddlware.DataValueField = "WHID"
                ddlware.DataBind()
                ddlware.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            'If (ds.Tables(4).Rows.Count > 0) Then
            '    ddlgodown.DataSource = ds.Tables(4)
            '    ddlgodown.DataTextField = "GodownCode"
            '    ddlgodown.DataValueField = "entryid"
            '    ddlgodown.DataBind()
            '    ddlgodown.Items.Insert(0, New ListItem("--Select--", 0))
            'End If

            'If (ds.Tables(5).Rows.Count > 0) Then
            '    ddllot.DataSource = ds.Tables(5)
            '    ddllot.DataTextField = "LotNo"
            '    ddllot.DataValueField = "entryid"
            '    ddllot.DataBind()
            '    ddllot.Items.Insert(0, New ListItem("--Select--", 0))
            'End If

            If (ds.Tables(6).Rows.Count > 0) Then
                ddlEquipment.DataSource = ds.Tables(6)
                ddlEquipment.DataTextField = "Equipment_Name"
                ddlEquipment.DataValueField = "AutoID"
                ddlEquipment.DataBind()
                ddlEquipment.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            ddlunit.DataSource = ds.Tables(7)
            ddlunit.DataTextField = "Package"
            ddlunit.DataValueField = "CodeID"
            ddlunit.DataBind()
            ddlunit.Items.Insert(0, New ListItem("--Select--", 0))
            ddlSurveyor.DataSource = ds.Tables(8)
            ddlSurveyor.DataTextField = "SurveyorName"
            ddlSurveyor.DataValueField = "SurveyorId"
            ddlSurveyor.DataBind()
            ddlSurveyor.Items.Insert(0, New ListItem("--Select--", 0))

            ddlEquipmentNo.DataSource = ds.Tables(9)
            ddlEquipmentNo.DataTextField = "trailername"
            ddlEquipmentNo.DataValueField = "trailerid"
            ddlEquipmentNo.DataBind()
            ddlEquipmentNo.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btncontainer_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_Insert_Container_Ex_Loading '" & Trim(txtcontainer.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                txtcontainer.Text = ""
                textqt.Text = ""
                ddlsize.SelectedValue = "0"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No already added .');", True)
                Exit Sub
            End If
            If (ds.Tables(1).Rows.Count > 0) Then
                If Val(txtqtydeliv.Text) < (Val(ds.Tables(1).Rows(0)("Qty")) + Val(textqt.Text)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container Quantity should not be greater than delivered Quantity .');", True)
                    textqt.Text = ""
                    textqt.Focus()
                    Exit Sub
                End If
            End If
            strSql = ""
            strSql = "USP_Insert_Temp_Container_Ex_Loading "
            strSql += "'" & Trim(txtcontainer.Text & "") & "','" & Trim(textqt.Text & "") & "','" & Trim(ddlsize.SelectedItem.Text) & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            Container(sender, e)
            txtcontainer.Text = ""
            textqt.Text = ""
            ddlsize.SelectedValue = "0"
            up_grid1.Update()
            'UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Container(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            dt = db.sub_GetDatatable("USP_Select_Temp_Container_Ex_Loading '" & Session("UserId_BondCFS") & "'")
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
            dt = db.sub_GetDatatable("USP_Delete_Container_Ex_Loading '" & AutoID & "','" & Session("UserId_BondCFS") & "'")
            Container(sender, e)
            If (dt.Rows.Count > 0) Then
            End If
            up_grid1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnGodown_Click(sender As Object, e As EventArgs)
        Try
            If txtdelivered.Text > Val(txtrecevied.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Delivered Quantity cannot be greater than received quantity .');", True)
                txtdelivered.Text = ""
                txtdelivered.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql += "select ISNULL(Sum(DeliveredQty),0) from Temp_Godown_Ex_Loading where UniqueID='" & Session("UserId_BondCFS") & "' and IsCancel=0"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If Val(txtrecevied.Text) < Val(dt.Rows(0)(0)) + Val(txtdelivered.Text) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Delivered Quantity cannot be greater than received quantity .');", True)
                    txtdelivered.Text = ""
                    txtdelivered.Focus()
                    Exit Sub
                End If
            End If
            strSql = ""
            strSql += "select * from Temp_Godown_Ex_Loading where iscancel=0 and uniqueid='" & Session("UserId_BondCFS") & "' and LotNo='" & Trim(ddllot.SelectedValue & "") & "'"
            strSql += " and Godown='" & Trim(ddlgodown.SelectedValue & "") & "' and BatchNo='" & Trim(ddlBatch.SelectedItem.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Same Batch No against same lot and godown is not valid');", True)
                Exit Sub
            End If
            strSql = ""
            strSql = " USP_Insert_Temp_Godown_Ex_Loading  "
            strSql += "'" & Trim(ddlgodown.SelectedValue & "") & "','" & Trim(ddllot.SelectedValue & "") & "','" & Trim(txtdelivered.Text) & "','" & Trim(txtrecevied.Text) & "','" & Session("UserId_BondCFS") & "','" & Trim(ddlBatch.SelectedItem.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            Godown(sender, e)
            ddlgodown.SelectedValue = "0"
            ddlgodown_SelectedIndexChanged(sender, e)
            ddllot_TextChanged(sender, e)
            txtdelivered.Text = ""
            txtrecevied.Text = ""
            ddlgodown.Focus()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Godown(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            dt = db.sub_GetDatatable("USP_Select_Temp_Godown_Ex_Loading '" & Session("UserId_BondCFS") & "'")
            grdgodown.DataSource = dt
            grdgodown.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDelete_Click1(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            dt = db.sub_GetDatatable("USP_Delete_Godown_Ex_Loading '" & AutoID & "','" & Session("UserId_BondCFS") & "'")
            Godown(sender, e)
            If (dt.Rows.Count > 0) Then
            End If
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtnocn_TextChanged(sender As Object, e As EventArgs)
        Try
            Control_Clear(sender, e)
            If (Request.QueryString("EntryIDEdit") = "") And (Request.QueryString("EntryIDView") = "") Then
                strSql = ""
                strSql = "USP_Unloading_Sheet_M_No_Changed '" & Trim(txtUnloadingSheetNo.Text & "") & "' "
                dt = db.sub_GetDatatable(strSql)
                If Not dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Unloading No not found');", True)
                    Control_Clear(sender, e)
                    ddlware_SelectedIndexChanged(sender, e)
                    txtUnloadingSheetNo.Text = ""
                    Exit Sub
                End If

                'strSql = ""
                'strSql = "USP_NOC_Fill '" & Trim(txtnocn.Text & "") & "' "
                'dt = db.sub_GetDatatable(strSql)
                'If dt.Rows.Count > 0 Then
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Bond Ex is already done on this NOC No');", True)
                '    Control_Clear(sender, e)
                'txtnocn.Text = ""
                '    Exit Sub
                'End If

                strSql = ""
                strSql = "Usp_Unloading_No_Validation '" & Trim(txtUnloadingSheetNo.Text & "") & "' "
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Unloading Sheet already Loaded');", True)
                    Control_Clear(sender, e)
                    ddlware_SelectedIndexChanged(sender, e)
                    txtUnloadingSheetNo.Text = ""
                    Exit Sub
                End If
            End If

            strSql = ""
            strSql = "USP_Select_Unloading_Dets '" & Trim(txtUnloadingSheetNo.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtboe.Text = Trim(dt.Rows(0)("BE_NO") & "")
                txtdate.Text = Trim(dt.Rows(0)("BE_DATE") & "")
                txtbond.Text = Trim(dt.Rows(0)("BondNo") & "")
                If Trim(dt.Rows(0)("BondDate") & "") <> "" Then
                    txtbonddate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BondDate") & "")).ToString("yyyy-MM-dd")
                End If
                'txtigm.Text = Trim(dt.Rows(0)("ItemNo") & "")load
                'txtDeposit.Text = Trim(dt.Rows(0)("DepositNo") & "")
                ddlCustomer.SelectedValue = Trim(dt.Rows(0)("CUSTOMER") & "")
                ddlimport.SelectedValue = Trim(dt.Rows(0)("CONSIGNEE") & "")
                ddlcha.SelectedValue = Trim(dt.Rows(0)("cha") & "")
                ddlware.SelectedValue = Trim(dt.Rows(0)("WareHousID") & "")
                txtqtyrecei.Text = Trim(dt.Rows(0)("Qty") & "")
                textuit.Text = Trim(dt.Rows(0)("unit") & "")
                txtgross.Text = Trim(dt.Rows(0)("grossWt") & "")
                txtArea.Text = Trim(dt.Rows(0)("AreaOccp") & "")
                txtvalue.Text = Trim(dt.Rows(0)("Value") & "")
                txtduty.Text = Trim(dt.Rows(0)("Duty") & "")
                txtnocn.Text = Trim(dt.Rows(0)("NOC_No") & "")

                Check(sender, e)
                Container(sender, e)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Records Not Found');", True)
                Control_Clear(sender, e)
                ddlware_SelectedIndexChanged(sender, e)
                txtUnloadingSheetNo.Text = ""
                Exit Sub
            End If
            ddlware_SelectedIndexChanged(sender, e)
            UpdatePanel6.Update()
            txtEx.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
   
    Protected Sub Check(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_Loading_sheet_M'" & Trim(txtUnloadingSheetNo.Text & "") & "','" & Trim(txtbond.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                txtbalqty.Text = txtqtyrecei.Text - Val(ds.Tables(0).Rows(0)("Qty"))
            Else
                txtbalqty.Text = txtqtyrecei.Text
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                textbalwt.Text = Format(txtgross.Text - Val(ds.Tables(1).Rows(0)("GrossWt")), "0.0")
            Else
                textbalwt.Text = txtgross.Text
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                textsqm.Text = Format(txtArea.Text - Val(ds.Tables(2).Rows(0)("Area")), "0.0")
            Else
                textsqm.Text = txtArea.Text
            End If

            If (ds.Tables(3).Rows.Count > 0) Then
                txtbalvalue.Text = txtvalue.Text - Val(ds.Tables(3).Rows(0)("Value"))
            Else
                txtbalvalue.Text = txtvalue.Text
            End If

            If (ds.Tables(4).Rows.Count > 0) Then
                txtbalduty.Text = txtduty.Text - Val(ds.Tables(4).Rows(0)("Duty"))
            Else
                txtbalduty.Text = txtduty.Text
            End If
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Control_Clear(sender As Object, e As EventArgs)
        Try

            txtbondt.Text = ""
            txtboe.Text = ""
            txtdate.Text = ""
            txtigm.Text = ""
            txtbond.Text = ""
            txtbonddate.Text = ""
            txtDeposit.Text = ""
            ddlCustomer.SelectedValue = "0"
            ddlimport.SelectedValue = "0"
            ddlcha.SelectedValue = "0"
            txtEx.Text = ""
            txtExBedate.Text = ""
            ddlware.SelectedValue = "0"
            txtNoVehicles.Text = ""
            txtdelivered.Text = ""
            txtqtyrecei.Text = ""
            txtbalqty.Text = ""
            txtqtydeliv.Text = ""
            textuit.Text = ""
            ddlunit.SelectedValue = "0"
            txtgross.Text = ""
            textbalwt.Text = ""
            txtgrosswdeli.Text = ""
            txtArea.Text = ""
            textsqm.Text = ""
            txtvalue.Text = ""
            txtbalvalue.Text = ""
            txtvalu.Text = ""
            txtduty.Text = ""
            txtbalduty.Text = ""
            txtdut.Text = ""
            txtrecevied.Text = ""
            ddlEquipment.SelectedValue = "0"
            ddlSurveyor.SelectedValue = "0"
            txtremarks.Text = ""
            txtExBedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            db.sub_ExecuteNonQuery("Delete from Temp_Container_Ex Where UniqueID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Godown_Ex Where UniqueID=" & Session("UserId_BondCFS") & "")
            Container(sender, e)
            Godown(sender, e)
            ddlware_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtqtydeliv_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtqtydeliv.Text > Val(txtbalqty.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Delivered Quantity cannot be grater than balance quantity .');", True)
                txtqtydeliv.Text = ""
                txtqtydeliv.Focus()
                Exit Sub
            End If

            txtgrosswdeli.Text = Format((Val(txtgross.Text / txtqtyrecei.Text) * Val(txtqtydeliv.Text)), "0.0")
            textrelea.Text = Format(Val(txtArea.Text / txtqtyrecei.Text) * Val(txtqtydeliv.Text), "0.0")
            'textbalwt.Text = Val(textbalwt.Text) - Val(txtgrosswdeli.Text)
            'txtbalqty.Text = Val(txtbalqty.Text) - Val(txtqtydeliv.Text)
            If txtbalqty.Text <> 0 Then
                txtvalu.Text = Math.Round(Val(Val(txtbalvalue.Text) * Val(txtqtydeliv.Text)) / Val(txtbalqty.Text))
                txtdut.Text = Math.Round(Val(Val(txtbalduty.Text) * Val(txtqtydeliv.Text)) / Val(txtbalqty.Text))

            End If
            ddlunit.Focus()
            UpdatePanel3.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub textrelea_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim dblExArea As Double = 0
            strSql = ""
            strSql = "USP_NOC'" & Trim(txtnocn.Text & "") & "','" & Trim(txtbond.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            'If (ds.Tables(2).Rows.Count > 0) Then
            '    textsqm.Text = txtArea.Text - Val(ds.Tables(2).Rows(0)("Area"))
            'Else
            '    textsqm.Text = txtArea.Text
            'End If
            dblExArea = Val(ds.Tables(2).Rows(0)("Area"))
            dblExArea = dblExArea + Val(textrelea.Text)
            If dblExArea > Val(txtArea.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Area released cannot be greater than balance area .');", True)
                textrelea.Text = ""
                textrelea.Focus()
                Exit Sub
            End If
            'textsqm.Text = Val(textsqm.Text) - Val(textrelea.Text)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Dim dblCount As Double = 0
            For Each row In grdgodown.Rows
                dblCount += 1
                Exit For
            Next
            'If dblCount = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Fill Godown Details for Loading');", True)
            '    ddlgodown.Focus()
            '    Exit Sub
            'End If
            Dim strWorkyear As String = ""
            Dim InvoiceDate As Date = Trim(txttoDate.Text)
            If InvoiceDate.Month < 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") - 1 & "-" & Format(InvoiceDate, "yy")
            ElseIf InvoiceDate.Month >= 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") & "-" & Format(InvoiceDate, "yy") + 1
            End If

            If (Button2.Text = "Modify") Then
                strSql = ""
                strSql += "USP_Update_Loading_Sheet_M '" & Trim(txtentry.Text & "") & "','" & Trim(txtEx.Text & "") & "','" & Convert.ToDateTime(Trim(txtExBedate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtqtydeliv.Text & "") & "','" & Trim(ddlunit.SelectedValue & "") & "',"
                strSql += "'" & Trim(txtgrosswdeli.Text & "") & "','" & Trim(txtvalu.Text & "") & "','" & Trim(txtdut.Text & "") & "',"
                strSql += "'" & Trim(ddlEquipment.SelectedValue & "") & "','" & Trim(ddlSurveyor.SelectedValue & "") & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Session("UserId_BondCFS") & "'," & ddlEquipmentNo.SelectedValue & ",'" & Trim(txtUnloadingSheetNo.Text & "") & "','" & Trim(txtnocn.Text & "") & "','" & Trim(txtNoVehicles.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                Container_In(sender, e)
                Godown_in(sender, e)

                lblSession.Text = "Record modified for Loading Sheet No " & txtentry.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel5.Update()
            Else
                strSql = ""
                strSql += "USP_Insert_LOADING_SHEET_M '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtbond.Text & "") & "','" & Trim(txtEx.Text & "") & "','" & Convert.ToDateTime(Trim(txtExBedate.Text & "")).ToString("yyyy-MM-dd") & "',"
                strSql += "'" & Trim(txtqtydeliv.Text & "") & "','" & Trim(ddlunit.SelectedValue & "") & "','" & Trim(textrelea.Text & "") & "','" & Trim(textsqm.Text) & "','" & Trim(txtgrosswdeli.Text & "") & "',"
                strSql += "'" & Trim(txtboe.Text & "") & "','" & Trim(txtduty.Text & "") & "','" & Trim(txtvalue.Text & "") & "','" & Trim(txtvalu.Text & "") & "','" & Trim(txtdut.Text & "") & "',"
                strSql += "'" & Trim(ddlimport.SelectedValue & "") & "','" & Trim(ddlcha.SelectedValue & "") & "','" & Trim(lblA.Text & "") & "','" & Trim(lblB.Text & "") & "','" & Trim(lblC.Text & "") & "',"
                strSql += "'" & Trim(ddlEquipment.SelectedValue & "") & "','" & Trim(ddlSurveyor.SelectedValue & "") & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & strWorkyear & "','" & Session("UserId_BondCFS") & "'," & ddlEquipmentNo.SelectedValue & ",'" & Trim(txtUnloadingSheetNo.Text & "") & "','" & Trim(txtnocn.Text & "") & "','" & Trim(txtNoVehicles.Text & "") & "','" & Convert.ToDateTime(Trim(txtbonddate.Text & "")).ToString("yyyy-MM-dd") & "'"
                dt = db.sub_GetDatatable(strSql)
                txtentry.Text = dt.Rows(0)("EntryID")
                Container_In(sender, e)
                Godown_in(sender, e)

                lblSession.Text = "Record saved successfully for Loading Sheet No " & txtentry.Text & ""
                Control_Clear(sender, e)
                txtUnloadingSheetNo.Text = ""

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel5.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Godown_in(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_Loading_Sheet_G '" & txtentry.Text & "'"
            db.sub_ExecuteNonQuery(strSql)
            db.sub_ExecuteNonQuery("USP_INSERT_Loading_Sheet_G '" & txtbond.Text & "','" & Trim(txtentry.Text & "") & "','" & Session("UserId_BondCFS") & "'")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Container_In(sender As Object, e As EventArgs)
        Try
            Dim strWorkyear As String = ""
            Dim InvoiceDate As Date = Trim(txttoDate.Text)
            If InvoiceDate.Month < 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") - 1 & "-" & Format(InvoiceDate, "yy")
            ElseIf InvoiceDate.Month >= 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") & "-" & Format(InvoiceDate, "yy") + 1
            End If
            strSql = ""
            strSql = "USP_Loading_sheet_c '" & txtentry.Text & "'"
            db.sub_ExecuteNonQuery(strSql)
            For Each row In grdcontainer.Rows
                If CType(row.FindControl("chkCheck"), CheckBox).Checked = True Then
                    db.sub_ExecuteNonQuery("USP_Insert_Loading_Sheet_C '" & txtentry.Text & "','" & Trim(txtbond.Text & "") & "','" & strWorkyear & "','" & Session("UserId_BondCFS") & "','" & Trim(CType(row.FindControl("lblCNo"), Label).Text) & "'," & Val(CType(row.FindControl("lblCSize"), Label).Text) & "," & Val(CType(row.FindControl("txtLoadingQty"), TextBox).Text) & "")
                End If
            Next
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Bond_Ex(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = " USP_Select_bondEx '" & Trim(txtnocn.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtnocn.Text = Trim(dt.Rows(0)("NOCno") & "")
                'txtNocDate.Text = Trim(dt.Rows(0)("Date") & "")
                txtbondt.Text = Trim(dt.Rows(0)("Bondtype") & "")
                txtboe.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtdate.Text = Trim(dt.Rows(0)("BOE") & "")
                txtbond.Text = Trim(dt.Rows(0)("BondNo") & "")
                If Not Trim(dt.Rows(0)("Bond") & "") = "" Then
                    txtbonddate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Bond") & "")).ToString("yyyy-MM-dd")
                End If
                txtigm.Text = Trim(dt.Rows(0)("ItemNo") & "")
                txtDeposit.Text = Trim(dt.Rows(0)("DepositNo") & "")
                ddlCustomer.SelectedValue = Trim(dt.Rows(0)("CustID") & "")
                ddlimport.SelectedValue = Trim(dt.Rows(0)("ImporterId") & "")
                ddlcha.SelectedValue = Trim(dt.Rows(0)("CHAID") & "")
                ddlware.SelectedValue = Trim(dt.Rows(0)("WareHousID") & "")
                txtqtyrecei.Text = Trim(dt.Rows(0)("Qty") & "")
                textuit.Text = Trim(dt.Rows(0)("unit") & "")
                txtgross.Text = Trim(dt.Rows(0)("grossWt") & "")
                txtArea.Text = Trim(dt.Rows(0)("AreaOccp") & "")
                txtvalue.Text = Trim(dt.Rows(0)("Value") & "")
                txtduty.Text = Trim(dt.Rows(0)("Duty") & "")
                Check(sender, e)
                Container(sender, e)
            End If
            UpdatePanel6.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtvalu_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtvalu.Text > Val(txtbalvalue.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Value cannot be greater than balance value.');", True)
                txtvalu.Text = ""
                txtvalu.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try

    End Sub

    Protected Sub txtdut_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtdut.Text > Val(txtbalduty.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Duty cannot be greater than balance duty.');", True)
                txtdut.Text = ""
                txtdut.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtgrosswdeli_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtgrosswdeli.Text > Val(textbalwt.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Gross weight is greater than balance gross weight.');", True)
                txtgrosswdeli.Text = ""
                txtgrosswdeli.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            Control_Clear(sender, e)
            strSql = ""
            strSql += "select * from Temp_Loading_Entry where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtUnloadingSheetNo.Text = Trim(dt.Rows(0)("UnloadingNo") & "")
                Call txtnocn_TextChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlware_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlware.SelectedIndexChanged
        Try
            strSql = ""
            strSql += "select GM.EntryID,GM.GodownCode from Unloading_Sheet_G USG INNER JOIN GodownM GM ON USG.GodownNo=GM.EntryID where DepositNo='" & Trim(txtUnloadingSheetNo.Text & "") & "' AND GM.Warehousecode='" & ddlware.SelectedItem.Text & "'"
            dt = db.sub_GetDatatable(strSql)

            ddlgodown.DataSource = dt
            ddlgodown.DataTextField = "GodownCode"
            ddlgodown.DataValueField = "entryid"
            ddlgodown.DataBind()
            ddlgodown.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlgodown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlgodown.SelectedIndexChanged
        Try
            strSql = ""
            strSql += "select LM.LotNo,LM.entryid from Unloading_Sheet_G USG INNER JOIN LotM LM ON USG.LOTNO=LM.EntryID where DepositNo='" & Trim(txtUnloadingSheetNo.Text & "") & "' and GodownCode='" & ddlgodown.SelectedValue & "' and Warehousecode='" & ddlware.SelectedItem.Text & "'"
            dt = db.sub_GetDatatable(strSql)

            ddllot.DataSource = dt
            ddllot.DataTextField = "LotNo"
            ddllot.DataValueField = "entryid"
            ddllot.DataBind()

            ddllot.Items.Insert(0, New ListItem("--Select--", 0))
            strSql = ""
            strSql = "USP_Received_Qty_From_Batch '" & Trim(txtUnloadingSheetNo.Text & "") & "','" & Trim(ddlgodown.SelectedValue & "") & "','" & Trim(ddllot.SelectedValue & "") & "','" & Trim(ddlBatch.SelectedItem.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtrecevied.Text = Trim(dt.Rows(0)("Qty") & "")
                txtdelivered.Focus()
            End If
            ddlgodown.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddllot_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select distinct BatchNo from Unloading_Sheet_G Where DepositNo ='" & Trim(txtUnloadingSheetNo.Text & "") & "' and LotNo ='" & ddllot.SelectedValue & "' and GodownNo ='" & ddlgodown.SelectedValue & "'"
            dt = db.sub_GetDatatable(strSql)

            ddlBatch.DataSource = dt
            ddlBatch.DataTextField = "BatchNo"
            ddlBatch.DataValueField = "BatchNo"
            ddlBatch.DataBind()

            ddlBatch.Items.Insert(0, New ListItem("--Select--", 0))
            ddllot.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlBatch_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_Received_Qty_From_Batch '" & Trim(txtUnloadingSheetNo.Text & "") & "','" & Trim(ddlgodown.SelectedValue & "") & "','" & Trim(ddllot.SelectedValue & "") & "','" & Trim(ddlBatch.SelectedItem.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtrecevied.Text = Trim(dt.Rows(0)("Qty") & "")
                txtdelivered.Focus()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub SaveOk_ServerClick(sender As Object, e As EventArgs)
        Try
            lblPrintQue.Text = "Do you wish to print?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtLoadingQty_TextChanged(sender As Object, e As EventArgs)
        Try
            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += "update Temp_Container_Ex_Loading set Qty=" & Val(CType(row.FindControl("txtLoadingQty"), TextBox).Text) & " where AutoIdTemp=" & Val(CType(row.FindControl("lblAutoIdTemp"), Label).Text) & ""
                db.sub_ExecuteNonQuery(strSql)
            Next
            Container(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
