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
    Dim bondno, bondnoEdit As String
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
            db.sub_ExecuteNonQuery("Delete from Temp_Container_IN_Unloading Where UniqueID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Godown_Unloading Where UniqueID=" & Session("UserId_BondCFS") & "")

            textbonddate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtbondindate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtbedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            'txtExpiryDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
            Filldropdown()
            Container(sender, e)
            Godown(sender, e)

            If Not (Request.QueryString("DepositNoEdit") = "") Then
                bondno = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("DepositNoEdit")))

                strSql = ""
                strSql = "USP_Edit_BondIN '" & bondno & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                'Container(sender, e)
                Godown(sender, e)

                If (dt.Rows.Count > 0) Then
                    txtbondno.Text = Trim(dt.Rows(0)("DepositNo") & "")
                    txtnocno.Text = Trim(dt.Rows(0)("NOCNo") & "")
                    txtbondin.Text = Trim(dt.Rows(0)("BondNo") & "")
                    textbonddate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Bond") & "")).ToString("yyyy-MM-dd")
                    txtbondindate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BondInDate") & "")).ToString("yyyy-MM-dd")
                    txtqy.Text = Trim(dt.Rows(0)("Qty") & "")
                    ddlunit.SelectedValue = Trim(dt.Rows(0)("Unit") & "")
                    txtStoragearea.Text = Trim(dt.Rows(0)("AreaOccp") & "")
                    txtlocation.Text = Trim(dt.Rows(0)("Location") & "")
                    txtserial.Text = Trim(dt.Rows(0)("SerialNo") & "")
                    ddlstatus.SelectedValue = Trim(dt.Rows(0)("Status") & "")
                    txtkgs.Text = Trim(dt.Rows(0)("Grosswt") & "")
                    txtreg.Text = Trim(dt.Rows(0)("RegNo") & "")
                    ddlhous.SelectedValue = Trim(dt.Rows(0)("WareHousID") & "")
                    ddlEquipment.SelectedValue = Trim(dt.Rows(0)("EquipmentType") & "")
                    ddlSurveyor.SelectedValue = Trim(dt.Rows(0)("SurveyorName") & "")
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    'txtbondexpdate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BondExpDate") & "")).ToString("yyyy-MM-dd")
                    ddlSection.SelectedValue = Val(dt.Rows(0)("Section") & "")
                    txtPONo.Text = Trim(dt.Rows(0)("PO_NO") & "")
                    ddlEquipmentNo.SelectedValue = Trim(dt.Rows(0)("EQUIPMENT_NO") & "")

                    Bond_IN(sender, e)
                End If
                Godown(sender, e)
                Panel2.Enabled = True
                Panel3.Enabled = True
                txtnocno.ReadOnly = True
                btnsave.Text = "Modify"
            End If

            If Not (Request.QueryString("DepositNoView") = "") Then
                bondno = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("DepositNoView")))
                strSql = ""
                strSql = "USP_Edit_BondIN '" & bondno & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                'Container(sender, e)

                If (dt.Rows.Count > 0) Then
                    txtbondno.Text = Trim(dt.Rows(0)("DepositNo") & "")
                    txtnocno.Text = Trim(dt.Rows(0)("NOCNo") & "")
                    txtbondin.Text = Trim(dt.Rows(0)("BondNo") & "")
                    textbonddate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Bond") & "")).ToString("yyyy-MM-dd")
                    txtbondindate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BondInDate") & "")).ToString("yyyy-MM-dd")
                    txtqy.Text = Trim(dt.Rows(0)("Qty") & "")
                    ddlunit.SelectedValue = Trim(dt.Rows(0)("Unit") & "")
                    txtStoragearea.Text = Trim(dt.Rows(0)("AreaOccp") & "")
                    txtlocation.Text = Trim(dt.Rows(0)("Location") & "")
                    txtserial.Text = Trim(dt.Rows(0)("SerialNo") & "")
                    ddlstatus.SelectedValue = Trim(dt.Rows(0)("Status") & "")
                    txtkgs.Text = Trim(dt.Rows(0)("Grosswt") & "")
                    txtreg.Text = Trim(dt.Rows(0)("RegNo") & "")
                    ddlhous.SelectedValue = Trim(dt.Rows(0)("WareHousID") & "")
                    ddlEquipment.SelectedValue = Trim(dt.Rows(0)("EquipmentType") & "")
                    ddlSurveyor.SelectedValue = Trim(dt.Rows(0)("SurveyorName") & "")
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    'txtbondexpdate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BondExpDate") & "")).ToString("yyyy-MM-dd")
                    ddlSection.SelectedValue = Val(dt.Rows(0)("Section") & "")
                    txtPONo.Text = Trim(dt.Rows(0)("PO_NO") & "")
                    ddlEquipmentNo.SelectedValue = Trim(dt.Rows(0)("EQUIPMENT_NO") & "")
                    Bond_IN(sender, e)
                End If
                Godown(sender, e)
                Panel4.Enabled = False
                Panel2.Enabled = False
                Panel3.Enabled = False
                Panel1.Enabled = False
                txtnocno.ReadOnly = True

                btnSave.Text = "View"
                btnsave.Visible = False
                btnclear.Visible = False
            End If
            ddlhous_SelectedIndexChanged(sender, e)
            txtbe.Focus()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Get_Unloading_sheet_no")
            If dt.Rows.Count > 0 Then
                rptNOLIst.DataSource = dt
                rptNOLIst.DataBind()
            End If

            ds = db.sub_GetDataSets("USP_Fill_IN")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlhous.DataSource = ds.Tables(0)
                ddlhous.DataTextField = "WHName"
                ddlhous.DataValueField = "WHID"
                ddlhous.DataBind()
                ddlhous.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            'If (ds.Tables(1).Rows.Count > 0) Then
            '    ddlgodown.DataSource = ds.Tables(1)
            '    ddlgodown.DataTextField = "GodownCode"
            '    ddlgodown.DataValueField = "entryid"
            '    ddlgodown.DataBind()
            '    ddlgodown.Items.Insert(0, New ListItem("--Select--", 0))
            'End If

            'If (ds.Tables(2).Rows.Count > 0) Then
            '    ddllot.DataSource = ds.Tables(2)
            '    ddllot.DataTextField = "LotNo"
            '    ddllot.DataValueField = "entryid"
            '    ddllot.DataBind()
            '    ddllot.Items.Insert(0, New ListItem("--Select--", 0))
            'End If

            If (ds.Tables(3).Rows.Count > 0) Then
                ddlEquipment.DataSource = ds.Tables(3)
                ddlEquipment.DataTextField = "Equipment_Name"
                ddlEquipment.DataValueField = "AutoID"
                ddlEquipment.DataBind()
                ddlEquipment.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            ddlunit.DataSource = ds.Tables(4)
            ddlunit.DataTextField = "Package"
            ddlunit.DataValueField = "CodeID"
            ddlunit.DataBind()
            ddlunit.Items.Insert(0, New ListItem("--Select--", 0))
            ddlSurveyor.DataSource = ds.Tables(5)
            ddlSurveyor.DataTextField = "SurveyorName"
            ddlSurveyor.DataValueField = "SurveyorId"
            ddlSurveyor.DataBind()
            ddlSurveyor.Items.Insert(0, New ListItem("--Select--", 0))
            ddlSection.DataSource = ds.Tables(6)
            ddlSection.DataTextField = "SectionName"
            ddlSection.DataValueField = "ID"
            ddlSection.DataBind()
            ddlSection.Items.Insert(0, New ListItem("--Select--", 0))

            ddlEquipmentNo.DataSource = ds.Tables(7)
            ddlEquipmentNo.DataTextField = "trailername"
            ddlEquipmentNo.DataValueField = "trailerid"
            ddlEquipmentNo.DataBind()
            ddlEquipmentNo.Items.Insert(0, New ListItem("--Select--", 0))

            ds = db.sub_GetDataSets("USP_Fill_Noc_list")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlCustomer.DataSource = ds.Tables(0)
                ddlCustomer.DataTextField = "agentName"
                ddlCustomer.DataValueField = "agentID"
                ddlCustomer.DataBind()
                ddlCustomer.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If (ds.Tables(1).Rows.Count > 0) Then
                ddlConsignee.DataSource = ds.Tables(1)
                ddlConsignee.DataTextField = "ImporterName"
                ddlConsignee.DataValueField = "ImporterID"
                ddlConsignee.DataBind()
                ddlConsignee.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If (ds.Tables(2).Rows.Count > 0) Then
                ddlCHA.DataSource = ds.Tables(2)
                ddlCHA.DataTextField = "CHAName"
                ddlCHA.DataValueField = "CHAID"
                ddlCHA.DataBind()
                ddlCHA.Items.Insert(0, New ListItem("--Select--", 0))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            dt = db.sub_GetDatatable("USP_Delete_Container_IN_Unloading '" & AutoID & "','" & Session("UserId_BondCFS") & "'")
            Container(sender, e)
            If (dt.Rows.Count > 0) Then
            End If
            up_grid1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_Insert_Container_IN_Unloading '" & Trim(txtcontainer.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                txtcontainer.Text = ""
                textqt.Text = ""
                ddlsize.SelectedValue = "0"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No already added .');", True)
                Exit Sub
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                If Val(txtqy.Text) < (Val(ds.Tables(1).Rows(0)("Qty")) + Val(textqt.Text)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container qty should not be greater than Unloading Qty');", True)
                    textqt.Text = ""
                    textqt.Focus()
                    Exit Sub
                End If
            End If
            strSql = ""
            strSql = "USP_Insert_Temp_IN_Unloading "
            strSql += "'" & Trim(txtcontainer.Text & "") & "','" & Trim(textqt.Text & "") & "','" & Trim(ddlsize.SelectedItem.Text) & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            Container(sender, e)
            txtcontainer.Text = ""
            textqt.Text = ""
            ddlsize.SelectedValue = "0"
            up_grid1.Update()
            Button1.Text = "Add"
            Button1.Attributes.Add("Class", "btn btn-primary")
        Catch ex As Exception
            Button1.Text = "Add"
            Button1.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Container(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            dt = db.sub_GetDatatable("USP_Select_Temp_Container_IN_Unloading '" & Session("UserId_BondCFS") & "'")
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
    Protected Sub Button2_Click(sender As Object, e As EventArgs) 'GoDown add button
        Try
            strSql = ""
            strSql += "Select isnull(sum(Qty),0) as Qty from temp_godown_unloading where iscancel=0 and uniqueid='" & Session("UserId_BondCFS") & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                If Val(txtqy.Text) < (Val(dt1.Rows(0)("Qty")) + Val(textty.Text)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Godown Qty should not be greater than NOC Qty');", True)
                    textty.Text = ""
                    textty.Focus()
                    Exit Sub
                End If
            End If
            strSql = ""
            strSql += "select * from temp_godown_unloading where iscancel=0 and uniqueid='" & Session("UserId_BondCFS") & "' and LotNo='" & Trim(ddllot.SelectedValue & "") & "'"
            strSql += " and Godown='" & Trim(ddlgodown.SelectedValue & "") & "' and BatchNo='" & Trim(txtBatchNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Same Batch No against same lot and godown is not valid');", True)
                Exit Sub
            End If
            strSql = ""
            strSql = "USP_Insert_Temp_Godown_Unloading "
            strSql += "'" & Trim(ddlgodown.SelectedValue & "") & "','" & Trim(ddllot.SelectedValue & "") & "','" & Trim(textty.Text) & "','" & Session("UserId_BondCFS") & "',"
            strSql += "'" & Trim(txtBatchNo.Text & "") & "','" & Trim(txtWeight.Text & "") & "','" & Replace(Trim(txtRemarksGodown.Text & ""), "'", "''") & "','" & Replace(Trim(txtDescription.Text & ""), "'", "''") & "'"
            dt = db.sub_GetDatatable(strSql)
            Godown(sender, e)
            ddlgodown.SelectedValue = "0"
            ddllot.SelectedValue = "0"
            textty.Text = ""
            txtBatchNo.Text = ""
            txtWeight.Text = ""
            txtRemarksGodown.Text = ""
            txtDescription.Text = ""
            up_grid.Update()
            Button2.Text = "Add"
            Button2.Attributes.Add("Class", "btn btn-primary")
        Catch ex As Exception
            Button2.Text = "Add"
            Button2.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Godown(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            dt = db.sub_GetDatatable("USP_Select_Temp_Godown_Unloading '" & Session("UserId_BondCFS") & "'")
            grdgodown.DataSource = dt
            grdgodown.DataBind()
            
            up_grid.Update()
            If dt.Rows.Count > 0 Then
                pnlWarehouse.Enabled = False
            Else
                pnlWarehouse.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkDelete_Click1(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            dt = db.sub_GetDatatable("USP_Delete_Godown_Unloading '" & AutoID & "','" & Session("UserId_BondCFS") & "'")
            Godown(sender, e)
            If (dt.Rows.Count > 0) Then
            End If
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsave_Click(sender As Object, e As EventArgs)
        Try
            If (btnsave.Text = "Modify") Then
                strSql = ""
                strSql += "USP_Update_UNLOADING_SHEET_M '" & Trim(txtbondno.Text & "") & "','" & Trim(txtbe.Text & "") & "','" & Convert.ToDateTime(Trim(txtbedate.Text & "")).ToString("yyyy-MM-dd") & "',"
                strSql += "'" & Trim(Txtigm.Text & "") & "','" & Trim(txtitm.Text & "") & "','" & Trim(txtweek.Text & "") & "','" & Trim(txtday.Text & "") & "','" & Convert.ToDateTime(Trim(txtExpiryDate.Text & "")).ToString("yyyy-MM-dd") & "'," & Val(ddlConsignee.SelectedValue & "") & "," & Val(ddlCHA.SelectedValue & "") & "," & Val(ddlCustomer.SelectedValue & "") & ","
                strSql += "'" & Replace(Trim(txtcommodity.Text & ""), "'", "''") & "','" & Trim(txtvalue.Text & "") & "','" & Trim(txtduty.Text & "") & "'," & Val(ddlSection.SelectedValue & "") & ","
                strSql += "'" & Trim(txtbondin.Text & "") & "','" & Convert.ToDateTime(Trim(textbonddate.Text & "")).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(txtbondexpdate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtqy.Text & "") & "',"
                strSql += "'" & Trim(ddlunit.SelectedValue & "") & "','" & Trim(txtStoragearea.Text & "") & "','" & Trim(txtkgs.Text & "") & "','" & Trim(ddlstatus.SelectedValue & "") & "','" & Trim(txtserial.Text & "") & "','" & Trim(txtlocation.Text & "") & "','" & Trim(txtreg.Text & "") & "','" & Trim(txtPONo.Text & "") & "',"
                strSql += "'" & Trim(ddlhous.SelectedValue & "") & "','" & Trim(ddlEquipment.SelectedValue & "") & "','" & Trim(ddlSurveyor.SelectedValue & "") & "','" & Trim(ddlEquipmentNo.SelectedValue & "") & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Session("UserId_BondCFS") & "'"
                strSql += "," & Val(lblA.Text & "") & "," & Val(lblB.Text) & "," & Val(lblC.Text & "") & ",'" & Trim(txtNOCNo.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                Container_In(sender, e)
                Godown_in(sender, e)
                lblSession.Text = "Record modified successfully for Sheet NO " & txtbondno.Text & ""
                Control_Clear(sender, e)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel5.Update()
            Else
                strSql = ""
                strSql += "USP_Insert_UNLOADING_SHEET_M '" & Convert.ToDateTime(Trim(txtbondindate.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "','" & Trim(txtbe.Text & "") & "','" & Convert.ToDateTime(Trim(txtbedate.Text & "")).ToString("yyyy-MM-dd") & "',"
                strSql += "'" & Trim(Txtigm.Text & "") & "','" & Trim(txtitm.Text & "") & "','" & Trim(txtweek.Text & "") & "','" & Trim(txtday.Text & "") & "','" & Convert.ToDateTime(Trim(txtExpiryDate.Text & "")).ToString("yyyy-MM-dd") & "'," & Val(ddlConsignee.SelectedValue & "") & "," & Val(ddlCHA.SelectedValue & "") & "," & Val(ddlCustomer.SelectedValue & "") & ","
                strSql += "'" & Replace(Trim(txtcommodity.Text & ""), "'", "''") & "','" & Trim(txtvalue.Text & "") & "','" & Trim(txtduty.Text & "") & "'," & Val(ddlSection.SelectedValue & "") & ","
                strSql += "'" & Trim(txtbondin.Text & "") & "','" & Convert.ToDateTime(Trim(textbonddate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtbondexpdate.Text & "") & "','" & Trim(txtqy.Text & "") & "',"
                strSql += "'" & Trim(ddlunit.SelectedValue & "") & "','" & Trim(txtStoragearea.Text & "") & "','" & Trim(txtkgs.Text & "") & "','" & Trim(ddlstatus.SelectedValue & "") & "','" & Trim(txtserial.Text & "") & "','" & Trim(txtlocation.Text & "") & "','" & Trim(txtreg.Text & "") & "','" & Trim(txtPONo.Text & "") & "',"
                strSql += "'" & Trim(ddlhous.SelectedValue & "") & "','" & Trim(ddlEquipment.SelectedValue & "") & "','" & Trim(ddlSurveyor.SelectedValue & "") & "','" & Trim(ddlEquipmentNo.SelectedValue & "") & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Session("UserId_BondCFS") & "'"
                strSql += "," & Val(lblA.Text & "") & "," & Val(lblB.Text) & "," & Val(lblC.Text & "") & ",'" & Trim(txtNOCNo.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                txtbondno.Text = dt.Rows(0)("DepositNo")
                Container_In(sender, e)
                Godown_in(sender, e)

                lblSession.Text = "Record saved successfully for Sheet NO " & dt.Rows(0)("DepositNo") & ""
                Control_Clear(sender, e)
                txtNOCNo.Text = ""
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
            strSql = "USE_UNLOADING_SHEET_G '" & txtbondno.Text & "'"
            db.sub_ExecuteNonQuery(strSql)
            db.sub_ExecuteNonQuery("USP_INSERT_UNLOADING_SHEET_G '" & txtbondno.Text & "','" & Trim(txtnocno.Text & "") & "','" & Trim(txtbondin.Text & "") & "','" & Session("UserId_BondCFS") & "'")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Container_In(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_UNLOADING_SHEET_C '" & txtbondno.Text & "'"
            db.sub_ExecuteNonQuery(strSql)
            db.sub_ExecuteNonQuery("USP_INSERT_UNLOADING_SHEET_C '" & txtbondno.Text & "','" & Trim(txtnocno.Text & "") & "','" & Trim(txtbondin.Text & "") & "','" & Trim(textbond.Text & "") & "','" & Session("UserId_BondCFS") & "'")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtnocno_TextChanged(sender As Object, e As EventArgs)
        Try
            Control_Clear(sender, e)
            If (Request.QueryString("DepositNoEdit") = "") And (Request.QueryString("DepositNoView") = "") Then
                strSql = ""
                strSql = "USP_NOC_NO_VALIDATION_UNLOADING '" & Trim(txtNOCNo.Text & "") & "' "
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Unloading already done for this NOC No');", True)
                    Control_Clear(sender, e)
                    ddlhous_SelectedIndexChanged(sender, e)
                    txtnocno.Text = ""
                    txtnocno.Focus()
                    Exit Sub
                End If
            End If
            strSql = ""
            strSql = "USP_Select_NOC_NO_UNLOADING '" & Trim(txtNOCNo.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                'txtnocno.Text = Trim(dt.Rows(0)("NOCno") & "")
                'txtNocDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Noc") & "")).ToString("dd-MM-yyyy")
                'textbond.Text = Trim(dt.Rows(0)("Bondtype") & "")
                txtbe.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtbedate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BOE") & "")).ToString("yyyy-MM-dd")
                Txtigm.Text = Trim(dt.Rows(0)("IGMNo") & "")
                txtitm.Text = Trim(dt.Rows(0)("ItemNo") & "")
                txtweek.Text = Trim(dt.Rows(0)("Weeks") & "")
                txtday.Text = Trim(dt.Rows(0)("Days") & "")
                txtExpiryDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Expiry") & "")).ToString("yyyy-MM-dd")
                ddlConsignee.SelectedValue = Trim(dt.Rows(0)("ImporterName") & "")
                ddlCHA.SelectedValue = Trim(dt.Rows(0)("chaName") & "")
                ddlCustomer.SelectedValue = Trim(dt.Rows(0)("Customer") & "")
                txtcommodity.Text = Trim(dt.Rows(0)("Commodity") & "")
                txtqy.Text = Trim(dt.Rows(0)("Qty") & "")
                ddlunit.SelectedValue = Trim(dt.Rows(0)("unit") & "")
                txtkgs.Text = Trim(dt.Rows(0)("Grosswt") & "")
                txtStoragearea.Text = Trim(dt.Rows(0)("StorageSpace") & "")
                txtvalue.Text = Trim(dt.Rows(0)("Value") & "")
                txtduty.Text = Trim(dt.Rows(0)("Duty") & "")
                ddlhous.SelectedValue = Val(dt.Rows(0)("WHID") & "")
                Container(sender, e)
                ddlhous_SelectedIndexChanged(sender, e)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' NOC No not found');", True)
                Control_Clear(sender, e)
                ddlhous_SelectedIndexChanged(sender, e)
                txtnocno.Text = ""
                Exit Sub
            End If

            ddlSection.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Bond_IN(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_BOND_IN '" & Trim(txtnocno.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtnocno.Text = Trim(dt.Rows(0)("NOCno") & "")
                txtNocDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Noc") & "")).ToString("dd-MM-yyyy")
                textbond.Text = Trim(dt.Rows(0)("Bondtype") & "")
                txtbe.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtbedate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BOE") & "")).ToString("dd-MM-yyyy")
                Txtigm.Text = Trim(dt.Rows(0)("IGMNo") & "")
                txtitm.Text = Trim(dt.Rows(0)("ItemNo") & "")
                txtweek.Text = Trim(dt.Rows(0)("Weeks") & "")
                txtday.Text = Trim(dt.Rows(0)("Days") & "")
                txtExpiryDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Expiry") & "")).ToString("dd-MM-yyyy")
                'txtConsi.Text = Trim(dt.Rows(0)("ImporterName") & "")
                'txtCha.Text = Trim(dt.Rows(0)("chaName") & "")
                'txtCustomer.Text = Trim(dt.Rows(0)("Customer") & "")
                txtcommodity.Text = Trim(dt.Rows(0)("Commodity") & "")
                txtqty.Text = Trim(dt.Rows(0)("Qty") & "")
                txtunit.Text = Trim(dt.Rows(0)("unit") & "")
                txtgrosss.Text = Trim(dt.Rows(0)("Grosswt") & "")
                txtstorage.Text = Trim(dt.Rows(0)("StorageSpace") & "")
                txtvalue.Text = Trim(dt.Rows(0)("Value") & "")
                txtduty.Text = Trim(dt.Rows(0)("Duty") & "")
                Container(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Control_Clear(sender As Object, e As EventArgs)
        Try
            ddlEquipmentNo.SelectedValue = 0
            textbond.Text = ""
            txtbe.Text = ""
            txtbedate.Text = ""
            Txtigm.Text = ""
            txtitm.Text = ""
            txtweek.Text = ""
            txtday.Text = ""
            txtExpiryDate.Text = ""
            ddlConsignee.SelectedValue = 0
            ddlCHA.SelectedValue = 0
            ddlCustomer.SelectedValue = 0
            txtcommodity.Text = ""
            txtqty.Text = ""
            txtunit.Text = ""
            txtgrosss.Text = ""
            txtstorage.Text = ""
            txtvalue.Text = ""
            txtduty.Text = ""
            txtbondin.Text = ""
            textbonddate.Text = ""
            txtqy.Text = ""
            ddlunit.SelectedValue = "0"
            txtStoragearea.Text = ""
            txtlocation.Text = ""
            txtserial.Text = ""
            ddlstatus.SelectedValue = "0"
            txtkgs.Text = ""
            txtreg.Text = ""
            ddlhous.SelectedValue = "0"
            ddlEquipment.SelectedValue = "0"
            ddlSurveyor.SelectedValue = "0"
            txtremarks.Text = ""
            txtbondexpdate.Text = ""
            textbonddate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            'textbonddate_TextChanged(sender, e)
            db.sub_ExecuteNonQuery("Delete from Temp_Container_IN_Unloading Where UniqueID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Godown_Unloading Where UniqueID=" & Session("UserId_BondCFS") & "")
            Container(sender, e)
            Godown(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function

    Protected Sub textbonddate_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim strbondexpdate As String = ""
            strbondexpdate = Convert.ToDateTime(DateAdd("yyyy", 1, textbonddate.Text)).ToString("yyyy-MM-dd")
            txtbondexpdate.Text = Convert.ToDateTime(DateAdd("d", -1, strbondexpdate)).ToString("yyyy-MM-dd")
            'txtqy.Focus()
            UpdatePanel4.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    'Protected Sub textbonddate_lostfocus(sender As Object, e As EventArgs)
    '    Try
    '        Dim strbondexpdate As String = ""
    '        strbondexpdate = Convert.ToDateTime(DateAdd("yyyy", 1, textbonddate.Text)).ToString("yyyy-MM-dd")
    '        txtbondexpdate.Text = Convert.ToDateTime(DateAdd("d", -1, strbondexpdate)).ToString("yyyy-MM-dd")
    '        txtqy.Focus()
    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub
    Protected Sub txtqy_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtqy.Text) > Val(txtqty.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Qty should not be greater than NOC qty');", True)
                txtqy.Text = ""
                txtqy.Focus()
                Exit Sub
            End If
            ddlunit.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtStoragearea_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtStoragearea.Text > txtstorage.Text Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Storage area should not be greater than NOC area');", True)
                txtStoragearea.Text = ""
                txtStoragearea.Focus()
            Else
                txtlocation.Focus()
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtkgs_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtkgs.Text > txtgrosss.Text Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Storage area should not be greater than NOC area');", True)
                txtkgs.Text = ""
                txtkgs.Focus()
            Else
                txtreg.Focus()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            Control_Clear(sender, e)
            strSql = ""
            strSql += "select * from Temp_NOC_Search_Unloading where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtnocno.Text = Trim(dt.Rows(0)("NOCno") & "")
                Call txtnocno_TextChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlhous_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlhous.SelectedIndexChanged
        Try
            strSql = ""
            strSql += "select EntryID,GodownCode from GodownM where IsActive=1 and Warehousecode='" & ddlhous.SelectedItem.Text & "'"
            dt = db.sub_GetDatatable(strSql)

            ddlgodown.DataSource = dt
            ddlgodown.DataTextField = "GodownCode"
            ddlgodown.DataValueField = "entryid"
            ddlgodown.DataBind()

            ddlgodown.Items.Insert(0, New ListItem("--Select--", 0))
            ddlhous.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlgodown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlgodown.SelectedIndexChanged
        Try
            strSql = ""
            strSql += "select entryid,LotNo from LotM where IsActive=1 and Warehousecode='" & ddlhous.SelectedItem.Text & "' and GodownCode='" & ddlgodown.SelectedValue & "'"
            dt = db.sub_GetDatatable(strSql)

            ddllot.DataSource = dt
            ddllot.DataTextField = "LotNo"
            ddllot.DataValueField = "entryid"
            ddllot.DataBind()

            ddllot.Items.Insert(0, New ListItem("--Select--", 0))
            ddlgodown.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtweek_TextChanged(sender As Object, e As EventArgs) Handles txtweek.TextChanged
        Try
            If txtbondindate.Text <> "" Then
                If txtweek.Text <> "" Then
                    txtday.Text = Val(txtweek.Text) * 7
                    txtExpiryDate.Text = Convert.ToDateTime(DateAdd("d", (Val(txtweek.Text) * 7 - 1), Convert.ToDateTime(txtbondindate.Text).ToString("dd-MM-yyyy"))).ToString("yyyy-MM-dd")
                End If
            End If
            ddlConsignee.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtNOCNo_TextChanged1(sender As Object, e As EventArgs)
        Try

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
End Class
