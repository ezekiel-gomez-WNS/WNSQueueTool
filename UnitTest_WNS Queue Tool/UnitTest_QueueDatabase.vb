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
    <TestMethod()> Public Sub ParallelUpload_StatusCode_DtTable()
        Dim StatusCode As String
        Dim StatusDesc As String
        Dim UseThisStr As String
        Dim rowsAffected As Long

        StatusCode = InputBox("Status Code", "Insert Transaction  Status code that will be added in DB.")
        StatusDesc = InputBox("Status Description", "Insert the small description.")
        SUT1.Connect()
        'insert
        UseThisStr = mConfig1.Queries(9).Replace("[StatusCode]", StatusCode)
        UseThisStr = UseThisStr.Replace("[StatusDesc]", StatusDesc)
        rowsAffected = SUT1.ExecuteNonQuery(UseThisStr)
        Assert.IsTrue(rowsAffected = 1)

        'update Status Code
        UseThisStr = mConfig1.Queries(10).Replace("[StatusCode]", StatusCode)
        rowsAffected = SUT1.ExecuteNonQuery(UseThisStr.Replace("[Replacer]", "K"))
        Assert.IsTrue(rowsAffected = 1)

        'update Status Desc
        UseThisStr = mConfig1.Queries(11).Replace("[StatusDesc]", StatusDesc)
        rowsAffected = SUT1.ExecuteNonQuery(UseThisStr.Replace("[Replacer]", "test"))
        Assert.IsTrue(rowsAffected = 1)

        'delete
        UseThisStr = mConfig1.Queries(12).Replace("[StatusCode]", "K")
        rowsAffected = SUT1.ExecuteNonQuery(UseThisStr)
        Assert.IsTrue(rowsAffected = 1)
        SUT1.Disconnect()
    End Sub
    <TestMethod()> Public Sub ParallelUpload_AssignQueue()
        Dim rowsAffected As Long
        Dim AssignToUID As String
        Dim StartStatusCode As String
        Dim EndStatusCode As String
        Dim Remarks As String
        Dim UseThisStr As String
        SUT1.Connect()
        AssignToUID = InputBox("assign to", "uid")
        StartStatusCode = InputBox("Status", "StatusCode")
        EndStatusCode = InputBox("EndCode", "End Code")
        Remarks = InputBox("Remarks", "Remarks")

        'insert
        UseThisStr = mConfig1.Queries(13)
        If (StartStatusCode = "") Then
            MsgBox("Start Code needs to be non-null")
            Exit Sub
        Else
            UseThisStr = UseThisStr.Replace("[StartStatusCode]", StartStatusCode)
        End If
        If (EndStatusCode = "") Then
            UseThisStr = UseThisStr.Replace(",EndStatusCode", "")
            UseThisStr = UseThisStr.Replace(",'[EndStatusCode]'", "")
        Else
            UseThisStr = UseThisStr.Replace("[EndStatusCode]", EndStatusCode)
        End If
        If (AssignToUID = "") Then
            MsgBox("Assign ID cannot be null")
            Exit Sub
        Else
            UseThisStr = UseThisStr.Replace("[AssignToUID]", AssignToUID)
        End If
        If (Remarks = "") Then
            UseThisStr = UseThisStr.Replace(",Remarks", "")
            UseThisStr = UseThisStr.Replace(",'[Remarks]'", "")
        Else
            UseThisStr = UseThisStr.Replace("[Remarks]", Remarks)
        End If
        UseThisStr = UseThisStr.Replace("[ComputerName]", Environment.MachineName)
        UseThisStr = UseThisStr.Replace("[AssignByUID]", Environment.UserName)
        rowsAffected = SUT1.ExecuteNonQuery(UseThisStr)
        Assert.IsTrue(rowsAffected = 1)

        'update
        UseThisStr = mConfig1.Queries(14)
        If (StartStatusCode = "") Then
            MsgBox("Start Code needs to be non-null")
            Exit Sub
        Else
            UseThisStr = UseThisStr.Replace("[StartStatusCode]", StartStatusCode)
        End If
        If (EndStatusCode = "") Then
            UseThisStr = UseThisStr.Replace(",EndStatusCode", "")
            UseThisStr = UseThisStr.Replace(",'[EndStatusCode]'", "")
        Else
            UseThisStr = UseThisStr.Replace("[EndStatusCode]", EndStatusCode)
        End If
        If (AssignToUID = "") Then
            MsgBox("Assign ID cannot be null")
            Exit Sub
        Else
            UseThisStr = UseThisStr.Replace("[AssignToUID]", AssignToUID)
        End If
        If (Remarks = "") Then
            UseThisStr = UseThisStr.Replace(",Remarks", "")
            UseThisStr = UseThisStr.Replace(",'[Remarks]'", "")
        Else
            Remarks = "DeleteThis"
            UseThisStr = UseThisStr.Replace("[Remarks]", Remarks)
        End If

        rowsAffected = SUT1.ExecuteNonQuery(UseThisStr)
        Assert.IsTrue(rowsAffected = 1)

        'delete
        UseThisStr = mConfig1.Queries(15)
        rowsAffected = SUT1.ExecuteNonQuery(UseThisStr)
        Assert.IsTrue(rowsAffected > 1)
        SUT1.Disconnect()
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