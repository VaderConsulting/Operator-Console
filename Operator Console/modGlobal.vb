Imports Utility

Module modGlobal

    Public gFunctions As New Functions
    Public gSettings As New Collections.Specialized.OrderedDictionary
    Public gDecryptedEncryptionKey As String
    Public gPrimaryServer As String
    Public gSecondaryServer As String
    Public gTertiaryServer As String
    Public gCurrentServer As String
    'Public CurrentApplicationDomain As AppDomain = AppDomain.CurrentDomain
    Public gAppDataPath As String = ""

    Public Sub UnhandledErrorHandler(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        Dim Ex As Exception

        Ex = e.ExceptionObject
        gFunctions.FileLoggingObject.WriteErrorEvent("Unhandled exception:")
        gFunctions.FileLoggingObject.WriteErrorEvent(Ex.StackTrace)

        End
    End Sub

    Public Sub UnhandledFormErrorHandler(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        gFunctions.FileLoggingObject.WriteErrorEvent("Unhandled Form exception:")
        gFunctions.FileLoggingObject.WriteErrorEvent(e.Exception.StackTrace)
    End Sub

End Module
