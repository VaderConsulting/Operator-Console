Imports System.DirectoryServices
Imports Utility
Imports Utility.Functions
Imports Utility.Constants
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmOUSearch

    Private m_OU() As String = {}
    Private m_ObjectRoot As String = ""
    Public OUPath As String = ""
    Public UseUserRoot As Boolean = True
    Private m_UserRoot As String = ""
    Private m_PhoneRoot As String = ""

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal UserRoot As String, ByVal PhoneRoot As String)
        InitializeComponent()

        m_UserRoot = UserRoot
        m_PhoneRoot = PhoneRoot

        If UseUserRoot Then
            m_ObjectRoot = m_UserRoot
        Else
            m_ObjectRoot = m_PhoneRoot
        End If

    End Sub

    Private Function CheckForChildren(ByVal Entry As DirectoryEntry) As Boolean
        Dim Searcher As New DirectorySearcher(Entry)

        Searcher.SearchScope = SearchScope.OneLevel
        Searcher.Filter = "(|(objectClass=container)(objectClass=organizationalUnit))"
        Searcher.SizeLimit = 200000

        For Each Child As SearchResult In Searcher.FindAll
            Return True
        Next

        Return False
    End Function

    Private Function ConnectToDirectory() As Int32
        If m_ObjectRoot <> "" Then
            ' Connect to AD
            Dim Root As DirectoryEntry

            tvwDirectory.Nodes.Clear()

            Try

                Root = New DirectoryEntry(m_ObjectRoot, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
                Root.RefreshCache()

                ' Add a node as the root
                Dim RootLevel As New TreeNode
                RootLevel.Tag = m_ObjectRoot 'gFunctions.TargetDirectoryUserRootDN
                If UseUserRoot Then
                    RootLevel.Text = "User Hierarchy Root"
                Else
                    RootLevel.Text = "Phone Hierarchy Root"
                End If

                tvwDirectory.Nodes.Add(RootLevel)
                ' Now populate the treeview
                FillDirectoryTreeView(RootLevel, Root)
            Catch ex As Exception
                'Debug.Print(ex.Message)
                Select Case ex.Message.Trim
                    Case "The server is not operational."
                        Return gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.SERVER_NOT_FOUND)
                    Case Else
                        Return gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
                End Select
            End Try
        Else
            ' TODO:  Get Directory path
        End If
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If lstOU.SelectedIndex >= 0 Then

            OUPath = m_OU(lstOU.SelectedIndex)
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Close()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.None
            Close()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        DoOUSearch()
    End Sub

    Private Sub DoOUSearch()
        Dim SearchLimit As String
        Dim SearchLimitRawData As String

        lstOU.Items.Clear()
        tvwSelected.Nodes.Clear()

        Cursor.Current = Cursors.WaitCursor

        SearchLimitRawData = gFunctions.CollectionValue("SearchLimit", gSettings).Substring(0, 3)
        If SearchLimitRawData.Contains("|") Then
            SearchLimit = SearchLimitRawData.Substring(0, SearchLimitRawData.IndexOf("|"))
        Else
            SearchLimit = SearchLimitRawData
        End If

        Try
            ' TODO:  This binds to a server, which is ok for ADAM, but possibly not for AD
            Dim TheServerName As String = gFunctions.CurrentDirectoryServer

            If Not TheServerName.StartsWith("LDAP://") Then
                TheServerName = "LDAP://" & TheServerName
            End If

            If Not TheServerName.EndsWith("/") Then
                TheServerName &= "/"
            End If

            Dim root As DirectoryServices.DirectoryEntry

            If UseUserRoot Then
                root = New DirectoryServices.DirectoryEntry(TheServerName & gFunctions.CollectionValue("DirectoryUserRoot", gSettings), gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
            Else
                root = New DirectoryServices.DirectoryEntry(TheServerName & gFunctions.CollectionValue("DirectoryPhoneRoot", gSettings), gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
            End If

            Dim rootSearch As New DirectorySearcher(root)
            Dim SearchResult As SearchResult
            Dim results As SearchResultCollection

            rootSearch.PropertiesToLoad.Add("description")
            rootSearch.PropertiesToLoad.Add("name")
            rootSearch.PropertiesToLoad.Add("path")
            rootSearch.Sort.PropertyName = "name"
            rootSearch.Sort.Direction = SortDirection.Ascending
            rootSearch.CacheResults = True

            If radCode.Checked Then rootSearch.Filter = "(&(ObjectClass=organizationalUnit)(description=" & Me.txtSearch.Text & "*))"
            If radName.Checked Then rootSearch.Filter = "(&(ObjectClass=organizationalUnit)(name=" & Me.txtSearch.Text & "*))"

            rootSearch.SizeLimit = SearchLimit
            rootSearch.CacheResults = True

            results = rootSearch.FindAll

            Dim OUCount As Integer

            For Each SearchResult In results
                Try
                    ReDim Preserve m_OU(OUCount)

                    Dim Description As String = "[No Code]"
                    Dim Name As String = ""

                    Try
                        Description = SearchResult.Properties("description")(0).ToString
                    Catch
                    End Try

                    Try
                        Name = SearchResult.Properties("name")(0).ToString
                    Catch
                    End Try

                    lstOU.Items.Add(Name & " (" & Description & ")")
                    m_OU(OUCount) = SearchResult.Path
                Catch ex As Exception
                    'lblStatus.Text = ex.Message.ToString
                End Try

                OUCount += 1
            Next

            Me.Refresh()

            gFunctions.LastStatus.Clear()
            gFunctions.LastStatus.CADS_ERRORCODE = SUCCESS
        Catch ex As Exception
            Select Case ex.Message.Trim
                Case "The server is not operational."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.SERVER_NOT_FOUND)
                Case "Logon failure: unknown user name or bad password."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.SECURITY, ERROR_SECURITY.AUTHENTICATION_FAILURE)
                Case "A local error has occurred."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
                Case "An operations error occurred."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
                Case "The time limit for this request was exceeded."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.REQUEST_TIMEOUT)
                Case Else
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
            End Select
        End Try

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub FillDirectoryTreeView(ByVal TreeOrNode As Object, ByVal Directory As DirectoryEntry)
        Dim Searcher As New DirectorySearcher(Directory)

        Searcher.SearchScope = SearchScope.OneLevel
        Searcher.Filter = "(|(objectClass=container)(objectClass=organizationalUnit))"

        For Each Result As SearchResult In Searcher.FindAll
            Dim Entry As DirectoryEntry = Result.GetDirectoryEntry
            Dim Children As DirectoryEntries = Entry.Children
            Dim EntryHasChildNodes As Boolean = CheckForChildren(Entry)
            Dim Node As New TreeNode
            Dim Nodetext As String = Entry.Name.Remove(0, 3) '  Remove "CN="

            ' Remove "No OU's" message
            'If TreeOrNode.Name = tvwDirectory.Name Then lblNoOUDefined.Visible = False

            Node.ImageIndex = 0
            Node.SelectedImageIndex = 1
            Node.Text = Nodetext
            Node.Tag = Entry.Path

            If EntryHasChildNodes Then
                Dim DummyNode As TreeNode = Node.Nodes.Add("__CADSDummyNode", "__CADSDummyNode")
            End If

            TreeOrNode.Nodes.Add(Node)
        Next

    End Sub

    Private Sub FillNodes(ByVal TreeviewNode As TreeNode)
        Dim ThisNode As TreeNode = TreeviewNode 'tvwDirectory.SelectedNode
        Cursor.Current = Cursors.WaitCursor

        If ThisNode.Tag = Nothing Then ' tvwDirectory.SelectedNode.Tag = Nothing Then
        Else
            Dim Path As String = ThisNode.Tag 'tvwDirectory.SelectedNode.Tag
            Dim Entry As DirectoryEntry = New DirectoryEntry(Path, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
            If CheckForChildren(Entry) Then
                ThisNode.Nodes.Clear()
                FillDirectoryTreeView(ThisNode, Entry) '(tvwDirectory.SelectedNode, Entry)
            End If

            'txtSelectedNode.Text = Path
            'Console.WriteLine(tvwDirectory.SelectedNode.Text)
        End If

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub frmOUSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        Me.Show()
        Me.Refresh()

        If UseUserRoot Then
            m_ObjectRoot = m_UserRoot
        Else
            m_ObjectRoot = m_PhoneRoot
        End If

        ConnectToDirectory()
        txtSearch.Focus()

        tabOUSearch.TabPages.RemoveAt(1)
    End Sub

    Private Sub tvwDirectory_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwDirectory.AfterExpand
        Dim ThisNode As TreeNode = e.Node

        If ThisNode.Nodes.ContainsKey("__CADSDummyNode") Then
            ThisNode.Nodes("__CADSDummyNode").Remove()
        End If
        FillNodes(e.Node)
    End Sub

    Private Sub tvwDirectory_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwDirectory.AfterSelect
        FillNodes(e.Node)
    End Sub

    Private Sub tvwDirectory_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvwDirectory.AfterLabelEdit
        ' Go through each existing node and ensure there isn't one by this name
        Dim DuplicateNode As Boolean = False
        Dim ParentNode As TreeNode = e.Node.Parent

        If e.Node.Text.Length > 0 Then
            If Not ParentNode Is Nothing Then
                For Each ChildNode As TreeNode In ParentNode.Nodes
                    If ChildNode.Handle <> e.Node.Handle Then
                        If ChildNode.Text.ToLower = e.Node.Text.ToLower Then
                            'tvwDirectory.LabelEdit = True
                            'e.Node.BeginEdit()
                            e.CancelEdit = True
                            'DuplicateNode = True
                        End If
                    End If
                Next
            End If
        Else
            MsgBox("The OU name cannot be blank", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Validation error")
            e.CancelEdit = True
        End If
    End Sub

    Private Sub BuildTreeview(ByVal ObjectPath As String)
        'If Not ObjectPath.Contains("OU") Then Exit Sub
        'Dim MyUser As New DirectoryEntry(ObjectPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        'Dim Path As String()

        'Dim CommonPath As String = ""

        'If UseUserRoot Then
        '    CommonPath = gFunctions.TargetDirectoryUserRootDN.Substring(gFunctions.TargetDirectoryUserRootDN.IndexOf("=") - 2)
        'Else
        '    CommonPath = gFunctions.TargetDirectoryPhoneRootDN.Substring(gFunctions.TargetDirectoryPhoneRootDN.IndexOf("=") - 2)
        'End If

        'Dim CurrentParentPath As String = ""
        ''Dim TempParentOUPath As String()
        'Dim ParentOUPaths As New Collections.Specialized.NameValueCollection
        ''Dim ParentOUPath As String()

        'ObjectPath = Replace(ObjectPath.ToUpper, CommonPath.ToUpper, "")

        'If ObjectPath.EndsWith(",") Then ObjectPath = ObjectPath.Substring(0, Len(ObjectPath) - 1)

        '' Example:  "LDAP://WKS01:389/CN=ADAIR ELIZABETH,OU=ENGADINE 1050,OU=ST GEORGE  SHIRE LOCAL MARKET,OU=SHARED SERVICES RETAIL & RURAL BANKING,OU=PERSONAL DIVISION"

        'Path = Split(ObjectPath, ",")

        'Dim Parent As DirectoryEntry = MyUser.Parent
        'Dim OULevelCount As Integer = 0
        ''Debug.Print("Parent: " & Parent.Path)

        'For i As Integer = 0 To Parent.Path.ToString.Length - 1
        '    If Parent.Path.Substring(i, 1) = "," Then OULevelCount += 1
        '    'Debug.Print(Parent.Path.Substring(0, i + 1) & " OULevels: " & OULevelCount)
        'Next

        ''Debug.Print("User Root: " & gFunctions.TargetDirectoryUserRootDN)
        '' OULevelCount now contains the number of hierarchical levels deep the User is.

        'Dim Index As Integer
        '' Changed for the OU Search function...
        '' Add the actual object
        'ParentOUPaths.Add(Index.ToString, ObjectPath)
        'Index += 1
        'OULevelCount += 1

        '' Retrieve the path of each OU above the User, until we get to the user Root

        'Dim TopLevelPath As String = ""
        'If UseUserRoot Then
        '    TopLevelPath = gFunctions.TargetDirectoryUserRootDN
        'Else
        '    TopLevelPath = gFunctions.TargetDirectoryPhoneRootDN
        'End If

        'Do Until (Parent.Path.ToLower = TopLevelPath.ToLower) Or (Parent.ToString.Contains("OU") = False)
        '    'Debug.Print("Parent: " & Parent.Path)
        '    ParentOUPaths.Add(Index.ToString, Parent.Path)
        '    Parent = Parent.Parent
        '    Index += 1
        'Loop

        '' Now to reorder the Parent Paths
        'Dim OULevelCountBelowObjectRoot = Index
        'Dim ParentOUPath(ParentOUPaths.Count - 1) As String
        'For i As Integer = 0 To ParentOUPaths.Count - 1
        '    ParentOUPath(i) = ParentOUPaths((OULevelCountBelowObjectRoot - i - 1))
        '    Debug.Print(ParentOUPath(i))
        'Next

        '' Now what we have is an array of OU paths for each OU above the user.
        '' The Description field of each OU may contain a code.

        '' Retrieve that code:
        'Dim OUCode(ParentOUPaths.Count - 1) As String
        'For i As Integer = 0 To ParentOUPaths.Count - 1
        '    Dim OU As New DirectoryEntry(ParentOUPath(i))

        '    Try
        '        ' Is it MultiValued?
        '        OUCode(i) = OU.Properties("Description")(0).ToString & ""
        '    Catch ex As Exception
        '        ' No description (code)
        '        OUCode(i) = "No Code"
        '    End Try
        '    OU.Close()
        '    OU = Nothing
        'Next

        'For i As Integer = UBound(Path) To 0 Step -1
        '    Path(i) = Path(i).Remove(0, Path(i).IndexOf("=") + 1)
        'Next

        '' remove everything before the 'CN='
        ''Path(0) = Path(0).Remove(0, Path(0).IndexOf("CN") + 3)

        '' Finally build the tree structure
        'Dim ParentNode As TreeNode
        '' Add the root node

        'ParentNode = tvwSelected.Nodes.Add(Path(UBound(Path)) & " (" & OUCode(0) & ")")

        '' Now add each child node
        'For i As Integer = UBound(Path) - 1 To 1 Step -1   ' For i As Integer = UBound(Path) - 1 To 1 Step -1
        '    Dim ChildNode As New TreeNode

        '    ParentNode = AddNode(ParentNode, Path(i) & " (" & OUCode(UBound(Path) - i) & ")")
        'Next

        'MyUser.Close()
        'MyUser = Nothing

        'tvwSelected.ExpandAll()

        Dim CommonPath As String = ""

        If UseUserRoot Then
            CommonPath = gFunctions.TargetDirectoryUserRootDN.Substring(gFunctions.TargetDirectoryUserRootDN.IndexOf("=") - 2)
        Else
            CommonPath = gFunctions.TargetDirectoryPhoneRootDN.Substring(gFunctions.TargetDirectoryPhoneRootDN.IndexOf("=") - 2)
        End If

        Dim MyObject As New DirectoryEntry(ObjectPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
        Dim Path As String()
        'Dim CommonPath As String = gFunctions.TargetDirectoryUserRootDN.Substring(gFunctions.TargetDirectoryUserRootDN.IndexOf("=") - 2)
        Dim CurrentParentPath As String = ""
        'Dim TempParentOUPath As String()
        Dim ParentOUPaths As New Collections.Specialized.NameValueCollection
        'Dim ParentOUPath As String()

        ObjectPath = Replace(ObjectPath.ToUpper, CommonPath.ToUpper, "")

        If ObjectPath.EndsWith(",") Then ObjectPath = ObjectPath.Substring(0, Len(ObjectPath) - 1)

        ' Example:  "LDAP://WKS01:389/CN=ADAIR ELIZABETH,OU=ENGADINE 1050,OU=ST GEORGE  SHIRE LOCAL MARKET,OU=SHARED SERVICES RETAIL & RURAL BANKING,OU=PERSONAL DIVISION"

        Path = Split(ObjectPath, ",")
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

        If UseUserRoot Then
            ComparisonPath = gFunctions.TargetDirectoryUserRootDN
        Else
            ComparisonPath = gFunctions.TargetDirectoryPhoneRootDN
        End If
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

        ' Now to reorder the Parent Paths
        'Dim OULevelCountBelowObjectRoot = Index - 1

        'For i As Integer = 1 To ParentOUPaths.Count Step -1
        'ParentOUPath(i) = ParentOUPaths(ParentOUPaths.Count - i)
        'Next

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

        'For i As Integer = UBound(Path) To 0 Step -1
        'Path(i) = Path(i).Remove(0, 3)
        'Debug.Print(Path(i))
        'Debug.Print(CurrentParentPath)
        'Next

        ' remove everything before the 'CN='
        'Path(0) = Path(0).Remove(0, Path(0).IndexOf("CN") + 3)

        'For i As Integer = UBound(Path) To 0 Step -1
        'Path(i) = Path(i).Remove(0, Path(i).IndexOf("=") + 1)
        'Next

        ' Finally build the tree structure
        Dim ParentNode As TreeNode
        ' Add the root node

        ParentNode = tvwSelected.Nodes.Add(Path(UBound(Path)) & " (" & OUCode(0) & ")")

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

    Private Sub lstOU_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstOU.SelectedIndexChanged
        tvwSelected.Nodes.Clear()
        If lstOU.SelectedIndex >= 0 Then
            BuildTreeview(m_OU(lstOU.SelectedIndex))
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

End Class