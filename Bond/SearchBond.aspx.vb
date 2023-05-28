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
            fill_carting_details()
        End If
    End Sub  
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Private Sub fill_carting_details()

        Dim strSql As String
        Dim dt As New DataTable
        strSql = ""
        strSql = " GET_Sp_Bond_Search_Summary_New '" & Trim(txtNoc.Text & "") & "'"
        ds = db.sub_GetDataSets(strSql)
        grdInDetails.DataSource = ds.Tables(0)
        grdInDetails.DataBind()


        grdbondex.DataSource = ds.Tables(1)
        grdbondex.DataBind()

        grdInvoice.DataSource = ds.Tables(2)
        grdInvoice.DataBind()

        grdBondGatePass.DataSource = ds.Tables(3)
        grdBondGatePass.DataBind()

        grdBalance.DataSource = ds.Tables(5)
        grdBalance.DataBind()

        grdContainer.DataSource = ds.Tables(4)
        grdContainer.DataBind()
    End Sub
    Protected Sub txtNoc_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "GET_sp_NOCSearchDtels'" & Trim(txtNoc.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                'txtNoc.Text = Trim(dt.Rows(0)("NOCno") & "")
                txtbondtype.Text = Trim(dt.Rows(0)("BondType"))
                txtNocdate.Text = Trim(dt.Rows(0)("NOCDate") & "")
                txtNocValid.Text = Trim(dt.Rows(0)("ExpiryDate") & "")
                txtbeno.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtbedate.Text = Trim(dt.Rows(0)("BOEDate"))
                txtImporter.Text = Trim(dt.Rows(0)("ImporterName" & ""))
                txtCha.Text = Trim(dt.Rows(0)("CHAName") & "")
                txtbondNo.Text = Trim(dt.Rows(0)("BondNo") & "")
                txtBondDate.Text = Trim(dt.Rows(0)("BondDate") & "")
                txtbondExpiry.Text = Trim(dt.Rows(0)("BondExpiryDate") & "")
                txtLocation.Text = Trim(dt.Rows(0)("Location") & "")
                txtSerial.Text = Trim(dt.Rows(0)("SerialNo") & "")
                txtstatus.Text = Trim(dt.Rows(0)("Status") & "")
                txtBondReg.Text = Trim(dt.Rows(0)("RegNo") & "")
                txtCargo.Text = Trim(dt.Rows(0)("Commodity") & "")
                Call fill_carting_details()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try           
            'Control_Clear(sender, e)
            strSql = ""
            strSql += "select * from Temp_Noc_Search where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtNoc.Text = Trim(dt.Rows(0)("NOCno") & "")
                Call txtNoc_TextChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
        
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Call Indetailes()
    End Sub
    Protected Sub Indetailes()

        Try
            strSql = ""
            strSql = " GET_Sp_Bond_Search_Summary_New '" & Trim(txtNoc.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)

            strSql = ""
            strSql = "GET_sp_NOCSearchDtels'" & Trim(txtNoc.Text & "") & "','" & Session("UserId_BondCFS") & "'"
            dt1 = db.sub_GetDatatable(strSql)

            dt1.Columns.Remove("ExpiryDate")

            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt1.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    Dim introw As Integer = 4

                    wb.Worksheets.Add(dt1, "Serach" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")

                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt1.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(4)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(3).Height = 20

                        .Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(3, 1).Value = "Search Bond"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Font.FontSize = 17

                        introw = introw + 3 + dt1.Rows.Count
                        wb.Worksheet(1).Cell(introw, 1).InsertTable(ds.Tables(0))

                        introw += 2 + ds.Tables(0).Rows.Count
                        wb.Worksheet(1).Cell(introw, 1).InsertTable(ds.Tables(1))

                        introw += 2 + ds.Tables(1).Rows.Count
                        wb.Worksheet(1).Cell(introw, 1).InsertTable(ds.Tables(2))

                        introw += 2 + ds.Tables(2).Rows.Count
                        wb.Worksheet(1).Cell(introw, 1).InsertTable(ds.Tables(3))

                        introw += 2 + ds.Tables(3).Rows.Count
                        wb.Worksheet(1).Cell(introw, 1).InsertTable(ds.Tables(4))

                        introw += 2 + ds.Tables(4).Rows.Count
                        wb.Worksheet(1).Cell(introw, 1).InsertTable(ds.Tables(5))
                    End With

                   

                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=SearchBond" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
