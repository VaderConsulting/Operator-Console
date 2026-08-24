Imports System.DirectoryServices
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmPhoneDetails

    Private m_Phone As New DirectoryUser
    Public Phonename As String
    Private m_PhonePath As String
    Public ParentPhonesForm As frmPhones
    Private m_ChangesMade As Boolean = False
    Private m_LoadComplete As Boolean = False

    Public Property UserPath() As String
        Get
            Return m_PhonePath
        End Get
        Set(ByVal value As String)
            m_PhonePath = value
        End Set
    End Property

    Private Sub frmPhoneDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SizeForm()

        PlaceDetailElements()
        'GetUserDetails()
        BuildTreeview(m_PhonePath)
        FillDetailElements()
        m_LoadComplete = True
        txtMandatory.BackColor = System.Drawing.ColorTranslator.FromHtml(gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings)))
    End Sub

    Private Sub SizeForm()
        Dim Dimensions As String = gFunctions.CollectionValue("PhoneDetailFormSize", gSettings)

        Dim Size() As String
        Size = Split(Dimensions, "|")

        Me.Width = Size(0)
        Me.Height = Size(1)
        Me.CenterToParent()
    End Sub

    Private Sub GetPhoneDetails()
        'DirectoryConnection.RefreshCache()



        'If DirectoryConnection.Properties("sn").Count > 0 Then m_User.Surname = DirectoryConnection.Properties("sn")(0).ToString
        'If DirectoryConnection.Properties("givenName").Count > 0 Then m_User.GivenName = DirectoryConnection.Properties("givenName")(0).ToString
        'If DirectoryConnection.Properties("department").Count > 0 Then m_User.Department = DirectoryConnection.Properties("department")(0).ToString
        'If DirectoryConnection.Properties("description").Count > 0 Then m_User.Description = DirectoryConnection.Properties("description")(0).ToString
        'If DirectoryConnection.Properties("telephoneNumber").Count > 0 Then m_User.TelephoneNumber = DirectoryConnection.Properties("telephoneNumber")(0).ToString
        'If DirectoryConnection.Properties("mobile").Count > 0 Then m_User.MobileTelephoneNumber = DirectoryConnection.Properties("mobile")(0).ToString
        'If DirectoryConnection.Properties("name").Count > 0 Then m_User.DisplayName = DirectoryConnection.Properties("name")(0).ToString
        'If DirectoryConnection.Properties("description").Count > 0 Then m_User.Description = DirectoryConnection.Properties("description")(0).ToString
        'If DirectoryConnection.Properties("department").Count > 0 Then m_User.Department = DirectoryConnection.Properties("department")(0).ToString

        ' TODO:  Get the other AD properties

        'Me.Text = "Person Details - " & m_Username

        ''
    End Sub

    Private Sub FillDetailElements()
        Dim de As New DirectoryEntry(m_PhonePath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        de.RefreshCache()

        For Each c As Control In Me.pnlDetails.Controls
            If TypeOf c Is TextBox Then
                Dim t As TextBox = c

                'Debug.Print("Property to read: " & t.Tag.ToString)

                If t.Tag.ToString.ToLower.Contains("customfield") Then
                    Dim CustomFieldNumberToDisplay As String = t.Tag.ToString.ToLower.Replace("customfield", "")

                    For Each Value As String In de.Properties("wbemPath")
                        If Value Like ("{" & CustomFieldNumberToDisplay & "*}") Then
                            t.Text = Value.Substring(4, Value.Length - 5) ' 5 is the length = '{xx=' and '}'
                        End If
                    Next
                Else
                    If de.Properties(t.Tag).Count > 0 Then
                        t.Text = de.Properties(t.Tag)(0).ToString
                    Else
                        Try
                            t.Text = de.Properties(t.Tag)(0).Value.ToString
                        Catch ex1 As Exception
                            Try
                                t.Text = de.Properties(t.Tag).Value.ToString
                            Catch ex2 As Exception

                            End Try
                            'Debug.Print("Error retrieving User Property '" & t.Tag & "': " & ex.Message)
                        End Try
                    End If
                End If
            End If
        Next

        Dim TelephoneNumber As String = "[No number]"

        Try
            TelephoneNumber = de.Properties("telephoneNumber").Value.ToString
        Catch ex As Exception

        End Try

        Me.Text = "Phone Details (" & TelephoneNumber & ")"

        If de.Properties("manager").ToString <> "" Then
            btnUnassign.Enabled = True
        End If

        de.Close()
        de = Nothing
    End Sub

    Private Sub BuildTreeview(ByVal PhonePath As String)
        Dim MyPhone As New DirectoryEntry(PhonePath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim Path As String()
        Dim CommonPath As String = gFunctions.TargetDirectoryPhoneRootDN.Substring(gFunctions.TargetDirectoryPhoneRootDN.IndexOf("=") - 2)
        Dim CurrentParentPath As String = ""
        Dim ParentOUPaths As New Collections.Specialized.NameValueCollection

        PhonePath = Replace(PhonePath.ToUpper, CommonPath.ToUpper, "")

        If PhonePath.EndsWith(",") Then PhonePath = PhonePath.Substring(0, Len(PhonePath) - 1)

        Path = Split(PhonePath, ",")

        Dim Parent As DirectoryEntry = MyPhone.Parent
        Dim OULevelCount As Integer = 0

        For i As Integer = 0 To Parent.Path.ToString.Length - 1
            If Parent.Path.Substring(i, 1) = "," Then OULevelCount += 1
        Next

        ' OULevelCount now contains the number of hierarchical levels deep the User is.

        ' Retrieve the path of each OU above the User, until we get to the user Root
        Dim Index As Integer
        Do Until Parent.Path.ToLower = gFunctions.TargetDirectoryPhoneRootDN.ToLower
            ParentOUPaths.Add(Index.ToString, Parent.Path)
            Parent = Parent.Parent
            Index += 1
        Loop

        ' Now to reorder the Parent Paths
        Dim OULevelCountBelowPhoneRoot = Index

        ' Changes to support users in the root of the top OU
        If OULevelCountBelowPhoneRoot > 0 Then
            Dim ParentOUPath(ParentOUPaths.Count - 1) As String
            For i As Integer = 0 To ParentOUPaths.Count - 1
                ParentOUPath(i) = ParentOUPaths((OULevelCountBelowPhoneRoot - i - 1))
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
        Else
            For i As Integer = UBound(Path) To 0 Step -1
                Path(i) = Path(i).Remove(0, 3)
            Next

            ' remove everything before the 'CN='
            Path(0) = Path(0).Remove(0, Path(0).IndexOf("CN") + 3)

            ' Finally build the tree structure 
            Dim ParentNode As TreeNode
            ' Add the root node

            ParentNode = tvwDirectory.Nodes.Add(Path(UBound(Path)))

            ' Now add each child node
            For i As Integer = UBound(Path) - 1 To 1 Step -1
                Dim ChildNode As New TreeNode

                ParentNode = AddNode(ParentNode, Path(i))
            Next
        End If

        MyPhone.Close()
        MyPhone = Nothing

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

    Private Sub DoSave()
        Dim de As New DirectoryEntry(m_PhonePath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        ' TODO:  Security

        de.RefreshCache()

        ' Remember all custom attributes
        Dim Index As Integer = 0
        Dim CustomAttributes As New SortedList

        While Index < de.Properties("wbemPath").Count
            Dim Value As String = de.Properties("wbemPath")(Index)
            Debug.Print("Adding " & de.Properties("wbemPath")(Index).Substring(1, 2) & "=" & de.Properties("wbemPath")(Index))
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
            End If
        Next

        de.Properties("wbemPath").Clear()

        ' Now add the custom fields back from the Collection which contains all of the values
        For Each CustomAttribute In CustomAttributes.Keys
            de.Properties("wbemPath").Add(CustomAttributes.Item(CustomAttribute).ToString)
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
                                If t.Text.Trim.Length > 0 Then
                                    de.Properties(t.Tag).Value = t.Text
                                Else
                                    de.Properties(t.Tag).Clear()
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
            gFunctions.FileLoggingObject.WriteErrorEvent("Could not save Phone details.  The error returned is detailed below:")
            gFunctions.FileLoggingObject.WriteErrorEvent(ex.Message)
        End Try

        de.Close()
        lblStatus.Text = "Changes have been saved"
        btnSave.Enabled = False
        btnEdit.Enabled = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        DoSave()
        m_ChangesMade = False
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

        LayoutSettings = gFunctions.MySection(gFunctions.InteractiveUserUsername, "ConsolePhoneDetailFields")

        Settings = LayoutSettings.Keys
        Values = LayoutSettings.Values

        For Each Setting As String In LayoutSettings.Values
            Elements = Split(Setting, ",")
            Select Case Elements(0).ToLower
                Case "label"
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
                    Me.pnlDetails.Controls.Add(NewLabel)
                Case "textbox"
                    ' 0 = Type
                    ' 1 = Name
                    ' 2 = value
                    ' 3 = x position
                    ' 4 = y position
                    ' 5 = width
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
                    Dim AttributeStatus As String = gFunctions.ValueFromSetting(gFunctions.LoadConfigSetting("PhoneAttributes", Elements(1)), 2)

                    If AttributeStatus.ToLower = "mandatory" Then
                        NewTextbox.BackColor = System.Drawing.ColorTranslator.FromHtml(gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings)))
                    End If

                    Me.pnlDetails.Controls.Add(NewTextbox)

                    AddHandler NewTextbox.TextChanged, AddressOf Text_TextChanged
            End Select
            Index += 1
        Next


    End Sub

    Private Sub frmPhoneDetails_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case Chr(e.KeyCode)
            Case Convert.ToChar(113) ' F2
                AllowEdits()
            Case Convert.ToChar(27) ' Esc
                Me.Close()
        End Select
    End Sub

    Private Sub frmPhoneDetails_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Debug.Print("Details Form Size: " & Me.Width & "," & Me.Height)
    End Sub

    Private Sub DoDelete()
        Dim Phone As DirectoryEntry = New DirectoryEntry(m_PhonePath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim Parent As DirectoryEntry = New DirectoryEntry(Phone.Parent.Path, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        Parent.Children.Remove(Phone)
        Parent.Close()
        Parent = Nothing
        Phone = Nothing
        MsgBox("Operation Complete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Delete Phone")
        ' TODO:  Remove the specific number only.  i.e. cater for the occasion where multiple numbers exist in the list
        Try
            ParentPhonesForm.lstPhones.Items.Remove(Phonename)

        Catch

        End Try

        Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim Result As DialogResult = MessageBox.Show("Are you sure you want to delete this Phone?", "Confirmation required", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If Result = Windows.Forms.DialogResult.Yes Then
            DoDelete()

        Else
            MsgBox("Operation Cancelled", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Delete Phone")
        End If
    End Sub

    Private Sub btnUnassign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnassign.Click
        Dim Result As DialogResult = MessageBox.Show("Are you sure you want to unassign this Phone?", "Confirmation required", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If Result = Windows.Forms.DialogResult.Yes Then
            DoUnassign()
        Else
            MsgBox("Operation Cancelled", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Unassign Phone")
        End If
    End Sub

    Private Sub DoUnassign()
        Dim Phone As DirectoryEntry = New DirectoryEntry(m_PhonePath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        Phone.Properties("manager").Clear()

        Phone.CommitChanges()
        Phone.Close()
        Phone = Nothing

        MsgBox("Operation Complete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Unassign Phone")
        ParentPhonesForm.lstPhones.Items.Remove(Phonename)
        Close()
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
                    Return False
            End Select
        Else
            Return True
        End If
    End Function

    Private Sub frmPhoneDetails_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If DoClose() Then
            'Close()
        Else
            e.Cancel = True
        End If
    End Sub
End Class