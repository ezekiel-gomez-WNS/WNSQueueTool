Imports System.Xml
Public Class Config
    Private mPath As String
    Private mXmlDoc As Xml.XmlDocument
    Private mDatabaseTag As XmlElement
    Private mQueriesTag As XmlElement
    Private mQueries As String()

    Public Sub New(filepath As String)
        mPath = filepath
        mXmlDoc = New Xml.XmlDocument()
        mXmlDoc.Load(mPath)
        mDatabaseTag = mXmlDoc.ChildNodes(0).Item("Database")
        mQueriesTag = mXmlDoc.ChildNodes(0).Item("Queries")
    End Sub

    Public ReadOnly Property Path As String
        Get
            Return mPath
        End Get
    End Property

    Public ReadOnly Property ConnectionString As String
        Get
            Return mDatabaseTag.Item("ConnectionString").InnerText
        End Get
    End Property
    Public ReadOnly Property DeadLockRetryCount As Long
        Get
            Return mDatabaseTag.Attributes("deadlockretrycount").Value
        End Get
    End Property
    Public ReadOnly Property Queries As String()
        Get
            Dim i As Long
            ReDim mQueries(mQueriesTag.ChildNodes.Count - 1)
            For i = 0 To mQueries.Count - 1
                mQueries(i) = mQueriesTag.ChildNodes(i).InnerText.Trim
            Next
            Return mQueries
        End Get
        'sample utfyfuvyuhvyt
    End Property

End Class
