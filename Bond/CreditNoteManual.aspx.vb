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
            db.sub_ExecuteNonQuery("Delete from Temp_Credit_Note_Manual Where UserID=" & Session("UserId_BondCFS") & "")
            FillDropdown()
            Dim strWorkyear As String = ""
            If Now.Month < 4 Then
                strWorkyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                strWorkyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If
            txtworkyear.Text = strWorkyear
            txtcreditnotedate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")
            txtInvoiceDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            FillGrid()
            ddlCategory.Focus()
        End If
    End Sub
    Protected Sub FillDropdown()
        strSql = ""
        strSql += "USP_FILL_DROPDOWN_CREDIT_NOTE_MANUAL"
        ds = db.sub_GetDataSets(strSql)
        ddlCategory.DataSource = ds.Tables(0)
        ddlCategory.DataTextField = "TYPE"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("--Select--", 0))

        ddlTax.DataSource = ds.Tables(1)
        ddlTax.DataTextField = "TAX"
        ddlTax.DataValueField = "SETTINGSID"
        ddlTax.DataBind()
        ddlTax.Items.Insert(0, New ListItem("--Select--", 0))

        strSql = ""
        strSql += "USP_ACCOUNT_HEADS_CATEGORY_WISE '" & Trim(ddlCategory.SelectedValue) & "'"
        dt = db.sub_GetDatatable(strSql)
        ddlaccntheads.DataSource = dt
        ddlaccntheads.DataTextField = "AccountName"
        ddlaccntheads.DataValueField = "ACCOUNTID"
        ddlaccntheads.DataBind()
        ddlaccntheads.Items.Insert(0, New ListItem("--Select--", 0))

    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim strWorkyear As String = "", dblCount As Double = 0
            If Now.Month < 4 Then
                strWorkyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                strWorkyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If
            Dim dblSumSGSTAmt As Double = 0, dblSumNetAmtTotal As Double = 0, dblSumCGSTAmt As Double = 0, dblSumIGSTAmt As Double = 0, dblgrandtotal As Double = 0
            Dim Count As Double = 0, dblassessno As Double = 0
            strSql = ""
            strSql += "Select * from temp_credit_note_Manual where UserID='" & Session("UserId_BondCFS") & "' and IsCancel=0"
            dt1 = db.sub_GetDatatable(strSql)
            If Not dt1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No charges applied.');", True)
                Exit Sub
            End If
            For Each row In grdcharges.Rows
                'If CType(row.FindControl("txtcreditamt"), TextBox).Text = "" Then
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter credit amount');", True)
                '    CType(row.FindControl("txtcreditamt"), TextBox).Focus()
                '    Exit Sub
                'End If
                dblCount += Val(CType(row.FindControl("lblntamnt"), Label).Text)
            Next
            If dblCount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter credit amount');", True)
                grdcharges.Focus()
                Exit Sub
            End If
            'For Each row In grdcharges.Rows
            '    Dim Creditamt1 As Double = 0
            '    Dim netamt As Double = Val(CType(row.FindControl("lblntamnt"), Label).Text & "")
            '    Dim creditamt As Double = Val(CType(row.FindControl("txtcreditamt"), TextBox).Text & "")
            '    Dim accntid As Double = Val(CType(row.FindControl("lblaccntid"), Label).Text & "")
            '    strSql = ""
            '    strSql += "select ISNULL(SUM(creditamount),0) as CreditAmt from temp_credit_note where accountid='" & accntid & "' and UserId='" & Session("UserId_BondCFS") & "' and IsCancel=0"
            '    dt = db.sub_GetDatatable(strSql)
            '    Creditamt1 = Val(dt.Rows(0)("CreditAmt"))
            '    If creditamt + Creditamt1 > netamt Then
            '        ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "Key", "alert('Credit amount should not be greater than net amount.');", True)
            '        CType(row.FindControl("txtcreditamt"), TextBox).Text = ""
            '        CType(row.FindControl("txtcreditamt"), TextBox).Focus()
            '        Exit Sub
            '    End If
            'Next
            If Trim(txtcreditamount.Text & "") = "0" Or Trim(txtcreditamount.Text & "") = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid credit amount');", True)
                grdcharges.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql += "SELECT isnull(MAX(CreditNoteNo),0) FROM CreditNoteM WITH(XLOCK) WHERE WorkYear='" & strWorkyear & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows(0)(0) = 0 Then
                dblassessno = 1
            Else
                dblassessno = Val(dt.Rows(0)(0)) + 1
            End If
            'Call Sub_SGTRate()
            For Each row In grdcharges.Rows
                If Not Val(CType(row.FindControl("lblntamnt"), Label).Text & "") = 0 Then
                    Call Sub_SGTRate(Val(CType(row.FindControl("lblTaxID"), Label).Text))
                    strSql = ""
                    strSql += "USP_insert_into_creditnoted '" & dblassessno & "','" & Trim(strWorkyear & "") & "','" & Val(CType(row.FindControl("lblaccntid"), Label).Text) & "',"
                    strSql += "'" & Val(CType(row.FindControl("txtcreditamt"), TextBox).Text) & "','" & Val(CType(row.FindControl("lblntamnt"), Label).Text) & "',"
                    strSql += "'" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblSGST / 100), "0.00") & "','" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblCGST / 100), "0.00") & "',"
                    strSql += "'" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblIGST / 100), "0.00") & "','" & dbltaxgroupid & "','" & Trim(CType(row.FindControl("lblInvoiceNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblWorkyear"), Label).Text) & "','" & Trim(CType(row.FindControl("lblAssessNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text) & "',NULL, '" & Trim(CType(row.FindControl("lblInvoiceDate"), Label).Text) & "' "
                    db.sub_ExecuteNonQuery(strSql)
                End If
            Next
            strSql = ""
            strSql += "get_sum_charges_creditnote '" & dblassessno & "','" & strWorkyear & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                dblSumSGSTAmt = Val(dt.Rows(0)("SGST"))
                dblSumCGSTAmt = Val(dt.Rows(0)("CGST"))
                dblSumIGSTAmt = Val(dt.Rows(0)("IGST"))
                dblSumNetAmtTotal = Val(dt.Rows(0)("Amount"))
                dblgrandtotal = Val(dblSumSGSTAmt) + Val(dblSumCGSTAmt) + Val(dblSumIGSTAmt) + Val(dblSumNetAmtTotal)
            End If
            strSql = ""
            strSql += "USP_insert_into_creditnotem '" & dblassessno & "','" & strWorkyear & "','" & Convert.ToDateTime(txtcreditnotedate.Text).ToString("yyyy-MM-dd") & "','',"
            strSql += "'','" & Trim(lblpartyid.Text) & "','0','" & dblSumNetAmtTotal & "','" & dblSumSGSTAmt & "','" & dblSumCGSTAmt & "',"
            strSql += "'" & dblSumIGSTAmt & "','" & dblgrandtotal & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Session("UserId_BondCFS") & "'," & Val(ddlCategory.SelectedValue) & ""
            db.sub_ExecuteNonQuery(strSql)
            txtcreditno.Text = dblassessno
            txtcredityear.Text = strWorkyear
            txtCategoryID.Text = Val(ddlCategory.SelectedValue)            
            Clear()
            txtInvoiceNo.Text = ""
            txtworkyear.Text = strWorkyear
            ddlCategory.Focus()
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            lblSession.Text = "Record saved successfully for Credit Note " & dblassessno & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtinvno_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(ddlCategory.SelectedValue) = 0 Then
                ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "Key", "alert('Please select category first');", True)
                txtInvoiceNo.Text = ""
                ddlCategory.Focus()
                Exit Sub
            End If
            If Trim(txtworkyear.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "Key", "alert('Please enter workyear');", True)
                txtInvoiceNo.Text = ""
                txtworkyear.Focus()
                Exit Sub
            End If
            Clear()
            strSql = ""
            strSql += "USP_fetch_details_for_credit_note '" & Trim(txtInvoiceNo.Text & "") & "','" & Trim(txtworkyear.Text & "") & "','" & Session("UserId_BondCFS") & "'," & Val(ddlCategory.SelectedValue) & ""
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(1).Rows.Count > 0 Then
                If Not Trim(ds.Tables(1).Rows(0)("CreditNoteNo") & "") = 0 Then
                    lblsession1.Text = "Credit Note No: " & Trim(ds.Tables(1).Rows(0)("CreditNoteNo") & "") & " already generated for this assessment.Are you want to proceed further?"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
                    'UpdatePanel2.Update()
                    Exit Sub
                End If
            ElseIf ds.Tables(0).Rows.Count > 0 Then
                txtgstname.Text = Trim(ds.Tables(0).Rows(0)("GSTName") & "")
                txtgstin.Text = Trim(ds.Tables(0).Rows(0)("GSTAddress") & "")
                lblstatecode.Text = Trim(ds.Tables(0).Rows(0)("state_Code") & "")
                txtnettotal.Text = Trim(ds.Tables(0).Rows(0)("NetTotal") & "")
                txtsgst.Text = Val(ds.Tables(0).Rows(0)("SGST"))
                txtcgst.Text = Val(ds.Tables(0).Rows(0)("CGST"))
                txtigst.Text = Val(ds.Tables(0).Rows(0)("IGST"))
                txtgrandtotal.Text = Val(ds.Tables(0).Rows(0)("GrandTotal"))
                lblpartyid.Text = Trim(ds.Tables(0).Rows(0)("PartyID") & "")
            Else
                ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "Key", "alert('No records found');", True)
                txtInvoiceNo.Text = ""
                Exit Sub
            End If
            FillGrid()
            'Sub_SGTRate()
            sub_CalcTotals()
            grdcharges.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            txtcreditnoteNo.Text = ""
            txtgstname.Text = ""
            txtgstin.Text = ""
            lblpartyid.Text = ""
            lblstatecode.Text = ""
            txtnettotal.Text = ""
            txtsgst.Text = ""
            txtcgst.Text = ""
            txtigst.Text = ""
            txtgrandtotal.Text = ""
            txtcreditamount.Text = ""
            txtsgstcredit.Text = ""
            txtcgstcredit.Text = ""
            txtcredittotal.Text = ""
            txtremarks.Text = ""
            lblstatecode.Text = ""
            ddlCategory.SelectedValue = 0
            txtInvoiceNo.Text = ""
            db.sub_ExecuteNonQuery("Delete from Temp_Credit_Note_Manual Where UserID=" & Session("UserId_BondCFS") & "")
            FillGrid()
            ddlCategory.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub FillGrid()
        Try
            strSql = ""
            strSql += "USP_fetch_from_temp_credit_note_Manual '" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcharges.DataSource = dt
            grdcharges.DataBind()
            Up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Sub_SGTRate(TaxID As Double)
        Try
            Dim compid As String = ""
            strSql = ""
            strSql += "select Tinnumber from settings"
            dt9 = db.sub_GetDatatable(strSql)
            If dt9.Rows.Count > 0 Then
                compid = Trim(dt9.Rows(0)(0))
            End If
            strSql = ""
            strSql += "USP_GST_Cal_CreditNote " & TaxID & ""
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
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0, dblTaxAmount As Double = 0
            For Each row In grdcharges.Rows
                Dim CreditAmt As Double = Val(CType(row.FindControl("lblntamnt"), Label).Text & "")
                If Trim(CType(row.FindControl("lblTax"), Label).Text) = True Then
                    Sub_SGTRate(Val(CType(row.FindControl("lblTaxID"), Label).Text))
                    dblTaxAmount = CreditAmt
                    dbltotalcgst += Format(dblTaxAmount * (dblSGST / 100), "0.00")
                    dbltotalsgst += Format(dblTaxAmount * (dblCGST / 100), "0.00")
                    dbltotaligst += Format(dblTaxAmount * (dblIGST / 100), "0.00")
                End If
                dblGroup1Amt += CreditAmt
            Next
            dbltotal = dblGroup1Amt

            strSql = ""
            strSql += "select round(" & dbltotalcgst & ",0) as totalcgst,round(" & dbltotalsgst & ",0) as totalsgst,round(" & dbltotaligst & ",0) as totaligst"
            dt = db.sub_GetDatatable(strSql)
            dblalltotal = dbltotal - dbldisc + Val(dt.Rows(0)("totalsgst")) + Val(dt.Rows(0)("totalcgst")) + Val(dt.Rows(0)("totaligst"))
            txtcreditamount.Text = dbltotal
            txtsgstcredit.Text = Val(dt.Rows(0)("totalsgst"))
            txtcgstcredit.Text = Val(dt.Rows(0)("totalcgst"))
            txtigstcredit.Text = Val(dt.Rows(0)("totaligst"))
            txtcredittotal.Text = dblalltotal
            Up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtcreditamt_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim grdcharges As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)
            Dim netamt As Double = Val(CType(grdcharges.FindControl("lblntamnt"), Label).Text & "")
            Dim creditamt As Double = Val(CType(grdcharges.FindControl("txtcreditamt"), TextBox).Text & "")
            Dim accntid As Double = Val(CType(grdcharges.FindControl("lblaccntid"), Label).Text & "")
            
            Dim Creditamt1 As Double = 0
            strSql = ""
            strSql += "select ISNULL(creditamount,0) as CreditAmt from temp_credit_note where accountid='" & accntid & "' and UserId='" & Session("UserId_BondCFS") & "' and IsCancel=0"
            dt = db.sub_GetDatatable(strSql)
            Creditamt1 = Val(dt.Rows(0)("CreditAmt"))
            If creditamt + Creditamt1 > netamt Then
                ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "Key", "alert('Credit amount should not be greater than net amount.');", True)
                CType(grdcharges.FindControl("txtcreditamt"), TextBox).Text = ""
                CType(grdcharges.FindControl("txtcreditamt"), TextBox).Focus()
                Exit Sub
            End If
            'Sub_SGTRate()
            sub_CalcTotals()
            CType(grdcharges.FindControl("txtcreditamt"), TextBox).Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Button1_ServerClick(sender As Object, e As EventArgs)
        Clear()
        Dim ds1 As New DataSet
        strSql = ""
        strSql += "USP_fetch_details_for_credit_note '" & Trim(txtInvoiceNo.Text & "") & "','" & Trim(txtworkyear.Text & "") & "','" & Session("UserId_BondCFS") & "'," & Val(ddlCategory.SelectedValue) & ""
        ds1 = db.sub_GetDataSets(strSql)
        If ds1.Tables(0).Rows.Count > 0 Then
            txtgstname.Text = Trim(ds1.Tables(0).Rows(0)("GSTName") & "")
            txtgstin.Text = Trim(ds1.Tables(0).Rows(0)("GSTAddress") & "")
            lblstatecode.Text = Trim(ds1.Tables(0).Rows(0)("state_Code") & "")
            txtnettotal.Text = Trim(ds1.Tables(0).Rows(0)("NetTotal") & "")
            txtsgst.Text = Val(ds1.Tables(0).Rows(0)("SGST"))
            txtcgst.Text = Val(ds1.Tables(0).Rows(0)("CGST"))
            txtigst.Text = Val(ds1.Tables(0).Rows(0)("IGST"))
            txtgrandtotal.Text = Val(ds1.Tables(0).Rows(0)("GrandTotal"))
            lblpartyid.Text = Trim(ds1.Tables(0).Rows(0)("PartyID") & "")
        Else
            ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "Key", "alert('No records found');", True)
            txtInvoiceNo.Text = ""
            Exit Sub
        End If
        FillGrid()
        'Sub_SGTRate()
        sub_CalcTotals()
        grdcharges.Focus()
    End Sub

    Protected Sub Button3_ServerClick(sender As Object, e As EventArgs)
        Try
            Label1.Text = "Do you wish to print Credit Note?"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            UpdatePanel4.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_GST_Search_CreditManual where userid='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtgstin.Text = Trim(dt.Rows(0)("GSTNo") & "")
                txtgstname.Text = Trim(dt.Rows(0)("GSTName") & "")
                lblstatecode.Text = Val(dt.Rows(0)("Statecode"))
                lblpartyid.Text = Trim(dt.Rows(0)("Gstid") & "")
                'ddltraiff.SelectedValue = Trim(dt.Rows(0)("TariffID") & "")
                'Call Sub_SGTRate()
                strSql = ""
                strSql += "select isnull(sum(amount),0) as Amount from Temp_Credit_Note_Manual where userid=" & Session("UserId_BondCFS") & ""
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    dblGroup1Amt = Val(dt.Rows(0)(0))
                End If
                Call sub_CalcTotals()
            Else
                txtgstin.Text = ""
                txtgstname.Text = ""
                lblstatecode.Text = ""
                txtgstin.Focus()
                lblpartyid.Text = ""
                'ddltraiff.SelectedValue = 0
                Exit Sub
            End If
            ddlaccntheads.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkadd_Click(sender As Object, e As EventArgs) Handles lnkadd.Click
        Try
            strSql = ""
            strSql += "USP_INSERT_INTO_TEMP_CREDIT_NOTE_MANUAL " & Val(ddlaccntheads.SelectedValue) & ",'" & Trim(ddlaccntheads.SelectedItem.Text) & "','" & Val(txtCreditAmountManual.Text) & "',"
            strSql += "" & Session("UserId_BondCFS") & ",'" & Trim(txtworkyear.Text & "") & "','" & Trim(txtInvoiceNo.Text & "") & "','" & Trim(txtgstname.Text & "") & "','" & Trim(txtContainerNo.Text & "") & "',"
            strSql += "'" & Val(ddlSize.SelectedValue) & "','" & Val(ddlTax.SelectedValue) & "','" & Convert.ToDateTime(Trim(txtInvoiceDate.Text & "")).ToString("yyyy-MM-dd") & "'"
            db.sub_ExecuteNonQuery(strSql)
            FillGrid()
            sub_CalcTotals()
            Dim strWorkyear As String = ""
            If Now.Month < 4 Then
                strWorkyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                strWorkyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If
            'txtworkyear.Text = strWorkyear
            ddlaccntheads.SelectedValue = 0
            'txtInvoiceNo.Text = ""
            txtContainerNo.Text = ""
            ddlSize.SelectedValue = 20
            txtCreditAmountManual.Text = ""
            ddlTax.SelectedValue = 0

            lnkadd.Attributes.Add("Class", "btn btn-info")
        Catch ex As Exception
            lnkadd.Attributes.Add("Class", "btn btn-info")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_ACCOUNT_HEADS_CATEGORY_WISE '" & Trim(ddlCategory.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            ddlaccntheads.DataSource = dt
            ddlaccntheads.DataTextField = "AccountName"
            ddlaccntheads.DataValueField = "ACCOUNTID"
            ddlaccntheads.DataBind()
            ddlaccntheads.Items.Insert(0, New ListItem("--Select--", 0))

            ddlCategory.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkcancel.CommandArgument
            strSql = ""
            strSql += "Update temp_credit_note_Manual set IsCancel=1 where AutoID='" & AutoID & "'"
            db.sub_ExecuteNonQuery(strSql)
            FillGrid()
            sub_CalcTotals()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
