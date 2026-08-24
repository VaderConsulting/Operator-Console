<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDetails))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtName2 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtGivenName2 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtSurname2 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtTelephoneNumber2 = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtMobileNumber2 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDepartment2 = New System.Windows.Forms.TextBox
        Me.tvwDirectory = New System.Windows.Forms.TreeView
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtDescription2 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnEdit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pnlDetails = New System.Windows.Forms.Panel
        Me.lblReadme = New System.Windows.Forms.Label
        Me.btnPhones = New System.Windows.Forms.Button
        Me.Tooltips = New System.Windows.Forms.ToolTip(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel1.SuspendLayout()
        Me.pnlDetails.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name"
        '
        'txtName2
        '
        Me.txtName2.Location = New System.Drawing.Point(117, 6)
        Me.txtName2.Name = "txtName2"
        Me.txtName2.ReadOnly = True
        Me.txtName2.Size = New System.Drawing.Size(385, 20)
        Me.txtName2.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Given Name"
        '
        'txtGivenName2
        '
        Me.txtGivenName2.Location = New System.Drawing.Point(117, 33)
        Me.txtGivenName2.Name = "txtGivenName2"
        Me.txtGivenName2.ReadOnly = True
        Me.txtGivenName2.Size = New System.Drawing.Size(134, 20)
        Me.txtGivenName2.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(314, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Surname"
        '
        'txtSurname2
        '
        Me.txtSurname2.Location = New System.Drawing.Point(369, 33)
        Me.txtSurname2.Name = "txtSurname2"
        Me.txtSurname2.ReadOnly = True
        Me.txtSurname2.Size = New System.Drawing.Size(134, 20)
        Me.txtSurname2.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Telephone Number"
        '
        'txtTelephoneNumber2
        '
        Me.txtTelephoneNumber2.Location = New System.Drawing.Point(117, 59)
        Me.txtTelephoneNumber2.Name = "txtTelephoneNumber2"
        Me.txtTelephoneNumber2.ReadOnly = True
        Me.txtTelephoneNumber2.Size = New System.Drawing.Size(134, 20)
        Me.txtTelephoneNumber2.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(285, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Mobile Number"
        '
        'txtMobileNumber2
        '
        Me.txtMobileNumber2.Location = New System.Drawing.Point(369, 59)
        Me.txtMobileNumber2.Name = "txtMobileNumber2"
        Me.txtMobileNumber2.ReadOnly = True
        Me.txtMobileNumber2.Size = New System.Drawing.Size(134, 20)
        Me.txtMobileNumber2.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Department"
        '
        'txtDepartment2
        '
        Me.txtDepartment2.Location = New System.Drawing.Point(117, 89)
        Me.txtDepartment2.Name = "txtDepartment2"
        Me.txtDepartment2.ReadOnly = True
        Me.txtDepartment2.Size = New System.Drawing.Size(385, 20)
        Me.txtDepartment2.TabIndex = 11
        '
        'tvwDirectory
        '
        Me.tvwDirectory.Location = New System.Drawing.Point(16, 274)
        Me.tvwDirectory.Name = "tvwDirectory"
        Me.tvwDirectory.Size = New System.Drawing.Size(504, 154)
        Me.tvwDirectory.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 121)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Description"
        '
        'txtDescription2
        '
        Me.txtDescription2.Location = New System.Drawing.Point(117, 116)
        Me.txtDescription2.Name = "txtDescription2"
        Me.txtDescription2.ReadOnly = True
        Me.txtDescription2.Size = New System.Drawing.Size(386, 20)
        Me.txtDescription2.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 258)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(136, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Organisational Unit location"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEdit.Location = New System.Drawing.Point(419, 434)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(100, 23)
        Me.btnEdit.TabIndex = 17
        Me.btnEdit.Text = "Edit (F2)"
        Me.Tooltips.SetToolTip(Me.btnEdit, "Edit")
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(207, 434)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 23)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "Save"
        Me.Tooltips.SetToolTip(Me.btnSave, "Save")
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtName2)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtGivenName2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtDescription2)
        Me.Panel1.Controls.Add(Me.txtSurname2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtDepartment2)
        Me.Panel1.Controls.Add(Me.txtTelephoneNumber2)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtMobileNumber2)
        Me.Panel1.Location = New System.Drawing.Point(623, 47)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(588, 411)
        Me.Panel1.TabIndex = 20
        '
        'pnlDetails
        '
        Me.pnlDetails.AutoScroll = True
        Me.pnlDetails.Controls.Add(Me.lblReadme)
        Me.pnlDetails.Location = New System.Drawing.Point(0, 0)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(521, 251)
        Me.pnlDetails.TabIndex = 21
        '
        'lblReadme
        '
        Me.lblReadme.AutoSize = True
        Me.lblReadme.BackColor = System.Drawing.Color.Gold
        Me.lblReadme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReadme.Location = New System.Drawing.Point(119, 106)
        Me.lblReadme.Name = "lblReadme"
        Me.lblReadme.Size = New System.Drawing.Size(272, 13)
        Me.lblReadme.TabIndex = 22
        Me.lblReadme.Text = "The controls in this panel are added at runtime"
        Me.lblReadme.Visible = False
        '
        'btnPhones
        '
        Me.btnPhones.Image = CType(resources.GetObject("btnPhones.Image"), System.Drawing.Image)
        Me.btnPhones.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPhones.Location = New System.Drawing.Point(313, 434)
        Me.btnPhones.Name = "btnPhones"
        Me.btnPhones.Size = New System.Drawing.Size(100, 23)
        Me.btnPhones.TabIndex = 22
        Me.btnPhones.Text = "Phones"
        Me.Tooltips.SetToolTip(Me.btnPhones, "Phones")
        Me.btnPhones.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 467)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(531, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 23
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(56, 17)
        Me.lblStatus.Text = "Readonly"
        '
        'frmDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 489)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnPhones)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.tvwDirectory)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.Label8)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Person Details"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlDetails.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtGivenName2 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSurname2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTelephoneNumber2 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMobileNumber2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDepartment2 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDescription2 As System.Windows.Forms.TextBox
    Friend WithEvents tvwDirectory As System.Windows.Forms.TreeView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents lblReadme As System.Windows.Forms.Label
    Friend WithEvents btnPhones As System.Windows.Forms.Button
    Friend WithEvents Tooltips As System.Windows.Forms.ToolTip
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
End Class
