<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddBodies
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdAddOrbit = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCenterMass = New System.Windows.Forms.TextBox()
        Me.chkCenterMass = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdAddStation = New System.Windows.Forms.Button()
        Me.txtNumOfBodies = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMinSize = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMaxSize = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBodyMass = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdAddOrbit)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtCenterMass)
        Me.GroupBox1.Controls.Add(Me.chkCenterMass)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 79)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(316, 122)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Add Into Circular Orbit"
        '
        'cmdAddOrbit
        '
        Me.cmdAddOrbit.Location = New System.Drawing.Point(169, 44)
        Me.cmdAddOrbit.Name = "cmdAddOrbit"
        Me.cmdAddOrbit.Size = New System.Drawing.Size(128, 42)
        Me.cmdAddOrbit.TabIndex = 10
        Me.cmdAddOrbit.Text = "Add"
        Me.cmdAddOrbit.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(39, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Center Mass"
        '
        'txtCenterMass
        '
        Me.txtCenterMass.Location = New System.Drawing.Point(42, 75)
        Me.txtCenterMass.Name = "txtCenterMass"
        Me.txtCenterMass.Size = New System.Drawing.Size(56, 20)
        Me.txtCenterMass.TabIndex = 8
        Me.txtCenterMass.Text = "8000"
        '
        'chkCenterMass
        '
        Me.chkCenterMass.AutoSize = True
        Me.chkCenterMass.Checked = True
        Me.chkCenterMass.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCenterMass.Location = New System.Drawing.Point(21, 30)
        Me.chkCenterMass.Name = "chkCenterMass"
        Me.chkCenterMass.Size = New System.Drawing.Size(123, 17)
        Me.chkCenterMass.TabIndex = 7
        Me.chkCenterMass.Text = "Include Center Mass"
        Me.chkCenterMass.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdAddStation)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 207)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(314, 78)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Add Stationary"
        '
        'cmdAddStation
        '
        Me.cmdAddStation.Location = New System.Drawing.Point(91, 19)
        Me.cmdAddStation.Name = "cmdAddStation"
        Me.cmdAddStation.Size = New System.Drawing.Size(128, 42)
        Me.cmdAddStation.TabIndex = 11
        Me.cmdAddStation.Text = "Add"
        Me.cmdAddStation.UseVisualStyleBackColor = True
        '
        'txtNumOfBodies
        '
        Me.txtNumOfBodies.Location = New System.Drawing.Point(36, 38)
        Me.txtNumOfBodies.Name = "txtNumOfBodies"
        Me.txtNumOfBodies.Size = New System.Drawing.Size(59, 20)
        Me.txtNumOfBodies.TabIndex = 0
        Me.txtNumOfBodies.Text = "500"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "# to Add:"
        '
        'txtMinSize
        '
        Me.txtMinSize.Location = New System.Drawing.Point(121, 38)
        Me.txtMinSize.Name = "txtMinSize"
        Me.txtMinSize.Size = New System.Drawing.Size(44, 20)
        Me.txtMinSize.TabIndex = 3
        Me.txtMinSize.Text = "1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(118, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Min Size"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(177, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Max Size"
        '
        'txtMaxSize
        '
        Me.txtMaxSize.Location = New System.Drawing.Point(180, 38)
        Me.txtMaxSize.Name = "txtMaxSize"
        Me.txtMaxSize.Size = New System.Drawing.Size(44, 20)
        Me.txtMaxSize.TabIndex = 5
        Me.txtMaxSize.Text = "5"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(238, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Body Mass"
        '
        'txtBodyMass
        '
        Me.txtBodyMass.Location = New System.Drawing.Point(241, 38)
        Me.txtBodyMass.Name = "txtBodyMass"
        Me.txtBodyMass.Size = New System.Drawing.Size(56, 20)
        Me.txtBodyMass.TabIndex = 5
        Me.txtBodyMass.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(245, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "0 = Auto"
        '
        'frmAddBodies
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 308)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtMaxSize)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtMinSize)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtBodyMass)
        Me.Controls.Add(Me.txtNumOfBodies)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddBodies"
        Me.Opacity = 0.9R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Bodies"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtNumOfBodies As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtMinSize As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtMaxSize As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtBodyMass As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtCenterMass As TextBox
    Friend WithEvents chkCenterMass As CheckBox
    Friend WithEvents cmdAddOrbit As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents cmdAddStation As Button
End Class
