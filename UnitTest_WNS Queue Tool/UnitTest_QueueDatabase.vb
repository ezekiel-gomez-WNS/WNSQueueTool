Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WNSQueueTool

<TestClass()> Public Class UnitTest_QueueDatabase
    Private SUT As QueueDatabase
    Private mConfig As Config
    Private mCfgPath As String = "C:\Users\ezekiel.gomez\Documents\GitHub\WNSQueueTool\Dummy Configs\dummyCfg2.xml"

    Public Sub New()
        mConfig = New Config(mCfgPath)
        SUT = New QueueDatabase(mConfig.ConnectionString, mConfig.DeadLockRetryCount)
    End Sub

    <TestMethod()> Public Sub ExecuteQuery_Query0_Returns5Rows5cols()
        Dim dt As DataTable
        SUT.Connect()
        dt = SUT.ExecuteQuery(mConfig.Queries(0))

        Assert.IsTrue(dt.Rows.Count = 5)
        Assert.IsTrue(dt.Columns.Count = 5)

    End Sub

    Protected Overrides Sub Finalize()
        mConfig = Nothing
        SUT = Nothing
        MyBase.Finalize()
    End Sub
End Class