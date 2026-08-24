Imports System.DirectoryServices
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE


Public Class frmSearch

    Private m_LoadComplete As Boolean = False
    Private SearchFields As Collections.Specialized.StringCollection = My.Settings.SearchFields
    Private DisplayNames As String() = {}
    Private LDAPFields As String() = {}
    Private m_SearchFilter As String = "(objectClass=user)"
    Private m_SearchPrefix As String = ""
    Private m_SearchText() As String = {}
    Private m_GridContents As DataView
    Private m_GridSortOrder As New Collections.Specialized.NameValueCollection
    Private m_Functions As New Functions
    Public CanBeClosed As Boolean = False

#Region " GUI Elements "

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        
    End Sub

    Private Sub frmSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Result As Int32 = 0

        Me.Show()
        Me.Refresh()

        m_GridSortOrder.Add("1", "Name ASC")

        DirectoryConnection = m_Functions.GetDirectoryConnection(gSearchRoot)

        If LastStatus.CADS_ERRORCODE > SUCCESS Then
            Select Case LastStatus.LastException.Message
                Case ""
                    lblStatus.Image = frmConsoleParent.imlGUI.Images(1) '  Ok
                    lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                Case "The server is not operational."
                    Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                    Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                Case "Logon failure: unknown user name or bad password."
                    Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                    Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                Case "A local error has occurred."
                    Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                    Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                Case Else
                    Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                    Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            End Select
        End If

        Result = DoSearch()

        If Result = SUCCESS Then
            Try
                Me.pnlSearch.Controls("searchText0").Focus()
                Me.pnlSearch.Controls("SearchText0").BackColor = My.Settings.SearchTextboxColour
            Catch ex As Exception
                lblStatus.Text = "Error accessing textbox 'SearchText0'"
            End Try

            Me.grdDirectory.AlternatingRowsDefaultCellStyle.BackColor = My.Settings.SearchAlternatingRowBackColour
        Else

        End If

        ' Set a minimum form size, according to the number of columns
        Dim MinimumRequiredScreenWidth As Point = New Point(grdDirectory.ColumnCount * 57, 200)

        If My.Computer.Screen.Bounds.Width < MinimumRequiredScreenWidth.X Then
            MsgBox("Your primary screen video resolution is set too low.  You cannot display the search results." & vbCrLf & vbCrLf & _
                   "Correct your screen resolution or modify your Operator Console configuration to correct this problem.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Configuration error")
            Application.Exit()
        End If

        m_LoadComplete = True
    End Sub

    Private Sub grdDirectory_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDirectory.CellContentDoubleClick
        ShowDetailsForm()
    End Sub

    Private Sub pnlSearch_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlSearch.Resize
        ' Handles the form_maximize event
        If Not m_LoadComplete Then Exit Sub
        RedrawControls()
    End Sub

    Private Sub frmSearch_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp

        If e.KeyValue = 13 Then
            ShowDetailsForm()
            e.SuppressKeyPress = True
        End If

        If e.Shift Then ' They have the Shift key pressed, so this is a shortcut key
            For i As Int16 = 0 To grdDirectory.ColumnCount - 2
                If e.KeyValue = i + 112 Then
                    Try
                        Me.pnlSearch.Controls("SearchText" & i.ToString).Focus()
                        Me.pnlSearch.Controls("SearchText" & i.ToString).BackColor = My.Settings.SearchTextboxColour
                        e.SuppressKeyPress = True
                    Catch ex As Exception

                    End Try
                Else
                    Try
                        Me.pnlSearch.Controls("SearchText" & i.ToString).BackColor = Color.White
                    Catch ex As Exception
                    End Try
                End If
            Next
        End If
    End Sub

    Private Sub frmSearch_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not CanBeClosed Then ' The form may be allowed to closed, so check first
            If e.CloseReason = CloseReason.UserClosing Then
                If Me.ParentForm.MdiChildren.Count = 1 Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub tmrSearch_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearch.Tick
        tmrSearch.Enabled = False
        ' Which textbox has the focus?
        DoSearch()
    End Sub

    Private Sub frmSearch_ResizeEnd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd
        If Not m_LoadComplete Then Exit Sub
        RedrawControls()
    End Sub

    Private Sub grdDirectory_ColumnWidthChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdDirectory.ColumnWidthChanged
        If Not m_LoadComplete Then Exit Sub

        ' Because this event is spawned so many times, we will only selectively redraw the controls
        If e.Column.Index = grdDirectory.ColumnCount - 2 Then
            RedrawControls()
        End If
    End Sub

    Private Sub btnClearSortOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSortOrder.Click
        m_GridSortOrder.Clear()
        GetSortedData()
    End Sub

    Private Sub frmSearch_Move(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Move
        Me.tmrRefresh.Enabled = False
        Me.tmrRefresh.Interval = 500
        Me.tmrRefresh.Enabled = True
    End Sub

    Private Sub tmrRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRefresh.Tick
        Me.ParentForm.Refresh()
        Me.tmrRefresh.Enabled = False
    End Sub

    Private Sub mnuExportSearchResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportSearchResults.Click
        ' TODO: Export search results
        MsgBox("TODO: Export results")
    End Sub

    Private Sub SearchTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txtbox As TextBox
        Dim TextboxIndex As Int16 = 0
        Dim SearchText As String = ""

        Me.tmrSearch.Enabled = False
        Me.tmrSearch.Interval = My.Settings.SearchPauseTime

        txtbox = CType(sender, TextBox)

        SearchText = txtbox.Text

        ' No matter which textbox we have changed, the AD or ADAM Attribute name is contained within the Tag property.

        ' The search string will be made up of a combination of all of the text entered into all of the search boxes.

        ' Which textbox did we change?
        TextboxIndex = CInt(txtbox.Name.Replace("SearchText", "")) ' 6 = "SearchText".Length

        If SearchText.Trim.Length > 0 Then
            m_SearchText(TextboxIndex) = "(" & txtbox.Tag & "=" & SearchText & "*)"
        Else
            m_SearchText(TextboxIndex) = ""
        End If

        BuildSearchString()

        Me.tmrSearch.Enabled = True
    End Sub

    Private Sub TextFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txtbox As TextBox

        txtbox = CType(sender, TextBox)

        txtbox.SelectAll()
        txtbox.BackColor = My.Settings.SearchTextboxColour
    End Sub

    Private Sub TextLostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txtbox As TextBox

        txtbox = CType(sender, TextBox)

        txtbox.BackColor = Color.White
    End Sub

    Private Sub AscendingClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim CommandButton As Button
        Dim ButtonIndex = 0
        Dim SortText As String = ""

        CommandButton = CType(sender, Button)
        ButtonIndex = CInt(CommandButton.Name.Replace("btnSortAsc", "")) ' 10 = "btnSortAsc".Length
        'Debug.Print("Ascending " & ButtonIndex & " clicked")
        'm_GridSortOrder.Add(Me.pnlSearch.Controls("SearchText" & ButtonIndex).Tag & " ASC")
        SortText = Me.grdDirectory.Columns(ButtonIndex).Name & " ASC"

        ' Only add this field if it isn't already there
        If Not m_GridSortOrder.AllKeys.Contains(ButtonIndex.ToString) Then
            m_GridSortOrder.Add(ButtonIndex.ToString, SortText)
            GetSortedData()
        End If

    End Sub

    Private Sub DescendingClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim CommandButton As Button
        Dim ButtonIndex = 0
        Dim SortText As String = ""

        CommandButton = CType(sender, Button)
        ButtonIndex = CInt(CommandButton.Name.Replace("btnSortDesc", "")) ' 10 = "btnSortAsc".Length
        'Debug.Print("Descending " & ButtonIndex & " clicked")
        'm_GridSortOrder.Add(Me.pnlSearch.Controls("SearchText" & ButtonIndex).Tag & " DESC")
        SortText = Me.grdDirectory.Columns(ButtonIndex).Name & " DESC"

        ' Only add this field if it isn't already there
        If Not m_GridSortOrder.AllKeys.Contains(ButtonIndex.ToString) Then
            m_GridSortOrder.Add(ButtonIndex.ToString, SortText)
            GetSortedData()
        End If

    End Sub

    Private Sub KeyPressed(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        Dim txtbox As TextBox
        Dim i As Int16

        txtbox = CType(sender, TextBox)

        Select Case e.KeyCode
            Case 9 ' Tab key
                'txtbox.BackColor = Color.Yellow
            Case 27 ' Esc key
                For i = 0 To grdDirectory.Columns.Count - 2
                    Dim oTextbox As TextBox = Me.pnlSearch.Controls("SearchText" & i.ToString)
                    oTextbox.Text = ""
                    Me.lblStatus.Text = "Search text cleared"
                Next
        End Select

    End Sub

#End Region

    Private Function DoSearch() As Int32
        Dim Result As Int32

        Cursor.Current = Cursors.WaitCursor

        Result = GetSortedData()
        PlaceRuntimeControls()

        Cursor.Current = Cursors.Default
        Return Result

    End Function

    Private Function GetSortedData() As Int32
        Dim SortOrder As String = ""
        Dim Result As Int32
        Dim searchFilter As String = "(" & m_SearchPrefix & m_SearchFilter & ")"

        Result = CreateDataSource(searchFilter, m_GridContents)

        grdDirectory.DataSource = m_GridContents
        grdDirectory.Refresh()

        If Result = SUCCESS Then
            If grdDirectory.RowCount = My.Settings.SearchLimit Then
                lblStatus.Text = "Search limit exceeded (" & My.Settings.SearchLimit & ").  Adjust your search criteria to display all applicable results."
            Else
                lblStatus.Text = "Search complete.  " & grdDirectory.RowCount & " records found."
            End If
            lblStatus.Image = frmConsoleParent.imlGUI.Images(1) '  Ok
            lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        Else
            lblStatus.Text = gFunctions.GetErrorTypeAndCode(Result)
            Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0) ' Error
            Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        End If

        ' Hide the last column, as this is the User Path.
        grdDirectory.Columns.Item(grdDirectory.ColumnCount - 1).Visible = False

        Return Result
    End Function

    Private Sub PlaceRuntimeControls()
        Dim oTextbox As SearchTextbox
        Dim oImage As PictureBox
        Dim oShiftImage As PictureBox
        Dim oSortAscending As Button '= Nothing
        Dim oSortDescending As Button '= Nothing
        Dim oHeaderLabel As Label

        ' Allocate memory for the Search Textboxes
        ReDim Preserve m_SearchText(grdDirectory.Columns.Count)

        For Column As Int16 = 0 To grdDirectory.Columns.Count - 2 ' Need to hide the last column
            If m_LoadComplete Then
                oTextbox = Me.pnlSearch.Controls().Item("SearchText" & Column.ToString)
                oImage = Me.pnlSearch.Controls().Item("KeyImage" & Column.ToString)
                oShiftImage = Me.pnlSearch.Controls().Item("ShiftImage" & Column.ToString)
                oSortAscending = Me.pnlSearch.Controls().Item("btnSortAsc" & Column.ToString)
                oSortDescending = Me.pnlSearch.Controls().Item("btnSortDesc" & Column.ToString)
                oHeaderLabel = Me.pnlSearch.Controls().Item("lblHeader" & Column.ToString)
            Else
                oTextbox = New SearchTextbox
                oTextbox.Name = "SearchText" & Column.ToString
                oImage = New PictureBox
                oImage.Name = "KeyImage" & Column.ToString
                oShiftImage = New PictureBox
                oShiftImage.Name = "ShiftImage" & Column.ToString
                oSortAscending = New Button
                oSortAscending.Name = "btnSortAsc" & Column.ToString
                oSortDescending = New Button
                oSortDescending.Name = "btnSortDesc" & Column.ToString
                oHeaderLabel = New Label
                oHeaderLabel.Name = "lblHeader" & Column.ToString
            End If

            Dim ColumnWidth As Int16 = 0
            'ColumnWidth = (Me.Width \ grdDirectory.ColumnCount - 2) - 5
            ColumnWidth = grdDirectory.Columns(Column).Width
            ' Place the text search boxes
            'oTextbox.Left = grdDirectory.Left + ((Column) * ColumnWidth)
            oTextbox.Top = 20
            oTextbox.Width = ColumnWidth
            oTextbox.Anchor = AnchorStyles.Top + AnchorStyles.Right + AnchorStyles.Left
            oTextbox.Tag = LDAPFields(Column)

            ' Place the 'Shift' images
            oShiftImage.Image = frmConsoleParent.imlMain.Images.Item(0) ' Item 0 is the image for the Shift key
            oShiftImage.Width = 24
            oShiftImage.Height = 16
            oShiftImage.SizeMode = PictureBoxSizeMode.StretchImage
            'oCtrlImage.Left = oTextbox.Left
            oShiftImage.Anchor = AnchorStyles.Top + AnchorStyles.Left
            oShiftImage.Top = 3

            ' Place the Fx images
            oImage.Image = frmConsoleParent.imlMain.Images.Item(Column + 1)
            oImage.Width = 24
            oImage.Height = 16
            oImage.SizeMode = PictureBoxSizeMode.StretchImage
            'oImage.Left = oTextbox.Left + oCtrlImage.Width
            oImage.Anchor = AnchorStyles.Top + AnchorStyles.Left
            oImage.Top = 3

            ' Place the Sort buttons
            oSortAscending.Top = 40
            oSortAscending.Size = New Size(15, 15)
            oSortAscending.Left = oTextbox.Left
            oSortAscending.Anchor = AnchorStyles.Top + AnchorStyles.Left
            oSortAscending.TabStop = False

            oSortDescending.Top = 40
            oSortDescending.Size = New Size(15, 15)
            oSortDescending.Left = oTextbox.Left + oSortAscending.Width
            oSortDescending.Anchor = AnchorStyles.Top + AnchorStyles.Left
            oSortDescending.TabStop = False

            ' Give the buttons the correct image
            oSortAscending.ImageList = Me.imlSearch
            oSortAscending.ImageIndex = 0
            oSortDescending.ImageList = Me.imlSearch
            oSortDescending.ImageIndex = 1

            ' Now place the header labels
            oHeaderLabel.Top = 60
            oHeaderLabel.Left = oTextbox.Left
            oHeaderLabel.Width = ColumnWidth
            oHeaderLabel.TextAlign = ContentAlignment.TopCenter
            oHeaderLabel.Anchor = AnchorStyles.Top + AnchorStyles.Left
            oHeaderLabel.Text = grdDirectory.Columns(Column).Name

            ' Set the controls visible state
            oTextbox.Visible = False
            oShiftImage.Visible = False
            oImage.Visible = False
            oSortAscending.Visible = False
            oSortDescending.Visible = False
            oHeaderLabel.Visible = False

            If m_LoadComplete Then
            Else
                Me.pnlSearch.Controls.Add(oTextbox)
                Me.pnlSearch.Controls.Add(oImage)
                Me.pnlSearch.Controls.Add(oShiftImage)

                Me.pnlSearch.Controls.Add(oSortAscending)
                Me.pnlSearch.Controls.Add(oSortDescending)

                Me.pnlSearch.Controls.Add(oHeaderLabel)

                ' Prepare the search text array
                m_SearchText(Column) = ""

                ' Allow us to capture a few Events
                AddHandler oTextbox.KeyUp, AddressOf Me.KeyPressed
                AddHandler oTextbox.TextChanged, AddressOf Me.SearchTextChanged
                AddHandler oTextbox.GotFocus, AddressOf Me.TextFocus
                AddHandler oTextbox.LostFocus, AddressOf Me.TextLostFocus

                AddHandler oSortAscending.Click, AddressOf Me.AscendingClicked
                AddHandler oSortDescending.Click, AddressOf Me.DescendingClicked
            End If
        Next

        RedrawControls()

        'Me.Refresh()
    End Sub

    Private Sub RedrawControls()
        Dim ColumnLeftPosition As Int16 = 0

        'Debug.Print("Inside RedrawControls")

        ' Re-place the controls
        For Column As Int16 = 0 To grdDirectory.Columns.Count - 2 ' Need to hide the last column
            Try
                Dim oTextbox As TextBox = Me.pnlSearch.Controls("SearchText" & Column)
                Dim oImage As PictureBox = Me.pnlSearch.Controls("KeyImage" & Column)
                Dim oShiftImage As PictureBox = Me.pnlSearch.Controls("ShiftImage" & Column)
                Dim oSortAscending As Button = Me.pnlSearch.Controls("btnSortAsc" & Column)
                Dim oSortDescending As Button = Me.pnlSearch.Controls("btnSortDesc" & Column)
                Dim oHeaderLabel As Label = Me.pnlSearch.Controls("lblHeader" & Column)
                Dim ColumnWidth As Int16 = 0

                'ColumnWidth = (Me.Width \ grdDirectory.ColumnCount - 2)
                ColumnWidth = grdDirectory.Columns(Column).Width

                oTextbox.Left = ColumnLeftPosition + 7
                oTextbox.Width = ColumnWidth

                oShiftImage.Left = oTextbox.Left
                oImage.Left = oTextbox.Left + oShiftImage.Width

                oSortAscending.Left = oTextbox.Left
                oSortDescending.Left = oTextbox.Left + oSortAscending.Width

                oHeaderLabel.Left = oTextbox.Left
                oHeaderLabel.Width = ColumnWidth

                ColumnLeftPosition += ColumnWidth

                oTextbox.Visible = True
                oImage.Visible = True
                oShiftImage.Visible = True
                oSortAscending.Visible = True
                oSortDescending.Visible = True
                oHeaderLabel.Visible = True

            Catch ex As Exception
                'Debug.Print(ex.Message)
            End Try
        Next

    End Sub

    Private Sub BuildSearchString()
        Dim i As Int16
        Dim SearchString As String = ""

        For i = 0 To grdDirectory.Columns.Count - 2
            If m_SearchText(i).ToString.Trim.Length > 0 Then
                SearchString &= "(" & m_SearchText(i).ToString & ")"
            Else
                SearchString &= ""
            End If
        Next

        m_SearchPrefix = "&" & SearchString

        If My.Settings.FriendlySearchStringDisplay = True Then
            lblStatus.Text = "Searching..."
        Else
            lblStatus.Text = "Search filter: (" & m_SearchPrefix & m_SearchFilter & ")"
        End If


    End Sub

    ''' <summary>
    ''' Original source: http://www.velocityreviews.com/forums/t106086-ldap-query-to-datagrid.html
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateDataSource(ByVal Filter As String, ByRef View As ICollection) As Int32
        Dim dt As New DataTable()
        Dim dr As DataRow

        My.Settings.Reset() ' TODO:  Move to config page

        Dim iCounter As Int16 = 0

        ' Read the list of fields to retrieve
        For Each Field As String In SearchFields
            ReDim Preserve DisplayNames(iCounter)
            ReDim Preserve LDAPFields(iCounter)
            DisplayNames(iCounter) = Field.Substring(0, Field.IndexOf("=")).Trim
            LDAPFields(iCounter) = Field.Substring(Field.IndexOf("=") + 1).Trim
            dt.Columns.Add(New DataColumn(DisplayNames(iCounter), GetType(String)))

            iCounter += 1
        Next

        ' Add User path as the last field.  This column gets hidden later.
        dt.Columns.Add("User Path")
        ReDim Preserve LDAPFields(iCounter)
        LDAPFields(iCounter) = "path"
        lblStatus.Text = "Connecting..."
        Me.Refresh()

        Try
            ' Perform the actual search
            Dim root As New DirectoryServices.DirectoryEntry(gSearchRoot, TargetDirectoryConnectionUsername, TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind) ' Was gUserRoot
            Dim rootSearch As New DirectorySearcher(root)
            Dim SearchResult As SearchResult
            Dim results As SearchResultCollection

            rootSearch.PropertiesToLoad.AddRange(LDAPFields)
            rootSearch.Filter = Filter
            rootSearch.SizeLimit = My.Settings.SearchLimit
            results = rootSearch.FindAll
            For Each SearchResult In results
                Try
                    dr = dt.NewRow()
                    For iField = 0 To UBound(DisplayNames) - 1
                        If SearchResult.Properties.Contains(LDAPFields(iField)) Then
                            dr(iField) = SearchResult.Properties(LDAPFields(iField)).Item(0)
                        End If
                    Next
                    dr(dt.Columns.Count - 1) = SearchResult.Path
                    dt.Rows.Add(dr)
                Catch ex As Exception
                    'lblStatus.Text = ex.Message.ToString
                End Try
            Next

            lblStatus.Text = "Searching..."
            Me.Refresh()

            dt.TableName = "SearchResults"

            'Dim View As DataView = New DataView
            View = ApplySortOrder(dt)

            Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(1)
            Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText

            Return SUCCESS
            'Return View
        Catch ex As Exception
            'View = Nothing
            dt.TableName = "SearchResults"
            View = ApplySortOrder(dt)

            Select Case ex.Message.Trim
                Case "The server is not operational."
                    Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                    Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    Return gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.SERVER_NOT_FOUND)
                Case "Logon failure: unknown user name or bad password."
                    Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                    Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    Return gFunctions.ErrorResult(ERRORTYPE.SECURITY, ERROR_SECURITY.AUTHENTICATION_FAILURE)
                Case "A local error has occurred."
                    Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                    Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    Return gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
                Case Else
                    Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                    Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    Return gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
            End Select
        End Try
    End Function

    Private Function ApplySortOrder(ByVal Table As DataTable) As DataView
        Dim SortOrder As String = gFunctions.StringFromNameValueCollection(m_GridSortOrder, ",")
        Dim View As DataView = New DataView

        Try
            With View
                .Table = Table
                If SortOrder.Length > 0 Then .Sort = SortOrder
            End With
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

        lblSortOrder.Text = "Sort order: " & SortOrder

        Return View
    End Function

    Private Sub ShowDetailsForm()
        Dim DetailsForm As New frmDetails

        DetailsForm.Username = grdDirectory.CurrentRow.Cells(0).Value.ToString
        DetailsForm.UserPath = grdDirectory.CurrentRow.Cells(grdDirectory.CurrentRow.Cells().Count - 1).Value.ToString

        DetailsForm.ShowDialog()
        ' Afterwards we have to essentially do a refresh of the data
        DoSearch()
    End Sub

End Class