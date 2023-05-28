Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt9 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblGroup1Amt As Double

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
            db.sub_ExecuteNonQuery("Delete from Temp_Modify_Assessment Where UserID=" & Session("UserId_BondCFS") & "")
            Dim strWorkyear As String = ""
            If Now.Month < 4 Then
                strWorkyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                strWorkyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If
            txtworkyear.Text = strWorkyear
            Filldropdown()
            FillGrid()
            If Not (Request.QueryString("AccountIDEdit") = "") Then
                'AccountID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("AccountIDEdit")))

            End If
            txtinvno.Focus()
            ddlCommodity_TextChanged(sender, e)
        End If

    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Public Sub Grid()
        Try
            strSql = ""
            strSql += ""
            dt = db.sub_GetDatatable(strSql)
            grdcharges.DataSource = dt
            grdcharges.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_fill_dropdown_modify_assessment"
            dt = db.sub_GetDatatable(strSql)
            ddlaccntheads.DataSource = dt
            ddlaccntheads.DataTextField = "AccountName"
            ddlaccntheads.DataValueField = "AccountID"
            ddlaccntheads.DataBind()
            ddlaccntheads.Items.Insert(0, New ListItem("--Select--", 0))
            strSql = ""
            strSql += "USP_Fill_Noc_list_DOMESTIC"
            ds = db.sub_GetDataSets(strSql)
            ddlCommodity.DataSource = ds.Tables(8)
            ddlCommodity.DataTextField = "Commodity_Group_Name"
            ddlCommodity.DataValueField = "Commodity_Group_ID"
            ddlCommodity.DataBind()


            ddltxtTax.DataSource = ds.Tables(9)
            ddltxtTax.DataTextField = "tax_type_desc"
            ddltxtTax.DataValueField = "id"
            ddltxtTax.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dblSumSGSTAmt As Double = 0, dblSumNetAmtTotal As Double = 0, dblSumCGSTAmt As Double = 0, dblSumIGSTAmt As Double = 0, dblgrandtotal As Double = 0

            strSql = ""
            strSql += "Select * from Temp_Modify_Assessment where UserID='" & Session("UserId_BondCFS") & "' and IsCancel=0"
            dt1 = db.sub_GetDatatable(strSql)
            If Not dt1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No charges applied.');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary")
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_delete_n_insert_into_bond_assessd '" & Session("UserId_BondCFS") & "','" & Trim(txtinvno.Text & "") & "','" & Trim(txtworkyear.Text & "") & "'"
            db.sub_ExecuteNonQuery(strSql)

            strSql = ""
            strSql += "get_sum_charges_bond_MODIFY '" & Trim(txtinvno.Text & "") & "','" & Trim(txtworkyear.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                dblSumSGSTAmt = Val(dt.Rows(0)("SGST"))
                dblSumCGSTAmt = Val(dt.Rows(0)("CGST"))
                dblSumIGSTAmt = Val(dt.Rows(0)("IGST"))
                dblSumNetAmtTotal = Val(dt.Rows(0)("Amount"))
                dblgrandtotal = Val(dblSumSGSTAmt) + Val(dblSumCGSTAmt) + Val(dblSumIGSTAmt) + Val(dblSumNetAmtTotal)
            End If
            strSql = ""
            strSql += "USP_update_into_bond_assessm '" & dblSumSGSTAmt & "','" & dblSumCGSTAmt & "','" & dblSumIGSTAmt & "','" & dblSumNetAmtTotal & "','" & dblgrandtotal & "',"
            strSql += "'" & Trim(txtinvno.Text & "") & "','" & Trim(txtworkyear.Text & "") & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Trim(ddlCommodity.SelectedValue & "") & "','" & Trim(ddltxtTax.SelectedValue & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            Clear()
            txtinvno.Text = ""
            Dim strWorkyear As String = ""
            If Now.Month < 4 Then
                strWorkyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                strWorkyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If
            txtworkyear.Text = strWorkyear
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            lblSession.Text = "Assessment updated successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtinvno_TextChanged(sender As Object, e As EventArgs) Handles txtinvno.TextChanged
        Try
            Clear()          
            strSql = ""
            strSql += "USP_modify_assessment_assessno_textchanged '" & Trim(txtinvno.Text & "") & "','" & Session("UserId_BondCFS") & "','" & Trim(txtworkyear.Text) & "'"
            ds = db.sub_GetDataSets(strSql)
            If Not ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Assessment not generated for this Invoice No.');", True)
                txtinvno.Text = ""
                txtinvno.Focus()
                Exit Sub
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                txtinvdate.Text = Convert.ToDateTime(Trim(ds.Tables(1).Rows(0)("AssessDate"))).ToString("dd-MM-yyyy")
                txtvaliddate.Text = Convert.ToDateTime(Trim(ds.Tables(1).Rows(0)("ValidUptoDate"))).ToString("dd-MM-yyyy")
                txtgstin.Text = Trim(ds.Tables(1).Rows(0)("GSTIn_uniqID"))
                txtgstname.Text = Trim(ds.Tables(1).Rows(0)("gstname"))
                txtaddress.Text = Trim(ds.Tables(1).Rows(0)("gstaddress"))
                lblpartyid.Text = Trim(ds.Tables(1).Rows(0)("partyid"))
                lblstatecode.Text = Trim(ds.Tables(1).Rows(0)("state_code"))
                Sub_SGTRate()
                FillGrid()
            End If
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub Clear()
        Try            
            txtvaliddate.Text = ""
            txtinvdate.Text = ""
            txtgstin.Text = ""
            txtgstname.Text = ""
            txtaddress.Text = ""
            txtremarks.Text = ""
            ddlaccntheads.SelectedValue = 0
            txtamount.Text = ""
            divtblWOTOtal.Attributes.Add("style", "display:none")
            db.sub_ExecuteNonQuery("Delete from Temp_Modify_Assessment Where UserID=" & Session("UserId_BondCFS") & "")
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub FillGrid()
        Try
            strSql = ""
            strSql += "USP_fetch_from_temp_modify_assessment '" & Session("UserId_BondCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            grdcharges.DataSource = ds.Tables(0)
            grdcharges.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                divtblWOTOtal.Attributes.Add("style", "display:block")
            Else
                divtblWOTOtal.Attributes.Add("style", "display:none")
            End If
            If Val(ds.Tables(1).Rows(0)(0)) <> 0 Then
                dblGroup1Amt = Val(ds.Tables(1).Rows(0)(0))
                sub_CalcTotals()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Sub_SGTRate()
        Try
            Dim compid As String = ""
            strSql = ""
            strSql += "select Tinnumber from settings"
            dt9 = db.sub_GetDatatable(strSql)
            If dt9.Rows.Count > 0 Then
                compid = Trim(dt9.Rows(0)(0))
            End If
            strSql = ""
            strSql += "USP_GST_Cal_Web '" & Trim(lblTaxID.Text) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                dblSGST = Val(dt1.Rows(0)("SGST"))
                dblCGST = Val(dt1.Rows(0)("CGST"))
                dblIGST = Val(dt1.Rows(0)("IGST"))
                dbltaxgroupid = Trim(dt1.Rows(0)("settingsID") & "")
                strSGSTPer = "SGST " & dblSGST & "%"
                StrCGSTPEr = "CGST " & dblCGST & "%"
                StrIGSTPer = "IGST " & dblIGST & "%"
                If lblstatecode.Text = compid Then
                    dblIGST = 0
                    StrIGSTPer = "IGST " & dblIGST & "%"
                Else
                    dblSGST = 0
                    dblCGST = 0
                    strSGSTPer = "SGST " & dblSGST & "%"
                    StrCGSTPEr = "CGST " & dblCGST & "%"
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CalcTotals()
        Try
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0
            dbltotal = dblGroup1Amt
            dbltotalcgst = Format(dblGroup1Amt * (dblSGST / 100), "0.00")
            dbltotalsgst = Format(dblGroup1Amt * (dblCGST / 100), "0.00")
            dbltotaligst = Format(dblGroup1Amt * (dblIGST / 100), "0.00")
            strSql = ""
            strSql += "select round(" & dbltotalcgst & ",0) as totalcgst,round(" & dbltotalsgst & ",0) as totalsgst,round(" & dbltotaligst & ",0) as totaligst"
            dt = db.sub_GetDatatable(strSql)
            dblalltotal = dbltotal - dbldisc + Val(dt.Rows(0)("totalsgst")) + Val(dt.Rows(0)("totalcgst")) + Val(dt.Rows(0)("totaligst"))
            lblTotal.Text = dbltotal
            lbldisc.Text = dbldisc
            lblSgstPer.Text = strSGSTPer
            lblCgstPer.Text = StrCGSTPEr
            lblIgstPer.Text = StrIGSTPer
            lblCGST.Text = Val(dt.Rows(0)("totalcgst"))
            lblSGST.Text = Val(dt.Rows(0)("totalsgst"))
            lblIGST.Text = Val(dt.Rows(0)("totaligst"))
            lblAllTotal.Text = dblalltotal
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
            strSql += "Update Temp_Modify_Assessment set IsCancel=1 where AutoID='" & Auto_ID & "'"
            db.sub_ExecuteNonQuery(strSql)
            Sub_SGTRate()
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkadd_Click(sender As Object, e As EventArgs) Handles lnkadd.Click
        Try
            strSql = ""
            strSql += "select * from temp_modify_assessment where IsCancel=0 and AccountID='" & Trim(ddlaccntheads.SelectedValue) & "' and UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('These charges are already applied.');", True)
                lnkadd.Attributes.Add("Class", "btn btn-info")
                ddlaccntheads.Focus()
                Exit Sub
            End If

            Call Sub_SGTRate()

            strSql = ""
            strSql += "USP_insert_into_temp_modify_assessment '" & Trim(txtinvno.Text & "") & "','" & Trim(txtworkyear.Text & "") & "','" & Trim(ddlaccntheads.SelectedValue) & "',"
            strSql += "'" & Trim(txtamount.Text & "") & "','" & Format(Val(txtamount.Text) * (dblSGST / 100), "0.00") & "','" & Format(Val(txtamount.Text) * (dblCGST / 100), "0.00") & "',"
            strSql += "'" & Format(Val(txtamount.Text) * (dblIGST / 100), "0.00") & "','" & dbltaxgroupid & "','" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)

            FillGrid()

            ddlaccntheads.SelectedValue = 0
            txtamount.Text = ""
            lnkadd.Attributes.Add("Class", "btn btn-info")
        Catch ex As Exception
            lnkadd.Attributes.Add("Class", "btn btn-info")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtNetAmount_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)
            Call Sub_SGTRate()
            strSql = ""
            strSql += "USP_UPDATE_CHARGES_TEMP_MODIFY_ASSESSMENT '" & Val(CType(gr.FindControl("txtNetAmount"), TextBox).Text & "") & "','" & Val(CType(gr.FindControl("lblAutoID"), Label).Text & "") & "',"
            strSql += "'" & Format(Val(CType(gr.FindControl("txtNetAmount"), TextBox).Text & "") * (dblSGST / 100), "0.00") & "','" & Format(Val(CType(gr.FindControl("txtNetAmount"), TextBox).Text & "") * (dblCGST / 100), "0.00") & "',"
            strSql += "'" & Format(Val(CType(gr.FindControl("txtNetAmount"), TextBox).Text & "") * (dblIGST / 100), "0.00") & "','" & dbltaxgroupid & "'"
            db.sub_ExecuteNonQuery(strSql)
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlCommodity_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = " select * from Commodity_Group_M where Commodity_Group_ID=" & Val(ddlCommodity.SelectedValue) & " "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddltxtTax.SelectedValue = Val(dt.Rows(0)("TaxGroupID") & "")
                lblTaxID.Text = Val(dt.Rows(0)("settingsID") & "")
            Else
                ddltxtTax.SelectedValue = Val(dt.Rows(0)("TaxGroupID") & "")
                lblTaxID.Text = Val(dt.Rows(0)("settingsID") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
