Imports System.DirectoryServices
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmUserDetails

    Private m_User As New DirectoryEntry
    Private m_Username As String
    Private m_UserPath As String
    Public ParentSearchForm As frmUserSearch
    Private m_AuditCollection As New Collections.Specialized.StringDictionary
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

    Private Sub frmUserDetails_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If DoClose() Then
            ParentSearchForm.FocusOnSearchTextbox()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub frmDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SizeForm()

        PlaceDetailElements()
        'GetUserDetails()
        BuildTreeview(m_UserPath)
        FillDetailElements()
        m_LoadComplete = True
        txtMandatory.BackColor = System.Drawing.ColorTranslator.FromHtml(gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings)))
    End Sub

    Private Sub SizeForm()
        Dim Dimensions As String = gFunctions.CollectionValue("UserDetailFormSize", gSettings)

        Dim Size() As String
        Size = Split(Dimensions, "|")

        Me.Width = Size(0)
        Me.Height = Size(1)
        Me.CenterToParent()
    End Sub

    Private Sub FillDetailElements()
        Dim de As New DirectoryEntry(m_UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        de.RefreshCache()

        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c
                Dim Tag As String = ""
                Dim LookupKey As String = ""

                'Debug.Print("Property to read: " & t.Tag.ToString)
                If t.Tag.ToString.Contains("|") Then
                    Tag = t.Tag.ToString.Substring(0, t.Tag.ToString.IndexOf("|"))
                    LookupKey = t.Tag.ToString.Substring(t.Tag.ToString.IndexOf("|") + 1)
                Else
                    Tag = t.Tag
                End If

                If Tag.ToLower.Contains("customfield") Then
                    Dim CustomFieldNumberToDisplay As String = Tag.ToLower.Replace("customfield", "")

                    For Each Value As String In de.Properties("wbemPath")
                        If Value Like ("{" & CustomFieldNumberToDisplay & "*}") Then
                            t.Text = Value.Substring(4, Value.Length - 5) ' 5 is the length = '{xx=' and '}'
                            Debug.Print("Custom Field:" & CustomFieldNumberToDisplay & "=" & t.Text)
                        End If
                    Next
                Else
                    If de.Properties(Tag).Count > 0 Then
                        If LookupKey = "" Then
                            t.Text = de.Properties(Tag)(0).ToString
                        Else
                            ' retrieve the value from a Lookup list
                            Dim CollectionValues As Collections.Specialized.OrderedDictionary = gFunctions.GetLookupCollection(gFunctions.CurrentDirectoryServer, LookupKey, True, gFunctions.TargetDirectoryConfigurationRootDN, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword)
                            t.Text = gFunctions.GetLookupValue(de.Properties(Tag)(0).ToString, LookupKey)
                        End If
                    Else
                        Try
                            If LookupKey = "" Then
                                Try
                                    t.Text = de.Properties(Tag)(0).Value.ToString
                                Catch
                                    ' Not an issue, this is expected because there may be no value in this attribute
                                End Try
                            Else
                                ' retrieve the value from a Lookup list
                                t.Text = "Lookup on " & LookupKey & " using " & Tag & " = " & de.Properties(Tag)(0).value.ToString
                                Dim Values As Collections.Specialized.OrderedDictionary = gFunctions.GetLookupCollection(gFunctions.CurrentDirectoryServer, LookupKey, True, gFunctions.TargetDirectoryConfigurationRootDN, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword)
                            End If
                        Catch ex As Exception
                            'Debug.Print("Error retrieving User Property '" & t.Tag & "': " & ex.Message)
                        End Try
                    End If
                End If
            End If
        Next

        Dim GivenName As String = "[No given name]"
        Dim Surname As String = "[No Surname]"

        Try
            GivenName = de.Properties("givenName").Value.ToString
        Catch ex As Exception

        End Try

        Try
            Surname = de.Properties("sn").Value.ToString
        Catch ex As Exception

        End Try

        Me.Text = "Person Details (" & GivenName & " " & Surname & ")"

        de.Close()
        de = Nothing
    End Sub

    Private Sub BuildTreeview(ByVal UserPath As String)
        Dim MyUser As New DirectoryEntry(UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim Path As String()
        Dim CommonPath As String = gFunctions.TargetDirectoryUserRootDN.Substring(gFunctions.TargetDirectoryUserRootDN.IndexOf("=") - 2)
        Dim CurrentParentPath As String = ""
        'Dim TempParentOUPath As String()
        Dim ParentOUPaths As New Collections.Specialized.NameValueCollection
        'Dim ParentOUPath As String()

        UserPath = Replace(UserPath.ToUpper, CommonPath.ToUpper, "")

        If UserPath.EndsWith(",") Then UserPath = UserPath.Substring(0, Len(UserPath) - 1)

        Path = Split(UserPath, ",")

        Dim Parent As DirectoryEntry = MyUser.Parent
        Dim OULevelCount As Integer = 0
        'Debug.Print("Parent: " & Parent.Path)

        For i As Integer = 0 To Parent.Path.ToString.Length - 1
            If Parent.Path.Substring(i, 1) = "," Then OULevelCount += 1
            'Debug.Print(Parent.Path.Substring(0, i + 1) & " OULevels: " & OULevelCount)
        Next

        'Debug.Print("User Root: " & gFunctions.TargetDirectoryUserRootDN)
        ' OULevelCount now contains the number of hierarchical levels deep the User is.

        ' Retrieve the path of each OU above the User, until we get to the user Root
        Dim Index As Integer
        Do Until Parent.Path.ToLower = gFunctions.TargetDirectoryUserRootDN.ToLower
            'Debug.Print("Parent: " & Parent.Path)
            ParentOUPaths.Add(Index.ToString, Parent.Path)
            Parent = Parent.Parent
            Index += 1
        Loop

        ' Now to reorder the Parent Paths
        Dim OULevelCountBelowUserRoot = Index
        Dim ParentOUPath(ParentOUPaths.Count - 1) As String
        For i As Integer = 0 To ParentOUPaths.Count - 1
            ParentOUPath(i) = ParentOUPaths((OULevelCountBelowUserRoot - i - 1))
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

        For i As Integer = UBound(Path) To 0 Step -1
            Path(i) = Path(i).Remove(0, 3)
            'Debug.Print(Path(i))
            'Debug.Print(CurrentParentPath)
        Next

        ' remove everything before the 'CN='
        Path(0) = Path(0).Remove(0, Path(0).IndexOf("CN") + 3)

        ' Finally build the tree structure
        Dim ParentNode As TreeNode
        ' Add the root node

        ParentNode = tvwDirectory.Nodes.Add(Path(UBound(Path)) & " (" & OUCode(0) & ")")

        ' Now add each child node
        For i As Integer = UBound(Path) - 1 To 1 Step -1
            Dim ChildNode As New TreeNode

            ParentNode = AddNode(ParentNode, Path(i) & " (" & OUCode(UBound(Path) - i) & ")")
        Next

        MyUser.Close()
        MyUser = Nothing

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
                        Dim EditsEnabled As Boolean = True

                        If t.Name.ToLower.EndsWith("lookup") Then
                            EditsEnabled = False
                        End If

                        If t.Name.ToLower.EndsWith("audit") Then
                            EditsEnabled = False
                        End If

                        If EditsEnabled Then
                            t.ReadOnly = False
                        Else
                            t.ReadOnly = True
                        End If
                End Select
            Else
                If TypeOf c Is Button Then
                    Dim b As Button = c
                    If b.Name.ToLower.EndsWith("lookup") Then
                        b.Enabled = True
                    End If
                End If
            End If
        Next

        btnEdit.Enabled = False
        btnSave.Enabled = True
        lblStatus.Text = "Edit"
    End Sub

    Private Sub DoSave()
        Dim de As New DirectoryEntry(m_UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        ' TODO:  Security

        de.RefreshCache()

        ' Remember all custom attributes
        Dim Index As Integer = 0
        Dim CustomAttributes As New SortedList

        While Index < de.Properties("wbemPath").Count
            Dim Value As String = de.Properties("wbemPath")(Index)
            'Debug.Print("Adding " & de.Properties("wbemPath")(Index).Substring(1, 2) & "=" & de.Properties("wbemPath")(Index))
            CustomAttributes.Add(de.Properties("wbemPath")(Index).Substring(1, 2), de.Properties("wbemPath")(Index))

            Index += 1
        End While

        ' Now go through the textboxes we have on the form
        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

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

        de.Properties("wbemPath").Clear()

        ' Now add the custom fields back from the Collection which contains all of the values
        For Each CustomAttribute In CustomAttributes.Keys
            'Debug.Print("Wish to add to 'wbemPath', the value " & CustomAttributes.Item(CustomAttribute).ToString)
            de.Properties("wbemPath").Add(CustomAttributes.Item(CustomAttribute).ToString)
        Next

        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

                If t.Text.Trim.Length > 0 Then
                    If Not t.Tag.ToString.ToLower Like "customfield*" Then ' We have already done the custom fields
                        Select Case t.Tag.ToString.ToLower
                            Case "name", "distinguishedname", "cn"
                                ' We never modify these!
                            Case Else
                                ' Normal and audit fields
                                Dim IsAudit As Boolean = False
                                Dim IsLookup As Boolean = False
                                Dim IsNormal As Boolean = True

                                If t.Name.ToString.ToLower.EndsWith("lookup") Then
                                    IsLookup = True
                                    IsNormal = False
                                End If
                                If t.Name.ToString.ToLower.EndsWith("audit") Then
                                    IsAudit = True
                                    IsNormal = False
                                End If

                                If IsLookup Then
                                    ' Lookup value.  Don't save as it is display only.
                                End If

                                If IsAudit Then
                                    Dim ReplacementText As String = m_AuditCollection(t.Name)
                                    Dim CurrentHostname As String = System.Net.Dns.GetHostName

                                    ReplacementText = Replace(ReplacementText, "[Windows Username]", "[" & My.User.Name & "]")
                                    ReplacementText = Replace(ReplacementText, "[Date]", "[" & Date.Now.Date & "]")
                                    ReplacementText = Replace(ReplacementText, "[Time]", "[" & Date.Now.TimeOfDay.ToString & "]")
                                    ReplacementText = Replace(ReplacementText, "[CADS Username]", "[" & gFunctions.InteractiveUserUsername & "]")
                                    ReplacementText = Replace(ReplacementText, "[Windows Computername]", "[" & My.Computer.Name & "]")
                                    ReplacementText = Replace(ReplacementText, "[IP Address]", "[" & System.Net.Dns.GetHostEntry(CurrentHostname).AddressList(0).ToString & "]")
                                    de.Properties(t.Tag).Value = ReplacementText
                                End If

                                If IsNormal Then
                                    If t.Text.Trim.Length > 0 Then
                                        de.Properties(t.Tag).Value = t.Text
                                    Else
                                        de.Properties(t.Tag).Clear()
                                    End If
                                End If

                        End Select
                    End If
                End If
                t.ReadOnly = True
            End If
        Next

        Try
            de.CommitChanges()
        Catch ex As Exception
            MsgBox("Could not save details.  The message returned was " & ex.Message, MsgBoxStyle.Exclamation, "Error")
            gFunctions.FileLoggingObject.WriteErrorEvent("Could not save User details.  The error returned is detailed below:")
            gFunctions.FileLoggingObject.WriteErrorEvent(ex.Message)
        End Try

        de.Close()
        lblStatus.Text = "Changes have been saved"
        btnSave.Enabled = False
        btnEdit.Enabled = True
        ParentSearchForm.DoSearch()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        DoSave()
        m_ChangesMade = False
    End Sub

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

        LayoutSettings = gFunctions.MySection(gFunctions.InteractiveUserUsername, "ConsoleUserDetailFields")

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
                        NewButton.Enabled = False
                        Me.pnlDetails.Controls.Add(NewButton)
                        AddHandler NewButton.Click, AddressOf Me.Lookup_Click
                    Else
                        If Settings(Index).ToString.ToLower.EndsWith("audit") Then
                            ' Add this to the Audit collection
                            m_AuditCollection.Add(Settings(Index), Elements(3))
                            Dim NewTextbox As New TextBox
                            NewTextbox.Name = Settings(Index)
                            NewTextbox.Tag = Elements(1)
                            ' Item 2 is the description, which is not used
                            NewTextbox.Left = Elements(4)
                            NewTextbox.Top = Elements(5)
                            NewTextbox.Width = Elements(6)
                            NewTextbox.TabIndex = Elements(7)
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
                            NewTextbox.ReadOnly = True
                            Dim AttributeStatus As String = gFunctions.ValueFromSetting(gFunctions.LoadConfigSetting("UserAttributes", Elements(1)), 2)

                            If AttributeStatus.ToLower = "mandatory" Then
                                NewTextbox.BackColor = System.Drawing.ColorTranslator.FromHtml(gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings)))
                            End If

                            Me.pnlDetails.Controls.Add(NewTextbox)

                            AddHandler NewTextbox.TextChanged, AddressOf Text_TextChanged

                        End If
                        End If
            End Select
            Index += 1
        Next
    End Sub

    Private Sub frmDetails_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Debug.Print(e.KeyCode)
        Select Case Chr(e.KeyCode)
            Case Convert.ToChar(113) ' F2
                AllowEdits()
            Case Convert.ToChar(27)  ' Esc
                Me.Close()
                'Case Convert.ToChar(46)  ' Del
                'AskDelete()
        End Select
    End Sub

    Private Sub frmDetails_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Debug.Print("User Details Form Size: " & Me.Width & "," & Me.Height)
    End Sub

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

    Private Sub btnMessages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMessages.Click
        Dim MessagesForm As New frmUserMessages

        MessagesForm.Show()
    End Sub

    Private Sub DoDelete()
        Dim User As DirectoryEntry = New DirectoryEntry(m_UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim Parent As DirectoryEntry = New DirectoryEntry(User.Parent.Path, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        Parent.Children.Remove(User)

        MsgBox("Operation Complete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Delete User")

        ParentSearchForm.DoSearch()

        Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        AskDelete()
    End Sub

    Private Sub AskDelete()
        Dim Result As DialogResult = MessageBox.Show("Are you sure you want to delete this User?", "Confirmation required", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If Result = Windows.Forms.DialogResult.Yes Then
            DoDelete()
        Else
            MsgBox("Operation Cancelled", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Delete User")
        End If
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
                    ParentSearchForm.DoSearch()
                    Return True
                Case Windows.Forms.DialogResult.Cancel
            End Select
        Else
            Return True
        End If
    End Function

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        'MsgBox("Sorry, your current access rights do not allow you to perform this operation.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Information")
        Dim User As New DirectoryEntry(m_UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        Dim NewPath As String = ""
        Dim OUForm As New frmOUSearch(gFunctions.TargetDirectoryUserRootDN, gFunctions.TargetDirectoryPhoneRootDN)

        OUForm.UseUserRoot = True
        Dim Result As DialogResult = OUForm.ShowDialog()

        If Result = Windows.Forms.DialogResult.OK Then
            Try
                gFunctions.MoveUser(m_UserPath, OUForm.OUPath)
                If gFunctions.LastStatus.CADS_ERRORCODE = ERRORTYPE.SUCCESS Then
                    MsgBox("Operation Complete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Move Person")
                Else
                    MsgBox("Operation Failed", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Move Person")
                End If
            Catch ex As Exception
                MsgBox("Operation Failed", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Move Person")
            End Try
        Else
            MsgBox("Operation Canceled", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Move Person")
        End If
    End Sub

End Class