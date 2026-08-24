Imports System.DirectoryServices
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmNewPerson

    Private m_OU() As String = {}
    Private m_User As New DirectoryUser
    Private m_Username As String
    Private m_UserPath As String
    Private m_ChangesMade As Boolean = False
    Private m_LoadComplete As Boolean = False

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

    Private Sub frmNewPerson_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Dimensions As String = gFunctions.CollectionValue("NewUserFormSize", gSettings)

        Dim Size() As String
        Size = Split(Dimensions, "|")

        Me.Width = Size(0)
        Me.Height = Size(1)

        PlaceDetailElements()
        m_LoadComplete = True
        txtMandatory.BackColor = System.Drawing.ColorTranslator.FromHtml(gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings)))
        tvwDirectory.BackColor = System.Drawing.ColorTranslator.FromHtml(gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings)))

        Me.CenterToParent()
    End Sub

    Private Sub FillDetailElements()
        Dim de As New DirectoryEntry(m_UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

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
        'Dim MyUser As New DirectoryEntry(UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        'Dim Path As String()
        'Dim CommonPath As String = gFunctions.TargetDirectoryUserRootDN.Substring(gFunctions.TargetDirectoryUserRootDN.IndexOf("=") - 2)

        'UserPath = Replace(UserPath.ToUpper, CommonPath.ToUpper, "")

        'If UserPath.EndsWith(",") Then UserPath = UserPath.Substring(0, Len(UserPath) - 1)

        'Path = Split(UserPath, ",")

        'For i As Integer = UBound(Path) To 0 Step -1
        '    Path(i) = Path(i).Remove(0, 3)
        'Next

        '' remove everything before the 'CN='
        'Path(0) = Path(0).Remove(0, Path(0).IndexOf("CN") + 3)

        '' Finally build the tree structure
        'Dim ParentNode As TreeNode = tvwDirectory.Nodes.Add(Path(UBound(Path)))
        'For i As Integer = UBound(Path) - 1 To 1 Step -1
        '    Dim ChildNode As New TreeNode
        '    ParentNode = AddNode(ParentNode, Path(i))
        'Next
        'tvwDirectory.ExpandAll()


        ' This is different to the UserDetails form because in this instance, 
        ' the User doesn't actually exist.
        ' So instead of the User we have to start with the OU the user sits in.



        ' ============================================================================
        ' ============================================================================
        'UserPath = UserPath.Replace("CN=NothingInParticular,", "")
        'Dim MyUser As New DirectoryEntry(UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        'Dim Path As String()
        'Dim CommonPath As String = gFunctions.TargetDirectoryUserRootDN.Substring(gFunctions.TargetDirectoryUserRootDN.IndexOf("=") - 2)
        'Dim CurrentParentPath As String = ""
        'Dim ParentOUPaths As New Collections.Specialized.NameValueCollection

        'If UserPath.EndsWith(",") Then UserPath = UserPath.Substring(0, Len(UserPath) - 1)

        'Path = Split(UserPath, ",")

        'Dim Parent As DirectoryEntry = MyUser '.Parent
        'Dim OULevelCount As Integer = 0
        ''Debug.Print("Parent: " & Parent.Path)

        'For i As Integer = 0 To Parent.Path.ToString.Length - 1
        '    If Parent.Path.Substring(i, 1) = "," Then OULevelCount += 1
        '    'Debug.Print(Parent.Path.Substring(0, i + 1) & " OULevels: " & OULevelCount)
        'Next

        ''Debug.Print("User Root: " & gFunctions.TargetDirectoryUserRootDN)
        '' OULevelCount now contains the number of hierarchical levels deep the User is.

        '' Retrieve the path of each OU above the User, until we get to the user Root
        'Dim Index As Integer
        'Do Until Parent.Path.ToLower = gFunctions.TargetDirectoryUserRootDN.ToLower
        '    'Debug.Print("Parent: " & Parent.Path)
        '    ParentOUPaths.Add(Index.ToString, Parent.Path)
        '    Parent = Parent.Parent
        '    Index += 1
        'Loop

        '' Now to reorder the Parent Paths
        'Dim OULevelCountBelowUserRoot = Index
        'Dim ParentOUPath(ParentOUPaths.Count - 1) As String
        'For i As Integer = 0 To ParentOUPaths.Count - 1
        '    ParentOUPath(i) = ParentOUPaths((OULevelCountBelowUserRoot - i - 1))
        'Next

        '' Now what we have is an array of OU paths for each OU above the user.
        '' The Description field of each OU may contain a code.

        '' Retrieve that code:
        'Dim OUCode(ParentOUPaths.Count - 1) As String
        'For i As Integer = 0 To ParentOUPaths.Count - 1
        '    Dim OU As New DirectoryEntry(ParentOUPath(i), gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        '    Try
        '        ' Is it MultiValued?
        '        OUCode(i) = OU.Properties("Description")(0).ToString & ""
        '    Catch ex As Exception
        '        ' No description (code)
        '        OUCode(i) = "No Code"
        '    End Try
        '    OU.Close()
        'Next

        'For i As Integer = UBound(Path) To 0 Step -1
        '    Path(i) = Path(i).Remove(0, 3)
        '    'Debug.Print(Path(i))
        '    'Debug.Print(CurrentParentPath)
        'Next

        '' remove everything before the 'CN='
        'Path(0) = Path(0).Remove(0, Path(0).IndexOf("CN") + 3)

        '' Finally build the tree structure
        'Dim ParentNode As TreeNode
        '' Add the root node

        'ParentNode = tvwDirectory.Nodes.Add(Path(UBound(Path)) & " (" & OUCode(0) & ")")

        '' Now add each child node
        ''For i As Integer = UBound(Path) - 1 To 1 Step -1
        'For i As Integer = OUCode.Length - 1 To 1 Step -1
        '    Dim ChildNode As New TreeNode

        '    ParentNode = AddNode(ParentNode, Path(i) & " (" & OUCode(OUCode.Length - i) & ")")
        'Next

        'MyUser.Close()
        'MyUser = Nothing

        'tvwDirectory.ExpandAll()


        Dim CommonPath As String = ""

        CommonPath = gFunctions.TargetDirectoryUserRootDN.Substring(gFunctions.TargetDirectoryUserRootDN.IndexOf("=") - 2)
        UserPath = UserPath.Replace("CN=NothingInParticular,", "")
        Dim MyObject As New DirectoryEntry(UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim Path As String()
        'Dim CommonPath As String = gFunctions.TargetDirectoryUserRootDN.Substring(gFunctions.TargetDirectoryUserRootDN.IndexOf("=") - 2)
        Dim CurrentParentPath As String = ""
        'Dim TempParentOUPath As String()
        Dim ParentOUPaths As New Collections.Specialized.NameValueCollection
        'Dim ParentOUPath As String()

        UserPath = Replace(UserPath.ToUpper, CommonPath.ToUpper, "")

        If UserPath.EndsWith(",") Then UserPath = UserPath.Substring(0, Len(UserPath) - 1)

        ' Example:  "LDAP://WKS01:389/CN=ADAIR ELIZABETH,OU=ENGADINE 1050,OU=ST GEORGE  SHIRE LOCAL MARKET,OU=SHARED SERVICES RETAIL & RURAL BANKING,OU=PERSONAL DIVISION"

        Path = Split(UserPath, ",")
        For i As Integer = 0 To UBound(Path)
            Path(i) = Path(i).Remove(0, Path(i).IndexOf("=") + 1)
        Next

        Dim Parent As DirectoryEntry = MyObject.Parent
        Dim OULevelCount As Integer = Path.Length
        'Debug.Print("Parent: " & Parent.Path)

        'For i As Integer = 0 To Parent.Path.ToString.Length - 1
        'If Parent.Path.Substring(i, 1) = "," Then OULevelCount += 1
        'Debug.Print(Parent.Path.Substring(0, i + 1) & " OULevels: " & OULevelCount)
        'Next

        'Debug.Print("User Root: " & gFunctions.TargetDirectoryUserRootDN)
        ' OULevelCount now contains the number of hierarchical levels deep the User is.

        ' Retrieve the path of each OU above the User, until we get to the user Root
        Dim Index As Integer = 0
        Dim ComparisonPath As String = ""

        'If UseUserRoot Then
        ComparisonPath = gFunctions.TargetDirectoryUserRootDN
        'Else
        'ComparisonPath = gFunctions.TargetDirectoryPhoneRootDN
        'End If
        ParentOUPaths.Add(Index.ToString, MyObject.Path)
        Index += 1
        Do
            ParentOUPaths.Add(Index.ToString, Parent.Path)
            Parent = Parent.Parent
            Index += 1
        Loop Until Parent.Path.ToLower = ComparisonPath.ToLower 'gFunctions.TargetDirectoryUserRootDN.ToLower

        Dim ParentOUPath(ParentOUPaths.Count - 1) As String
        For i As Integer = 0 To ParentOUPaths.Count - 1
            ParentOUPath(i) = ParentOUPaths(ParentOUPaths.Count - i - 1)
        Next

        ' Now what we have is an array of OU paths for each OU above the user.
        ' The Description field of each OU may contain a code.

        ' Retrieve that code:
        Dim OUCode(ParentOUPaths.Count - 1) As String
        For i As Integer = 0 To ParentOUPaths.Count - 1
            Dim OU As New DirectoryEntry(ParentOUPath(i), gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

            Try
                ' Is it MultiValued?
                OUCode(i) = OU.Properties("Description")(0).ToString & ""
            Catch ex As Exception
                ' No description (code)
                OUCode(i) = "No Code"
            End Try
            OU.Close()
        Next

        ' Finally build the tree structure
        Dim ParentNode As TreeNode
        ' Add the root node

        ParentNode = tvwDirectory.Nodes.Add(Path(UBound(Path)) & " (" & OUCode(0) & ")")

        ' Now add each child node
        For i As Integer = UBound(Path) - 1 To 0 Step -1
            Dim ChildNode As New TreeNode

            ParentNode = AddNode(ParentNode, Path(i) & " (" & OUCode(UBound(Path) - i) & ")")
        Next

        MyObject.Close()
        MyObject = Nothing

        tvwDirectory.ExpandAll()

    End Sub

    Private Function AddNode(ByRef Parent As TreeNode, ByVal Text As String) As TreeNode
        Dim ReturnNode As TreeNode = Nothing

        ReturnNode = Parent.Nodes.Add(Text)

        Return ReturnNode
    End Function

    'Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    Dim de As New DirectoryEntry(m_UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

    '    ' TODO:  Security

    '    de.RefreshCache()

    '    For Each c As Control In Me.pnlDetails.Controls
    '        If TypeOf c Is TextBox Then
    '            Dim t As TextBox = c
    '            Console.WriteLine(t.Name & "(" & t.Tag & ") = " & t.Text)
    '            Select Case t.Tag.ToString.ToLower
    '                Case "name", "distinguishedName" ' Don't modify these values
    '                Case Else
    '                    If t.Text.Trim.Length > 0 Then
    '                        de.Properties(t.Tag).Value = t.Text
    '                    Else
    '                        de.Properties(t.Tag).Clear()
    '                    End If
    '            End Select

    '        End If
    '    Next

    '    de.CommitChanges()
    '    lblStatus.Text = "Changes have been saved"

    'End Sub

    Private Sub Lookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim TheButton As Button = sender

        Dim SourceAttributeName As String = Replace(TheButton.Name.Replace("btn", ""), "lookup", "", Compare:=CompareMethod.Text)
        Dim SourceAttributeValue As String = Me.pnlDetails.Controls.Item("txt" & SourceAttributeName).Text
        Dim CurrentValue As String = Me.pnlDetails.Controls.Item("txt" & SourceAttributeName).Text
        Dim ListName As String = Me.pnlDetails.Controls.Item("txt" & SourceAttributeName & "Lookup").Tag.ToString.Substring(Me.pnlDetails.Controls.Item("txt" & SourceAttributeName & "Lookup").Tag.ToString.IndexOf("|") + 1)
        Dim AssociatedControl As TextBox = Me.pnlDetails.Controls.Item("txt" & SourceAttributeName)
        Dim LookupSelectForm As New frmLookupSelect

        LookupSelectForm.ListName = ListName
        LookupSelectForm.LookupKey = CurrentValue

        Dim LookupResult As DialogResult = LookupSelectForm.ShowDialog()

        If LookupResult = Windows.Forms.DialogResult.OK Then
            Dim ReturnedListItem As frmLookupSelect.ListItem = LookupSelectForm.LookupValue

            Me.pnlDetails.Controls.Item("txt" & SourceAttributeName).Text = ReturnedListItem.Key
            Me.pnlDetails.Controls.Item("txt" & SourceAttributeName & "Lookup").Text = ReturnedListItem.Value
            Debug.Print(AssociatedControl.Text)
        End If

        LookupSelectForm = Nothing

    End Sub

    Private Sub Text_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If m_LoadComplete Then m_ChangesMade = True
    End Sub

    ''' <summary>
    ''' Place each form control according to the detail found in the configuration file
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PlaceDetailElements()
        Dim LayoutSettings As New Collections.Specialized.OrderedDictionary()
        Dim Elements() As String
        Dim Settings As ICollection
        Dim Values As ICollection
        Dim Index As Integer

        LayoutSettings = gFunctions.MySection(gFunctions.InteractiveUserUsername, "ConsoleNewUserFields")

        Settings = LayoutSettings.Keys
        Values = LayoutSettings.Values

        For Each Setting As String In LayoutSettings.Values
            Elements = Split(Setting, ",")
            Select Case Elements(0).ToLower
                ' <line1>Line,0,0,50,50,Red,1</line1>
                'Case "line"
                '    ' Elements Index types are:
                '    ' 0 = Type
                '    ' 1 = x1 co-ordinate
                '    ' 2 = y1 co-ordinate
                '    ' 3 = x2 co-ordinate
                '    ' 4 = y2 co-ordinate
                '    ' 5 = colour
                '    ' 6 = thickness
                '    Dim Point1 As New Point
                '    Dim Point2 As New Point
                '    Point1.X = Elements(1)
                '    Point1.Y = Elements(2)
                '    Point2.X = Elements(3)
                '    Point2.Y = Elements(4)
                '    Dim Colour As String = Elements(5)
                '    Dim PenThickness As String = Elements(6)
                '    Dim DrawingPen As New Pen(System.Drawing.ColorTranslator.FromHtml(Colour), PenThickness)
                '    Dim g As System.Drawing.Graphics = Me.CreateGraphics
                '    g.DrawLine(DrawingPen, Point1, Point2)
                Case "label"
                    ' Elements Index types are:
                    ' 0 = Type
                    ' 1 = Text
                    ' 2 = x position
                    ' 3 = y position
                    Dim NewLabel As New Label
                    NewLabel.Name = Settings(Index)
                    NewLabel.Text = Elements(1)
                    NewLabel.Left = Elements(2)
                    NewLabel.Top = Elements(3)
                    NewLabel.AutoSize = True
                    NewLabel.TextAlign = ContentAlignment.MiddleCenter
                    Me.pnlDetails.Controls.Add(NewLabel)
                Case "textbox"
                    ' There are two types of Textbox:
                    ' 1. Normal
                    ' 2. Lookup
                    '
                    ' For the Normal type, the following is true:
                    ' 0 = Type
                    ' 1 = Attribute Name
                    ' 2 = Text (Used in CADS Configuration Utility)
                    ' 3 = Left position
                    ' 4 = Top position
                    ' 5 = width
                    ' 6 = TabIndex

                    ' For the lookup type, the following is true:
                    ' 0 = Type
                    ' 1 = Lookup Attribute Name
                    ' 2 = Text (Used in CADS Configuration Utility)
                    ' 3 = Lookup List name
                    ' 4 = Left position
                    ' 5 = Top position
                    ' 6 = width
                    ' 7 = TabIndex
                    If Settings(Index).ToString.ToLower.EndsWith("lookup") Then
                        Dim NewTextbox As New TextBox
                        NewTextbox.Name = Settings(Index)
                        NewTextbox.Tag = Elements(1)
                        ' Item 2 is the description, which is not used
                        NewTextbox.Tag &= "|" & Elements(3)
                        NewTextbox.Left = Elements(4)
                        NewTextbox.Top = Elements(5)
                        NewTextbox.Width = Elements(6)
                        NewTextbox.TabIndex = Elements(7)
                        NewTextbox.Height = 20
                        NewTextbox.ReadOnly = True
                        Me.pnlDetails.Controls.Add(NewTextbox)
                        ' Now add the associated Command button
                        Dim NewButton As New Button
                        NewButton.Name = "btn" & Elements(1) & "Lookup"
                        NewButton.Text = "..."
                        NewButton.Width = 30
                        NewButton.Left = NewTextbox.Left + NewTextbox.Width
                        NewButton.Top = NewTextbox.Top
                        NewButton.Height = NewTextbox.Height
                        NewButton.TabStop = False
                        'NewButton.Enabled = False
                        Me.pnlDetails.Controls.Add(NewButton)
                        AddHandler NewButton.Click, AddressOf Me.Lookup_Click
                    Else
                        If Settings(Index).ToString.ToLower.EndsWith("audit") Then
                            Dim NewTextbox As New TextBox
                            NewTextbox.Name = Settings(Index)
                            NewTextbox.Tag = Elements(1)
                            ' Item 2 is the description, which is not used
                            NewTextbox.Left = Elements(3)
                            NewTextbox.Top = Elements(4)
                            NewTextbox.Width = Elements(5)
                            NewTextbox.TabIndex = Elements(6)
                            NewTextbox.Height = 20
                            NewTextbox.ReadOnly = True
                            Me.pnlDetails.Controls.Add(NewTextbox)
                        End If
                        If Not Settings(Index).ToString.ToLower.EndsWith("audit") Then
                            Dim NewTextbox As New TextBox
                            NewTextbox.Name = Settings(Index)
                            NewTextbox.Tag = Elements(1)
                            ' Item 2 is the description, which is not used
                            NewTextbox.Left = Elements(3)
                            NewTextbox.Top = Elements(4)
                            NewTextbox.Width = Elements(5)
                            NewTextbox.TabIndex = Elements(6)
                            NewTextbox.Height = 20
                            Dim AttributeStatus As String = gFunctions.ValueFromSetting(gFunctions.LoadConfigSetting("UserAttributes", Elements(1)), 2)

                            If AttributeStatus.ToLower = "mandatory" Then
                                NewTextbox.BackColor = System.Drawing.ColorTranslator.FromHtml(gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings)))
                            End If

                            'NewTextbox.ReadOnly = True
                            Me.pnlDetails.Controls.Add(NewTextbox)

                            AddHandler NewTextbox.TextChanged, AddressOf Text_TextChanged
                        End If
                    End If
            End Select
            Index += 1
        Next
    End Sub

    'Private Sub PlaceDetailElements()
    '    Dim LayoutSettings As New Collections.Specialized.OrderedDictionary()
    '    Dim Elements() As String
    '    Dim Settings As ICollection
    '    Dim Values As ICollection
    '    Dim Index As Integer

    '    'LayoutSettings = gConfigServer.MySection(gFunctions.InteractiveUserUsername, gFunctions.CurrentADSitename, "ConsoleNewUserFields")

    '    Settings = LayoutSettings.Keys
    '    Values = LayoutSettings.Values

    '    For Each Setting As String In LayoutSettings.Values
    '        Elements = Split(Setting, ",")
    '        Select Case Elements(0).ToLower
    '            Case "label"
    '                ' 0 = Type
    '                ' 1 = Text
    '                ' 2 = x position
    '                ' 3 = y position
    '                Dim NewLabel As New Label
    '                NewLabel.Name = Settings(Index)
    '                NewLabel.Text = Elements(1)
    '                NewLabel.Left = Elements(2)
    '                NewLabel.Top = Elements(3)
    '                NewLabel.AutoSize = True
    '                Me.pnlDetails.Controls.Add(NewLabel)
    '            Case "textbox"
    '                ' 0 = Type
    '                ' 1 = Name
    '                ' 2 = value
    '                ' 3 = x position
    '                ' 4 = y position
    '                ' 5 = width
    '                Dim NewTextbox As New TextBox
    '                NewTextbox.Name = Settings(Index)
    '                NewTextbox.Tag = Elements(1)
    '                ' Item 2 is the description, which is not used
    '                NewTextbox.Left = Elements(3)
    '                NewTextbox.Top = Elements(4)
    '                NewTextbox.Width = Elements(5)
    '                NewTextbox.TabIndex = Elements(6)
    '                NewTextbox.Height = 20
    '                NewTextbox.ReadOnly = False
    '                Me.pnlDetails.Controls.Add(NewTextbox)
    '        End Select
    '        Index += 1
    '    Next
    'End Sub

    Private Sub btnPhones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhones.Click
        Dim PhonesForm As New frmPhones

        Dim User As DirectoryEntry = New DirectoryEntry(m_UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        ' m_UserPath
        PhonesForm.UserPath = m_UserPath

        Dim Result As DialogResult = PhonesForm.ShowDialog()

        If Result = Windows.Forms.DialogResult.OK Then

        End If

        User.Close()
        User = Nothing
    End Sub

    Private Sub DoSave()
        Dim ParentOU As New DirectoryEntry(m_UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim SaveNotValidAtThisTime As Boolean = False
        Dim UserSurname As String = ""
        Dim UserFirstname As String = ""

        ' What format do we use to create a user?  Found in config file
        Dim UserDisplayNameFormat As String = "[sn] [givenName]"
        Dim UserCNFormat As String = "[sn] [givenName]"
        Try
            UserDisplayNameFormat = gSettings("UserDisplayNameFormat").ToString & ""
            UserCNFormat = gSettings("UserCNFormat").ToString & ""
        Catch ex As Exception
            ' Exception means there is no format defined
        End Try

        If UserDisplayNameFormat = "" Then
            UserDisplayNameFormat = "[sn] [givenName]"
        End If

        If UserCNFormat = "" Then
            UserCNFormat = "[sn] [givenName]"
        End If

        Dim UserCN As String = UserCNFormat
        Dim UserDisplayname As String = UserDisplayNameFormat

        ' Replace any GUID's
        Dim NewGUID As String = Guid.NewGuid.ToString
        UserCN = Replace(UserCN, "[GUID]", NewGUID)
        UserDisplayname = Replace(UserDisplayname, "[GUID]", NewGUID)

        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

                If t.BackColor = System.Drawing.ColorTranslator.FromHtml(gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings))) Then
                    If t.Text.Trim.Length = 0 Then SaveNotValidAtThisTime = True
                End If
            End If
        Next

        If SaveNotValidAtThisTime Then
            MsgBox("This Person cannot be created.  You must enter data for all mandatory fields.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Information")
            Exit Sub
        End If

        ' TODO:  Security

        '' Remember all custom attributes
        Dim Index As Integer = 0
        Dim CustomAttributes As New SortedList

        ' Now go through the textboxes we have on the form
        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

                ' **********************************************
                ' Hardcoded to use sn and givenName
                Select Case t.Tag.ToString.ToLower
                    Case "sn"
                        UserSurname = t.Text
                    Case "givenname"
                        UserFirstname = t.Text
                End Select
                ' **********************************************
                
                UserCN = Replace(UserCN, "[" & t.Tag & "]", t.Text)
                UserDisplayname = Replace(UserDisplayname, "[" & t.Tag & "]", t.Text)

                If t.Text.Trim.Length > 0 Then
                    If t.Tag.ToString.ToLower Like "customfield*" Then ' We have to set or add this value
                        Dim CustomFieldNumber As String = t.Tag.ToString.Substring(11, 2)
                        Dim CustomFieldValue As String = "{" & CustomFieldNumber & "=" & t.Text & "}"

                        If CustomAttributes.Contains(CustomFieldNumber) Then                      ' Set
                            Debug.Print("Setting " & CustomFieldNumber & "=" & CustomFieldValue)
                            CustomAttributes(CustomFieldNumber) = CustomFieldValue
                        Else                                                                      ' Add
                            Debug.Print("Adding " & CustomFieldNumber & "=" & CustomFieldValue)
                            CustomAttributes.Add(CustomFieldNumber, CustomFieldValue)
                        End If
                    End If
                Else
                    If t.Tag.ToString.ToLower Like "customfield*" Then                             ' Remove
                        Dim CustomFieldNumber As String = t.Tag.ToString.Substring(11, 2)
                        Dim CustomFieldValue As String = "{" & CustomFieldNumber & "=" & t.Text & "}"

                        If CustomAttributes.Contains(CustomFieldNumber) Then ' Check if it already has a value first
                            Debug.Print("Removing " & CustomFieldNumber & " (" & CustomAttributes(CustomFieldNumber) & ")")
                            CustomAttributes.Remove(CustomFieldNumber)
                        End If
                    End If
                End If
            Else
                If TypeOf c Is Button Then
                    Dim b As Button = c

                    If b.Name.ToLower.EndsWith("lookup") Then
                        b.Enabled = False
                    End If
                End If
            End If
        Next

        ' Now create the User
        Dim User As DirectoryEntry = Nothing

        User = ParentOU.Children.Add("CN=" & UserCN, "user")
        Try
            User.CommitChanges()
        Catch ex As Exception

        End Try


        User.Properties("givenName").Add(UserFirstname)
        User.Properties("sn").Add(UserSurname)

        Try
            User.CommitChanges()
        Catch ex As Exception
            'MsgBox("Could not save details.  The message returned was " & ex.Message, MsgBoxStyle.Exclamation, "Error")
            gFunctions.FileLoggingObject.WriteErrorEvent("Could not create New User.  The error returned is detailed below:")
            gFunctions.FileLoggingObject.WriteErrorEvent(ex.Message)
            gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(DIRECTORY, ERROR_DIRECTORY.CREATE_USER_ERROR)
            gFunctions.LastStatus.LastException = ex
        End Try

        ' Load all properties
        'User.RefreshCache()

        'User.Properties("wbemPath").Clear()

        ' Now add the custom fields back from the Collection which contains all of the values
        For Each CustomAttribute In CustomAttributes.Keys
            gFunctions.FileLoggingObject.WriteDebugEvent("Adding to 'wbemPath' Collection, the value '" & CustomAttributes.Item(CustomAttribute).ToString & "'")
            User.Properties("wbemPath").Add(CustomAttributes.Item(CustomAttribute).ToString)
        Next

        gFunctions.FileLoggingObject.WriteDebugEvent("Saving wbemPath Attributes to object")
        Try
            User.CommitChanges()
        Catch ex As Exception
            gFunctions.FileLoggingObject.WriteErrorEvent("Could not update wbemPath.  The error returned is detailed below:")
            gFunctions.FileLoggingObject.WriteErrorEvent(ex.Message)
        End Try

        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

                Try
                    gFunctions.FileLoggingObject.WriteDebugEvent("Attempting to work with User property from Control tag.  Property name is: '" & t.Tag & "'")
                    If t.Text.Trim.Length > 0 Then
                        If Not t.Tag.ToString.ToLower Like "customfield*" Then ' We have already done the custom fields
                            Select Case t.Tag.ToString.ToLower
                                Case "name", "distinguishedname", "cn"
                                    ' We never modify these!
                                Case Else
                                    ' Normal fields
                                    If t.Name.ToString.ToLower.EndsWith("lookup") Then
                                        ' Lookup value.  Don't save as it is display only.
                                    Else
                                        If t.Text.Trim.Length > 0 Then
                                            If User.Properties.Contains(t.Tag) Then
                                                User.Properties(t.Tag).Value = t.Text
                                            Else
                                                User.Properties(t.Tag).Add(t.Text)
                                            End If
                                            'User.Properties(t.Tag).Add(t.Text)
                                        Else
                                            User.Properties(t.Tag).Clear()
                                        End If
                                        End If
                            End Select
                        End If
                    End If
                Catch ex As Exception
                    gFunctions.FileLoggingObject.WriteDebugEvent("Failed to extract tag from Control.  The control name is '" & t.Name & "'")
                End Try
                t.ReadOnly = True
            End If
            gFunctions.FileLoggingObject.WriteDebugEvent("Attempting to Commit User to Directory")
            If gFunctions.FileLoggingObject.LogLevel And LOGGING_LEVEL.DEBUGGING Then
                Try
                    User.CommitChanges()
                    gFunctions.FileLoggingObject.WriteDebugEvent("Commit passed.")
                Catch ex2 As Exception
                    gFunctions.FileLoggingObject.WriteDebugEvent("Commit failed.  The error was " & ex2.Message)
                End Try
            End If
        Next

        Try
            User.CommitChanges()
        Catch ex As Exception
            'MsgBox("Could not save details.  The message returned was " & ex.Message, MsgBoxStyle.Exclamation, "Error")
            gFunctions.FileLoggingObject.WriteErrorEvent("Could not save New User.  The error returned is detailed below:")
            gFunctions.FileLoggingObject.WriteErrorEvent(ex.Message)
            gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(DIRECTORY, ERROR_DIRECTORY.CREATE_USER_ERROR)
            gFunctions.LastStatus.LastException = ex
        End Try

        User.Close()
        ParentOU.Close()
        User = Nothing
        ParentOU = Nothing
        lblStatus.Text = "Changes have been saved"
        btnSave.Enabled = False
        MsgBox("Operation Complete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Create Person")
        gFunctions.LastStatus.Clear()
        gFunctions.LastStatus.CADS_ERRORCODE = Types.ERRORTYPE.SUCCESS

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim Result As DialogResult

        Try
            DoSave()
            m_ChangesMade = False
            Close()
        Catch ex As Exception
            Select Case gFunctions.LastStatus.LastException.Message
                Case "The object already exists. (Exception from HRESULT: 0x80071392)"
                    Result = MsgBox("This User already exists.  Check you have the correct details (including the target OU) and try again.", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Error")
                    Err.Clear()
                Case Else
            End Select
            
            'btnSave.Enabled = False
        End Try

    End Sub

    Private Sub frmNewPerson_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Debug.Print("New User Form Size: " & Me.Width & "," & Me.Height)
    End Sub

    Private Sub btnSetOU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetOU.Click
        Dim OUForm As New frmOUSearch(gFunctions.TargetDirectoryUserRootDN, gFunctions.TargetDirectoryPhoneRootDN)

        Dim User As DirectoryEntry = New DirectoryEntry(m_UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        ' m_UserPath
        OUForm.OUPath = m_UserPath

        Dim Result As DialogResult = OUForm.ShowDialog()

        Dim Username As String = "CN=NothingInParticular" ' Doesn't matter as we are just using this to build the treeview

        If Result = Windows.Forms.DialogResult.OK Then
            tvwDirectory.Nodes.Clear()
            m_UserPath = OUForm.OUPath
            BuildTreeview("LDAP://" & gFunctions.CurrentDirectoryServer & Username & "," & gFunctions.RemoveServerFromDN(OUForm.OUPath))
            Me.btnSave.Enabled = True
            Me.tvwDirectory.BackColor = Color.White
        End If

        User.Close()
        User = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Function DoClose() As Boolean
        If m_ChangesMade Then
            Dim Result As DialogResult = MsgBox("Save changes?", MsgBoxStyle.YesNoCancel, "Confirmation required")

            Select Case Result
                Case Windows.Forms.DialogResult.Yes
                    DoSave()
                    Return True
                Case Windows.Forms.DialogResult.No
                    Return True
                Case Windows.Forms.DialogResult.Cancel

            End Select
        Else
            Return True
        End If
    End Function

    Private Sub frmNewPerson_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If DoClose() Then
        Else
            e.Cancel = True
        End If
    End Sub

End Class