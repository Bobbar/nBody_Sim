<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GraphicsViewer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenDataFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewFromXToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewFromYToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewFromZToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.View3DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.PanelDraw = New GraphicsDisplay.BufferedPanel
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewsToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(704, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenDataFileToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenDataFileToolStripMenuItem
        '
        Me.OpenDataFileToolStripMenuItem.Name = "OpenDataFileToolStripMenuItem"
        Me.OpenDataFileToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.OpenDataFileToolStripMenuItem.Text = "&Open Data File"
        '
        'ViewsToolStripMenuItem
        '
        Me.ViewsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewFromXToolStripMenuItem, Me.ViewFromYToolStripMenuItem, Me.ViewFromZToolStripMenuItem, Me.View3DToolStripMenuItem})
        Me.ViewsToolStripMenuItem.Name = "ViewsToolStripMenuItem"
        Me.ViewsToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ViewsToolStripMenuItem.Text = "Views"
        '
        'ViewFromXToolStripMenuItem
        '
        Me.ViewFromXToolStripMenuItem.Name = "ViewFromXToolStripMenuItem"
        Me.ViewFromXToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ViewFromXToolStripMenuItem.Text = "View from &X"
        '
        'ViewFromYToolStripMenuItem
        '
        Me.ViewFromYToolStripMenuItem.Name = "ViewFromYToolStripMenuItem"
        Me.ViewFromYToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ViewFromYToolStripMenuItem.Text = "View from &Y"
        '
        'ViewFromZToolStripMenuItem
        '
        Me.ViewFromZToolStripMenuItem.Name = "ViewFromZToolStripMenuItem"
        Me.ViewFromZToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ViewFromZToolStripMenuItem.Text = "View From &Z"
        '
        'View3DToolStripMenuItem
        '
        Me.View3DToolStripMenuItem.Name = "View3DToolStripMenuItem"
        Me.View3DToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.View3DToolStripMenuItem.Text = "View 3D"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PanelDraw
        '
        Me.PanelDraw.BackColor = System.Drawing.SystemColors.ControlText
        Me.PanelDraw.Location = New System.Drawing.Point(12, 41)
        Me.PanelDraw.Name = "PanelDraw"
        Me.PanelDraw.Size = New System.Drawing.Size(680, 446)
        Me.PanelDraw.TabIndex = 1
        '
        'GraphicsViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 499)
        Me.Controls.Add(Me.PanelDraw)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "GraphicsViewer"
        Me.Text = "Graphics Viewer"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenDataFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewFromXToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewFromYToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewFromZToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents View3DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelDraw As GraphicsDisplay.BufferedPanel

End Class
