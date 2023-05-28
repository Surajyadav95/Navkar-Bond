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
            fill_carting_details()
            txtWorkYear.Text = Session("Workyear")
            btnsearch_Click(sender, e)
        End If
    End Sub  
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Private Sub fill_carting_details()

        Dim strSql As String
        Dim dt As New DataTable
        strSql = ""
        strSql = " usp_fill_invoice_search '" & Trim(txtassessmentno.Text & "") & "','" & Trim(txtWorkYear.Text & "") & "'"
        ds = db.sub_GetDataSets(strSql)
         
        grdListOfContainer.DataSource = ds.Tables(0)
        grdListOfContainer.DataBind()

        grdOtherCharges.DataSource = ds.Tables(1)
        grdOtherCharges.DataBind()

        grdModeOfPayment.DataSource = ds.Tables(2)
        grdModeOfPayment.DataBind()

        strSql = ""
        strSql = "SP_SearchAssessmnet_CreditNote '" & Trim(txtassessmentno.Text) & "','" & Trim(txtWorkYear.Text) & "','Bond'"
        dt = db.sub_GetDatatable(strSql)
        grdCreditNote.DataSource = dt
        grdCreditNote.DataBind()

        grdDiscountAmount.DataSource = ds.Tables(3)
        grdDiscountAmount.DataBind()
    End Sub
  
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "usp_invoice_search'" & Trim(txtassessmentno.Text & "") & "','" & Trim(txtWorkYear.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtassessmentdate.Text = Trim(dt.Rows(0)("Assessment Date") & "")
                txtvalidupto.Text = Trim(dt.Rows(0)("ValidUptoDate") & "")
                txtAssessType.Text = Trim(dt.Rows(0)("BondType") & "")
                txtdeliverytpe.Text = Trim(dt.Rows(0)("DeliveryType") & "")
                txtigmno.Text = Trim(dt.Rows(0)("IGMNo") & "")
                txtitemno.Text = Trim(dt.Rows(0)("ItemNo") & "")
                txttariffid.Text = Trim(dt.Rows(0)("TariffID") & "")
                txtDescription.Text = Trim(dt.Rows(0)("TariffDescription") & "")
                txtchaname.Text = Trim(dt.Rows(0)("CHAName") & "")
                txtImporter.Text = Trim(dt.Rows(0)("ImporterName") & "")
                'txtvalue.Text = Trim(dt.Rows(0)(" ") & "")
                'txtduty.Text = Trim(dt.Rows(0)(" ") & "")
                'txtremarks.Text = Trim(dt.Rows(0)(" ") & "")
                txtAssessmentGenerated.Text = Trim(dt.Rows(0)("Assessmet Users") & "")
                txtAssessmentGeneratedOn.Text = Trim(dt.Rows(0)("AddedOn") & "")
                txt20.Text = Trim(dt.Rows(0)("T20") & "")
                txt40.Text = Trim(dt.Rows(0)("T40") & "")
                'txt45.Text = Trim(dt.Rows(0)("NOCno") & "")
                'txtReceiptGeneratby.Text = Trim(dt.Rows(0)("NOCno") & "")
                'txtReceiptGeneratOn.Text = Trim(dt.Rows(0)("NOCno") & "")
                txtGstinnumber.Text = Trim(dt.Rows(0)("GSTIn_uniqID") & "")
                txtpartyName.Text = Trim(dt.Rows(0)("GSTName") & "")
                txtsgst.Text = Trim(dt.Rows(0)("sgst") & "")
                txtcgst.Text = Trim(dt.Rows(0)("cgst") & "")
                txtigst.Text = Trim(dt.Rows(0)("igst") & "")
                txtNettotal.Text = Trim(dt.Rows(0)("NetTotal") & "")
                txtservicetax.Text = Trim(dt.Rows(0)("ServiceTax") & "")
                txtGrandtotal.Text = Trim(dt.Rows(0)("GrandTotal") & "")
                Call fill_carting_details()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            Clear()
            strSql = ""
            strSql += "select * from Temp_Assess_Search where UserID='" & Session("UserId_BondCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtassessmentno.Text = Trim(dt.Rows(0)("AssessNo") & "")

            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub Clear()
        Try
            txtassessmentdate.Text = ""
            txtvalidupto.Text = ""
            txtAssessType.Text = ""
            txtdeliverytpe.Text = ""
            txtigmno.Text = ""
            txtitemno.Text = ""
            txttariffid.Text = ""
            txtDescription.Text = ""
            txtchaname.Text = ""
            txtImporter.Text = ""
            txtAssessmentGenerated.Text = ""
            txtAssessmentGeneratedOn.Text = ""
            txt20.Text = ""
            txt40.Text = ""
            txtGstinnumber.Text = ""
            txtpartyName.Text = ""
            txtsgst.Text = ""
            txtcgst.Text = ""
            txtigst.Text = ""
            txtNettotal.Text = ""
            txtservicetax.Text = ""
            txtGrandtotal.Text = ""

            strSql = ""
            strSql = ""
            dt = db.sub_GetDatatable(strSql)

            grdListOfContainer.DataSource = dt
            grdListOfContainer.DataBind()

            grdOtherCharges.DataSource = dt
            grdOtherCharges.DataBind()

            grdModeOfPayment.DataSource = dt
            grdModeOfPayment.DataBind()

            grdCreditNote.DataSource = dt
            grdCreditNote.DataBind()

            grdDiscountAmount.DataSource = dt
            grdDiscountAmount.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
