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
            db.sub_ExecuteNonQuery("Delete from Temp_Lot Where UniqueID=" & Session("UserId_BondCFS") & "")
            Filldropdown()
            Add(sender, e)

            If Not (Request.QueryString("EntryIDView") = "") Then
                EntryID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("EntryIDView")))
                strSql = ""
                strSql = "USP_View_LotM '" & EntryID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                Add(sender, e)
                If (dt.Rows.Count > 0) Then
                    lblentryID.Text = Trim(dt.Rows(0)("EntryID") & "")
                    'lblwhname.Text = Trim(dt.Rows(0)("WHName") & "")
                    'txtWarehousename.Text = Trim(dt.Rows(0)("WHName") & ""
                    ddlgodown.SelectedValue = Trim(dt.Rows(0)("GodownCode") & "")
                    txtCenter.Text = Trim(dt.Rows(0)("CentreCode") & "")
                    txtWarehouse.Text = Trim(dt.Rows(0)("Warehousecode") & "")
                    txtWarehouseDesc.Text = Trim(dt.Rows(0)("Warehousedesc") & "")
                    'txtStorage.Text = Trim(dt.Rows(0)("LotNo") & "")
                    'txtTotalCapacity.Text = Trim(dt.Rows(0)("Area") & "")
                    'txtwidth.Text = Trim(dt.Rows(0)("Height") & "")
                    'txtlenth.Text = Trim(dt.Rows(0)("Lenght") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")

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
            dt = db.sub_GetDatatable("USP_Get_Lot_M_List")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If

            ds = db.sub_GetDataSets("USP_Fill_Lot")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlgodown.DataSource = ds.Tables(0)
                ddlgodown.DataTextField = "GodownCode"
                ddlgodown.DataValueField = "entryid"
                ddlgodown.DataBind()
                ddlgodown.Items.Insert(0, New ListItem("--Select--", 0))
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

            For Each row As GridViewRow In grdcontainer.Rows

                strSql = ""
                strSql += "USP_INSERT_Lot_Save'" & Trim(ddlgodown.SelectedValue & "") & "','" & Trim(txtCenter.Text & "") & "','" & Trim(txtWarehouse.Text & "") & "','" & Trim(txtWarehouseDesc.Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblStorage"), Label).Text) & "','" & Trim(CType(row.FindControl("lblTotalCapacity"), Label).Text) & "','" & Trim(CType(row.FindControl("lbllenth"), Label).Text) & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblwidth"), Label).Text) & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully for Entry ID " & dt.Rows(0)("EntryID") & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            Next

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            strSql = ""
            strSql += "USP_LOT_MASTER_NAME '" & Trim(txtStorage.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtStorage.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Storage bin already exists .');", True)
                txtStorage.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql = "USP_Insert_Temp_Lot"
            strSql += "'" & Trim(txtStorage.Text & "") & "','" & Trim(txtTotalCapacity.Text & "") & "','" & Trim(txtlenth.Text & "") & "','" & Trim(txtwidth.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            Add(sender, e)
            txtStorage.Text = ""
            txtTotalCapacity.Text = ""
            txtlenth.Text = ""
            txtwidth.Text = ""
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Add(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            dt = db.sub_GetDatatable("USP_Select_Temp_Lot '" & Session("UserId_BondCFS") & "'")
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlgodown_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_Select_GodownM '" & Trim(ddlgodown.SelectedItem.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                ''ddlgodown.SelectedValue = Trim(dt.Rows(0)("GodownCode") & "")
                txtCenter.Text = Trim(dt.Rows(0)("centrecode") & "")
                txtWarehouse.Text = Trim(dt.Rows(0)("Warehousecode") & "")
                txtWarehouseDesc.Text = Trim(dt.Rows(0)("warehousedesc") & "")

            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
