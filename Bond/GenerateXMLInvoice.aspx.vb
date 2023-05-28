Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel
Imports Outlook = Microsoft.Office.Interop.Outlook

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim intYrID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtAsOnDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            btnsearch_Click(sender, e)
        End If
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_NOC_Assessment_summary_XML '" & Convert.ToDateTime(txtAsOnDate.Text).ToString("yyyy-MM-dd 23:59:00 ") & "'"            
            dt = db.sub_GetDatatable(strSql)
            grdSummary.DataSource = dt
            grdSummary.DataBind()
            'up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    
    Protected Sub btnXMLInvoice_Click(sender As Object, e As EventArgs) Handles btnXMLInvoice.Click
        Try
            Dim dblChkCount As Double = 0
            For Each row In grdSummary.Rows
                If CType(row.findcontrol("chkInv"), CheckBox).Checked = True Then
                    dblChkCount += 1
                End If
            Next
            If dblChkCount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select atleast one invoice');", True)
                Exit Sub
            End If
            Dim strXml As String
            Dim dt As New DataTable
            Const HEADER As String = ""
            Dim TAG_BEGIN As String
            Dim TAG_END As String
            Dim intR As Integer
            Dim intfilenum As Integer
            Dim strComb As String
            Dim strRemoteGUIID As String
            Dim strVCHKey As String
            Dim strVarIDA As String
            Dim strVarIDB As String
            Dim strVarIDC As String
            Dim strVarIDD As String
            Dim dtinvoicedate As DateTime
            Dim dblAlertID As Double
            Dim dblMasterID As Double
            Dim dblVoucherKey As Double
            Dim dblTotalA_B As Double
            Dim dblRemoteID As Double, dblVCHKEY As Double
            Dim StrNarration As String
            Dim strcontdets As String
            Dim strsbno As String
            Dim filename As String
            Dim dblcount As Double
            Dim intRow As Integer
            'lblPleaseWait.Visible = True
            dblcount = 1
            TAG_BEGIN = "<ENVELOPE>"
            TAG_END = "</ENVELOPE>"

            strXml = HEADER
            strXml = strXml & vbCrLf & TAG_BEGIN
            strXml = strXml & vbCrLf & "<HEADER>"
            strXml = strXml & vbCrLf & "<TALLYREQUEST>" & "Import Data" & "</TALLYREQUEST>"     'Line 1
            strXml = strXml & vbCrLf & "</HEADER>"
            strXml = strXml & vbCrLf & "<BODY>" & vbCrLf
            strXml = strXml & vbCrLf & "<IMPORTDATA>"
            strXml = strXml & vbCrLf & "<REQUESTDESC>"
            strXml = strXml & vbCrLf & "<REPORTNAME>" & "Vouchers" & "</REPORTNAME>" & vbCrLf   'Line 2
            strXml = strXml & vbCrLf & "<STATICVARIABLES>"
            strXml = strXml & vbCrLf & "<SVCURRENTCOMPANY>" & "MSA Global Logistics Private Limited - (From 1-Apr-2015)" & "</SVCURRENTCOMPANY>"
            strXml = strXml & vbCrLf & "</STATICVARIABLES>"
            strXml = strXml & vbCrLf & "</REQUESTDESC>"
            strXml = strXml & vbCrLf & "<REQUESTDATA>" & vbCrLf

            For Each row In grdSummary.Rows
                If CType(row.findcontrol("chkInv"), CheckBox).Checked = True Then
                    dblTotalA_B = 0
                    '*****FETCHING DATA FROM EXPORTTALLY****
                    strSql = ""
                    strSql = "SELECT top 1 REMOTEID, VCHKEY, GUID, ALTERID, MASTERID, VOUCHERKEY FROM ExpTally ORDER BY EntryID Desc"
                    dt = db.sub_GetDatatable(strSql)
                    If dt.Rows.Count > 0 Then

                        strRemoteGUIID = "d2826989-075f-1" & intYrID & "da-96af-8929b305cf7e-0000e" & "d3757a97-399e-4184-b42b-92bc231c1ff4-0000d"
                        strRemoteGUIID = strRemoteGUIID & Trim(dt.Rows(0)("REMOTEID")) + 1
                        strVCHKey = "d" & Trim(dt.Rows(0)("VCHKEY")) + 1
                        strVCHKey = "d2826762-075f-1" & intYrID & "da-96af-8929b305cf7d-0000a0d9:" & Trim(strVCHKey)
                        dblAlertID = Val(dt.Rows(0)("ALTERID")) + 1
                        dblMasterID = Val(dt.Rows(0)("MASTERID")) + 1
                        dblVoucherKey = Val(dt.Rows(0)("VOUCHERKEY")) + 1
                        dblRemoteID = Trim(dt.Rows(0)("REMOTEID")) + 1
                        dblVCHKEY = Trim(dt.Rows(0)("VCHKEY")) + 1
                        ' End If
                        '****Saving DATA FROM EXPORTTALLY*****
                        Dim dt1 As New DataTable
                        strSql = ""
                        strSql = " insert into ExpTally (REMOTEID,VCHKEY,GUID,ALTERID,MASTERID,VOUCHERKEY ) "
                        strSql += " values('" & Val(dblRemoteID) & "','" & Val(dblVCHKEY) & "','" & Trim(strRemoteGUIID) & "','" & Val(dblAlertID) & "','" & Val(dblMasterID) & "','" & Val(dblVoucherKey) & "')"
                        db.sub_ExecuteNonQuery(strSql)
                    End If
                    If Val(CType(row.FindControl("lblGrandTotal"), Label).Text) <> 0 Then
                        If dblcount = 1 Then
                            strXml = strXml & vbCrLf & "<TALLYMESSAGE xmlns:UDF=""TallyUDF"">" & vbCrLf
                        End If
                        dblcount = dblcount + 1
                        strXml = strXml & vbCrLf & "<VOUCHER REMOTEID=""" & strRemoteGUIID & """ VCHKEY=""" & strVCHKey & """ VCHTYPE=""Sales/CFS"" ACTION=""Create"" OBJVIEW=""Accounting Voucher View"">" & vbCrLf
                        strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS.LIST TYPE=""Number"">" & vbCrLf
                        strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS>" & "-1" & "</OLDAUDITENTRYIDS>"
                        strXml = strXml & vbCrLf & "</OLDAUDITENTRYIDS.LIST>" & vbCrLf

                        strXml = strXml & vbCrLf & "<DATE>" & Convert.ToDateTime(Trim(CType(row.FindControl("lblAssessDate"), Label).Text)).ToString("yyyyMMdd") & "</DATE>"
                        strXml = strXml & vbCrLf & "<GUID>" & "" & strRemoteGUIID & "" & "</GUID>"
                        'strXml = strXml & vbCrLf & "<STATENAME>" & Trim(grdDetails.Rows(i).Cells("State").Value) & "</STATENAME>"
                        strXml = strXml & vbCrLf & "<VATDEALERTYPE >" & "" & "</VATDEALERTYPE >"
                        '-for remarks
                        Dim dbl20 As Double
                        Dim dbl40 As Double
                        StrNarration = ""
                        strcontdets = ""
                        Dim dt7 As New DataTable
                        ''Remarks
                        dbl20 = 0
                        dbl40 = 0
                        strSql = ""
                        strSql += "select ND.ContainerNo,Size from bond_assessM BM inner join NOC N on BM.NOCNo=N.NOCNo inner join NOC_Dets ND on N.NOCNo=ND.NOCNo where AssessNo='" & Trim(CType(row.FindControl("lblassessNo"), Label).Text) & "' and WorkYear='" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "'"
                        dt7 = db.sub_GetDatatable(strSql)
                        For k As Integer = 0 To dt7.Rows.Count - 1
                            If Val(dt7.Rows(k)("size")) = 20 Then
                                dbl20 = dbl20 + 1
                            ElseIf Val(dt7.Rows(k)("size")) = 40 Then
                                dbl40 = dbl40 + 1
                            End If
                        Next
                        strsbno = ""
                        strcontdets = "20 X " & dbl20
                        strcontdets = strcontdets & " / " & " 40 X " & " " & dbl40
                        Dim dt8 As New DataTable
                        strSql = ""
                        strSql += "SELECT TOP 1 M.IGMNo, M.ItemNo, I.ImporterName,C.CHAName From bond_assessM M inner join Importer I on M.ImporterID=I.ImporterID inner Join CHA C on M.Chaid=C.CHAID WHERE  AssessNo ='" & Trim(CType(row.FindControl("lblassessNo"), Label).Text) & "' and WorkYear='" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "'"
                        dt8 = db.sub_GetDatatable(strSql)
                        If dt8.Rows.Count > 0 Then
                            strsbno = Trim(dt8.Rows(0)("IGMNO") & "") & "- " & Trim(dt8.Rows(0)("ItemNo") & "") & ", CHA: " & Replace(Trim(dt8.Rows(0)("CHAName")), "&", "") & ", Importer: " & Replace(dt8.Rows(0)("ImporterName"), "&", "") & ""
                        End If
                        StrNarration = ""
                        StrNarration = "Inv. No" & ":" & Trim(CType(row.FindControl("lblInvoiceNo"), Label).Text) & "," & "No of containers " & strcontdets & ", IGM/Item No." & strsbno
                        strXml = strXml & vbCrLf & "<NARRATION>" & StrNarration & "</NARRATION>"
                        'strXml = strXml & vbCrLf & "<COUNTRYOFRESIDENCE>" & "India" & "</COUNTRYOFRESIDENCE>"
                        strXml = strXml & vbCrLf & "<PARTYGSTIN>" & Trim(CType(row.FindControl("lblGSTIn"), Label).Text) & "</PARTYGSTIN>"
                        strXml = strXml & vbCrLf & "<TAXUNITNAME>" & "Default Tax Unit" & "</TAXUNITNAME>"
                        strXml = strXml & vbCrLf & "<VOUCHERTYPENAME>" & "Sales/CFS" & "</VOUCHERTYPENAME>"
                        strXml = strXml & vbCrLf & "<REFERENCE>" & Trim(CType(row.FindControl("lblInvoiceNo"), Label).Text) & "</REFERENCE>"
                        strXml = strXml & vbCrLf & "<VOUCHERNUMBER>" & Trim(CType(row.FindControl("lblInvoiceNo"), Label).Text) & "</VOUCHERNUMBER>"
                        strXml = strXml & vbCrLf & "<PARTYLEDGERNAME>" & Trim(CType(row.FindControl("lblTallyName"), Label).Text) & "</PARTYLEDGERNAME>"
                        strXml = strXml & vbCrLf & "<BASICBASEPARTYNAME>" & Trim(CType(row.FindControl("lblTallyName"), Label).Text) & "</BASICBASEPARTYNAME>"
                        strXml = strXml & vbCrLf & "<CSTFORMISSUETYPE>"
                        strXml = strXml & vbCrLf & "</CSTFORMISSUETYPE>"
                        strXml = strXml & vbCrLf & "<CSTFORMRECVTYPE>"
                        strXml = strXml & vbCrLf & "</CSTFORMRECVTYPE>"
                        strXml = strXml & vbCrLf & "<FBTPAYMENTTYPE>" & "Default" & "</FBTPAYMENTTYPE>"
                        strXml = strXml & vbCrLf & "<PERSISTEDVIEW>" & "Accounting Voucher View" & "</PERSISTEDVIEW>"
                        'strXml = strXml & vbCrLf & "<PLACEOFSUPPLY>" & Trim(grdDetails.Rows(i).Cells("State").Value) & "</PLACEOFSUPPLY>"
                        strXml = strXml & vbCrLf & "<BASICBUYERNAME>" & Trim(CType(row.FindControl("lblTallyName"), Label).Text) & "</BASICBUYERNAME>"
                        strXml = strXml & vbCrLf & "<BASICDATETIMEOFINVOICE>" & Convert.ToDateTime(Trim(CType(row.FindControl("lblAssessDate"), Label).Text)).ToString("dd-MMM-yyyy") & " at " & Convert.ToDateTime(Trim(CType(row.FindControl("lblAssessDate"), Label).Text)).ToString("hh:ss") & """</BASICDATETIMEOFINVOICE>"
                        strXml = strXml & vbCrLf & "<BASICDATETIMEOFREMOVAL>" & Convert.ToDateTime(Trim(CType(row.FindControl("lblAssessDate"), Label).Text)).ToString("dd-MMM-yyyy") & " at " & Convert.ToDateTime(Trim(CType(row.FindControl("lblAssessDate"), Label).Text)).ToString("hh:ss") & """</BASICDATETIMEOFREMOVAL>"
                        'strXml = strXml & vbCrLf & "<BUYERADDRESSTYPE>" & Trim(grdDetails.Rows(i).Cells("GSTNAME").Value) & "</BUYERADDRESSTYPE>"
                        '<COSTCENTRENAME>CFS</COSTCENTRENAME>
                        'strXml = strXml & vbCrLf & "<COSTCENTRENAME>" & "CFS" & "</COSTCENTRENAME>"
                        strXml = strXml & vbCrLf & "<VCHGSTCLASS>"
                        strXml = strXml & vbCrLf & "</VCHGSTCLASS>"
                        strXml = strXml & vbCrLf & "<ENTEREDBY>" & "account" & "</ENTEREDBY>"
                        strXml = strXml & vbCrLf & "<DIFFACTUALQTY>" & "No" & "</DIFFACTUALQTY>"
                        strXml = strXml & vbCrLf & "<ISMSTFROMSYNC>" & "No" & "</ISMSTFROMSYNC>"
                        strXml = strXml & vbCrLf & "<ASORIGINAL>" & "No" & "</ASORIGINAL>"
                        strXml = strXml & vbCrLf & "<AUDITED>" & "No" & "</AUDITED>"
                        strXml = strXml & vbCrLf & "<FORJOBCOSTING>" & "No" & "</FORJOBCOSTING>"
                        strXml = strXml & vbCrLf & "<ISOPTIONAL>" & "No" & "</ISOPTIONAL>"
                        strXml = strXml & vbCrLf & "<EFFECTIVEDATE>" & Convert.ToDateTime(Trim(CType(row.FindControl("lblAssessDate"), Label).Text)).ToString("yyyyMMdd") & "</EFFECTIVEDATE>"
                        strXml = strXml & vbCrLf & "<USEFOREXCISE>" & "No" & "</USEFOREXCISE>"
                        strXml = strXml & vbCrLf & "<ISFORJOBWORKIN>" & "No" & "</ISFORJOBWORKIN>"
                        strXml = strXml & vbCrLf & "<ALLOWCONSUMPTION>" & "No" & "</ALLOWCONSUMPTION>"
                        strXml = strXml & vbCrLf & "<USEFORINTEREST>" & "No" & "</USEFORINTEREST>"
                        strXml = strXml & vbCrLf & "<USEFORGAINLOSS>" & "No" & "</USEFORGAINLOSS>"
                        strXml = strXml & vbCrLf & "<USEFORGODOWNTRANSFER>" & "No" & "</USEFORGODOWNTRANSFER>"
                        strXml = strXml & vbCrLf & "<USEFORCOMPOUND>" & "No" & "</USEFORCOMPOUND>"
                        strXml = strXml & vbCrLf & "<USEFORSERVICETAX>" & "No" & "</USEFORSERVICETAX>"
                        strXml = strXml & vbCrLf & "<ISEXCISEVOUCHER>" & "No" & "</ISEXCISEVOUCHER>"
                        strXml = strXml & vbCrLf & "<EXCISETAXOVERRIDE>" & "No" & "</EXCISETAXOVERRIDE>"
                        strXml = strXml & vbCrLf & "<USEFORTAXUNITTRANSFER>" & "No" & "</USEFORTAXUNITTRANSFER>"
                        strXml = strXml & vbCrLf & "<EXCISEOPENING>" & "No" & "</EXCISEOPENING>"
                        strXml = strXml & vbCrLf & "<USEFORFINALPRODUCTION>" & "No" & "</USEFORFINALPRODUCTION>"
                        'strXml = strXml & vbCrLf & "<ALTERID>" & dblAlertID & "</ALTERID>"
                        'strXml = strXml & vbCrLf & "<EXCISEOPENING>" & "No" & "</EXCISEOPENING>"
                        'strXml = strXml & vbCrLf & "<USEFORFINALPRODUCTION>" & "No" & "</USEFORFINALPRODUCTION>"
                        strXml = strXml & vbCrLf & "<ISTDSOVERRIDDEN>" & "No" & "</ISTDSOVERRIDDEN>"
                        strXml = strXml & vbCrLf & "<ISTCSOVERRIDDEN>" & "No" & "</ISTCSOVERRIDDEN>"
                        strXml = strXml & vbCrLf & "<ISTDSTCSCASHVCH>" & "No" & "</ISTDSTCSCASHVCH>"
                        strXml = strXml & vbCrLf & "<INCLUDEADVPYMTVCH>" & "No" & "</INCLUDEADVPYMTVCH>"
                        strXml = strXml & vbCrLf & "<ISSUBWORKSCONTRACT>" & "No" & "</ISSUBWORKSCONTRACT>"
                        strXml = strXml & vbCrLf & "<ISVATOVERRIDDEN>" & "No" & "</ISVATOVERRIDDEN>"
                        strXml = strXml & vbCrLf & "<IGNOREORIGVCHDATE>" & "No" & "</IGNOREORIGVCHDATE>"
                        strXml = strXml & vbCrLf & "<ISSERVICETAXOVERRIDDEN>" & "No" & "</ISSERVICETAXOVERRIDDEN>"
                        strXml = strXml & vbCrLf & "<ISISDVOUCHER>" & "No" & "</ISISDVOUCHER>"
                        strXml = strXml & vbCrLf & "<ISEXCISEOVERRIDDEN>" & "No" & "</ISEXCISEOVERRIDDEN>"
                        strXml = strXml & vbCrLf & "<ISEXCISESUPPLYVCH>" & "No" & "</ISEXCISESUPPLYVCH>"
                        strXml = strXml & vbCrLf & "<ISGSTOVERRIDDEN>" & "No" & "</ISGSTOVERRIDDEN>"
                        strXml = strXml & vbCrLf & "<GSTNOTEXPORTED>" & "No" & "</GSTNOTEXPORTED>"
                        strXml = strXml & vbCrLf & "<ISVATPRINCIPALACCOUNT>" & "No" & "</ISVATPRINCIPALACCOUNT>"
                        strXml = strXml & vbCrLf & "<ISSHIPPINGWITHINSTATE>" & "No" & "</ISSHIPPINGWITHINSTATE>"
                        strXml = strXml & vbCrLf & "<ISCANCELLED>" & "No" & "</ISCANCELLED>"
                        strXml = strXml & vbCrLf & "<HASCASHFLOW>" & "No" & "</HASCASHFLOW>"
                        strXml = strXml & vbCrLf & "<ISPOSTDATED>" & "No" & "</ISPOSTDATED>"
                        strXml = strXml & vbCrLf & "<USETRACKINGNUMBER>" & "No" & "</USETRACKINGNUMBER>"
                        strXml = strXml & vbCrLf & "<ISINVOICE>" & "No" & "</ISINVOICE>"
                        strXml = strXml & vbCrLf & "<MFGJOURNAL>" & "No" & "</MFGJOURNAL>"
                        strXml = strXml & vbCrLf & "<HASDISCOUNTS>" & "No" & "</HASDISCOUNTS>"
                        strXml = strXml & vbCrLf & "<ASPAYSLIP>" & "No" & "</ASPAYSLIP>"
                        strXml = strXml & vbCrLf & "<ISCOSTCENTRE>" & "No" & "</ISCOSTCENTRE>"
                        strXml = strXml & vbCrLf & "<ISSTXNONREALIZEDVCH>" & "No" & "</ISSTXNONREALIZEDVCH>"
                        strXml = strXml & vbCrLf & "<ISEXCISEMANUFACTURERON>" & "No" & "</ISEXCISEMANUFACTURERON>"
                        strXml = strXml & vbCrLf & "<ISBLANKCHEQUE>" & "No" & "</ISBLANKCHEQUE>"
                        strXml = strXml & vbCrLf & "<ISVOID>" & "No" & "</ISVOID>"
                        strXml = strXml & vbCrLf & "<ISONHOLD>" & "No" & "</ISONHOLD>"
                        strXml = strXml & vbCrLf & "<ORDERLINESTATUS>" & "No" & "</ORDERLINESTATUS>"
                        strXml = strXml & vbCrLf & "<VATISAGNSTCANCSALES>" & "No" & "</VATISAGNSTCANCSALES>"
                        strXml = strXml & vbCrLf & "<VATISPURCEXEMPTED>" & "No" & "</VATISPURCEXEMPTED>"
                        strXml = strXml & vbCrLf & "<ISVATRESTAXINVOICE>" & "No" & "</ISVATRESTAXINVOICE>"
                        strXml = strXml & vbCrLf & "<VATISASSESABLECALCVCH>" & "No" & "</VATISASSESABLECALCVCH>"
                        strXml = strXml & vbCrLf & "<ISVATDUTYPAID>" & "No" & "</ISVATDUTYPAID>"
                        strXml = strXml & vbCrLf & "<ISDELIVERYSAMEASCONSIGNEE>" & "No" & "</ISDELIVERYSAMEASCONSIGNEE>"
                        strXml = strXml & vbCrLf & "<ISDISPATCHSAMEASCONSIGNOR>" & "No" & "</ISDISPATCHSAMEASCONSIGNOR>"
                        strXml = strXml & vbCrLf & "<ISDELETED>" & "No" & "</ISDELETED>"
                        strXml = strXml & vbCrLf & "<CHANGEVCHMODE>" & "No" & "</CHANGEVCHMODE>"
                        'strXml = strXml & vbCrLf & "<ASORIGINAL>" & "No" & "</ASORIGINAL>"
                        'strXml = strXml & vbCrLf & "<VCHISFROMSYNC>" & "No" & "</VCHISFROMSYNC>"
                        strXml = strXml & vbCrLf & "<ALTERID>" & dblAlertID & "</ALTERID>"
                        strXml = strXml & vbCrLf & "<MASTERID>" & dblMasterID & "</MASTERID>"
                        strXml = strXml & vbCrLf & "<VOUCHERKEY>" & dblVoucherKey & "</VOUCHERKEY>"
                        strXml = strXml & vbCrLf & "<EXCLUDEDTAXATIONS.LIST>"
                        strXml = strXml & vbCrLf & "</EXCLUDEDTAXATIONS.LIST>"
                        strXml = strXml & vbCrLf & "<OLDAUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "</OLDAUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "<ACCOUNTAUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "</ACCOUNTAUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "<AUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "</AUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "<DUTYHEADDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</DUTYHEADDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<SUPPLEMENTARYDUTYHEADDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</SUPPLEMENTARYDUTYHEADDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<INVOICEDELNOTES.LIST>"
                        strXml = strXml & vbCrLf & "</INVOICEDELNOTES.LIST>"
                        strXml = strXml & vbCrLf & "<INVOICEORDERLIST.LIST>"
                        strXml = strXml & vbCrLf & "</INVOICEORDERLIST.LIST>"
                        strXml = strXml & vbCrLf & "<INVOICEINDENTLIST.LIST>"
                        strXml = strXml & vbCrLf & "</INVOICEINDENTLIST.LIST>"
                        strXml = strXml & vbCrLf & "<ATTENDANCEENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "</ATTENDANCEENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "<ORIGINVOICEDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</ORIGINVOICEDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<INVOICEEXPORTLIST.LIST>"
                        strXml = strXml & vbCrLf & "</INVOICEEXPORTLIST.LIST>"
                    End If
                    If Val(CType(row.FindControl("lblGrandTotal"), Label).Text) <> 0 Then
                        strXml = strXml & vbCrLf & "<ALLLEDGERENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS.LIST TYPE=""Number"">"
                        strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS>" & "-1" & "</OLDAUDITENTRYIDS>"
                        strXml = strXml & vbCrLf & "</OLDAUDITENTRYIDS.LIST>"
                        strXml = strXml & vbCrLf & "<LEDGERNAME>" & Trim(CType(row.FindControl("lblTallyName"), Label).Text) & "</LEDGERNAME>"
                        strXml = strXml & vbCrLf & "<GSTCLASS>"
                        strXml = strXml & vbCrLf & "</GSTCLASS>"
                        strXml = strXml & vbCrLf & "<ISDEEMEDPOSITIVE>" & "Yes" & "</ISDEEMEDPOSITIVE>"
                        strXml = strXml & vbCrLf & "<LEDGERFROMITEM>" & "No" & "</LEDGERFROMITEM>"
                        strXml = strXml & vbCrLf & "<REMOVEZEROENTRIES>" & "No" & "</REMOVEZEROENTRIES>"
                        strXml = strXml & vbCrLf & "<ISPARTYLEDGER>" & "Yes" & "</ISPARTYLEDGER>"
                        strXml = strXml & vbCrLf & "<ISLASTDEEMEDPOSITIVE>" & "Yes" & "</ISLASTDEEMEDPOSITIVE>"
                        'strXml = strXml & vbCrLf & "<AMOUNT>" & "-" & Format(Round(mshfDetails.TextMatrix(intR, 8)), "0.00") & "</AMOUNT>"
                        strXml = strXml & vbCrLf & "<AMOUNT>" & "-" & Format(Val(CType(row.FindControl("lblGrandTotal"), Label).Text), "0.00") & "</AMOUNT>"
                        strXml = strXml & vbCrLf & "<VATEXPAMOUNT>" & "-" & Format(Val(CType(row.FindControl("lblGrandTotal"), Label).Text), "0.00") & "</VATEXPAMOUNT>"
                        strXml = strXml & vbCrLf & "<SERVICETAXDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</SERVICETAXDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<BANKALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "</BANKALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "<BILLALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "<NAME>" & Trim(CType(row.FindControl("lblInvoiceNo"), Label).Text) & "</NAME>"
                        strXml = strXml & vbCrLf & "<BILLTYPE>" & "New Ref" & "</BILLTYPE>"
                        strXml = strXml & vbCrLf & "<TDSDEDUCTEEISSPECIALRATE>" & "No" & "</TDSDEDUCTEEISSPECIALRATE>"
                        'strXml = strXml & vbCrLf & "<AMOUNT>" & "-" & Format(Round(mshfDetails.TextMatrix(intR, 8)), "0.00") & "</AMOUNT>"
                        strXml = strXml & vbCrLf & "<AMOUNT>" & "-" & Format(Val(CType(row.FindControl("lblGrandTotal"), Label).Text), "0.00") & "</AMOUNT>"
                        strXml = strXml & vbCrLf & "<INTERESTCOLLECTION.LIST>"
                        strXml = strXml & vbCrLf & "</INTERESTCOLLECTION.LIST>"
                        strXml = strXml & vbCrLf & "<STBILLCATEGORIES.LIST>"
                        strXml = strXml & vbCrLf & "</STBILLCATEGORIES.LIST>"
                        strXml = strXml & vbCrLf & "</BILLALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "<INTERESTCOLLECTION.LIST>"
                        strXml = strXml & vbCrLf & "</INTERESTCOLLECTION.LIST>"
                        strXml = strXml & vbCrLf & "<OLDAUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "</OLDAUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "<ACCOUNTAUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "</ACCOUNTAUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "<AUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "</AUDITENTRIES.LIST>"
                        strXml = strXml & vbCrLf & "<INPUTCRALLOCS.LIST>"
                        strXml = strXml & vbCrLf & "</INPUTCRALLOCS.LIST>"
                        strXml = strXml & vbCrLf & "<DUTYHEADDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</DUTYHEADDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<EXCISEDUTYHEADDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</EXCISEDUTYHEADDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<RATEDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</RATEDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<SUMMARYALLOCS.LIST>"
                        strXml = strXml & vbCrLf & "</SUMMARYALLOCS.LIST>"
                        strXml = strXml & vbCrLf & "<STPYMTDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</STPYMTDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<EXCISEPAYMENTALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "</EXCISEPAYMENTALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "<TAXBILLALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "</TAXBILLALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "<TAXOBJECTALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "</TAXOBJECTALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "<TDSEXPENSEALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "</TDSEXPENSEALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "<VATSTATUTORYDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</VATSTATUTORYDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<COSTTRACKALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "</COSTTRACKALLOCATIONS.LIST>"
                        strXml = strXml & vbCrLf & "<REFVOUCHERDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</REFVOUCHERDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<INVOICEWISEDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</INVOICEWISEDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<VATITCDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</VATITCDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "<ADVANCETAXDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</ADVANCETAXDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</ALLLEDGERENTRIES.LIST>"
                    End If
                    If Val(CType(row.FindControl("lblGrandTotal"), Label).Text) <> 0 Then
                        Dim dt2 As New DataTable
                        strSql = ""
                        strSql = "SP_get_Other_charge_BOND '" & Trim(CType(row.FindControl("lblassessNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "'"
                        dt2 = db.sub_GetDatatable(strSql)
                        For k As Integer = 0 To dt2.Rows.Count - 1
                            Dim dt3 As New DataTable
                            strSql = ""
                            strSql = "exec Get_SP_IMPChargesdets_BOnd '" & Val(dt2.Rows(k)("accountid")) & "','" & Trim(CType(row.FindControl("lblassessNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "'"
                            dt3 = db.sub_GetDatatable(strSql)
                            If dt3.Rows.Count > 0 Then
                                If Val(dt3.Rows(0)("Accountid")) = 0 Then
                                    GoTo nextrecord
                                End If
                            End If
                            strXml = strXml & vbCrLf & "<ALLLEDGERENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS.LIST TYPE=""Number"">"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS>" & "-1" & "</OLDAUDITENTRYIDS>"
                            strXml = strXml & vbCrLf & "</OLDAUDITENTRYIDS.LIST>"
                            strXml = strXml & vbCrLf & "<LEDGERNAME>" & dt2.Rows(k)("tallyname") & "</LEDGERNAME>"
                            strXml = strXml & vbCrLf & "<GSTCLASS>"
                            strXml = strXml & vbCrLf & "</GSTCLASS>"
                            strXml = strXml & vbCrLf & "<ISDEEMEDPOSITIVE>" & "No" & "</ISDEEMEDPOSITIVE>"
                            strXml = strXml & vbCrLf & "<LEDGERFROMITEM>" & "No" & "</LEDGERFROMITEM>"
                            strXml = strXml & vbCrLf & "<REMOVEZEROENTRIES>" & "No" & "</REMOVEZEROENTRIES>"
                            strXml = strXml & vbCrLf & "<ISPARTYLEDGER>" & "No" & "</ISPARTYLEDGER>"
                            strXml = strXml & vbCrLf & "<ISLASTDEEMEDPOSITIVE>" & "No" & "</ISLASTDEEMEDPOSITIVE>"
                            Dim dt5 As New DataTable
                            strSql = ""
                            strSql = " sP_otherchargeamt_BOND '" & Val(dt2.Rows(k)("accountid")) & "','" & Trim(CType(row.FindControl("lblassessNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "'"
                            dt5 = db.sub_GetDatatable(strSql)
                            If dt5.Rows.Count > 0 Then
                                strXml = strXml & vbCrLf & "<AMOUNT>" & Format(dt5.Rows(0)("Amount"), "0.00") & "</AMOUNT>"
                                strXml = strXml & vbCrLf & "<VATEXPAMOUNT>" & Format(dt5.Rows(0)("Amount"), "0.00") & "</VATEXPAMOUNT>"
                            End If
                            strXml = strXml & vbCrLf & "<SERVICETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</SERVICETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<BANKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</BANKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<BILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</BILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<INTERESTCOLLECTION.LIST>"
                            strXml = strXml & vbCrLf & "</INTERESTCOLLECTION.LIST>"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</OLDAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<ACCOUNTAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</ACCOUNTAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<AUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</AUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<INPUTCRALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "</INPUTCRALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "<DUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</DUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<EXCISEDUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</EXCISEDUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<RATEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</RATEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<SUMMARYALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "</SUMMARYALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "<STPYMTDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</STPYMTDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<EXCISEPAYMENTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</EXCISEPAYMENTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TAXBILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TAXBILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TAXOBJECTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TAXOBJECTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TDSEXPENSEALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TDSEXPENSEALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<VATSTATUTORYDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</VATSTATUTORYDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<COSTTRACKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</COSTTRACKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<REFVOUCHERDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</REFVOUCHERDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<INVOICEWISEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</INVOICEWISEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<VATITCDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</VATITCDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<ADVANCETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</ADVANCETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</ALLLEDGERENTRIES.LIST>"
nextrecord:
                        Next
                    End If
                    Dim dblTaxamt As Double
                    Dim dblEcesAmt As Double
                    Dim dblHcessAmt As Double
                    Dim dblNettotal As Double
                    Dim dblSBCessamt As Double
                    Dim dblKKCessamt As Double
                    Dim dblgrandtotal As Double
                    dblTaxamt = 0
                    dblEcesAmt = 0
                    dblHcessAmt = 0
                    dblNettotal = 0
                    dblgrandtotal = 0
                    dblKKCessamt = 0
                    dblSBCessamt = 0
                    Dim dt6 As New DataTable
                    strSql = ""             '  ASK TO SRI WHAT SHOULD BE THERE IN CELLS
                    strSql = "select sgst, cgst, igst,  grandtotal, NetTotal From bond_assessM  where AssessNo ='" & Trim(CType(row.FindControl("lblassessNo"), Label).Text) & "' AND WorkYear='" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "' and iscancel=0 "
                    dt6 = db.sub_GetDatatable(strSql)
                    If dt6.Rows.Count > 0 Then
                        dblTaxamt = Val(dt6.Rows(0)("sgst"))
                        dblSBCessamt = Val(dt6.Rows(0)("cgst"))
                        dblKKCessamt = Val(dt6.Rows(0)("igst"))
                        If dblTaxamt <> 0 Then
                            strXml = strXml & vbCrLf & "<ALLLEDGERENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS.LIST TYPE=""Number"">"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS>" & "-1" & "</OLDAUDITENTRYIDS>"
                            strXml = strXml & vbCrLf & "</OLDAUDITENTRYIDS.LIST>"
                            'NEED TO ASK SIR
                            strXml = strXml & vbCrLf & "<LEDGERNAME>" & "SGST" & "</LEDGERNAME>"
                            strXml = strXml & vbCrLf & "<GSTCLASS>"
                            strXml = strXml & vbCrLf & "</GSTCLASS>"
                            strXml = strXml & vbCrLf & "<ISDEEMEDPOSITIVE>" & "No" & "</ISDEEMEDPOSITIVE>"
                            strXml = strXml & vbCrLf & "<LEDGERFROMITEM>" & "No" & "</LEDGERFROMITEM>"
                            strXml = strXml & vbCrLf & "<REMOVEZEROENTRIES>" & "No" & "</REMOVEZEROENTRIES>"
                            strXml = strXml & vbCrLf & "<ISPARTYLEDGER>" & "No" & "</ISPARTYLEDGER>"
                            strXml = strXml & vbCrLf & "<ISLASTDEEMEDPOSITIVE>" & "No" & "</ISLASTDEEMEDPOSITIVE>"
                            'strXml = strXml & vbCrLf & "<AMOUNT>" & Format(Round(dblTaxamt), "0.00") & "</AMOUNT>"
                            strXml = strXml & vbCrLf & "<AMOUNT>" & Format(dblTaxamt, "0.00") & "</AMOUNT>"
                            strXml = strXml & vbCrLf & "<VATEXPAMOUNT>" & Format(dblTaxamt, "0.00") & "</VATEXPAMOUNT>"
                            strXml = strXml & vbCrLf & "<SERVICETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</SERVICETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<BANKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</BANKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<BILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</BILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<INTERESTCOLLECTION.LIST>"
                            strXml = strXml & vbCrLf & "</INTERESTCOLLECTION.LIST>"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</OLDAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<ACCOUNTAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</ACCOUNTAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<AUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</AUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<INPUTCRALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "</INPUTCRALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "<DUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</DUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<EXCISEDUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</EXCISEDUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<RATEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</RATEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<SUMMARYALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "</SUMMARYALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "<STPYMTDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</STPYMTDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<EXCISEPAYMENTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</EXCISEPAYMENTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TAXBILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TAXBILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TAXOBJECTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TAXOBJECTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TDSEXPENSEALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TDSEXPENSEALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<VATSTATUTORYDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</VATSTATUTORYDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<COSTTRACKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</COSTTRACKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<REFVOUCHERDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</REFVOUCHERDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<INVOICEWISEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</INVOICEWISEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<VATITCDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</VATITCDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<ADVANCETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</ADVANCETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</ALLLEDGERENTRIES.LIST>"
                        End If
                        If dblSBCessamt <> 0 Then
                            strXml = strXml & vbCrLf & "<ALLLEDGERENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS.LIST TYPE=""Number"">"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS>" & "-1" & "</OLDAUDITENTRYIDS>"
                            strXml = strXml & vbCrLf & "</OLDAUDITENTRYIDS.LIST>"
                            'strXml = strXml & vbCrLf & "<TAXCLASSIFICATIONNAME>" & "Labour, Service, Job Work and Other Charges" & "</TAXCLASSIFICATIONNAME>"
                            '***************NEED TO ASK SIR
                            strXml = strXml & vbCrLf & "<LEDGERNAME>" & "CGST" & "</LEDGERNAME>"
                            strXml = strXml & vbCrLf & "<GSTCLASS>"
                            strXml = strXml & vbCrLf & "</GSTCLASS>"
                            strXml = strXml & vbCrLf & "<ISDEEMEDPOSITIVE>" & "No" & "</ISDEEMEDPOSITIVE>"
                            strXml = strXml & vbCrLf & "<LEDGERFROMITEM>" & "No" & "</LEDGERFROMITEM>"
                            strXml = strXml & vbCrLf & "<REMOVEZEROENTRIES>" & "No" & "</REMOVEZEROENTRIES>"
                            strXml = strXml & vbCrLf & "<ISPARTYLEDGER>" & "No" & "</ISPARTYLEDGER>"
                            strXml = strXml & vbCrLf & "<ISLASTDEEMEDPOSITIVE>" & "No" & "</ISLASTDEEMEDPOSITIVE>"
                            strXml = strXml & vbCrLf & "<AMOUNT>" & Format(dblSBCessamt, "0.00") & "</AMOUNT>"
                            strXml = strXml & vbCrLf & "<VATEXPAMOUNT>" & Format(dblSBCessamt, "0.00") & "</VATEXPAMOUNT>"
                            strXml = strXml & vbCrLf & "<SERVICETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</SERVICETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<BANKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</BANKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<BILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</BILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<INTERESTCOLLECTION.LIST>"
                            strXml = strXml & vbCrLf & "</INTERESTCOLLECTION.LIST>"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</OLDAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<ACCOUNTAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</ACCOUNTAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<AUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</AUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<INPUTCRALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "</INPUTCRALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "<DUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</DUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<EXCISEDUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</EXCISEDUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<RATEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</RATEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<SUMMARYALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "</SUMMARYALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "<STPYMTDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</STPYMTDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<EXCISEPAYMENTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</EXCISEPAYMENTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TAXBILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TAXBILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TAXOBJECTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TAXOBJECTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TDSEXPENSEALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TDSEXPENSEALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<VATSTATUTORYDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</VATSTATUTORYDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<COSTTRACKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</COSTTRACKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<REFVOUCHERDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</REFVOUCHERDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<INVOICEWISEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</INVOICEWISEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<VATITCDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</VATITCDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<ADVANCETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</ADVANCETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</ALLLEDGERENTRIES.LIST>"
                        End If
                        '-----------------------------END---------------------------------------------------------------
                        ' swachh bharat cess
                        '-----------------------------SB-Cess------------------------------------------------------------

                        If dblKKCessamt <> 0 Then
                            strXml = strXml & vbCrLf & "<ALLLEDGERENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS.LIST TYPE=""Number"">"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRYIDS>" & "-1" & "</OLDAUDITENTRYIDS>"
                            strXml = strXml & vbCrLf & "</OLDAUDITENTRYIDS.LIST>"
                            'strXml = strXml & vbCrLf & "<TAXCLASSIFICATIONNAME>" & "Labour, Service, Job Work and Other Charges" & "</TAXCLASSIFICATIONNAME>"
                            'NEED TO ASK SIR
                            strXml = strXml & vbCrLf & "<LEDGERNAME>" & "IGST" & "</LEDGERNAME>"
                            strXml = strXml & vbCrLf & "<GSTCLASS>"
                            strXml = strXml & vbCrLf & "</GSTCLASS>"
                            strXml = strXml & vbCrLf & "<ISDEEMEDPOSITIVE>" & "No" & "</ISDEEMEDPOSITIVE>"
                            strXml = strXml & vbCrLf & "<LEDGERFROMITEM>" & "No" & "</LEDGERFROMITEM>"
                            strXml = strXml & vbCrLf & "<REMOVEZEROENTRIES>" & "No" & "</REMOVEZEROENTRIES>"
                            strXml = strXml & vbCrLf & "<ISPARTYLEDGER>" & "No" & "</ISPARTYLEDGER>"
                            strXml = strXml & vbCrLf & "<ISLASTDEEMEDPOSITIVE>" & "No" & "</ISLASTDEEMEDPOSITIVE>"
                            strXml = strXml & vbCrLf & "<AMOUNT>" & Format(dblKKCessamt, "0.00") & "</AMOUNT>"
                            strXml = strXml & vbCrLf & "<VATEXPAMOUNT>" & Format(dblKKCessamt, "0.00") & "</VATEXPAMOUNT>"
                            strXml = strXml & vbCrLf & "<SERVICETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</SERVICETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<BANKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</BANKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<BILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</BILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<INTERESTCOLLECTION.LIST>"
                            strXml = strXml & vbCrLf & "</INTERESTCOLLECTION.LIST>"
                            strXml = strXml & vbCrLf & "<OLDAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</OLDAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<ACCOUNTAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</ACCOUNTAUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<AUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "</AUDITENTRIES.LIST>"
                            strXml = strXml & vbCrLf & "<INPUTCRALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "</INPUTCRALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "<DUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</DUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<EXCISEDUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</EXCISEDUTYHEADDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<RATEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</RATEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<SUMMARYALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "</SUMMARYALLOCS.LIST>"
                            strXml = strXml & vbCrLf & "<STPYMTDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</STPYMTDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<EXCISEPAYMENTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</EXCISEPAYMENTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TAXBILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TAXBILLALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TAXOBJECTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TAXOBJECTALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<TDSEXPENSEALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</TDSEXPENSEALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<VATSTATUTORYDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</VATSTATUTORYDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<COSTTRACKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "</COSTTRACKALLOCATIONS.LIST>"
                            strXml = strXml & vbCrLf & "<REFVOUCHERDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</REFVOUCHERDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<INVOICEWISEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</INVOICEWISEDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<VATITCDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</VATITCDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "<ADVANCETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</ADVANCETAXDETAILS.LIST>"
                            strXml = strXml & vbCrLf & "</ALLLEDGERENTRIES.LIST>"
                        End If
                        strXml = strXml & vbCrLf & "<PAYROLLMODEOFPAYMENT.LIST>"
                        strXml = strXml & vbCrLf & "</PAYROLLMODEOFPAYMENT.LIST>"
                        strXml = strXml & vbCrLf & "<ATTDRECORDS.LIST>"
                        strXml = strXml & vbCrLf & "</ATTDRECORDS.LIST>"
                        strXml = strXml & vbCrLf & "<TEMPGSTRATEDETAILS.LIST>"
                        strXml = strXml & vbCrLf & "</TEMPGSTRATEDETAILS.LIST >"
                        strXml = strXml & vbCrLf & "</VOUCHER>" & vbCrLf
                    End If
                    strSql = ""   '****Value for cells is not written must write after conformation from sir***
                    strSql = "Update bond_assessM set IsXMLG=1,IsXMLGBy='" & Session("UserId_BondCFS") & "' where AssessNo ='" & Trim(CType(row.FindControl("lblassessNo"), Label).Text) & "'AND WorkYear='" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "'"
                    db.sub_ExecuteNonQuery(strSql)
                End If
            Next
            btnsearch_Click(sender, e)
            UpdatePanel.Update()
            '--------------------------------------END-------------------------------------------------
            strXml = strXml & vbCrLf & "</TALLYMESSAGE>"
            strXml = strXml & vbCrLf & "<TALLYMESSAGE xmlns:UDF=""TallyUDF"">"
            strXml = strXml & vbCrLf & "<COMPANY>"
            strXml = strXml & vbCrLf & "<REMOTECMPINFO.LIST MERGE=""Yes"">"
            strXml = strXml & vbCrLf & "<NAME>" & "d2826762-075f-11da-96af-8929b305cf7d" & "</NAME>"
            strXml = strXml & vbCrLf & "<REMOTECMPNAME>" & "MSA Global Logistics Private Limited - (From 1-Apr-2015)" & "</REMOTECMPNAME>"
            strXml = strXml & vbCrLf & "<REMOTECMPSTATE>" & "Maharashtra" & "</REMOTECMPSTATE>"
            strXml = strXml & vbCrLf & "</REMOTECMPINFO.LIST>"
            strXml = strXml & vbCrLf & "</COMPANY>"
            strXml = strXml & vbCrLf & "</TALLYMESSAGE>"
            strXml = strXml & vbCrLf & "<TALLYMESSAGE xmlns:UDF=""TallyUDF"">"
            strXml = strXml & vbCrLf & "<COMPANY>"
            strXml = strXml & vbCrLf & "<REMOTECMPINFO.LIST MERGE=""Yes"">"
            strXml = strXml & vbCrLf & "<NAME>" & "d2826762-075f-11da-96af-8929b305cf7d" & "</NAME>"
            strXml = strXml & vbCrLf & "<REMOTECMPNAME>" & "MSA Global Logistics Private Limited - (From 1-Apr-2015)" & "</REMOTECMPNAME>"
            strXml = strXml & vbCrLf & "<REMOTECMPSTATE>" & "Maharashtra" & "</REMOTECMPSTATE>"
            strXml = strXml & vbCrLf & "</REMOTECMPINFO.LIST>"
            strXml = strXml & vbCrLf & "</COMPANY>"
            strXml = strXml & vbCrLf & "</TALLYMESSAGE>"
            strXml = strXml & vbCrLf & "</REQUESTDATA>"
            strXml = strXml & vbCrLf & "</IMPORTDATA>"
            strXml = strXml & vbCrLf & "</BODY>"
            strXml = strXml & TAG_END

            'filename = "BondINV" & Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & ".xml"
            'Dim strfilePath As String = ""
            ''strfilePath = "~/XMLFiles/"
            'strfilePath = Server.MapPath("~/XMLFiles/")
            ''strfilePath = "D:/Bond20092018/Bond/XMLFiles/"
            'strfilePath += filename
            'Dim writeFileXML As System.IO.TextWriter = New StreamWriter(strfilePath)
            'writeFileXML.WriteLine(strXml)
            'writeFileXML.Flush()
            'writeFileXML.Close()
            'writeFileXML = Nothing
            'Response.ContentType = ContentType
            'Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(strfilePath)))
            'Response.WriteFile(strfilePath)
            ''HttpContext.Current.ApplicationInstance.CompleteRequest()
            'Response.End()
            ViewState("DownloadData") = strXml

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:ShowAlert();", True)
            btnsearch_Click(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnHidden_Click(sender As Object, e As EventArgs) Handles btnHidden.Click
        Try
            Dim filename As String
            filename = "BondINV" & Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & ".xml"
            Dim strfilePath As String = ""
            'strfilePath = "~/XMLFiles/"
            strfilePath = Server.MapPath("~/XMLFiles/")
            'strfilePath = "D:/Bond20092018/Bond/XMLFiles/"
            strfilePath += filename
            Dim writeFileXML As System.IO.TextWriter = New StreamWriter(strfilePath)
            writeFileXML.WriteLine(ViewState("DownloadData"))
            writeFileXML.Flush()
            writeFileXML.Close()
            writeFileXML = Nothing
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(strfilePath)))
            Response.WriteFile(strfilePath)
            'HttpContext.Current.ApplicationInstance.CompleteRequest()
            Response.End()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkLock_Click(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            strSql = ""
            strSql += "Update bond_assessM set IsRLocked=1,IsRLockedBY='" & Session("UserId_BondCFS") & "' where AssessNo ='" & Trim(CType(gr.FindControl("lblassessNo"), Label).Text) & "'AND WorkYear='" & Trim(CType(gr.FindControl("lblworkyear"), Label).Text) & "'"
            db.sub_ExecuteNonQuery(strSql)
            btnsearch_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invoice Locked Successfully');", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
