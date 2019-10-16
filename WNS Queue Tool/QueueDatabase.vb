Public Class QueueDatabase
    Public Sub New(connectionString As String)

    End Sub

    Public Sub Connect()

    End Sub
    Public Sub Disconnect()

    End Sub
    Public Function ExecuteNonQuery(qry As String) As Long
        Return Nothing
    End Function
    Public Function ExecuteQuery(qry As String) As DataTable
        Return Nothing
    End Function

End Class
