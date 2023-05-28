Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim WHID, WHIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Filldropdown()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_NOC_INVOICE_VALIDITY_INVENTORY '" & Trim(txtnoc.Text & "") & "','" & Convert.ToDateTime(Trim(txtStorageVal.Text & "")).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(txtInsuranceVal.Text & "")).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & Convert.ToDateTime(Trim(txtNOCExpiryDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Val(txtArea.Text & "") & "','" & Val(txtWeight.Text & "") & "','" & Trim(ddlCustomer.SelectedValue & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            Call db.AmmendmentLog("" & Trim(txtnoc.Text) & " modify Weight=" & Val(txtWeight.Text & "") & ",Area=" & Val(txtArea.Text & "") & " details.", Session("UserId_BondCFS"), "'Inventory NOC Dets Change'")
            txtNOCExpiryDate.Text = ""
            txtWeight.Text = ""
            txtArea.Text = ""
            txtStorageVal.Text = ""
            txtInsuranceVal.Text = ""
            lblSession.Text = "NOC Details updated for NOC " & txtnoc.Text & ""
            txtnoc.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtnoc_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_NOC_INVENTORY_VALIDITY_UPDATE '" & Trim(txtnoc.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If Not ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('NOC No not found / Only Inventory NOCs are allowed');", True)
                txtnoc.Text = ""
                txtNOCExpiryDate.Text = ""
                txtArea.Text = ""
                txtWeight.Text = ""
                txtStorageVal.Text = ""
                txtInsuranceVal.Text = ""
                Exit Sub
            Else
                If Not Trim(ds.Tables(0).Rows(0)("ExpiryDate")) = "" Then
                    txtNOCExpiryDate.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("ExpiryDate"))).ToString("yyyy-MM-dd")
                End If
                txtArea.Text = Trim(ds.Tables(0).Rows(0)("StorageSpace"))
                txtWeight.Text = Trim(ds.Tables(0).Rows(0)("GrossWt"))
                If ds.Tables(1).Rows.Count > 0 Then
                    txtStorageVal.Text = Convert.ToDateTime(Trim(ds.Tables(1).Rows(0)("StorageFrom"))).ToString("yyyy-MM-dd")
                    txtInsuranceVal.Text = Convert.ToDateTime(Trim(ds.Tables(1).Rows(0)("InsuranceFrom"))).ToString("yyyy-MM-dd")
                End If
            End If
            'If ds.Tables(1).Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invoice Validity for this NOC No already updated.');", True)
            '    txtnoc.Text = ""
            '    txtNOCExpiryDate.Text = ""
            '    txtArea.Text = ""
            '    txtWeight.Text = ""
            '    txtStorageVal.Text = ""
            '    txtInsuranceVal.Text = ""
            '    Exit Sub
            'End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_Fill_Noc_list")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlCustomer.DataSource = ds.Tables(0)
                ddlCustomer.DataTextField = "agentName"
                ddlCustomer.DataValueField = "agentID"
                ddlCustomer.DataBind()
                ddlCustomer.Items.Insert(0, New ListItem("--Select--", 0))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
