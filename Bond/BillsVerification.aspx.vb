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
            db.sub_ExecuteNonQuery("Delete from TEMP_BILL_VERIFICATION Where UserID=" & Session("UserId_BondCFS") & "")
            txtVerificationDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-1)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtVendorBillDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            Filldropdown()
            FillGrid()
            If Not (Request.QueryString("VerificationNo") = "") Then
                strSql = ""
                strSql += "USP_BILLVERIFICATION_VIEW_VERIFICATION_DETAILS '" & ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("VerificationNo"))) & "'," & Session("UserId_BondCFS") & ""
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    txtVerificationNo.Text = Val(dt.Rows(0)("VerificationNo"))
                    txtVerificationDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("VerificationDate"))).ToString("yyyy-MM-dd")
                    ddlVendor.SelectedValue = Val(dt.Rows(0)("VendorID"))
                    txtVendorBillNo.Text = Trim(dt.Rows(0)("VendorBillNo") & "")
                    txtVendorBillDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("VendorBillDate"))).ToString("yyyy-MM-dd")
                    ddlBillType.SelectedValue = Val(dt.Rows(0)("BillType") & "")
                    ddlActivity.SelectedValue = Val(dt.Rows(0)("Activity") & "")
                    txtfromDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Periodfrom"))).ToString("yyyy-MM-dd")
                    txttoDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Periodto"))).ToString("yyyy-MM-dd")
                    chkIsGST.Checked = Trim(dt.Rows(0)("IsGSTApplicable") & "")
                    lblstatecode.Text = Trim(dt.Rows(0)("State") & "")
                    FillGrid()
                End If
                lnkadd.Visible = False
                Panel2.Enabled = False
                Panel1.Enabled = False
            End If
            ddlVendor.Focus()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_BILLVERIFICATION_FILL_DROPDOWN"
            ds = db.sub_GetDataSets(strSql)
            ddlVendor.DataSource = ds.Tables(0)
            ddlVendor.DataTextField = "Name"
            ddlVendor.DataValueField = "VendorId"
            ddlVendor.DataBind()
            ddlVendor.Items.Insert(0, New ListItem("--Select--", 0))

            ddlBillType.DataSource = ds.Tables(1)
            ddlBillType.DataTextField = "Name"
            ddlBillType.DataValueField = "ID"
            ddlBillType.DataBind()
            ddlBillType.Items.Insert(0, New ListItem("--Select--", 0))

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
            strSql += "select * from Temp_Bill_Verification where IsCancel=0 and UserId='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('First add container details');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary")
                Exit Sub
            End If
            If chkIsGST.Checked = True Then
                Sub_SGTRate()
            Else
                Sub_SGTRate_WithoutGST()
            End If
            strSql = ""
            strSql += "USP_BILLVERIFICATION_INSERT_INTO_VENDOR_BILL_M '" & Convert.ToDateTime(txtVerificationDate.Text).ToString("yyyy-MM-dd") & "','" & Trim(txtVendorBillNo.Text) & "',"
            strSql += "'" & Convert.ToDateTime(txtVendorBillDate.Text).ToString("yyyy-MM-dd") & "'," & Val(ddlVendor.SelectedValue) & "," & Val(ddlBillType.SelectedValue) & ","
            strSql += "'" & Trim(ddlActivity.SelectedItem.Text) & "','" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & chkIsGST.Checked & "','" & Val(lblTotal.Text) & "','" & Val(dblCGST) & "','" & Val(lblCGST.Text) & "','" & Val(dblSGST) & "','" & Val(lblSGST.Text) & "',"
            strSql += "'" & Val(dblIGST) & "','" & Val(lblIGST.Text) & "','" & Val(lblAllTotal.Text) & "'," & Session("UserId_BondCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtVerificationNo.Text = Val(dt.Rows(0)(0))
            End If
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            Clear()
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
            strSql += "USP_BILLVERIFICATION_FILLGRID '" & Session("UserId_BondCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            grdcontainer.DataSource = ds.Tables(0)
            grdcontainer.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                divtblWOTOtal.Attributes.Add("style", "display:block")
                If chkIsGST.Checked = True Then
                    Sub_SGTRate()
                Else
                    Sub_SGTRate_WithoutGST()
                End If
                dblGroup1Amt = Val(ds.Tables(1).Rows(0)(0))
                sub_CalcTotals()
            Else
                divtblWOTOtal.Attributes.Add("style", "display:none")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            txtVerificationDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-1)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtVendorBillDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            ddlVendor.SelectedValue = 0
            txtVendorBillNo.Text = ""
            ddlBillType.SelectedValue = 0
            ddlActivity.SelectedValue = 0
            txtNOCNo.Text = ""
            chkIsGST.Checked = True
            db.sub_ExecuteNonQuery("Delete from TEMP_BILL_VERIFICATION Where UserID=" & Session("UserId_BondCFS") & "")
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkadd_Click(sender As Object, e As EventArgs) Handles lnkadd.Click
        Try
            strSql = ""
            strSql += "USP_BILLVERIFICATION_VALIDATION_ON_NOCNO_ADD " & Session("UserId_BondCFS") & ",'" & Trim(txtNOCNo.Text) & "','" & Trim(ddlActivity.SelectedItem.Text) & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('NOC No already added');", True)
                lnkadd.Attributes.Add("Class", "btn btn-info")
                Exit Sub
            End If
            If Not ds.Tables(1).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & Trim(ds.Tables(2).Rows(0)(0)) & "');", True)
                txtNOCNo.Focus()
                lnkadd.Attributes.Add("Class", "btn btn-info")
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_BILLVERIFICATION_INSERT_INTO_TEMP_BILL_VERIFICATION_NOCNo '" & Trim(ddlActivity.SelectedItem.Text) & "','" & Trim(txtNOCNo.Text) & "','" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            FillGrid()
            lnkadd.Attributes.Add("Class", "btn btn-info")
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
    Protected Sub Button1_ServerClick(sender As Object, e As EventArgs)
        Try
            lblquoteApprove.Text = "Are you sure to settled and closed?"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnshow_Click(sender As Object, e As EventArgs) Handles btnshow.Click
        Try
            chkIsGST.Checked = True
            db.sub_ExecuteNonQuery("Delete from TEMP_BILL_VERIFICATION Where UserID=" & Session("UserId_BondCFS") & "")
            FillGrid()
            strSql = ""
            strSql += "USP_BILLVERIFICATION_INSERT_INTO_TEMP_BILL_VERIFICATION '" & Trim(ddlActivity.SelectedItem.Text) & "','" & Convert.ToDateTime(Trim(txtfromDate.Text)).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & Convert.ToDateTime(Trim(txttoDate.Text)).ToString("yyyy-MM-dd") & "','" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            FillGrid()
            btnshow.Text = "Show"
            btnshow.Attributes.Add("Class", "btn btn-primary")
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
            strSql += "USP_GST_Cal"
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
    Public Sub Sub_SGTRate_WithoutGST()
        Try
            
            dblSGST = 0
            dblCGST = 0
            dblIGST = 0
            dbltaxgroupid = 0
            strSGSTPer = "SGST " & dblSGST & "%"
            StrCGSTPEr = "CGST " & dblCGST & "%"
            StrIGSTPer = "IGST " & dblIGST & "%"

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
    
    Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select isnull(State,0) as State from Vendor_M where VendorId=" & Val(ddlVendor.SelectedValue) & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblstatecode.Text = Val(dt.Rows(0)("State"))
            End If
            ddlVendor.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub chkIsGST_CheckedChanged(sender As Object, e As EventArgs)
        Try
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlActivity_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            chkIsGST.Checked = True
            db.sub_ExecuteNonQuery("Delete from TEMP_BILL_VERIFICATION Where UserID=" & Session("UserId_BondCFS") & "")
            FillGrid()
            ddlActivity.Focus()
            UpdatePanel4.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnYes_ServerClick(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "update Vendor_Bill_M set IsSettled=1,SettledBy=" & Session("UserId_BondCFS") & ",SettledOn=GETDATE() where VerificationNo=" & Val(txtVerificationNo.Text) & ""
            db.sub_ExecuteNonQuery(strSql)
            txtVerificationNo.Text = ""
            Label1.Text = "Settled and closed successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
