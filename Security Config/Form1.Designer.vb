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
        Me.lstLevels = New System.Windows.Forms.ListBox
        Me.grdSecurity = New System.Windows.Forms.DataGridView
        Me.chkRead = New System.Windows.Forms.CheckBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblDefault = New System.Windows.Forms.Label
        Me.chkModify = New System.Windows.Forms.CheckBox
        Me.chkAdd = New System.Windows.Forms.CheckBox
        Me.chkDelete = New System.Windows.Forms.CheckBox
        Me.btnSet = New System.Windows.Forms.Button
        CType(Me.grdSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstLevels
        '
        Me.lstLevels.FormattingEnabled = True
        Me.lstLevels.Location = New System.Drawing.Point(12, 38)
        Me.lstLevels.Name = "lstLevels"
        Me.lstLevels.Size = New System.Drawing.Size(183, 264)
        Me.lstLevels.TabIndex = 0
        '
        'grdSecurity
        '
        Me.grdSecurity.AllowUserToAddRows = False
        Me.grdSecurity.AllowUserToDeleteRows = False
        Me.grdSecurity.AllowUserToResizeColumns = False
        Me.grdSecurity.AllowUserToResizeRows = False
        Me.grdSecurity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSecurity.Location = New System.Drawing.Point(201, 38)
        Me.grdSecurity.Name = "grdSecurity"
        Me.grdSecurity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSecurity.Size = New System.Drawing.Size(413, 377)
        Me.grdSecurity.TabIndex = 1
        '
        'chkRead
        '
        Me.chkRead.AutoSize = True
        Me.chkRead.Location = New System.Drawing.Point(3, 3)
        Me.chkRead.Name = "chkRead"
        Me.chkRead.Size = New System.Drawing.Size(52, 17)
        Me.chkRead.TabIndex = 2
        Me.chkRead.Text = "Read"
        Me.chkRead.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnSet)
        Me.Panel1.Controls.Add(Me.chkDelete)
        Me.Panel1.Controls.Add(Me.chkAdd)
        Me.Panel1.Controls.Add(Me.chkModify)
        Me.Panel1.Controls.Add(Me.chkRead)
        Me.Panel1.Location = New System.Drawing.Point(13, 341)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(182, 91)
        Me.Panel1.TabIndex = 3
        '
        'lblDefault
        '
        Me.lblDefault.Location = New System.Drawing.Point(12, 322)
        Me.lblDefault.Name = "lblDefault"
        Me.lblDefault.Size = New System.Drawing.Size(183, 16)
        Me.lblDefault.TabIndex = 4
        Me.lblDefault.Text = "Default values (no selected level)"
        '
        'chkModify
        '
        Me.chkModify.AutoSize = True
        Me.chkModify.Location = New System.Drawing.Point(122, 3)
        Me.chkModify.Name = "chkModify"
        Me.chkModify.Size = New System.Drawing.Size(57, 17)
        Me.chkModify.TabIndex = 3
        Me.chkModify.Text = "Modify"
        Me.chkModify.UseVisualStyleBackColor = True
        '
        'chkAdd
        '
        Me.chkAdd.AutoSize = True
        Me.chkAdd.Location = New System.Drawing.Point(3, 26)
        Me.chkAdd.Name = "chkAdd"
        Me.chkAdd.Size = New System.Drawing.Size(45, 17)
        Me.chkAdd.TabIndex = 4
        Me.chkAdd.Text = "Add"
        Me.chkAdd.UseVisualStyleBackColor = True
        '
        'chkDelete
        '
        Me.chkDelete.AutoSize = True
        Me.chkDelete.Location = New System.Drawing.Point(122, 26)
        Me.chkDelete.Name = "chkDelete"
        Me.chkDelete.Size = New System.Drawing.Size(57, 17)
        Me.chkDelete.TabIndex = 5
        Me.chkDelete.Text = "Delete"
        Me.chkDelete.UseVisualStyleBackColor = True
        '
        'btnSet
        '
        Me.btnSet.Location = New System.Drawing.Point(103, 50)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(75, 23)
        Me.btnSet.TabIndex = 6
        Me.btnSet.Text = "Set"
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 444)
        Me.Controls.Add(Me.lblDefault)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdSecurity)
        Me.Controls.Add(Me.lstLevels)
        Me.Name = "frmMain"
        Me.Text = "Security Configuration"
        CType(Me.grdSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstLevels As System.Windows.Forms.ListBox
    Friend WithEvents grdSecurity As System.Windows.Forms.DataGridView
    Friend WithEvents chkRead As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkDelete As System.Windows.Forms.CheckBox
    Friend WithEvents chkAdd As System.Windows.Forms.CheckBox
    Friend WithEvents chkModify As System.Windows.Forms.CheckBox
    Friend WithEvents lblDefault As System.Windows.Forms.Label
    Friend WithEvents btnSet As System.Windows.Forms.Button

End Class
