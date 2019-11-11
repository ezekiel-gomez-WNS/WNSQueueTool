Imports System.Data.SqlClient
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WNSQueueTool

<TestClass()> Public Class UnitTest_QueueDatabase
    Private SUT As QueueDatabase
    Private SUT1 As QueueDatabase
    Private mConfig As Config
    Private mCfgPath As String = "C:\Users\[username]\Documents\GitHub\WNSQueueTool\Dummy Configs\dummyCfg2.xml"
    Private mConfig1 As Config
    Private mCfgPath1 As String = "C:\Users\[username]\Documents\GitHub\WNSQueueTool\Dummy Configs\dummyCfg3.xml"
    Public TestTime As String = DateAndTime.Now

    Public Sub New()
        mCfgPath = mCfgPath.Replace("[username]", Environment.UserName)
        mCfgPath1 = mCfgPath1.Replace("[username]", Environment.UserName)
        mConfig = New Config(mCfgPath)
        mConfig1 = New Config(mCfgPath1)
        SUT = New QueueDatabase(mConfig.ConnectionString, mConfig.DeadLockRetryCount)
        SUT1 = New QueueDatabase(mConfig1.ConnectionString, mConfig1.DeadLockRetryCount)
    End Sub
    <TestMethod()> Public Sub ParallelUpload_PopulateQueue_DtTable()
        Dim rowsAffected As Long
        SUT1.Connect()
        'insert
        rowsAffected = SUT1.ExecuteNonQuery(mConfig1.Queries(1).Replace("[UserName]", "'u321936'"))
        Assert.IsTrue(rowsAffected = 1)

        'update
        rowsAffected = SUT1.ExecuteNonQuery(mConfig1.Queries(2).Replace("[UserName]", "'u321936'"))
        Assert.IsTrue(rowsAffected = 1)

        'delete
        rowsAffected = SUT1.ExecuteNonQuery(mConfig1.Queries(3).Replace("[UserName]", "'u321936'"))
        Assert.IsTrue(rowsAffected = 1)
        SUT1.Disconnect()

    End Sub
    <TestMethod()> Public Sub ParallelUpload_PopulateQueueUpload_DtTable()
        Dim rowsAffected As Long
        SUT1.Connect()
        'insert
        rowsAffected = SUT1.ExecuteNonQuery(mConfig1.Queries(5).Replace("[UserName]", "'u321936'"))
        Assert.IsTrue(rowsAffected = 1)

        'update
        rowsAffected = SUT1.ExecuteNonQuery(mConfig1.Queries(6).Replace("[UserName]", "'u321936'"))
        Assert.IsTrue(rowsAffected = 1)

        'delete
        rowsAffected = SUT1.ExecuteNonQuery(mConfig1.Queries(7))
        Assert.IsTrue(rowsAffected = 1)
        SUT1.Disconnect()

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