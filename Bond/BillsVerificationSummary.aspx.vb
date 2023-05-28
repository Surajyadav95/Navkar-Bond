Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt10 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-7)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
            FillDropdown()
            btnSave_Click(sender, e)
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Convert.ToDateTime(Trim(txtfromDate.Text)).ToString("yyyy-MM-dd") > Convert.ToDateTime(Trim(txttoDate.Text)).ToString("yyyy-MM-dd") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid dates');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += " USP_BILLVERIFICATION_SUMMARY '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd") & "','" & Trim(ddlcriteria.SelectedValue & "") & "','" & Trim(ddlVendor.SelectedValue & "") & "','" & Trim(txtVendorBillNo.Text & "") & "',"
            strSql += "'" & Trim(ddlbilltype.SelectedValue & "") & "','" & Trim(ddlActivity.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub

    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlVendor.SelectedValue = 0
            txtVendorBillNo.Text = ""
            ddlbilltype.SelectedValue = 0
            ddlActivity.SelectedValue = 0
            If ddlcriteria.SelectedValue = 0 Then
                divVendor.Attributes.Add("style", "display:none")
                divVendorBillNo.Attributes.Add("style", "display:none")
                divBillType.Attributes.Add("style", "display:none")
                divActivity.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 1 Then
                divVendor.Attributes.Add("style", "display:block")
                divVendorBillNo.Attributes.Add("style", "display:none")
                divBillType.Attributes.Add("style", "display:none")
                divActivity.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 2 Then
                divVendor.Attributes.Add("style", "display:none")
                divVendorBillNo.Attributes.Add("style", "display:block")
                divBillType.Attributes.Add("style", "display:none")
                divActivity.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 3 Then
                divVendor.Attributes.Add("style", "display:none")
                divVendorBillNo.Attributes.Add("style", "display:none")
                divBillType.Attributes.Add("style", "display:block")
                divActivity.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 4 Then
                divVendor.Attributes.Add("style", "display:none")
                divVendorBillNo.Attributes.Add("style", "display:none")
                divBillType.Attributes.Add("style", "display:none")
                divActivity.Attributes.Add("style", "display:block")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub FillDropdown()
        Try
            strSql = ""
            strSql += "USP_BILLVERIFICATION_FILL_DROPDOWN"
            ds = db.sub_GetDataSets(strSql)
            ddlVendor.DataSource = ds.Tables(0)
            ddlVendor.DataTextField = "Name"
            ddlVendor.DataValueField = "VendorId"
            ddlVendor.DataBind()
            ddlVendor.Items.Insert(0, New ListItem("All", 0))

            ddlbilltype.DataSource = ds.Tables(1)
            ddlbilltype.DataTextField = "Name"
            ddlbilltype.DataValueField = "ID"
            ddlbilltype.DataBind()
            ddlbilltype.Items.Insert(0, New ListItem("All", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim VerificationNo As String = lnkcancel.CommandArgument

            strSql = ""
            strSql += "update Vendor_Bill_M set IsCancel=1,CancelledBy='" & Session("UserId_BondCFS") & "',CancelledOn=GETDATE() where VerificationNo='" & Trim(VerificationNo & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            btnSave_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Cancelled successfully');", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += " USP_BILLVERIFICATION_SUMMARY '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd") & "','" & Trim(ddlcriteria.SelectedValue & "") & "','" & Trim(ddlVendor.SelectedValue & "") & "','" & Trim(txtVendorBillNo.Text & "") & "',"
            strSql += "'" & Trim(ddlbilltype.SelectedValue & "") & "','" & Trim(ddlActivity.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Bills Verification" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(4)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(3).Height = 20
                        .Cell(2, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(3, 1).Value = "Bills Verification Details"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Font.FontSize = 17
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=BillsVerification" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
End Class
