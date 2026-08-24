Imports System
Imports System.IO
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.ServiceModel.Description

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetImage()
        'ConnectToWebService()
        
    End Sub

    Private Sub ConnectToWebService()
        ' This is used to connect to the ConfigServer

        ' ---------------------------------------------
        ' Define the Server name and Port
        Dim ServerName As String = "localhost"
        Dim Port As String = "8000"

        Dim ServerAddress As String

        If Port <> "" Then
            ServerAddress = ServerName & ":" & Port
        Else
            ServerAddress = ServerName
        End If
        ' ---------------------------------------------

        Try
            Using cf As New ChannelFactory(Of IService)(New WebHttpBinding(), "http://" & ServerAddress)
                cf.Endpoint.Behaviors.Add(New WebHttpBehavior())

                Dim channel As IService = cf.CreateChannel()

                Dim s As String

                Console.WriteLine("Calling EchoWithGet via HTTP GET: ")
                s = channel.EchoWithGet("Hello, world")
                Console.WriteLine("   Output: {0}", s)

                Console.WriteLine("")
                Console.WriteLine("This can also be accomplished by navigating to")
                Console.WriteLine("http://" & ServerAddress & "/EchoWithGet?s=Hello, world!")
                Console.WriteLine("in a web browser while this sample is running.")

                Console.WriteLine("")

                Console.WriteLine("Calling EchoWithPost via HTTP POST: ")
                s = channel.EchoWithPost("Hello, world")
                Console.WriteLine("   Output: {0}", s)

            End Using
        Catch cex As CommunicationException
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetImage()
        ' ---------------------------------------------
        ' Define the Server name and Port
        Dim ServerName As String = "localhost"
        Dim Port As String = "8000"

        Dim ServerAddress As String

        If Port <> "" Then
            ServerAddress = ServerName & ":" & Port
        Else
            ServerAddress = ServerName
        End If
        ' ---------------------------------------------

        Try
            Dim Binding As New WebHttpBinding

            Dim MaxMessageSize As Long = (1024 * 1024) * 5 'Mb

            Binding.MaxBufferSize = MaxMessageSize
            Binding.MaxReceivedMessageSize = MaxMessageSize

            Using cf As New ChannelFactory(Of IService)(Binding, "http://" & ServerAddress)
                cf.Endpoint.Behaviors.Add(New WebHttpBehavior())

                Dim channel As IService = cf.CreateChannel()

                Dim ImageFile As MemoryStream = channel.GetImage("babcock-Mountain-View-2.jpg")

                ImageFile.Seek(0, SeekOrigin.Begin)

                File.WriteAllBytes("c:\temp\babcock-Mountain-View-2.jpg", ImageFile.ToArray)
            End Using
        Catch cex As CommunicationException
        Catch ex As Exception
        End Try
    End Sub

End Class
