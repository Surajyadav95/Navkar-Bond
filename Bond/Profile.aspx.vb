Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim UserID, UserIDEdit As String
    Dim ed As New clsEncodeDecode
    Dim ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillDropdown()
            PopulateDays()
            PopulateMonth()
            PopulateYear()
            If Not (Request.QueryString("UserIDEdit") = "") Then
                UserID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("UserIDEdit")))
                strSql = ""
                strSql = "USP_profile_modify '" & UserID & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblID.Text = Trim(dt.Rows(0)("UserID") & "")
                    txtName.Text = Trim(dt.Rows(0)("UserName") & "")
                    txtlast.Text = Trim(dt.Rows(0)("LastName") & "")
                    txtaddress.Text = Trim(dt.Rows(0)("Address") & "")
                    txtCity.Text = Trim(dt.Rows(0)("City") & "")
                    txtPinCode.Text = Trim(dt.Rows(0)("PINCode") & "")
                    ddlState.SelectedItem.Text = Trim(dt.Rows(0)("State") & "")
                    ddluser.SelectedItem.Text = Trim(dt.Rows(0)("UserType"))
                    txtloging.Text = Trim(dt.Rows(0)("EmpID"))
                    'ddldefaultsite.SelectedValue = Trim(ds.Tables(0).Rows(0)("DefaultSite") & "")
                    ddluser.SelectedValue = Trim(dt.Rows(0)("UserType") & "")
                    'txtdatejoin.Text = Trim(Convert.ToDateTime(ds.Tables(0).Rows(0)("DateOfJoining")).ToString("dd-MM-yyyy") & "")
                    txtEmailID.Text = Trim(dt.Rows(0)("email_ID") & "")
                    txtTelephone.Text = Trim(dt.Rows(0)("Telephone") & "")
                    txtMobileNo.Text = Trim(dt.Rows(0)("ContactNo") & "")
                    ddldesignation.SelectedItem.Text = Trim(dt.Rows(0)("Designation") & "")
                    ddldept.SelectedItem.Text = Trim(dt.Rows(0)("Dept") & "")
                    'imgAvatar.ImageUrl = Trim(dt.Rows(0)("ProfileUrl") & "")
                    If Trim(dt.Rows(0)("Gender")) = "Male" Then
                        rdmale.Checked = True
                    Else
                        rdfemale.Checked = True
                    End If
                    ddlDay.SelectedItem.Text = Trim(dt.Rows(0)("D") & "")
                    ddlMonth.SelectedValue = Trim(dt.Rows(0)("M") & "")
                    ddlYear.SelectedItem.Text = Trim(dt.Rows(0)("Y") & "")

                End If
                btnSave.Text = "Modify"

            End If

            If Not (Request.QueryString("UserIDView") = "") Then
                UserID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("UserIDView")))
                strSql = ""
                strSql = "USP_profile_modify '" & UserID & "'"
                If (dt.Rows.Count > 0) Then
                    dt = db.sub_GetDatatable(strSql)
                    lblID.Text = Trim(dt.Rows(0)("UserID") & "")
                    txtName.Text = Trim(dt.Rows(0)("UserName") & "")
                    txtlast.Text = Trim(dt.Rows(0)("LastName") & "")
                    txtaddress.Text = Trim(dt.Rows(0)("Address") & "")
                    txtCity.Text = Trim(dt.Rows(0)("City") & "")
                    txtPinCode.Text = Trim(dt.Rows(0)("PINCode") & "")
                    ddlState.SelectedItem.Text = Trim(dt.Rows(0)("State") & "")
                    ddluser.SelectedItem.Text = Trim(dt.Rows(0)("UserType"))
                    txtloging.Text = Trim(dt.Rows(0)("EmpID"))
                    'ddldefaultsite.SelectedValue = Trim(ds.Tables(0).Rows(0)("DefaultSite") & "")
                    ddluser.SelectedValue = Trim(dt.Rows(0)("UserType") & "")
                    'txtdatejoin.Text = Trim(Convert.ToDateTime(ds.Tables(0).Rows(0)("DateOfJoining")).ToString("dd-MM-yyyy") & "")
                    txtEmailID.Text = Trim(dt.Rows(0)("email_ID") & "")
                    txtTelephone.Text = Trim(dt.Rows(0)("Telephone") & "")
                    txtMobileNo.Text = Trim(dt.Rows(0)("ContactNo") & "")
                    ddldesignation.SelectedItem.Text = Trim(dt.Rows(0)("Designation") & "")
                    ddldept.SelectedItem.Text = Trim(dt.Rows(0)("Dept") & "")
                    'imgAvatar.ImageUrl = Trim(dt.Rows(0)("ProfileUrl") & "")
                    If Trim(dt.Rows(0)("Gender")) = "Male" Then
                        rdmale.Checked = True
                    Else
                        rdfemale.Checked = True
                    End If
                    ddlDay.SelectedItem.Text = Trim(dt.Rows(0)("D") & "")
                    ddlMonth.SelectedValue = Trim(dt.Rows(0)("M") & "")
                    ddlYear.SelectedItem.Text = Trim(dt.Rows(0)("Y") & "")

                End If
                btnSave.Visible = False
                Panel3.Enabled = False
                Panel2.Enabled = False
                btnSearchImage.Enabled = False
            End If

        End If
    End Sub
    Protected Sub fillDropdown()
        Try

            ds = db.sub_GetDataSets("use_fill_master_profile")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlState.DataSource = ds.Tables(0)
                ddlState.DataTextField = "State"
                ddlState.DataValueField = "State_Code"
                ddlState.DataBind()
                ddlState.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddluser.DataSource = ds.Tables(1)
                ddluser.DataTextField = "Designation"
                ddluser.DataValueField = "AutoID"
                ddluser.DataBind()
                ddluser.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddldesignation.DataSource = ds.Tables(2)
                ddldesignation.DataTextField = "Desi_Name"
                ddldesignation.DataValueField = "Desi_id"
                ddldesignation.DataBind()
                ddldesignation.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If (ds.Tables(3).Rows.Count > 0) Then
                ddldept.DataSource = ds.Tables(3)
                ddldept.DataTextField = "deptName"
                ddldept.DataValueField = "deptId"
                ddldept.DataBind()
                ddldept.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            lbltitle.Text = "Create User"
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub PopulateDays()
        ddlDay.Items.Clear()
        Dim lt As ListItem = New ListItem
        lt.Text = "DD"
        lt.Value = "0"
        'ddlDay.Items.Add(lt)
        Dim days As Integer = DateTime.DaysInMonth(Now.Year, Now.Month)
        Dim i As Integer = 1
        Do While (i <= days)
            lt = New ListItem
            lt.Text = i.ToString
            lt.Value = i.ToString
            ddlDay.Items.Add(lt)
            i = (i + 1)
        Loop
        ddlDay.Items.FindByValue(DateTime.Now.Day.ToString).Selected = True
    End Sub

    Private Sub PopulateMonth()
        Try

            ddlMonth.Items.Clear()
            Dim lt As ListItem = New ListItem
            lt.Text = "MM"
            lt.Value = "0"

            Dim i As Integer = 1

            Do While (i <= 12)
                lt = New ListItem
                Dim monthName As String = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)
                strSql = ""
                strSql += "Select Convert(Varchar(3),'" & monthName & "')"
                dt = db.sub_GetDatatable(strSql)
                lt.Text = Trim(dt.Rows(0)(0) & "")
                lt.Value = i.ToString
                ddlMonth.Items.Add(lt)
                i = (i + 1)
            Loop
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString).Selected = True
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Private Sub PopulateYear()
        ddlYear.Items.Clear()
        Dim lt As ListItem = New ListItem
        lt.Text = "YYYY"
        lt.Value = "0"
        'ddlYear.Items.Add(lt)
        Dim i As Integer = DateTime.Now.Year
        Do While (i >= 1950)
            lt = New ListItem
            lt.Text = i.ToString
            lt.Value = i.ToString
            ddlYear.Items.Add(lt)
            i = (i - 1)
        Loop
        ddlYear.Items.FindByValue(DateTime.Now.Year.ToString).Selected = True
    End Sub
    Public Function GenerateRandomString(ByRef iLength As Integer) As String

        Dim rdm As New Random()
        Dim allowChrs() As Char = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ".ToCharArray()
        Dim sResult As String = ""

        For i As Integer = 0 To iLength - 1
            sResult += allowChrs(rdm.Next(0, allowChrs.Length))
        Next

        Return sResult
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim DOB As String = ddlDay.SelectedItem.Text + "-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedItem.Text
            Dim gender As String
            If (rdfemale.Checked = True) Then
                gender = "Female"
            Else
                gender = "Male"
            End If
            If Trim(ddluser.SelectedValue) = "--SELECT--" Then
                MsgBox("Please select User Type", vbCritical)
                ddluser.Focus()
                Exit Sub
            End If
            If btnSave.Text = "Save" Then
                strSql = ""
                strSql = "select Count(*) from UserDetails where firstname='" & Trim(txtName.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtName.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('User with same name exists');", True)
                    Exit Sub
                End If
                strSql = ""
                strSql += "USP_create_user_insert "
                strSql += "'" & Trim(txtName.Text & "") & "','" & Trim(txtlast.Text & "") & "','" & Trim(txtloging.Text & "") & "','" & gender & "','" & Trim(Convert.ToDateTime(DOB).ToString("yyyy-MM-dd") & "") & "',"
                strSql += "'" & Trim(txtaddress.Text & "") & "','" & Trim(txtCity.Text) & "','" & Trim(ddlState.SelectedItem.Text) & "','" & Trim(txtPinCode.Text) & "',"
                strSql += "'" & Trim(ddluser.SelectedItem.Text) & "','" & Trim(txtEmailID.Text) & "','" & Trim(txtTelephone.Text) & "','" & Trim(txtMobileNo.Text) & "',"
                strSql += "'" & Trim(ddldesignation.SelectedItem.Text) & "','" & Trim(ddldept.SelectedItem.Text) & "'"
                dt = db.sub_GetDatatable(strSql)

                If (dt.Rows.Count > 0) Then
                    hdautoid.Value = dt.Rows(0)(0)
                End If



againGenerateName: Dim strFileName As String = ""
                Dim strFilePath As String = ""

                Dim number As String = GenerateRandomString(5)


                strFileName = btnSearchImage.FileName
                strFileName = btnSearchImage.PostedFile.FileName
                strFilePath = "~\images\UserProfilePic\" & Trim(number) & ".jpg"

                Dim fileName As String = Path.GetFileName("" & Trim(number) & "")
                Dim strFileSavePath As String = (Server.MapPath("~/images/UserProfilePic/"))

                If (File.Exists(Server.MapPath(strFilePath))) Then
                    GoTo againGenerateName1
                End If


                If Trim(strFileName) <> "" And Trim(strFilePath) <> "" Then
                    btnSearchImage.SaveAs(Server.MapPath(strFilePath))

                    Dim strimgPath As String = "~\images\UserProfilePic\ " & Trim(number) & ".jpg"


                    Dim strSQL As String = ""

                    strSQL = "UPDATE UserDetails SET FileName='" & strimgPath & "' where UserID=" & Trim(hdautoid.Value & "") & ""
                    If db.sub_ExecuteNonQuery(strSQL) > 0 Then
                        Session("FileName") = strimgPath
                        imgAvatar.ImageUrl = strimgPath
                        UP_image.Update()
                    End If
                Else
                    strFilePath = ""
                End If
                lblSession.Text = "Record saved successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()

            Else
                strSql = ""
                strSql += "USP_Update_User_Profile "
                strSql += "'" & Trim(lblID.Text & "") & "','" & Trim(txtName.Text & "") & "','" & Trim(txtlast.Text & "") & "','" & Trim(txtloging.Text & "") & "','" & gender & "','" & Trim(Convert.ToDateTime(DOB).ToString("yyyy-MM-dd") & "") & "',"
                strSql += "'" & Trim(txtaddress.Text & "") & "','" & Trim(txtCity.Text) & "','" & Trim(ddlState.SelectedItem.Text) & "','" & Trim(txtPinCode.Text) & "',"
                strSql += "'" & Trim(ddluser.SelectedItem.Text) & "','" & Trim(txtEmailID.Text) & "','" & Trim(txtTelephone.Text) & "','" & Trim(txtMobileNo.Text) & "',"
                strSql += "'" & Trim(ddldesignation.SelectedItem.Text) & "','" & Trim(ddldept.SelectedItem.Text) & "'"
                dt = db.sub_GetDatatable(strSql)

                If (dt.Rows.Count > 0) Then
                    hdautoid.Value = dt.Rows(0)(0)
                End If



againGenerateName1: Dim strFileName As String = ""
                Dim strFilePath As String = ""

                Dim number As String = GenerateRandomString(5)


                strFileName = btnSearchImage.FileName
                strFileName = btnSearchImage.PostedFile.FileName
                strFilePath = "~\images\UserProfilePic\" & Trim(number) & ".jpg"

                Dim fileName As String = Path.GetFileName("" & Trim(number) & "")
                Dim strFileSavePath As String = (Server.MapPath("~/images/UserProfilePic/"))

                If (File.Exists(Server.MapPath(strFilePath))) Then
                    GoTo againGenerateName1
                End If


                If Trim(strFileName) <> "" And Trim(strFilePath) <> "" Then
                    btnSearchImage.SaveAs(Server.MapPath(strFilePath))

                    Dim strimgPath As String = "~\images\UserProfilePic\ " & Trim(number) & ".jpg"


                    Dim strSQL As String = ""

                    strSQL = "UPDATE UserDetails SET FileName='" & strimgPath & "' where UserID=" & Trim(hdautoid.Value & "") & ""
                    If db.sub_ExecuteNonQuery(strSQL) > 0 Then
                        Session("FileName") = strimgPath
                        imgAvatar.ImageUrl = strimgPath
                        UP_image.Update()
                    End If
                Else
                    strFilePath = ""
                End If
                lblSession.Text = "Record modified successfully for User ID " & lblID.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()

            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function
End Class
