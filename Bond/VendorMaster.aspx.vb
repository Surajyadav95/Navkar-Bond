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
    Dim VendorID As String
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
            btnsearch_Click(sender, e)
            If Not (Request.QueryString("VendorIDEdit") = "") Then
                VendorID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("VendorIDEdit")))
                strSql = ""
                strSql = "USP_Edit_Vendor '" & VendorID & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    LblVendorID.Text = Trim(dt.Rows(0)("fuel_VendorID") & "")
                    txtvendorID.Text = Trim(dt.Rows(0)("fuel_VendorID") & "")
                    lblVendorName.Text = Trim(dt.Rows(0)("fuel_VendorName") & "")
                    'lblDriverNo.Text = Trim(dt.Rows(0)("DriverNo") & "")
                    txtvendorName.Text = Trim(dt.Rows(0)("fuel_VendorName") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("isactive") & "")
                    txtAddress.Text = Trim(dt.Rows(0)("address") & "")

                End If

                btnSave.Text = "Update"
            End If
        End If
    End Sub
 Public Sub grid()
        strSql = ""
        strSql += "USP_Get_Fuel_Entry_Dets'" & Trim(txtsearchm.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
     
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
      
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            If (btnSave.Text = "Update") Then
                If Trim(txtvendorName.Text) <> Trim(lblVendorName.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM fuel_vendorM WHERE fuel_VendorName='" & Trim(txtvendorName.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        txtvendorName.Text = ""
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Vendor name already exists .');", True)
                        txtvendorName.Focus()
                        Exit Sub
                    End If
                End If
                strSql = ""
                strSql += "sp_update_vedor '" & Trim(LblVendorID.Text & "") & "','" & Trim(txtvendorName.Text & "") & "','" & Trim(txtAddress.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            Else
                strSql = ""
                strSql += "USP_Vendor_NAME '" & Replace(Trim(txtvendorName.Text & ""), "'", "''") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtvendorName.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Vendor Name already exists .');", True)
                    txtvendorName.Focus()
                    Exit Sub
                End If

                strSql = ""
                strSql += "sp_save_Fuel '" & Trim(txtvendorName.Text & "") & "','" & Trim(txtAddress.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_BondCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "sp_search_Fuel'" & Trim(txtsearchm.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
