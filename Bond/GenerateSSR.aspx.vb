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
    Dim AccountID, AccountIDView As String
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
            db.sub_ExecuteNonQuery("Delete from Temp_generate_ssr Where UserID=" & Session("UserId_BondCFS") & "")
            txtssrdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            Filldropdown()
            FillGrid()
            txtnocno.Focus()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_SSR_FILL_DROPDOWN"
            ds = db.sub_GetDataSets(strSql)
            ddlaccntheads.DataSource = ds.Tables(0)
            ddlaccntheads.DataTextField = "AccountName"
            ddlaccntheads.DataValueField = "AccountID"
            ddlaccntheads.DataBind()
            ddlaccntheads.Items.Insert(0, New ListItem("--Select--", 0))

            ddlservicetype.DataSource = ds.Tables(1)
            ddlservicetype.DataTextField = "Service_name"
            ddlservicetype.DataValueField = "ID"
            ddlservicetype.DataBind()
            ddlservicetype.Items.Insert(0, New ListItem("--Select--", 0))

            ddlmaterialtype.DataSource = ds.Tables(2)
            ddlmaterialtype.DataTextField = "Material_name"
            ddlmaterialtype.DataValueField = "ID"
            ddlmaterialtype.DataBind()
            ddlmaterialtype.Items.Insert(0, New ListItem("--Select--", 0))

            ddlSSRType.DataSource = ds.Tables(3)
            ddlSSRType.DataTextField = "SSRType"
            ddlSSRType.DataValueField = "AutoID"
            ddlSSRType.DataBind()
            ddlSSRType.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_SSR_FETCH_FROM_TEMP_GENERATE_SSR '" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No Records added');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary")
                txtcontainerno.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql += "USP_SSR_INSERT_INTO_BOND_SSR '" & Convert.ToDateTime(txtssrdate.Text).ToString("yyyy-MM-dd") & "','" & Trim(txtnocno.Text & "") & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Session("UserId_BondCFS") & "','" & Trim(ddlSSRType.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtssrnoprint.Text = Trim(dt.Rows(0)(0) & "")
            End If
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            Clear()
            txtnocno.Text = ""
            txtssrdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtnocno.Focus()
            lblSession.Text = "Record saved successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub FillGrid()
        Try
            strSql = ""
            strSql += "USP_SSR_FETCH_FROM_TEMP_GENERATE_SSR '" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtnocno_TextChanged(sender As Object, e As EventArgs)
        Try
            Clear()
            strSql = ""
            strSql += "USP_SSR_NOC_TEXTCHANGED '" & Trim(txtnocno.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            If Not ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid noc no.');", True)
                txtnocno.Text = ""
                txtnocno.Focus()
                Exit Sub
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                txtnocdate.Text = Trim(ds.Tables(1).Rows(0)("NOCDate"))
                txtcustomer.Text = Trim(ds.Tables(1).Rows(0)("agentName"))
                txtcha.Text = Trim(ds.Tables(1).Rows(0)("CHAName"))
            End If
            FillGrid()
            txtcontainerno.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            txtnocdate.Text = ""
            txtcustomer.Text = ""
            txtcha.Text = ""
            txtcontainerno.Text = ""
            ddlsize.SelectedValue = "20"
            ddlaccntheads.SelectedValue = 0
            ddlservicetype.SelectedValue = 0
            ddlmaterialtype.SelectedValue = 0
            txtamount.Text = ""
            txtpkgs.Text = ""
            txtremarks.Text = ""
            db.sub_ExecuteNonQuery("Delete from Temp_generate_ssr Where UserID=" & Session("UserId_BondCFS") & "")
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkadd_Click(sender As Object, e As EventArgs) Handles lnkadd.Click
        Try
            strSql = ""
            strSql += "USP_SSR_VALIDATION_ADD_CONTAINER_DETAILS '" & Trim(txtcontainerno.Text & "") & "','" & Trim(txtnocno.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            'If Not ds.Tables(0).Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid container no.');", True)
            '    lnkadd.Attributes.Add("Class", "btn btn-info")
            '    txtcontainerno.Text = ""
            '    txtcontainerno.Focus()
            '    Exit Sub
            'End If
            If ds.Tables(1).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Conatiner no already added');", True)
                lnkadd.Attributes.Add("Class", "btn btn-info")
                txtcontainerno.Text = ""
                txtcontainerno.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_SSR_INSERT_INTO_TEMP_GENRATE_SSR '" & Trim(txtcontainerno.Text & "") & "','" & Trim(ddlsize.SelectedValue & "") & "','" & Trim(ddlaccntheads.SelectedValue) & "',"
            strSql += "'" & Trim(ddlservicetype.SelectedValue) & "','" & Trim(ddlmaterialtype.SelectedValue) & "','" & Trim(txtamount.Text & "") & "','" & Trim(txtpkgs.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)            
            FillGrid()
            txtcontainerno.Text = ""
            ddlsize.SelectedValue = "20"
            ddlaccntheads.SelectedValue = 0
            ddlservicetype.SelectedValue = 0
            ddlmaterialtype.SelectedValue = 0
            txtamount.Text = ""
            txtpkgs.Text = ""
            lnkadd.Attributes.Add("Class", "btn btn-info")
            txtcontainerno.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim Auto_ID As String = lnkcancel.CommandArgument
            strSql = ""
            strSql += "Update Temp_generate_ssr set iscancel=1 where auto_id='" & Auto_ID & "' and userid='" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlservicetype_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_SSR_AMOUNT_FETCH '" & ddlservicetype.SelectedValue & "','" & ddlmaterialtype.SelectedValue & "','" & ddlsize.SelectedValue & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtamount.Text = Trim(dt.Rows(0)(0))
            Else
                txtamount.Text = ""
            End If
            ddlservicetype.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlmaterialtype_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_SSR_AMOUNT_FETCH '" & ddlservicetype.SelectedValue & "','" & ddlmaterialtype.SelectedValue & "','" & ddlsize.SelectedValue & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtamount.Text = Trim(dt.Rows(0)(0))
            Else
                txtamount.Text = ""
            End If
            ddlmaterialtype.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlsize_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_SSR_AMOUNT_FETCH '" & ddlservicetype.SelectedValue & "','" & ddlmaterialtype.SelectedValue & "','" & ddlsize.SelectedValue & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtamount.Text = Trim(dt.Rows(0)(0))
            Else
                txtamount.Text = ""
            End If
            ddlsize.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Button1_ServerClick(sender As Object, e As EventArgs)
        Try
            lblquoteApprove.Text = "Do you wish to print SSR?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub grdcontainer_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                Dim ddlaccntheadsgrid As DropDownList = CType(e.Row.FindControl("ddlaccntheadsgrid"), DropDownList)
                Dim ddlservicetypegrid As DropDownList = CType(e.Row.FindControl("ddlservicetypegrid"), DropDownList)
                Dim ddlmaterialtypegrid As DropDownList = CType(e.Row.FindControl("ddlmaterialtypegrid"), DropDownList)
                strSql = ""
                strSql += "USP_SSR_FILL_DROPDOWN"
                ds = db.sub_GetDataSets(strSql)
                ddlaccntheadsgrid.DataSource = ds.Tables(0)
                ddlaccntheadsgrid.DataTextField = "AccountName"
                ddlaccntheadsgrid.DataValueField = "AccountID"
                ddlaccntheadsgrid.DataBind()
                ddlaccntheadsgrid.Items.Insert(0, New ListItem("--Select--", 0))

                ddlservicetypegrid.DataSource = ds.Tables(1)
                ddlservicetypegrid.DataTextField = "Service_name"
                ddlservicetypegrid.DataValueField = "ID"
                ddlservicetypegrid.DataBind()
                ddlservicetypegrid.Items.Insert(0, New ListItem("--Select--", 0))

                ddlmaterialtypegrid.DataSource = ds.Tables(2)
                ddlmaterialtypegrid.DataTextField = "Material_name"
                ddlmaterialtypegrid.DataValueField = "ID"
                ddlmaterialtypegrid.DataBind()
                ddlmaterialtypegrid.Items.Insert(0, New ListItem("--Select--", 0))

                Dim accountid As String = CType(e.Row.FindControl("lblaccntid"), Label).Text
                If Not (accountid = "") Then
                    ddlaccntheadsgrid.Items.FindByValue(accountid).Selected = True
                End If
                Dim lblservicetype As String = CType(e.Row.FindControl("lblservicetype"), Label).Text
                If Not (lblservicetype = "") Then
                    ddlservicetypegrid.Items.FindByValue(lblservicetype).Selected = True
                End If
                Dim lblmaterialtype As String = CType(e.Row.FindControl("lblmaterialtype"), Label).Text
                If Not (lblmaterialtype = "") Then
                    ddlmaterialtypegrid.Items.FindByValue(lblmaterialtype).Selected = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlservicetypegrid_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, DropDownList).NamingContainer, GridViewRow)

            strSql = ""
            strSql += "USP_SSR_AMOUNT_FETCH '" & Trim(CType(gr.FindControl("ddlservicetypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlmaterialtypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("lblsize"), Label).Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                CType(gr.FindControl("txtamountgrid"), TextBox).Text = Trim(dt.Rows(0)(0))
            Else
                CType(gr.FindControl("txtamountgrid"), TextBox).Text = ""
            End If
            strSql = ""
            strSql += "USP_SSR_UPDATE_INTO_TEMP_GENERATE_SSR '" & Trim(CType(gr.FindControl("lblautoid"), Label).Text) & "','" & Trim(CType(gr.FindControl("ddlservicetypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlmaterialtypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("txtamountgrid"), TextBox).Text) & "'"
            strSql += ",'" & Trim(CType(gr.FindControl("ddlaccntheadsgrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlaccntheadsgrid"), DropDownList).SelectedItem.Text) & "'"

            db.sub_ExecuteNonQuery(strSql)
            UpdatePanel1.Update()
            CType(gr.FindControl("ddlservicetypegrid"), DropDownList).Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlmaterialtypegrid_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, DropDownList).NamingContainer, GridViewRow)

            strSql = ""
            strSql += "USP_SSR_AMOUNT_FETCH '" & Trim(CType(gr.FindControl("ddlservicetypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlmaterialtypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("lblsize"), Label).Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                CType(gr.FindControl("txtamountgrid"), TextBox).Text = Trim(dt.Rows(0)(0))
            Else
                CType(gr.FindControl("txtamountgrid"), TextBox).Text = ""
            End If
            strSql = ""
            strSql += "USP_SSR_UPDATE_INTO_TEMP_GENERATE_SSR '" & Trim(CType(gr.FindControl("lblautoid"), Label).Text) & "','" & Trim(CType(gr.FindControl("ddlservicetypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlmaterialtypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("txtamountgrid"), TextBox).Text) & "'"
            strSql += ",'" & Trim(CType(gr.FindControl("ddlaccntheadsgrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlaccntheadsgrid"), DropDownList).SelectedItem.Text) & "'"

            db.sub_ExecuteNonQuery(strSql)
            UpdatePanel1.Update()
            CType(gr.FindControl("ddlmaterialtypegrid"), DropDownList).Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtamountgrid_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)
            strSql = ""
            strSql += "USP_SSR_UPDATE_INTO_TEMP_GENERATE_SSR '" & Trim(CType(gr.FindControl("lblautoid"), Label).Text) & "','" & Trim(CType(gr.FindControl("ddlservicetypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlmaterialtypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("txtamountgrid"), TextBox).Text) & "'"
            strSql += ",'" & Trim(CType(gr.FindControl("ddlaccntheadsgrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlaccntheadsgrid"), DropDownList).SelectedItem.Text) & "'"
            db.sub_ExecuteNonQuery(strSql)
            CType(gr.FindControl("txtamountgrid"), TextBox).Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlaccntheadsgrid_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, DropDownList).NamingContainer, GridViewRow)
            strSql = ""
            strSql += "USP_SSR_UPDATE_INTO_TEMP_GENERATE_SSR '" & Trim(CType(gr.FindControl("lblautoid"), Label).Text) & "','" & Trim(CType(gr.FindControl("ddlservicetypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlmaterialtypegrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("txtamountgrid"), TextBox).Text) & "'"
            strSql += ",'" & Trim(CType(gr.FindControl("ddlaccntheadsgrid"), DropDownList).SelectedValue) & "','" & Trim(CType(gr.FindControl("ddlaccntheadsgrid"), DropDownList).SelectedItem.Text) & "'"
            db.sub_ExecuteNonQuery(strSql)
            CType(gr.FindControl("ddlaccntheadsgrid"), DropDownList).Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
