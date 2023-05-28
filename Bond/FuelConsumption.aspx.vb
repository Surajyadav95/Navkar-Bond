Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt6 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
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

            txtIssuedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            btnsearch_Click(sender, e)
            Filldropdown()
            txtFuelRegNo.Text = ""
            ddlFuelType.Focus()

        End If
    End Sub
 Public Sub grid()
        strSql = ""
        strSql += "SP_Fill_Fuel_consumption'" & Trim(txtsearchm.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("usp_fuel_fill_Dropdown_Consumption")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlFuelType.DataSource = ds.Tables(0)
                ddlFuelType.DataTextField = "FuelType"
                ddlFuelType.DataValueField = "Fuelid"
                ddlFuelType.DataBind()
                ddlFuelType.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddlCostCenter.DataSource = ds.Tables(1)
                ddlCostCenter.DataTextField = "cost_center"
                ddlCostCenter.DataValueField = "id"
                ddlCostCenter.DataBind()
                ddlCostCenter.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddlVehicleType.DataSource = ds.Tables(2)
                ddlVehicleType.DataTextField = "vehicleType"
                ddlVehicleType.DataValueField = "vehicleTypeID"
                ddlVehicleType.DataBind()
                ddlVehicleType.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If (ds.Tables(3).Rows.Count > 0) Then
                ddlTraileId.DataSource = ds.Tables(3)
                ddlTraileId.DataTextField = "trailerid"
                ddlTraileId.DataValueField = "trailerid"
                ddlTraileId.DataBind()
                ddlTraileId.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(4).Rows.Count > 0) Then
                ddlDriverName.DataSource = ds.Tables(4)
                ddlDriverName.DataTextField = "DriverName"
                ddlDriverName.DataValueField = "driverID"
                ddlDriverName.DataBind()
                ddlDriverName.Items.Insert(0, New ListItem("--Select--", 0))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SP_Fill_Fuel_consumption '" & Trim(txtsearchm.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlFuelType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim dt As New DataTable
            strSql = ""
            strSql = "exec SP_fuel_consumption_indexchange '" & Trim(ddlFuelType.SelectedItem.Text) & "','" & Val(ddlCostCenter.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtBalQty.Text = Val(dt.Rows(0)(0) & "")
            End If
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlCostCenter_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim dt1 As New DataTable
            strSql = ""
            strSql = "exec SP_fuel_consumption_indexchange '" & Trim(ddlFuelType.SelectedItem.Text) & "','" & Val(ddlCostCenter.SelectedValue) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtBalQty.Text = Val(dt1.Rows(0)(0) & "")
            End If
            'UpdatePanel4.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtTrailerNo_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim dt2 As New DataTable
            strSql = ""
            strSql = "usp_trailers_fill_chenge'" & Trim(txtTrailerNo.Text & "") & "'"
            dt2 = db.sub_GetDatatable(strSql)
            If dt2.Rows.Count > 0 Then
                ddlTraileId.SelectedValue = Trim(dt2.Rows(0)("trailerid") & "")
                ddlDriverName.SelectedValue = Trim(dt2.Rows(0)("DriverID") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SP_Fuel_consumption'" & Convert.ToDateTime(Trim(txtIssuedate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlCostCenter.SelectedValue & "") & "','" & Trim(ddlTraileId.SelectedValue & "") & "','" & Trim(txtBalQty.Text & "") & "',"
            strSql += "'" & Trim(txtIssueQty.Text & "") & "','" & Trim(ddlFuelType.SelectedItem.Text & "") & "','" & Trim(txtReadingFrom.Text & "") & "','" & Trim(txtReadingTo.Text & "") & "','" & Trim(ddlDriverName.SelectedValue & "") & "',"
            strSql += "'" & Session("UserId_BondCFS") & "','" & Trim(txtRemarks.Text & "") & "'"
            dt6 = db.sub_GetDatatable(strSql)
            lblSession.Text = "Record Saved successfully  "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtIssueQty_TextChanged(sender As Object, e As EventArgs)
        Try
            If Trim(txtIssueQty.Text & "") <> "" Then
                If Val(txtIssueQty.Text & "") > Val(txtBalQty.Text & "") Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Issue Qty can not greater than balance Qty..! ');", True)
                    txtIssueQty.Focus()
                    txtIssueQty.Text = ""
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim FuelRegID As String = lnkcancel.CommandArgument
            'Dim WorkYear As String = grdSummary.DataKeys(row.RowIndex).Value.ToString()
            Dim str As String = ""
            txtFuelRegNo.Text = FuelRegID
            'TxtWorkYear.Text = WorkYear

            ClientScript.RegisterStartupScript(Page.GetType(), "OpenList", "<script>OpenCancelFuel(); </script>")

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
