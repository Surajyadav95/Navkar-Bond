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
    'Dim lblinsvalidupto As Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Convert.ToInt32(Session("UserId_BondCFS")) = 0 Then
            Response.Redirect("~/Bond/Login.aspx")
            Exit Sub
        End If
        'If Session("UserId_BondCFS") Is Nothing Then
        '    Session("UserId_BondCFS") = Request.Cookies("UserID").Value
        '    Session("UserName_BondCFS") = Request.Cookies("UserName").Value
        '    Session("CompID") = Request.Cookies("CompID").Value
        '    Session("Workyear") = Request.Cookies("Workyear").Value            
        'End If
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from Temp_Assessment Where UserID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from temp_gst_search Where UserID=" & Session("UserId_BondCFS") & "")

            txtvalidDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtAssessmentDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtInsValidUpto.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            grid()
            Filldropdown()
            txtNocNo.Focus()
            ddlCommodity_TextChanged(sender, e)
        End If
    End Sub
    Public Sub grid()
        strSql = ""
        strSql += "select * from Temp_Assessment where userid=" & Session("UserId_BondCFS") & ""
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
        strSql += "select * from Temp_Assessment where userid=" & Session("UserId_BondCFS") & ""
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
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No charges found!');", True)
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
    Protected Sub txtNocNo_TextChanged(sender As Object, e As EventArgs) Handles txtNocNo.TextChanged
        Try
            Clear()
            strSql = ""
            strSql = "usp_fill_invoice '" & Trim(txtNocNo.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If Not ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!');", True)
                txtNocNo.Text = ""
                txtNocNo.Focus()
                Exit Sub
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                lblsession1.Text = "Assessment already generated. Do you want to generate assessment again for extended period?"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
                UpdatePanel6.Update()
                Exit Sub
            End If
            If ds.Tables(3).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Assessment already generated!');", True)
                txtNocNo.Text = ""
                txtNocNo.Focus()
                Exit Sub
            End If
            If ds.Tables(2).Rows.Count > 0 Then
                dt = ds.Tables(2)
            End If
            If dt.Rows.Count > 0 Then
                'txtAssessmentDate.Text = Trim(dt.Rows(0)("NDate") & "")
                txtNocDate.Text = Trim(dt.Rows(0)("NDate") & "")
                txtbondtype.Text = Trim(dt.Rows(0)("BondType") & "")
                txtbedate.Text = Trim(dt.Rows(0)("BDate") & "")
                txtbe.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtigm.Text = Trim(dt.Rows(0)("igmno") & "")
                Txtitem.Text = Trim(dt.Rows(0)("itemno") & "")
                'txtimpid.Text = Trim(dt.Rows(0)("Importerid") & "")
                txtimport.Text = Trim(dt.Rows(0)("ImporterName") & "")
                txtCustomer.Text = Trim(dt.Rows(0)("agentName") & "")
                txtcha.Text = Trim(dt.Rows(0)("chaname") & "")
                txtcommodity.Text = Trim(dt.Rows(0)("Commodity") & "")
                txtqty.Text = Trim(dt.Rows(0)("qty") & "")
                txtunit.Text = Trim(dt.Rows(0)("unit") & "")
                txtcargo.Text = Trim(dt.Rows(0)("cargotype") & "")
                txt20.Text = Trim(dt.Rows(0)("T20") & "")
                txt40.Text = Trim(dt.Rows(0)("T40") & "")
                txtArea.Text = Trim(dt.Rows(0)("StorageSpace") & "")
                txtChargesArea.Text = Trim(dt.Rows(0)("StorageSpace") & "")
                txtgrosskgs.Text = Trim(dt.Rows(0)("grosswt") & "")
                txtvalue.Text = Trim(dt.Rows(0)("value") & "")
                txtduty.Text = Trim(dt.Rows(0)("duty") & "")
                txtvalduty.Text = Val(txtvalue.Text) + Val(txtduty.Text)
                lblchaid.Text = Trim(dt.Rows(0)("CHAID") & "")
                lblcustid.Text = Trim(dt.Rows(0)("CustID") & "")
                lblimporterid.Text = Trim(dt.Rows(0)("ImporterId") & "")
                txtvalidDate.Text = Convert.ToDateTime(dt.Rows(0)("ValidDate")).ToString("yyyy-MM-dd")
                ddltraiff.SelectedValue = Trim(dt.Rows(0)("TariffID") & "")
                txtInsValidUpto.Text = Convert.ToDateTime(dt.Rows(0)("InsValidDate")).ToString("yyyy-MM-dd")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!');", True)
                txtNocNo.Text = ""
                txtNocNo.Focus()
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
            db.sub_ExecuteNonQuery("Delete from Temp_Assessment Where UserID=" & Session("UserId_BondCFS") & "")
            Call Sub_SGTRate()
            strSql = ""
            strSql += "USP_CALCULATION_noc_tariff '" & Convert.ToDateTime(Trim(txtAssessmentDate.Text)).ToString("yyyyMMdd") & "','" & Trim(ddltraiff.SelectedItem.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid tariff details. Please contact your administrator!');", True)
                ddltraiff.Focus()
                Exit Sub
            End If
            Call sub_chargesgrid()
            Call grid1()
            Call sub_CalcTotals()
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
            strSql += "USP_noc_assessment_for_accountid '" & Trim(ddltraiff.SelectedItem.Text) & "','" & Convert.ToDateTime(Trim(txtAssessmentDate.Text)).ToString("yyyyMMdd") & "','" & Trim(txtNocNo.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    strSql = ""
                    strSql += "SELECT Upper(AccountName) AccountName,GroupID FROM bond_AccountMaster WHERE AccountID=" & Val(ds.Tables(0).Rows(i)("AccountID")) & ""
                    dt5 = db.sub_GetDatatable(strSql)
                    If InStr(dt5.Rows(0)("AccountName"), "INSURANCE") > 0 Then
                        Call sub_CalcInsuranceAmount(ds.Tables(0).Rows(i)("AccountID"))
                        'ElseIf InStr(dt5.Rows(0)("AccountName"), "LOADING") > 0 Then
                        '    Call sub_loadingcharges(ds.Tables(0).Rows(i)("AccountID"))
                    Else
                        Call sub_fetchcharges(ds.Tables(0).Rows(i)("AccountID"))
                    End If

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
                        strSql += "USP_insert_into_temp_assessment '" & Trim(ds.Tables(0).Rows(i)("AccountID")) & "','" & Val(dt4.Rows(0)("dblnetamount")) & "',0,0,'" & Session("UserId_BondCFS") & "'"
                        db.sub_ExecuteNonQuery(strSql)
                    End If
                Next
            End If
            Dim blnAccountFound As Boolean
            If ds.Tables(1).Rows.Count > 0 Then
                For i = 0 To ds.Tables(1).Rows.Count - 1
                    If ds.Tables(1).Rows(i)("amount") <> 0 Then
                        strSql = ""
                        strSql += "select * from temp_assessment where accountid='" & ds.Tables(1).Rows(i)("accountid") & "' and UserId='" & Session("UserId_BondCFS") & "'"
                        dt = db.sub_GetDatatable(strSql)
                        If dt.Rows.Count > 0 Then
                            blnAccountFound = True
                        End If
                        If blnAccountFound = False Then
                            strSql = ""
                            strSql += "USP_insert_into_temp_assessment '" & Trim(ds.Tables(1).Rows(i)("accountid")) & "','" & ds.Tables(1).Rows(i)("amount") & "','" & ds.Tables(1).Rows(i)("IsSTax") & "',0,'" & Session("UserId_BondCFS") & "'"
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
    Private Sub sub_loadingcharges(strAccountID As String)
        Try
            Dim dblSQM As Double = 0, dblPerc As Double = 0, dblAmount As Double = 0, dblDestuffDays As Double = 0, dblIGMWeight As Double = 0, dblPaidAmount As Double = 0
            Dim blnSTax As Boolean = False
            Dim strAccountName As String = ""
            Dim dblDestuffDate As Date
            Dim intDays As Double
            Dim Intweeks As Integer = 0
            dblSQM = Val(lblArea.Text)

            dblNetAmount = 0
            dblPaidAmount = 0
            intDays = 0

            Dim bondtype As String = ""

            strSql = ""
            strSql += "USP_calculation_fetchcharges_noc '" & Trim(ddltraiff.SelectedItem.Text & "") & "','" & strAccountID & "','" & Convert.ToDateTime(txtAssessmentDate.Text).ToString("yyyyMMdd") & "','" & Trim(txtNocNo.Text) & "'"
            ds1 = db.sub_GetDataSets(strSql)
            If ds1.Tables(0).Rows.Count > 0 Then
                intDays = DateDiff("d", txtNocDate.Text, txtvalidDate.Text) + 1
                For i = 0 To ds1.Tables(0).Rows.Count - 1
                    If Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "F" And ds1.Tables(0).Rows(i)("size") = "20" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txt20.Text)
                    ElseIf ds1.Tables(0).Rows(i)("SorF") = "F" And ds1.Tables(0).Rows(i)("size") = "40" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txt40.Text)

                    End If

                    If intDays Mod 7 = 0 Then
                        Intweeks = intDays / 7
                    Else
                        Intweeks = Int((intDays / 7)) + 1
                    End If

                    If ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txtChargesArea.Text) * Intweeks

                    End If
                Next
            End If

            If ds1.Tables(2).Rows.Count > 0 Then
                dblPaidAmount = dblPaidAmount + Val(ds1.Tables(2).Rows(0)(0))
            End If

            dblNetAmount = dblNetAmount - dblPaidAmount

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CalcTotals()
        Try
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0
            dblGroup1Amt = dblGroup1Amt + dblGroup2Amt
            dbltotal = dblGroup1Amt
            dbltotalcgst = Format(dblGroup1Amt * (dblSGST / 100), "0.00")
            dbltotalsgst = Format(dblGroup1Amt * (dblCGST / 100), "0.00")
            dbltotaligst = Format(dblGroup1Amt * (dblIGST / 100), "0.00")
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
            lblAllTotal.Text = dblalltotal
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
            Dim intDays As Double
            Dim Intweeks As Integer = 0
            dblSQM = Val(lblArea.Text)

            dblNetAmount = 0
            dblPaidAmount = 0
            intDays = 0

            Dim bondtype As String = ""

            strSql = ""
            strSql += "USP_calculation_fetchcharges_noc '" & Trim(ddltraiff.SelectedItem.Text & "") & "','" & strAccountID & "','" & Convert.ToDateTime(txtAssessmentDate.Text).ToString("yyyyMMdd") & "','" & Trim(txtNocNo.Text) & "'"
            ds1 = db.sub_GetDataSets(strSql)
            If ds1.Tables(0).Rows.Count > 0 Then
                intDays = DateDiff("d", txtNocDate.Text, txtvalidDate.Text) + 1
                If intDays Mod 7 = 0 Then
                    Intweeks = intDays / 7
                Else
                    Intweeks = Int((intDays / 7)) + 1
                End If
                For i = 0 To ds1.Tables(0).Rows.Count - 1
                    If ds1.Tables(3).Rows.Count > 0 Then
                        If strAccountID = 23 Then
                            If ds1.Tables(0).Rows(i)("IsInternal") = True And Trim(ds1.Tables(3).Rows(0)("CargoCategory") & "") <> "I" Then
                                GoTo lblnext
                            End If
                        End If
                    End If
                    If Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "S" Then
                        dblNetAmount = dblNetAmount + slab_CalcAmount(ds1.Tables(0).Rows(i)("SlabID"), intDays, 0, Val(txtgrosskgs.Text), Convert.ToDateTime(txtNocDate.Text).ToString("dd-MMM-yyyy HH:mm"), strAccountID, ds1.Tables(0).Rows(i)("Size"))
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "F" And ds1.Tables(1).Rows(i)("ConsiderArea") = False Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "20" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txt20.Text)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "40" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txt40.Text)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "45" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txt40.Text)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "Q" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txtqty.Text)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "O" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(Val(txtgrosskgs.Text) / 1000)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "D" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(Val(txtgrosskgs.Text) / 1000) * intDays
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "F" And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txtChargesArea.Text) * Intweeks
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "W" Then
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Intweeks
                    End If
                    'If ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                    '    dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(txtArea.Text) * Intweeks
                    'End If
                    If ds1.Tables(0).Rows(i)("IsSTax") = True Then
                        dblSTaxOnAmount = dblSTaxOnAmount + dblNetAmount
                    End If
