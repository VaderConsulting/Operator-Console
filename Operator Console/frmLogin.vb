Public Class frmLogin

    'Private m_IsAuthenticated As Boolean = False
    'Private m_Username As String = ""
    'Private m_Password As String = ""

    'Public ReadOnly Property IsAuthenticated() As Boolean
    '    Get
    '        Return m_IsAuthenticated
    '    End Get
    'End Property

    'Public ReadOnly Property Username() As String
    '    Get
    '        Return m_Username
    '    End Get
    'End Property

    'Public ReadOnly Property Password() As String
    '    Get
    '        Return m_Password
    '    End Get
    'End Property

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        ' TODO:  Authenticate the user
        'm_Username = txtUsername.Text
        'm_Password = txtPassword.Text
        gUsername = txtUsername.Text
        gPassword = txtPassword.Text
        gAuthenticated = True
        'm_IsAuthenticated = True

        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        'm_IsAuthenticated = False
        gAuthenticated = False
        Me.Close()
    End Sub

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = My.Application.Info.Title & " - Login"
        Me.Show()
        Me.txtUsername.Text = My.User.Name
        Me.txtPassword.Focus()
    End Sub
End Class
