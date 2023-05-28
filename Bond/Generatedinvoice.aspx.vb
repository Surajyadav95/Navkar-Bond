Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            'txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
            Filldropdown()
            btnsearch_Click(sender, e)
        End If
    End Sub
    Public Sub grid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_Fill_gate_BOND_IN_register")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlcha.DataSource = ds.Tables(0)
                ddlcha.DataTextField = "CHAName"
                ddlcha.DataValueField = "CHAID"
                ddlcha.DataBind()
                ddlcha.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddlimpor.DataSource = ds.Tables(1)
                ddlimpor.DataTextField = "ImporterName"
                ddlimpor.DataValueField = "ImporterID"
                ddlimpor.DataBind()
                ddlimpor.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If (ds.Tables(2).Rows.Count > 0) Then
                ddlcustomer.DataSource = ds.Tables(2)
                ddlcustomer.DataTextField = "CustomerName"
                ddlcustomer.DataValueField = "Cust_ID"
                ddlcustomer.DataBind()
                ddlcustomer.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            ddlWarehouse.DataSource = ds.Tables(3)
            ddlWarehouse.DataTextField = "warehouse_code"
            ddlWarehouse.DataValueField = "warehouse_code"
            ddlWarehouse.DataBind()
            ddlWarehouse.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlcha.SelectedValue = 0
            ddlimpor.SelectedValue = 0
            ddlcustomer.SelectedValue = 0
            ddlWarehouse.SelectedValue = 0
            If (ddlCategory.SelectedValue = "1") Then
                divCHA.Attributes.Add("style", "display:block")
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0

            Else
                divCHA.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "2") Then
                divImpoeter.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlcustomer.SelectedValue = 0

            Else
                divImpoeter.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "3") Then
                divCustomer.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0

            Else
                divCustomer.Attributes.Add("style", "display:None")
            End If
            
            If (ddlCategory.SelectedValue = "4") Then
                divNoc.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divNoc.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "5") Then
                divNoc.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divcargo.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "6") Then
                divWarehouse.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divWarehouse.Attributes.Add("style", "display:None")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            'Dim Fromdate As Date = "2018-10-01"
            strSql = ""
            strSql += " USP_Generated_Invoice_search '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 23:59:00 ") & "',"
            strSql += "'" & Trim(ddlCategory.SelectedValue & "") & "','" & Trim(ddlcha.SelectedItem.Text & "") & "',"
            strSql += "'" & Trim(ddlimpor.SelectedItem.Text & "") & "','" & Trim(ddlcustomer.SelectedItem.Text & "") & "',"
            strSql += "'" & Trim(txtNoc.Text & "") & "','" & Trim(ddlWarehouse.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub

    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            'Dim Fromdate As Date = "2018-10-01"
            strSql = ""
            strSql += " USP_Generated_Invoice_search '" & Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy 23:59:00 ") & "',"
            strSql += "'" & Trim(ddlCategory.SelectedValue & "") & "','" & Trim(ddlcha.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlimpor.SelectedValue & "") & "','" & Trim(ddlcustomer.SelectedValue & "") & "',"
            strSql += "'" & Trim(txtNoc.Text & "") & "','" & Trim(ddlWarehouse.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Pending Invoices " & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=ListofPendingInvoice" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        'getdatewiseWO()
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No record found!');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            lblCancel.Text = ""
            Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
            Dim Auto_ID As String = lnkbtn.CommandArgument
            'Dim ReceiptNo As String = grdcontainer.DataKeys(gvrow.RowIndex).Value.ToString()
            lblCancel.Text = Auto_ID
            lblModifyTitle.Text = "Enter not required reason for Ex No. " & Auto_ID & " "
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            upModalCancel.Update()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If (txtremarks.Text = "") Then
                ScriptManager.RegisterStartupScript(btnsave, btnsave.GetType, "Key", "alert('Please fill the required fields!');", True)
                txtremarks.BorderColor = System.Drawing.Color.Red
                Exit Sub
            End If

            Dim strAddress As String = ""
            strAddress = Replace(Trim(txtremarks.Text), "'", "''")
            strSql = ""
            strSql += "USP_NOT_REQUIRED_DOCUMENT " & lblCancel.Text & ",'" & strAddress & "','" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            txtremarks.Text = ""
            upModalCancel.Update()
            btnsearch_Click(sender, e)
            'lblSession.Text = "Record Cancelled Successfully"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            'UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
End Class
