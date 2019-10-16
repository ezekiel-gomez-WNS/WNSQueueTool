Imports System.Xml
Public Class Config
    Dim mPath As String
    Dim mXmlDoc As Xml.XmlDocument
    Dim mDatabaseTag As XmlElement
    Dim mQueriesTag As XmlElement
    Dim mQueries As String()

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

    Public ReadOnly Property Queries As String()
        Get
            Dim i As Long
            ReDim mQueries(mQueriesTag.ChildNodes.Count - 1)
            For i = 0 To mQueries.Count - 1
                mQueries(i) = mQueriesTag.ChildNodes(i).InnerText
            Next
            Return mQueries
        End Get
    End Property

End Class
