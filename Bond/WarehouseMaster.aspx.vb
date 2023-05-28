Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim WHID, WHIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btnsearch_Click(sender, e)
            If Not (Request.QueryString("WHIDEdit") = "") Then
                WHID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("WHIDEdit")))
                strSql = ""
                strSql = "USP_Edit_Warehouse '" & WHID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblWHID.Text = Trim(dt.Rows(0)("WHID") & "")
                    lblwhname.Text = Trim(dt.Rows(0)("WHName") & "")
                    lblwhCode.Text = Trim(dt.Rows(0)("Warehouse_Code") & "")
                    lblwhSeries.Text = Trim(dt.Rows(0)("Series_Code") & "")
                    txtWarehousename.Text = Trim(dt.Rows(0)("WHName") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")
                    txtWarehouseCode.Text = Trim(dt.Rows(0)("Warehouse_Code") & "")
                    txtseriesCode.Text = Trim(dt.Rows(0)("Series_Code") & "")
                End If

                btnSave.Text = "Update"
            End If
            If Not (Request.QueryString("WHIDView") = "") Then
                WHID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("WHIDView")))
                strSql = ""
                strSql = "USP_Edit_Warehouse '" & WHID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblWHID.Text = Trim(dt.Rows(0)("WHID") & "")
                    lblwhname.Text = Trim(dt.Rows(0)("WHName") & "")
                    lblwhCode.Text = Trim(dt.Rows(0)("Warehouse_Code") & "")
                    lblwhSeries.Text = Trim(dt.Rows(0)("Series_Code") & "")
                    txtWarehousename.Text = Trim(dt.Rows(0)("WHName") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")
                    txtWarehouseCode.Text = Trim(dt.Rows(0)("Warehouse_Code") & "")
                    txtseriesCode.Text = Trim(dt.Rows(0)("Series_Code") & "")
                End If
                txtWarehousename.ReadOnly = True
                txtWarehouseCode.ReadOnly = True
                txtseriesCode.ReadOnly = True
                chkisActive.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False

            End If
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If (btnSave.Text = "Update") Then

                If Trim(txtWarehousename.Text) <> Trim(lblwhname.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM WarehouseM WHERE WHName='" & Trim(txtWarehousename.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        txtWarehousename.Text = ""
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Warehouse name already exists .');", True)
                        txtWarehousename.Focus()
                        Exit Sub
                    End If
                End If
                If Trim(txtWarehouseCode.Text) <> Trim(lblwhCode.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM WarehouseM WHERE Warehouse_Code='" & Trim(txtWarehouseCode.Text & "") & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        txtWarehouseCode.Text = ""
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Warehouse Code already exists .');", True)
                        Exit Sub
                        txtWarehouseCode.Focus()
                    End If
                End If
                If Trim(txtseriesCode.Text) <> Trim(lblwhSeries.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM WarehouseM WHERE Series_Code='" & Trim(txtseriesCode.Text & "") & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        txtseriesCode.Text = ""
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Series Code already exists .');", True)
                        Exit Sub
                        txtseriesCode.Focus()
                    End If
                End If

                strSql = ""
                strSql += "USP_Update_Warehouse'" & Trim(lblWHID.Text & "") & "','" & Trim(txtWarehousename.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "',"
                strSql += "'" & Trim(txtWarehouseCode.Text & "") & "','" & Trim(txtseriesCode.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record updated successfully for Warehouse ID " & lblWHID.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                ''UpdatePanel3.Update()
            Else
                strSql = ""
                strSql += "USP_Warehouse_Name '" & Replace(Trim(txtWarehousename.Text & ""), "'", "''") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtWarehousename.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Warehouse name already exists .');", True)
                    Exit Sub
                    txtWarehousename.Focus()
                End If

                strSql = ""
                strSql += "SELECT Warehouse_Code FROM WarehouseM WHERE Warehouse_Code='" & Replace(Trim(txtWarehouseCode.Text & ""), "'", "''") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtWarehouseCode.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Warehouse Code already exists .');", True)
                    Exit Sub
                    txtWarehouseCode.Focus()
                End If

                strSql = ""
                strSql += "SELECT Series_Code FROM WarehouseM WHERE Series_Code='" & Replace(Trim(txtseriesCode.Text & ""), "'", "''") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtseriesCode.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Series Code already exists .');", True)
                    Exit Sub
                    txtseriesCode.Focus()
                End If

                strSql = ""
                strSql += "USP_save_warehouse'" & Replace(Trim(txtWarehousename.Text & ""), "'", "''") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "',"
                strSql += "'" & Trim(txtWarehouseCode.Text & "") & "','" & Trim(txtseriesCode.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully for Warehouse ID " & dt.Rows(0)("WHID") & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                ''UpdatePanel3.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_search_warehousename '" & Trim(txtsearch.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            'grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub
End Class
