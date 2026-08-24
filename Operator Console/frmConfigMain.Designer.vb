<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfigMain))
        Me.tabConfig = New System.Windows.Forms.TabControl
        Me.tabOUs = New System.Windows.Forms.TabPage
        Me.lblNoOUDefined = New System.Windows.Forms.Label
        Me.pnlOrganisationalUnit = New System.Windows.Forms.Panel
        Me.txtSelectedNode = New System.Windows.Forms.TextBox
        Me.lblSelectedNode = New System.Windows.Forms.Label
        Me.tvwSelectedBranch = New System.Windows.Forms.TreeView
        Me.tvwDirectory = New System.Windows.Forms.TreeView
        Me.imlTree = New System.Windows.Forms.ImageList(Me.components)
        Me.tabSites = New System.Windows.Forms.TabPage
        Me.lblNoSitesDefined = New System.Windows.Forms.Label
        Me.tvwSites = New System.Windows.Forms.TreeView
        Me.tabAccountCodes = New System.Windows.Forms.TabPage
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.tabConfig.SuspendLayout()
        Me.tabOUs.SuspendLayout()
        Me.pnlOrganisationalUnit.SuspendLayout()
        Me.tabSites.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabConfig
        '
        Me.tabConfig.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabConfig.Controls.Add(Me.tabOUs)
        Me.tabConfig.Controls.Add(Me.tabSites)
        Me.tabConfig.Controls.Add(Me.tabAccountCodes)
        Me.tabConfig.Location = New System.Drawing.Point(12, 12)
        Me.tabConfig.Name = "tabConfig"
        Me.tabConfig.SelectedIndex = 0
        Me.tabConfig.Size = New System.Drawing.Size(610, 395)
        Me.tabConfig.TabIndex = 0
        '
        'tabOUs
        '
        Me.tabOUs.Controls.Add(Me.lblNoOUDefined)
        Me.tabOUs.Controls.Add(Me.pnlOrganisationalUnit)
        Me.tabOUs.Controls.Add(Me.tvwDirectory)
        Me.tabOUs.Location = New System.Drawing.Point(4, 22)
        Me.tabOUs.Name = "tabOUs"
        Me.tabOUs.Size = New System.Drawing.Size(602, 369)
        Me.tabOUs.TabIndex = 2
        Me.tabOUs.Text = "Organisational Units"
        Me.tabOUs.UseVisualStyleBackColor = True
        '
        'lblNoOUDefined
        '
        Me.lblNoOUDefined.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNoOUDefined.AutoSize = True
        Me.lblNoOUDefined.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoOUDefined.Location = New System.Drawing.Point(106, 107)
        Me.lblNoOUDefined.Name = "lblNoOUDefined"
        Me.lblNoOUDefined.Size = New System.Drawing.Size(401, 13)
        Me.lblNoOUDefined.TabIndex = 4
        Me.lblNoOUDefined.Text = "There are no Organizational Units defined under your 'UserRoot' Path"
        '
        'pnlOrganisationalUnit
        '
        Me.pnlOrganisationalUnit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlOrganisationalUnit.Controls.Add(Me.txtSelectedNode)
        Me.pnlOrganisationalUnit.Controls.Add(Me.lblSelectedNode)
        Me.pnlOrganisationalUnit.Controls.Add(Me.tvwSelectedBranch)
        Me.pnlOrganisationalUnit.Location = New System.Drawing.Point(3, 240)
        Me.pnlOrganisationalUnit.Name = "pnlOrganisationalUnit"
        Me.pnlOrganisationalUnit.Size = New System.Drawing.Size(595, 106)
        Me.pnlOrganisationalUnit.TabIndex = 1
        '
        'txtSelectedNode
        '
        Me.txtSelectedNode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelectedNode.Location = New System.Drawing.Point(90, 3)
        Me.txtSelectedNode.Name = "txtSelectedNode"
        Me.txtSelectedNode.Size = New System.Drawing.Size(502, 20)
        Me.txtSelectedNode.TabIndex = 2
        '
        'lblSelectedNode
        '
        Me.lblSelectedNode.AutoSize = True
        Me.lblSelectedNode.Location = New System.Drawing.Point(3, 3)
        Me.lblSelectedNode.Name = "lblSelectedNode"
        Me.lblSelectedNode.Size = New System.Drawing.Size(81, 13)
        Me.lblSelectedNode.TabIndex = 1
        Me.lblSelectedNode.Text = "Selected Node:"
        '
        'tvwSelectedBranch
        '
        Me.tvwSelectedBranch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tvwSelectedBranch.Location = New System.Drawing.Point(3, 29)
        Me.tvwSelectedBranch.Name = "tvwSelectedBranch"
        Me.tvwSelectedBranch.Size = New System.Drawing.Size(214, 74)
        Me.tvwSelectedBranch.TabIndex = 0
        '
        'tvwDirectory
        '
        Me.tvwDirectory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwDirectory.ImageIndex = 0
        Me.tvwDirectory.ImageList = Me.imlTree
        Me.tvwDirectory.Location = New System.Drawing.Point(3, 3)
        Me.tvwDirectory.Name = "tvwDirectory"
        Me.tvwDirectory.SelectedImageIndex = 0
        Me.tvwDirectory.Size = New System.Drawing.Size(596, 231)
        Me.tvwDirectory.TabIndex = 0
        '
        'imlTree
        '
        Me.imlTree.ImageStream = CType(resources.GetObject("imlTree.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlTree.TransparentColor = System.Drawing.Color.Transparent
        Me.imlTree.Images.SetKeyName(0, "folder_closed.png")
        Me.imlTree.Images.SetKeyName(1, "folder_open_minus.png")
        Me.imlTree.Images.SetKeyName(2, "folder_open.png")
        Me.imlTree.Images.SetKeyName(3, "folder_open_plus.png")
        Me.imlTree.Images.SetKeyName(4, "folder_closed_plus.png")
        '
        'tabSites
        '
        Me.tabSites.Controls.Add(Me.lblNoSitesDefined)
        Me.tabSites.Controls.Add(Me.tvwSites)
        Me.tabSites.Location = New System.Drawing.Point(4, 22)
        Me.tabSites.Name = "tabSites"
        Me.tabSites.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSites.Size = New System.Drawing.Size(602, 369)
        Me.tabSites.TabIndex = 1
        Me.tabSites.Text = "Sites"
        Me.tabSites.UseVisualStyleBackColor = True
        '
        'lblNoSitesDefined
        '
        Me.lblNoSitesDefined.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNoSitesDefined.AutoSize = True
        Me.lblNoSitesDefined.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoSitesDefined.Location = New System.Drawing.Point(140, 107)
        Me.lblNoSitesDefined.Name = "lblNoSitesDefined"
        Me.lblNoSitesDefined.Size = New System.Drawing.Size(325, 13)
        Me.lblNoSitesDefined.TabIndex = 5
        Me.lblNoSitesDefined.Text = "There are no Sites defined under your 'PhoneRoot' Path"
        '
        'tvwSites
        '
        Me.tvwSites.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwSites.ImageIndex = 0
        Me.tvwSites.ImageList = Me.imlTree
        Me.tvwSites.Location = New System.Drawing.Point(3, 6)
        Me.tvwSites.Name = "tvwSites"
        Me.tvwSites.SelectedImageIndex = 0
        Me.tvwSites.Size = New System.Drawing.Size(596, 231)
        Me.tvwSites.TabIndex = 1
        '
        'tabAccountCodes
        '
        Me.tabAccountCodes.Location = New System.Drawing.Point(4, 22)
        Me.tabAccountCodes.Name = "tabAccountCodes"
        Me.tabAccountCodes.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAccountCodes.Size = New System.Drawing.Size(602, 369)
        Me.tabAccountCodes.TabIndex = 3
        Me.tabAccountCodes.Text = "Account Codes"
        Me.tabAccountCodes.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(547, 413)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(466, 413)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 439)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(634, 22)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "Idle"
        '
        'lblStatus
        '
        Me.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(26, 17)
        Me.lblStatus.Text = "Idle"
        '
        'frmConfigMain
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(634, 461)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.tabConfig)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfigMain"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configuration"
        Me.tabConfig.ResumeLayout(False)
        Me.tabOUs.ResumeLayout(False)
        Me.tabOUs.PerformLayout()
        Me.pnlOrganisationalUnit.ResumeLayout(False)
        Me.pnlOrganisationalUnit.PerformLayout()
        Me.tabSites.ResumeLayout(False)
        Me.tabSites.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabConfig As System.Windows.Forms.TabControl
    Friend WithEvents tabSites As System.Windows.Forms.TabPage
    Friend WithEvents tabOUs As System.Windows.Forms.TabPage
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents tabAccountCodes As System.Windows.Forms.TabPage
    Friend WithEvents pnlOrganisationalUnit As System.Windows.Forms.Panel
    Friend WithEvents tvwSelectedBranch As System.Windows.Forms.TreeView
    Friend WithEvents tvwDirectory As System.Windows.Forms.TreeView
    Friend WithEvents lblSelectedNode As System.Windows.Forms.Label
    Friend WithEvents imlTree As System.Windows.Forms.ImageList
    Friend WithEvents txtSelectedNode As System.Windows.Forms.TextBox
    Friend WithEvents lblNoOUDefined As System.Windows.Forms.Label
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tvwSites As System.Windows.Forms.TreeView
    Friend WithEvents lblNoSitesDefined As System.Windows.Forms.Label
End Class
