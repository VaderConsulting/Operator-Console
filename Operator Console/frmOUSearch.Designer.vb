<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOUSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOUSearch))
        Me.btnOK = New System.Windows.Forms.Button
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lstOU = New System.Windows.Forms.ListBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.radCode = New System.Windows.Forms.RadioButton
        Me.radName = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.tabOUSearch = New System.Windows.Forms.TabControl
        Me.tabText = New System.Windows.Forms.TabPage
        Me.tvwSelected = New System.Windows.Forms.TreeView
        Me.tabBrowse = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tvwDirectory = New System.Windows.Forms.TreeView
        Me.btnClose = New System.Windows.Forms.Button
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOUSearch.SuspendLayout()
        Me.tabText.SuspendLayout()
        Me.tabBrowse.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(301, 386)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(12, 137)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 13
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 31)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 11
        Me.PictureBox1.TabStop = False
        '
        'lstOU
        '
        Me.lstOU.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstOU.FormattingEnabled = True
        Me.lstOU.Location = New System.Drawing.Point(6, 59)
        Me.lstOU.Name = "lstOU"
        Me.lstOU.Size = New System.Drawing.Size(314, 147)
        Me.lstOU.TabIndex = 5
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(245, 6)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(7, 8)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(233, 20)
        Me.txtSearch.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Search for OU"
        '
        'radCode
        '
        Me.radCode.AutoSize = True
        Me.radCode.Checked = True
        Me.radCode.Location = New System.Drawing.Point(131, 34)
        Me.radCode.Name = "radCode"
        Me.radCode.Size = New System.Drawing.Size(50, 17)
        Me.radCode.TabIndex = 3
        Me.radCode.TabStop = True
        Me.radCode.Text = "Code"
        Me.radCode.UseVisualStyleBackColor = True
        '
        'radName
        '
        Me.radName.AutoSize = True
        Me.radName.Location = New System.Drawing.Point(187, 34)
        Me.radName.Name = "radName"
        Me.radName.Size = New System.Drawing.Size(53, 17)
        Me.radName.TabIndex = 4
        Me.radName.Text = "Name"
        Me.radName.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Search on"
        '
        'tabOUSearch
        '
        Me.tabOUSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabOUSearch.Controls.Add(Me.tabText)
        Me.tabOUSearch.Controls.Add(Me.tabBrowse)
        Me.tabOUSearch.Location = New System.Drawing.Point(123, 12)
        Me.tabOUSearch.Name = "tabOUSearch"
        Me.tabOUSearch.SelectedIndex = 0
        Me.tabOUSearch.Size = New System.Drawing.Size(334, 368)
        Me.tabOUSearch.TabIndex = 0
        '
        'tabText
        '
        Me.tabText.Controls.Add(Me.tvwSelected)
        Me.tabText.Controls.Add(Me.lstOU)
        Me.tabText.Controls.Add(Me.Label2)
        Me.tabText.Controls.Add(Me.txtSearch)
        Me.tabText.Controls.Add(Me.radName)
        Me.tabText.Controls.Add(Me.btnSearch)
        Me.tabText.Controls.Add(Me.radCode)
        Me.tabText.Location = New System.Drawing.Point(4, 22)
        Me.tabText.Name = "tabText"
        Me.tabText.Padding = New System.Windows.Forms.Padding(3)
        Me.tabText.Size = New System.Drawing.Size(326, 342)
        Me.tabText.TabIndex = 0
        Me.tabText.Text = "Text"
        Me.tabText.UseVisualStyleBackColor = True
        '
        'tvwSelected
        '
        Me.tvwSelected.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tvwSelected.Location = New System.Drawing.Point(3, 212)
        Me.tvwSelected.Name = "tvwSelected"
        Me.tvwSelected.Size = New System.Drawing.Size(320, 127)
        Me.tvwSelected.TabIndex = 16
        '
        'tabBrowse
        '
        Me.tabBrowse.Controls.Add(Me.Panel1)
        Me.tabBrowse.Location = New System.Drawing.Point(4, 22)
        Me.tabBrowse.Name = "tabBrowse"
        Me.tabBrowse.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBrowse.Size = New System.Drawing.Size(326, 342)
        Me.tabBrowse.TabIndex = 1
        Me.tabBrowse.Text = "Browse"
        Me.tabBrowse.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tvwDirectory)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 336)
        Me.Panel1.TabIndex = 0
        '
        'tvwDirectory
        '
        Me.tvwDirectory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwDirectory.Location = New System.Drawing.Point(3, 3)
        Me.tvwDirectory.Name = "tvwDirectory"
        Me.tvwDirectory.Size = New System.Drawing.Size(314, 330)
        Me.tvwDirectory.TabIndex = 15
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(382, 386)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 25
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmOUSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 418)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.tabOUSearch)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(480, 452)
        Me.Name = "frmOUSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "OU Search"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOUSearch.ResumeLayout(False)
        Me.tabText.ResumeLayout(False)
        Me.tabText.PerformLayout()
        Me.tabBrowse.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lstOU As System.Windows.Forms.ListBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents radCode As System.Windows.Forms.RadioButton
    Friend WithEvents radName As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tabOUSearch As System.Windows.Forms.TabControl
    Friend WithEvents tabText As System.Windows.Forms.TabPage
    Friend WithEvents tabBrowse As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tvwDirectory As System.Windows.Forms.TreeView
    Friend WithEvents tvwSelected As System.Windows.Forms.TreeView
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
