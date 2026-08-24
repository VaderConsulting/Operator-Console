Imports System.Windows.Forms
Imports Utility
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmConsoleParent

    Private m_ChildFormNumber As Integer

#Region " GUI Elements "

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasteToolStripMenuItem.Click
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim AboutForm As New frmSplash

        AboutForm.DoAboutBox = True

        AboutForm.ShowDialog()

        AboutForm = Nothing
    End Sub

    Private Sub SearchMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowSearchForm()
    End Sub

    Private Sub frmConsoleParent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DoStartupConfig()
    End Sub

    Private Sub frmConsoleParent_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Debug.Print("Console Form Size: " & Me.Width & "," & Me.Height)
        Me.Refresh()
    End Sub

#End Region

    Private Sub SizeForm()
        Dim Dimensions As String = gFunctions.CollectionValue("ConsoleFormSize", gSettings)
        Dim Size(1) As String

        If Dimensions <> Constants.DEFAULTSTRINGVALUE Then
            Size = Split(Dimensions, "|")
        Else
            Size(0) = "800"
            Size(1) = "600"
        End If

        Me.Width = Size(0)
        Me.Height = Size(1)
        Me.CenterToScreen()
    End Sub

    'Private Sub DoLogin()
    '    Dim LoginForm As New Utility.frmLogin
    '    Dim PerformAutoLogon As Boolean = False
    '    Dim CADSVersion As String = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor
    '    Dim LoginResult As DialogResult
    '    Dim UserSecurityLevel As String = Constants.DEFAULTSTRINGVALUE
    '    Dim EncryptedPassword As String = Constants.DEFAULTSTRINGVALUE

    '    'gFunctions.StartClient()

    '    Dim EncryptedEncryptionKey As String = gFunctions.ConfigClient.LoadConfigSetting("SystemSettings", "EncryptionKey")
    '    Dim gDecryptedEncryptionKey As String = gFunctions.SimpleCrypt(EncryptedEncryptionKey)
    '    Dim Username As String = ""
    '    Dim UserPassword = ""

    '    Do

    '        'LoginResult = LoginForm.ShowDialog(Me)

    '        'If LoginResult = Windows.Forms.DialogResult.Cancel Then
    '        '    Application.Exit()
    '        'End If

    '        'Username = LoginForm.InteractiveUserUsername     ' "Operator"
    '        'UserPassword = LoginForm.InteractiveUserPassword ' "OperatorPassword"

    '        Username = "Operator"
    '        UserPassword = "OperatorPassword"

    '        gFunctions.InteractiveUserUsername = Username
    '        gFunctions.InteractiveUserPassword = UserPassword

    '        Try
    '            EncryptedPassword = gFunctions.XMLEncrypt(UserPassword, gDecryptedEncryptionKey)
    '            UserSecurityLevel = gFunctions.ConfigClient.PerformCADSAuthentication(Username, EncryptedPassword, CADSVersion, False)

    '            Debug.Print(UserSecurityLevel)

    '            'gFunctions.StopClient()

    '            'GlobalSettings.Initialise()

    '            'LoginForm.AllowWindowsLogon = False

    '            '


    '            'UserSecurityLevel = gConfigServer.PerformCADSAuthentication(Username, gFunctions.XMLEncrypt(UserPassword, gDecryptedEncryptionKey), CADSVersion, False)
    '            '''''''''''''''UserSecurityLevel = gConfigServer.PerformCADSAuthentication(InteractiveUserUsername, InteractiveUserPassword, CADSVersion, PerformAutoLogon)
    '        Catch ex1 As System.ServiceModel.EndpointNotFoundException
    '            MsgBox("The Configuration server could not be contacted.  This is a critical error.  The Operator Console cannot continue." & vbCrLf & vbCrLf & "The actual error returned was:" & vbCrLf & ex1.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Critical error")
    '            Application.Exit()
    '        Catch ex2 As Exception
    '            MsgBox("An unexpected error has occured.  This is a critical error.  The Operator Console cannot continue." & vbCrLf & vbCrLf & "The error message returned was:" & vbCrLf & ex2.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Critical error")
    '            Application.Exit()
    '        End Try
    '    Loop Until UserSecurityLevel <> Constants.DEFAULTSTRINGVALUE

    '    'LoginForm.Close()
    '    'LoginForm = Nothing

    '    UserSecurityLevel = ""

    '    If UserSecurityLevel <> Constants.DEFAULTSTRINGVALUE Then ' Logon is ok
    '        ' Backdoor...
    '        If Username = "Stratatel" Then
    '            gFunctions.InteractiveUserUsername = "SysAdmin"
    '        End If

    '        'gSettings = gConfigServer.MySettings(gFunctions.InteractiveUserUsername, gFunctions.CurrentADSitename)

    '        gFunctions.InteractiveUserIsAuthenticated = True

    '        Me.Text = "Operator Console (Logged on as " & gFunctions.InteractiveUserUsername & ")"

    '        Me.Show()
    '        Me.Refresh()
    '        DoStartupConfig()

    '        Dim SearchForm As frmSearch = ShowSearchForm()

    '        'SearchForm.WindowState = FormWindowState.Maximized
    '    End If

    'End Sub

    Private Sub ShowNewPersonForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click, NewPersonToolStripButton.Click, NewWindowToolStripMenuItem.Click
        ' Create a new instance of the child form.
        Dim ChildForm As New frmNewPerson
        ' Make it a child of this MDI form before showing it.
        'ChildForm.MdiParent = Me

        'm_ChildFormNumber += 1
        ChildForm.Text = "New Person" ' (" & m_ChildFormNumber & ")"

        ChildForm.Show()

    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim OpenFileDialog As New OpenFileDialog

        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub ShowMaintainHierarchyForm()
        Dim MaintainHierarchyForm As New frmMaintainOU(gFunctions.TargetDirectoryUserRootDN, gFunctions.TargetDirectoryPhoneRootDN)

        MaintainHierarchyForm.ShowDialog()

    End Sub

    Private Sub ShowSearchForm()
        Console.WriteLine("Creating SearchForm")
        Dim SearchForm As New frmUserSearch

        SearchForm.MdiParent = Me
        Console.WriteLine("Showing SearchForm")
        SearchForm.Show()

        'Return SearchForm

        ' TODO:  Check configuration to see whether we open forms maximised
        'SearchForm.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub DoStartupConfig()
        Dim SearchRoot As String = ""
        Dim UserRoot As String = ""
        Dim PhoneRoot As String = ""
        Dim ConfigurationRoot As String = ""
        Dim ResultCode As Utility.Types.ERRORTYPE = SUCCESS
        Dim ImagePath As String = ""

        Try
            gAppDataPath = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData
        Catch ex As Exception
            gFunctions.FileLoggingObject.WriteErrorEvent("Could not get path to the Application Data directory!  The error returned is contained on the following line(s)")
            gFunctions.FileLoggingObject.WriteErrorEvent(ex.Message)
        End Try

        ' Set up Error handling.
        'AddHandler CurrentApplicationDomain.UnhandledException, AddressOf UnhandledErrorHandler
        'AddHandler Application.ThreadException, AddressOf UnhandledFormErrorHandler

        'gFunctions.UpdateCADSConfigFile()      ' Copy cads.config to my PC

        ' Perform the actual logon.  If logon is successful, copy the [user].config to my PC
        '
        Dim LogonResult As String = ""
        'LogonResult = gFunctions.DoLogon("Operator", "operatorpassword", False)
        LogonResult = gFunctions.DoLogon()
        If LogonResult = Constants.DEFAULTSTRINGVALUE Then
            MsgBox("Your logon attempt has failed.")
            End
        End If
        gFunctions.CloneCollection(gFunctions.ConfigSettings, gSettings)  ' Get the Users settings into a collection so I can retrieve values directly
        ShowSearchForm()

        Try
            ' Get background image path
            ImagePath = My.Application.Info.DirectoryPath & "\Images\" & gFunctions.ValueFromSetting(gFunctions.CollectionValue("BackgroundImage", gSettings))
            If System.IO.File.Exists(ImagePath) Then
                Me.BackgroundImage = Image.FromFile(ImagePath)
                Me.BackgroundImageLayout = ImageLayout.Stretch
            Else
                ' Copy the image file to this machine
                'System.IO.File.Copy
            End If

        Catch ex As Exception
            MsgBox("Configuration settings could not be loaded.  This is a critical error.  The Operator Console cannot continue." & vbCrLf & _
                   vbCrLf & "The returned error was: " & vbCrLf & _
                   ex.Message & vbCrLf & vbCrLf & _
                   "Stratatel reference: " & gFunctions.GetErrorTypeAndCode(gFunctions.LastStatus.CADS_ERRORCODE) _
                   , MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Critical error")
            ' TODO:  Load cached settings
            End
        End Try

        Me.Text = "Operator Console (Logged on as " & gFunctions.InteractiveUserUsername & ")"

        SizeForm()
    End Sub

    'Private Function DetermineTargetServerName(ByVal ServerNames() As String) As String
    '    Dim Server As String
    '    Dim Index As Integer = 0
    '    Dim ServerUp As Boolean = False

    '    Do
    '        Server = ServerNames(Index)
    '        If Server.Contains(":") Then
    '            Server = Server.Substring(0, Server.IndexOf(":"))
    '        End If

    '        Try
    '            If My.Computer.Network.Ping(Server) Then
    '                ServerUp = True
    '            End If
    '        Catch ex As Exception
    '            ' Could not resolve this name - just as good as a ping failure
    '        End Try
    '        Index += 1
    '    Loop Until Index = 3 Or ServerUp

    '    If ServerUp Then
    '        Return ServerNames(Index - 1)
    '    Else
    '        Return Constants.DEFAULTSTRINGVALUE
    '    End If
    'End Function

    Private Sub SearchToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchToolStripButton.Click
        ShowSearchForm()
    End Sub

    Private Sub NewPhoneToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewPhoneToolStripButton.Click
        ' Create a new instance of the child form.
        Dim ChildForm As New frmNewPhone
        ' Make it a child of this MDI form before showing it.
        'ChildForm.MdiParent = Me

        'm_ChildFormNumber += 1
        ChildForm.Text = "New Phone" ' (" & m_ChildFormNumber & ")"

        ChildForm.Show()
    End Sub

    Private Sub MaintainHierarchyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaintainHierarchyToolStripMenuItem.Click
        ShowMaintainHierarchyForm()
    End Sub

    Private Sub LogoffToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoffToolStripMenuItem.Click
        gFunctions.InteractiveUserIsAuthenticated = False
        gFunctions.InteractiveUserUsername = ""
        gFunctions.InteractiveUserPassword = ""

        For Each Child As frmUserSearch In Me.MdiChildren
            Child.CanBeClosed = True
            Child.Close()
        Next

        gFunctions.DoLogon()
        Me.Text = "Operator Console (Logged on as " & gFunctions.InteractiveUserUsername & ")"
        ShowSearchForm()
    End Sub

    Private Sub OUSearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OUSearchToolStripMenuItem.Click
        ShowSearchOUForm
    End Sub

    Private Sub PhoneSearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PhoneSearchToolStripMenuItem.Click
        Dim PhoneSearchForm As New frmPhoneSearch

        PhoneSearchForm.Show()

    End Sub

    Private Sub ShowSearchOUForm()
        Dim ShowOUForm As New frmOUSearch(gFunctions.TargetDirectoryUserRootDN, gFunctions.TargetDirectoryPhoneRootDN)

        ShowOUForm.ShowDialog()

    End Sub

End Class
