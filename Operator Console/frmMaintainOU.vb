Imports System.DirectoryServices
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmMaintainOU


#Region " C# code to import directory into a treeview "

    'void Form1_Load(object sender, EventArgs e)
    '{
    '  // Open the Top Node of our Instance
    '  DirectoryEntry entry = new DirectoryEntry("LDAP://localhost:20389/cn=MSDN");
    '
    '  // Add the entry to the top of the tree
    '  TreeNode node = treeView1.Nodes.Add(entry.Name);
    '
    '  // Add the DirectoryEntry to the tag
    '  node.Tag = entry;
    '  
    '  // Add all the subnodes
    '  AddEntries(entry, node);
    '  
    '}
    '
    'void AddEntries(DirectoryEntry entry, TreeNode node)
    '{
    '  // Go through all children of the entry
    '  foreach (DirectoryEntry subentry in entry.Children)
    '  {
    '    // Add the new subnodes to the TreeView
    '    TreeNode subnode = node.Nodes.Add(subentry.Name);
    '    
    '    // Add the DirectoryEntry to the tag of the node
    '    subnode.Tag = subentry;
    '
    '    // Recursively Call this method
    '    AddEntries(subentry, subnode);
    '  }
    '}


#End Region

#Region " Constructor "


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal UserRoot As String, ByVal PhoneRoot As String)
        InitializeComponent()

        m_UserRoot = UserRoot
        m_PhoneRoot = PhoneRoot

    End Sub
