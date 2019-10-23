Public Class GenericHelpers
    'Public Function ConvertJSONtoDictionary(JSONString As String) As Dictionary(Of String, Object)
    '    Try
    '        Dim arrTxt() As String = Split(JSONString.Replace("{", "").Replace("}", ""), ",")
    '        Dim txtCnt1 As Integer = arrTxt.Distinct().Count()

    '        For i As Integer = 0 To arrTxt.Length - 1
    '            arrTxt(i) = Left(arrTxt(i), arrTxt(i).IndexOf(":"))
    '        Next

    '        Dim txtCnt2 As Integer = arrTxt.Distinct().Count()

    '        If txtCnt1 <> txtCnt2 Then
    '            Throw New Exception("Duplicate Key Found.")
    '        End If
    '        If ConvertJSONtoDictionary.ContainsKey(txtCnt1) Then
    '            Throw New Exception("Duplicate Key Found.")
    '        End If

    '        Dim dict As Object = New JavaScriptSerializer().Deserialize(Of Object)(JSONString)
    '        Return dict
    '    Catch e As Exception
    '        Throw New Exception(e.Message)
    '    End Try
    'End Function

    Public Function ConvertJSONtoDictionary(JSONString As String) As Dictionary(Of String, Object)
        Try
            Dim arrTxt() As String = Split(JSONString.Replace("{", "").Replace("}", ""), ",")
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
End Class
