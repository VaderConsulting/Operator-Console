<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaintainOU
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaintainOU))
        Me.tabConfig = New System.Windows.Forms.TabControl
        Me.tabOUs = New System.Windows.Forms.TabPage
        Me.pnlOrganisationalUnit = New System.Windows.Forms.Panel
        Me.txtSelectedNode = New System.Windows.Forms.TextBox
        Me.lblSelectedNode = New System.Windows.Forms.Label
        Me.tvwDirectory = New System.Windows.Forms.TreeView
        Me.mnuTreeview = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddOU = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteOU = New System.Windows.Forms.ToolStripMenuItem
        Me.MoveOU = New System.Windows.Forms.ToolStripMenuItem
        Me.RenameOUToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.imlTree = New System.Windows.Forms.ImageList(Me.components)
        Me.btnOK = New System.Windows.Forms.Button
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.tabConfig.SuspendLayout()
        Me.tabOUs.SuspendLayout()
        Me.pnlOrganisationalUnit.SuspendLayout()
        Me.mnuTreeview.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabConfig
        '
        Me.tabConfig.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabConfig.Controls.Add(Me.tabOUs)
        Me.tabConfig.Location = New System.Drawing.Point(119, 25)
        Me.tabConfig.Name = "tabConfig"
        Me.tabConfig.SelectedIndex = 0
        Me.tabConfig.Size = New System.Drawing.Size(493, 383)
        Me.tabConfig.TabIndex = 0
        '
        'tabOUs
        '
        Me.tabOUs.Controls.Add(Me.pnlOrganisationalUnit)
        Me.tabOUs.Controls.Add(Me.tvwDirectory)
        Me.tabOUs.Location = New System.Drawing.Point(4, 22)
        Me.tabOUs.Name = "tabOUs"
        Me.tabOUs.Size = New System.Drawing.Size(485, 357)
        Me.tabOUs.TabIndex = 2
        Me.tabOUs.Text = "Organisational Units"
        Me.tabOUs.UseVisualStyleBackColor = True
        '
        'pnlOrganisationalUnit
        '
        Me.pnlOrganisationalUnit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlOrganisationalUnit.Controls.Add(Me.txtSelectedNode)
        Me.pnlOrganisationalUnit.Controls.Add(Me.lblSelectedNode)
        Me.pnlOrganisationalUnit.Location = New System.Drawing.Point(4, 325)
        Me.pnlOrganisationalUnit.Name = "pnlOrganisationalUnit"
        Me.pnlOrganisationalUnit.Size = New System.Drawing.Size(478, 29)
        Me.pnlOrganisationalUnit.TabIndex = 1
        '
        'txtSelectedNode
        '
        Me.txtSelectedNode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelectedNode.Location = New System.Drawing.Point(80, 3)
        Me.txtSelectedNode.Name = "txtSelectedNode"
        Me.txtSelectedNode.Size = New System.Drawing.Size(395, 20)
        Me.txtSelectedNode.TabIndex = 2
        '
        'lblSelectedNode
        '
        Me.lblSelectedNode.AutoSize = True
        Me.lblSelectedNode.Location = New System.Drawing.Point(3, 6)
        Me.lblSelectedNode.Name = "lblSelectedNode"
        Me.lblSelectedNode.Size = New System.Drawing.Size(71, 13)
        Me.lblSelectedNode.TabIndex = 1
        Me.lblSelectedNode.Text = "Selected OU:"
        '
        'tvwDirectory
        '
        Me.tvwDirectory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwDirectory.ContextMenuStrip = Me.mnuTreeview
        Me.tvwDirectory.ImageIndex = 0
        Me.tvwDirectory.ImageList = Me.imlTree
        Me.tvwDirectory.Location = New System.Drawing.Point(3, 3)
        Me.tvwDirectory.Name = "tvwDirectory"
        Me.tvwDirectory.SelectedImageIndex = 0
        Me.tvwDirectory.Size = New System.Drawing.Size(479, 316)
        Me.tvwDirectory.TabIndex = 0
        '
        'mnuTreeview
        '
        Me.mnuTreeview.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddOU, Me.DeleteOU, Me.MoveOU, Me.RenameOUToolStripMenuItem})
        Me.mnuTreeview.Name = "mnuTreeview"
        Me.mnuTreeview.Size = New System.Drawing.Size(138, 92)
        '
        'AddOU
        '
        Me.AddOU.Name = "AddOU"
        Me.AddOU.Size = New System.Drawing.Size(137, 22)
        Me.AddOU.Text = "Add OU"
        '
        'DeleteOU
        '
        Me.DeleteOU.Name = "DeleteOU"
        Me.DeleteOU.Size = New System.Drawing.Size(137, 22)
        Me.DeleteOU.Text = "Delete OU"
        '
        'MoveOU
        '
        Me.MoveOU.Name = "MoveOU"
        Me.MoveOU.Size = New System.Drawing.Size(137, 22)
        Me.MoveOU.Text = "Move OU"
        '
        'RenameOUToolStripMenuItem
        '
        Me.RenameOUToolStripMenuItem.Name = "RenameOUToolStripMenuItem"
        Me.RenameOUToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.RenameOUToolStripMenuItem.Text = "Rename OU"
        '
        'imlTree
        '
        Me.imlTree.ImageStream = CType(resources.GetObject("imlTree.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlTree.TransparentColor = System.Drawing.Color.Transparent
        Me.imlTree.Images.SetKeyName(0, "Folder_Closed.png")
        Me.imlTree.Images.SetKeyName(1, "Folder_Open.png")
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(456, 414)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(26, 17)
        Me.lblStatus.Text = "Idle"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 25)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(561, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Maintain the Directory Hierarchy by selecting the appropriate OU, and then right-" & _
            "click and select the required function."
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(12, 131)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(537, 414)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 25
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmMaintainOU
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(624, 449)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.tabConfig)
        Me.Controls.Add(Me.btnOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMaintainOU"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Maintain Hierarchy"
        Me.tabConfig.ResumeLayout(False)
        Me.tabOUs.ResumeLayout(False)
        Me.pnlOrganisationalUnit.ResumeLayout(False)
        Me.pnlOrganisationalUnit.PerformLayout()
        Me.mnuTreeview.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabConfig As System.Windows.Forms.TabControl
    Friend WithEvents tabOUs As System.Windows.Forms.TabPage
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents pnlOrganisationalUnit As System.Windows.Forms.Panel
    Friend WithEvents tvwDirectory As System.Windows.Forms.TreeView
    Friend WithEvents lblSelectedNode As System.Windows.Forms.Label
    Friend WithEvents imlTree As System.Windows.Forms.ImageList
    Friend WithEvents txtSelectedNode As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuTreeview As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddOU As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteOU As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveOU As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents RenameOUToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
