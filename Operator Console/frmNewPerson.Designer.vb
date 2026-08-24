<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewPerson
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewPerson))
        Me.tvwDirectory = New System.Windows.Forms.TreeView
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.pnlDetails = New System.Windows.Forms.Panel
        Me.lblReadme = New System.Windows.Forms.Label
        Me.btnPhones = New System.Windows.Forms.Button
        Me.Tooltips = New System.Windows.Forms.ToolTip(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.txtMandatory = New System.Windows.Forms.TextBox
        Me.btnSetOU = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.pnlDetails.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tvwDirectory
        '
        Me.tvwDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwDirectory.Enabled = False
        Me.tvwDirectory.Location = New System.Drawing.Point(12, 234)
        Me.tvwDirectory.Name = "tvwDirectory"
        Me.tvwDirectory.Size = New System.Drawing.Size(523, 92)
        Me.tvwDirectory.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 218)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(140, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Organisational Unit Location"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Enabled = False
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(298, 333)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "Save"
        Me.Tooltips.SetToolTip(Me.btnSave, "Save")
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'pnlDetails
        '
        Me.pnlDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlDetails.AutoScroll = True
        Me.pnlDetails.Controls.Add(Me.lblReadme)
        Me.pnlDetails.Location = New System.Drawing.Point(117, 12)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(419, 206)
        Me.pnlDetails.TabIndex = 21
        '
        'lblReadme
        '
        Me.lblReadme.AutoSize = True
        Me.lblReadme.BackColor = System.Drawing.Color.Gold
        Me.lblReadme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReadme.Location = New System.Drawing.Point(80, 87)
        Me.lblReadme.Name = "lblReadme"
        Me.lblReadme.Size = New System.Drawing.Size(272, 13)
        Me.lblReadme.TabIndex = 22
        Me.lblReadme.Text = "The controls in this panel are added at runtime"
        Me.lblReadme.Visible = False
        '
        'btnPhones
        '
        Me.btnPhones.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPhones.Image = CType(resources.GetObject("btnPhones.Image"), System.Drawing.Image)
        Me.btnPhones.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPhones.Location = New System.Drawing.Point(379, 333)
        Me.btnPhones.Name = "btnPhones"
        Me.btnPhones.Size = New System.Drawing.Size(75, 23)
        Me.btnPhones.TabIndex = 22
        Me.btnPhones.Text = "Phones"
        Me.Tooltips.SetToolTip(Me.btnPhones, "Phones")
        Me.btnPhones.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 365)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(547, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 23
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(70, 17)
        Me.lblStatus.Text = "New Person"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 28
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(12, 118)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 27
        Me.PictureBox2.TabStop = False
        '
        'txtMandatory
        '
        Me.txtMandatory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtMandatory.Location = New System.Drawing.Point(12, 333)
        Me.txtMandatory.Name = "txtMandatory"
        Me.txtMandatory.ReadOnly = True
        Me.txtMandatory.Size = New System.Drawing.Size(100, 20)
        Me.txtMandatory.TabIndex = 29
        Me.txtMandatory.Text = "Mandatory Data"
        Me.txtMandatory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSetOU
        '
        Me.btnSetOU.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSetOU.Image = CType(resources.GetObject("btnSetOU.Image"), System.Drawing.Image)
        Me.btnSetOU.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSetOU.Location = New System.Drawing.Point(217, 333)
        Me.btnSetOU.Name = "btnSetOU"
        Me.btnSetOU.Size = New System.Drawing.Size(75, 23)
        Me.btnSetOU.TabIndex = 18
        Me.btnSetOU.Text = "Set OU"
        Me.btnSetOU.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(460, 333)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 30
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmNewPerson
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(547, 387)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.txtMandatory)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnPhones)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.tvwDirectory)
        Me.Controls.Add(Me.btnSetOU)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Label8)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(563, 423)
        Me.Name = "frmNewPerson"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New Person"
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlDetails.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tvwDirectory As System.Windows.Forms.TreeView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents lblReadme As System.Windows.Forms.Label
    Friend WithEvents btnPhones As System.Windows.Forms.Button
    Friend WithEvents Tooltips As System.Windows.Forms.ToolTip
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents txtMandatory As System.Windows.Forms.TextBox
    Friend WithEvents btnSetOU As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
