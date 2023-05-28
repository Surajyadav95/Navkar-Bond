Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Configuration

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt10 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("delete from Temp_BondNotice where UserID=" & Session("UserId_BondCFS") & "")
            txtAsOnDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtForMonth.Text = 3
            btnsearch_Click(sender, e)
        End If
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_BONDNOTICE_FILLGRID '" & Convert.ToDateTime(txtAsOnDate.Text).ToString("yyyy-MM-dd") & "','" & Val(txtForMonth.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try            
            strSql = ""
            strSql += "USP_BONDNOTICE_FILLGRID '" & Convert.ToDateTime(txtAsOnDate.Text).ToString("yyyy-MM-dd") & "','" & Val(txtForMonth.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            dt = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Noc Register" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
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
                        .Cell(2, 1).Value = "As On Date: " + Convert.ToDateTime(txtAsOnDate.Text).ToString("dd MMM yyyy")
                        .Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(3, 1).Value = "Bond Notice Details"
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
                    Response.AddHeader("content-disposition", "attachment;filename=BondNotice" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
    
    Protected Sub grdcontainer_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                strSql = ""
                strSql += "SELECT TOP 1 Noticeno FROM Bond_Notice WHERE BondNo='" & CType(e.Row.FindControl("lblBondNo"), Label).Text & "' AND BOENO='" & CType(e.Row.FindControl("lblBOENo"), Label).Text & "'  AND IsCancel=0 ORDER BY noticeid DESC"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0)("Noticeno")) = 1 Then
                        e.Row.Attributes.Add("style", "background-color:aqua")
                    ElseIf Val(dt.Rows(0)("Noticeno")) = 2 Then
                        e.Row.Attributes.Add("style", "background-color:pink")
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkNotice1_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkNotice1 As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkNotice1.Parent.Parent, GridViewRow)
            Dim BondNo As String = lnkNotice1.CommandArgument
            Dim BOENo As String = grdcontainer.DataKeys(row.RowIndex).Value.ToString()
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            Dim intMaxID As Integer
            strSql = ""
            strSql += "USP_BONDNOTICE_VALIDATION_FOR_NOTICE1 '" & BondNo & "','" & BOENo & "'"
            ds = db.sub_GetDataSets(strSql)
            intMaxID = Val(ds.Tables(1).Rows(0)(0)) + 1
            db.sub_ExecuteNonQuery("delete from Temp_BondNotice where UserID=" & Session("UserId_BondCFS") & "")
            strSql = ""
            strSql += "USP_BONDNOTICE_INSERT_INTO_TEMP_BONDNOTICE " & intMaxID & ",'" & BondNo & "','" & Trim(CType(gr.FindControl("lblDescription"), Label).Text.ToString()) & "',"
            strSql += "'" & Convert.ToDateTime(Trim(CType(gr.FindControl("lblBondExpiryDate"), Label).Text.ToString())).ToString("yyyy-MM-dd") & "','" & Trim(CType(gr.FindControl("lblBalanceQty"), Label).Text.ToString()) & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblBalanceDuty"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblBOENo"), Label).Text.ToString()) & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblImporterName"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblImporterAddress"), Label).Text.ToString()) & "',"
            strSql += "'" & Convert.ToDateTime(Trim(CType(gr.FindControl("lblBondDate"), Label).Text.ToString())).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(CType(gr.FindControl("lblBOEDate"), Label).Text.ToString())).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblManifestQty"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblExQty"), Label).Text.ToString()) & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblCHAName"), Label).Text.ToString()) & "','" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)

            If ds.Tables(0).Rows.Count > 0 Then
                lblsession.Text = "Bond Notice 1 already sent. Do you want to resend the same notice again?"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel6.Update()
                Exit Sub
            Else
                strSql = ""
                strSql += "USP_BONDNOTICE_SAVE_NOTICE_INTO_BOND_NOTICE " & intMaxID & ",'" & BondNo & "','" & BOENo & "','" & Trim(CType(gr.FindControl("lblBalanceQty"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblBalanceDuty"), Label).Text.ToString()) & "',1"
                db.sub_ExecuteNonQuery(strSql)
            End If
            btnsearch_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:OpenNotice1Print();", True)
            'db.sub_ExecuteNonQuery("delete from Temp_BondNotice where UserID=" & Session("UserId_BondCFS") & "")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkNotice2_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkNotice1 As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkNotice1.Parent.Parent, GridViewRow)
            Dim BondNo As String = lnkNotice1.CommandArgument
            Dim BOENo As String = grdcontainer.DataKeys(row.RowIndex).Value.ToString()
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            Dim intMaxID As Integer
            strSql = ""
            strSql += "USP_BONDNOTICE_VALIDATION_FOR_NOTICE2 '" & BondNo & "','" & BOENo & "'"
            ds = db.sub_GetDataSets(strSql)
            intMaxID = Val(ds.Tables(2).Rows(0)(0)) + 1
            db.sub_ExecuteNonQuery("delete from Temp_BondNotice where UserID=" & Session("UserId_BondCFS") & "")
            strSql = ""
            strSql += "USP_BONDNOTICE_INSERT_INTO_TEMP_BONDNOTICE " & intMaxID & ",'" & BondNo & "','" & Trim(CType(gr.FindControl("lblDescription"), Label).Text.ToString()) & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblBondExpiryDate"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblBalanceQty"), Label).Text.ToString()) & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblBalanceDuty"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblBOENo"), Label).Text.ToString()) & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblImporterName"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblImporterAddress"), Label).Text.ToString()) & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblBondDate"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblBOEDate"), Label).Text.ToString()) & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblManifestQty"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblExQty"), Label).Text.ToString()) & "',"
            strSql += "'" & Trim(CType(gr.FindControl("lblCHAName"), Label).Text.ToString()) & "','" & Session("UserId_BondCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            If Not ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Bond Notice 1 not yet sent. Cannot generate Notice 2.');", True)
                Exit Sub
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                lblsession2.Text = "Bond Notice 2 already sent. Do you want to resend the same notice again?"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
                UpdatePanel1.Update()
                Exit Sub
            Else              
                strSql = ""
                strSql += "USP_BONDNOTICE_SAVE_NOTICE_INTO_BOND_NOTICE " & intMaxID & ",'" & BondNo & "','" & BOENo & "','" & Trim(CType(gr.FindControl("lblBalanceQty"), Label).Text.ToString()) & "','" & Trim(CType(gr.FindControl("lblBalanceDuty"), Label).Text.ToString()) & "',2"
                db.sub_ExecuteNonQuery(strSql)
            End If
            btnsearch_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:OpenNotice2Print();", True)

            'db.sub_ExecuteNonQuery("delete from Temp_BondNotice where UserID=" & Session("UserId_BondCFS") & "")

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnYesNotice1_ServerClick(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select * from Temp_BondNotice where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                strSql = ""
                strSql += "USP_BONDNOTICE_SAVE_NOTICE_INTO_BOND_NOTICE " & Val(dt.Rows(0)("NoticeID")) & ",'" & Trim(dt.Rows(0)("BondNo")) & "','" & Trim(dt.Rows(0)("BOENo")) & "','" & Trim(dt.Rows(0)("BQty")) & "','" & Trim(dt.Rows(0)("BDuty")) & "',1"
                db.sub_ExecuteNonQuery(strSql)
            End If
            'db.sub_ExecuteNonQuery("delete from Temp_BondNotice where UserID=" & Session("UserId_BondCFS") & "")
            btnsearch_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:OpenNotice1Print();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnyesnotice2_ServerClick(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select * from Temp_BondNotice where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                strSql = ""
                strSql += "USP_BONDNOTICE_SAVE_NOTICE_INTO_BOND_NOTICE " & Val(dt.Rows(0)("NoticeID")) & ",'" & Trim(dt.Rows(0)("BondNo")) & "','" & Trim(dt.Rows(0)("BOENo")) & "','" & Trim(dt.Rows(0)("BQty")) & "','" & Trim(dt.Rows(0)("BDuty")) & "',2"
                db.sub_ExecuteNonQuery(strSql)
            End If
            'db.sub_ExecuteNonQuery("delete from Temp_BondNotice where UserID=" & Session("UserId_BondCFS") & "")
            btnsearch_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:OpenNotice2Print();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
