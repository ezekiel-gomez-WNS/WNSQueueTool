Imports System.Web.Script.Serialization
Public Class GenericHelpers
    Public Function ConvertJSONtoDictionary(JSONString As String) As Dictionary(Of String, Object)
        Try
            Dim arrTxt() As String = Split(JSONString.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", ""), ",")
            Dim keyDict As String
            Dim valueDict As String

            ConvertJSONtoDictionary = New Dictionary(Of String, Object)

            For i As Long = 0 To arrTxt.Length - 1

                keyDict = arrTxt(i).Split(":")(0)
                keyDict = keyDict.Remove(0, 1)
                keyDict = keyDict.Remove(keyDict.Length - 1, 1)
                valueDict = arrTxt(i).Split(":")(1)
                valueDict = valueDict.Remove(0, 1)
                valueDict = valueDict.Remove(valueDict.Length - 1, 1)

                If ConvertJSONtoDictionary.ContainsKey(keyDict) Then
                    ConvertJSONtoDictionary = Nothing
                    Throw New Exception("Duplicate Key Found.")
                Else
                    ConvertJSONtoDictionary.Add(keyDict, valueDict)
                End If
            Next
        Catch e As Exception
            Throw New Exception(e.Message)
        End Try
    End Function

    Public Function ConvertDTtoJSON(dt As DataTable) As String
        Return New JavaScriptSerializer().Serialize(From dr As DataRow In
        dt.Rows Select dt.Columns.Cast(Of DataColumn)().ToDictionary _
        (Function(col) col.ColumnName, Function(col) dr(col)))
    End Function
End Class
