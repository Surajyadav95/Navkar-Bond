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
        'If Session("UserId_BondCFS") Is Nothing Then
        '    Session("UserId_BondCFS") = Request.Cookies("UserIDPRE_Bond").Value
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

            If Not (Request.QueryString("EntryIDEdit") = "") Then
                EntryID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("EntryIDEdit")))
                strSql = ""
                strSql = "USP_Edit_GodownM '" & EntryID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblentryID.Text = Trim(dt.Rows(0)("EntryID") & "")
                    'lblwhname.Text = Trim(dt.Rows(0)("WHName") & "")
                    'txtWarehousename.Text = Trim(dt.Rows(0)("WHName") & "")
                    txtcode.Text = Trim(dt.Rows(0)("CentreCode") & "")
                    txtgodown.Text = Trim(dt.Rows(0)("GodownCode") & "")
                    txtdateof.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Data") & "")).ToString("yyyy-MM-dd")
                    ddlWarehouse.SelectedValue = Trim(dt.Rows(0)("Warehousecode") & "")
                    txtWarehouseDesc.Text = Trim(dt.Rows(0)("Warehousedesc") & "")
                    txtConstrucctive.Text = Trim(dt.Rows(0)("conscapacity") & "")
                    txtThumbrule.Text = Trim(dt.Rows(0)("thumbcapacity") & "")
                    txtwidth.Text = Trim(dt.Rows(0)("Width") & "")
                    txtlenth.Text = Trim(dt.Rows(0)("Lenght") & "")
                    txtHeight.Text = Trim(dt.Rows(0)("Height") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")
                    txtAreaIn.Text = Trim(dt.Rows(0)("Area_In_SQM") & "")
                    txtAreaSqft.Text = Trim(dt.Rows(0)("Area_In_SQFT") & "")
                End If
                btnSave.Text = "Update"
                txtcode.ReadOnly = True
                txtgodown.ReadOnly = True
                txtdateof.ReadOnly = True
                ddlWarehouse.Visible = True
            End If
            If Not (Request.QueryString("EntryIDView") = "") Then
                EntryID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("EntryIDView")))
                strSql = ""
                strSql = "USP_Edit_GodownM '" & EntryID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblentryID.Text = Trim(dt.Rows(0)("EntryID") & "")
                    'lblwhname.Text = Trim(dt.Rows(0)("WHName") & "")
                    'txtWarehousename.Text = Trim(dt.Rows(0)("WHName") & "")
                    txtcode.Text = Trim(dt.Rows(0)("CentreCode") & "")
                    txtgodown.Text = Trim(dt.Rows(0)("GodownCode") & "")
                    txtdateof.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Data") & "")).ToString("yyyy-MM-dd")
                    ddlWarehouse.SelectedValue = Trim(dt.Rows(0)("Warehousecode") & "")
                    txtWarehouseDesc.Text = Trim(dt.Rows(0)("Warehousedesc") & "")
                    txtConstrucctive.Text = Trim(dt.Rows(0)("conscapacity") & "")
                    txtThumbrule.Text = Trim(dt.Rows(0)("thumbcapacity") & "")
                    txtwidth.Text = Trim(dt.Rows(0)("Width") & "")
                    txtlenth.Text = Trim(dt.Rows(0)("Lenght") & "")
                    txtHeight.Text = Trim(dt.Rows(0)("Height") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")
                    txtAreaIn.Text = Trim(dt.Rows(0)("Area_In_SQM") & "")
                    txtAreaSqft.Text = Trim(dt.Rows(0)("Area_In_SQFT") & "")
                End If
                Panel2.Enabled = False
                Panel1.Enabled = False
                chkisActive.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False

            End If
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Get_Godown_List")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If
            ds = db.sub_GetDataSets("usp_godown_fill")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlWarehouse.DataSource = ds.Tables(0)
                ddlWarehouse.DataTextField = "Warehouse_Code"
                ddlWarehouse.DataValueField = "Warehouse_Code"
                ddlWarehouse.DataBind()
                ddlWarehouse.Items.Insert(0, New ListItem("--Select--", 0))
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

                strSql = ""
                strSql += " USP_Update_GodownM '" & Trim(lblentryID.Text & "") & "','" & Trim(txtcode.Text & "") & "','" & Replace(Trim(txtgodown.Text & ""), "'", "''") & "','" & Convert.ToDateTime(Trim(txtdateof.Text & "")).ToString("yyyy-MM-dd") & "',"
                strSql += "'" & (ddlWarehouse.SelectedValue & "") & "','" & Trim(txtWarehouseDesc.Text & "") & "',"
                strSql += "'" & Trim(txtConstrucctive.Text & "") & "','" & Trim(txtThumbrule.Text & "") & "','" & Trim(txtwidth.Text & "") & "',"
                strSql += "'" & Trim(txtlenth.Text & "") & "','" & Trim(txtHeight.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "',"
                strSql += "'" & Trim(txtAreaIn.Text & "") & "','" & Trim(txtAreaSqft.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record updated successfully for Entry ID " & lblentryID.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()

            Else

                strSql = ""
                strSql += "USP_GodownM_Name '" & Replace(Trim(txtgodown.Text & ""), "'", "''") & "','" & Trim(ddlWarehouse.SelectedItem.Text) & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtgodown.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Godown code already exists .');", True)
                    Exit Sub
                End If

                strSql = ""
                strSql += " USP_insert_godown '" & Trim(txtcode.Text & "") & "','" & Replace(Trim(txtgodown.Text & ""), "'", "''") & "','" & Convert.ToDateTime(Trim(txtdateof.Text & "")).ToString("yyyy-MM-dd") & "',"
                strSql += "'" & (ddlWarehouse.SelectedValue & "") & "','" & Trim(txtWarehouseDesc.Text & "") & "',"
                strSql += "'" & Trim(txtConstrucctive.Text & "") & "','" & Trim(txtThumbrule.Text & "") & "','" & Trim(txtwidth.Text & "") & "',"
                strSql += "'" & Trim(txtlenth.Text & "") & "','" & Trim(txtHeight.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "',"
                strSql += "'" & Trim(txtAreaIn.Text & "") & "','" & Trim(txtAreaSqft.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully for Entry ID " & dt.Rows(0)("EntryID") & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtAreaIn_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtAreaIn.Text <> "" Then
                'txtAreaSqft.Text = Val(txtAreaIn.Text) * 10.76
                txtAreaSqft.Text = Val(txtAreaIn.Text) * 10.76
            Else
                txtAreaSqft.Text = ""
            End If
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub

End Class
