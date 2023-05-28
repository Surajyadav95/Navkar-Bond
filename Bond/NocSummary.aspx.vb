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
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-7)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
            Filldropdown()
            btnsearch_Click(sender, e)
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_Fill_List_NOCSummary")
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
            ddlWarehouse.DataTextField = "Warehouse_code"
            ddlWarehouse.DataValueField = "Warehouse_code"
            ddlWarehouse.DataBind()
            ddlWarehouse.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If (ddlCategory.SelectedValue = "1") Then
                divCHA.Attributes.Add("style", "display:block")
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                ddlBondType.SelectedValue = 0
                ddlWarehouse.SelectedValue = 0

            Else
                divCHA.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "2") Then
                divImpoeter.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                ddlBondType.SelectedValue = 0
                ddlWarehouse.SelectedValue = 0
            Else
                divImpoeter.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "3") Then
                divCustomer.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlBondType.SelectedValue = 0
                ddlWarehouse.SelectedValue = 0
            Else
                divCustomer.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "4") Then
                divBond.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                ddlWarehouse.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divBond.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "5") Then
                divNoc.Attributes.Add("style", "display:block")
                ddlcha.SelectedValue = 0
                ddlimpor.SelectedValue = 0
                ddlcustomer.SelectedValue = 0
                ddlWarehouse.SelectedValue = 0
                txtNoc.Text = ""
            Else
                divNoc.Attributes.Add("style", "display:None")
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
            strSql = ""
            strSql += " USP_NOC_Summary '" & Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy 23:59:00 ") & "',"
            strSql += "'" & Trim(ddlCategory.SelectedValue & "") & "','" & Trim(ddlcha.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlimpor.SelectedValue & "") & "','" & Trim(ddlcustomer.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlBondType.SelectedValue & "") & "','" & Trim(txtNoc.Text & "") & "','" & Trim(ddlWarehouse.SelectedValue & "") & "'"
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
    Protected Sub btnprint_Click(sender As Object, e As EventArgs)
        Try
            For Each row In grdcontainer.Rows
                Dim NOCNo As RadioButton = CType(row.FindControl("redbutton"), RadioButton)
                Dim NOCNo1 As String = CType(row.FindControl("lblNOCNo"), Label).Text.ToString()
                If NOCNo.Checked = True Then
                    Dim redirect As String = "<script>window.open('../Report_Bond/BondLogo_Print.aspx?NOCNo=" & NOCNo1 & "');</script>"
                    Response.Write(redirect)
                    'Response.Redirect("../Report_Bond/BondLogo_Print.aspx?NOCNo=" + NOCNo1)

                End If
            Next

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub


    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function
End Class
