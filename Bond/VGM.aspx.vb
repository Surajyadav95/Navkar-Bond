Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Security
Imports System.IO
Imports System.Xml
Imports System.Security.Cryptography.X509Certificates
Imports Microsoft.Reporting.WebForms
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_bond_general
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode
    Private Crc32Table(255) As Long
    Dim dtSPID As New DataTable
    Dim dtTempI As New DataTable
    Dim strDesc As String = ""
    Dim strtable As String = ""
    Dim strText As String = ""
    Dim strMailTo As String = ""
    Dim strCcIDs As String = ""
    Dim strToIDs As String = ""
    Dim strBCCIDs As String = ""

    Dim strSubject As String = ""
    Dim strBodyText As String = ""
    Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    Dim streamids As String() = Nothing

    Dim mimeType As String = Nothing

    Dim encoding1 As String = Nothing

    Dim extension As String = Nothing

    Dim deviceInfo As String

    Dim bytes As Byte()

    Dim lr As New Microsoft.Reporting.WebForms.LocalReport
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

        End If

    End Sub
    Protected Sub Filldropdown()
        Try

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    'Public Sub VgmSave()

    '    Dim pWeighmentTransaction As WeighmentTransaction = prepareObjectFromControlsVGM()
    '    WeighmentTransaction.Insert(pWeighmentTransaction)
    '    If pWeighmentTransaction.TransactionId > 0 Then
    '        hdnTransactionId.Value = pWeighmentTransaction.TransactionId
    '        If hdnContStuffRefId.Value <> "" And hdnContStuffRefId.Value <> "0" Then
    '            Dim ParamCollection As New System.Collections.Hashtable
    '            HeaderfunVGM()
    '            Dim FileGenerationName As String = ConfigurationManager.AppSettings("CrystalreportrptPdfFilePath") & "WeighmentAnnex1_" & txtContainerNo.Text & Format(Now, "ddMMyyyyHHmm") & ".pdf"
    '            ''Session.Add("Parameters", ParamCollection)
    '            Session.Add("ReportPath", Server.MapPath("..\DotMatrixRep"))
    '            Dim ReportPath As String = Server.MapPath("..\DotMatrixRep")
    '            Dim Report1 As String = "WeighmentReportAnnex.rpt"

    '            Dim path_l As String = "http://"
    '            path_l = path_l + HttpContext.Current.Request.ServerVariables("SERVER_NAME") + ":"
    '            path_l = path_l + HttpContext.Current.Request.ServerVariables("SERVER_PORT") + "/"
    '            path_l = path_l + ConfigurationManager.AppSettings("ApplicationAlias") + "/"
    '            path_l = path_l + "CrViewerSch.aspx?ContStuffRefId=" + hdnContStuffRefId.Value + "&Locationid=" + Session.Item("loginLocation") + "&ContNo=" + txtContainerNo.Text + "&FileName=" + FileGenerationName + "&ReportPath=" + ReportPath + "&Report1=" + Report1
    '            ''Label1.Text = path_l

    '            '  System.Threading.Thread.Sleep(5000)
    '            Dim url As String = path_l
    '            Try
    '                Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
    '                Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
    '            Catch ex As Exception

    '            End Try
    '            SendMail(FileGenerationName)
    '            'Webservicecall()
    '            OdxSendData(pWeighmentTransaction.WeighmentSlipNo)
    '        End If

    '        manageFieldScripts(False)
    '        btnPrint.Visible = True
    '    End If
    'End Sub
    Private Sub OdxSendData()
        Dim Data As String = ""

        Dim OdxRefNO As String = "" ' "ODeX/MH/CFS/1701/50008"
        Dim OdcKey As String = "" ' "F900TZNE7YPL"
        Dim OdcUrl As String = "" ' "http://testvgm.odex.co/RS/iVGMService/xml/saveVgmWb"
        Dim OdcUserid As String = ""
        Dim OdcPassword As String = ""
        Dim WeightmentAddress1 As String = ""
        Dim WeightmentAddress2 As String = ""
        ''ODEX Data

        Dim machineip As String = ""
        machineip = "192.168.1.177" ''GetIPAddress() ' hdnMachineIp.Value
        Dim dbr As SqlDataReader
        'Dim db As New dbAccess
        Try
            dt1 = db.sub_GetDatatable("GET_VGM_REF_DATA")
            If dt1.Rows.Count > 0 Then
                OdxRefNO = Trim(dt1.Rows(0)("ODX_REF_CODE"))
                OdcKey = Trim(dt1.Rows(0)("ODX_KEY"))
                OdcUrl = Trim(dt1.Rows(0)("ODX_URL"))
                OdcUserid = Trim(dt1.Rows(0)("ODEX_USER_ID"))
                OdcPassword = Trim(dt1.Rows(0)("ODEX_PASSWORD"))
                WeightmentAddress1 = Trim(dt1.Rows(0)("ADDR_1"))
                WeightmentAddress2 = Trim(dt1.Rows(0)("ADDR_2"))
            End If
            'dbr = db.getDR("GET_VGM_REF_DATA")
            'If dbr.HasRows Then
            '    While dbr.Read
            '        If dbr("ODX_REF_CODE").ToString <> "" Then
            '            OdxRefNO = dbr("ODX_REF_CODE")
            '        End If
            '        If dbr("ODX_KEY").ToString <> "" Then
            '            OdcKey = dbr("ODX_KEY")
            '        End If
            '        If dbr("ODX_URL").ToString <> "" Then
            '            OdcUrl = dbr("ODX_URL")
            '        End If
            '        If dbr("ODEX_USER_ID").ToString <> "" Then
            '            OdcUserid = dbr("ODEX_USER_ID")
            '        End If
            '        If dbr("ODEX_PASSWORD").ToString <> "" Then
            '            OdcPassword = dbr("ODEX_PASSWORD")
            '        End If
            '        If dbr("ADDR_1").ToString <> "" Then
            '            WeightmentAddress1 = dbr("ADDR_1")
            '        End If
            '        If dbr("ADDR_2").ToString <> "" Then
            '            WeightmentAddress2 = dbr("ADDR_2")
            '        End If
            '    End While
            'End If
            'dbr.Close()
        Catch ex As Exception
        End Try
        'db.closeDB()
        ''End ODEX Data

        Dim address As Uri
        address = New Uri(OdcUrl)
        Data = XMLString(OdxRefNO, OdcKey, WeightmentAddress1, WeightmentAddress2)
        ServicePointManager.ServerCertificateValidationCallback = [Delegate].Combine(New RemoteCertificateValidationCallback(AddressOf ValidateServerCertificate))
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(address), HttpWebRequest)
        Dim bytes As Byte()
        bytes = System.Text.Encoding.ASCII.GetBytes(Data)
        ' ServicePointManager.Expect100Continue = True
        'ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
        ' ServicePointManager.DefaultConnectionLimit = 9999
        '-------------------------------------------------
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        '-------------------------------------------------

        request.ContentType = "application/xml; encoding='utf-8'"
        ' request.ContentType = "text/xml;charset=UTF-8"
        request.Credentials = New NetworkCredential(OdcUserid, OdcPassword)
        request.ContentLength = bytes.Length
        request.Method = "POST"
        Dim requestStream As Stream = request.GetRequestStream()
        requestStream.Write(bytes, 0, bytes.Length)
        requestStream.Close()

        Dim result As String = ""
        Dim responseStr As String = ""
        Dim response As HttpWebResponse
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        If response.StatusCode = HttpStatusCode.OK Then
            Dim responseStream As Stream = response.GetResponseStream()
            responseStr = New StreamReader(responseStream).ReadToEnd()
            If responseStr <> "" Then
                Dim sr As New System.IO.StringReader(responseStr)
                Dim doc As New XmlDocument
                doc.Load(sr)
                Dim reader As New XmlNodeReader(doc)
                While reader.Read()
                    Select Case reader.NodeType
                        Case XmlNodeType.Element
                            If reader.Name = "response" Then
                                If reader.Read() Then
                                    If reader.Value.Trim() <> Nothing Then
                                        result = reader.Value.Trim()
                                    End If
                                End If
                            End If
                    End Select
                End While
            End If
        End If
        strSql = ""
        strSql = " UPDATE VGM_D SET response='" & result & "' WHERE SlipId='" & Trim(txtVGMNo.Text & "") & "'"
        db.sub_ExecuteNonQuery(strSql)

        ''INSERT ODEXDATA IN TABLE
        'Dim ERROR_MSG As String = ""

        'Dim dbr2 As OleDb.OleDbDataReader
        'Dim db2 As New dbAccess
        'Try
        '    dbr2 = db2.ReadDB("INSERT INTO DBO.ODEX_CONTAINER_DETAILS(CONT_NO,VGM_GEN_DATE,ODEX_STATUS,INPUT_STRING,RESPONSE,CONT_REF_ID) VALUES ( " & "'" & txtContainerNo.Text & "'," & "pkg_functions.to_date('" & Format(Now, "dd\/MM\/yyyy HH:mm") & "'),'" & result & "','" & Data & "','" & responseStr & "'," & hdnContRefId.Value & ")")
        '    dbr2.Close()
        'Catch ex As Exception
        'End Try
        'db2.closeDB()

        'Dim dbO As New DBAccess 'object:db for database connectivity from class:DBAccess
        'Try
        '    dbO.BeginTransaction()
        '    'Before Assigning class variables object:db for database should get clear to store parameters
        '    dbO.ClearParameters()
        '    'Assigning class variables to parameters of stored procedure

        '    dbO.AddParameter("@CONT_NO", txtContainerNo.Text)
        '    dbO.AddParameter("@ODEX_STATUS", result)
        '    dbO.AddParameter("@INPUT_STRING", Data)
        '    dbO.AddParameter("@REPONSE", responseStr)
        '    dbO.AddParameter("@CONT_REF_ID", hdnContRefId.Value)
        '    dbO.AddParameter("@ERRMSG", ERROR_MSG, ParameterDirection.Output)
        '    'Executing store procedure
        '    dbO.ExecuteScalar("PKG_INSERT.ODEX_CONTAINER_DETAILS_INS", CommandType.StoredProcedure)
        '    'Assigning return value of stored procedure to class object
        '    If dbO.Parameters.Item("@ERRMSG").value.ToString <> Nothing Then
        '        ERROR_MSG = dbO.Parameters.Item("@ERRMSG").value.ToString
        '        dbO.RollbackTransaction() ' Rollback the transaction if any 
        '    Else
        '        dbO.CommitTransaction()   'Commits the transaction
        '    End If
        'Catch ex As Exception
        '    dbO.RollbackTransaction() ' Rollback the transaction if any 
        'End Try
        'dbO.CloseDB()

        ''END  ODEXDATA
    End Sub
    Public Shared Function ValidateServerCertificate(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function
    'Public Shared Function ComputeChecksum(ByVal bytes As Byte()) As UShort
    '    Dim crc As UShort = &H0US ' The calculation start with 0x00
    '    For i As Integer = 0 To bytes.Length - 1
    '        Dim index As Byte = CByte(((crc) And &HFF) Xor bytes(i))
    '        crc = CUShort((crc >> 8) Xor table(index))
    '    Next
    '    Return Not crc
    'End Function
    Private Function XMLString(ByVal pOdxRefNo As String, ByVal pOdcKey As String, ByVal strWeightmentAddress1 As String, ByVal strWeightmentAddress2 As String) As String
        Dim hashKey As String = ""
        Dim strString As String = ""
        Dim Data As String = ""
        Dim WebBrigWtTime As String = ""

        'Dim dbr As OleDb.OleDbDataReader
        Dim dbr As SqlDataReader
        'Dim db As New dbAccess
        'Try
        '    'dbr = db.storedprocedure_ReadDB("WEIGHMENT_TRANSACTION_DATE", hdnContRefId.Value)
        '    dbr = db.getDR("WEIGHMENT_TRANSACTION_DATE " & hdnContRefId.Value & "")

        '    If dbr.HasRows Then
        '        While dbr.Read
        '            If dbr("DATE_TIME").ToString <> "" Then
        '                WebBrigWtTime = dbr("DATE_TIME")
        '            End If
        '        End While
        '    End If
        '    dbr.Close()
        'Catch ex As Exception
        'End Try
        'db.closeDB()
        WebBrigWtTime = Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm:ss")
        Dim OdxRefNO As String = pOdxRefNo
        Dim OdcKey As String = pOdcKey
        Dim WebBrigRegNo As String = "95960"
        Dim WebBrigSlipNo As String = ""


        Dim ContSize As String = ""
        Dim MaxPayble As String = ""
        ' Dim weighBridgeAddrLn1 As String = "kon-savla road,tal-panvel"
        Dim arr As String = ""
        For Each row In grdVGMDets.Rows
            Dim GrossWt As Double = Val(CType(row.FindControl("txtTareweight"), TextBox).Text) + Val(CType(row.FindControl("txtCargoWt"), TextBox).Text)
            strSql = ""
            strSql += "USP_INSERT_INTO_VGM_D '" & Trim(CType(row.FindControl("lblShipperID"), Label).Text) & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "',"
            strSql += "'" & Trim(CType(row.FindControl("lblEntryID"), Label).Text) & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text) & "',"
            strSql += "'" & Trim(CType(row.FindControl("txtPayLoadID"), TextBox).Text) & "','1','" & Trim(CType(row.FindControl("txtCargoWt"), TextBox).Text) & "',"
            strSql += "'" & Trim(CType(row.FindControl("txtTareweight"), TextBox).Text) & "','" & GrossWt & "',"
            strSql += "'" & Trim(CType(row.FindControl("lblCargoType"), Label).Text) & "','" & Trim(CType(row.FindControl("lblUNNO"), Label).Text) & "','" & Session("UserId_BondCFS") & "',"
            strSql += "'" & Trim(CType(row.FindControl("txtContactPerson"), TextBox).Text) & "','" & Trim(CType(row.FindControl("txtContactNo"), TextBox).Text) & "'"

            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                WebBrigSlipNo = Trim(dt.Rows(0)("SlipNo") & "")
                txtVGMNo.Text = Trim(dt.Rows(0)("SlipID") & "")
            End If
            Dim hdntxtNetWt As Double = 0
            If Trim(CType(row.FindControl("txtTareweight"), TextBox).Text) = "" Then
                hdntxtNetWt = "0"
            End If
            ContSize = Trim(CType(row.FindControl("lblSize"), Label).Text)
            MaxPayble = Trim(CType(row.FindControl("txtPayLoadID"), TextBox).Text)

            arr = Math.Round(Convert.ToDouble(Trim(CType(row.FindControl("txtTareweight"), TextBox).Text)), 2) + Math.Round(Convert.ToDouble(Trim(CType(row.FindControl("txtCargoWt"), TextBox).Text)), 2)

            If arr.IndexOf(".") = -1 Then
                arr = arr + ".00"
            End If
        Next

        strString = txtContainerNo.Text + arr + OdxRefNO + WebBrigSlipNo + OdcKey

        Dim lCrc32Value As Long

        On Error Resume Next
        lCrc32Value = InitCrc32()
        lCrc32Value = AddCrc32(strString, _
           lCrc32Value)

        hashKey = Crc32.ComputeChecksum(Encoding.UTF8.GetBytes(strString))

        'GetCrc32(lCrc32Value)
        'Crc32.ComputeChecksum(Encoding.UTF8.GetBytes(strString))


        Data = "<?xml version=" + """1.0""" + " encoding=" + """UTF-8""" + " ?><iVGMWBVo><cntnrNo>" _
        + txtContainerNo.Text + "</cntnrNo><bookNo></bookNo><totWt>" + arr + "</totWt><weighBridgeRegNo>" _
        + WebBrigRegNo + "</weighBridgeRegNo><weighBridgeSlipNo>" + WebBrigSlipNo + "</weighBridgeSlipNo><weighBridgeWtTs>" _
        + WebBrigWtTime + "</weighBridgeWtTs><odexRefNo>" + OdxRefNO + "</odexRefNo><authDesignation></authDesignation>" _
        + "<cntnrSize>1X" + ContSize + "</cntnrSize><cscPlateMaxWtLimit>" + MaxPayble + "</cscPlateMaxWtLimit>" _
        + "<weighBridgeAddrLn1>" + strWeightmentAddress1 + "</weighBridgeAddrLn1><weighBridgeAddrLn2>" + strWeightmentAddress2 + "</weighBridgeAddrLn2><authMobNo></authMobNo><locId></locId><linerId>" _
        + "</linerId><authPrsnNm></authPrsnNm><hashKey>" + hashKey + "</hashKey></iVGMWBVo>"

        Return Data
    End Function
    Protected Sub btnVGMShow_Click(sender As Object, e As EventArgs)
        Try
            If Len(Trim(txtContainerNo.Text & "")) <> "11" Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('Enter valid Container No.');", True)
                txtContainerNo.Text = ""
                Exit Sub
            End If
            txtVGMNo.Text = ""
            strSql = ""
            strSql += "get_sp_vgm_details '" & Trim(txtContainerNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                grdVGMDets.DataSource = dt
                grdVGMDets.DataBind()
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('Records not found.');", True)
                txtContainerNo.Text = ""
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnVGMSend_Click(sender As Object, e As EventArgs)
        Try
            Dim blnVGM As Boolean = False
            For Each row In grdVGMDets.Rows
                blnVGM = True
            Next
            If blnVGM = False Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('First Validate VGM Details');", True)
                txtContainerNo.Focus()
                Exit Sub
            End If

            For Each row In grdVGMDets.Rows
                Dim hdntxtNetWt As Double = 0
                If (Trim(CType(row.FindControl("txtTareweight"), TextBox).Text) = "" Or Trim(CType(row.FindControl("txtTareweight"), TextBox).Text) = "0") Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('Tare Weight should not be blank');", True)
                    txtContainerNo.Focus()
                    Exit Sub
                End If
                If (Trim(CType(row.FindControl("txtCargoWt"), TextBox).Text) = "" Or Trim(CType(row.FindControl("txtCargoWt"), TextBox).Text) = "0") Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('Cargo Weight should not be blank');", True)
                    txtContainerNo.Focus()
                    Exit Sub
                End If
                If (Trim(CType(row.FindControl("txtGrossWt"), TextBox).Text) = "" Or Trim(CType(row.FindControl("txtGrossWt"), TextBox).Text) = "0") Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('Gross Weight should not be blank');", True)
                    txtContainerNo.Focus()
                    Exit Sub
                End If
                If (Trim(CType(row.FindControl("txtPayLoadID"), TextBox).Text) = "" Or Trim(CType(row.FindControl("txtPayLoadID"), TextBox).Text) = "0") Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('Pay Load should not be blank');", True)
                    txtContainerNo.Focus()
                    Exit Sub
                End If
                If (Val(CType(row.FindControl("txtPayLoadID"), TextBox).Text) < Val(CType(row.FindControl("txtGrossWt"), TextBox).Text)) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('Pay Load should not be smaller than Gross Wt');", True)
                    txtContainerNo.Focus()
                    Exit Sub
                End If
            Next

            OdxSendData()
            Dim SLIPID As String = Trim(txtVGMNo.Text & "")

            strSql = ""
            strSql += "USP_VALIDATION_VGM_MAILS '" & SLIPID & "'"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 Then

                GoTo lblnext
            Else
                LoadReport(SLIPID, sender, e)
            End If
lblnext:
            Clear()
            btnVGMSend.Text = "Send"
            btnVGMSend.Attributes.Add("Class", "btn btn-primary")
            Label2.Text = "Record Saved successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel4.Update()

        Catch ex As Exception
            btnVGMSend.Text = "Send"
            btnVGMSend.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Clear()
        txtContainerNo.Text = ""

        strSql = ""
        dt = db.sub_GetDatatable(strSql)
        grdVGMDets.DataSource = dt
        grdVGMDets.DataBind()
    End Sub
    Public Function InitCrc32(Optional ByVal Seed As Long = _
   &HEDB88320, Optional ByVal Precondition As _
   Long = &HFFFFFFFF) As Long

        '// Declare counter variable iBytes, 
        'counter variable iBits, 
        'value variables lCrc32 and lTempCrc32

        Dim iBytes As Integer, iBits As Integer, lCrc32 As Long
        Dim lTempCrc32 As Long

        '// Turn on error trapping
        On Error Resume Next

        '// Iterate 256 times
        For iBytes = 0 To 255

            '// Initiate lCrc32 to counter variable
            lCrc32 = iBytes

            '// Now iterate through each bit in counter byte
            For iBits = 0 To 7
                '// Right shift unsigned long 1 bit
                lTempCrc32 = lCrc32 And &HFFFFFFFE
                lTempCrc32 = lTempCrc32 \ &H2
                lTempCrc32 = lTempCrc32 And &H7FFFFFFF

                '// Now check if temporary is less than zero and then 
                'mix Crc32 checksum with Seed value
                If (lCrc32 And &H1) <> 0 Then
                    lCrc32 = lTempCrc32 Xor Seed
                Else
                    lCrc32 = lTempCrc32
                End If
            Next

            '// Put Crc32 checksum value in the holding array
            Crc32Table(iBytes) = lCrc32
        Next

        '// After this is done, set function value to the 
        'precondition value
        InitCrc32 = Precondition

    End Function

    '// The function above is the initializing function, now 
    'we have to write the computation function
    Public Function AddCrc32(ByVal Item As String, _
      ByVal Crc32 As Long) As Long

        '// Declare following variables
        Dim bCharValue As Byte, iCounter As Integer, lIndex As Long
        Dim lAccValue As Long, lTableValue As Long

        '// Turn on error trapping
        On Error Resume Next

        '// Iterate through the string that is to be checksum-computed
        For iCounter = 1 To Len(Item)

            '// Get ASCII value for the current character
            bCharValue = Asc(Mid$(Item, iCounter, 1))

            '// Right shift an Unsigned Long 8 bits
            lAccValue = Crc32 And &HFFFFFF00
            lAccValue = lAccValue \ &H100
            lAccValue = lAccValue And &HFFFFFF

            '// Now select the right adding value from the 
            'holding table
            lIndex = Crc32 And &HFF
            lIndex = lIndex Xor bCharValue
            lTableValue = Crc32Table(lIndex)

            '// Then mix new Crc32 value with previous 
            'accumulated Crc32 value
            Crc32 = lAccValue Xor lTableValue
        Next

        '// Set function value the the new Crc32 checksum
        AddCrc32 = Crc32

    End Function

    '// At last, we have to write a function so that we 
    'can get the Crc32 checksum value at any time
    Public Function GetCrc32(ByVal Crc32 As Long) As Long
        '// Turn on error trapping
        On Error Resume Next

        '// Set function to the current Crc32 value
        GetCrc32 = Crc32 Xor &HFFFFFFFF

    End Function
    Private Sub LoadReport(SLIPID As String, sender As Object, e As EventArgs)
        Try
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/VGMRDLC.rdlc")

            'Dim VGMNo As String = ed.Decrypt(HttpUtility.UrlDecode(SLIPID))

            dt = db.sub_GetDatatable("USP_VGM_PRINT '" & SLIPID & "'")
            If dt.Rows.Count > 0 Then
                Dim ShipperName As String = Trim(dt.Rows(2)("Particulars") & "")
                Dim WDate As String = Convert.ToDateTime(Trim(dt.Rows(9)("Particulars") & "")).ToString("dd-MM-yyyy")

                Dim p1 As New ReportParameter("ShipperName", ShipperName)
                Dim p2 As New ReportParameter("WDate", WDate)

                Dim datasource As New ReportDataSource("DataSet1", dt)

                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.DataSources.Add(datasource)

                Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2})
            End If

            strSql = ""
            strSql += "USP_VALIDATION_VGM_MAILS '" & SLIPID & "'"
            dtSPID = db.sub_GetDatatable(strSql)
            If dtSPID.Rows.Count > 0 Then
                strText = ""
                strText = "<font face='Calibri' color='black' size=3>A VGM, &lt; " & Trim(dtSPID.Rows(0)("SlipNo")) & " &gt; has been generated at " & Trim(dtSPID.Rows(0)("Yard")) & " for container No " & Trim(dtSPID.Rows(0)("ContainerNo")) & "</font> <br>"

                strtable += "<html><body>"
                strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black' Style='width: 100%; height: auto;'>  "
                strtable += "<tr bgcolor='white'><font face='Calibri' color='black' size='3'>"
                strtable += "<td style='padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'>Greetings, <br><br>" & strText & "<br>"
                strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black' Style='width: 100%; height: auto;'>  "
                strtable = strtable & "<font face='Calibri' color='black' size=3>Please find attachment. </font> "
                strtable += "<br>"
                strtable += "<font face='Calibri' color='black' size=3>"

                strtable += "<br>"
                strtable += "<font face='Calibri' color='black' size=3>Best Regards,<br>"
                ' strtable += "[" & Trim(dtSPID.Rows(ispID)("Person Name")) & "]</font>"
                strtable += "&lt; " & Trim(dtSPID.Rows(0)("Regards")) & " &gt; </font>"

                strtable += "</table>"
                strtable += "</body></html>"
                strtable += "</table>"
                strCcIDs = ""
                strBCCIDs = ""
                For i = 0 To dtSPID.Rows.Count - 1
                    strMailTo = Trim(dtSPID.Rows(i)("MailTo")) + ";" + strMailTo
                    If Trim(dtSPID.Rows(i)("MailCC")) <> "" Then
                        strCcIDs = Trim(dtSPID.Rows(i)("MailCC")) + ";" + strCcIDs
                    End If
                    If Trim(dtSPID.Rows(i)("MailBCC")) <> "" Then
                        strBCCIDs = Trim(dtSPID.Rows(i)("MailBCC")) + ";" + strBCCIDs
                    End If
                Next
                strToIDs = strMailTo
                If (strToIDs = ";;") Then
                    Exit Sub
                End If

                strSubject = "VGM for [" & Trim(dtSPID.Rows(0)("ContainerNo")) & "] "
                strBodyText = strtable & "<br><br>"
                Dim strmaildomain As String = ""
                'Dim strCcIDs As String = ""
                Dim intPortNo As Integer
                Dim strFrom As String = ""
                Dim strMailPswrd As String = ""
                Dim mm As New MailMessage
                Dim varSplitTo() As String

                strmaildomain = Trim(dtSPID.Rows(0)("SMTPServer"))
                intPortNo = Val(dtSPID.Rows(0)("SMTPServerPort"))
                strFrom = Trim(dtSPID.Rows(0)("MailFromID"))
                strMailPswrd = Trim(dtSPID.Rows(0)("UserPassword"))
                Dim mailAddress As New MailAddress(strFrom)
                If Trim(strToIDs) <> "" Then
                    varSplitTo = Split(strToIDs, ";")
                    For Each Toid As String In varSplitTo
                        If Trim(Toid) <> "" And Len(Trim(Toid)) >= 5 Then
                            If InStr(Toid, "@") > 0 Then
                                mm.To.Add(Trim(Toid))
                            End If
                        End If
                    Next
                End If
                If Trim(strCcIDs) <> "" Then
                    varSplitTo = Split(strCcIDs, ";")
                    For Each Toid As String In varSplitTo
                        If Trim(Toid) <> "" And Len(Trim(Toid)) >= 5 Then
                            If InStr(Toid, "@") > 0 Then
                                mm.CC.Add(Trim(Toid))
                            End If
                        End If
                    Next
                End If
                If Trim(strBCCIDs) <> "" Then
                    varSplitTo = Split(strBCCIDs, ";")
                    For Each Toid As String In varSplitTo
                        If Trim(Toid) <> "" And Len(Trim(Toid)) >= 5 Then
                            If InStr(Toid, "@") > 0 Then
                                mm.Bcc.Add(Trim(Toid))
                            End If
                        End If
                    Next
                End If
                mm.Subject = strSubject
                mm.Body = strBodyText
                mm.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "VGM_" & Trim(dtSPID.Rows(0)("ContainerNo")) & ".pdf")))
                mm.IsBodyHtml = True
                mm.From = mailAddress
                Dim smtpClient As New SmtpClient(strmaildomain, intPortNo)
                Dim credentials As New System.Net.NetworkCredential(strFrom, strMailPswrd)
                smtpClient.EnableSsl = True
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = credentials
                smtpClient.Send(mm)
                strSql = ""
                strSql = " UPDATE VGM_D SET IsMailSend=1 WHERE SlipId='" & SLIPID & "'"
                db.sub_ExecuteNonQuery(strSql)

                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Mail Sent successfully');", True)
                'btnSave_Click(Sender, e)
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Function ExportReportToPDF(path As String, reportName As String) As String

        Dim bytes As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, mimeType, encoding1, extension, streamids, warnings)
        Dim filename As String = path & reportName
        Using fs = New System.IO.FileStream(filename, System.IO.FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
        End Using

        Return filename
    End Function
End Class
