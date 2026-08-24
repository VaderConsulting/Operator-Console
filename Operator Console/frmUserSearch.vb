Imports System.DirectoryServices
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE


Public Class frmUserSearch

    Private m_LoadComplete As Boolean = False
    Private SearchFields As Collections.Specialized.OrderedDictionary = Nothing
    Private DisplayNames As String() = {}
    Private LDAPFields As String() = {}
    Private FieldWidths As String() = {}
    Private m_SearchFilter As String = "(objectClass=user)"
    Private m_SearchPrefix As String = ""
    Private m_SearchText() As String = {}
    Private m_DataView() As DataView
    Private m_GridSortOrder As New Collections.Specialized.OrderedDictionary
    Public CanBeClosed As Boolean = False
    Private m_SelectedSearchBoxName As String = "SearchText0"
    Private m_SelectedGridRow As Integer = 0

#Region " GUI Elements "

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DoKeyPress(sender, e)
    End Sub

    Private Sub frmSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Start()
        SizeForm()
        RedrawControls()
    End Sub

    Private Sub grdDirectory_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDirectory.CellClick
        m_SelectedGridRow = e.RowIndex
        FocusOnSearchTextbox()
    End Sub

    Private Sub pnlSearch_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlSearch.Resize
        ' Handles the form_maximize event
        If Not m_LoadComplete Then Exit Sub
        RedrawControls()
    End Sub

    Private Sub frmSearch_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        'Debug.Print("Processing KeyUp()")
        'DoKeyPress(sender, e)
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

    Private Sub grdDirectory_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDirectory.CellDoubleClick
        m_SelectedGridRow = e.RowIndex
        SelectGridRow()
        ShowDetailsForm()
    End Sub

    Private Sub grdDirectory_ColumnWidthChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdDirectory.ColumnWidthChanged
        'Debug.Print("ColumnWidthChanged() and LoadComplete = " & m_LoadComplete.ToString)

        If Not m_LoadComplete Then Exit Sub


        'Debug.Print("e.Column.Index =" & e.Column.Index)
        ' Because this event is spawned so many times, we will only selectively redraw the controls
        'If e.Column.Index = grdDirectory.ColumnCount - 2 Then
        Debug.Print("Column width")
        For i As Integer = 0 To grdDirectory.ColumnCount - 1
            Debug.Print(grdDirectory.Columns(i).Name.ToString.PadLeft(6, " ") & " " & grdDirectory.Columns(i).Width.ToString.PadLeft(5, " "))
        Next
        RedrawControls()
        'End If
    End Sub

    Private Sub btnClearSortOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSortOrder.Click
        m_GridSortOrder.Clear()
        GetSortedData()
        FocusOnSearchTextbox()
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
        Me.tmrSearch.Interval = gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchPauseTime", gSettings))

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
        Dim BackColour As String = gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings))

        txtbox = CType(sender, TextBox)

        'Debug.Print("Inside TextFocus:Focus has been changed to " & txtbox.Name)

        m_SelectedSearchBoxName = txtbox.Name
        FocusOnSearchTextbox()
        txtbox.SelectAll()
        'txtbox.BackColor = System.Drawing.ColorTranslator.FromHtml(BackColour)
    End Sub

    Private Sub TextLostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txtbox As TextBox

        'Debug.Print("Inside TextLostFocus()")

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
        If Not m_GridSortOrder.Contains(ButtonIndex.ToString) Then
            m_GridSortOrder.Add(ButtonIndex.ToString, SortText)
            GetSortedData()
        End If

        Me.pnlSearch.Controls().Item("SearchText" & ButtonIndex).Focus()

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
        If Not m_GridSortOrder.Contains(ButtonIndex.ToString) Then
            m_GridSortOrder.Add(ButtonIndex.ToString, SortText)
            GetSortedData()
        End If

        Me.pnlSearch.Controls().Item("SearchText" & ButtonIndex).Focus()

    End Sub

    Private Sub KeyPressed(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        'Debug.Print("Inside KeyPressed")
        'DoKeyPress(sender, e)
    End Sub

#End Region

#Region " Other methods "

    Private Sub Start()
        Dim Result As Int32 = 0

        'SearchFields.Clear()
        m_GridSortOrder.Clear()

        Me.Show()
        Me.Refresh()

        gFunctions.CopySetting("SortOrder", "1", gSettings, m_GridSortOrder)

        gFunctions.TargetDirectoryConnectionUsername = gFunctions.CollectionValue("DirectoryUserUsername", gSettings)
        gFunctions.TargetDirectoryConnectionPassword = gFunctions.CollectionValue("DirectoryUserPassword", gSettings)
        gFunctions.ConnectAnonymously = False

        'gFunctions.DirectoryConnection = gFunctions.GetDirectoryConnection(gFunctions.CollectionValue("DirectoryUserRoot", gSettings)) ' gSearchRoot

        If gFunctions.LastStatus.CADS_ERRORCODE > SUCCESS Then
            Select Case gFunctions.LastStatus.LastException.Message
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

        DoSearch()

        PlaceRuntimeControls()

        'FocusOnSearchTextbox()

        If gFunctions.LastStatus.CADS_ERRORCODE = SUCCESS Then
            'Debug.Print("Start():  Calling FocusOnSearchTextbox()")

            SetAlternateRowColour()

            ' Set a minimum form size, according to the number of columns
            Dim MinimumRequiredScreenWidth As Point = New Point(grdDirectory.ColumnCount * 57, 200)

            If My.Computer.Screen.Bounds.Width < MinimumRequiredScreenWidth.X Then
                MsgBox("Your primary screen video resolution is set too low.  You cannot display the search results." & vbCrLf & vbCrLf & _
                       "Correct your screen resolution or modify your Operator Console configuration to correct this problem.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Configuration error")
                End
            End If

            SizeColumns()

            m_SelectedSearchBoxName = "SearchText0"
            FocusOnSearchTextbox()
        End If

        m_LoadComplete = True
    End Sub

    Public Sub FocusOnSearchTextbox()
        Dim BackColour As String = gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings))
        'Debug.Print("Inside FocusOnSearchTextbox")
        Try
            Dim txtbox As TextBox = Me.pnlSearch.Controls.Item(m_SelectedSearchBoxName)
            'txtbox.Focus()
            txtbox.BackColor = System.Drawing.ColorTranslator.FromHtml(BackColour)
            'txtbox.SelectAll()
            Me.pnlSearch.Controls.Item(m_SelectedSearchBoxName).Focus()

            'Me.pnlSearch.Controls(m_SelectedSearchBoxName).BackColor = System.Drawing.ColorTranslator.FromHtml(BackColour)
        Catch ex As Exception
            lblStatus.Text = "Error accessing textbox '" & m_SelectedSearchBoxName & "'"
        End Try

    End Sub

    Private Sub SetAlternateRowColour()
        Dim AlternateRowColour As String = gFunctions.ValueFromSetting(gFunctions.CollectionValue("AlternatingRowBackColour", gSettings))

        Me.grdDirectory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(AlternateRowColour)
    End Sub

    Public Sub DoSearch()

        Cursor.Current = Cursors.WaitCursor

        GetSortedData()

        'PlaceRuntimeControls()

        'Debug.Print("Search textbox name: " & m_SelectedSearchBoxName)
        'Debug.Print("SetSearch:  Calling FocusOnSearchTextbox()")
        'FocusOnSearchTextbox()

        m_SelectedGridRow = 0
        SelectGridRow()

        Cursor.Current = Cursors.Default

    End Sub

    Private Sub SizeForm()
        Dim Dimensions As String = gFunctions.CollectionValue("SearchFormSize", gSettings)
        Dim Size(1) As String

        If Dimensions <> Constants.DEFAULTSTRINGVALUE Then
            Size = Split(Dimensions, "|")
        Else
            Size(0) = "800"
            Size(1) = "600"
        End If

        Me.Width = Size(0)
        Me.Height = Size(1)

    End Sub

    Private Sub SizeColumns()
        ' Size columns appropriately
        For Index As Integer = 0 To FieldWidths.Length - 1
            grdDirectory.Columns(Index).Width = FieldWidths(Index)
            'Debug.Print("Column " & Index & " width = " & FieldWidths(Index))
        Next
        grdDirectory.Refresh()
    End Sub

    Private Sub GetSortedData()
        Dim SortOrder As String = ""
        Dim Result As Int32
        Dim searchFilter As String = "(" & m_SearchPrefix & m_SearchFilter & ")"
        Dim SearchLimit As String

        SearchLimit = gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchLimit", gSettings))

        Dim SearchCount As Integer = 1
        ReDim m_DataView(SearchCount)

        For SearchNumber = 0 To SearchCount - 1
            ' build a Datatable, and save it to m_GridContents
            CreateDataSource(searchFilter, m_DataView(SearchNumber))
        Next

        ' TODO:  Merge multiple record sets into the one dataset
        '        This is necessary because we will need to perform multiple searches (for each target OU)
        '        http://www.dotnetheaven.com/UploadFile/rahul4_saxena/MergetheData06012007012539AM/MergetheData.aspx

        Dim ResultantDataSet As New DataSet

        ResultantDataSet.Tables.Add(m_DataView(0).Table)
        grdDirectory.DataSource = ResultantDataSet.Tables(0)

        grdDirectory.Refresh()

        If gFunctions.LastStatus.CADS_ERRORCODE = SUCCESS Then
            If grdDirectory.RowCount = SearchLimit Then
                lblStatus.Text = "Search limit exceeded (" & SearchLimit & ").  Adjust your search criteria to display all applicable results." ' TODO:  Load from config
            Else
                lblStatus.Text = "Search complete.  " & grdDirectory.RowCount & " records found."
            End If
            lblStatus.Image = frmConsoleParent.imlGUI.Images(1) '  Ok
            lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        Else
            Select Case gFunctions.LastStatus.CADS_ERRORCODE
                Case Is > 0
                    'Debug.Print("Error number: " & gFunctions.LastStatus.CADS_ERRORCODE)
            End Select
            lblStatus.Text = gFunctions.GetErrorTypeAndCode(gFunctions.LastStatus.CADS_ERRORCODE) & ". Current Directory Server='" & gFunctions.CurrentDirectoryServer & "'"
            Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0) ' Error
            Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        End If

        ' Hide the last column, as this is the User Path.
        If grdDirectory.ColumnCount > 0 Then
            grdDirectory.Columns.Item(grdDirectory.ColumnCount - 1).Visible = False
        End If
        gFunctions.LastStatus.CADS_ERRORCODE = Result
    End Sub

    Private Sub TextMouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseWheel
        Debug.Print("Delta: " & e.Delta.ToString)

        ' Little bit of magic here...
        Dim NumberOfLines As Integer = Math.Abs(e.Delta / 80)

        If e.Delta > 0 Then
            'DoMoveUp(NumberOfLines)
            DoMoveUp(1)
        Else
            'DoMoveDown(NumberOfLines)
            DoMoveDown(1)
        End If

        'Debug.Print("Mouse Wheel event")
    End Sub

    Private Sub PlaceRuntimeControls()
        Dim oTextbox As SearchTextbox = Nothing
        Dim oImage As PictureBox = Nothing
        Dim oShiftImage As PictureBox = Nothing
        Dim oSortAscending As Button = Nothing
        Dim oSortDescending As Button = Nothing
        Dim oHeaderLabel As Label = Nothing

        'Debug.Print("Inside PlaceRuntimeControls()")

        ' Allocate memory for the Search Textboxes
        ReDim Preserve m_SearchText(grdDirectory.Columns.Count)

        For Column As Int16 = 0 To grdDirectory.Columns.Count - 2 ' Need to hide the last column
            If m_LoadComplete Then
                oTextbox = Me.pnlSearch.Controls().Item("SearchText" & Column.ToString)
                If Column <= 11 Then
                    oImage = Me.pnlSearch.Controls().Item("KeyImage" & Column.ToString)
                    oShiftImage = Me.pnlSearch.Controls().Item("ShiftImage" & Column.ToString)
                End If
                oSortAscending = Me.pnlSearch.Controls().Item("btnSortAsc" & Column.ToString)
                oSortDescending = Me.pnlSearch.Controls().Item("btnSortDesc" & Column.ToString)
                oHeaderLabel = Me.pnlSearch.Controls().Item("lblHeader" & Column.ToString)
            Else
                oTextbox = New SearchTextbox
                oTextbox.Name = "SearchText" & Column.ToString
                If Column <= 11 Then
                    oImage = New PictureBox
                    oImage.Name = "KeyImage" & Column.ToString
                    oShiftImage = New PictureBox
                    oShiftImage.Name = "ShiftImage" & Column.ToString
                End If
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

            If Column <= 11 Then
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
            End If

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

            If Column <= 11 Then
                oShiftImage.Visible = False
                oImage.Visible = False
            End If

            oSortAscending.Visible = False
            oSortDescending.Visible = False
            oHeaderLabel.Visible = False

            If m_LoadComplete Then
            Else
                Me.pnlSearch.Controls.Add(oTextbox)
                If Column <= 11 Then
                    Me.pnlSearch.Controls.Add(oImage)
                    Me.pnlSearch.Controls.Add(oShiftImage)
                End If
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
                AddHandler oTextbox.MouseWheel, AddressOf Me.TextMouseWheel

                AddHandler oSortAscending.Click, AddressOf Me.AscendingClicked
                AddHandler oSortDescending.Click, AddressOf Me.DescendingClicked
            End If
        Next

        RedrawControls()

        'Me.Refresh()
    End Sub

    Private Sub RedrawControls()
        Dim ColumnLeftPosition As Int16 = 0

        Dim oTextbox As TextBox = Nothing
        Dim oImage As PictureBox = Nothing
        Dim oShiftImage As PictureBox = Nothing
        Dim oSortAscending As Button = Nothing
        Dim oSortDescending As Button = Nothing

        ' Re-place the controls
        For Column As Int16 = 0 To grdDirectory.Columns.Count - 2 ' Need to hide the last column
            Try
                oTextbox = Me.pnlSearch.Controls("SearchText" & Column)
                If Column <= 11 Then
                    oImage = Me.pnlSearch.Controls("KeyImage" & Column)
                    oShiftImage = Me.pnlSearch.Controls("ShiftImage" & Column)
                End If
                oSortAscending = Me.pnlSearch.Controls("btnSortAsc" & Column)
                oSortDescending = Me.pnlSearch.Controls("btnSortDesc" & Column)

                Dim oHeaderLabel As Label = Me.pnlSearch.Controls("lblHeader" & Column)
                Dim ColumnWidth As Int16 = 0

                'ColumnWidth = (Me.Width \ grdDirectory.ColumnCount - 2)
                ColumnWidth = grdDirectory.Columns(Column).Width

                oTextbox.Left = ColumnLeftPosition + 7
                oTextbox.Width = ColumnWidth

                If Column <= 11 Then    
                    oShiftImage.Left = oTextbox.Left
                    oImage.Left = oTextbox.Left + oShiftImage.Width
                End If
                oSortAscending.Left = oTextbox.Left
                oSortDescending.Left = oTextbox.Left + oSortAscending.Width
                oHeaderLabel.Left = oTextbox.Left
                oHeaderLabel.Width = ColumnWidth

                ColumnLeftPosition += ColumnWidth

                oTextbox.Visible = True
                If Column <= 11 Then
                    oImage.Visible = True
                    oShiftImage.Visible = True
                End If
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
                SearchString &= m_SearchText(i).ToString
            Else
                SearchString &= ""
            End If
        Next

        m_SearchPrefix = "&" & SearchString

        'If My.Settings.FriendlySearchStringDisplay = True Then ' TODO:  Load from config
        lblStatus.Text = "Searching..."
        'Else
        'lblStatus.Text = "Search filter: (" & m_SearchPrefix & m_SearchFilter & ")"
        'End If


    End Sub

    ''' <summary>
    ''' Original source: http://www.velocityreviews.com/forums/t106086-ldap-query-to-datagrid.html
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateDataSource(ByVal Filter As String, ByRef View As ICollection)
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim SearchLimit As String
        Dim EncryptedPassword As String
        Dim EncryptionKey As String
        Dim SearchLimitRawData As String

        SearchLimitRawData = gFunctions.CollectionValue("SearchLimit", gSettings).Substring(0, 3)
        If SearchLimitRawData.Contains("|") Then
            SearchLimit = SearchLimitRawData.Substring(0, SearchLimitRawData.IndexOf("|"))
        Else
            SearchLimit = SearchLimitRawData
        End If

        Dim iCounter As Int16 = 0

        ' Get Search Fields
        Try
            SearchFields = gFunctions.MySection(gFunctions.InteractiveUserUsername, "ConsoleSearchFields")

            Dim FieldNames As ICollection = SearchFields.Keys
            Dim FieldValues As ICollection = SearchFields.Values

            ' Read the list of fields to retrieve
            For Each Field As String In SearchFields.Values
                ReDim Preserve DisplayNames(iCounter)
                ReDim Preserve LDAPFields(iCounter)
                ReDim Preserve FieldWidths(iCounter)

                Dim FieldName As String = ""
                Dim DisplayName As String = ""
                FieldName = FieldNames(iCounter)
                DisplayName = FieldValues(iCounter)
                DisplayNames(iCounter) = DisplayName.Substring(0, DisplayName.IndexOf("|")).Trim
                FieldWidths(iCounter) = DisplayName.Substring(DisplayName.IndexOf("|") + 1).Trim
                LDAPFields(iCounter) = FieldName
                dt.Columns.Add(New DataColumn(DisplayNames(iCounter), GetType(String)))

                iCounter += 1
            Next

            ' Add User path as the last field.  This column gets hidden later.
            dt.Columns.Add("User Path")
            ReDim Preserve LDAPFields(iCounter)
            LDAPFields(iCounter) = "path"
            lblStatus.Text = "Connecting..."
            Me.Refresh()

            gFunctions.TargetDirectoryConnectionUsername = gFunctions.CollectionValue("DirectoryUserUsername", gSettings)
            EncryptedPassword = gFunctions.CollectionValue("DirectoryUserPassword", gSettings)
            EncryptionKey = gFunctions.SimpleCrypt(gFunctions.CollectionValue("EncryptionKey", gSettings))
            gFunctions.TargetDirectoryConnectionPassword = gFunctions.XMLDecrypt(EncryptedPassword, EncryptionKey)

            Try
                ' Perform the actual search
                ' TODO:  This binds to a server, which is ok for ADAM, but possibly not for AD
                Dim TheServerName As String = gFunctions.CurrentDirectoryServer

                If Not TheServerName.StartsWith("LDAP://") Then
                    TheServerName = "LDAP://" & TheServerName
                End If

                If Not TheServerName.EndsWith("/") Then
                    TheServerName &= "/"
                End If
                Dim root As New DirectoryServices.DirectoryEntry(TheServerName & gFunctions.CollectionValue("DirectoryUserRoot", gSettings), gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
                Dim rootSearch As New DirectorySearcher(root)
                Dim SearchResult As SearchResult
                Dim results As SearchResultCollection

                rootSearch.PropertiesToLoad.AddRange(LDAPFields)
                rootSearch.Filter = Filter
                rootSearch.SizeLimit = SearchLimit
                rootSearch.CacheResults = True
                ' Set sort options
                Dim SortOrder As String = gFunctions.StringFromOrderedDictionary(m_GridSortOrder, "|")
                ' NOTE:  distinguishedName cannot be used in the server-side sort.  (http://support.microsoft.com/?kbid=842637)
                If SortOrder <> "" Then
                    Dim Sort() As String = SortOrder.Split("|")
                    Dim LDAPSortField As String = ""
                    ' TODO:  FIX!!!!!
                    If Sort(0).ToLower.Contains("asc") Then rootSearch.Sort.Direction = SortDirection.Ascending
                    If Sort(0).ToString.ToLower.Contains("desc") Then rootSearch.Sort.Direction = SortDirection.Descending
                    LDAPSortField = Sort(0).Substring(0, Sort(0).IndexOf(" ")).ToString
                    rootSearch.Sort.PropertyName = LDAPFields(0).ToString
                End If

                results = rootSearch.FindAll
                For Each SearchResult In results
                    Try
                        dr = dt.NewRow()
                        For iField = 0 To UBound(DisplayNames) '- 1
                            If SearchResult.Properties.Contains(LDAPFields(iField)) Then
                                If SearchResult.Properties(LDAPFields(iField)).Item(0).ToString.Contains("[]") Then
                                    dr(iField) = gFunctions.ConvertGUIDToString(SearchResult.Properties(LDAPFields(iField)).Item(0))
                                Else
                                    dr(iField) = SearchResult.Properties(LDAPFields(iField)).Item(0)
                                End If

                                'Debug.Print(dr(iField).ToString)
                            End If
                        Next
                        dr(dt.Columns.Count - 1) = SearchResult.Path
                        dt.Rows.Add(dr)
                    Catch ex As Exception
                        'lblStatus.Text = ex.Message.ToString
                    End Try
                Next

                lblStatus.Text = "Searching.  Applied Filter = " & Filter
                Me.Refresh()

                dt.TableName = "SearchResults"

                View = ApplySortOrder(dt)

                Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(1)
                Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText

                gFunctions.LastStatus.CADS_ERRORCODE = SUCCESS
            Catch ex As Exception
                dt.TableName = "SearchResults"
                View = ApplySortOrder(dt)

                Select Case ex.Message.Trim
                    Case "The server is not operational."
                        Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                        Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                        Debug.Print(ex.Message)
                        gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.SERVER_NOT_FOUND)
                    Case "Logon failure: unknown user name or bad password."
                        Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                        Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                        Debug.Print(ex.Message)
                        gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.SECURITY, ERROR_SECURITY.AUTHENTICATION_FAILURE)
                    Case "A local error has occurred."
                        Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                        Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                        Debug.Print(ex.Message)
                        gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
                    Case "An operations error occurred."
                        Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                        Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                        Debug.Print(ex.Message)
                        gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
                    Case "The time limit for this request was exceeded."
                        Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                        Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                        Debug.Print(ex.Message)
                        gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.REQUEST_TIMEOUT)
                    Case Else
                        Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
                        Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                        Debug.Print(ex.Message)
                        gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
                End Select
            End Try
        Catch ex As Exception ' Attempting to connect to Config Server
            Me.lblStatus.Image = frmConsoleParent.imlGUI.Images(0)
            Me.lblStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            Debug.Print(ex.Message)
            gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.CONFIGURATION, ERROR_CONFIGURATION.CONFIG_SERVER_TIMEOUT_ERROR)
        End Try
    End Sub

    Private Function ApplySortOrder(ByVal Table As DataTable) As DataView
        Dim SortOrder As String = gFunctions.StringFromOrderedDictionary(m_GridSortOrder, ",")
        Dim View As DataView = New DataView

        Try
            With View
                .Table = Table
                If SortOrder.Length > 0 Then .Sort = SortOrder.Replace("|", ",")
            End With
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

        lblSortOrder.Text = "Sort order: " & SortOrder.Replace("|", ",")

        Return View
    End Function

    Private Sub ShowDetailsForm()
        If grdDirectory.Rows.Count > 0 Then

            Dim DetailsForm As New frmUserDetails

            DetailsForm.Username = grdDirectory.Rows(m_SelectedGridRow).Cells(0).Value.ToString
            DetailsForm.UserPath = grdDirectory.Rows(m_SelectedGridRow).Cells(grdDirectory.CurrentRow.Cells().Count - 1).Value.ToString

            Me.lblStatus.Text = "Details form opened."
            DetailsForm.ParentSearchForm = Me
            DetailsForm.Show()
            DetailsForm.Focus()
            DetailsForm.BringToFront()
        End If
    End Sub

    Private Sub DoKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim i As Int16

        ' Keep the name of the currently selected textbox
        Dim SavedSearchTextboxName As String = m_SelectedSearchBoxName

        'Dim BackColour As String = gFunctions.ValueFromSetting(gFunctions.CollectionValue("SearchTextboxColour", gSettings))

        Debug.Print(e.KeyCode)

        Select Case e.KeyCode
            Case 33, Keys.PageUp
                DoMoveUp(10)
                'If m_SelectedGridRow > 10 Then
                '    m_SelectedGridRow -= 10
                '    SelectGridRow()
                '    e.SuppressKeyPress = True
                '    grdDirectory.ResumeLayout()
                'Else
                '    m_SelectedGridRow = 0
                '    SelectGridRow()
                '    e.SuppressKeyPress = True
                '    grdDirectory.ResumeLayout()
                'End If
            Case 34, Keys.PageDown
                DoMoveDown(10)
                'If m_SelectedGridRow < grdDirectory.RowCount - 10 Then
                '    m_SelectedGridRow += 10
                '    SelectGridRow()
                '    e.SuppressKeyPress = True
                '    grdDirectory.ResumeLayout()
                'Else
                '    m_SelectedGridRow = grdDirectory.RowCount - 1
                '    SelectGridRow()
                '    e.SuppressKeyPress = True
                '    grdDirectory.ResumeLayout()
                'End If
            Case Keys.Enter, 13
                e.SuppressKeyPress = True
                ShowDetailsForm()
            Case Keys.Shift, 16
                Exit Sub
            Case Keys.Escape
                For i = 0 To grdDirectory.Columns.Count - 2
                    Dim oTextbox As TextBox = Me.pnlSearch.Controls("SearchText" & i.ToString)
                    oTextbox.Text = ""
                    Me.lblStatus.Text = "Search text cleared."
                Next
            Case Keys.Left
            Case Keys.Up
                DoMoveUp(1)
                'If m_SelectedGridRow > 0 Then
                '    m_SelectedGridRow -= 1
                '    SelectGridRow()
                '    e.SuppressKeyPress = True
                'End If
            Case Keys.Right
            Case Keys.Down
                DoMoveDown(1)
                'If m_SelectedGridRow < grdDirectory.RowCount - 1 Then
                '    m_SelectedGridRow += 1
                '    SelectGridRow()
                '    e.SuppressKeyPress = True
                '    grdDirectory.ResumeLayout()
                'End If
            Case Keys.F5
                'Debug.Print("F5 pressed")
                If Not e.Shift Then
                    DoSearch()
                    Me.lblStatus.Text = "Search results refreshed. " & grdDirectory.RowCount & " records found."
                End If
        End Select

        If e.Shift Then ' They have the Shift key pressed, so this is a shortcut key
            For i = 0 To grdDirectory.ColumnCount - 2
                If e.KeyValue = i + 112 Then
                    Try
                        m_SelectedSearchBoxName = "SearchText" & i.ToString
                        FocusOnSearchTextbox()
                        e.SuppressKeyPress = True
                        Exit Sub
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

        ' Now set the name of the currently selected textbox back to what it was
        m_SelectedSearchBoxName = SavedSearchTextboxName
    End Sub

    Private Sub DoMoveUp(ByVal LineCount As Integer)
        If m_SelectedGridRow > LineCount Then
            m_SelectedGridRow -= LineCount
        Else
            m_SelectedGridRow = 0
        End If

        SelectGridRow()
        grdDirectory.ResumeLayout()
    End Sub

    Private Sub DoMoveDown(ByVal LineCount As Integer)
        If m_SelectedGridRow < grdDirectory.RowCount - LineCount Then
            m_SelectedGridRow += LineCount
        Else
            m_SelectedGridRow = grdDirectory.RowCount - 1
        End If

        SelectGridRow()
        grdDirectory.ResumeLayout()
    End Sub

    Private Sub SelectGridRow()
        If grdDirectory.Rows.Count > 0 Then
            grdDirectory.Rows(m_SelectedGridRow).Selected = True
            If Not grdDirectory.Rows(m_SelectedGridRow).Displayed Then
                grdDirectory.FirstDisplayedScrollingRowIndex = m_SelectedGridRow
            End If
        End If
    End Sub

#End Region

    Private Sub frmSearch_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Debug.Print("Search Form Size: " & Me.Width & "," & Me.Height)
    End Sub

    Private Sub frmSearch_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        FocusOnSearchTextbox()
    End Sub

    Private Sub grdDirectory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDirectory.Click
        FocusOnSearchTextbox()
    End Sub

End Class