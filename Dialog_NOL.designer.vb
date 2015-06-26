<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dialog_NOL
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dialog_NOL))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tvwNols = New System.Windows.Forms.TreeView()
        Me.imgIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lblSelected = New System.Windows.Forms.Label()
        Me.prgProgress = New System.Windows.Forms.ProgressBar()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkBrushes = New System.Windows.Forms.CheckBox()
        Me.chkShapes = New System.Windows.Forms.CheckBox()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkPens = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.tvwNols)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(281, 289)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'tvwNols
        '
        Me.tvwNols.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwNols.ImageIndex = 0
        Me.tvwNols.ImageList = Me.imgIcons
        Me.tvwNols.Location = New System.Drawing.Point(0, 0)
        Me.tvwNols.Name = "tvwNols"
        Me.tvwNols.SelectedImageIndex = 0
        Me.tvwNols.Size = New System.Drawing.Size(281, 289)
        Me.tvwNols.TabIndex = 0
        '
        'imgIcons
        '
        Me.imgIcons.ImageStream = CType(resources.GetObject("imgIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.imgIcons.Images.SetKeyName(0, "Libraries.bmp")
        Me.imgIcons.Images.SetKeyName(1, "LibShut.bmp")
        Me.imgIcons.Images.SetKeyName(2, "LibOpen.bmp")
        Me.imgIcons.Images.SetKeyName(3, "FolderShut.bmp")
        Me.imgIcons.Images.SetKeyName(4, "FolderOpen.bmp")
        Me.imgIcons.Images.SetKeyName(5, "Brush.bmp")
        Me.imgIcons.Images.SetKeyName(6, "Pen.bmp")
        Me.imgIcons.Images.SetKeyName(7, "Shape.bmp")
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(295, 290)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 24)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "Close"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lblSelected
        '
        Me.lblSelected.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSelected.AutoSize = True
        Me.lblSelected.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelected.Location = New System.Drawing.Point(5, 9)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Size = New System.Drawing.Size(0, 13)
        Me.lblSelected.TabIndex = 6
        Me.lblSelected.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'prgProgress
        '
        Me.prgProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.prgProgress.Location = New System.Drawing.Point(8, 320)
        Me.prgProgress.Name = "prgProgress"
        Me.prgProgress.Size = New System.Drawing.Size(281, 24)
        Me.prgProgress.TabIndex = 8
        Me.prgProgress.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(295, 320)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 24)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.Visible = False
        '
        'chkBrushes
        '
        Me.chkBrushes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkBrushes.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBrushes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkBrushes.Location = New System.Drawing.Point(6, 19)
        Me.chkBrushes.Name = "chkBrushes"
        Me.chkBrushes.Size = New System.Drawing.Size(65, 17)
        Me.chkBrushes.TabIndex = 12
        Me.chkBrushes.Text = "Brushes"
        Me.chkBrushes.UseVisualStyleBackColor = True
        '
        'chkShapes
        '
        Me.chkShapes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkShapes.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShapes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkShapes.Location = New System.Drawing.Point(6, 65)
        Me.chkShapes.Name = "chkShapes"
        Me.chkShapes.Size = New System.Drawing.Size(65, 17)
        Me.chkShapes.TabIndex = 14
        Me.chkShapes.Text = "Symbols"
        Me.chkShapes.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreate.Enabled = False
        Me.btnCreate.Location = New System.Drawing.Point(295, 244)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(88, 40)
        Me.btnCreate.TabIndex = 15
        Me.btnCreate.Text = "Create Catalogue..."
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkPens)
        Me.GroupBox2.Controls.Add(Me.chkBrushes)
        Me.GroupBox2.Controls.Add(Me.chkShapes)
        Me.GroupBox2.Location = New System.Drawing.Point(295, 25)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(88, 95)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        '
        'chkPens
        '
        Me.chkPens.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkPens.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPens.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkPens.Location = New System.Drawing.Point(6, 42)
        Me.chkPens.Name = "chkPens"
        Me.chkPens.Size = New System.Drawing.Size(65, 17)
        Me.chkPens.TabIndex = 15
        Me.chkPens.Text = "Pens"
        Me.chkPens.UseVisualStyleBackColor = True
        '
        'Dialog_NOL
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 356)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.prgProgress)
        Me.Controls.Add(Me.lblSelected)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(340, 340)
        Me.Name = "Dialog_NOL"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Named Object Libraries Catalogue"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tvwNols As System.Windows.Forms.TreeView
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents imgIcons As System.Windows.Forms.ImageList
    Friend WithEvents lblSelected As System.Windows.Forms.Label
    Friend WithEvents prgProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkBrushes As System.Windows.Forms.CheckBox
    Friend WithEvents chkShapes As System.Windows.Forms.CheckBox
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkPens As System.Windows.Forms.CheckBox
End Class
