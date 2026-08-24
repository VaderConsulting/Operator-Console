<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheckPassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCheckPassword))
        Me.txtEncrypted1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtClearText1 = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtEncrypted2 = New System.Windows.Forms.TextBox
        Me.txtClearText2 = New System.Windows.Forms.TextBox
        Me.btnSimpleDecrypt = New System.Windows.Forms.Button
        Me.btnDecrypt = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtEncrypted1
        '
        Me.txtEncrypted1.Location = New System.Drawing.Point(36, 30)
        Me.txtEncrypted1.Name = "txtEncrypted1"
        Me.txtEncrypted1.Size = New System.Drawing.Size(170, 20)
        Me.txtEncrypted1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Encrypted"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Clear text"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSimpleDecrypt)
        Me.GroupBox1.Controls.Add(Me.txtClearText1)
        Me.GroupBox1.Controls.Add(Me.txtEncrypted1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(297, 105)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Encryption Key"
        '
        'txtClearText1
        '
        Me.txtClearText1.Location = New System.Drawing.Point(36, 71)
        Me.txtClearText1.Name = "txtClearText1"
        Me.txtClearText1.Size = New System.Drawing.Size(170, 20)
        Me.txtClearText1.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnDecrypt)
        Me.GroupBox2.Controls.Add(Me.txtClearText2)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtEncrypted2)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 132)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(297, 105)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Connection Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Encrypted"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Clear text"
        '
        'txtEncrypted2
        '
        Me.txtEncrypted2.Location = New System.Drawing.Point(36, 32)
        Me.txtEncrypted2.Name = "txtEncrypted2"
        Me.txtEncrypted2.Size = New System.Drawing.Size(170, 20)
        Me.txtEncrypted2.TabIndex = 0
        '
        'txtClearText2
        '
        Me.txtClearText2.Location = New System.Drawing.Point(36, 71)
        Me.txtClearText2.Name = "txtClearText2"
        Me.txtClearText2.Size = New System.Drawing.Size(170, 20)
        Me.txtClearText2.TabIndex = 3
        '
        'btnSimpleDecrypt
        '
        Me.btnSimpleDecrypt.Location = New System.Drawing.Point(212, 28)
        Me.btnSimpleDecrypt.Name = "btnSimpleDecrypt"
        Me.btnSimpleDecrypt.Size = New System.Drawing.Size(75, 23)
        Me.btnSimpleDecrypt.TabIndex = 4
        Me.btnSimpleDecrypt.Text = "Decrypt"
        Me.btnSimpleDecrypt.UseVisualStyleBackColor = True
        '
        'btnDecrypt
        '
        Me.btnDecrypt.Enabled = False
        Me.btnDecrypt.Location = New System.Drawing.Point(212, 30)
        Me.btnDecrypt.Name = "btnDecrypt"
        Me.btnDecrypt.Size = New System.Drawing.Size(75, 23)
        Me.btnDecrypt.TabIndex = 5
        Me.btnDecrypt.Text = "Decrypt"
        Me.btnDecrypt.UseVisualStyleBackColor = True
        '
        'frmCheckPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(316, 247)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCheckPassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Check Password"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtEncrypted1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtClearText1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSimpleDecrypt As System.Windows.Forms.Button
    Friend WithEvents txtClearText2 As System.Windows.Forms.TextBox
    Friend WithEvents txtEncrypted2 As System.Windows.Forms.TextBox
    Friend WithEvents btnDecrypt As System.Windows.Forms.Button
End Class
