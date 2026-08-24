Imports System
Imports System.IO
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.ServiceModel.Description

<ServiceContract()> _
Public Interface IService
    <OperationContract()> _
    <WebGet()> _
    Function EchoWithGet(ByVal s As String) As String

    <OperationContract()> _
    <WebInvoke()> _
    Function EchoWithPost(ByVal s As String) As String

    <OperationContract()> _
    <WebGet()> _
    Function GetImage(ByVal Name As String) As MemoryStream

End Interface