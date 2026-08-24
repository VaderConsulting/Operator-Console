<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.radListManager = New System.Windows.Forms.RadioButton
        Me.radConfigServer = New System.Windows.Forms.RadioButton
        Me.radOperatorConsole = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUsername = New System.Windows.Forms.TextBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.btnGeneratePassword = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Help = New System.Windows.Forms.HelpProvider
        Me.btnCheckPassword = New System.Windows.Forms.Button
        Me.btnCreate = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.radListManager)
        Me.Panel1.Controls.Add(Me.radConfigServer)
        Me.Panel1.Controls.Add(Me.radOperatorConsole)
        Me.Help.SetHelpString(Me.Panel1, "Select the appropriate application or system")
        Me.Panel1.Location = New System.Drawing.Point(13, 13)
        Me.Panel1.Name = "Panel1"
        Me.Help.SetShowHelp(Me.Panel1, True)
        Me.Panel1.Size = New System.Drawing.Size(309, 165)
        Me.Panel1.TabIndex = 0
        '
        'radListManager
        '
        Me.radListManager.AutoSize = True
        Me.radListManager.Location = New System.Drawing.Point(3, 49)
        Me.radListManager.Name = "radListManager"
        Me.radListManager.Size = New System.Drawing.Size(127, 17)
        Me.radListManager.TabIndex = 1
        Me.radListManager.Text = "CADS 4 List Manager"
        Me.radListManager.UseVisualStyleBackColor = True
        '
        'radConfigServer
        '
        Me.radConfigServer.AutoSize = True
        Me.radConfigServer.Location = New System.Drawing.Point(3, 26)
        Me.radConfigServer.Name = "radConfigServer"
        Me.radConfigServer.Size = New System.Drawing.Size(130, 17)
        Me.radConfigServer.TabIndex = 0
        Me.radConfigServer.Text = "CADS 4 Config Server"
        Me.radConfigServer.UseVisualStyleBackColor = True
        '
        'radOperatorConsole
        '
        Me.radOperatorConsole.AutoSize = True
        Me.radOperatorConsole.Checked = True
        Me.radOperatorConsole.Location = New System.Drawing.Point(3, 3)
        Me.radOperatorConsole.Name = "radOperatorConsole"
        Me.radOperatorConsole.Size = New System.Drawing.Size(148, 17)
        Me.radOperatorConsole.TabIndex = 0
        Me.radOperatorConsole.TabStop = True
        Me.radOperatorConsole.Text = "CADS 4 Operator Console"
        Me.radOperatorConsole.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 219)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Username"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 242)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Password"
        '
        'txtUsername
        '
        Me.txtUsername.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Help.SetHelpString(Me.txtUsername, "The Username to use")
        Me.txtUsername.Location = New System.Drawing.Point(74, 216)
        Me.txtUsername.Name = "txtUsername"
        Me.Help.SetShowHelp(Me.txtUsername, True)
        Me.txtUsername.Size = New System.Drawing.Size(248, 20)
        Me.txtUsername.TabIndex = 3
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Help.SetHelpString(Me.txtPassword, "The Password to use")
        Me.txtPassword.Location = New System.Drawing.Point(74, 239)
        Me.txtPassword.Name = "txtPassword"
        Me.Help.SetShowHelp(Me.txtPassword, True)
        Me.txtPassword.Size = New System.Drawing.Size(248, 20)
        Me.txtPassword.TabIndex = 3
        '
        'btnGeneratePassword
        '
        Me.btnGeneratePassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Help.SetHelpString(Me.btnGeneratePassword, "Create time-sensitive Password")
        Me.btnGeneratePassword.Location = New System.Drawing.Point(12, 184)
        Me.btnGeneratePassword.Name = "btnGeneratePassword"
        Me.Help.SetShowHelp(Me.btnGeneratePassword, True)
        Me.btnGeneratePassword.Size = New System.Drawing.Size(75, 23)
        Me.btnGeneratePassword.TabIndex = 4
        Me.btnGeneratePassword.Text = "Generate"
        Me.btnGeneratePassword.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 274)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(214, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "These passwords are date sensitive!"
        '
        'btnCheckPassword
        '
        Me.btnCheckPassword.Location = New System.Drawing.Point(247, 184)
        Me.btnCheckPassword.Name = "btnCheckPassword"
        Me.btnCheckPassword.Size = New System.Drawing.Size(75, 23)
        Me.btnCheckPassword.TabIndex = 6
        Me.btnCheckPassword.Text = "Check Password"
        Me.btnCheckPassword.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(130, 184)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(75, 23)
        Me.btnCreate.TabIndex = 7
        Me.btnCreate.Text = "Create"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(342, 296)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.btnCheckPassword)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnGeneratePassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.HelpButton = True
        Me.Help.SetHelpString(Me, "Stratatel Backdoor")
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(350, 768)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(350, 215)
        Me.Name = "frmMain"
        Me.Help.SetShowHelp(Me, True)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stratatel Password Generator"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents radConfigServer As System.Windows.Forms.RadioButton
    Friend WithEvents radOperatorConsole As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnGeneratePassword As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents radListManager As System.Windows.Forms.RadioButton
    Friend WithEvents Help As System.Windows.Forms.HelpProvider
    Friend WithEvents btnCheckPassword As System.Windows.Forms.Button
    Friend WithEvents btnCreate As System.Windows.Forms.Button

End Class
