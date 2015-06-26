<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SisControl
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SisControl))
        Me.AxSis = New Cadcorp.SIS.Control.AxInterop.AxSis
        CType(Me.AxSis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxSis
        '
        Me.AxSis.Location = New System.Drawing.Point(24, 40)
        Me.AxSis.Name = "AxSis"
        Me.AxSis.OcxState = CType(resources.GetObject("AxSis.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSis.Size = New System.Drawing.Size(168, 160)
        Me.AxSis.TabIndex = 0
        '
        'SisControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(218, 227)
        Me.Controls.Add(Me.AxSis)
        Me.Name = "SisControl"
        Me.Opacity = 0
        Me.ShowInTaskbar = False
        Me.Text = "SisControl"
        CType(Me.AxSis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents AxSis As Cadcorp.SIS.Control.AxInterop.AxSis

End Class
