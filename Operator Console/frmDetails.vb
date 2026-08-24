Imports System.DirectoryServices
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmDetails

    Private m_User As New DirectoryUser
    Private m_Username As String
    Private m_UserPath As String

    Public Property Username() As String
        Get
            Return m_Username
        End Get
        Set(ByVal value As String)
            m_Username = value
        End Set
    End Property

    Public Property UserPath() As String
        Get
            Return m_UserPath
        End Get
        Set(ByVal value As String)
            m_UserPath = value
        End Set
    End Property

    Private Sub frmDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PlaceDetailElements()
        GetUserDetails()
        FillDetailElements()
    End Sub

    Private Sub GetUserDetails()
        DirectoryConnection.RefreshCache()

        If DirectoryConnection.Properties("sn").Count > 0 Then m_User.Surname = DirectoryConnection.Properties("sn")(0).ToString
        If DirectoryConnection.Properties("givenName").Count > 0 Then m_User.GivenName = DirectoryConnection.Properties("givenName")(0).ToString
        If DirectoryConnection.Properties("department").Count > 0 Then m_User.Department = DirectoryConnection.Properties("department")(0).ToString
        If DirectoryConnection.Properties("description").Count > 0 Then m_User.Description = DirectoryConnection.Properties("description")(0).ToString
        If DirectoryConnection.Properties("telephoneNumber").Count > 0 Then m_User.TelephoneNumber = DirectoryConnection.Properties("telephoneNumber")(0).ToString
        If DirectoryConnection.Properties("mobile").Count > 0 Then m_User.MobileTelephoneNumber = DirectoryConnection.Properties("mobile")(0).ToString
        If DirectoryConnection.Properties("name").Count > 0 Then m_User.DisplayName = DirectoryConnection.Properties("name")(0).ToString
        If DirectoryConnection.Properties("description").Count > 0 Then m_User.Description = DirectoryConnection.Properties("description")(0).ToString
        If DirectoryConnection.Properties("department").Count > 0 Then m_User.Department = DirectoryConnection.Properties("department")(0).ToString

        ' TODO:  Get the other AD properties

        Me.Text = "Person Details - " & m_Username

        BuildTreeview(m_UserPath)
    End Sub

    Private Sub FillDetailElements()
        Dim de As New DirectoryEntry(m_UserPath, TargetDirectoryConnectionUsername, TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        de.RefreshCache()

        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

                If de.Properties(t.Tag).Count > 0 Then
                    t.Text = de.Properties(t.Tag)(0).ToString
                End If
            End If
        Next
        de = Nothing
    End Sub

    Private Sub BuildTreeview(ByVal UserPath As String)
        Dim MyUser As New DirectoryEntry(UserPath, TargetDirectoryConnectionUsername, TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim Path As String()
        Dim CommonPath As String = TargetDirectoryUserRootDN.Substring(TargetDirectoryUserRootDN.IndexOf("=") - 2)

        UserPath = Replace(UserPath.ToUpper, CommonPath.ToUpper, "")

        If UserPath.EndsWith(",") Then UserPath = UserPath.Substring(0, Len(UserPath) - 1)

        Path = Split(UserPath, ",")

        For i As Integer = UBound(Path) To 0 Step -1
            Path(i) = Path(i).Remove(0, 3)
            Console.WriteLine(Path(i))
        Next

        ' remove everything before the 'CN='
        Path(0) = Path(0).Remove(0, Path(0).IndexOf("CN") + 3)

        ' Finally build the tree structure
        Dim ParentNode As TreeNode = tvwDirectory.Nodes.Add(Path(UBound(Path)))
        For i As Integer = UBound(Path) - 1 To 1 Step -1
            Dim ChildNode As New TreeNode
            ParentNode = AddNode(ParentNode, Path(i))
        Next
        tvwDirectory.ExpandAll()
    End Sub

    Private Function AddNode(ByRef Parent As TreeNode, ByVal Text As String) As TreeNode
        Dim ReturnNode As TreeNode = Nothing

        ReturnNode = Parent.Nodes.Add(Text)

        Return ReturnNode
    End Function

    ''' <summary>
    ''' Set the readonly property of all textboxes to false
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        AllowEdits()
    End Sub

    Private Sub AllowEdits()
        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c
                Select Case t.Tag.ToString.ToLower
                    Case "distinguishedname", "name" ' We do not allow edits to these attributes
                    Case Else
                        t.ReadOnly = False
                End Select

            End If
        Next

        btnEdit.Enabled = False
        btnSave.Enabled = True
        lblStatus.Text = "Edit"
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim de As New DirectoryEntry(m_UserPath, TargetDirectoryConnectionUsername, TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        ' TODO:  Security

        de.RefreshCache()

        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c
                Console.WriteLine(t.Name & "(" & t.Tag & ") = " & t.Text)
                Select Case t.Tag.ToString.ToLower
                    Case "name", "distinguishedName" ' Don't modify these values
                    Case Else
                        If t.Text.Trim.Length > 0 Then
                            de.Properties(t.Tag).Value = t.Text
                        Else
                            de.Properties(t.Tag).Clear()
                        End If
                End Select

            End If
        Next

        de.CommitChanges()
        lblStatus.Text = "Changes have been saved"

    End Sub

    ''' <summary>
    ''' Place each form control according to the detail found in the configuration file
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PlaceDetailElements()
        Dim LayoutSettings As New Collections.Specialized.StringCollection()
        Dim Elements() As String

        LayoutSettings = My.Settings.DetailFields

        For Each Setting As String In LayoutSettings
            Elements = Split(Setting, ",")
            Select Case Elements(0).ToLower
                Case "label"
                    ' 0 = Type
                    ' 1 = Name
                    ' 2 = Text
                    ' 3 = x position
                    ' 4 = y position
                    Dim NewLabel As New Label
                    NewLabel.Name = Elements(1)
                    NewLabel.Text = Elements(2)
                    NewLabel.Left = Elements(3)
                    NewLabel.Top = Elements(4)
                    NewLabel.AutoSize = True
                    Me.pnlDetails.Controls.Add(NewLabel)
                Case "textbox"
                    ' 0 = Type
                    ' 1 = Name
                    ' 2 = value
                    ' 3 = x position
                    ' 4 = y position
                    ' 5 = width
                    ' 6 = height
                    Dim NewTextbox As New TextBox
                    NewTextbox.Name = Elements(1)
                    NewTextbox.Tag = Elements(2)
                    NewTextbox.Left = Elements(3)
                    NewTextbox.Top = Elements(4)
                    NewTextbox.Width = Elements(5)
                    NewTextbox.Height = Elements(6)
                    NewTextbox.ReadOnly = True
                    Me.pnlDetails.Controls.Add(NewTextbox)
            End Select
        Next


    End Sub

    Private Sub frmDetails_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case Chr(e.KeyCode)
            Case Convert.ToChar(113) ' F2
                AllowEdits()
            Case Convert.ToChar(27) ' Esc
                Me.Close()
        End Select
    End Sub

End Class