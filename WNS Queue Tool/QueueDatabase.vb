Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Data.OleDb

Public Class QueueDatabase
    Private mConStr As String
    Private mDeadlockRetryCount As Long
    Private mCon As SqlConnection

    Public Sub New(connectionString As String, Optional deadlockRetryCounter As Long = 0)
        mConStr = connectionString
        mDeadlockRetryCount = deadlockRetryCounter
        mCon = New SqlConnection
    End Sub

    Public Sub Connect()
        If mCon.State = ConnectionState.Open Then
        Else
            mCon.ConnectionString = mConStr
            mCon.Open()
        End If
    End Sub
    Public Sub Disconnect()
        If mCon.State = ConnectionState.Open Then
            mCon.Close()
        End If
        mCon = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        Disconnect()
        MyBase.Finalize()
    End Sub

    Public Function ExecuteNonQuery(qry As String) As Long
        Dim flag As Boolean
        Dim retryCounter As Long
        Dim cmd As New SqlCommand(qry, mCon)
        ExecuteNonQuery = 0
        Do
            Try
                ExecuteNonQuery = cmd.ExecuteNonQuery()
                flag = True
            Catch ex As SqlException
                If retryCounter < mDeadlockRetryCount Then
                    retryCounter = retryCounter + 1
                Else
                    flag = True
                End If
            End Try
        Loop While (Not flag)

        cmd = Nothing
    End Function


    Public Function ExecuteQuery(qry As String) As DataTable
        Dim flag As Boolean
        Dim retryCounter As Long
        Dim da As SqlDataAdapter
        ExecuteQuery = New DataTable
        Do
            Try
                da = New SqlDataAdapter(qry, mCon)
                da.Fill(ExecuteQuery)
                da = Nothing
                flag = True
            Catch ex As SqlException
                If retryCounter < mDeadlockRetryCount Then
                    retryCounter = retryCounter + 1
                Else
                    flag = True
                End If
            End Try
        Loop While (Not flag)

    End Function

End Class