lblnext:
                Next
            End If

            If ds1.Tables(2).Rows.Count > 0 Then
                dblPaidAmount = dblPaidAmount + Val(ds1.Tables(2).Rows(0)(0))
            End If

            dblNetAmount = dblNetAmount - dblPaidAmount
            dblNetAmount = Format(dblNetAmount, "0.00")
            dblSTaxOnAmount = dblSTaxOnAmount - dblPaidAmount
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Function slab_CalcAmount(slabID As Integer, DaysValue As Double, percentage As Double, Weight As Double, InDate As Date, AccountID As String, size As Integer) As Double
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

                    If ds3.Tables(2).Rows.Count > 0 Then
                        If size = 20 Then
                            dblSlabAmount = dblWeekValue * Val(ds3.Tables(2).Rows(0)("Value")) * Val(txt20.Text)
                        ElseIf size = 40 Then
                            dblSlabAmount = dblWeekValue * Val(ds3.Tables(2).Rows(0)("Value")) * Val(txt40.Text)
                        End If
                    End If

                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount

                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Percentage" Then

                    If ds3.Tables(1).Rows.Count > 0 Then
                        dblSlabAmount = Val(ds3.Tables(1).Rows(0)("Value"))
                    End If
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount
                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Weight" Then
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
                                strSql += "select slabID from bond_tariffdetails where accountID='" & AccountID & "' and chargesbased=" & Val(Charges) & " and Size=20 and bondtype='Noc' and tariffID=" & Trim(ddltraiff.SelectedItem.Text & "") & " and iscancel='0' order by entryid desc "
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
                                strSql += "select slabID from bond_tariffdetails where accountID='" & AccountID & "' and chargesbased=" & Val(Charges) & " and Size=40 and bondtype='Noc'"
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
                            strSql += "select slabID from bond_tariffdetails where accountID='" & AccountID & "' and chargesbased=" & Val(Charges) & " and Size=0 and bondtype='Noc'"
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

                    'If ds3.Tables(3).Rows.Count > 0 Then
                    '    If Val(txtgrosskgs.Text) Mod 100 > 0 Then
                    '        dblSlabAmount = Math.Round(Val(ds3.Tables(2).Rows(0)("Value")) * (Val(txtgrosskgs.Text) / 100))
                    '    End If
                    '    If dblSlabAmount < 85 Then
                    '        dblSlabAmount = 85
                    '    End If
                    'Else

                    '    dblSlabAmount = Math.Round(Val(ds3.Tables(2).Rows(0)("Value")) * (Val(txtgrosskgs.Text) / 100))

                    '    If dblSlabAmount < 85 Then
                    '        dblSlabAmount = 85
                    '    End If
                    'End If
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
    Private Sub sub_CalcInsuranceAmount(strAccountID As String)
        Try
            Dim dblInsAmount As Double = 0, dblInsAmountPaid As Double = 0, dblInsRate As Double = 0, dtRecordDate As Date, dblPaidAmount As Double = 0
            Dim blnSTax As Boolean = False
            Dim intTotalIDays As Integer = 0, intTotalWeek As Integer = 0
            Dim lblinsvalidup As Date
            Dim dblDestuffDate As Date
            Dim dbltemp As Double = 0
            dblNetAmount = 0
            If Val(txtvalduty.Text) <> 0 Then
                intTotalIDays = DateDiff("d", Convert.ToDateTime(txtNocDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtInsValidUpto.Text).ToString("yyyy-MM-dd"))
            End If
            If intTotalIDays Mod 7 = 0 Then
                intTotalWeek = intTotalIDays / 7
                'lblinsvalidup = Format(DateAdd("d", (intTotalWeek * 7), dtpnocdate - 1), "dd-MMM-yyyy")
                lblinsvalidup = Convert.ToDateTime(DateAdd("d", (intTotalWeek * 7), DateAdd("d", -1, Convert.ToDateTime(txtNocDate.Text).ToString("yyyy-MM-dd")))).ToString("yyyy-MM-dd")
                lblinsvalidupto.Text = lblinsvalidup
            Else
                intTotalWeek = Int(intTotalIDays / 7) + 1
                'lblinsvalidup = Format(DateAdd("d", (intTotalWeek * 7), dtpnocdate - 1), "dd-MMM-yyyy")
                lblinsvalidup = Convert.ToDateTime(DateAdd("d", (intTotalWeek * 7), DateAdd("d", -1, Convert.ToDateTime(txtNocDate.Text).ToString("yyyy-MM-dd")))).ToString("yyyy-MM-dd")
                lblinsvalidupto.Text = lblinsvalidup
            End If

            strSql = ""
            strSql += "usp_CalcInsurance'" & Trim(ddltraiff.SelectedItem.Text & "") & "','" & Convert.ToDateTime(txtAssessmentDate.Text).ToString("yyyyMMdd") & "','" & Trim(txtNocNo.Text) & "','" & Trim(strAccountID) & "'"
            ds4 = db.sub_GetDataSets(strSql)
            If ds4.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds4.Tables(0).Rows.Count - 1
                    If Trim(ds4.Tables(0).Rows(i)("SorF") & "") = "F" Then
                        dblNetAmount = Val(txtvalduty.Text) * Val(ds4.Tables(0).Rows(0)("InsRate") / 1000) * intTotalWeek
                    ElseIf Trim(ds4.Tables(0).Rows(i)("SorF") & "") = "W" Then
                        dblNetAmount += Val(ds4.Tables(0).Rows(i)("InsRate")) * intTotalWeek                   
                    End If

                Next
           
                ''dbltemp = Val(txtvalduty.Text) * Val(ds4.Tables(0).Rows(0)("InsRate") / 1000)

            End If

            'dblNetAmount = dbltemp * intTotalWeek
            'dblNetAmount = Math.Round(dblNetAmount)
            dblInsAmount = Format(dblNetAmount, "0.00")
            If ds4.Tables(0).Rows(0)("IsSTax") = True Then
                dblSTaxOnAmount = dblSTaxOnAmount + dblNetAmount
            End If
            If ds4.Tables(2).Rows.Count > 0 Then
                dblPaidAmount = dblPaidAmount + Val(ds4.Tables(2).Rows(0)(0))
            End If
            If Not Math.Ceiling(dblNetAmount) <= dblPaidAmount Then
                dblNetAmount = dblNetAmount - dblPaidAmount
            End If
            If dblNetAmount < 1 Then
                dblNetAmount = Math.Ceiling(dblNetAmount)
            End If
            dblNetAmount = Format(dblNetAmount, "0.00")

            dblSTaxOnAmount = dblSTaxOnAmount - dblPaidAmount
            'dblGroup1Amt = dblGroup1Amt + dblInsAmount

            'If dblInsAmount <> 0 Then
            '    strSql = ""
            '    strSql += "USP_insert_into_temp_assessment '" & Trim(ds4.Tables(1).Rows(0)("accountid")) & "','" & dblInsAmount & "','1',0,'" & Session("UserId_BondCFS") & "'"
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
                Exit Sub
            End If
            Call Sub_SGTRate()
            strSql = ""
            strSql += "USP_save_part_for_noc_assessment '" & Trim(ddltraiff.SelectedItem.Text) & "','" & strWorkyear & "','" & Trim(txtNocNo.Text) & "'"
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
            For Each row In rptIndentLIst.Rows
                strSql = ""
                strSql += "USP_insert_into_bond_assessd '" & dblassessno & "','" & strWorkyear & "','" & Val(CType(row.FindControl("lblaccntid"), Label).Text) & "',"
                strSql += "'" & Val(CType(row.FindControl("lblntamnt"), Label).Text) & "','" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblSGST / 100), "0.00") & "',"
                strSql += "'" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblCGST / 100), "0.00") & "','" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblIGST / 100), "0.00") & "',"
                strSql += "'" & dbltaxgroupid & "','" & Trim(txtNocNo.Text) & "','" & Trim(txtChargesArea.Text) & "'"
                db.sub_ExecuteNonQuery(strSql)
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
            If lblinsvalidupto.Text = "" Then
                lblinsvalidupto1 = ""
            Else
                lblinsvalidupto1 = Convert.ToDateTime(lblinsvalidupto.Text).ToString("yyyy-MM-dd")
            End If
            strSql = ""
            strSql += "USP_insert_into_bond_assessm '" & dblassessno & "','" & strWorkyear & "','" & lblchaid.Text & "','" & lblcustid.Text & "','" & lblimporterid.Text & "',"
            strSql += "'" & Convert.ToDateTime(txtAssessmentDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txtvalidDate.Text).ToString("yyyy-MM-dd") & "','" & Trim(lblinsvalidupto1 & "") & "',"
            strSql += "'" & Trim(txtNocNo.Text) & "','" & Trim(txtbondtype.Text) & "','" & Trim(txtbe.Text) & "','" & Trim(ddltraiff.SelectedItem.Text) & "','" & Trim(txtigm.Text) & "',"
            strSql += "'" & Trim(Txtitem.Text) & "','" & Trim(txt20.Text) & "','" & Trim(txt40.Text) & "','" & dblSumNetAmtTotal & "','" & dblSumNetAmtTotal & "','" & Session("UserId_BondCFS") & "',"
            strSql += "'" & dblSumSGSTAmt & "','" & dblSumCGSTAmt & "','" & dblSumIGSTAmt & "','" & dblgrandtotal & "','" & lblpartyid.Text & "','" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "','" & Trim(ddlCommodity.SelectedValue & "") & "','" & Trim(ddltxtTax.SelectedValue & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            txtassessno.Text = dblassessno
            txtworkyear.Text = strWorkyear
            Clear()
            txtNocNo.Focus()
            txtNocNo.Text = ""
            txtAssessmentDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
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
            txtbondtype.Text = ""
            txtbedate.Text = ""
            txtbe.Text = ""
            txtigm.Text = ""
            Txtitem.Text = ""
            txtimport.Text = ""
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
            txtvalidDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtInsValidUpto.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            'txtAssessmentDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            db.sub_ExecuteNonQuery("Delete from Temp_Assessment Where UserID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from temp_gst_search Where UserID=" & Session("UserId_BondCFS") & "")

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
                    'ddltraiff.SelectedValue = Trim(dt.Rows(0)("TariffID") & "")

                    Call Sub_SGTRate()
                    strSql = ""
                    strSql += "select isnull(sum(amount),0) as Amount from Temp_Assessment where userid=" & Session("UserId_BondCFS") & ""
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
                    'ddltraiff.SelectedValue = 0
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub ONExtendNOC()
        Try
            strSql = ""
            strSql = "usp_fill_invoice '" & Trim(txtNocNo.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(2).Rows.Count > 0 Then
                dt = ds.Tables(2)
            End If
            If dt.Rows.Count > 0 Then
                'txtAssessmentDate.Text = Trim(dt.Rows(0)("NDate") & "")
                txtNocDate.Text = Trim(dt.Rows(0)("NDate") & "")
                txtbondtype.Text = Trim(dt.Rows(0)("BondType") & "")
                txtbedate.Text = Trim(dt.Rows(0)("BDate") & "")
                txtbe.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtigm.Text = Trim(dt.Rows(0)("igmno") & "")
                Txtitem.Text = Trim(dt.Rows(0)("itemno") & "")
                'txtimpid.Text = Trim(dt.Rows(0)("Importerid") & "")
                txtimport.Text = Trim(dt.Rows(0)("ImporterName") & "")
                txtCustomer.Text = Trim(dt.Rows(0)("agentName") & "")
                txtcha.Text = Trim(dt.Rows(0)("chaname") & "")
                txtcommodity.Text = Trim(dt.Rows(0)("Commodity") & "")
                txtqty.Text = Trim(dt.Rows(0)("qty") & "")
                txtunit.Text = Trim(dt.Rows(0)("unit") & "")
                txtcargo.Text = Trim(dt.Rows(0)("cargotype") & "")
                txt20.Text = Trim(dt.Rows(0)("T20") & "")
                txt40.Text = Trim(dt.Rows(0)("T40") & "")
                txtArea.Text = Trim(dt.Rows(0)("StorageSpace") & "")
                txtChargesArea.Text = Trim(dt.Rows(0)("StorageSpace") & "")
                txtgrosskgs.Text = Trim(dt.Rows(0)("grosswt") & "")
                txtvalue.Text = Trim(dt.Rows(0)("value") & "")
                txtduty.Text = Trim(dt.Rows(0)("duty") & "")
                txtvalduty.Text = Val(txtvalue.Text) + Val(txtduty.Text)
                lblchaid.Text = Trim(dt.Rows(0)("CHAID") & "")
                lblcustid.Text = Trim(dt.Rows(0)("CustID") & "")
                lblimporterid.Text = Trim(dt.Rows(0)("ImporterId") & "")
                If Not Trim(dt.Rows(0)("extendupto")) = "" Then
                    txtvalidDate.Text = Convert.ToDateTime(dt.Rows(0)("extendupto")).ToString("yyyy-MM-dd")
                    txtInsValidUpto.Text = Convert.ToDateTime(dt.Rows(0)("extendupto")).ToString("yyyy-MM-dd")

                Else
                    txtvalidDate.Text = Convert.ToDateTime(dt.Rows(0)("ValidDate")).ToString("yyyy-MM-dd")
                    txtInsValidUpto.Text = Convert.ToDateTime(dt.Rows(0)("ValidDate")).ToString("yyyy-MM-dd")

                End If
                ddltraiff.SelectedValue = Trim(dt.Rows(0)("TariffID") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!');", True)
                txtNocNo.Text = ""
                txtNocNo.Focus()
                Exit Sub
            End If

            txtgstin.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Button1_ServerClick(sender As Object, e As EventArgs)
        Try
            ONExtendNOC()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnNOCList_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select top 1 * from Temp_NOC_List_NOCAssessment where userid=" & Session("UserId_BondCFS") & " order by AddedOn desc"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtNocNo.Text = Trim(dt.Rows(0)("NOCNo") & "")
                txtNocNo_TextChanged(sender, e)
            End If
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

