<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearch
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearch))
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.btnClearSortOrder = New System.Windows.Forms.Button
        Me.lblSortOrder = New System.Windows.Forms.Label
        Me.grdDirectory = New System.Windows.Forms.DataGridView
        Me.mnuGrdDirectory = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuExportSearchResults = New System.Windows.Forms.ToolStripMenuItem
        Me.imlSearch = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.tmrSearch = New System.Windows.Forms.Timer(Me.components)
        Me.tipSearch = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.pnlSearch.SuspendLayout()
        CType(Me.grdDirectory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuGrdDirectory.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSearch
        '
        Me.pnlSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlSearch.Controls.Add(Me.btnClearSortOrder)
        Me.pnlSearch.Controls.Add(Me.lblSortOrder)
        Me.pnlSearch.Controls.Add(Me.grdDirectory)
        Me.pnlSearch.Location = New System.Drawing.Point(12, 12)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(600, 407)
        Me.pnlSearch.TabIndex = 0
        '
        'btnClearSortOrder
        '
        Me.btnClearSortOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClearSortOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearSortOrder.ImageIndex = 2
        Me.btnClearSortOrder.Location = New System.Drawing.Point(3, 381)
        Me.btnClearSortOrder.Name = "btnClearSortOrder"
        Me.btnClearSortOrder.Size = New System.Drawing.Size(75, 23)
        Me.btnClearSortOrder.TabIndex = 5
        Me.btnClearSortOrder.Text = "Clear Order"
        Me.btnClearSortOrder.UseVisualStyleBackColor = True
        '
        'lblSortOrder
        '
        Me.lblSortOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSortOrder.AutoSize = True
        Me.lblSortOrder.Location = New System.Drawing.Point(82, 386)
        Me.lblSortOrder.Name = "lblSortOrder"
        Me.lblSortOrder.Size = New System.Drawing.Size(56, 13)
        Me.lblSortOrder.TabIndex = 4
        Me.lblSortOrder.Text = "Sort order:"
        '
        'grdDirectory
        '
        Me.grdDirectory.AllowUserToAddRows = False
        Me.grdDirectory.AllowUserToDeleteRows = False
        Me.grdDirectory.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdDirectory.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdDirectory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdDirectory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdDirectory.CausesValidation = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdDirectory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdDirectory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDirectory.ColumnHeadersVisible = False
        Me.grdDirectory.ContextMenuStrip = Me.mnuGrdDirectory
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdDirectory.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdDirectory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdDirectory.Location = New System.Drawing.Point(6, 75)
        Me.grdDirectory.MultiSelect = False
        Me.grdDirectory.Name = "grdDirectory"
        Me.grdDirectory.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdDirectory.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdDirectory.RowHeadersVisible = False
        Me.grdDirectory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdDirectory.ShowEditingIcon = False
        Me.grdDirectory.Size = New System.Drawing.Size(591, 305)
        Me.grdDirectory.TabIndex = 3
        Me.grdDirectory.TabStop = False
        '
        'mnuGrdDirectory
        '
        Me.mnuGrdDirectory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuExportSearchResults})
        Me.mnuGrdDirectory.Name = "mnuColumnSort"
        Me.mnuGrdDirectory.Size = New System.Drawing.Size(118, 26)
        '
        'mnuExportSearchResults
        '
        Me.mnuExportSearchResults.Name = "mnuExportSearchResults"
        Me.mnuExportSearchResults.Size = New System.Drawing.Size(117, 22)
        Me.mnuExportSearchResults.Text = "Export"
        '
        'imlSearch
        '
        Me.imlSearch.ImageStream = CType(resources.GetObject("imlSearch.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlSearch.TransparentColor = System.Drawing.Color.White
        Me.imlSearch.Images.SetKeyName(0, "Up.png")
        Me.imlSearch.Images.SetKeyName(1, "Down.png")
        Me.imlSearch.Images.SetKeyName(2, "delete.ico")
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 422)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(624, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 17)
        '
        'tmrSearch
        '
        Me.tmrSearch.Interval = 2000
        '
        'tmrRefresh
        '
        Me.tmrRefresh.Interval = 3000
        '
        'frmSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(624, 444)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.pnlSearch)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmSearch"
        Me.ShowInTaskbar = False
        Me.Text = "Search"
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.grdDirectory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuGrdDirectory.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents grdDirectory As System.Windows.Forms.DataGridView
    Public WithEvents tmrSearch As System.Windows.Forms.Timer
    Friend WithEvents lblSortOrder As System.Windows.Forms.Label
    Friend WithEvents mnuGrdDirectory As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuExportSearchResults As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents imlSearch As System.Windows.Forms.ImageList
    Friend WithEvents tipSearch As System.Windows.Forms.ToolTip
    Friend WithEvents btnClearSortOrder As System.Windows.Forms.Button
    Friend WithEvents tmrRefresh As System.Windows.Forms.Timer
End Class
