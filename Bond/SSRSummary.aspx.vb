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
            Filldropdown()
            btnSave_Click(sender, e)
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_SSR_SUMMARY_FILLDROPDOWN")

            ddlcha.DataSource = ds.Tables(0)
            ddlcha.DataTextField = "CHAName"
            ddlcha.DataValueField = "CHAID"
            ddlcha.DataBind()
            ddlcha.Items.Insert(0, New ListItem("--Select--", 0))

            ddlcustomer.DataSource = ds.Tables(2)
            ddlcustomer.DataTextField = "CustomerName"
            ddlcustomer.DataValueField = "Cust_ID"
            ddlcustomer.DataBind()
            ddlcustomer.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim SearchText As String = ""
            If ddlCategory.SelectedValue = "All" Then
                SearchText = ""
            ElseIf ddlCategory.SelectedValue = "NOC No" Then
                SearchText = Trim(txtnocnosearch.Text & "")
            ElseIf ddlCategory.SelectedValue = "Customer" Then
                SearchText = Trim(ddlcustomer.SelectedValue & "")
            ElseIf ddlCategory.SelectedValue = "CHA" Then
                SearchText = Trim(ddlcha.SelectedValue & "")
            End If
            strSql = ""
            strSql += " USP_SSR_SUMMARY '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & ddlCategory.SelectedValue & "','" & SearchText & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub
    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        Try
            ddlcha.SelectedValue = 0
            ddlcustomer.SelectedValue = 0
            txtnocnosearch.Text = ""
            If (ddlCategory.SelectedValue = "CHA") Then
                divCHA.Attributes.Add("style", "display:block")
            Else
                divCHA.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "Customer") Then
                divCustomer.Attributes.Add("style", "display:block")
            Else
                divCustomer.Attributes.Add("style", "display:None")
            End If

            If (ddlCategory.SelectedValue = "NOC No") Then
                divnocno.Attributes.Add("style", "display:block")
            Else
                divnocno.Attributes.Add("style", "display:None")
            End If
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
            Dim SearchText As String = ""
            If ddlCategory.SelectedValue = "All" Then
                SearchText = ""
            ElseIf ddlCategory.SelectedValue = "NOC No" Then
                SearchText = Trim(txtnocnosearch.Text & "")
            ElseIf ddlCategory.SelectedValue = "Customer" Then
                SearchText = Trim(ddlcustomer.SelectedValue & "")
            ElseIf ddlCategory.SelectedValue = "CHA" Then
                SearchText = Trim(ddlcha.SelectedValue & "")
            End If
            strSql = ""
            strSql += " USP_SSR_SUMMARY '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & ddlCategory.SelectedValue & "','" & SearchText & "'"
            dt = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "SSR Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
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
                        .Cell(3, 1).Value = "SSR Details"
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
                    Response.AddHeader("content-disposition", "attachment;filename=SSRSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
