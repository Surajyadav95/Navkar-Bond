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
            Filldropdown()
            txtentryDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtfrom.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtUpTo.Text = Convert.ToDateTime(Now).AddYears(1).ToString("yyyy-MM-dd")
        End If
    End Sub  
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("usp_vendor_tariff")
            'If (ds.Tables(0).Rows.Count > 0) Then
            '    ddlcategory.DataSource = ds.Tables(0)
            '    ddlcategory.DataTextField = "Name"
            '    ddlcategory.DataValueField = "ID"
            '    ddlcategory.DataBind()
            '    ddlcategory.Items.Insert(0, New ListItem("--Select--", 0))
            'End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddlvendorInvoice.DataSource = ds.Tables(1)
                ddlvendorInvoice.DataTextField = "Name"
                ddlvendorInvoice.DataValueField = "ID"
                ddlvendorInvoice.DataBind()
                ddlvendorInvoice.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddlAccount.DataSource = ds.Tables(2)
                ddlAccount.DataTextField = "AccountName"
                ddlAccount.DataValueField = "AccountID"
                ddlAccount.DataBind()
                ddlAccount.Items.Insert(0, New ListItem("--Select--", 0))
            End If


            If (ds.Tables(3).Rows.Count > 0) Then
                ddlservice.DataSource = ds.Tables(3)
                ddlservice.DataTextField = "Service_Name"
                ddlservice.DataValueField = "ID"
                ddlservice.DataBind()
                ddlservice.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(4).Rows.Count > 0) Then
                ddlmaterial.DataSource = ds.Tables(4)
                ddlmaterial.DataTextField = "Material_Name"
                ddlmaterial.DataValueField = "ID"
                ddlmaterial.DataBind()
                ddlmaterial.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(5).Rows.Count > 0) Then
                ddlequipment.DataSource = ds.Tables(5)
                ddlequipment.DataTextField = "Equipment_Name"
                ddlequipment.DataValueField = "AutoID"
                ddlequipment.DataBind()
                ddlequipment.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(6).Rows.Count > 0) Then
                ddlstuffing.DataSource = ds.Tables(6)
                ddlstuffing.DataTextField = "StuffingType"
                ddlstuffing.DataValueField = "TypeId"
                ddlstuffing.DataBind()
                ddlstuffing.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(7).Rows.Count > 0) Then
                ddlbased.DataSource = ds.Tables(7)
                ddlbased.DataTextField = "BasedOn"
                ddlbased.DataValueField = "ID"
                ddlbased.DataBind()
                ddlbased.Items.Insert(0, New ListItem("--Select--", 0))
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_INSERT_SETTINGS_TARIFF_LCC_DETS'" & Convert.ToDateTime(Trim(txtentryDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txttariffId.Text & "") & "','" & Trim(ddlvendorInvoice.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlAccount.SelectedValue & "") & "','" & Trim(ddlservice.SelectedValue & "") & "','" & Trim(ddlmaterial.SelectedValue & "") & "','" & Trim(ddlequipment.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlstuffing.SelectedValue & "") & "','" & Convert.ToDateTime(Trim(txtfrom.Text & "")).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(txtUpTo.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlbased.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlSize.SelectedItem.Text & "") & "','" & Trim(txtvendorrate.Text & "") & "','" & Trim(txtourrate.Text & "") & "','" & Session("UserIDPRE_Bond") & "'"
            dt = db.sub_GetDatatable(strSql)
            lblSession.Text = "Record saved successfully  "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
