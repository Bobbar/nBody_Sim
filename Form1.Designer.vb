<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Render = New System.Windows.Forms.PictureBox()
        Me.lblFPS = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.tmrFollow = New System.Windows.Forms.Timer(Me.components)
        Me.txtSpeedX = New System.Windows.Forms.TextBox()
        Me.txtSpeedY = New System.Windows.Forms.TextBox()
        Me.txtSize = New System.Windows.Forms.TextBox()
        Me.txtMass = New System.Windows.Forms.TextBox()
        Me.txtFlag = New System.Windows.Forms.TextBox()
        Me.butAddBall = New System.Windows.Forms.Button()
        Me.butRemoveBalls = New System.Windows.Forms.Button()
        Me.butUpdate = New System.Windows.Forms.Button()
        Me.lblBalls = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.UpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.txtFPS = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TimeStep = New System.Windows.Forms.NumericUpDown()
        Me.NumThreads = New System.Windows.Forms.NumericUpDown()
        Me.PhysicsWorker = New System.ComponentModel.BackgroundWorker()
        Me.tmrRender = New System.Windows.Forms.Timer(Me.components)
        Me.lblDelay = New System.Windows.Forms.Label()
        Me.lblVisBalls = New System.Windows.Forms.Label()
        Me.lblScale = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.Options = New System.Windows.Forms.ToolStripMenuItem()
        Me.BallLines = New System.Windows.Forms.ToolStripMenuItem()
        Me.FBallSOI = New System.Windows.Forms.ToolStripMenuItem()
        Me.Trails = New System.Windows.Forms.ToolStripMenuItem()
        Me.Draw = New System.Windows.Forms.ToolStripMenuItem()
        Me.AntiA = New System.Windows.Forms.ToolStripMenuItem()
        Me.Invert = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmCull = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tools = New System.Windows.Forms.ToolStripMenuItem()
        Me.TotalMassToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmLoad = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdTrails = New System.Windows.Forms.Button()
        Me.UI_Worker = New System.ComponentModel.BackgroundWorker()
        Me.lblRenTime = New System.Windows.Forms.Label()
        Me.cmdStor = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.SeekBar = New System.Windows.Forms.TrackBar()
        CType(Me.Render, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimeStep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumThreads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.SeekBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Render
        '
        Me.Render.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Render.BackColor = System.Drawing.Color.White
        Me.Render.Location = New System.Drawing.Point(0, 60)
        Me.Render.Name = "Render"
        Me.Render.Size = New System.Drawing.Size(1319, 620)
        Me.Render.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Render.TabIndex = 15
        Me.Render.TabStop = False
        '
        'lblFPS
        '
        Me.lblFPS.AutoSize = True
        Me.lblFPS.BackColor = System.Drawing.Color.Black
        Me.lblFPS.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFPS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFPS.Location = New System.Drawing.Point(11, 64)
        Me.lblFPS.Name = "lblFPS"
        Me.lblFPS.Size = New System.Drawing.Size(42, 16)
        Me.lblFPS.TabIndex = 18
        Me.lblFPS.Text = "FPS: "
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(692, 298)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Label11"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.ForestGreen
        Me.Label12.Location = New System.Drawing.Point(807, 5)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 20)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "G: On"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(910, 710)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(99, 20)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "Friction: On"
        Me.Label13.Visible = False
        '
        'Timer3
        '
        Me.Timer3.Interval = 1
        '
        'Button1
        '
        Me.Button1.ForeColor = System.Drawing.Color.Red
        Me.Button1.Location = New System.Drawing.Point(597, 702)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(82, 28)
        Me.Button1.TabIndex = 26
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(347, 710)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(57, 19)
        Me.Button2.TabIndex = 27
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(176, 4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(57, 23)
        Me.Button3.TabIndex = 28
        Me.Button3.Text = "Stop"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'tmrFollow
        '
        Me.tmrFollow.Interval = 20
        '
        'txtSpeedX
        '
        Me.txtSpeedX.Location = New System.Drawing.Point(379, 6)
        Me.txtSpeedX.Name = "txtSpeedX"
        Me.txtSpeedX.Size = New System.Drawing.Size(53, 20)
        Me.txtSpeedX.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.txtSpeedX, "X Velocity")
        '
        'txtSpeedY
        '
        Me.txtSpeedY.Location = New System.Drawing.Point(438, 6)
        Me.txtSpeedY.Name = "txtSpeedY"
        Me.txtSpeedY.Size = New System.Drawing.Size(52, 20)
        Me.txtSpeedY.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.txtSpeedY, "Y Velocity")
        '
        'txtSize
        '
        Me.txtSize.Location = New System.Drawing.Point(496, 6)
        Me.txtSize.Name = "txtSize"
        Me.txtSize.Size = New System.Drawing.Size(61, 20)
        Me.txtSize.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.txtSize, "Body Diameter")
        '
        'txtMass
        '
        Me.txtMass.Location = New System.Drawing.Point(563, 6)
        Me.txtMass.Name = "txtMass"
        Me.txtMass.Size = New System.Drawing.Size(63, 20)
        Me.txtMass.TabIndex = 32
        Me.ToolTip1.SetToolTip(Me.txtMass, "Body Mass")
        '
        'txtFlag
        '
        Me.txtFlag.Location = New System.Drawing.Point(632, 6)
        Me.txtFlag.Name = "txtFlag"
        Me.txtFlag.Size = New System.Drawing.Size(37, 20)
        Me.txtFlag.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.txtFlag, "Flags")
        '
        'butAddBall
        '
        Me.butAddBall.BackColor = System.Drawing.SystemColors.ControlDark
        Me.butAddBall.ForeColor = System.Drawing.Color.Black
        Me.butAddBall.Location = New System.Drawing.Point(3, 3)
        Me.butAddBall.Name = "butAddBall"
        Me.butAddBall.Size = New System.Drawing.Size(75, 23)
        Me.butAddBall.TabIndex = 34
        Me.butAddBall.Text = "Add Bodies"
        Me.butAddBall.UseVisualStyleBackColor = False
        '
        'butRemoveBalls
        '
        Me.butRemoveBalls.Location = New System.Drawing.Point(84, 4)
        Me.butRemoveBalls.Name = "butRemoveBalls"
        Me.butRemoveBalls.Size = New System.Drawing.Size(86, 23)
        Me.butRemoveBalls.TabIndex = 36
        Me.butRemoveBalls.Text = "Remove All"
        Me.butRemoveBalls.UseVisualStyleBackColor = True
        '
        'butUpdate
        '
        Me.butUpdate.Location = New System.Drawing.Point(675, 5)
        Me.butUpdate.Name = "butUpdate"
        Me.butUpdate.Size = New System.Drawing.Size(54, 21)
        Me.butUpdate.TabIndex = 37
        Me.butUpdate.Text = "Update"
        Me.butUpdate.UseVisualStyleBackColor = True
        '
        'lblBalls
        '
        Me.lblBalls.AutoSize = True
        Me.lblBalls.BackColor = System.Drawing.Color.Black
        Me.lblBalls.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalls.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBalls.Location = New System.Drawing.Point(11, 112)
        Me.lblBalls.Name = "lblBalls"
        Me.lblBalls.Size = New System.Drawing.Size(41, 16)
        Me.lblBalls.TabIndex = 38
        Me.lblBalls.Text = "Balls:"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(737, 4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(64, 22)
        Me.Button4.TabIndex = 42
        Me.Button4.Text = "ToT Mass"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(1018, 5)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(77, 20)
        Me.Button5.TabIndex = 43
        Me.Button5.Text = "ScreenShot"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'UpDown1
        '
        Me.UpDown1.Location = New System.Drawing.Point(869, 6)
        Me.UpDown1.Name = "UpDown1"
        Me.UpDown1.Size = New System.Drawing.Size(48, 20)
        Me.UpDown1.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.UpDown1, "Follow Ball Index")
        '
        'txtFPS
        '
        Me.txtFPS.Location = New System.Drawing.Point(289, 6)
        Me.txtFPS.Name = "txtFPS"
        Me.txtFPS.Size = New System.Drawing.Size(30, 20)
        Me.txtFPS.TabIndex = 46
        Me.txtFPS.Text = "60"
        Me.ToolTip1.SetToolTip(Me.txtFPS, "FPS Limiter")
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 5000
        Me.ToolTip1.InitialDelay = 0
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ReshowDelay = 100
        Me.ToolTip1.ShowAlways = True
        '
        'TimeStep
        '
        Me.TimeStep.DecimalPlaces = 3
        Me.TimeStep.Increment = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.TimeStep.Location = New System.Drawing.Point(238, 6)
        Me.TimeStep.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.TimeStep.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.TimeStep.Name = "TimeStep"
        Me.TimeStep.Size = New System.Drawing.Size(48, 20)
        Me.TimeStep.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.TimeStep, "Time Step")
        Me.TimeStep.Value = New Decimal(New Integer() {5, 0, 0, 196608})
        '
        'NumThreads
        '
        Me.NumThreads.Location = New System.Drawing.Point(325, 6)
        Me.NumThreads.Maximum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.NumThreads.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumThreads.Name = "NumThreads"
        Me.NumThreads.Size = New System.Drawing.Size(48, 20)
        Me.NumThreads.TabIndex = 49
        Me.ToolTip1.SetToolTip(Me.NumThreads, "Number Of Threads")
        Me.NumThreads.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'PhysicsWorker
        '
        Me.PhysicsWorker.WorkerReportsProgress = True
        Me.PhysicsWorker.WorkerSupportsCancellation = True
        '
        'tmrRender
        '
        Me.tmrRender.Enabled = True
        Me.tmrRender.Interval = 1
        '
        'lblDelay
        '
        Me.lblDelay.AutoSize = True
        Me.lblDelay.BackColor = System.Drawing.Color.Black
        Me.lblDelay.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDelay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDelay.Location = New System.Drawing.Point(11, 96)
        Me.lblDelay.Name = "lblDelay"
        Me.lblDelay.Size = New System.Drawing.Size(45, 16)
        Me.lblDelay.TabIndex = 48
        Me.lblDelay.Text = "Delay:"
        '
        'lblVisBalls
        '
        Me.lblVisBalls.AutoSize = True
        Me.lblVisBalls.BackColor = System.Drawing.Color.Black
        Me.lblVisBalls.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisBalls.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblVisBalls.Location = New System.Drawing.Point(11, 128)
        Me.lblVisBalls.Name = "lblVisBalls"
        Me.lblVisBalls.Size = New System.Drawing.Size(62, 16)
        Me.lblVisBalls.TabIndex = 49
        Me.lblVisBalls.Text = "#Visible: "
        '
        'lblScale
        '
        Me.lblScale.AutoSize = True
        Me.lblScale.BackColor = System.Drawing.Color.Black
        Me.lblScale.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScale.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblScale.Location = New System.Drawing.Point(11, 144)
        Me.lblScale.Name = "lblScale"
        Me.lblScale.Size = New System.Drawing.Size(45, 16)
        Me.lblScale.TabIndex = 50
        Me.lblScale.Text = "Scale:"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Options, Me.Tools})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1316, 24)
        Me.MenuStrip1.TabIndex = 51
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'Options
        '
        Me.Options.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BallLines, Me.FBallSOI, Me.Trails, Me.Draw, Me.AntiA, Me.Invert, Me.tsmCull})
        Me.Options.Name = "Options"
        Me.Options.Size = New System.Drawing.Size(61, 20)
        Me.Options.Text = "Options"
        '
        'BallLines
        '
        Me.BallLines.CheckOnClick = True
        Me.BallLines.Name = "BallLines"
        Me.BallLines.Size = New System.Drawing.Size(161, 22)
        Me.BallLines.Text = "Follow Ball Lines"
        '
        'FBallSOI
        '
        Me.FBallSOI.CheckOnClick = True
        Me.FBallSOI.Name = "FBallSOI"
        Me.FBallSOI.Size = New System.Drawing.Size(161, 22)
        Me.FBallSOI.Text = "Follow Ball SOI"
        '
        'Trails
        '
        Me.Trails.CheckOnClick = True
        Me.Trails.Name = "Trails"
        Me.Trails.Size = New System.Drawing.Size(161, 22)
        Me.Trails.Text = "Trails"
        '
        'Draw
        '
        Me.Draw.Checked = True
        Me.Draw.CheckOnClick = True
        Me.Draw.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Draw.Name = "Draw"
        Me.Draw.Size = New System.Drawing.Size(161, 22)
        Me.Draw.Text = "Draw"
        '
        'AntiA
        '
        Me.AntiA.Checked = True
        Me.AntiA.CheckOnClick = True
        Me.AntiA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AntiA.Name = "AntiA"
        Me.AntiA.Size = New System.Drawing.Size(161, 22)
        Me.AntiA.Text = "Anti-Aliasing"
        '
        'Invert
        '
        Me.Invert.CheckOnClick = True
        Me.Invert.Name = "Invert"
        Me.Invert.Size = New System.Drawing.Size(161, 22)
        Me.Invert.Text = "Invert Colors"
        '
        'tsmCull
        '
        Me.tsmCull.Checked = True
        Me.tsmCull.CheckOnClick = True
        Me.tsmCull.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tsmCull.Name = "tsmCull"
        Me.tsmCull.Size = New System.Drawing.Size(161, 22)
        Me.tsmCull.Text = "Culling"
        '
        'Tools
        '
        Me.Tools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TotalMassToolStripMenuItem, Me.tsmSave, Me.tsmLoad})
        Me.Tools.Name = "Tools"
        Me.Tools.Size = New System.Drawing.Size(47, 20)
        Me.Tools.Text = "Tools"
        '
        'TotalMassToolStripMenuItem
        '
        Me.TotalMassToolStripMenuItem.Name = "TotalMassToolStripMenuItem"
        Me.TotalMassToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.TotalMassToolStripMenuItem.Text = "Total Mass"
        '
        'tsmSave
        '
        Me.tsmSave.Name = "tsmSave"
        Me.tsmSave.Size = New System.Drawing.Size(130, 22)
        Me.tsmSave.Text = "Save State"
        '
        'tsmLoad
        '
        Me.tsmLoad.Name = "tsmLoad"
        Me.tsmLoad.Size = New System.Drawing.Size(130, 22)
        Me.tsmLoad.Text = "Load State"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.NumThreads)
        Me.Panel1.Controls.Add(Me.TimeStep)
        Me.Panel1.Controls.Add(Me.cmdTrails)
        Me.Panel1.Controls.Add(Me.butAddBall)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.txtSpeedX)
        Me.Panel1.Controls.Add(Me.txtFPS)
        Me.Panel1.Controls.Add(Me.txtSpeedY)
        Me.Panel1.Controls.Add(Me.UpDown1)
        Me.Panel1.Controls.Add(Me.txtSize)
        Me.Panel1.Controls.Add(Me.Button5)
        Me.Panel1.Controls.Add(Me.txtMass)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.txtFlag)
        Me.Panel1.Controls.Add(Me.butUpdate)
        Me.Panel1.Controls.Add(Me.butRemoveBalls)
        Me.Panel1.Location = New System.Drawing.Point(3, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1116, 30)
        Me.Panel1.TabIndex = 52
        '
        'cmdTrails
        '
        Me.cmdTrails.Location = New System.Drawing.Point(927, 5)
        Me.cmdTrails.Name = "cmdTrails"
        Me.cmdTrails.Size = New System.Drawing.Size(68, 20)
        Me.cmdTrails.TabIndex = 47
        Me.cmdTrails.Text = "Trails"
        Me.cmdTrails.UseVisualStyleBackColor = True
        '
        'UI_Worker
        '
        Me.UI_Worker.WorkerReportsProgress = True
        Me.UI_Worker.WorkerSupportsCancellation = True
        '
        'lblRenTime
        '
        Me.lblRenTime.AutoSize = True
        Me.lblRenTime.BackColor = System.Drawing.Color.Black
        Me.lblRenTime.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRenTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRenTime.Location = New System.Drawing.Point(11, 80)
        Me.lblRenTime.Name = "lblRenTime"
        Me.lblRenTime.Size = New System.Drawing.Size(67, 16)
        Me.lblRenTime.TabIndex = 53
        Me.lblRenTime.Text = "Ren Time:"
        '
        'cmdStor
        '
        Me.cmdStor.Location = New System.Drawing.Point(1125, 33)
        Me.cmdStor.Name = "cmdStor"
        Me.cmdStor.Size = New System.Drawing.Size(37, 20)
        Me.cmdStor.TabIndex = 54
        Me.cmdStor.Text = "Stor"
        Me.cmdStor.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(1168, 32)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(62, 20)
        Me.Button6.TabIndex = 55
        Me.Button6.Text = "StopStor"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(1236, 32)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(62, 20)
        Me.Button7.TabIndex = 56
        Me.Button7.Text = "Replay"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'SeekBar
        '
        Me.SeekBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SeekBar.Location = New System.Drawing.Point(6, 686)
        Me.SeekBar.Name = "SeekBar"
        Me.SeekBar.Size = New System.Drawing.Size(1301, 45)
        Me.SeekBar.TabIndex = 57
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1316, 733)
        Me.Controls.Add(Me.SeekBar)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.cmdStor)
        Me.Controls.Add(Me.lblRenTime)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblScale)
        Me.Controls.Add(Me.lblVisBalls)
        Me.Controls.Add(Me.lblDelay)
        Me.Controls.Add(Me.lblBalls)
        Me.Controls.Add(Me.lblFPS)
        Me.Controls.Add(Me.Render)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.MenuStrip1)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(221, 230)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gravity Simulator by Bobby Lovell"
        CType(Me.Render, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimeStep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumThreads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.SeekBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Render As System.Windows.Forms.PictureBox
    Friend WithEvents lblFPS As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents tmrFollow As System.Windows.Forms.Timer
    Friend WithEvents txtSpeedX As System.Windows.Forms.TextBox
    Friend WithEvents txtSpeedY As System.Windows.Forms.TextBox
    Friend WithEvents txtSize As System.Windows.Forms.TextBox
    Friend WithEvents txtMass As System.Windows.Forms.TextBox
    Friend WithEvents txtFlag As System.Windows.Forms.TextBox
    Friend WithEvents butAddBall As System.Windows.Forms.Button
    Friend WithEvents butRemoveBalls As System.Windows.Forms.Button
    Friend WithEvents butUpdate As System.Windows.Forms.Button
    Friend WithEvents lblBalls As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents UpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtFPS As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents PhysicsWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents tmrRender As Timer
    Friend WithEvents lblDelay As Label
    Friend WithEvents lblVisBalls As Label
    Friend WithEvents lblScale As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents Options As ToolStripMenuItem
    Friend WithEvents BallLines As ToolStripMenuItem
    Friend WithEvents FBallSOI As ToolStripMenuItem
    Friend WithEvents Tools As ToolStripMenuItem
    Friend WithEvents TotalMassToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Trails As ToolStripMenuItem
    Friend WithEvents Draw As ToolStripMenuItem
    Friend WithEvents AntiA As ToolStripMenuItem
    Friend WithEvents Invert As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents UI_Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents cmdTrails As Button
    Friend WithEvents TimeStep As NumericUpDown
    Friend WithEvents tsmCull As ToolStripMenuItem
    Friend WithEvents tsmSave As ToolStripMenuItem
    Friend WithEvents tsmLoad As ToolStripMenuItem
    Friend WithEvents NumThreads As NumericUpDown
    Friend WithEvents lblRenTime As Label
    Friend WithEvents cmdStor As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents SeekBar As TrackBar
End Class
