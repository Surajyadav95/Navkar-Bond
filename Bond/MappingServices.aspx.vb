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

            txtentryDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            btnsearch_Click(sender, e)
            Filldropdown()
            ddlAccount.Focus()
        End If
    End Sub
 Public Sub grid()
        strSql = ""
        strSql += "SP_Category_Wise_Services'" & Trim(txtsearchm.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("usp_Mapping_Services_Fill")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlAccount.DataSource = ds.Tables(0)
                ddlAccount.DataTextField = "AccountName"
                ddlAccount.DataValueField = "AccountID"
                ddlAccount.DataBind()
                ddlAccount.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddlservice.DataSource = ds.Tables(1)
                ddlservice.DataTextField = "Service_Name"
                ddlservice.DataValueField = "ID"
                ddlservice.DataBind()
                ddlservice.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddlmaterial.DataSource = ds.Tables(2)
                ddlmaterial.DataTextField = "Material_Name"
                ddlmaterial.DataValueField = "ID"
                ddlmaterial.DataBind()
                ddlmaterial.Items.Insert(0, New ListItem("--Select--", 0))
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
            strSql += "SELECT Count(*) FROM Category_Wise_Services_M WHERE  AccountID='" & Trim(ddlAccount.SelectedValue & "") & "' and Service_Type_ID='" & Trim(ddlservice.SelectedValue & "") & "' and Material_Type_ID='" & Trim(ddlmaterial.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows(0)(0) >= 1) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This mapping services already exists.');", True)
                ddlAccount.SelectedValue = 0
                ddlservice.SelectedValue = 0
                ddlmaterial.SelectedValue = 0
                ddlAccount.Focus()
                'MsgBox("This mapping services already exists", vbInformation)
                Exit Sub
            End If

            strSql = ""
            strSql = "USP_List_Of_Mapped_Services'" & Convert.ToDateTime(Trim(txtentryDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlAccount.SelectedValue & "") & "','" & Trim(ddlservice.SelectedValue & "") & "','" & Trim(ddlmaterial.SelectedValue & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserIDPRE_Bond") & "'"
            dt = db.sub_GetDatatable(strSql)
            lblSession.Text = "Record Saved successfully  "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try '
            strSql = ""
            strSql += "SP_Category_Wise_Services'" & Trim(txtsearchm.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
