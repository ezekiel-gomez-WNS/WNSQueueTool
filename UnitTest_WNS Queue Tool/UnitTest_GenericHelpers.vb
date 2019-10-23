Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WNSQueueTool

<TestClass()> Public Class UnitTest_GenericHelpers
    Dim SUT As GenericHelpers
    Dim jsonTxt = "{'Column1':'value1','Column2':'value2','Column3':'value3'}"

    <TestMethod()> Public Sub JSONtoDICT_ReturnsValue1()
        SUT = New GenericHelpers
        Dim dict = SUT.ConvertJSONtoDictionary(jsonTxt)
        Assert.AreEqual("value1", dict("Column1"))
        SUT = Nothing
    End Sub

    <TestMethod()> Public Sub JSONtoDICT_ReturnsValue2()
        SUT = New GenericHelpers
        Dim dict = SUT.ConvertJSONtoDictionary(jsonTxt)
        Assert.AreEqual("value2", dict("Column2"))
        SUT = Nothing
    End Sub

    <TestMethod()> Public Sub JSONtoDICT_ReturnsValue3()
        SUT = New GenericHelpers
        Dim dict = SUT.ConvertJSONtoDictionary(jsonTxt)
        Assert.AreEqual("value3", dict("Column3"))
        SUT = Nothing
    End Sub

    <TestMethod()> Public Sub JSONtoDICT_DuplicateKey()
        SUT = New GenericHelpers
        Dim jsonText = "{'Column1':'value1','Column1':'value2','Column3':'value3'}"
        Try
            Dim dict = SUT.ConvertJSONtoDictionary(jsonText)
        Catch e As Exception
            Assert.AreEqual(e.Message, "Duplicate Key Found.")
        End Try
        SUT = Nothing
    End Sub

    <TestMethod()> Public Sub JSONtoDICT_IntValue()
        SUT = New GenericHelpers
        Dim jsonText = "{'Column1':'1','Column2':'2','Column3':'value3'}"
        Dim dict = SUT.ConvertJSONtoDictionary(jsonText)
        Assert.AreEqual(1, CInt(dict("Column1")))
        SUT = Nothing
    End Sub

    <TestMethod()> Public Sub JSONtoDICT_BlankKey()
        SUT = New GenericHelpers
        Dim jsonText = "{'Column1':'1','Column2':'2','':'','Column3':'value3'}"
        Dim dict = SUT.ConvertJSONtoDictionary(jsonText)
        Assert.AreEqual(1, CInt(dict("Column1")))
        SUT = Nothing
    End Sub

    <TestMethod()> Public Sub JSONtoDICT_ValueHasQuote()
        SUT = New GenericHelpers
        Dim jsonText = "{'Column1':'value1','Column2':'value'2','Column3':'value3'}"
        Dim dict = SUT.ConvertJSONtoDictionary(jsonText)
        Assert.AreEqual("value'2", dict("Column2"))
        SUT = Nothing
    End Sub

End Class