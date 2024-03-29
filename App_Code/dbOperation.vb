﻿Imports System.Data.SqlClient
Imports System.Data

Public Class dbOperation


    Public conn As System.Data.SqlClient.SqlConnection
    Public cmd As New SqlCommand
    Public Sub sub_ConnectDB()
        Try
            Dim strConnString As String = "", strFileName As String = "", stringReader As String = ""

            '   strConnString = "Data Source=XYZ-PC\SQLEXPRESS;Initial Catalog=SignTechnic11112016;Integrated Security=True;"
            strConnString = ConfigurationManager.ConnectionStrings("SqlConnString").ConnectionString

            If conn Is Nothing OrElse conn.ConnectionString = Nothing Then
                conn = New System.Data.SqlClient.SqlConnection(strConnString)
                ' System.Math.Max(System.Threading.Interlocked.Increment(connCount), connCount - 1)
            End If
            If conn.State = System.Data.ConnectionState.Closed Then
                Try
                    conn.Open()
                Catch Ex As Exception
                    conn = New System.Data.SqlClient.SqlConnection(strConnString)
                    conn.Open()
                    '  System.Math.Max(System.Threading.Interlocked.Increment(connCount), connCount - 1)
                End Try
            End If
            If cmd Is Nothing Then
                cmd = New System.Data.SqlClient.SqlCommand()
                cmd.CommandType = System.Data.CommandType.Text
                cmd.Connection = conn
            End If

        Catch ex As Exception
            If conn.State = ConnectionState.Open Then conn.Close()
            '  MsgBox("Error in procedure: " & System.Reflection.MethodBase.GetCurrentMethod.Name & vbCrLf & ex.Message.ToString)
        End Try
    End Sub

    Public Function sub_GetDatatable(ByVal strSQL As String) As DataTable
        Dim dt As New DataTable
        Try
            sub_ConnectDB()
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim cmd As New SqlCommand(strSQL, conn)
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd
            da.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
           
        End Try

        Return dt
    End Function
    Public Function sub_GetDataSets(ByVal strSQL As String) As DataSet
        Dim ds As New DataSet
        Try
            sub_ConnectDB()
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim cmd As New SqlCommand(strSQL, conn)
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd
            da.Fill(ds)
            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
          
        End Try

        Return ds
    End Function

    Public Function sub_ExecuteNonQuery(ByVal strSQL As String) As Integer
        Dim i As Integer
        Try
            sub_ConnectDB()
            Dim cmd As New SqlCommand(strSQL, conn)
            i = cmd.ExecuteNonQuery()

        Catch ex As Exception
         
        End Try
        Return i
    End Function
 
    Public Function sub_getSingleValue(ByVal strExcQuery As String, ByVal blIstrim As Boolean, ByVal colName As String) As String
        Dim dt As New DataTable, strReturnValue As String = ""
        Try
            sub_ConnectDB()
            Dim cmd As New SqlCommand(strExcQuery, conn)
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd
            da.Fill(dt)
           
            If dt.Rows.Count > 0 Then
                If blIstrim = True Then
                    strReturnValue = Trim(dt.Rows(0)(colName))
                Else
                    strReturnValue = dt.Rows(0)(colName)
                End If
            Else
                strReturnValue = ""
            End If

        Catch ex As Exception
          
        End Try

        Return strReturnValue
    End Function
    Public Function sub_SendMail(ByVal strModuleName As String, ByVal strDocumentName As String, ByVal strSubject As String, ByVal strBodyText As String, ByVal strToIDs As String, ByVal strCcIDs As String, ByVal strBccIDs As String, ByVal strAttachFileName As String, ByVal strAttachFilePath As String, ByVal UserID As Integer, ByVal SalesPersonID As Integer) As Boolean
        Dim blFlag_Mail As Boolean = False
        Dim dtDate As New Date, strFrom As String = "", strSQL As String = ""
        Try

            dtDate = Now

            'dtTemp.Clear()
            'strSQL = ""
            'strSQL = "Select AutoMail_emailID From Settings "
            'dtTemp = sub_GetDatatable(strSQL)
            'If dtTemp.Rows.Count > 0 Then
            '    strFrom = dtTemp.Rows(0)("AutoMail_emailID")
            'Else
            '    strFrom = ""
            'End If

            'Updating(MailLogDets)
            Dim cmd As New SqlCommand
            strSQL = ""
            strSQL = "INSERT INTO MailLogDets(EntryDate, ModuleName, DocumentSent, DocName, DocPath, Subject, BodyText, FromId, ToIds, CCIds, BCCIds, IsMailSent, SalesPersonID) "
            strSQL += "VALUES(@EntryDate, @ModuleName, @DocumentSent, @DocName, @DocPath, @Subject, @BodyText, @FromId, @ToIds, @CCIds, @BCCIds, @IsMailSent, @SalesPersonID) "
            cmd.Parameters.AddWithValue("@EntryDate", Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            cmd.Parameters.AddWithValue("@ModuleName", Trim(strModuleName))
            cmd.Parameters.AddWithValue("@DocumentSent", Trim(strDocumentName))
            cmd.Parameters.AddWithValue("@DocName", Trim(strAttachFileName))
            cmd.Parameters.AddWithValue("@DocPath", Trim(strAttachFilePath))
            cmd.Parameters.AddWithValue("@Subject", strSubject)
            cmd.Parameters.AddWithValue("@BodyText", strBodyText)
            cmd.Parameters.AddWithValue("@FromID", strFrom)
            cmd.Parameters.AddWithValue("@ToIds", Trim(strToIDs))
            cmd.Parameters.AddWithValue("@CCIds", Trim(strCcIDs))
            cmd.Parameters.AddWithValue("@BCCIds", Trim(strBccIDs))
            cmd.Parameters.AddWithValue("@IsMailSent", False)
            cmd.Parameters.AddWithValue("@SalesPersonID", SalesPersonID)
            'cmd.Parameters.AddWithValue("@ErrorMailDate", "")
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL
            cmd.Connection = conn
            If conn.State = ConnectionState.Closed Then conn.Open()
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            conn.Close()

            blFlag_Mail = True

        Catch ex As Exception


        End Try

        Return blFlag_Mail
    End Function

    Public Function ExecuteUsingCmd(cmd As SqlCommand, strSQL As String) As Integer
        Dim i As Integer = 0
        Try
            sub_ConnectDB()
            cmd.CommandText = strSQL
            cmd.Connection = conn
            i = cmd.ExecuteNonQuery()
        Catch ex As Exception
            i = 0
        End Try

        Return i
    End Function
End Class
