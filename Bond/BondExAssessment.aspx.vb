Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9 As DataTable
    Dim ds, ds1, ds2, ds3, ds4 As DataSet
    Dim db As New dbOperation_bond_general

    Dim PONO, GRNNO As String
    Dim taxamount As Double = 0
    Dim total As Double
    Dim intWareHouseID As Integer = 0
    Dim dblSTaxOnAmount As Double
    Dim dblNetAmount As Double
    Dim dblNetAmount_IND As Double
    Dim dblNetPaid As Double
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim ed As New clsEncodeDecode
    Dim dblGroup1Amt As Double
    Dim dblGroup2Amt As Double
    Dim StrPTo As String
    Dim StrPFrom As String
    Dim blnStorageWeek As Boolean = False
    'Dim lblinsvalidupto As Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Convert.ToInt32(Session("UserId_BondCFS")) = 0 Then
            Response.Redirect("~/Bond/Login.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from Temp_ExAssessment Where UserID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from temp_gst_search Where UserID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Bond_Ex_Noc_Search Where UserID=" & Session("UserId_BondCFS") & "")
            txtvalidDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtAssessmentDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            grid()
            Filldropdown()
            txtdepositno.Focus()
            ddlCommodity_TextChanged(sender, e)
        End If
    End Sub
    Public Sub grid()
        strSql = ""
        strSql += "select accountid ,accountname,amount,Rate,UserId,Rate, format(FromDate,'yyyy-MM-dd') as P_From, format(ToDate,'yyyy-MM-dd') as P_TO,Weeks from Temp_ExAssessment where userid=" & Session("UserId_BondCFS") & ""
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            lblchargescount.Visible = True
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = True
            divtblWOTOtal.Attributes.Add("style", "display:block")
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
        Else
            lblchargescount.Visible = False
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = False
            divtblWOTOtal.Attributes.Add("style", "display:none")
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
        End If
    End Sub
    Public Sub grid1()
        strSql = ""
        strSql += "select accountid ,accountname,amount,Rate,UserId,Rate, format(FromDate,'yyyy-MM-dd') as P_From, format(ToDate,'yyyy-MM-dd') as P_TO,Weeks from Temp_ExAssessment where userid=" & Session("UserId_BondCFS") & ""
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            lblchargescount.Visible = True
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = True
            divtblWOTOtal.Attributes.Add("style", "display:block")
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
            btnsave.Focus()
        Else
            lblchargescount.Visible = False
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = False
            divtblWOTOtal.Attributes.Add("style", "display:none")
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No charges found!');", True)
            ddltraiff.Focus()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("usp_fill_Traiff")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddltraiff.DataSource = ds.Tables(0)
                ddltraiff.DataTextField = "TariffID"
                ddltraiff.DataValueField = "EntryID"
                ddltraiff.DataBind()
                ddltraiff.Items.Insert(0, New ListItem("--Select--", 0))
            End If

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
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub txtNocNo_TextChanged(sender As Object, e As EventArgs)
        Try
            Clear()
            strSql = ""
            strSql += "USP_bond_ex_validation_for_invoice '" & Trim(txtdepositno.Text) & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(1).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Ex Assessment already generated for this Deposite No.');", True)
                txtdepositno.Text = ""
                txtdepositno.Focus()
                Exit Sub
            End If
            'If ds.Tables(3).Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Receipt not made for previous invoice. Cannot proceed');", True)
            '    txtdepositno.Text = ""
            '    txtdepositno.Focus()
            '    Exit Sub
            'End If
            If Not ds.Tables(2).Rows.Count > 0 Then
                lblsession1.Text = "Gate Pass not generated. Do you want to proceed?"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
                UpdatePanel6.Update()
                Exit Sub
            End If

            strSql = ""
            strSql = "BONEx_noctext_changed_assessment '" & Trim(txtdepositno.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtbondinno.Text = Trim(dt.Rows(0)("bondno") & "")
                txtNocDate.Text = Trim(dt.Rows(0)("NOCDate") & "")
                txtbondindate.Text = Trim(dt.Rows(0)("EntryDate") & "")
                txtNocNo.Text = Trim(dt.Rows(0)("nocno") & "")
                txtboeno.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtigm.Text = Trim(dt.Rows(0)("IGMNo") & "")
                txtItemNo.Text = Trim(dt.Rows(0)("ItemNo") & "")

                txtbondtype.Text = Trim(dt.Rows(0)("bondtype") & "")
                txtCustomer.Text = Trim(dt.Rows(0)("agentName") & "")
                txtcustaddress.Text = Trim(dt.Rows(0)("Address") & "")
                txtimporter.Text = Trim(dt.Rows(0)("ImporterName") & "")
                txtcha.Text = Trim(dt.Rows(0)("chaname") & "")
                txtcommodity.Text = Trim(dt.Rows(0)("Commodity") & "")
                txtqty.Text = Trim(dt.Rows(0)("qty") & "")
                txtunit.Text = Trim(dt.Rows(0)("unit") & "")
                If Trim(dt.Rows(0)("cargotype") & "") = "HAZ" Then
                    txtArea.Text = Trim(dt.Rows(0)("AreaBalance") & "") * 2
                    txtChargesArea.Text = Trim(dt.Rows(0)("AreaBalance") & "") * 2
                Else
                    txtArea.Text = Trim(dt.Rows(0)("AreaBalance") & "")
                    txtChargesArea.Text = Trim(dt.Rows(0)("AreaBalance") & "")
                End If
                txtcargo.Text = Trim(dt.Rows(0)("cargotype") & "")
                txtgrosskgs.Text = Trim(dt.Rows(0)("GrossWt") & "")
                txtvalue.Text = Trim(dt.Rows(0)("value") & "")
                txtduty.Text = Trim(dt.Rows(0)("duty") & "")
                txtvalduty.Text = Val(txtvalue.Text) + Val(txtduty.Text)
                txt20.Text = Trim(dt.Rows(0)("Size20") & "")
                txt40.Text = Trim(dt.Rows(0)("Size40") & "")
                lblchaid.Text = Trim(dt.Rows(0)("CHAID") & "")
                lblcustid.Text = Trim(dt.Rows(0)("CustID") & "")
                lblimporterid.Text = Trim(dt.Rows(0)("ImporterId") & "")
                txtvalidDate.Text = Convert.ToDateTime(dt.Rows(0)("ExpiryDate")).ToString("yyyy-MM-dd")
                ddltraiff.SelectedValue = Trim(dt.Rows(0)("TariffID") & "")
                txtGatePassDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("GatePassDate") & "")).ToString("yyyy-MM-dd")
                txtNoVehicles.Text = Trim(dt.Rows(0)("NoVehicles") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!');", True)
                txtdepositno.Text = ""
                txtdepositno.Focus()
                Exit Sub
            End If
            txtgstin.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkUpdate_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_bond_GST '" & Trim(txtgstin.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtgstname.Text = Trim(dt.Rows(0)("GSTName") & "")
                lblstatecode.Text = Val(dt.Rows(0)("state_Code"))
                lblpartyid.Text = Trim(dt.Rows(0)("GSTID") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('GST No not found');", True)
                txtgstin.Text = ""
                txtgstname.Text = ""
                lblstatecode.Text = ""
                txtgstin.Focus()
                lblpartyid.Text = ""
                Exit Sub
            End If
            ddltraiff.Focus()
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
    Protected Sub btncal_Click(sender As Object, e As EventArgs)
        Try
            db.sub_ExecuteNonQuery("Delete from Temp_ExAssessment Where UserID=" & Session("UserId_BondCFS") & "")
            Call Sub_SGTRate()
            strSql = ""
            strSql += "USP_bond_exassessment_calculation_area '" & Trim(ddltraiff.SelectedItem.Text) & "','" & Trim(txtNocNo.Text) & "','" & Convert.ToDateTime(Trim(txtGatePassDate.Text)).ToString("yyyyMMdd") & "','" & Trim(txtdepositno.Text) & "','" & Trim(txtcargo.Text) & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(1).Rows.Count > 0 Then
                    txtArea.Text = ""
                    txtChargesArea.Text = ""
                    txtArea.Text = ds.Tables(1).Rows(0)("Areareleased")
                    txtChargesArea.Text = ds.Tables(1).Rows(0)("Areareleased")
                End If
            Else
                If ds.Tables(2).Rows.Count > 0 Then
                    txtArea.Text = ""
                    txtChargesArea.Text = ""
                    txtArea.Text = ds.Tables(2).Rows(0)("AreaBalance")
                    txtChargesArea.Text = ds.Tables(2).Rows(0)("AreaBalance")
                End If
            End If

            If ds.Tables(3).Rows.Count > 0 Then
                txtvalue.Text = ""
                txtduty.Text = ""
                txtvalduty.Text = ""
                If ds.Tables(1).Rows.Count > 0 Then
                    txtvalue.Text = Trim(ds.Tables(1).Rows(0)("deliveredvalue"))
                    txtduty.Text = Trim(ds.Tables(1).Rows(0)("deliveredduty"))
                    txtvalduty.Text = Val(txtvalue.Text) + Val(txtduty.Text)
                End If
            Else
                txtvalue.Text = ""
                txtduty.Text = ""
                If ds.Tables(4).Rows.Count > 0 Then
                    txtvalue.Text = Trim(ds.Tables(4).Rows(0)("value"))
                    txtduty.Text = Trim(ds.Tables(4).Rows(0)("duty"))
                    txtvalduty.Text = Val(txtvalue.Text) + Val(txtduty.Text)
                End If
            End If
            If Not ds.Tables(5).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid tariff details. Please contact your administrator!');", True)
                ddltraiff.Focus()
                Exit Sub
            End If
            Call sub_chargesgrid()
            Call grid1()
            Call sub_CalcTotals()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_chargesgrid()
        Try
            dblSTaxOnAmount = 0
            dblGroup1Amt = 0
            dblGroup2Amt = 0
            strSql = ""
            strSql += "USP_bondex_assessment_for_accountid '" & Trim(ddltraiff.SelectedItem.Text) & "','" & Convert.ToDateTime(Trim(txtGatePassDate.Text)).ToString("yyyyMMdd") & "','" & Trim(txtNocNo.Text & "") & "'"
            strSql += ",'" & Trim(txtigm.Text) & "','" & Trim(txtbondtype.Text) & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dblNetAmount = 0
                    strSql = ""
                    strSql += "SELECT Upper(AccountName) AccountName,GroupID FROM bond_AccountMaster WHERE AccountID=" & Val(ds.Tables(0).Rows(i)("AccountID")) & ""
                    dt5 = db.sub_GetDatatable(strSql)
                    'If txtbondinno.Text <> "" Then
                    If InStr(Trim(dt5.Rows(0)("AccountName")).ToUpper(), "STORAGE") > 0 Then
                        Call sub_CalculateStorage(ds.Tables(0).Rows(i)("AccountID"))
                    ElseIf InStr(Trim(dt5.Rows(0)("AccountName")).ToUpper(), "INSURANCE") > 0 Then
                        Call sub_CalcInsuranceAmount()
                    Else
                        Call sub_fetchcharges(ds.Tables(0).Rows(i)("AccountID"))
                    End If
                    'End If

                    If dblNetAmount <> 0 Then
                        strSql = ""
                        strSql += "SELECT AccountName,GroupID,round(" & dblNetAmount & ",0) as dblnetamount FROM bond_AccountMaster WHERE AccountID=" & Val(ds.Tables(0).Rows(i)("AccountID")) & ""
                        dt4 = db.sub_GetDatatable(strSql)

                        If dt4.Rows(0)("GroupID") = 1 Then
                            dblGroup1Amt = dblGroup1Amt + Val(dt4.Rows(0)("dblnetamount"))
                        Else
                            dblGroup2Amt = dblGroup2Amt + Val(dt4.Rows(0)("dblnetamount"))
                        End If
                        strSql = ""
                        strSql += "USP_insert_into_temp_Exassessment '" & Trim(ds.Tables(0).Rows(i)("AccountID")) & "','" & Val(dt4.Rows(0)("dblnetamount")) & "',0,0,'" & Session("UserId_BondCFS") & "'"
                        If InStr(Trim(dt5.Rows(0)("AccountName")).ToUpper(), "INSURANCE") > 0 Then
                            If Trim(lblinsvalidupto.Text) = "" Then
                                strSql += ",NULL,NULL"
                            Else
                                strSql += ",'" & Convert.ToDateTime(Trim(lblinsvalidupto.Text & "")).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(lblinsValidToDate.Text & "")).ToString("yyyy-MM-dd") & "'"
                            End If
                        Else
                            If Trim(lblfrom.Text) = "" Then
                                strSql += ",NULL,NULL"
                            Else
                                strSql += ",'" & Convert.ToDateTime(Trim(lblfrom.Text & "")).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(lblToDate.Text & "")).ToString("yyyy-MM-dd") & "'"
                            End If
                        End If
                        db.sub_ExecuteNonQuery(strSql)
                    End If
                Next
            End If
            Dim blnAccountFound As Boolean
            Dim lblrevalidupto As String = ""

            If ds.Tables(1).Rows.Count > 0 Then
                For i = 0 To ds.Tables(1).Rows.Count - 1
                    If ds.Tables(1).Rows(i)("amount") <> 0 Then
                        strSql = ""
                        strSql += "select * from Temp_ExAssessment where accountid='" & ds.Tables(1).Rows(i)("accountid") & "' and UserId='" & Session("UserId_BondCFS") & "'"
                        dt = db.sub_GetDatatable(strSql)
                        If dt.Rows.Count > 0 Then
                            blnAccountFound = True
                        End If
                        If blnAccountFound = False Then
                            If ds.Tables(2).Rows.Count > 0 Then
                                If Convert.ToDateTime(ds.Tables(2).Rows(0)("ValidUptoDate")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                                    lblrevalidupto = ds.Tables(2).Rows(0)("ValidUptoDate")
                                    StrPFrom = lblrevalidupto
                                    lblfrom.Text = StrPFrom
                                Else
                                    lblrevalidupto = DateAdd("d", 1, ds.Tables(2).Rows(0)("ValidUptoDate"))
                                    StrPFrom = lblrevalidupto
                                    lblfrom.Text = StrPFrom
                                End If
                            Else
                                If ds1.Tables(7).Rows.Count > 0 Then
                                    If Convert.ToDateTime(ds1.Tables(7).Rows(0)("StorageFrom")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                                        lblrevalidupto = ds1.Tables(7).Rows(0)("StorageFrom")
                                        StrPFrom = lblrevalidupto
                                        lblfrom.Text = StrPFrom
                                    Else
                                        lblrevalidupto = DateAdd("d", 1, ds1.Tables(7).Rows(0)("StorageFrom"))
                                        StrPFrom = lblrevalidupto
                                        lblfrom.Text = StrPFrom
                                    End If
                                Else
                                    lblrevalidupto = ""
                                    StrPFrom = lblrevalidupto
                                    lblfrom.Text = StrPFrom
                                End If
                            End If
                            If ds.Tables(3).Rows.Count > 0 Then
                                If Convert.ToDateTime(ds.Tables(3).Rows(0)("ValidUptoDate")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                                    lblrevalidupto = ds.Tables(3).Rows(0)("ValidUptoDate")
                                    StrPFrom = lblrevalidupto
                                    lblfrom.Text = StrPFrom
                                Else
                                    lblrevalidupto = DateAdd("d", 1, ds.Tables(3).Rows(0)("ValidUptoDate"))
                                    StrPFrom = lblrevalidupto
                                    lblfrom.Text = StrPFrom
                                End If
                            End If
                            Dim intdays As Double = 0, Intweeks As Double = 0
                            intdays = DateDiff("d", lblfrom.Text, txtGatePassDate.Text) + 1
                            If intDays Mod 7 = 0 Then
                                Intweeks = intDays / 7
                            Else
                                Intweeks = Int((intDays / 7)) + 1
                            End If
                            lblToDate.Text = Convert.ToDateTime(DateAdd("d", (Intweeks * 7 - 1), StrPFrom)).ToString("yyyy-MM-dd")
                            strSql = ""
                            strSql += "USP_insert_into_temp_Exassessment '" & Trim(ds.Tables(1).Rows(i)("accountid")) & "','" & ds.Tables(1).Rows(i)("amount") & "','" & ds.Tables(1).Rows(i)("IsSTax") & "',0,'" & Session("UserId_BondCFS") & "'"
                            If Trim(lblfrom.Text) = "" Then
                                strSql += ",NULL,NULL"
                            Else
                                strSql += ",'" & Convert.ToDateTime(Trim(lblfrom.Text & "")).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(lblToDate.Text & "")).ToString("yyyy-MM-dd") & "'"
                            End If
                            db.sub_ExecuteNonQuery(strSql)
                        End If
                        If ds.Tables(1).Rows(i)("IsSTax") = True Then
                            dblSTaxOnAmount = dblSTaxOnAmount + Val(ds.Tables(1).Rows(i)("amount"))
                        End If
                        dblGroup1Amt = dblGroup1Amt + Val(ds.Tables(1).Rows(i)("amount"))
                    End If
                Next

            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CalcTotals()
        Try
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0
            dbltotal = dblGroup1Amt + dblGroup2Amt
            dbltotalcgst = Format(dbltotal * (dblSGST / 100), "0.00")
            dbltotalsgst = Format(dbltotal * (dblCGST / 100), "0.00")
            dbltotaligst = Format(dbltotal * (dblIGST / 100), "0.00")
            strSql = ""
            strSql += "select CEILING(" & dbltotalcgst & ") as totalcgst,CEILING(" & dbltotalsgst & ") as totalsgst,CEILING(" & dbltotaligst & ") as totaligst"
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
            lblAllTotal.Text = Format(dblalltotal, "0")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_fetchcharges(strAccountID As String)
        Try
            Dim dblSQM As Double = 0, dblPerc As Double = 0, dblAmount As Double = 0, dblDestuffDays As Double = 0, dblIGMWeight As Double = 0, dblPaidAmount As Double = 0
            Dim blnSTax As Boolean = False
            Dim strAccountName As String = ""
            Dim dblDestuffDate As Date
            Dim intDays As Double, intTotalDays As Double = 0
            Dim Intweeks As Integer = 0
            Dim lblrevalidupto As String = ""
            Dim dblRWeight As Double = 0, dblRArea As Double = 0
            dblSQM = Val(lblArea.Text)

            dblNetAmount = 0
            dblPaidAmount = 0
            intDays = 0
            intTotalDays = DateDiff("d", Convert.ToDateTime(txtNocDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyy-MM-dd")) + 1
            Dim bondtype As String = ""
            dblRWeight = Math.Ceiling(Val(Val(txtgrosskgs.Text) / 1000) / 5)
            dblRWeight = dblRWeight * 5
            dblRArea = Math.Ceiling(Val(txtChargesArea.Text) / 5)
            dblRArea = dblRArea * 5
            strSql = ""
            strSql += "USP_calculation_fetchcharges_bondex '" & Trim(ddltraiff.SelectedItem.Text & "") & "','" & strAccountID & "','" & Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") & "','" & Trim(txtNocNo.Text) & "'"
            strSql += ",'" & Trim(txtigm.Text) & "','" & Trim(txtbondtype.Text) & "','" & Trim(txtdepositno.Text) & "'"
            ds1 = db.sub_GetDataSets(strSql)
            If strAccountID = 11 Then
                If ds1.Tables(5).Rows.Count > 0 Then
                    If Val(ds1.Tables(5).Rows(0)("InQty")) = Val(txtqty.Text) Then
                        If ds1.Tables(6).Rows.Count > 0 Then
                            If Val(ds1.Tables(6).Rows(0)("NetAmount")) <> 0 Then
                                dblNetAmount = Val(ds1.Tables(6).Rows(0)("NetAmount"))
                                GoTo lblnext
                            End If
                        End If
                    End If
                End If
            End If
            If ds1.Tables(0).Rows.Count > 0 Then
                intDays = DateDiff("d", txtNocDate.Text, txtGatePassDate.Text) + 1
                If intDays Mod 7 = 0 Then
                    Intweeks = intDays / 7
                Else
                    Intweeks = Int((intDays / 7)) + 1
                End If
                For i = 0 To ds1.Tables(0).Rows.Count - 1
                    If Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "S" Then
                        dblNetAmount = dblNetAmount + slab_CalcAmount(ds1.Tables(0).Rows(i)("SlabID"), intDays, 0, Val(txtgrosskgs.Text), Convert.ToDateTime(txtNocDate.Text).ToString("dd-MMM-yyyy HH:mm"), strAccountID, ds1.Tables(0).Rows(i)("Size"), intTotalDays)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "F" And ds1.Tables(1).Rows(i)("ConsiderArea") = False Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "20" And ds1.Tables(1).Rows(i)("ConsiderArea") = False Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txt20.Text)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "40" And ds1.Tables(1).Rows(i)("ConsiderArea") = False Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txt40.Text)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "45" And ds1.Tables(1).Rows(i)("ConsiderArea") = False Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txt40.Text)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "Q" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txtqty.Text)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "O" And ds1.Tables(0).Rows(i)("size") = "20" And Val(txt20.Text) > 0 Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * dblRWeight
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "O" And ds1.Tables(0).Rows(i)("size") = "40" And Val(txt40.Text) > 0 Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * dblRWeight
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "D" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(Val(txtgrosskgs.Text) / 1000) * intDays
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "V" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txtNoVehicles.Text)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "F" And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * dblRArea * Intweeks
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "20" And Val(txt20.Text) > 0 And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * dblRArea * Intweeks
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "40" And Val(txt40.Text) > 0 And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * dblRArea * Intweeks
                    End If
                    If ds1.Tables(0).Rows(i)("IsSTax") = True Then
                        dblSTaxOnAmount = dblSTaxOnAmount + dblNetAmount
                    End If
                Next
            End If
