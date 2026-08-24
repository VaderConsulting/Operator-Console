Imports System.DirectoryServices
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmConfigMain

    Private m_UserRoot As String = ""
    Private m_PhoneRoot As String = ""

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

    Private Sub frmConfigMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Result As Int32

        lblStatus.Text = "Connecting to User OU's..."
        Me.Show()
        Me.Refresh()
        Result = ConnectToUserDirectory()

        If Result = SUCCESS Then
            lblStatus.Text = "Connecting to Sites..."
            Me.Refresh()
            Result = ConnectToSites()
            If Result = SUCCESS Then
                lblStatus.Text = "Idle"
            Else
                lblStatus.Text = gFunctions.GetErrorTypeAndCode(Result)
            End If
        Else
            lblStatus.Text = gFunctions.GetErrorTypeAndCode(Result)
        End If
    End Sub

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


    Private Function ConnectToUserDirectory() As Int32
        If m_UserRoot <> "" Then
            ' Connect to AD
            Dim Root As DirectoryEntry

            tvwDirectory.Nodes.Clear()
            lblNoOUDefined.Visible = True

            Try

                Root = New DirectoryEntry(m_UserRoot, TargetDirectoryConnectionUsername, TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
                Root.RefreshCache()

                ' Now populate the treeview
                FillDirectoryTreeView(Me.tvwDirectory, Root)

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

    Private Function ConnectToSites() As Int32
        If m_PhoneRoot <> "" Then
            ' Connect to AD
            Dim Root As DirectoryEntry

            tvwSites.Nodes.Clear()
            lblNoSitesDefined.Visible = True

            Try
                Root = New DirectoryEntry(m_PhoneRoot, TargetDirectoryConnectionUsername, TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
                Root.RefreshCache()

                ' Now populate the treeview
                FillDirectoryTreeView(Me.tvwSites, Root)

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

    Private Sub FillDirectoryTreeView(ByVal TreeOrNode As Object, ByVal Directory As DirectoryEntry, Optional ByVal SingleEntry As Boolean = False)
        Dim Searcher As New DirectorySearcher(Directory)

        Searcher.SearchScope = SearchScope.OneLevel

        For Each Result As SearchResult In Searcher.FindAll
            Dim Entry As DirectoryEntry = Result.GetDirectoryEntry
            Dim Children As DirectoryEntries = Entry.Children
            Dim EntryHasChildNodes As Boolean = CheckForChildren(Entry)
            Dim Node As New TreeNode
            Dim Nodetext As String = Entry.Name.Remove(0, 3) '  Remove "CN="

            Console.WriteLine("TabIndex: " & tabConfig.SelectedIndex)

            ' Remove "No OU's" or "No Sites" message
            If TreeOrNode.Name = tvwDirectory.Name Then lblNoOUDefined.Visible = False
            If TreeOrNode.Name = tvwSites.Name Then lblNoSitesDefined.Visible = False

            If EntryHasChildNodes Then
                ' TODO:  Get one child entry so that the plus is shown on the treeview
                'FillDirectoryTreeView(Node, Entry, True)
                ' Set plus icon
                Node.ImageIndex = 4
                Node.SelectedImageIndex = 3
            Else
                ' set icon without plus
                Node.ImageIndex = 0
                Node.SelectedImageIndex = 2
            End If
            Node.Text = Nodetext
            Node.Tag = Entry.Path
            TreeOrNode.Nodes.Add(Node)
            'If SingleEntry Then Exit Sub
        Next

    End Sub

    Private Function CheckForChildren(ByVal Entry As DirectoryEntry) As Boolean
        Dim Searcher As New DirectorySearcher(Entry)

        Searcher.SearchScope = SearchScope.OneLevel
        Searcher.Filter = "(|(objectClass=container)(objectClass=organizationalUnit))"

        For Each Child As SearchResult In Searcher.FindAll
            Return True
        Next

        Return False
    End Function

    Private Sub tvwDirectory_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwDirectory.AfterSelect
        Dim ThisNode As TreeNode = tvwDirectory.SelectedNode
        Cursor.Current = Cursors.WaitCursor

        If tvwDirectory.SelectedNode.Tag = Nothing Then
        Else
            Dim Path As String = tvwDirectory.SelectedNode.Tag
            Dim Entry As DirectoryEntry = New DirectoryEntry(Path, TargetDirectoryConnectionUsername, TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
            If CheckForChildren(Entry) Then
                ThisNode.Nodes.Clear()
                FillDirectoryTreeView(tvwDirectory.SelectedNode, Entry)
            End If

            txtSelectedNode.Text = Path
            Console.WriteLine(tvwDirectory.SelectedNode.Text)
        End If

        Cursor.Current = Cursors.Default

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

End Class