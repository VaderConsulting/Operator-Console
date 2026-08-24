Imports System
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.ServiceModel.Description

Public Class Form1

    Private Host As WebServiceHost '= New WebServiceHost(GetType(Service), New Uri("http://localhost:8000/"))

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Host.Close()
        Debug.Print("Service is closed.")
        Host = Nothing
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Port As String = "8000"
        Dim ServerAddress As String

        ' ---------------------------------------------
        ' Define the Server name and Port
        If Port <> "" Then
            ServerAddress = "localhost:" & Port
        Else
            ServerAddress = "localhost"
        End If
        ' ---------------------------------------------

        Host = New WebServiceHost(GetType(Service), New Uri("http://" & ServerAddress & "/"))

        Dim Binding As New WebHttpBinding

        Dim MaxMessageSize As Long = (1024 * 1024) * 5 'Mb

        Binding.MaxBufferSize = MaxMessageSize
        Binding.MaxReceivedMessageSize = MaxMessageSize

        Dim Endpoint As ServiceEndpoint = Host.AddServiceEndpoint(GetType(IService), Binding, "")

        ' Turn off the HTTP Help page:
        Dim sdb As ServiceDebugBehavior = Host.Description.Behaviors.Find(Of ServiceDebugBehavior)()
        sdb.HttpHelpPageEnabled = False

        Host.Open()
        Debug.Print("Service is running...")
    End Sub

End Class