lblnext:
            If ds1.Tables(3).Rows.Count > 0 Then
                If Convert.ToDateTime(ds1.Tables(3).Rows(0)("ValidUptoDate")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                    lblrevalidupto = ds1.Tables(3).Rows(0)("ValidUptoDate")
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom
                Else
                    lblrevalidupto = DateAdd("d", 1, ds1.Tables(3).Rows(0)("ValidUptoDate"))
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom
                End If
            Else
                If ds1.Tables(7).Rows.Count > 0 Then
                    If Convert.ToDateTime(ds1.Tables(7).Rows(0)("StorageFrom")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                        lblrevalidupto = ds1.Tables(7).Rows(0)("StorageFrom")
                        StrPFrom = lblrevalidupto
                        lblfrom.Text = StrPFrom
                    Else
                        lblrevalidupto = DateAdd("d", 1, ds1.Tables(7).Rows(0)("StorageFrom"))
                        StrPFrom = lblrevalidupto
                        lblfrom.Text = StrPFrom
                    End If
                Else
                    lblrevalidupto = ""
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom
                End If
            End If
            If ds1.Tables(4).Rows.Count > 0 Then
                If Convert.ToDateTime(ds1.Tables(4).Rows(0)("ValidUptoDate")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                    lblrevalidupto = ds1.Tables(4).Rows(0)("ValidUptoDate")
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom

                Else
                    lblrevalidupto = DateAdd("d", 1, ds1.Tables(4).Rows(0)("ValidUptoDate"))
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom

                End If
            End If
            intDays = DateDiff("d", lblfrom.Text, txtGatePassDate.Text) + 1
            If intDays Mod 7 = 0 Then
                Intweeks = intDays / 7
            Else
                Intweeks = Int((intDays / 7)) + 1
            End If
            lblToDate.Text = Convert.ToDateTime(DateAdd("d", (Intweeks * 7 - 1), Trim(txtNocDate.Text))).ToString("yyyy-MM-dd")
            If ds1.Tables(2).Rows.Count > 0 Then
                dblPaidAmount = dblPaidAmount + Val(ds1.Tables(2).Rows(0)(0))
            End If
            If ds1.Tables(8).Rows.Count > 0 Then
                dblPaidAmount = dblPaidAmount + Val(ds1.Tables(8).Rows(0)(0))
            End If
            If dblNetAmount - dblPaidAmount > 0 Then
                dblNetAmount = dblNetAmount - dblPaidAmount
            Else
                dblNetAmount = 0
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CalculateStorage(strAccountID As String)
        Try
            Dim dblSQM As Double = 0, dblPerc As Double = 0, dblAmount As Double = 0, dblDestuffDays As Double = 0, dblIGMWeight As Double = 0, dblPaidAmount As Double = 0
            Dim blnSTax As Boolean = False
            Dim strAccountName As String = "", varDate As String = ""
            Dim dblDestuffDate As Date
            Dim intDays As Double, dblrate As Double, intNOCadvance As Double, intTotalDays As Double = 0
            Dim Intweeks As Integer = 0
            Dim lblrevalidupto As String = "", lblprvaliddate As String = ""
            dblSQM = Val(lblArea.Text)
            Dim weeks As Double = 0, intCount As Integer, intCalcDays As Integer, intPrvDays As Integer
            dblNetAmount = 0
            dblPaidAmount = 0
            intDays = 0
            intCount = 1
            intPrvDays = 0
            Dim bondtype As String = ""
            intTotalDays = DateDiff("d", Convert.ToDateTime(txtNocDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyy-MM-dd")) + 1

            strSql = ""
            strSql += "USP_calculation_storagecharges_bondexassessment '" & Trim(ddltraiff.SelectedItem.Text & "") & "','" & strAccountID & "','" & Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") & "','" & Trim(txtNocNo.Text) & "'"
            strSql += ",'" & Trim(txtigm.Text) & "','" & Trim(txtbondtype.Text) & "','" & Trim(txtdepositno.Text) & "'"
            ds1 = db.sub_GetDataSets(strSql)
            If ds1.Tables(2).Rows.Count > 0 Then
                If Convert.ToDateTime(ds1.Tables(2).Rows(0)("ValidUptoDate")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                    lblrevalidupto = ds1.Tables(2).Rows(0)("ValidUptoDate")
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom
                Else
                    lblrevalidupto = DateAdd("d", 1, ds1.Tables(2).Rows(0)("ValidUptoDate"))
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom
                End If
            Else
                If ds1.Tables(7).Rows.Count > 0 Then
                    If Convert.ToDateTime(ds1.Tables(7).Rows(0)("StorageFrom")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                        lblrevalidupto = ds1.Tables(7).Rows(0)("StorageFrom")
                        StrPFrom = lblrevalidupto
                        lblfrom.Text = StrPFrom
                    Else
                        lblrevalidupto = DateAdd("d", 1, ds1.Tables(7).Rows(0)("StorageFrom"))
                        StrPFrom = lblrevalidupto
                        lblfrom.Text = StrPFrom
                    End If
                Else
                    lblrevalidupto = ""
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom
                End If

            End If
            If ds1.Tables(3).Rows.Count > 0 Then
                If Convert.ToDateTime(ds1.Tables(3).Rows(0)("ValidUptoDate")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                    lblrevalidupto = ds1.Tables(3).Rows(0)("ValidUptoDate")
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom
                Else
                    lblrevalidupto = DateAdd("d", 1, ds1.Tables(3).Rows(0)("ValidUptoDate"))
                    StrPFrom = lblrevalidupto
                    lblfrom.Text = StrPFrom
                End If
            End If
            'lblfrom.Text = "2019-10-22"
            'StrPFrom = "2019-10-22"
            Dim date1 As String = Convert.ToDateTime(StrPFrom).ToString("yyyyMMdd")
            Dim date2 As String = Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd")
            If Convert.ToDateTime(StrPFrom).ToString("yyyyMMdd") > Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                txtvalidDate.Text = Convert.ToDateTime(StrPFrom).ToString("yyyy-MM-dd")
                Exit Sub
            End If
            intDays = DateDiff("d", StrPFrom, txtGatePassDate.Text) + 1
            If intDays Mod 7 = 0 Then
                weeks = intDays / 7
            Else
                weeks = Int(intDays / 7) + 1
            End If
            txtvalidDate.Text = Convert.ToDateTime(DateAdd("d", (weeks * 7 - 1), StrPFrom)).ToString("yyyy-MM-dd")
            If lblrevalidupto <> "" Then
                intDays = DateDiff("d", StrPFrom, txtGatePassDate.Text) + 1
                If intDays Mod 7 = 0 Then
                    Intweeks = intDays / 7
                Else
                    Intweeks = Int(intDays / 7) + 1
                End If
                txtvalidDate.Text = Convert.ToDateTime(DateAdd("d", (Intweeks * 7 - 1), StrPFrom)).ToString("yyyy-MM-dd")
                If intDays Mod 7 = 0 Then
                    weeks = intDays / 7
                Else
                    weeks = Int(intDays / 7) + 1
                End If
            End If
            strSql = ""
            strSql += "SELECT SorF,BondType, SlabID, FixedAmt, IsSTax, effectiveFrom, effectiveUpto,Size FROM bond_tariffdetails WHERE TariffID='" & Trim(ddltraiff.SelectedItem.Text) & "' and iscancel=0   "
            strSql += " AND AccountID='" & strAccountID & "' AND BondTYpe='BondEx' AND Effectiveupto>'" & Convert.ToDateTime(StrPFrom).ToString("yyyyMMdd") & "' AND Effectivefrom <='" & Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") & "' ORDER BY effectivefrom"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    strSql = ""
                    strSql += "select * From bond_tariffdetails where BONDTYpe='BondEx' AND EffectiveFrom ='" & dt.Rows(0)("effectiveFrom") & "' AND effectiveupto ='" & dt.Rows(0)("effectiveupto") & "' AND TariffID='" & Trim(ddltraiff.SelectedItem.Text) & "' AND AccountID='" & strAccountID & "' and iscancel=0 "
                    dt1 = db.sub_GetDatatable(strSql)
                    If dt1.Rows.Count > 0 Then
                        If dt.Rows(0)("effectiveupto") <= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                            varDate = Right(dt.Rows(0)("effectiveupto"), 2) & "-" & Mid(dt.Rows(0)("effectiveupto"), 5, 2) & "-" & Left(dt.Rows(0)("effectiveupto"), 4)
                            intDays = DateDiff("d", Convert.ToDateTime(StrPFrom).ToString("dd-MMM-yyyy"), Convert.ToDateTime(varDate).ToString("dd-MMM-yyyy")) + 1
                            Intweeks = Math.Ceiling(intDays / 7)
                            lblprvaliddate = Convert.ToDateTime(DateAdd("d", (Intweeks * 7 - 1), StrPFrom)).ToString("dd-MMM-yyyy")
                            If Convert.ToDateTime(lblprvaliddate).ToString("yyyyMMdd") > Convert.ToDateTime(varDate).ToString("yyyyMMdd") Then
                                lblprvaliddate = Convert.ToDateTime(DateAdd("d", (Intweeks * 7 - 8), StrPFrom)).ToString("dd-MMM-yyyy")
                                intDays = DateDiff("d", Convert.ToDateTime(StrPFrom).ToString("dd-MMM-yyyy"), lblprvaliddate) + 1
                            End If
                        Else
                            intDays = DateDiff("d", StrPFrom, txtGatePassDate.Text) + 1
                        End If
                        If intCount = 1 Then
                            intCalcDays = intDays
                        Else
                            intCalcDays = intDays - intPrvDays
                        End If
                        intPrvDays = intPrvDays + intCalcDays
                        If intCalcDays Mod 7 = 0 Then
                            Intweeks = intCalcDays / 7
                        Else
                            Intweeks = Int(intCalcDays / 7) + 1
                        End If
                    End If
                    If Trim(dt.Rows(i)("SorF")) = "S" Then
                        dblNetAmount = dblNetAmount + slab_CalcAmount(dt.Rows(i)("SlabID"), intDays, 0, Val(txtgrosskgs.Text), Convert.ToDateTime(txtNocDate.Text).ToString("dd-MMM-yyyy HH:mm"), strAccountID, Trim(dt.Rows(i)("Size")), intTotalDays)
                    ElseIf Trim(dt.Rows(i)("SorF")) = "D" Then
                        Dim intdays1 As Integer = 0
                        intdays1 = DateDiff("d", txtNocDate.Text, txtGatePassDate.Text) + 1
                        dblNetAmount = dblNetAmount + intdays1 * Val(dt.Rows(i)("FixedAmt")) * (Val(txtgrosskgs.Text) / 1000)
                    ElseIf Trim(dt.Rows(i)("SorF")) = "C" Then
                        dblNetAmount = dblNetAmount + Intweeks * Val(dt.Rows(i)("FixedAmt"))
                    Else
                        If ds1.Tables(1).Rows.Count > 0 Then
                            If ds1.Tables(1).Rows(0)("ConsiderArea") = True Then
                                dblNetAmount = dblNetAmount + (Intweeks * txtChargesArea.Text * Val(dt.Rows(i)("FixedAmt")))
                                dblrate = Val(dt.Rows(i)("FixedAmt"))
                                'txtChargesArea.Text = ""
                                If dt.Rows(i)("IsSTax") = True Then
                                    dblSTaxOnAmount = dblSTaxOnAmount + Val(dt.Rows(i)("FixedAmt"))
                                End If
                            End If
                        End If
                    End If

                    intCount = intCount + 1
                Next
                lblToDate.Text = txtvalidDate.Text
                intNOCadvance = 0
                If blnStorageWeek = True Then
                    If ds1.Tables(5).Rows.Count > 0 Then
                        If ds1.Tables(5).Rows(0)(0) = 0 Then
                            intNOCadvance = 0
                        Else
                            intNOCadvance = Val(ds1.Tables(5).Rows(0)(0))
                        End If
                    End If
                    If ds1.Tables(6).Rows.Count > 0 Then
                        If ds1.Tables(6).Rows(0)(0) = 0 Then
                            intNOCadvance += 0
                        Else
                            intNOCadvance += Val(ds1.Tables(6).Rows(0)(0))
                        End If
                    End If
                End If
                If ds1.Tables(8).Rows.Count > 0 Then
                    If ds1.Tables(8).Rows(0)(0) = 0 Then
                        intNOCadvance += 0
                    Else
                        intNOCadvance += Val(ds1.Tables(8).Rows(0)(0))
                    End If
                End If
                dblNetAmount = dblNetAmount - intNOCadvance
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Function slab_CalcAmount(slabID As Integer, DaysValue As Double, percentage As Double, Weight As Double, InDate As Date, AccountID As String, size As Integer, TotalDaysValue As Double) As Double
        Try
            Dim dblSlabAmount As Double = 0, dblWeekValue As Double = 0, dbldtvaliddate As Double = 0, dblAddSecs As Double = 0, dblHrs As Double = 0, dblActualHrs As Double = 0, dblAmt As Double = 0, dblTotDays As Double = 0, dblLeftHrs As Double = 0
            Dim dtValidDate As Date, dblclcamt As Double = 0
            Dim intValidCounter As Integer = 0

            If DaysValue Mod 7 = 0 Then
                dblWeekValue = DaysValue / 7
            Else
                dblWeekValue = Int(DaysValue / 7) + 1
            End If
            dblHrs = DateDiff("s", Trim(Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm") & ""), Trim(Convert.ToDateTime(txtvalidDate.Text).ToString("dd-MMM-yyyy HH:mm") & ""))
            dblActualHrs = DateDiff("s", Trim(Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm") & ""), Trim(Convert.ToDateTime(txtvalidDate.Text).ToString("dd-MMM-yyyy HH:mm") & "")) 'DateDiff("s", Format(InDate, "dd-MMM-yyyy HH:mm"), Format(txtvaldate.Text, "dd-MMM-yyyy HH:mm"))
            dblHrs = (dblHrs / 60) / 60
            dblActualHrs = (dblActualHrs / 60) / 60

            dblTotDays = Int(dblActualHrs / 24)
            If dblHrs > 24 Then
                dblLeftHrs = dblActualHrs - (dblHrs * 24)
            Else
                dblLeftHrs = dblActualHrs
            End If
            strSql = ""
            strSql += " USP_cal_STORAGE_SLABS " & slabID & "," & percentage & "," & dblWeekValue & "," & Weight & "," & dblTotDays & "," & dblLeftHrs & ""
            ds3 = db.sub_GetDataSets(strSql)
            If ds3.Tables(0).Rows.Count > 0 Then
                If Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Days" Then
                    For i = 0 To ds3.Tables(0).Rows.Count - 1
                        If Trim(ds3.Tables(0).Rows(i)("FromSlab")) <= DaysValue Then
                            If Trim(ds3.Tables(0).Rows(i)("ToSlab")) < DaysValue Then
                                dblSlabAmount = dblSlabAmount + (Val(ds3.Tables(0).Rows(i)("ToSlab")) - Val(ds3.Tables(0).Rows(i)("FromSlab")) + 1) * Val(ds3.Tables(0).Rows(i)("Value"))
                            Else
                                dblSlabAmount = dblSlabAmount + (Val(DaysValue) - Val(ds3.Tables(0).Rows(i)("FromSlab")) + 1) * Val(ds3.Tables(0).Rows(i)("Value"))
                            End If
                        End If
                    Next
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount

                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Weeks" Then
                    If TotalDaysValue Mod 7 = 0 Then
                        dblWeekValue = TotalDaysValue / 7
                    Else
                        dblWeekValue = Int(TotalDaysValue / 7) + 1
                    End If
                    If size = 20 Then
                        blnStorageWeek = True
                        For i = 0 To ds3.Tables(0).Rows.Count - 1
                            If Trim(ds3.Tables(0).Rows(i)("FromSlab")) <= dblWeekValue Then
                                If Trim(ds3.Tables(0).Rows(i)("ToSlab")) <= dblWeekValue Then
                                    'dblSlabAmount = dblWeekValue * Val(ds3.Tables(0).Rows(i)("Value")) * Val(txt20.Text)
                                    dblSlabAmount += Val(ds3.Tables(0).Rows(i)("Value")) * Val(txt20.Text)

                                End If
                            End If
                        Next
                    ElseIf size = 40 Then
                        blnStorageWeek = True
                        For i = 0 To ds3.Tables(0).Rows.Count - 1
                            If Trim(ds3.Tables(0).Rows(i)("FromSlab")) <= dblWeekValue Then
                                If Trim(ds3.Tables(0).Rows(i)("ToSlab")) <= dblWeekValue Then
                                    'dblSlabAmount = dblWeekValue * Val(ds3.Tables(2).Rows(i)("Value")) * Val(txt40.Text)
                                    dblSlabAmount += Val(ds3.Tables(2).Rows(i)("Value")) * Val(txt40.Text)

                                End If
                            End If
                        Next
                    End If

                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount

                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Percentage" Then

                    If ds3.Tables(1).Rows.Count > 0 Then
                        dblSlabAmount = Val(ds3.Tables(1).Rows(0)("Value"))
                    End If
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount
                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Weight" Then

                    'For i = 0 To ds3.Tables(0).Rows.Count - 1
                    '    If Trim(ds3.Tables(0).Rows(i)("FromSlab")) <= Weight Then
                    '        If Trim(ds3.Tables(0).Rows(i)("ToSlab")) < Weight Then
                    '            dblSlabAmount = dblSlabAmount + (Val(ds3.Tables(0).Rows(i)("ToSlab")) - Val(ds3.Tables(0).Rows(i)("FromSlab")) + 1) * Val(ds3.Tables(0).Rows(i)("Value"))
                    '        Else
                    '            dblSlabAmount = dblSlabAmount + (Val(Weight) - Val(ds3.Tables(0).Rows(i)("FromSlab")) + 1) * Val(ds3.Tables(0).Rows(i)("Value"))
                    '        End If
                    '    End If
                    'Next
                    Dim Charges As Integer = 0
                    Dim strImpSlab As String = ""
                    Dim dtImpSlab As New DataTable
                    Dim dtslabid As New DataTable
                    'strSql = ""
                    'strSql += "select * from Temp_Assessment where accountid in (select * from bond_tariffdetails where ChargesBased in (11,12) and bondtype='Noc' and tariffID='" & Trim(ddltraiff.SelectedItem.Text & "") & "')"
                    'dt = db.sub_GetDatatable(strSql)
                    If dblNetAmount = 0 Then
                        If (txt20.Text <> "0" And txt40.Text = "0") Or (txt20.Text = "0" And txt40.Text <> "0") Then
                            Charges = 11
                            If txt20.Text <> "0" Then
                                strSql = ""
                                strSql += "select slabID from bond_tariffdetails where accountID='" & AccountID & "' and chargesbased=" & Val(Charges) & " and Size=20 and bondtype='BondEx' and tariffID='" & Trim(ddltraiff.SelectedItem.Text & "") & "' and iscancel='0' order by entryid desc "
                                dtslabid = db.sub_GetDatatable(strSql)
                                If dtslabid.Rows.Count > 0 Then
                                    slabID = Val(dtslabid.Rows(0)("slabID"))
                                End If
                                strImpSlab = ""
                                strImpSlab += "SELECT * FROM bond_slabs WHERE SlabID=" & slabID & " and " & Weight & " BETWEEN FromSlab and ToSlab ORDER BY FromSlab"
                                dtImpSlab = db.sub_GetDatatable(strImpSlab)
                                If dtImpSlab.Rows.Count > 0 Then
                                    dblSlabAmount = Val(dtImpSlab.Rows(0)("Value"))
                                End If
                            ElseIf txt40.Text <> "0" Then
                                strSql = ""
                                strSql += "select slabID from bond_tariffdetails where accountID='" & AccountID & "' and chargesbased=" & Val(Charges) & " and Size=40 and bondtype='BondEx'"
                                dtslabid = db.sub_GetDatatable(strSql)
                                If dtslabid.Rows.Count > 0 Then
                                    slabID = Val(dtslabid.Rows(0)("slabID"))
                                End If
                                strImpSlab = ""
                                strImpSlab += "SELECT * FROM bond_slabs WHERE SlabID=" & slabID & " and " & Weight & " BETWEEN FromSlab and ToSlab ORDER BY FromSlab"
                                dtImpSlab = db.sub_GetDatatable(strImpSlab)
                                If dtImpSlab.Rows.Count > 0 Then
                                    dblSlabAmount = Val(dtImpSlab.Rows(0)("Value"))
                                End If
                            End If

                        ElseIf (txt20.Text <> "0" And txt40.Text <> "0") Then
                            Charges = 12
                            strSql = ""
                            strSql += "select slabID from bond_tariffdetails where accountID='" & AccountID & "' and chargesbased=" & Val(Charges) & " and Size=0 and bondtype='BondEx'"
                            dtslabid = db.sub_GetDatatable(strSql)
                            If dtslabid.Rows.Count > 0 Then
                                slabID = Val(dtslabid.Rows(0)("slabID"))
                            End If
                            strImpSlab = ""
                            strImpSlab += "SELECT * FROM bond_slabs WHERE SlabID=" & slabID & " and " & Weight & " BETWEEN FromSlab and ToSlab ORDER BY FromSlab"
                            dtImpSlab = db.sub_GetDatatable(strImpSlab)
                            If dtImpSlab.Rows.Count > 0 Then
                                dblSlabAmount = Val(dtImpSlab.Rows(0)("Value"))
                            End If
                        End If
                    End If
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount
                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Hours" Then
                    dtValidDate = Trim(Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(InDate, "dd-MMM-yyyy HH:mm")
                    txtvalidDate.Text = Trim(Convert.ToDateTime(DateAdd("s", 359 * 60, Now)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", 359 * 60, Now), "dd-MMM-yyyy HH:mm")
                    dbldtvaliddate = Trim(Convert.ToDateTime(dtValidDate).ToString("yyyyMMddHHmm") & "") 'Format(dtValidDate, "yyyyMMddHHmm")
                    While dbldtvaliddate <= Trim(Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & "") 'Format(Now, "yyyyMMddHHmm")
                        If intValidCounter = 0 Then
                            dblAddSecs = Val(360) * Val(60)
                            dtValidDate = Trim(Convert.ToDateTime(DateAdd("s", dblAddSecs, dtValidDate)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", dblAddSecs, dtValidDate), "dd-MMM-yyyy HH:mm")
                            intValidCounter = intValidCounter + 1
                        ElseIf intValidCounter = 1 Then
                            dblAddSecs = Val(360) * Val(60)
                            dtValidDate = Trim(Convert.ToDateTime(DateAdd("s", dblAddSecs, dtValidDate)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", dblAddSecs, dtValidDate), "dd-MMM-yyyy HH:mm")
                            intValidCounter = intValidCounter + 1
                        ElseIf intValidCounter = 2 Then
                            dblAddSecs = Val(720) * Val(60)
                            dtValidDate = Trim(Convert.ToDateTime(DateAdd("s", dblAddSecs, dtValidDate)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", dblAddSecs, dtValidDate), "dd-MMM-yyyy HH:mm")
                            intValidCounter = 0
                        End If
                        dbldtvaliddate = Trim(Convert.ToDateTime(dtValidDate).ToString("yyyyMMddHHmm") & "") 'Format(dtValidDate, "yyyyMMddHHmm")
                        dbldtvaliddate += 1
                    End While
                    dtValidDate = Trim(Convert.ToDateTime(DateAdd("s", -60, dtValidDate)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", -60, dtValidDate), "dd-MMM-yyyy HH:mm")



                    If ds3.Tables(5).Rows.Count > 0 Then
                        dblSlabAmount = Val(ds3.Tables(5).Rows(0)("Value")) * dblTotDays
                    End If
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount

                    If ds3.Tables(6).Rows.Count > 0 Then
                        dblSlabAmount = Val(ds3.Tables(6).Rows(0)("Value"))
                    End If
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount
                End If

            End If
            Return slab_CalcAmount
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Function
    Private Sub sub_CalcInsuranceAmount()
        Try
            Dim dblInsAmount As Double = 0, dblInsAmountPaid As Double = 0, dblInsRate As Double = 0, dtRecordDate As Date, lblreinsvupto As String, strbondindate As String
            Dim blnSTax As Boolean = False
            Dim intTotalIDays As Integer = 0, intTotalWeek As Integer = 0, varDate As String = ""
            Dim lblinsvalidup As Date, lblprvaliddate As String
            Dim dblDestuffDate As Date
            Dim intDays As Integer
            Dim dbltemp As Double = 0, weeks As Double, Intweeks As Double
            Dim intCount As Integer
            Dim intCalcDays As Integer
            Dim intPrvDays As Integer, dblrate As Double

            dblNetAmount = 0
            intCount = 1
            intPrvDays = 0
            If Val(txtvalduty.Text) <> 0 Then
                intTotalIDays = DateDiff("d", Convert.ToDateTime(txtNocDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtvalidDate.Text).ToString("yyyy-MM-dd")) + 1
            End If

            strSql = ""
            strSql += "usp_CalcInsurance_BondExAssessment'" & Trim(ddltraiff.SelectedItem.Text & "") & "','" & Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") & "','" & Trim(txtNocNo.Text) & "',"
            strSql += "'" & Trim(txtigm.Text & "") & "','" & Trim(txtbondtype.Text) & "','" & Trim(txtdepositno.Text) & "'"
            ds2 = db.sub_GetDataSets(strSql)
            If ds2.Tables(2).Rows.Count > 0 Then
                If Convert.ToDateTime(ds2.Tables(2).Rows(0)("insuValidUptoDate")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtvalidDate.Text).ToString("yyyyMMdd") Then
                    lblreinsvupto = ds2.Tables(2).Rows(0)("insuValidUptoDate")
                    strbondindate = lblreinsvupto
                Else
                    lblreinsvupto = DateAdd("d", 1, ds2.Tables(2).Rows(0)("insuValidUptoDate"))
                    strbondindate = lblreinsvupto
                End If
            Else
                If ds2.Tables(4).Rows.Count > 0 Then
                    If Convert.ToDateTime(ds2.Tables(4).Rows(0)("InsuranceFrom")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtvalidDate.Text).ToString("yyyyMMdd") Then
                        lblreinsvupto = DateAdd("d", 1, ds2.Tables(4).Rows(0)("InsuranceFrom"))
                        strbondindate = lblreinsvupto
                    Else
                        lblreinsvupto = DateAdd("d", 1, ds2.Tables(4).Rows(0)("InsuranceFrom"))
                        strbondindate = lblreinsvupto
                    End If
                Else
                    lblreinsvupto = ""
                    strbondindate = lblreinsvupto
                End If
            End If
            If ds2.Tables(3).Rows.Count > 0 Then
                If Convert.ToDateTime(ds2.Tables(3).Rows(0)("insuValidUptoDate")).ToString("yyyyMMdd") >= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                    lblreinsvupto = DateAdd("d", 1, ds2.Tables(3).Rows(0)("insuValidUptoDate"))
                    strbondindate = lblreinsvupto
                    'lblfrom.Text = StrPFrom
                Else
                    lblreinsvupto = DateAdd("d", 1, ds2.Tables(3).Rows(0)("insuValidUptoDate"))
                    strbondindate = lblreinsvupto
                    'lblfrom.Text = StrPFrom
                End If
            End If
            'lblinsvalidupto.Text = "2019-10-22"
            'strbondindate = "2019-10-22"
            If Convert.ToDateTime(strbondindate).ToString("yyyyMMdd") > Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                lblinsvalidup = Convert.ToDateTime(strbondindate).ToString("dd-MMM-yyyy")
                lblinsvalidupto.Text = strbondindate
                Exit Sub
            End If
            If intTotalIDays Mod 7 = 0 Then
                intTotalWeek = intTotalIDays / 7
                'lblinsvalidup = Format(DateAdd("d", (intTotalWeek * 7), dtpnocdate - 1), "dd-MMM-yyyy")
                If strbondindate <> "" Then
                    lblinsvalidup = Convert.ToDateTime(DateAdd("d", (intTotalWeek * 7), DateAdd("d", -1, Convert.ToDateTime(txtNocDate.Text).ToString("yyyy-MM-dd"))).ToString("dd-MM-yyyy"))
                    lblinsvalidupto.Text = strbondindate
                End If
            Else
                intTotalWeek = Int(intTotalIDays / 7) + 1
                'lblinsvalidup = Format(DateAdd("d", (intTotalWeek * 7), dtpnocdate - 1), "dd-MMM-yyyy")
                lblinsvalidup = Convert.ToDateTime(DateAdd("d", (intTotalWeek * 7), DateAdd("d", -1, Convert.ToDateTime(txtNocDate.Text).ToString("yyyy-MM-dd"))).ToString("dd-MM-yyyy"))
                lblinsvalidupto.Text = strbondindate
            End If

            If ds2.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds2.Tables(0).Rows.Count - 1
                    strSql = ""
                    strSql += "select * From bond_tariffdetails where BondType='BondEx' and iscancel=0  AND EffectiveFrom ='" & ds2.Tables(0).Rows(i)("effectiveFrom") & "' AND effectiveupto ='" & ds2.Tables(0).Rows(i)("effectiveupto") & "' AND TariffID='" & Trim(ddltraiff.SelectedItem.Text) & "' AND AccountID in (select AccountID from bond_accountmaster where AccountName like '%Insurance%')"
                    dt = db.sub_GetDatatable(strSql)
                    If dt.Rows.Count > 0 Then
                        If ds2.Tables(0).Rows(i)("effectiveupto") <= Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyyMMdd") Then
                            varDate = Right(ds2.Tables(0).Rows(i)("effectiveupto"), 2) & "-" & Mid(ds2.Tables(0).Rows(i)("effectiveupto"), 5, 2) & "-" & Left(ds2.Tables(0).Rows(i)("effectiveupto"), 4)
                            intDays = DateDiff("d", Convert.ToDateTime(strbondindate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(varDate).ToString("dd-MMM-yyyy")) + 1
                            Intweeks = Math.Ceiling(intDays / 7)
                            lblprvaliddate = Convert.ToDateTime(DateAdd("d", (Intweeks * 7 - 1), strbondindate)).ToString("dd-MMM-yyyy")
                            If Convert.ToDateTime(lblprvaliddate).ToString("yyyyMMdd") > Convert.ToDateTime(varDate).ToString("yyyyMMdd") Then
                                lblprvaliddate = Convert.ToDateTime(DateAdd("d", (Intweeks * 7 - 8), strbondindate)).ToString("dd-MMM-yyyy")
                            End If
                            intDays = DateDiff("d", Convert.ToDateTime(strbondindate).ToString("dd-MMM-yyyy"), lblprvaliddate) + 1
                        Else
                            intDays = DateDiff("d", strbondindate, txtGatePassDate.Text) + 1
                        End If
                        If intCount = 1 Then
                            intCalcDays = intDays
                        Else
                            intCalcDays = intDays - intPrvDays
                        End If
                        intPrvDays = intPrvDays + intCalcDays
                        Intweeks = Math.Ceiling(intCalcDays / 7)
                        If Trim(ds2.Tables(0).Rows(i)("SorF") & "") = "F" Then
                            dblNetAmount = Val(txtvalduty.Text) * Val(dt.Rows(0)("FixedAmt") / 1000) * Intweeks
                        ElseIf Trim(ds2.Tables(0).Rows(i)("SorF") & "") = "W" Then
                            dblNetAmount += Val(dt.Rows(i)("FixedAmt")) * Intweeks
                        End If
                        lblinsValidToDate.Text = Convert.ToDateTime(DateAdd("d", (Intweeks * 7 - 1), strbondindate)).ToString("yyyy-MM-dd")
                        dblNetAmount = Format(dblNetAmount, "0.00")
                        dblrate = Val(dt.Rows(0)("InsRate"))
                        dblInsAmount = dblNetAmount
                        If dblInsAmount < 0 Then
                            dblInsAmount = 0
                        End If
                    End If
                    intCount = intCount + 1
                Next
            End If
            If dblNetAmount < 1 Then
                dblNetAmount = Math.Ceiling(dblNetAmount)
            End If
            dblInsAmount = Format(dblNetAmount, "0.00")
            'dblGroup1Amt = dblGroup1Amt + dblInsAmount
            'If dblInsAmount <> 0 Then
            '    strSql = ""
            '    strSql += "USP_insert_into_temp_assessment '" & Trim(ds.Tables(1).Rows(0)("accountid")) & "','" & dblInsAmount & "','1',0,'" & Session("UserId_BondCFS") & "'"
            '    db.sub_ExecuteNonQuery(strSql)
            'End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            Dim strWorkyear As String = ""
            Dim InvoiceDate As Date = Trim(txtAssessmentDate.Text)
            If InvoiceDate.Month < 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") - 1 & "-" & Format(InvoiceDate, "yy")
            ElseIf InvoiceDate.Month >= 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") & "-" & Format(InvoiceDate, "yy") + 1
            End If
            Dim count As Double = 0, dblassessno As Double = 0
            Dim dblSumSGSTAmt As Double = 0, dblSumNetAmtTotal As Double = 0, dblSumCGSTAmt As Double = 0, dblSumIGSTAmt As Double = 0, dblgrandtotal As Double = 0

            For Each row In rptIndentLIst.Rows
                count += 1
            Next
            If Not count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No details selected for assessment');", True)
                btncal.Focus()
                Exit Sub
            End If

            Call Sub_SGTRate()
            strSql = ""
            strSql += "USP_save_part_for_noc_assessment_Ex '" & Trim(ddltraiff.SelectedItem.Text) & "','" & strWorkyear & "','" & Trim(txtNocNo.Text) & "'"
            ds = db.sub_GetDataSets(strSql)
            If Not ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Tariff ID: " & Trim(ddltraiff.SelectedItem.Text) & " not found in database. Please contact your administrator!');", True)
                Exit Sub
            End If
            If ds.Tables(2).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invoice No " & ds.Tables(2).Rows(0)(0) & " already generated. Cannot proceed!');", True)
                Exit Sub
            End If
            If ds.Tables(1).Rows(0)(0) = 0 Then
                dblassessno = 1
            Else
                dblassessno = Val(ds.Tables(1).Rows(0)(0)) + 1
            End If
            'lblfrom.Text = "2019-10-11"
            For Each row In rptIndentLIst.Rows
                strSql = ""
                strSql += "USP_insert_into_bond_assessd_EX '" & dblassessno & "','" & strWorkyear & "','" & Val(CType(row.FindControl("lblaccntid"), Label).Text) & "',"
                strSql += "'" & Val(CType(row.FindControl("lblntamnt"), Label).Text) & "','" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblSGST / 100), "0.00") & "',"
                strSql += "'" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblCGST / 100), "0.00") & "','" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblIGST / 100), "0.00") & "',"
                strSql += "'" & dbltaxgroupid & "','" & Trim(txtNocNo.Text) & "'"
                If Trim(CType(row.FindControl("lblFromDate"), Label).Text) = "" Then
                    strSql += ",NULL"
                Else
                    strSql += ",'" & Convert.ToDateTime(Trim(CType(row.FindControl("lblFromDate"), Label).Text)).ToString("yyyy-MM-dd") & "'"
                End If
                If Trim(CType(row.FindControl("lblToDate"), Label).Text) = "" Then
                    strSql += ",NULL,"
                Else
                    strSql += ",'" & Convert.ToDateTime(Trim(CType(row.FindControl("lblToDate"), Label).Text)).ToString("yyyy-MM-dd") & "',"
                End If
                strSql += "'" & Trim(txtChargesArea.Text & "") & "','" & Trim(txtvalue.Text & "") & "','" & Trim(txtduty.Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
lblnext:
            Next
            strSql = ""
            strSql += "get_sum_charges_bond '" & dblassessno & "', '" & strWorkyear & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                dblSumSGSTAmt = Val(dt.Rows(0)("SGST"))
                dblSumCGSTAmt = Val(dt.Rows(0)("CGST"))
                dblSumIGSTAmt = Val(dt.Rows(0)("IGST"))
                dblSumNetAmtTotal = Val(dt.Rows(0)("Amount"))
                dblgrandtotal = Val(dblSumSGSTAmt) + Val(dblSumCGSTAmt) + Val(dblSumIGSTAmt) + Val(dblSumNetAmtTotal)
            End If
            Dim lblinsvalidupto1 As String = ""
            If Trim(lblinsValidToDate.Text & "") = "" Then
                lblinsvalidupto1 = Convert.ToDateTime(txtvalidDate.Text).ToString("yyyy-MM-dd")
            Else
                lblinsvalidupto1 = Convert.ToDateTime(lblinsValidToDate.Text).ToString("yyyy-MM-dd")
            End If

            strSql = ""
            strSql += "USP_insert_into_bond_assessm_Ex '" & dblassessno & "','" & strWorkyear & "','" & lblchaid.Text & "','" & lblcustid.Text & "','" & lblimporterid.Text & "',"
            strSql += "'" & Convert.ToDateTime(txtAssessmentDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txtvalidDate.Text).ToString("yyyy-MM-dd") & "','" & lblinsvalidupto1 & "',"
            strSql += "'" & Trim(txtNocNo.Text) & "','" & Trim(txtbondtype.Text) & "','" & Trim(txtboeno.Text) & "','" & Trim(ddltraiff.SelectedItem.Text) & "','" & Trim(txtigm.Text) & "',"
            strSql += "'" & Trim(txtcustaddress.Text) & "','" & Trim(txt20.Text) & "','" & Trim(txt40.Text) & "','" & dblSumNetAmtTotal & "','" & dblSumNetAmtTotal & "','" & Session("UserId_BondCFS") & "',"
            strSql += "'" & dblSumSGSTAmt & "','" & dblSumCGSTAmt & "','" & dblSumIGSTAmt & "','" & dblgrandtotal & "','" & lblpartyid.Text & "','" & Trim(txtdepositno.Text) & "','" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "','" & Trim(txtItemNo.Text) & "','" & Trim(ddlCommodity.SelectedValue & "") & "','" & Trim(ddltxtTax.SelectedValue & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            txtassessno.Text = dblassessno
            txtworkyear.Text = strWorkyear
            Clear()

            txtdepositno.Focus()
            txtdepositno.Text = ""
            btnsave.Text = "Save"
            btnsave.Attributes.Add("Class", "btn btn-success btn-sm outline pull-right")
            lblSession.Text = "Record Saved successfully for Assess NO " & dblassessno & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            btnsave.Text = "Save"
            btnsave.Attributes.Add("Class", "btn btn-success btn-sm outline pull-right")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub saveQuoOK_ServerClick(sender As Object, e As EventArgs)
        lblquoteApprove.Text = "Do you wish to print Invoice?"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
        UpdatePanel5.Update()
    End Sub
    Public Sub Clear()
        Try
            txtNocDate.Text = ""
            txtGatePassDate.Text = ""
            txtboeno.Text = ""
            txtbondtype.Text = ""
            txtNocNo.Text = ""
            txtcustaddress.Text = ""
            txtbondinno.Text = ""
            txtigm.Text = ""
            txtbondindate.Text = ""
            txtimporter.Text = ""
            txtCustomer.Text = ""
            txtcha.Text = ""
            txtcommodity.Text = ""
            txtqty.Text = ""
            txtunit.Text = ""
            txtcargo.Text = ""
            txt20.Text = ""
            txt40.Text = ""
            txtArea.Text = ""
            txtChargesArea.Text = ""
            txtgrosskgs.Text = ""
            txtvalue.Text = ""
            txtduty.Text = ""
            txtvalduty.Text = ""
            lblchaid.Text = ""
            lblcustid.Text = ""
            lblimporterid.Text = ""
            lblchaid.Text = ""
            lblimporterid.Text = ""
            txtgstin.Text = ""
            txtgstname.Text = ""
            lblstatecode.Text = ""
            ddltraiff.SelectedValue = 0
            db.sub_ExecuteNonQuery("Delete from Temp_ExAssessment Where UserID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from temp_gst_search Where UserID=" & Session("UserId_BondCFS") & "")
            'db.sub_ExecuteNonQuery("Delete from Temp_Bond_Ex_Noc_Search Where UserID=" & Session("UserId_BondCFS") & "")
            grid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from temp_gst_search where userid='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows.Count > 0 Then
                    txtgstin.Text = Trim(dt.Rows(0)("GSTNo") & "")
                    txtgstname.Text = Trim(dt.Rows(0)("GSTName") & "")
                    lblstatecode.Text = Val(dt.Rows(0)("Statecode"))
                    lblpartyid.Text = Trim(dt.Rows(0)("Gstid") & "")
                    Call Sub_SGTRate()
                    strSql = ""
                    strSql += "select isnull(sum(amount),0) as Amount from Temp_ExAssessment where userid=" & Session("UserId_BondCFS") & ""
                    dt = db.sub_GetDatatable(strSql)
                    If dt.Rows.Count > 0 Then
                        dblGroup1Amt = Val(dt.Rows(0)(0))
                    End If
                    Call sub_CalcTotals()
                    btncal.Focus()
                Else
                    txtgstin.Text = ""
                    txtgstname.Text = ""
                    lblstatecode.Text = ""
                    txtgstin.Focus()
                    lblpartyid.Text = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentlist_Click(sender As Object, e As EventArgs)
        Try
            Clear()
            strSql = ""
            strSql += "select * from Temp_Bond_Ex_Noc_Search where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtdepositno.Text = Trim(dt.Rows(0)("NOCno") & "")
                Call txtNocNo_TextChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Button1_ServerClick(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "BONEx_noctext_changed_assessment '" & Trim(txtdepositno.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtbondinno.Text = Trim(dt.Rows(0)("bondno") & "")
                txtNocDate.Text = Trim(dt.Rows(0)("NOCDate") & "")
                txtbondindate.Text = Trim(dt.Rows(0)("EntryDate") & "")
                txtNocNo.Text = Trim(dt.Rows(0)("nocno") & "")
                txtboeno.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtigm.Text = Trim(dt.Rows(0)("IGMNo") & "")
                txtbondtype.Text = Trim(dt.Rows(0)("bondtype") & "")
                txtCustomer.Text = Trim(dt.Rows(0)("agentName") & "")
                txtcustaddress.Text = Trim(dt.Rows(0)("Address") & "")
                txtimporter.Text = Trim(dt.Rows(0)("ImporterName") & "")
                txtcha.Text = Trim(dt.Rows(0)("chaname") & "")
                txtcommodity.Text = Trim(dt.Rows(0)("Commodity") & "")
                txtqty.Text = Trim(dt.Rows(0)("qty") & "")
                txtunit.Text = Trim(dt.Rows(0)("unit") & "")
                If Trim(dt.Rows(0)("cargotype") & "") = "HAZ" Then
                    txtArea.Text = Trim(dt.Rows(0)("AreaBalance") & "") * 2
                    txtChargesArea.Text = Trim(dt.Rows(0)("AreaBalance") & "") * 2
                Else
                    txtArea.Text = Trim(dt.Rows(0)("AreaBalance") & "")
                    txtChargesArea.Text = Trim(dt.Rows(0)("AreaBalance") & "")
                End If
                txtcargo.Text = Trim(dt.Rows(0)("cargotype") & "")
                txtgrosskgs.Text = Trim(dt.Rows(0)("GrossWt") & "")
                txtvalue.Text = Trim(dt.Rows(0)("value") & "")
                txtduty.Text = Trim(dt.Rows(0)("duty") & "")
                txtvalduty.Text = Val(txtvalue.Text) + Val(txtduty.Text)
                txt20.Text = Trim(dt.Rows(0)("Size20") & "")
                txt40.Text = Trim(dt.Rows(0)("Size40") & "")
                lblchaid.Text = Trim(dt.Rows(0)("CHAID") & "")
                lblcustid.Text = Trim(dt.Rows(0)("CustID") & "")
                lblimporterid.Text = Trim(dt.Rows(0)("ImporterId") & "")
                txtvalidDate.Text = Convert.ToDateTime(dt.Rows(0)("ExpiryDate")).ToString("yyyy-MM-dd")
                ddltraiff.SelectedValue = Trim(dt.Rows(0)("TariffID") & "")
                txtGatePassDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("GatePassDate") & "")).ToString("yyyy-MM-dd")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!');", True)
                txtdepositno.Text = ""
                txtdepositno.Focus()
                Exit Sub
            End If
            txtgstin.Focus()
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