#End Region
    Private m_PhoneRoot As String = ""
    Private m_UserRoot As String = ""

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

    Private Function ConnectToUserDirectory() As Int32
        If m_UserRoot <> "" Then
            ' Connect to AD
            Dim Root As DirectoryEntry

            tvwDirectory.Nodes.Clear()

            Try

                Root = New DirectoryEntry(m_UserRoot, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
                Root.RefreshCache()

                ' Add a node as the root
                Dim RootLevel As New TreeNode
                RootLevel.Tag = gFunctions.TargetDirectoryUserRootDN
                RootLevel.Text = "User Hierarchy Root"

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

    Private Sub AddOU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddOU.Click
        If Not tvwDirectory.SelectedNode Is Nothing Then
            tvwDirectory.SelectedNode.Expand()
            tvwDirectory.Refresh()

            Dim NewOUForm As New frmNewOU

            NewOUForm.OUParentPath = tvwDirectory.SelectedNode.Tag
            NewOUForm.ParentOUName = tvwDirectory.SelectedNode.Text
            NewOUForm.ParentTreeNode = tvwDirectory.SelectedNode
            NewOUForm.ShowDialog()
            FillNodes(tvwDirectory.SelectedNode)
            tvwDirectory.SelectedNode.Expand()
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub DeleteOU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteOU.Click
        Dim MessageResult As DialogResult = MessageBox.Show("Delete " & tvwDirectory.SelectedNode.Text & " ?", "Confirmation required", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        Dim DoDelete As Boolean = False

        If MessageResult = Windows.Forms.DialogResult.Yes Then
            Dim ThisOU As New DirectoryServices.DirectoryEntry(tvwDirectory.SelectedNode.Tag, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

            Dim Searcher As New DirectorySearcher(ThisOU)
            Dim Result As SearchResult

            Searcher.SearchScope = SearchScope.OneLevel
            Searcher.Filter = ("(objectClass=*)")
            Searcher.SizeLimit = 1

            Result = Searcher.FindOne()

            If Not Result Is Nothing Then
                'Dim DeleteResult As DialogResult = MessageBox.Show("This OU is not empty!  Please confirm you wish to DELETE this OU and all Users and OU's within it.", "Confirmation required", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                Dim DeleteResult As DialogResult = MessageBox.Show("This OU is not empty!  You cannot delete this OU.", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                If DeleteResult = Windows.Forms.DialogResult.Yes Then
                    Dim ConfirmationResult As DialogResult = MessageBox.Show("Are you sure?  This operation cannot be undone.", "Confirmation required", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                    If ConfirmationResult = Windows.Forms.DialogResult.Yes Then
                        DoDelete = True
                    Else
                        DoDelete = False
                    End If
                Else
                    DoDelete = False
                End If
            Else ' No results found
                DoDelete = True
            End If

            If DoDelete Then
                Dim OU As DirectoryEntry = New DirectoryEntry(tvwDirectory.SelectedNode.Tag, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

                OU.DeleteTree()
                OU.CommitChanges()
                MsgBox("Operation Complete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Delete OU")
                tvwDirectory.SelectedNode.Remove()
                FillNodes(tvwDirectory.SelectedNode.Parent)
            Else
                MsgBox("Operation Cancelled", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Delete OU")
            End If
        Else
            MsgBox("Operation Cancelled", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Delete OU")
        End If

    End Sub

    'Private Function ConnectToSites() As Int32
    '    If m_PhoneRoot <> "" Then
    '        ' Connect to AD
    '        Dim Root As DirectoryEntry

    '        tvwSites.Nodes.Clear()
    '        lblNoSitesDefined.Visible = True

    '        Try
    '            Root = New DirectoryEntry(m_PhoneRoot, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
    '            Root.RefreshCache()

    '            ' Now populate the treeview
    '            FillDirectoryTreeView(Me.tvwSites, Root)

    '        Catch ex As Exception
    '            'Debug.Print(ex.Message)
    '            Select Case ex.Message.Trim
    '                Case "The server is not operational."
    '                    Return gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.SERVER_NOT_FOUND)
    '                Case Else
    '                    Return gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
    '            End Select
    '        End Try
    '    Else
    '        ' TODO:  Get Directory path
    '    End If
    'End Function

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

            txtSelectedNode.Text = Path
            'Console.WriteLine(tvwDirectory.SelectedNode.Text)
        End If

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub frmMaintainOU_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Result As Int32

        lblStatus.Text = "Connecting to User OU's..."
        Me.Show()
        Me.Refresh()
        Result = ConnectToUserDirectory()
        If gFunctions.LastStatus.CADS_ERRORCODE = Utility.Types.ERRORTYPE.SUCCESS Then
            lblStatus.Text = "Idle"

            'If Result = SUCCESS Then
            '    lblStatus.Text = "Connecting to Sites..."
            '    Me.Refresh()
            '    'Result = ConnectToSites()
            '    If Result = SUCCESS Then
            '        lblStatus.Text = "Idle"
            '    Else
            '        lblStatus.Text = gFunctions.GetErrorTypeAndCode(Result)
            '    End If
            'Else
            '    lblStatus.Text = gFunctions.GetErrorTypeAndCode(Result)
            'End If
        Else
            'MsgBox("boom!")
        End If
    End Sub

    'Private Sub AddNode(ByVal DirEntry As DirectoryEntry, ByRef TreeOrTreeView As Object, ByRef NewNode As TreeNode)
    '    Dim SystemObjectTypes As New Collections.Specialized.StringCollection
    '    Dim NeverAllowedObjects As New Collections.Specialized.StringCollection
    '    Dim AddNode As Boolean = False

    '    'TODO:  Place these object names into the application configuration (external?)
    '    'NeverAllowedObjects.Add("Builtin")
    '    'NeverAllowedObjects.Add("Computers")
    '    NeverAllowedObjects.Add("Domain Controllers")
    '    NeverAllowedObjects.Add("ForeignSecurityPrincipals")
    '    NeverAllowedObjects.Add("Program Data")
    '    NeverAllowedObjects.Add("System")
    '    NeverAllowedObjects.Add("Microsoft Exchange System Objects")
    '    NeverAllowedObjects.Add("LostAndFound")
    '    NeverAllowedObjects.Add("Infrastructure")
    '    NeverAllowedObjects.Add("DomainUpdates")
    '    NeverAllowedObjects.Add("Operations")
    '    NeverAllowedObjects.Add("DomainUpdates")
    '    NeverAllowedObjects.Add("ComPartitionSets")
    '    NeverAllowedObjects.Add("ComPartitions")
    '    NeverAllowedObjects.Add("AdminSDHolder")
    '    NeverAllowedObjects.Add("Meetings")
    '    NeverAllowedObjects.Add("Policies")
    '    NeverAllowedObjects.Add("WinsockServices")
    '    NeverAllowedObjects.Add("WMIPolicy")
    '    NeverAllowedObjects.Add("user")
    '    NeverAllowedObjects.Add("group")
    '    NeverAllowedObjects.Add("contact")
    '    NeverAllowedObjects.Add("computer")

    '    'TODO:  Place these object types into the application configuration (external?)
    '    SystemObjectTypes.Add("mSMQConfiguration")
    '    SystemObjectTypes.Add("serviceConnectionPoint")
    '    SystemObjectTypes.Add("mSMQConfiguration")
    '    SystemObjectTypes.Add("printQueue")
    '    SystemObjectTypes.Add("nTFRSSubscriptions")
    '    SystemObjectTypes.Add("nTFRSSubscriber")
    '    SystemObjectTypes.Add("rIDSet")
    '    SystemObjectTypes.Add("rRASAdministrationConnectionPoint")
    '    SystemObjectTypes.Add("foreignSecurityPrincipal")
    '    SystemObjectTypes.Add("publicFolder")
    '    SystemObjectTypes.Add("domainPolicy")
    '    SystemObjectTypes.Add("classStore")
    '    SystemObjectTypes.Add("dfsConfiguration")
    '    SystemObjectTypes.Add("nTFRSSettings")
    '    SystemObjectTypes.Add("fileLinkTracking")
    '    SystemObjectTypes.Add("rIDManager")
    '    SystemObjectTypes.Add("samServer")
    '    SystemObjectTypes.Add("msWMI-Som")
    '    SystemObjectTypes.Add("rpcContainer")
    '    SystemObjectTypes.Add("nTFRSReplicaSet")
    '    SystemObjectTypes.Add("nTFRSMember")
    '    SystemObjectTypes.Add("infrastructureUpdate")
    '    SystemObjectTypes.Add("builtinDomain")
    '    SystemObjectTypes.Add("groupPolicyContainer")
    '    SystemObjectTypes.Add("msExchSystemObjectsContainer")
    '    SystemObjectTypes.Add("lostAndFound")
    '    SystemObjectTypes.Add("builtinDomain")

    '    ' Work out if this object belongs in our view of the directory tree
    '    If chkHideSystemBranches.Checked Then
    '        ' Discount certain object types
    '        If Not NeverAllowedObjects.Contains(NewNode.Text) And _
    '           Not NeverAllowedObjects.Contains(DirEntry.SchemaClassName) And _
    '           Not SystemObjectTypes.Contains(DirEntry.SchemaClassName) Then
    '            AddNode = True
    '        End If
    '    Else
    '        ' We still don't want to see certain objects, so check that
    '        If Not NeverAllowedObjects.Contains(NewNode.Text) And _
    '           Not NeverAllowedObjects.Contains(DirEntry.SchemaClassName) Then
    '            AddNode = True
    '        End If
    '    End If

    '    ' Now if appropriate, add the object to our tree
    '    If AddNode Then

    '    End If

    '    NeverAllowedObjects = Nothing
    '    SystemObjectTypes = Nothing
    'End Sub

    Private Sub tabConfig_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabConfig.SelectedIndexChanged
        Select Case tabConfig.SelectedIndex
            Case 0
                ' OU's
            Case 1
                ' Sites
        End Select
    End Sub

    Private Sub tvwDirectory_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwDirectory.AfterExpand
        Dim ThisNode As TreeNode = e.Node

        If ThisNode.Nodes.ContainsKey("__CADSDummyNode") Then
            ThisNode.Nodes("__CADSDummyNode").Remove()
        End If
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

    Private Sub tvwDirectory_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwDirectory.AfterSelect
        FillNodes(e.Node)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub MoveOU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveOU.Click
        MsgBox("Sorry, your current access rights do not allow you to perform this operation.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Information")
    End Sub

    Private Sub RenameOUToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameOUToolStripMenuItem.Click
        MsgBox("Sorry, your current access rights do not allow you to perform this operation.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Information")
    End Sub

End Class