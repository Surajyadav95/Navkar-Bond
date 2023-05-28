Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
    Dim strCategory As String
    Dim strCategoryDetails As String
    Dim strCategorySPName As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    
        If Not IsPostBack Then
            txtdateofModi.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtWorkYear.Text = Session("Workyear")
            'txtAssessmentDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            Filldropdown()
        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("exec get_sp_table 'Party_GST_M',' GSTID , GSTName','','GSTName'")
            If (dt.Rows.Count > 0) Then
                ddlpartyName.DataSource = dt
                ddlpartyName.DataTextField = "GSTName"
                ddlpartyName.DataValueField = "GSTID"
                ddlpartyName.DataBind()
                ddlpartyName.Items.Insert(0, New ListItem("--Select--", 0))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
   
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function

    Protected Sub ddlpartyName_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "usp_modify_fill '" & Trim(ddlpartyName.SelectedItem.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblgst.Text = Trim(dt.Rows(0)("GSTIn_uniqID") & "")
                txGSTAddress.Text = Trim(dt.Rows(0)("GSTAddress") & "")
            
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "usp_search_modify'" & Trim(txtAssessment.Text & "") & "','" & Session("Workyear") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtGstIn.Text = Trim(dt.Rows(0)("GSTIn_uniqID") & "")
                ddlpartyName.SelectedValue = Trim(dt.Rows(0)("PartyId") & "")
                txGSTAddress.Text = Trim(dt.Rows(0)("GSTAddress") & "")
                'txtoldpartyid.Text = Trim(dt.Rows(0)("PartyId") & "")
                txtAssessmentDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Assess Date") & "")).ToString("dd-MM-yyyy")
                'If Val(dt.Rows(0)("gst")) = 0 Then
                '    chkgstapplicable.Checked = False
                'Else
                '    chkgstapplicable.Checked = True
                'End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dblmaxcrno As Double
            Dim dblSGST As Double = 0
            Dim dblIGST As Double = 0
            Dim dblCGST As Double = 0
            Dim strSQL As String
            Dim dbldiscamt As Double = 0

            If chkgstapplicable.Checked = False Then
                strSGSTPer = "SGST" & " @ " & 0 & "%"
                StrCGSTPEr = "CGST" & " @ " & 0 & "%"
                StrIGSTPer = "IGST" & " @ " & 0 & "%"
                If Not dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified invoice is without GST. Are you sure to update GST Party?');", True)
                    Exit Sub
                End If
            End If
            strCategory = ""
            strCategoryDetails = ""
            strCategorySPName = ""
            If ddlCategory.SelectedValue = "Bond" Then
                strCategory = "bond_assessM"
                strCategoryDetails = "bond_assessD"
                strCategorySPName = "get_sum_charges_bond"
            End If
            Dim dtdays As New DataTable
            Dim intdays As Integer

            strSQL = "get_Sp_nettotal '" & strCategory & "','" & Trim(txtAssessment.Text) & "','" & Trim(txtWorkYear.Text) & "'"
            dt = db.sub_GetDatatable(strSQL)
            If dt.Rows.Count > 0 Then
                If (Trim(ddlpartyName.SelectedValue)) = False Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dblCGST = (Val(dt.Rows(i)("Net Amount")) - Val(dt.Rows(i)("Discount Amount"))) * dblCGST / 100
                        dblSGST = (Val(dt.Rows(i)("Net Amount")) - Val(dt.Rows(i)("Discount Amount"))) * dblSGST / 100
                        dblCGST = Format(dblCGST, "0.00")
                        dblIGST = Format(dblIGST, "0.00")
                        dblSGST = Format(dblSGST, "0.00")

                        strSQL = "exec  get_Sp_UpdateTaxAssessD '" & Trim(dt.Rows(i)("Name") & "") & "','" & Trim(txtAssessment.Text) & "',"
                        strSQL += " '" & Trim(txtWorkYear.Text) & "','" & Trim(dt.Rows(i)("First") & "") & "','" & Trim(dt.Rows(i)("Second") & "") & "',"
                        strSQL += " '" & Trim(dt.Rows(i)("Account ID") & "") & "','" & dblCGST & "','" & dblSGST & "','" & dblIGST & "'"
                        dt = db.sub_GetDatatable(strSQL)
                    Next
                Else
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dblIGST = (Val(dt.Rows(i)("Net Amount")) - Val(dt.Rows(i)("Discount Amount"))) * dblIGST / 100
                        dblCGST = Format(dblCGST, "0.00")
                        dblIGST = Format(dblIGST, "0.00")
                        dblSGST = Format(dblSGST, "0.00")

                        strSQL = "exec  get_Sp_UpdateTaxAssessD '" & Trim(dt.Rows(i)("Name") & "") & "','" & Trim(txtAssessment.Text) & "',"
                        strSQL += " '" & Trim(txtWorkYear.Text) & "','" & Trim(dt.Rows(i)("First") & "") & "','" & Trim(dt.Rows(i)("Second") & "") & "',"
                        strSQL += " '" & Trim(dt.Rows(i)("Account ID") & "") & "','" & dblCGST & "','" & dblSGST & "','" & dblIGST & "'"
                        dt = db.sub_GetDatatable(strSQL)
                    Next
                End If
            End If

            Dim dtdiscamt As New DataTable
            strSQL = ""
            strSQL = "select * from " & strCategory & " where assessno='" & Trim(txtAssessment.Text) & "' and workyear='" & Trim(txtWorkYear.Text) & "'"
            dtdiscamt = db.sub_GetDatatable(strSQL)
            If dtdiscamt.Rows.Count > 0 Then
                dbldiscamt = Val(dtdiscamt.Rows(0)("NetTotal1"))
            End If

            Dim dtfill As New DataTable
            strSQL = ""
            strSQL = "exec " & strCategorySPName & " '" & Trim(txtAssessment.Text & "") & "','" & Trim(Trim(txtWorkYear.Text) & "") & "'"
            dtfill = db.sub_GetDatatable(strSQL)
            If dtfill.Rows.Count > 0 Then
                strSQL = ""
                strSQL = "update " & strCategory & "  set   ServiceTax ='" & strSGSTPer & "' , SBCCess ='" & StrCGSTPEr & "'  , KKCess ='" & StrIGSTPer & "' , Grandtotal=" & Val(Val(dtfill.Rows(0)("SGST")) + Val(dtfill.Rows(0)("CGST")) + Val(dtfill.Rows(0)("IGST")) + Val(dtfill.Rows(0)("Amount")) - Val(dbldiscamt)) & ",  PartyId='" & Trim(ddlpartyName.SelectedValue) & "' ,SGST=" & Val(dtfill.Rows(0)("SGST") & "") & " ,CGST=" & Val(dtfill.Rows(0)("CGST") & "") & " ,IGST=" & Val(dtfill.Rows(0)("IGST") & "") & " "
                strSQL += " where AssessNo ='" & Trim(txtAssessment.Text) & "' AND WorkYear = '" & Trim(txtWorkYear.Text) & "'"
                dt = db.sub_GetDatatable(strSQL)
            End If
            Dim strQuery As String
            strQuery = ""
            If chkgstapplicable.Checked = False Then
                strQuery = "Exec USP_Inser_Modify_GST_Inv '" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd") & "','" & Trim(ddlCategory.SelectedValue) & "', '" & Trim(txtAssessment.Text) & "','" & Trim(txtWorkYear.Text) & "','" & Trim(txtGstIn.Text) & "', '" & Trim(ddlpartyName.SelectedValue) & "', NULL, " & Session("UserId_BondCFS") & " "
            Else
                strQuery = "Exec USP_Inser_Modify_GST_Inv '" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd") & "','" & Trim(ddlCategory.SelectedValue) & "', '" & Trim(txtAssessment.Text) & "','" & Trim(txtWorkYear.Text) & "','" & Trim(txtGstIn.Text) & "', '" & Trim(ddlpartyName.SelectedValue) & "', '" & Convert.ToDateTime(Trim(txtAssessmentDate.Text & "")).ToString("yyyy-MM-dd ") & "', " & Session("UserId_BondCFS") & " "
            End If
            db.sub_ExecuteNonQuery(strSQL)
            lblSession.Text = "Record updated successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
