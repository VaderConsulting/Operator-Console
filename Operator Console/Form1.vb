Imports System
Imports System.Data
Imports System.Configuration
Imports System.Xml
Imports System.Xml.XPath

Public Class Form1

    Private Const SUCCESS As Int32 = 0

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Reference: http://msdn.microsoft.com/en-us/library/ms897209.aspx</remarks>
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        'Dim Filename As String = "C:\Users\daver.STRATATEL-PERTH\AppData\Local\Temporary Projects\WindowsApplication1\External.config"
        'Dim oConfig As New ExternalConfig(Filename)
        'Dim ReturnResult As Int32 = 0
        'Dim ReturnString As String = ""
        'Dim DefaultString As String = ""
        'Dim ReturnInteger As Integer = 0
        'Dim DefaultInteger As Integer = 0

        '' ====================================================================================================
        'ReturnResult = oConfig.ReadString("Setting1", ReturnString, DefaultString)
        'If ReturnResult = SUCCESS Then
        '    Debug.Print(ReturnString)
        'Else
        '    Debug.Print("Error " & ReturnResult.ToString & " returning configuration value for Setting1")
        'End If
        '' ====================================================================================================


        '' ====================================================================================================
        'ReturnResult = oConfig.ReadString("Setting2", ReturnString, DefaultString)
        'If ReturnResult = SUCCESS Then
        '    Debug.Print(ReturnString)
        'Else
        '    Debug.Print("Error " & ReturnResult.ToString & " returning configuration value for Setting2")
        'End If
        '' ====================================================================================================


        '' ====================================================================================================
        'ReturnResult = oConfig.ReadInteger("Setting3", ReturnInteger, DefaultInteger)
        'If ReturnResult = SUCCESS Then
        '    Debug.Print(ReturnInteger)
        'Else
        '    Debug.Print("Error " & ReturnResult.ToString & " returning configuration value for Setting3")
        'End If
        '' ====================================================================================================

        '' ====================================================================================================
        'ReturnResult = oConfig.WriteString("Setting3", "NewValue")
        'If ReturnResult = SUCCESS Then
        '    Debug.Print("Successfully wrote Setting3")
        'Else
        '    Debug.Print("Error " & ReturnResult.ToString & " writing configuration value for Setting3")
        'End If
        '' ====================================================================================================


        
    End Sub

End Class



