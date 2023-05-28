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
    Dim ID, GodownIDView As String
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
            db.sub_ExecuteNonQuery("Delete from Temp_truck_out Where ADDED_BY=" & Session("UserId_BondCFS") & "")
            txtslipdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtlrdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
            Add(sender, e)
        End If

    End Sub
   
    Protected Sub Filldropdown()
        Try

            dt = db.sub_GetDatatable("USP_truck_fill_pecent_slip_OUT")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If

            ds = db.sub_GetDataSets("USP_TRUCK_FILL")
            If ds.Tables(0).Rows.Count > 0 Then
                ddlparty.DataSource = ds.Tables(0)
                ddlparty.DataTextField = "agentName"
                ddlparty.DataValueField = "agentID"
                ddlparty.DataBind()
                ddlparty.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                ddlcontype.DataSource = ds.Tables(1)
                ddlcontype.DataTextField = "ContainerType"
                ddlcontype.DataValueField = "ContainerTypeID"
                ddlcontype.DataBind()
                ddlcontype.Items.Insert(0, New ListItem("--Select--", 0))
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
            If Not txtcontainer.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Add Container Details!.');", True)
                Exit Sub
            End If

            strSql = ""
            strSql += "USP_INSERT_TRUCK_OUT'" & Convert.ToDateTime(Trim(txtslipdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
            strSql += "'" & Trim(txtSlipNoIN.Text & "") & "','" & Session("UserId_BondCFS") & "','" & Trim(txtremarks.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            txtSlipNoPrint.Text = dt.Rows(0)("SLIP_OUT_NO")
            Clear(sender, e)
            txtSlipNoIN.Text = ""
            lblSession.Text = "Record Saved successfully Slip No " & Val(txtSlipNoPrint.Text) & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnadd_Click(sender As Object, e As EventArgs)
        Try
            Dim size1 As String = ""
            If grdcontainer.Rows.Count = 2 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' Size is Invalid!.');", True)
                Exit Sub
            End If
            For Each row As GridViewRow In grdcontainer.Rows

                size1 = Val(CType(row.FindControl("lblsize"), Label).Text.ToString())

                'size1 = 20
            Next
            If size1 = "20" Then
                If ddlsize.SelectedItem.Text <> 20 Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Size is Invalid!.');", True)
                    Exit Sub
                End If
            ElseIf size1 = "40" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Size is Invalid!!.');", True)
                Exit Sub
            ElseIf size1 = "45" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' Size is Invalid!.');", True)
                Exit Sub
            End If

            strSql = ""
            strSql += "USP_truck_name_out '" & Replace(Trim(txtcontainer.Text & ""), "'", "''") & "','" & Session("UserId_BondCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                txtcontainer.Text = ""

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No Already Exists .');", True)
                Exit Sub
            End If
            If Not (ds.Tables(1).Rows.Count > 0) Then
                txtcontainer.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container not gate in or already gate out.');", True)
                Exit Sub
            End If
            strSql = ""
            strSql = "USP_INSERT_TEMP_TRUCK_OUT'" & Trim(txtcontainer.Text & "") & "','" & Trim(ddlsize.SelectedItem.Text & "") & "','" & Trim(ddlcontype.SelectedValue & "") & "','" & Trim(ddlstatus.SelectedItem.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            Add(sender, e)
            txtcontainer.Text = ""
            ddlsize.SelectedValue = "0"
            ddlcontype.SelectedValue = "0"
            ddlstatus.SelectedValue = "0"

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Add(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_FILL_TEMP_out '" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkdelete_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            strSql = ""
            strSql = "USP_Delete_Temp_truck_out'" & AutoID & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            Add(sender, e)
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub SaveOk_ServerClick(sender As Object, e As EventArgs)
        Try
            lblPrintQue.Text = "Do you wish to print Slip?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Clear(sender As Object, e As EventArgs)
        Try
            db.sub_ExecuteNonQuery("Delete from Temp_truck_out Where ADDED_BY=" & Session("UserId_BondCFS") & "")
            txtslipdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtlrdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Add(sender, e)
            ddlSlipType.SelectedValue = 0
            txtVehilceNo.Text = ""
            ddlparty.SelectedValue = 0
            txtdriver.Text = ""
            txtlicense.Text = ""
            txtcontact.Text = ""
            txtlrNO.Text = ""
            txtremarks.Text = ""
            UpdatePanel1.Update()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtSlipNoIN_TextChanged(sender As Object, e As EventArgs) Handles txtSlipNoIN.TextChanged
        Try
            Clear(sender, e)
            strSql = ""
            strSql += "USP_SLIP_IN_NO_TEXT_CHANGED " & Val(txtSlipNoIN.Text & "") & "," & Session("UserId_BondCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddlSlipType.SelectedValue = Trim(dt.Rows(0)("SLIP_TYPE") & "")
                txtVehilceNo.Text = Trim(dt.Rows(0)("VEHICLE_NO") & "")
                ddlparty.SelectedValue = Trim(dt.Rows(0)("AGENTID") & "")
                txtdriver.Text = Trim(dt.Rows(0)("DRIVER_NAME") & "")
                txtlicense.Text = Trim(dt.Rows(0)("LICENSE_NO") & "")
                txtcontact.Text = Trim(dt.Rows(0)("CONTACT_NO") & "")
                txtlrNO.Text = Trim(dt.Rows(0)("LR_NO") & "")
                txtlrdate.Text = Trim(dt.Rows(0)("SlipDate") & "")
                Add(sender, e)
                txtcontainer.Focus()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!.');", True)
                txtSlipNoIN.Text = ""
                txtSlipNoIN.Focus()
                Clear(sender, e)
                Exit Sub
            End If
            UpdatePanel1.Update()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnTruckOut_Click(sender As Object, e As EventArgs)
        Try
            Clear(sender, e)
            txtSlipNoIN.Text = ""
            strSql = ""
            strSql += "select * from Temp_Truck_Slip_IN where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtSlipNoIN.Text = Trim(dt.Rows(0)("Slip_In_No") & "")
                Call txtSlipNoIN_TextChanged(sender, e)
            End If
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
