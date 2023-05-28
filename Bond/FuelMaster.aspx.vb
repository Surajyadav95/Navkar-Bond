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
    Dim TariffID, TariffIDView As String
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
        'End If
        If Not IsPostBack Then

            txtfueldate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            btnsearch_Click(sender, e)
            Filldropdown()
            txtFuelRegNo.Text = ""
            ddlFuelType.Focus()
        End If
    End Sub
 Public Sub grid()
        strSql = ""
        strSql += "USP_Get_Fuel_Entry_Dets'" & Trim(txtsearchm.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("usp_fuel_fill_Dropdown")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlFuelType.DataSource = ds.Tables(0)
                ddlFuelType.DataTextField = "FuelType"
                ddlFuelType.DataValueField = "Fuelid"
                ddlFuelType.DataBind()
                ddlFuelType.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddlCostCenter.DataSource = ds.Tables(1)
                ddlCostCenter.DataTextField = "cost_center"
                ddlCostCenter.DataValueField = "id"
                ddlCostCenter.DataBind()
                ddlCostCenter.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddlVendorName.DataSource = ds.Tables(2)
                ddlVendorName.DataTextField = "fuel_VendorName"
                ddlVendorName.DataValueField = "fuel_VendorID"
                ddlVendorName.DataBind()
                ddlVendorName.Items.Insert(0, New ListItem("--Select--", 0))
            End If
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
            strSql = "USP_INSERT_FUELM'" & Convert.ToDateTime(Trim(txtfueldate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlFuelType.SelectedItem.Text & "") & "','" & Trim(txtVehicleNo.Text & "") & "','" & Trim(ddlCostCenter.SelectedValue & "") & "',"
            strSql += "'" & Trim(txtFuelQty.Text & "") & "','" & Trim(txtRate.Text & "") & "','" & Trim(ddlVendorName.SelectedItem.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            lblSession.Text = "Record Saved successfully  "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_Get_Fuel_Entry_Dets'" & Trim(txtsearchm.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim FuelRegID As String = lnkcancel.CommandArgument
            'Dim WorkYear As String = grdSummary.DataKeys(row.RowIndex).Value.ToString()
            Dim str As String = ""
            txtFuelRegNo.Text = FuelRegID
            'TxtWorkYear.Text = WorkYear
           
            ClientScript.RegisterStartupScript(Page.GetType(), "OpenList", "<script>OpenCancelInvoice(); </script>")

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
