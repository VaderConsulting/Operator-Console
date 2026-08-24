Imports System.IO
Imports System.ServiceModel.Web

Public Class Service
    Implements IService

    Public Function EchoWithGet(ByVal s As String) As String Implements IService.EchoWithGet
        Debug.Print("EchoWithGet called.  Passed string was " & s)
        Return "EchoWithGet called.  Passed string was " & s
    End Function

    Public Function EchoWithPost(ByVal s As String) As String Implements IService.EchoWithPost
        Debug.Print("EchoWithPost called.  Passed string was " & s)
        Return "EchoWithPost called.  Passed string was " & s
    End Function

    Public Function GetImage(ByVal Name As String) As MemoryStream Implements IService.GetImage
        Dim LocalPath As String = "C:\temp\Test_Applications\Images\"
        Dim Filename As String = LocalPath & Name

        Dim ImageFile As New IO.FileStream(Filename, FileMode.Open)
        Dim ReturnStream As New MemoryStream

        ReturnStream.SetLength(ImageFile.Length)
        ImageFile.Read(ReturnStream.GetBuffer, 0, ImageFile.Length)
        ImageFile.Close()
        ReturnStream.Flush()

        WebOperationContext.Current.OutgoingResponse.ContentType = "raw"

        Return ReturnStream

    End Function

End Class