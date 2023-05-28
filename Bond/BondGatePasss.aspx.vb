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
    Dim GPNo, GPNoView As String
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
        '    Session("Workyear") = Request.Cookies("Workyear").Value
        'End If
        If Not IsPostBack Then
            txtgateDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            db.sub_ExecuteNonQuery("Delete from Temp_gate_pass Where UniqueID=" & Session("UserId_BondCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Bond_Gate_Search Where UserID=" & Session("UserId_BondCFS") & "")
            Filldropdown()
            Container(sender, e)             
            txttallyNo.Focus()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Get_Dropdown_gate_list")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    Protected Sub txtnocno_TextChanged(sender As Object, e As EventArgs)
        Try
            Clear(sender, e)
            strSql = ""
            strSql = "USP_Gate_new_Fill '" & Trim(txttallyNo.Text & "") & "' "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Gate pass already done');", True)
                txttallyNo.Text = ""
                txttallyNo.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql = "select * from bond_assessm where iscancel=0 and DeliveryType='E' and ExDepositeNo='" & Trim(txttallyNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invoice not made against this bond ex. Cannot Proceed');", True)
                txttallyNo.Text = ""
                txttallyNo.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql = "usp_gate_pass_fill'" & Trim(txttallyNo.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txttallyNo.Text = Trim(dt.Rows(0)("EntryID") & "")
                txtnocno.Text = Trim(dt.Rows(0)("NOCNo")& "")
                textbond.Text = Trim(dt.Rows(0)("BondNo") & "")
                txtbe.Text = Trim(dt.Rows(0)("BOENO") & "")
                txtbedate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("bonddate") & "")).ToString("yyyy-MM-dd")
                TxtManifested.Text = Trim(dt.Rows(0)("manifiestqty") & "")
                txtBalanec.Text = Trim(dt.Rows(0)("balanceqty"))
                txtGross.Text = Trim(dt.Rows(0)("manifiestwt") & "")
                txtBalanecw.Text = Trim(dt.Rows(0)("balancewt") & "")
                txtImporter.Text = Trim(dt.Rows(0)("Importer") & "")
                txtCha.Text = Trim(dt.Rows(0)("CHA") & "")
                txtDescrlption.Text = Trim(dt.Rows(0)("Remarks") & "")
                txtexBoe.Text = Trim(dt.Rows(0)("ExBoeNo") & "")
                txtBoeDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("exboedate") & "")).ToString("dd-MM-yyyy")
                txtDelivered.Text = Trim(dt.Rows(0)("deliverdqty") & "")
                txtDeliveredQty.Text = Trim(dt.Rows(0)("deliverdwt") & "")          
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnadd_Click(sender As Object, e As EventArgs)
        Try
            'If Val(textqt.Text) = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container Qty should not be zero');", True)
            '    textqt.Text = ""
            '    textqt.Focus()
            '    Exit Sub
            'End If
            strSql = ""
            strSql = "USP_temp_gate_select '" & Trim(txtcontainer.Text & "") & "','" & Session("UserId_BondCFS") & "','" & Trim(txtTrailer.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If Not txtcontainer.Text = "" Then                
                If (ds.Tables(0).Rows.Count > 0) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No already added');", True)
                    txtcontainer.Text = ""
                    textqt.Text = ""
                    ddlsize.SelectedValue = "0"
                    txtTrailer.Text = ""
                    txtStuff.Text = ""
                    txtStufwt.Text = ""
                    Exit Sub
                End If
            End If
            If Not txtTrailer.Text = "" Then
                If (ds.Tables(2).Rows.Count > 0) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Trailer No already added');", True)
                    txtcontainer.Text = ""
                    textqt.Text = ""
                    ddlsize.SelectedValue = "0"
                    txtTrailer.Text = ""
                    txtStuff.Text = ""
                    txtStufwt.Text = ""
                    Exit Sub
                End If
            End If
            'txtBalanecw 'txtQty
            If ds.Tables(1).Rows.Count > 0 Then
                'If Val(txtQty.Text) < (Val(ds.Tables(1).Rows(0)("Qty")) + Val(textqt.Text)) Then
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container Qty should not be greater than NOC Qty');", True)
                '    textqt.Text = ""
                '    textqt.Focus()
                '    Exit Sub
                'End If

                If Val(txtDelivered.Text) < (Val(ds.Tables(1).Rows(0)("StuffQTy")) + Val(txtStuff.Text)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container Stuff Quantity should not be greater than delivered Quantity');", True)
                    txtStuff.Text = ""
                    txtStuff.Focus()
                    Exit Sub
                End If

                If Val(txtDeliveredQty.Text) < (Val(ds.Tables(1).Rows(0)("Stuffwt")) + Val(txtStufwt.Text)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container Stuff Weight should not be greater than delivered weight');", True)
                    txtStufwt.Text = ""
                    txtStufwt.Focus()
                    Exit Sub
                End If
            End If
            strSql = ""
            strSql = "USP_Insert_temp_gatepass "
            strSql += "'" & Trim(txtTrailer.Text & "") & "','" & Trim(txtcontainer.Text & "") & "','" & Trim(textqt.Text & "") & "','" & Trim(txtStuff.Text & "") & "','" & Trim(txtStufwt.Text & "") & "','" & Trim(ddlsize.SelectedItem.Text) & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            Container(sender, e)
            txtcontainer.Text = ""
            textqt.Text = ""
            ddlsize.SelectedValue = "0"
            txtTrailer.Text = ""
            txtStuff.Text = ""
            txtStufwt.Text = ""
            up_grid1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Container(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            dt = db.sub_GetDatatable("USP_Select_Temp_gate_pass '" & Session("UserId_BondCFS") & "'")
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            Dim strContainer As String = "", intcontainerA As Integer = 0, intcontainerB As Integer = 0, intcontainerC As Integer = 0
            For i = 0 To dt.Rows.Count - 1
                strContainer = Trim(dt.Rows(i)("Container_Size"))
                If InStr(strContainer, "20") > 0 Then
                    intcontainerA += 1
                ElseIf InStr(strContainer, "40") > 0 Then
                    intcontainerB += 1
                ElseIf InStr(strContainer, "45") > 0 Then
                    intcontainerC += 1
                End If
            Next
            lblA.Text = intcontainerA
            lblB.Text = intcontainerB
            lblC.Text = intcontainerC
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            dt = db.sub_GetDatatable("USP_Delete_Gate_Pass '" & AutoID & "','" & Session("UserId_BondCFS") & "'")
            Container(sender, e)
            If (dt.Rows.Count > 0) Then
            End If
            up_grid1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_INSERT_BONDGATEPASS'" & Convert.ToDateTime(Trim(txtgateDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txttallyNo.Text & "") & "','" & Trim(txtnocno.Text & "") & "',"
            strSql += "'" & Trim(txtBalanec.Text & "") & "','" & Trim(txtBalanecw.Text & "") & "','" & Trim(txtDelivered.Text & "") & "','" & Trim(txtDeliveredQty.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            txtGatePasNo.Text = dt.Rows(0)("GPNo")
            txtgpno.Text = dt.Rows(0)("GPNo")
            Container_In(sender, e)
            Clear(sender, e)
            txttallyNo.Text = ""
            lblSession.Text = "Record saved successfully for Gate Pass No " & dt.Rows(0)("GPNo") & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Container_In(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_gate_Container '" & txtGatePasNo.Text & "'"
            db.sub_ExecuteNonQuery(strSql)
            db.sub_ExecuteNonQuery("USP_INSERT_GATE_PASS_CONTAINER '" & txtGatePasNo.Text & "','" & Trim(txtnocno.Text & "") & "','" & Session("UserId_BondCFS") & "'")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub saveQuoOK_ServerClick(sender As Object, e As EventArgs)
        Try
            lblquoteApprove.Text = "Do you wish to print gate pass?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear(sender As Object, e As EventArgs)
        Try
            txtGatePasNo.Text = ""
            textbond.Text = ""
            txtbe.Text = ""
            txtbedate.Text = ""
            TxtManifested.Text = ""
            txtBalanec.Text = ""
            txtGross.Text = ""
            txtBalanecw.Text = ""
            txtImporter.Text = ""
            txtCha.Text = ""
            txtDescrlption.Text = ""
            txtexBoe.Text = ""
            txtBoeDate.Text = ""
            txtDelivered.Text = ""
            txtDeliveredQty.Text = ""
            txtcontainer.Text = ""
            textqt.Text = ""
            ddlsize.SelectedValue = "0"
            txtTrailer.Text = ""
            txtStuff.Text = ""
            txtStufwt.Text = ""
            txtgateDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            db.sub_ExecuteNonQuery("Delete from Temp_gate_pass Where UniqueID=" & Session("UserId_BondCFS") & "")
            Container(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            Clear(sender, e)
            strSql = ""
            strSql += "select * from Temp_Bond_Gate_Search where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txttallyNo.Text = Trim(dt.Rows(0)("NOCno") & "")
                Call txtnocno_TextChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnTrailers_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select top 1 * from TEMP_GATEPASS_TRAILER where UserID='" & Session("UserId_BondCFS") & "' and NOCNo='" & Trim(txtnocno.Text & "") & "' order by addedon desc"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtTrailer.Text = Trim(dt.Rows(0)("TRAILERNO") & "")
                UpdatePanel3.Update()
                txtStuff.Focus()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
