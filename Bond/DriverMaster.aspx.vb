Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim DriverID As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            getItemList()
            If Not (Request.QueryString("DriverIDEdit") = "") Then
                DriverID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("DriverIDEdit")))
                strSql = ""
                strSql = "USP_Edit_Driver '" & DriverID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    LblDriverID.Text = Trim(dt.Rows(0)("driverid") & "")
                    lblDriverName.Text = Trim(dt.Rows(0)("drivername") & "")
                    lblDriverNo.Text = Trim(dt.Rows(0)("DriverNo") & "")
                    txtDriverName.Text = Trim(dt.Rows(0)("drivername") & "")
                    'chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")
                    txtDriverNo.Text = Trim(dt.Rows(0)("DriverNo") & "")
                    'txtseriesCode.Text = Trim(dt.Rows(0)("Series_Code") & "")
                End If

                btnSave.Text = "Update"
            End If
        End If
    End Sub

    Protected Sub getItemList()
        Try
            strSql = ""
            strSql += "SELECT * from Drivers"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            'up_grid.Update()
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
                If Trim(txtDriverName.Text) <> Trim(lblDriverName.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM Drivers WHERE drivername='" & Trim(txtDriverName.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        txtDriverName.Text = ""
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Drivers name already exists .');", True)
                        txtDriverName.Focus()
                        Exit Sub
                    End If
                End If

                If Trim(txtDriverNo.Text) <> Trim(lblDriverNo.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM Drivers WHERE DriverNo='" & Trim(txtDriverNo.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        txtDriverNo.Text = ""
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Drivers No already exists .');", True)
                        txtDriverNo.Focus()
                        Exit Sub
                    End If
                End If

                strSql = ""
                strSql += "USP_Update_Driver'" & Trim(LblDriverID.Text & "") & "','" & Replace(Trim(txtDriverName.Text & ""), "'", "''") & "','" & Replace(Trim(txtDriverNo.Text & ""), "'", "''") & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record updated successfully for Driver ID " & LblDriverID.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            Else
                strSql = ""
                strSql += "USP_Driver_NAME '" & Replace(Trim(txtDriverName.Text & ""), "'", "''") & "','" & Replace(Trim(txtDriverNo.Text & ""), "'", "''") & "'"
                ds = db.sub_GetDataSets(strSql)
                If (ds.Tables(0).Rows.Count > 0) Then
                    txtDriverName.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Driver Name already exists .');", True)
                    Exit Sub
                End If
                If (ds.Tables(1).Rows.Count > 0) Then
                    txtDriverNo.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' Driver No already exists .');", True)
                    Exit Sub
                End If

                strSql = ""
                strSql += "USP_INSERT_DRIVERS'" & Replace(Trim(txtDriverName.Text & ""), "'", "''") & "','" & Replace(Trim(txtDriverNo.Text & ""), "'", "''") & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                'UpdatePanel.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.getItemList()
    End Sub
End Class
