Imports System.Data.SqlClient
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WNSQueueTool

<TestClass()> Public Class UnitTest_QueueDatabase
    Private SUT As QueueDatabase
    Private mConfig As Config
    Private mCfgPath As String = "C:\Users\[username]\Documents\GitHub\WNSQueueTool\Dummy Configs\dummyCfg2.xml"

    Public Sub New()
        mCfgPath = mCfgPath.Replace("[username]", Environment.UserName)
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

    <TestMethod()> Public Sub QueueDatabase_Disconnect()
        SUT.Disconnect()

        Dim dt As DataTable
        Try
            dt = SUT.ExecuteQuery(mConfig.Queries(0))
            Assert.Fail("connection not disconnected")
        Catch e As Exception
            Assert.AreEqual(e.Message, "Fill: SelectCommand.Connection property has not been initialized.")
        End Try

    End Sub

    <TestMethod()> Public Sub ExecuteNonQuery_InsertUpdateDelete()
        Dim rowsAffected As Long
        SUT.Connect()

        'insert
        rowsAffected = SUT.ExecuteNonQuery(mConfig.Queries(1))
        Assert.IsTrue(rowsAffected = 1)

        'update
        rowsAffected = SUT.ExecuteNonQuery(mConfig.Queries(2))
        Assert.IsTrue(rowsAffected = 1)

        'delete
        rowsAffected = SUT.ExecuteNonQuery(mConfig.Queries(3))
        Assert.IsTrue(rowsAffected = 1)

    End Sub

    Protected Overrides Sub Finalize()
        mConfig = Nothing
        SUT = Nothing
        MyBase.Finalize()
    End Sub
End Class