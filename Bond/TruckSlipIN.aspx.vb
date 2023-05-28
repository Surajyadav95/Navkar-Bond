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
            db.sub_ExecuteNonQuery("Delete from Temp_truck Where ADDED_BY=" & Session("UserId_BondCFS") & "")
            txtslipdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtlrdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
            Add(sender, e)
            txtNOCNo.Focus()
        End If

    End Sub
   
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_truck_fill_pecent_slip")
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
            strSql += "select * from truck_in where VEHICLE_NO='" & Trim(txtVehilceNo.Text & "") & "' and IS_OUT=0"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Truck In Slip already generated for this vehicle no!.');", True)
                txtVehilceNo.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_INSERT_TRUCK_IN'" & Convert.ToDateTime(Trim(txtslipdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlSlipType.SelectedItem.Text & "") & "',"
            strSql += "'" & Trim(txtVehilceNo.Text & "") & "','" & Trim(ddlparty.SelectedValue & "") & "','" & Trim(txtdriver.Text & "") & "','" & Trim(txtlicense.Text & "") & "',"
            strSql += "'" & Trim(txtcontact.Text & "") & "','" & Trim(txtlrNO.Text & "") & "','" & Convert.ToDateTime(Trim(txtlrdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_BondCFS") & "','" & Trim(txtremarks.Text & "") & "','" & Trim(txtNOCNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            txtSlipNoPrint.Text = dt.Rows(0)("SLIP_NO")
            Clear(sender, e)
            txtNOCNo.Text = ""
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
            strSql += "USP_truck_name '" & Replace(Trim(txtcontainer.Text & ""), "'", "''") & "','" & Session("UserId_BondCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                txtcontainer.Text = ""

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No Already Exists .');", True)
                Exit Sub
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Truck In Slip already generated for this container no!.');", True)
                txtcontainer.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = "USP_INSERT_TEMP_TRUCK'" & Trim(txtcontainer.Text & "") & "','" & Trim(ddlsize.SelectedItem.Text & "") & "','" & Trim(ddlcontype.SelectedValue & "") & "','" & Trim(ddlstatus.SelectedItem.Text & "") & "','" & Session("UserId_BondCFS") & "'"
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
            strSql = "USP_FILL_TEMP '" & Session("UserId_BondCFS") & "'"
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
            strSql = "USP_Delete_Temp_truck'" & AutoID & "','" & Session("UserId_BondCFS") & "'"
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
            db.sub_ExecuteNonQuery("Delete from Temp_truck Where ADDED_BY=" & Session("UserId_BondCFS") & "")
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

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtNOCNo_TextChanged(sender As Object, e As EventArgs) Handles txtNOCNo.TextChanged
        Try
            Clear(sender, e)
            strSql = ""
            strSql += "USP_NOC_NO_CHANGE_TRUCK_IN '" & Trim(txtNOCNo.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlparty.SelectedValue = Val(ds.Tables(0).Rows(0)("CustID"))
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('NOC No not found');", True)
                txtNOCNo.Text = ""
                txtNOCNo.Focus()
                Exit Sub
            End If
            UpdatePanel1.Update()
            UpdatePanel2.Update()
            ddlSlipType.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btntruckIn_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_Container_for_TruckIN Where User_ID=" & Session("UserId_BondCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtcontainer.Text = Trim(dt.Rows(0)("Container_No") & "")
                ddlsize.SelectedValue = Val(dt.Rows(0)("Size") & "")
            End If
            up_grid.Update()
            ddlcontype.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnNOCList_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select top 1 * from Temp_NOC_List_TruckIn where userid=" & Session("UserId_BondCFS") & " order by AddedOn desc"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtNOCNo.Text = Trim(dt.Rows(0)("NOCNo") & "")
                txtNOCNo_TextChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class