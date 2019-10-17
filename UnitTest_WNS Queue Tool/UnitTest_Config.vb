Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WNSQueueTool

<TestClass()> Public Class UnitTest_Config
    Dim SUT As WNSQueueTool.Config
    Dim mPath As String = "C:\Users\ezekiel.gomez\Documents\GitHub\WNSQueueTool\Dummy Configs\dummyCfg1.xml"

    Public Sub New()

    End Sub

    <TestMethod()> Public Sub PropertyPath_StoredCorrectly()
        SUT = New Config(mPath)
        Assert.IsTrue(mPath = SUT.Path)
        SUT = Nothing
    End Sub
    <TestMethod()> Public Sub Database_GetDatabaseInfo()
        SUT = New Config(mPath)
        Assert.AreEqual("abcde", SUT.ConnectionString)
        Assert.IsTrue(SUT.DeadLockRetryCount = 5)
        SUT = Nothing
    End Sub
    <TestMethod()> Public Sub Queries_CountIs3()
        SUT = New Config(mPath)
        Assert.IsTrue(3 = SUT.Queries.Count)
        SUT = Nothing
    End Sub
    <TestMethod()> Public Sub Queries_FirstQuery_isSelectYou()
        SUT = New Config(mPath)
        Assert.AreEqual("Select * FROM You", SUT.Queries(0))
        SUT = Nothing
    End Sub
    <TestMethod()> Public Sub Queries_SecondQuery_isSelectHim()
        SUT = New Config(mPath)
        Assert.AreEqual("Select * FROM Him", SUT.Queries(1))
        SUT = Nothing
    End Sub
    <TestMethod()> Public Sub Queries_ThirdQuery_isSelectThem()
        SUT = New Config(mPath)
        Assert.AreEqual("Select * FROM Them", SUT.Queries(2))
        SUT = Nothing
    End Sub
    Protected Overrides Sub Finalize()

        MyBase.Finalize()
    End Sub
End Class