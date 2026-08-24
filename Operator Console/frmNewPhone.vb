Imports System.DirectoryServices
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmNewPhone

    'Private m_Phone As New DirectoryUser
    Private m_Phonename As String
    Private m_PhonePath As String
    Private m_ChangesMade As Boolean = False
    Private m_LoadComplete As Boolean = False

    Public Property Phonename() As String
        Get
            Return m_Phonename
        End Get
        Set(ByVal value As String)
            m_Phonename = value
        End Set
    End Property

    Public Property PhonePath() As String
        Get
            Return m_PhonePath
        End Get
        Set(ByVal value As String)
            m_PhonePath = value
        End Set
    End Property

    Private Sub frmNewPhone_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Dimensions As String = gFunctions.CollectionValue("NewPhoneFormSize", gSettings)

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

    Private Sub GetUserDetails()
        BuildTreeview(m_PhonePath)
    End Sub

    Private Sub FillDetailElements()
        Dim de As New DirectoryEntry(m_PhonePath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

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
        Dim MyUser As New DirectoryEntry(UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim Path As String()
        Dim CommonPath As String = gFunctions.TargetDirectoryUserRootDN.Substring(gFunctions.TargetDirectoryUserRootDN.IndexOf("=") - 2)

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

    Private Sub DoSave()
        Dim ParentOU As New DirectoryEntry(m_PhonePath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim SaveNotValidAtThisTime As Boolean = False

        ' What format do we use to create a phone?  Found in config file
        Dim PhoneDisplayNameFormat As String = ""
        Dim PhoneCNFormat As String = ""
        Try
            PhoneDisplayNameFormat = gSettings("PhoneDisplayNameFormat").ToString
            PhoneCNFormat = gSettings("PhoneCNFormat").ToString
        Catch
        End Try

        If PhoneDisplayNameFormat = "" Then
            PhoneDisplayNameFormat = "CADS TELEPHONE [GUID]"
        End If

        If PhoneCNFormat = "" Then
            PhoneCNFormat = "CADS TELEPHONE [GUID]"
        End If

        Dim PhoneCN As String = PhoneCNFormat
        Dim PhoneDisplayname As String = PhoneDisplayNameFormat

        ' Replace any GUID's
        Dim NewGUID As String = Guid.NewGuid.ToString
        PhoneCN = Replace(PhoneCN, "[GUID]", NewGUID)
        PhoneDisplayname = Replace(PhoneDisplayname, "[GUID]", NewGUID)

        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

                If t.BackColor = System.Drawing.ColorTranslator.FromHtml(gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings))) Then
                    If t.Text.Trim.Length = 0 Then SaveNotValidAtThisTime = True
                End If
            End If
        Next

        If SaveNotValidAtThisTime Then
            MsgBox("This Phone cannot be created.  You must enter data for all mandatory fields.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Information")
            Exit Sub
        End If

        ' TODO:  Security

        'de.RefreshCache()

        '' Remember all custom attributes
        Dim Index As Integer = 0
        Dim CustomAttributes As New SortedList

        ' Now go through the textboxes we have on the form
        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

                PhoneCN = Replace(PhoneCN, "[" & t.Tag & "]", t.Text)
                PhoneDisplayname = Replace(PhoneDisplayname, "[" & t.Tag & "]", t.Text)

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
        Dim Phone As DirectoryEntry = ParentOU.Children.Add("CN=" & PhoneCN, "user")

        'User.Properties("distinguishedName")(0) = "CN=" & UserCN
        Try
            Phone.CommitChanges()
        Catch ex As Exception
            MsgBox("Could not save details.  The message returned was " & ex.Message, MsgBoxStyle.Exclamation, "Error")
            gFunctions.FileLoggingObject.WriteErrorEvent("Could create Phone.  The error returned is detailed below:")
            gFunctions.FileLoggingObject.WriteErrorEvent(ex.Message)
        End Try
        ' Load all properties
        Phone.RefreshCache()

        Phone.Properties("wbemPath").Clear()

        ' Now add the custom fields back from the Collection which contains all of the values
        For Each CustomAttribute In CustomAttributes.Keys
            'Debug.Print("Wish to add to 'wbemPath', the value " & CustomAttributes.Item(CustomAttribute).ToString)
            Phone.Properties("wbemPath").Add(CustomAttributes.Item(CustomAttribute).ToString)
        Next

        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

                If t.Text.Trim.Length > 0 Then
                    If Not t.Tag.ToString.ToLower Like "customfield*" Then ' We have already done the custom fields
                        Select Case t.Tag.ToString.ToLower
                            Case "name", "distinguishedname"
                                ' We never modify these!
                            Case Else
                                ' Normal fields
                                If t.Name.ToString.ToLower.EndsWith("lookup") Then
                                    ' Lookup value.  Don't save as it is display only.
                                Else
                                    If t.Text.Trim.Length > 0 Then
                                        Phone.Properties(t.Tag).Value = t.Text
                                    Else
                                        Phone.Properties(t.Tag).Clear()
                                    End If
                                End If
                        End Select
                    End If
                End If
                t.ReadOnly = True
            End If
        Next

        Try
            Phone.CommitChanges()
        Catch ex As Exception
            MsgBox("Could not save details.  The message returned was " & ex.Message, MsgBoxStyle.Exclamation, "Error")
            gFunctions.FileLoggingObject.WriteErrorEvent("Could not save Phone details.  The error returned is detailed below:")
            gFunctions.FileLoggingObject.WriteErrorEvent(ex.Message)
        End Try

        Phone.Close()
        ParentOU.Close()
        Phone = Nothing
        ParentOU = Nothing
        lblStatus.Text = "Changes have been saved"
        btnSave.Enabled = False
        MsgBox("Operation Complete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Create Phone")
        Close()

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

        LayoutSettings = gFunctions.MySection(gFunctions.InteractiveUserUsername, "ConsoleNewPhoneFields")

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
                            Dim AttributeStatus As String = gFunctions.ValueFromSetting(gFunctions.LoadConfigSetting("PhoneAttributes", Elements(1)), 2)

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

    Private Sub btnSetOU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetOU.Click
        Dim OUForm As New frmOUSearch(gFunctions.TargetDirectoryUserRootDN, gFunctions.TargetDirectoryPhoneRootDN)

        Dim Phone As DirectoryEntry = New DirectoryEntry(m_PhonePath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        ' m_UserPath
        OUForm.OUPath = m_PhonePath
        OUForm.UseUserRoot = False

        Dim Result As DialogResult = OUForm.ShowDialog()

        Dim Phonename As String = "CN=NothingInParticular" ' Doesn't matter as we are just using this to build the treeview

        If Result = Windows.Forms.DialogResult.OK Then
            'Debug.Print(OUForm.OUPath)
            m_PhonePath = OUForm.OUPath
            BuildTreeview("LDAP://" & gFunctions.CurrentDirectoryServer & Phonename & "," & gFunctions.RemoveServerFromDN(OUForm.OUPath))
            Me.btnSave.Enabled = True
        End If

        Phone.Close()
        Phone = Nothing
    End Sub

End Class