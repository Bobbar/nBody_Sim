Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.InteropServices
Public Class Form1

    Public RecordFileName As String

    Sub StartMeasuring()
        TestCount += 1
        ' Console.WriteLine("Test " & TestCount & " has started ...")
        Time = GetCurrentMillisecondCount
    End Sub
    Sub StopMeasuring()
        Dim ExecutionTime As Integer = GetCurrentMillisecondCount - GetPreviousMillisecondCount
        Console.WriteLine(ExecutionTime & " milliseconds.")
    End Sub
    ReadOnly Property GetCurrentMillisecondCount
        Get
            Return My.Computer.Clock.TickCount
        End Get
    End Property
    ReadOnly Property GetPreviousMillisecondCount As Integer
        Get
            Return Time
        End Get
    End Property
    Private Function NextIndex() As Long
    End Function
    Public Function HighestArr() As Integer
        HighestArr = 0
        If Loop1I > Math.Max(Loop3I, Loop2I) Then
            HighestArr = Loop1I
        Else
            HighestArr = Math.Max(Loop3I, Loop2I)
        End If
    End Function
    'Public Sub ShrinkBallArray()
    '    'On Error Resume Next
    '    ' Debug.Print("Cleaning Ball Array")
    '    Dim TempArray() As BallParms
    '    '       t.Abort()
    '    '  bolStopLoop = True
    '    't.Join()
    '    '
    '    ReDim TempArray(UBound(Ball))
    '    Array.Copy(Ball, TempArray, Ball.Length)
    '    ReDim Ball(0)
    '    For i = 0 To UBound(TempArray)
    '        If TempArray(i).Visible = True Then
    '            ReDim Preserve Ball(UBound(Ball) + 1)
    '            Ball(UBound(Ball)) = TempArray(i)
    '            ' Debug.Print(InStr(1, Ball(UBound(Ball)).Flags, "F"))
    '            If InStr(1, Ball(UBound(Ball)).Flags, "F") > 0 Then
    '                lngFollowBall = UBound(Ball)
    '            End If
    '            'Debug.Print lngFollowBall & " " & Ball(lngFollowBall).Flags
    '        End If
    '    Next i
    '    '  bolStopLoop = False
    '    't.Resume()
    '    '  t = New Threading.Thread(AddressOf Me.MainLoop)
    '    '  t.Start()
    'End Sub

    Public Function SegmentsIntersect(ByVal X1 As Double,
    ByVal Y1 As Double, ByVal X2 As Double, ByVal Y2 As _
    Double, ByVal A1 As Double, ByVal B1 As Double, ByVal _
    A2 As Double, ByVal B2 As Double) As Boolean
        Dim dx As Double
        Dim dy As Double
        Dim da As Double
        Dim db As Double
        Dim t As Double
        Dim s As Double
        'With DispSect
        '.Xa1 = X1
        '.Ya1 = Y1
        '.Xa2 = X2
        '.Ya2 = Y2
        '.Xb1 = A1
        '.Yb1 = B2
        '.Xb2 = A2
        '.Yb2 = B2
        'End With
        'Render.Render2.DrawWidth = 2
        'Render.Render2.Line (x1, y1)-(x2, y2), vbBlack
        'Render.Render2.Line (A1, B1)-(A2, B2), vbBlack
        dx = X2 - X1
        dy = Y2 - Y1
        da = A2 - A1
        db = B2 - B1
        If (da * dy - db * dx) = 0 Then
            ' The segments are parallel.
            SegmentsIntersect = False
            Exit Function
        End If
        s = (dx * (B1 - Y1) + dy * (X1 - A1)) / (da * dy - db * dx)
        t = (da * (Y1 - B1) + db * (A1 - X1)) / (db * dx - da * dy)
        SegmentsIntersect = (s >= 0.0# And s <= 1.0# And t >= 0.0# And t <= 1.0#)
        'If SegmentsIntersect Then Debug.Print Time & " Intersection"
        'Render.Render2.DrawWidth = 3
        'Render.Render2.PSet (x1 + t * dx, y1 + t * dy), vbWhite
        ' End If
        ' If it exists, the point of intersection is:
    End Function
    Private Sub Render_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Render.Paint
        '        On Error GoTo Leave
        '        'Render.Image = New Bitmap(Render.ClientSize.Width, Render.ClientSize.Height)
        '        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        '        'e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic
        '        If Not chkTrails.Checked And chkDraw.Checked Then
        '            For i = 1 To UBound(Ball)
        '                If Ball(i).Visible Then ' And Ball(i).LocX > 0 And Ball(i).LocX < Render.Width And Ball(i).LocY > 0 And Ball(i).LocY < Render.Height Then
        '                    'g = g + 1
        '                    ' If g > 4 Then g = 0
        '                    ' e.Graphics.FillEllipse(Brushes.LightBlue, ball_loc_x(i) - 1, ball_loc_y(i) - 1, BallSize(i) + 2, BallSize(i) + 2)
        '                    ' e.Graphics.FillEllipse(Brushes.Blue, ball_loc_x(i), ball_loc_y(i), BallSize(i), BallSize(i))
        '                    If InStr(1, Ball(i).Flags, "S") = False And Ball(i).ShadAngle <> 0 And InStr(1, Ball(i).Flags, "R") = False And chkShadow.Checked Then
        '                        Dim Bx1 As Single, Bx2 As Single, By1 As Single, By2 As Single
        '                        Bx1 = Ball(i).LocX + (Ball(i).Size * 2) * Cos(Ball(i).ShadAngle)
        '                        By1 = Ball(i).LocY + (Ball(i).Size * 2) * Sin(Ball(i).ShadAngle)
        '                        Bx2 = Ball(i).LocX + (Ball(i).Size / 2) * Cos(Ball(i).ShadAngle) '(Ball(i).Size * Cos(Ball(i).ShadAngle * PI / 180)) + Ball(i).LocX 'Ball(i).LocX + (Ball(i).Size * 2) * Cos(Ball(i).ShadAngle)
        '                        By2 = Ball(i).LocY + (Ball(i).Size / 2) * Sin(Ball(i).ShadAngle) '(Ball(i).Size * Sin(Ball(i).ShadAngle * PI / 180)) + Ball(i).LocY 'Ball(i).LocY + (Ball(i).Size * 2) * Sin(Ball(i).ShadAngle)
        '                        'Debug.Print(Ball(i).Flags)
        '                        Dim myBrush2 As New LinearGradientBrush(New Point(Bx1, By1), New Point(Bx2, By2), Color.FromArgb(26, 26, 26, 1), Ball(i).Color) 'SolidBrush(Ball(i).Color)
        '                        e.Graphics.FillEllipse(myBrush2, Ball(i).LocX - Ball(i).Size / 2, Ball(i).LocY - Ball(i).Size / 2, Ball(i).Size, Ball(i).Size)
        '                        e.Graphics.ScaleTransform(pic_scale, pic_scale)
        '                        'Dim myBrush As New SolidBrush(Ball(i).Color)
        '                        'e.Graphics.FillEllipse(myBrush, Ball(i).LocX - Ball(i).Size / 2, Ball(i).LocY - Ball(i).Size / 2, Ball(i).Size, Ball(i).Size)
        '                        '  e.Graphics.DrawLine(Pens.White, Bx1, By1, Bx2, By2)
        '                    Else
        '                        e.Graphics.ScaleTransform(pic_scale, pic_scale)
        '                        Dim myPen As New Pen(Color.Red)
        '                        Dim myBrush As New SolidBrush(Ball(i).Color)
        '                        e.Graphics.FillEllipse(myBrush, Ball(i).LocX - Ball(i).Size / 2, Ball(i).LocY - Ball(i).Size / 2, Ball(i).Size, Ball(i).Size)
        '                        e.Graphics.DrawEllipse(myPen, Ball(i).LocX - Ball(i).Size / 2, Ball(i).LocY - Ball(i).Size / 2, Ball(i).Size, Ball(i).Size)
        '                    End If
        '                    'e.Graphics.FillEllipse(Brushes.Black, Ball(i).LocX - Ball(i).Size / 2 - 1, Ball(i).LocY - Ball(i).Size / 2 - 1, Ball(i).Size + 2, Ball(i).Size + 2)
        '                End If
        '            Next
        '        End If
        'Leave:
    End Sub
    Private Sub Matrix_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs)
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' t.Abort()
        't2.Abort()
        ' t3.Abort()
        'm.Abort()
        bolStopLoop = True
        Me.Dispose()
        ' End
    End Sub
    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = 16 Then bolShiftDown = True
        If e.KeyCode = 17 Then
            bolStopLoop = True
            ' t.Suspend()
            't2.Suspend()
            't3.Suspend()
            bolAltDown = True
        End If
    End Sub
    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
    End Sub
    Private Sub Form1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = 16 Then bolShiftDown = False
        If e.KeyCode = 17 Then
            bolStopLoop = False
            't.Resume()
            ' t2.Resume()
            't3.Resume()
            bolAltDown = False
            'MasterLoop()
            'MainLoop()
        End If
    End Sub
    Private Sub Render_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Render.MouseDown
        On Error Resume Next
        'Debug.Print("RenLoc: " & e.Location.ToString)
        'Debug.Print("OffLoc: " & ScaleMousePosRelative(New SPoint(e.Location.X, e.Location.Y)).ToString)

        If e.Button = Windows.Forms.MouseButtons.Right Then
            bolStopDraw = True
            ReDim Preserve Ball(UBound(Ball) + 1)
            ' Debug.Print("Index: " & UBound(Ball))
            Ball(UBound(Ball)).Color = RandomRGBColor().ToArgb
            Ball(UBound(Ball)).LocX = ScaleMousePosRelative(New SPoint(e.Location)).X - (Ball(UBound(Ball)).Size / 2)  ' ScaleMousePosRelative(e.Location).X - Ball(UBound(Ball)).Size / 2
            Ball(UBound(Ball)).LocY = ScaleMousePosRelative(New SPoint(e.Location)).Y - (Ball(UBound(Ball)).Size / 2)
            Ball(UBound(Ball)).SpeedX = 0
            Ball(UBound(Ball)).SpeedY = 0
            Ball(UBound(Ball)).Size = 1
            'Ball(UBound(Ball)).Flags = ""
            '  Ball(UBound(Ball)).IsFragment = False
            '  Ball(UBound(Ball)).Index = UBound(Ball)
            Ball(UBound(Ball)).UID = RndIntUID(UBound(Ball)) 'Guid.NewGuid.ToString
            Ball(UBound(Ball)).Mass = fnMass(Ball(UBound(Ball)).Size)
            Ball(UBound(Ball)).Visible = 1
            '  Ball(UBound(Ball)).Flags = Ball(UBound(Ball)).Flags + "BH"
            MouseIndex = UBound(Ball)
            Timer3.Enabled = True
        End If
        If Windows.Forms.MouseButtons.Left Then
            If bolAltDown Then
                tmrFollow.Enabled = False
                bolFollow = False
                '  Ball(lngFollowBall).Flags = Replace(Ball(lngFollowBall).Flags, "F", "")
                lngFollowBall = 0
            End If
            'For i = 1 To UBound(Ball)
            '    Ball(i).Old_LocX = Ball(i).LocX
            '    Ball(i).Old_LocY = Ball(i).LocY
            'Next
            If Sel = -1 Then
                For i = 1 To UBound(Ball)
                    'Debug.Print "LocX: " & Ball(i).LocX & vbCrLf & "LocY: " & Ball(i).LocY & vbCrLf & "SpeedX: " & Ball(i).SpeedX & vbCrLf & "SpeedY: " & Ball(i).SpeedY
                    If MouseOver(New SPoint(e.Location), Ball(i)) Then
                        'Debug.Print(Render.PointToClient(New Point(Ball(i).LocX, Ball(i).LocY)).ToString)
                        ' Debug.Print("BLoc: " & Ball(i).LocX & "-" & Ball(i).LocY)
                        Debug.Print("-----")
                        Debug.Print("Body Index: " & i & "  Body UID: " & Ball(i).UID & "  Body Loc: " & Ball(i).LocX & "," & Ball(i).LocY)
                        Debug.Print("Mass: " & Ball(i).Mass & " " & "Size: " & Ball(i).Size)
                        Debug.Print("InRoche: " & Ball(i).InRoche.ToString & " " & "RocheF: " & Ball(i).ForceTot)
                        Debug.Print("ThreadID: " & Ball(i).ThreadID & " " & "BlockID: " & Ball(i).BlockID)
                        Debug.Print("Visible: " & Ball(i).Visible)
                        Debug.Print("LastColID: " & Ball(i).LastColID)
                        If Not bolAltDown And bolShiftDown Then MoV = 1
                        Sel = i
                        'If bolShiftDown Then
                        '    Ball(Sel).Locked = True
                        '    '  Ball(Sel).Flags = "BH"
                        'End If
                        If bolAltDown Then
                            lngFollowBall = Sel
                            FollowGUID = Ball(Sel).UID
                            ' Ball(Sel).Flags = Ball(Sel).Flags + "F"
                            ' tmrFollow.Enabled = True
                        End If
                        txtSpeedX.Text = Ball(Sel).SpeedX
                        txtSpeedY.Text = Ball(Sel).SpeedY
                        txtSize.Text = Ball(Sel).Size
                        txtMass.Text = Ball(Sel).Mass
                        'txtFlag.Text = Ball(Sel).Flags
                        PubSel = Sel
                        'Debug.Print("Mass: " & Ball(i).Mass & " units")
                        ' Debug.Print("Index: " & i)
                    End If
                Next
            End If
            ' If bolShiftDown Then
            MouseDnX = e.X
            MouseDnY = e.Y
            ' End If
        End If
    End Sub
    Private Sub Render_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Render.MouseMove
        On Error GoTo Err
        If e.Button = Windows.Forms.MouseButtons.Left Then
            'For i = 0 To UBound(Ball)
            '    Ball(i).Old_LocX = Ball(i).LocX
            '    Ball(i).Old_LocY = Ball(i).LocY
            'Next
            If Sel = -1 And bolShiftDown Then

                For i = 0 To UBound(Ball)
                    'ScaleMousePos(e.X) > Ball(i).LocX And ScaleMousePos(e.X) < Ball(i).LocX + Ball(i).Size And ScaleMousePos(e.Y) > Ball(i).LocY And ScaleMousePos(e.Y) < Ball(i).LocY + Ball(i).Size And Not bolShiftDown Then
                    If MouseOver(New SPoint(e.Location), Ball(i)) Then
                        MoV = 1
                        Sel = i
                    End If
                Next
            End If
            If MoV = 1 Then
                'Ball(Sel).MovinG = True
                Ball(Sel).LocX = ScaleMousePosRelative(New SPoint(e.Location)).X
                Ball(Sel).LocY = ScaleMousePosRelative(New SPoint(e.Location)).Y
                'ax(Sel) = 0
                'ay(Sel) = 0
                'bCenterX(Sel) = e.X
                'bCenterY(Sel) = e.Y
                'If Ball(Sel).Old_LocX < Ball(Sel).LocX - Ball(Sel).Size / 2 Then Ball(Sel).SpeedX = (Ball(Sel).LocX - Ball(Sel).Old_LocX) / 2 'To the right
                'If Ball(Sel).Old_LocX > Ball(Sel).LocX - Ball(Sel).Size / 2 Then Ball(Sel).SpeedX = (Ball(Sel).LocX - Ball(Sel).Old_LocX) / 2 'To the left
                'If Ball(Sel).Old_LocY < Ball(Sel).LocY - Ball(Sel).Size / 2 Then Ball(Sel).SpeedY = (Ball(Sel).LocY - Ball(Sel).Old_LocY) / 2 'Up
                'If Ball(Sel).Old_LocY > Ball(Sel).LocY - Ball(Sel).Size / 2 Then Ball(Sel).SpeedY = (Ball(Sel).LocY - Ball(Sel).Old_LocY) / 2 'Down
            Else
                If e.Button = Windows.Forms.MouseButtons.Left Then
                    'Dim StartPos As New Point(MouseDnX, MouseDnY)
                    'Dim EndPos As New Point(e.X, e.Y)
                    'Dim DiffPos As Point
                    'DiffPos = StartPos - EndPos
                    'Dim Dist As Integer
                    'Dist = Math.Sqrt((StartPos.X - EndPos.X) ^ 2 + (StartPos.Y - EndPos.Y) ^ 2)
                    Dim MouseXDiff As Double
                    Dim MouseYDiff As Double
                    MouseUpX = e.X 'ScaleMousePosExact(e.Location).X
                    MouseUpY = e.Y 'ScaleMousePosExact(e.Location).Y
                    MouseXDiff = MouseUpX - MouseDnX
                    MouseYDiff = MouseUpY - MouseDnY
                    Dim MouseDiff As New SPoint(MouseXDiff, MouseYDiff)
                    RelBallPosMod.X = RelBallPosMod.X + ScaleMousePosExact(MouseDiff).X
                    RelBallPosMod.Y = RelBallPosMod.Y + ScaleMousePosExact(MouseDiff).Y
                    MouseDnX = e.X 'ScaleMousePosExact(e.Location).X
                    MouseDnY = e.Y 'ScaleMousePosExact(e.Location).Y
                    ' Debug.Print(RelBallPosMod.ToString)
                End If
            End If
        End If
Err:

    End Sub
    Private Function MouseOver(MousePos As SPoint, Ball As Prim_Struct) As Boolean
        'ScaleMousePos(e.X) > Ball(i).LocX And ScaleMousePos(e.X) < Ball(i).LocX + Ball(i).Size And ScaleMousePos(e.Y) > Ball(i).LocY And ScaleMousePos(e.Y) < Ball(i).LocY + Ball(i).Size And Not bolShiftDown Then
        ' MouseOver(New Point(e.Location), New Point(Ball(i).LocX, Ball(i).LocY + Ball(i).Size))
        Dim Dist As Double = Math.Sqrt((ScaleMousePosRelative(MousePos).X - Ball.LocX) ^ 2 + (ScaleMousePosRelative(MousePos).Y - Ball.LocY) ^ 2)
        If Dist < Ball.Size / 2 Then
            Return True
        End If
        'If ScaleMousePos(MousePos.X) > Ball.LocX And ScaleMousePos(MousePos.X) < Ball.LocX + Ball.Size And ScaleMousePos(MousePos.Y) > Ball.LocY And ScaleMousePos(MousePos.Y) < Ball.LocY + Ball.Size Then
        '    Return True
        'End If
    End Function
    Private Sub Render_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Render.MouseUp
        On Error Resume Next
        Timer3.Enabled = False
        bolStopDraw = False
        'For i = 0 To UBound(Ball)
        '    Ball(i).MovinG = False
        'Next
        Sel = -1
        MoV = 0
        'If Windows.Forms.MouseButtons.Left And bolShiftDown Then
        '    Dim MouseXDiff As Double
        '    Dim MouseYDiff As Double
        '    MouseUpX = e.X
        '    MouseUpY = e.Y
        '    MouseXDiff = MouseUpX - MouseDnX
        '    MouseYDiff = MouseUpY - MouseDnY
        '    RelBallPosMod.X = RelBallPosMod.X + MouseXDiff
        '    RelBallPosMod.Y = RelBallPosMod.Y + MouseYDiff
        '    Debug.Print(RelBallPosMod.ToString)
        '    For i = 1 To UBound(Ball)
        '        Ball(i).LocX = Ball(i).LocX + MouseXDiff
        '        Ball(i).LocY = Ball(i).LocY + MouseYDiff
        '    Next
        'End If
        If bolAltDown And lngFollowBall > 0 Then
            'Dim DiffX As Double, DiffY As Double
            'Dim g As Long
            'FollowX = Ball(lngFollowBall).LocX
            'FollowY = Ball(lngFollowBall).LocY
            'DiffX = FollowX - (Render.Size.Width / 2)
            'DiffY = FollowY - (Render.Size.Height / 2)
            'For g = 1 To UBound(Ball)
            '    Ball(g).LocX = Ball(g).LocX - DiffX
            '    Ball(g).LocY = Ball(g).LocY - DiffY
            'Next g
            'FollowX = Ball(lngFollowBall).LocX
            'FollowY = Ball(lngFollowBall).LocY
            bolFollow = True
            'tmrFollow.Enabled = True
            'Render.Refresh()
        End If
    End Sub
    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If gravity = 0.5 Then gravity = 0 : Exit Sub
        gravity = 0.5
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        SetColors(Me)
        pic_scale = 1
        ScreenCenterX = Me.Render.Width / 2
        ScreenCenterY = Me.Render.Height / 2
        ScaleOffset.X = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).X
        ScaleOffset.Y = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).Y
        intTargetFPS = txtFPS.Text
        Dim X As Single = Me.Render.Width / 2
        Dim Y As Single = Me.Render.Height / 2
        ScaleOffset.X = ScaleMousePosExact(New SPoint(X, Y)).X
        ScaleOffset.Y = ScaleMousePosExact(New SPoint(X, Y)).Y
        bolShadows = False
        UpDownVal = 0
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)
        'For i = 0 To 100
        'BallSize(i) = 50
        'Next
        'Hsize = BallSize(0) / 2
        'Ball_IndX = -1
        StepMulti = TimeStep.Value
        bGrav = 1
        'fOn = 0
        ReDim Ball(0)
        ' Timer1.Enabled = True
        bolStop = False
        Me.Show()
        'MainLoop()
        ' MasterLoop()
        ' ThreadNum = NumThreads.Value
        bolStopLoop = False
        't = New Threading.Thread(AddressOf Me.MainLoop)
        ' t2 = New Threading.Thread(AddressOf Me.MainLoop2)
        ' t3 = New Threading.Thread(AddressOf Me.MainLoop3)
        '    m = New Threading.Thread(AddressOf Me.MasterLoop)
        '  s = New Threading.Thread(AddressOf Me.ShrinkBallArray)
        Thread1Done = False
        Thread2Done = False
        Thread3Done = False
        bolStart1 = False
        bolStart2 = False
        bolStart3 = False
        PubIndex = 1
        UI_Worker.RunWorkerAsync()
        ' tmrRender.Enabled = True
        '   PhysicsWorker.RunWorkerAsync()
        'MainLoop()
    End Sub
    Private Sub Label1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Erase Ball
        ReDim Ball(0)
        bolFollow = False
    End Sub
    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        On Error Resume Next
        Ball(MouseIndex).Size = Ball(MouseIndex).Size + 0.5
        Ball(MouseIndex).Mass = fnMass(Ball(MouseIndex).Size) ' ^ 2
    End Sub
    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim tX, tY
    '    Dim g As System.Drawing.Graphics
    '    Dim p As New System.Drawing.Pen(Color.Green, 4)
    '    g = Render.CreateGraphics
    '    tX = ball_loc_x(0) + (BallSize(0) / 2)
    '    tY = ball_loc_y(0) + (BallSize(0) / 2)
    '    g.DrawEllipse(p, tX, tY, 10, 10)
    '    'Render.Circle (X, Y), 100, &HFF00&
    'End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        bolStop = Not bolStop
        If bolStop Then
            '   SeekIndex = SeekBar.Value
            Button3.Text = "Start"

            ' tmrFollow.Enabled = False
            't.Suspend()
            ' t2.Suspend()
            ' t3.Suspend()
            bolStopLoop = True
        Else
            Button3.Text = "Stop"

            '    tmrFollow.Enabled = True
            ' t.Resume()
            '  t2.Resume()
            ' t3.Resume()
            bolStopLoop = False
            '  MainLoop()
        End If
        'MasterLoop()
    End Sub
    Private Sub tmrFollow_Tick(sender As Object, e As EventArgs) Handles tmrFollow.Tick
        On Error Resume Next
        'Debug.Print(UBound(Ball) & " " & lngFollowBall)
        'If UBound(Ball) > 0 And lngFollowBall > 0 Then
        txtSpeedX.Text = Ball(lngFollowBall).SpeedX
        txtSpeedY.Text = Ball(lngFollowBall).SpeedY
        txtSize.Text = Ball(lngFollowBall).Size
        txtMass.Text = Ball(lngFollowBall).Mass
        '  txtFlag.Text = Ball(lngFollowBall).Flags
        'End If
        'If bolBallsRemoved Then ShrinkBallArray()
    End Sub

    Private Sub butAddBall_Click(sender As Object, e As EventArgs) Handles butAddBall.Click


        frmAddBodies.Show()


        'On Error Resume Next
        'Const Balls As Long = 8000
        'Dim Radius As Double = 1000
        'Dim px, py As Double

        'Dim i As Long
        'For i = 0 To Balls
        '    ReDim Preserve Ball(UBound(Ball) + 1)
        '    Ball(UBound(Ball)).Color = RandomRGBColor() 'colDefBodyColor
        '    Ball(UBound(Ball)).Visible = True

        '    px = GetRandomNumber(1, Render.Width / pic_scale) - ScaleOffset.X - RelBallPosMod.X
        '    py = GetRandomNumber(1, Render.Height / pic_scale) - ScaleOffset.Y - RelBallPosMod.Y

        '    Dim magv As Double = circleV(px, py, solarmass)

        '    Dim absangle As Double = Atan(Abs(py / px))
        '    Dim thetav As Double = PI / 2 - absangle
        '    Dim phiv As Double = Rnd() * PI
        '    Dim vx As Double = -1 * Sign(py) * Cos(thetav) * magv
        '    Dim vy As Double = Sign(px) * Sin(thetav) * magv


        '    Ball(UBound(Ball)).LocX = px
        '    Ball(UBound(Ball)).LocY = py

        '    Ball(UBound(Ball)).SpeedX = vx
        '    Ball(UBound(Ball)).SpeedY = vy
        '    Ball(UBound(Ball)).Flags = ""
        '    Ball(UBound(Ball)).Size = 1 'GetRandomNumber(1, 2)
        '    Ball(UBound(Ball)).Mass = 1 'fnMass(Ball(UBound(Ball)).Size) ' * 2
        'Next

        'ReDim Preserve Ball(UBound(Ball) + 1)
        'Ball(UBound(Ball)).Color = Color.Black 'RandomRGBColor() 'colDefBodyColor
        'Ball(UBound(Ball)).Visible = True
        'Ball(UBound(Ball)).LocX = Render.Width / 2 / pic_scale - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
        'Ball(UBound(Ball)).LocY = Render.Height / 2 / pic_scale - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
        'Ball(UBound(Ball)).SpeedX = 0
        'Ball(UBound(Ball)).SpeedY = 0
        'Ball(UBound(Ball)).Flags = "BH"
        'Ball(UBound(Ball)).Size = 15
        'Ball(UBound(Ball)).Mass = solarmass 'fnMass(Ball(UBound(Ball)).Size) ' * 2



        ''For i = 0 To Balls
        ''    ReDim Preserve Ball(UBound(Ball) + 1)
        ''    Ball(UBound(Ball)).Color = RandomRGBColor() 'colDefBodyColor
        ''    Ball(UBound(Ball)).Visible = True
        ''    Ball(UBound(Ball)).LocX = GetRandomNumber(1, Render.Width / pic_scale) - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
        ''    Ball(UBound(Ball)).LocY = GetRandomNumber(1, Render.Height / pic_scale) - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
        ''    Ball(UBound(Ball)).SpeedX = 0
        ''    Ball(UBound(Ball)).SpeedY = 0
        ''    Ball(UBound(Ball)).Flags = ""
        ''    Ball(UBound(Ball)).Size = GetRandomNumber(1, 5)
        ''    Ball(UBound(Ball)).Mass = fnMass(Ball(UBound(Ball)).Size) ' * 2
        ''Next






    End Sub


    Private Sub butRemoveBalls_Click(sender As Object, e As EventArgs) Handles butRemoveBalls.Click
        ' If m.ThreadState = Threading.ThreadState.Running Then
        'm.Abort()
        'Array.Clear(Ball, 0, Ball.Count)
        'ReDim Ball(0)
        'bolFollow = False
        'm = New Threading.Thread(AddressOf Me.MasterLoop)
        'm.Start()
        ' Else
        bolStopWorker = True
        bolDraw = False

        Do Until Not PhysicsWorker.IsBusy
            wait(300)
        Loop
        If Not PhysicsWorker.IsBusy Then
            Array.Clear(Ball, 0, Ball.Count)
            Render.Image = Nothing
            ReDim Ball(0)
            bolFollow = False
            ' End If
            bolStopWorker = False
            bolDraw = True
            PhysicsWorker.RunWorkerAsync()
            UI_Worker.RunWorkerAsync()
        End If
        'MainLoop()
        'Erase Ball
    End Sub
    Private Sub butUpdate_Click(sender As Object, e As EventArgs) Handles butUpdate.Click
        Ball(PubSel).SpeedX = txtSpeedX.Text
        Ball(PubSel).SpeedY = txtSpeedY.Text
        Ball(PubSel).Size = txtSize.Text
        Ball(PubSel).Mass = txtMass.Text
        '  Ball(PubSel).Flags = txtFlag.Text
    End Sub
    Private Sub Painter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
    End Sub
    Private Sub Render_PaddingChanged(sender As Object, e As EventArgs) Handles Render.PaddingChanged
    End Sub

    Private Sub txtStep_HelpRequested(sender As Object, hlpevent As HelpEventArgs)
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim TotalMass As Double
        For i = 1 To UBound(Ball)
            If Ball(i).Visible = 1 Then TotalMass = TotalMass + Ball(i).Mass
        Next i
        Debug.Print("Total Mass: " & TotalMass)
        MsgBox("Total Mass: " & TotalMass)
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Dim cap_image As Image
        Me.Render.Image.Save(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Screenshot-" & Now.ToString("_hhmmss") & ".bmp", System.Drawing.Imaging.ImageFormat.Bmp)
        'opens a save dialog box for saving the settings
        'cap_image = Render.Image
        'cap_image.Save("C:\Image.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg)
    End Sub
    Private Sub Form1_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
    End Sub
    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'Render.Width = Me.Width - 30
        'Render.Height = Me.Height - 70
        'If bGrav = 1 Then
        '    RenderWindowDims.X = CInt(Me.Render.Width)
        '    RenderWindowDims.Y = CInt(Me.Render.Height)
        'End If
        'If bolFollow Then
        '    Dim DiffX As Double, DiffY As Double
        '    If Ball(lngFollowBall).LocX <> FollowX Or Ball(lngFollowBall).LocY <> FollowY Then
        '        DiffX = Ball(lngFollowBall).LocX - (Render.Width / 2) 'FollowX
        '        DiffY = Ball(lngFollowBall).LocY - (Render.Height / 2) 'FollowY
        '        For i = 1 To UBound(Ball)
        '            Ball(i).LocX = Ball(i).LocX - DiffX
        '            Ball(i).LocY = Ball(i).LocY - DiffY
        '        Next
        '        FollowX = Ball(lngFollowBall).LocX
        '        FollowY = Ball(lngFollowBall).LocY
        '    End If
        'End If
    End Sub
    Private Sub UpDown1_ValueChanged(sender As Object, e As EventArgs) Handles UpDown1.ValueChanged
        On Error Resume Next
        Dim Sel As Long
        If UpDown1.Value < UpDownVal Then 'down click
            UpDownVal = UpDown1.Value
            ' Ball(lngFollowBall).Flags = Replace$(Ball(lngFollowBall).Flags, "F", "")
            Sel = UpDown1.Value
            If Ball(Sel).Visible = 0 Then
                Do Until Ball(Sel).Visible = 1 Or Sel = UpDown1.Minimum
                    Sel = Sel - 1
                Loop
            End If
            UpDown1.Value = Sel
            lngFollowBall = Sel
            FollowGUID = Ball(Sel).UID
            ' Ball(lngFollowBall).Flags = Ball(lngFollowBall).Flags + "F"
            txtSpeedX.Text = Ball(Sel).SpeedX
            txtSpeedY.Text = Ball(Sel).SpeedY
            txtSize.Text = Ball(Sel).Size
            txtMass.Text = Ball(Sel).Mass
            '  txtFlag.Text = Ball(Sel).Flags
            PubSel = Sel
            Dim DiffX As Double, DiffY As Double
            FollowX = Ball(lngFollowBall).LocX
            FollowY = Ball(lngFollowBall).LocY
            DiffX = FollowX - (Render.Width / 2)
            DiffY = FollowY - (Render.Height / 2)
            'For b = 1 To UBound(Ball)
            '    Ball(b).LocX = Ball(b).LocX - DiffX
            '    Ball(b).LocY = Ball(b).LocY - DiffY
            'Next b
            FollowX = Ball(lngFollowBall).LocX
            FollowY = Ball(lngFollowBall).LocY
            bolFollow = True
        Else
            UpDownVal = UpDown1.Value
            '  Ball(lngFollowBall).Flags = Replace$(Ball(lngFollowBall).Flags, "F", "")
            Sel = UpDown1.Value
            If Ball(Sel).Visible = 0 Then
                Do Until Ball(Sel).Visible = 1 Or Sel = UpDown1.Maximum
                    Sel = Sel + 1
                Loop
            End If
            UpDown1.Value = Sel
            lngFollowBall =
                 FollowGUID = Ball(Sel).UID
            '  Ball(lngFollowBall).Flags = Ball(lngFollowBall).Flags + "F"
            txtSpeedX.Text = Ball(Sel).SpeedX
            txtSpeedY.Text = Ball(Sel).SpeedY
            txtSize.Text = Ball(Sel).Size
            txtMass.Text = Ball(Sel).Mass
            PubSel = Sel
            Dim DiffX As Double, DiffY As Double
            FollowX = Ball(lngFollowBall).LocX
            FollowY = Ball(lngFollowBall).LocY
            DiffX = FollowX - (Render.Width / 2)
            DiffY = FollowY - (Render.Height / 2)
            'For b = 1 To UBound(Ball)
            '    Ball(b).LocX = Ball(b).LocX - DiffX
            '    Ball(b).LocY = Ball(b).LocY - DiffY
            'Next b
            FollowX = Ball(lngFollowBall).LocX
            FollowY = Ball(lngFollowBall).LocY
            bolFollow = True
        End If
    End Sub
    Private Sub txtFPS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFPS.TextChanged
        Dim MaxFPS As Integer = 600
        Dim MinFPS As Integer = 5
        If txtFPS.Text = "" Then
            intTargetFPS = MinFPS
        Else
            If CInt(txtFPS.Text) < MinFPS Then
                intTargetFPS = MinFPS
            ElseIf CInt(txtFPS.Text) > MaxFPS Then
                intTargetFPS = MaxFPS
            Else
                intTargetFPS = txtFPS.Text
            End If
        End If
    End Sub
    Private Sub Form1_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        'If bolStop Then
        '    ' Button3.Text = "Start"
        '    Timer2.Enabled = False
        '    tmrFollow.Enabled = False
        '    't.Suspend()
        '    ' t2.Suspend()
        '    ' t3.Suspend()
        '    bolStopLoop = True
        'Else
        '    ' Button3.Text = "Stop"
        '    'Timer2.Enabled = True
        '    'tmrFollow.Enabled = True
        '    '' t.Resume()
        '    ''  t2.Resume()
        '    '' t3.Resume()
        '    'bolStopLoop = False
        '    'MainLoop()
        'End If
    End Sub
    Private Sub Form1_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        'RenderWindowDims.X = CInt(Me.Render.Width)
        'RenderWindowDims.Y = CInt(Me.Render.Height)
        ' UpdateScale()
        'If bolStop Then
        '    ' Button3.Text = "Start"
        '    'Timer2.Enabled = False
        '    'tmrFollow.Enabled = False
        '    't.Suspend()
        '    ' t2.Suspend()
        '    ' t3.Suspend()
        '    bolStopLoop = True
        'Else
        '    ' Button3.Text = "Stop"
        '    Timer2.Enabled = True
        '    ' tmrFollow.Enabled = True
        '    '' t.Resume()
        '    ''  t2.Resume()
        '    '' t3.Resume()
        '    bolStopLoop = False
        '    MainLoop()
        'End If
    End Sub
    Public BallViewLoc As New SPoint
    Private Sub Render_MouseWheel(sender As Object, e As MouseEventArgs) Handles Render.MouseWheel
        Dim scale_amount As Single = 0.05 * pic_scale
        If e.Delta > 0 Then
            pic_scale += scale_amount
        Else
            pic_scale -= scale_amount
        End If
        UpdateScale()
        ' Debug.Print(pic_scale)
        ScreenCenterX = Me.Render.Width / 2
        ScreenCenterY = Me.Render.Height / 2
        ScaleOffset.X = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).X
        ScaleOffset.Y = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).Y
        'Dim OffsetValue As New SPoint
        'OffsetValue.X = Render.Width / 2 * pic_scale ' RelBallPosMod.X / pic_scale
        'OffsetValue.Y = Render.Height / 2 * pic_scale 'RelBallPosMod.Y / pic_scale
        'Dim CentOffset As New SPoint(RelBallPosMod.X - OffsetValue.X, RelBallPosMod.Y - OffsetValue.Y)
        'Dim Dist As Double = Math.Sqrt((0 - Render.Width) ^ 2 + (0 - Render.Height) ^ 2)
        'Debug.Print(Dist / 4 * pic_scale)
        '  RelBallPosMod = RelBallPosMod - CentOffset
        'RelBallPosMod.X = RelBallPosMod.X + (40 * pic_scale)
        ' RelBallPosMod.Y = RelBallPosMod.Y + (30 * pic_scale)
        'Dim Vectors As New GeomLib.Vector2D(e.X, e.Y)
        'Dim curVec As New Point(e.X, e.Y)
        '' Dim newVex As Point = Vectors.UnitVector()
        'Dim mouse_true As New Point(e.X / pic_scale + BallViewLoc.X, e.Y / pic_scale + BallViewLoc.Y)
        'Dim mouse_relative As New Point(Render.)
        'Debug.Print(pic_scale)
        ' Debug.Print(RelBallPosMod.ToString)
    End Sub

    Public Structure ThreadStruct
        Public UpperBound As Integer
        Public LowerBound As Integer
        Public Bodys() As BallParms
    End Structure
    'Private Sub pWorkerThread(sender As Object, e As DoWorkEventArgs)
    '    Dim uBoundBody, lBoundBody As Integer
    '    Dim Bodys() As BallParms
    '    Dim M1 As Double
    '    Dim M2 As Double
    '    'Dim ClsForce As Double
    '    Dim ForceX As Double
    '    Dim ForceY As Double
    '    Dim TotMass As Double
    '    Dim Force As Double
    '    Dim BUB As Long
    '    Dim StartTick, EndTick, ElapTick As Long
    '    Dim DistX As Double
    '    Dim DistY As Double
    '    Dim Dist As Double
    '    Dim DistSqrt As Double
    '    Dim bolRocheLimit As Boolean = False
    '    Dim NewBalls As New List(Of BallParms)
    '    Dim MyParams As ThreadStruct = e.Argument
    '    ' Do Until bolStopWorker
    '    uBoundBody = MyParams.UpperBound
    '    lBoundBody = MyParams.LowerBound
    '    Bodys = MyParams.Bodys
    '    'Do While bolStopLoop
    '    '    Thread.Sleep(100)
    '    'Loop
    '    '  StartTick = Now.Ticks
    '    '  Thread.Sleep(intDelay)
    '    '  StartTimer()
    '    If uBoundBody > 1 Then
    '        BUB = uBoundBody
    '        For A = lBoundBody To uBoundBody
    '            If Ball(A).Visible Then
    '                If Ball(A).MovinG = False Then
    '                    For B = 1 To UBound(Bodys)
    '                        If Ball(B).Visible And A <> B Then
    '                            If bolShawdow Then
    '                                If InStr(1, Ball(A).Flags, "S") Then
    '                                    Dim m As Double, SlX As Double, SlY As Double
    '                                    SlX = Ball(B).LocX - Ball(A).LocX
    '                                    SlY = Ball(B).LocY - Ball(A).LocY
    '                                    m = SlY / SlX
    '                                    Ball(B).ShadAngle = Math.Atan2(Ball(B).LocY - Ball(A).LocY, Ball(B).LocX - Ball(A).LocX)   'Math.Tan(SlX / SlY) 'Math.Atan2(SlY, SlX) * 180 / PI 'Math.Atan(SlX / SlY) * 180 / PI
    '                                End If
    '                            End If
    '                            If Ball(B).LocX = Ball(A).LocX And Ball(B).LocY = Ball(A).LocY Then
    '                                CollideBodies(Ball(B), Ball(A))
    '                            End If
    '                            If bGrav = 0 Then
    '                            Else

    '                                DistX = Ball(B).LocX - Ball(A).LocX
    '                                DistY = Ball(B).LocY - Ball(A).LocY
    '                                Dist = (DistX * DistX) + (DistY * DistY)
    '                                DistSqrt = Sqrt(Dist)
    '                                If DistSqrt > 0 Then 'Gravity reaction
    '                                    If DistSqrt < (Ball(A).Size / 2) + (Ball(B).Size / 2) Then DistSqrt = (Ball(A).Size / 2) + (Ball(B).Size / 2) 'prevent screamers
    '                                    M1 = Ball(A).Mass '^ 2
    '                                    M2 = Ball(B).Mass ' ^ 2
    '                                    TotMass = M1 * M2
    '                                    Force = TotMass / (Dist * DistSqrt)
    '                                    'Ball(A).ForceX = Force * DistX
    '                                    'Ball(A).ForceY = Force * DistY

    '                                    ForceX = Force * DistX
    '                                    ForceY = Force * DistY

    '                                    Ball(A).SpeedX += StepMulti * ForceX / M1
    '                                    Ball(A).SpeedY += StepMulti * ForceY / M1


    '                                    If DistSqrt < 50 Then
    '                                        If Ball(B).Mass > Ball(A).Mass * 5 Then
    '                                            If Force > Ball(A).Mass / 2 Then 'And Ball(B).Mass > Ball(A).Mass * 5 Then
    '                                                bolRocheLimit = True
    '                                            ElseIf (Force * 1.5) < Ball(A).Mass / 2 Then
    '                                                bolRocheLimit = False
    '                                                Ball(A).IsFragment = False

    '                                            End If
    '                                            If bolRocheLimit And Ball(A).Size > 1 Then
    '                                                NewBalls.AddRange(FractureBall(A))
    '                                            End If
    '                                        Else
    '                                            bolRocheLimit = False
    '                                        End If

    '                                        If DistSqrt <= (Ball(A).Size / 2) + (Ball(B).Size / 2) Then 'Collision reaction
    '                                            If Not bolRocheLimit Then
    '                                                If Ball(A).Mass > Ball(B).Mass Then
    '                                                    CollideBodies(Ball(A), Ball(B))
    '                                                Else
    '                                                    CollideBodies(Ball(B), Ball(A))
    '                                                End If
    '                                            End If
    '                                        End If
    '                                    End If
    '                                Else
    '                                    '   Debugger.Break()
    '                                End If
    '                                ' StartTimer()

    '                                '  StopTimer()

    '                                'End If
    '                                '  If Ball(A).Mass > 350 And Ball(A).Visible = True Then 'solar wind
    '                                'If InStr(Ball(A).Flags, "S") = 0 Then Ball(A).Flags = Ball(A).Flags + "S"
    '                                'rc = (Ball(B).Size / 4) + (Ball(A).Size / 4)
    '                                'ry = (Ball(B).LocY - Ball(A).LocY) / 2
    '                                'rx = (Ball(B).LocX - Ball(A).LocX) / 2
    '                                'd = Sqrt(rx * rx + ry * ry)
    '                                'If d < 500 Then
    '                                '    Dim m As Double, SlX As Double, SlY As Double
    '                                '    Dim VecX As Double, VecY As Double
    '                                '    Dim C As Double, S As Double
    '                                '    Dim Dis As Double, DisSqr As Double, F As Double, Lx As Double, Ly As Double
    '                                '    SlX = Ball(B).LocX - Ball(A).LocX
    '                                '    SlY = Ball(B).LocY - Ball(A).LocY
    '                                '    m = SlY / SlX
    '                                '    a = Math.Atan2(Ball(B).LocY - Ball(A).LocY, Ball(B).LocX - Ball(A).LocX)
    '                                '    C = Cos(a)
    '                                '    S = Sin(a)
    '                                '    VecX = (Ball(B).LocX + Ball(B).Size * C) - Ball(B).LocX
    '                                '    VecY = (Ball(B).LocY + Ball(B).Size * S) - Ball(B).LocY
    '                                '    Lx = Ball(B).LocX - Ball(A).LocX
    '                                '    Ly = Ball(B).LocY - Ball(A).LocY
    '                                '    Dis = (Lx * Lx) + (Ly * Ly)
    '                                '    DisSqr = Sqrt(Dis)
    '                                '    F = (Ball(A).Mass ^ 2) / (Dis * DisSqr)
    '                                '    Ball(B).SpeedX = Ball(B).SpeedX + F * VecX
    '                                '    Ball(B).SpeedY = Ball(B).SpeedY + F * VecY
    '                            End If
    '                        End If
    '                        ' UpdateBody(Ball(A))
    '                    Next B
    '                End If
    '            End If
    '            Ball(A).LocX = Ball(A).LocX + (StepMulti * Ball(A).SpeedX)
    '            Ball(A).LocY = Ball(A).LocY + (StepMulti * Ball(A).SpeedY)
    '        Next A
    '    End If
    '    ''If UBound(Ball) > 10000 Then
    '    ''    ShrinkBallArray()
    '    ''End If
    '    If NewBalls.Count > 0 Then
    '        AddNewBalls(NewBalls)
    '    End If
    '    'pWorkerThread.ReportProgress(1, Ball)
    '    e.Result = Ball
    '    'EndTick = Now.Ticks
    '    'ElapTick = EndTick - StartTick
    '    'FPS = 10000000 / ElapTick
    '    'If FPS > intTargetFPS Then
    '    '    intDelay = intDelay + 1
    '    'Else
    '    '    If intDelay > 0 Then
    '    '        intDelay = intDelay - 1
    '    '    Else
    '    '        intDelay = 0
    '    '    End If
    '    'End If
    '    '   StopTimer()
    '    '  Loop
    'End Sub
    'Private Sub pThreadCompletion(ByVal sender As Object,
    '                               ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
    '    ' Debug.Print("Render complete " & Now.Ticks)
    '    Dim PassBall() As BallParms = e.Result
    '    If bolDraw And Not bolDrawing Then Drawr(PassBall) '  Me.Render.Image = Drawr(PassBall)
    'End Sub
    'Public ThreadNum As Integer ' = 1
    'Private StartTick, EndTick, ElapTick As Long
    'Public SeekIndex As Integer = 0
    'Public Prev_SeekIndex As Integer = 0
    'Private Sub PhysicsWorker_DoWork(sender As Object, events As DoWorkEventArgs) Handles PhysicsWorker.DoWork

    '    '  Try
    '    InitGPU()

    '        Dim BodyDiv As Integer


    '        Dim RunThreads As Integer
    '        ' Dim PlayArray() As BallParms
    '        Do Until bolStopWorker

    '            bolRendering = True
    '            If RunThreads <> ThreadNum Then RunThreads = ThreadNum

    '            'Do While bolStopLoop
    '            '    Thread.Sleep(100)
    '            'Loop
    '            'StartTick = Now.Ticks
    '            'Thread.Sleep(intDelay)

    '            ExecDelay()

    '            'Start loop
    '            'Calc Splits
    '            ' Ball = CullBodies(Ball)
    '            If Not bolPlaying Then
    '                'IF NOT PLAYING THEN RENDER NORMALLY*****************


    '                ' If UBound(Ball) > (VisibleBalls() * 2) 
    '                If (UBound(Ball) - VisibleBalls()) > 1000 Then
    '                    Ball = CullBodies(Ball)
    '                End If

    '                BodyDiv = Int(UBound(Ball) / RunThreads)
    '                Dim ExtraBodys As Integer = UBound(Ball) - (BodyDiv * RunThreads)
    '                ' Debug.Print(ExtraBodys)
    '                Dim Threads As New List(Of PhysicsChunk)
    '                Dim UB, LB As Integer
    '                'For i As Integer = 1 To RunThreads
    '                '    If i = 1 Then
    '                '        Threads.Add(New PhysicsChunk(BodyDiv, 0, Ball))
    '                '    Else
    '                '        LB = (BodyDiv * (i - 1)) + 1
    '                '        UB = BodyDiv * (i)
    '                '        If i = RunThreads Then UB += ExtraBodys
    '                '        Threads.Add(New PhysicsChunk(UB, LB, Ball))
    '                '    End If
    '                'Next

    '                'Dim rThreads As New List(Of Thread)
    '                'For Each trd As PhysicsChunk In Threads
    '                '    rThreads.Add(New Thread(New ThreadStart(AddressOf trd.CalcPhysics)))
    '                'Next



    '                'For Each rtrd As Thread In rThreads
    '                '    rtrd.Start()
    '                'Next

    '                'Dim bolThreadsDone As Boolean = False
    '                'Do Until bolThreadsDone 'rThread1.ThreadState = ThreadState.Stopped And rThread2.ThreadState = ThreadState.Stopped And rThread3.ThreadState = ThreadState.Stopped And rThread4.ThreadState = ThreadState.Stopped And rThread5.ThreadState = ThreadState.Stopped And rThread6.ThreadState = ThreadState.Stopped And rThread7.ThreadState = ThreadState.Stopped And rThread8.ThreadState = ThreadState.Stopped
    '                '    Thread.Sleep(1)
    '                '    Dim CompleteThreads As Integer = 0
    '                '    For Each rtrd As Thread In rThreads
    '                '        If rtrd.ThreadState = ThreadState.Stopped Then CompleteThreads += 1
    '                '    Next
    '                '    If CompleteThreads = RunThreads Then bolThreadsDone = True
    '                'Loop
    '                Dim mb As Integer = 1024 * 1024

    '                Dim chunk As New PhysicsChunk(UBound(Ball), 0, Ball)
    '                Dim gMemIn, gMemOut, cMemout
    '                gMemIn = gpu.Allocate(Of Single)(mb)
    '                gMemOut = gpu.Allocate(Of Single)(mb)
    '            gpu.CopyToDevice(Ball, Ball)
    '            gpu.Launch(1, 1, chunk.CalcPhysics, gMemIn, gMemOut)



    '                gpu.CopyFromDevice(gMemOut, cMemout)


    '                Ball = gMemOut

    '                Dim AllBodys As New List(Of BallParms)
    '                'For Each trd As PhysicsChunk In Threads
    '                '    AllBodys.AddRange(trd.MyBodys)
    '                'Next

    '                Ball = AllBodys.ToArray
    '                '  Debug.Print(UBound(Ball))

    '                If bolStoring Then
    '                    '  Dim BodyFrame
    '                    'RecordedBodies.Add(Ball)
    '                    '   StartTimer()
    '                    RecordFrame(Ball)
    '                    ' StopTimer()
    '                    'Using s As Stream = New MemoryStream()

    '                    '    ' Dim formatter As ProtoBuf.Serializer 'System.Runtime.Serialization.Formatters.Binary.BinaryFormatter

    '                    '    ProtoBuf.Serializer.Serialize(s, RecordedBodies.Item(1))
    '                    '    Debug.Print(RecordedBodies.Count & " - " & s.Length / 1024)
    '                    'End Using
    '                End If

    '                PhysicsWorker.ReportProgress(1, Ball)

    '            ElseIf bolPlaying Then
    '                'ELSE IF IS PLAYING THEN CYCLE REPLAY

    '                Dim PlayArray()() As Body_Rec_Parms = CompRecBodies.ToArray 'BallParms = RecordedBodies.ToArray



    '                For i = 0 To UBound(PlayArray(1)) 'Each b() As BallParms In RecordedBodies
    '                    ExecDelay()
    '                    If SeekIndex <> Prev_SeekIndex Then
    '                        i = SeekIndex
    '                        Prev_SeekIndex = SeekIndex
    '                    End If
    '                    Ball = ConvertFrame(PlayArray(i))

    '                    PhysicsWorker.ReportProgress(i, Ball)

    '                    CalcDelay()
    '                    bolRendering = False
    '                Next i


    '                'For Each b() As BallParms In RecordedBodies
    '                '    ExecDelay()
    '                '    Ball = b

    '                '    PhysicsWorker.ReportProgress(RecordedBodies.IndexOf(b), Ball)

    '                '    CalcDelay()
    '                'Next
    '            End If

    '            'END PLAYING CONDITION




    '            'End loop
    '            'EndTick = Now.Ticks
    '            'ElapTick = EndTick - StartTick
    '            'RenderTime = ElapTick / 10000
    '            'FPS = 10000000 / ElapTick
    '            'If FPS > intTargetFPS Then
    '            '    intDelay = intDelay + 1
    '            'Else
    '            '    If intDelay > 0 Then
    '            '        intDelay = intDelay - 1
    '            '    Else
    '            '        intDelay = 0
    '            '    End If
    '            'End If
    '            CalcDelay()
    '            bolRendering = False


    '        Loop



    '    'Catch ex As Exception
    '    '    Debug.Print(ex.Message)
    '    'End Try


    'End Sub
    Public Sub RecordFrame(Bodies() As BallParms)
        ' Dim tmpCompBodies(0) As Body_Rec_Parms
        Dim tmpCompBodies(UBound(Bodies)) As Body_Rec_Parms
        ' Dim i As Integer = 0
        'For Each body As BallParms In Bodies
        For s As Integer = 0 To UBound(Bodies)
            '  If Bodies(s).Visible Then
            tmpCompBodies(s).Size = Bodies(s).Size
            tmpCompBodies(s).LocX = Bodies(s).LocX
            tmpCompBodies(s).LocY = Bodies(s).LocY
            tmpCompBodies(s).Visible = Bodies(s).Visible
            tmpCompBodies(s).Flags = Bodies(s).Flags
            tmpCompBodies(s).Color = Bodies(s).Color
            tmpCompBodies(s).UID = Bodies(s).UID
            'ReDim Preserve tmpCompBodies(UBound(tmpCompBodies) + 1)
            '    i += 1
            '  End If

        Next
        CompRecBodies.Add(tmpCompBodies)
    End Sub
    Public Function ConvertFrame(Bodies() As Body_Rec_Parms) As BallParms()
        Dim tmpCompBodies(UBound(Bodies)) As BallParms
        'Dim i As Integer = 0
        For s As Integer = 0 To UBound(Bodies)
            'For Each body As Body_Rec_Parms In Bodies

            'tmpCompBodies(i).Size = body.Size
            'tmpCompBodies(i).LocX = body.LocX
            'tmpCompBodies(i).LocY = body.LocY
            'tmpCompBodies(i).Visible = body.Visible
            'tmpCompBodies(i).Flags = body.Flags
            'tmpCompBodies(i).Color = body.Color

            tmpCompBodies(s).Size = Bodies(s).Size
            tmpCompBodies(s).LocX = Bodies(s).LocX
            tmpCompBodies(s).LocY = Bodies(s).LocY
            tmpCompBodies(s).Visible = Bodies(s).Visible
            tmpCompBodies(s).Flags = Bodies(s).Flags
            tmpCompBodies(s).Color = Bodies(s).Color
            tmpCompBodies(s).UID = Bodies(s).UID



            'ReDim Preserve tmpCompBodies(UBound(tmpCompBodies) + 1)
            'i += 1


        Next
        'CompRecBodies.Add(tmpCompBodies)
        Return tmpCompBodies
    End Function




    Private Sub PhysicsWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles PhysicsWorker.ProgressChanged
        ' Debug.Print("Render complete " & Now.Ticks)
        'Dim tmpBodys As New List(Of BallParms)
        'tmpBodys.AddRange(e.UserState)

        Dim PassBall() As Prim_Struct = e.UserState ' tmpBodys.ToArray
        If bolPlaying Then

            SeekBar.Value = e.ProgressPercentage
            Me.Invalidate()
        End If
        If bolDraw And Not bolDrawing Then Drawr(PassBall) '  Me.Render.Image = Drawr(PassBall)

    End Sub
    'Public Function CalcPhysics(iUpperBody As Integer, iLowerBody As Integer, MainBodyArray() As BallParms) As List(Of BallParms)

    '    Dim uBoundBody, lBoundBody As Integer
    '    Dim Bodys() As BallParms
    '    Dim M1 As Double
    '    Dim M2 As Double
    '    'Dim ClsForce As Double
    '    Dim ForceX As Double
    '    Dim ForceY As Double
    '    Dim TotMass As Double
    '    Dim Force As Double
    '    Dim BUB As Long
    '    Dim StartTick, EndTick, ElapTick As Long
    '    Dim DistX As Double
    '    Dim DistY As Double
    '    Dim Dist As Double
    '    Dim DistSqrt As Double
    '    Dim bolRocheLimit As Boolean = False
    '    Dim NewBalls As New List(Of BallParms)
    '    uBoundBody = iUpperBody
    '    lBoundBody = iLowerBody
    '    Bodys = MainBodyArray


    '    ' Do Until bolStopWorker


    '    'Do While bolStopLoop
    '    '    Thread.Sleep(100)
    '    'Loop
    '    '  StartTick = Now.Ticks
    '    '  Thread.Sleep(intDelay)
    '    '  StartTimer()
    '    If uBoundBody > 1 Then
    '        BUB = uBoundBody
    '        For A = lBoundBody To uBoundBody
    '            If Ball(A).Visible Then
    '                If Ball(A).MovinG = False Then
    '                    For B = 1 To UBound(Bodys)
    '                        If Ball(B).Visible And A <> B Then
    '                            If bolShawdow Then
    '                                If InStr(1, Ball(A).Flags, "S") Then
    '                                    Dim m As Double, SlX As Double, SlY As Double
    '                                    SlX = Ball(B).LocX - Ball(A).LocX
    '                                    SlY = Ball(B).LocY - Ball(A).LocY
    '                                    m = SlY / SlX
    '                                    Ball(B).ShadAngle = Math.Atan2(Ball(B).LocY - Ball(A).LocY, Ball(B).LocX - Ball(A).LocX)   'Math.Tan(SlX / SlY) 'Math.Atan2(SlY, SlX) * 180 / PI 'Math.Atan(SlX / SlY) * 180 / PI
    '                                End If
    '                            End If
    '                            If Ball(B).LocX = Ball(A).LocX And Ball(B).LocY = Ball(A).LocY Then
    '                                CollideBodies(Ball(B), Ball(A))
    '                            End If
    '                            If bGrav = 0 Then
    '                            Else

    '                                DistX = Ball(B).LocX - Ball(A).LocX
    '                                DistY = Ball(B).LocY - Ball(A).LocY
    '                                Dist = (DistX * DistX) + (DistY * DistY)
    '                                DistSqrt = Sqrt(Dist)
    '                                If DistSqrt > 0 Then 'Gravity reaction
    '                                    If DistSqrt < (Ball(A).Size / 2) + (Ball(B).Size / 2) Then DistSqrt = (Ball(A).Size / 2) + (Ball(B).Size / 2) 'prevent screamers
    '                                    M1 = Ball(A).Mass '^ 2
    '                                    M2 = Ball(B).Mass ' ^ 2
    '                                    TotMass = M1 * M2
    '                                    Force = TotMass / (Dist * DistSqrt)
    '                                    'Ball(A).ForceX = Force * DistX
    '                                    'Ball(A).ForceY = Force * DistY

    '                                    ForceX = Force * DistX
    '                                    ForceY = Force * DistY

    '                                    Ball(A).SpeedX += StepMulti * ForceX / M1
    '                                    Ball(A).SpeedY += StepMulti * ForceY / M1


    '                                    If DistSqrt < 50 Then
    '                                        If Ball(B).Mass > Ball(A).Mass * 5 Then
    '                                            If Force > Ball(A).Mass / 2 Then 'And Ball(B).Mass > Ball(A).Mass * 5 Then
    '                                                bolRocheLimit = True
    '                                            ElseIf (Force * 1.5) < Ball(A).Mass / 2 Then
    '                                                bolRocheLimit = False
    '                                                Ball(A).IsFragment = False

    '                                            End If
    '                                            If bolRocheLimit And Ball(A).Size > 1 Then
    '                                                NewBalls.AddRange(FractureBall(A))
    '                                            End If
    '                                        Else
    '                                            bolRocheLimit = False
    '                                        End If

    '                                        If DistSqrt <= (Ball(A).Size / 2) + (Ball(B).Size / 2) Then 'Collision reaction
    '                                            If Not bolRocheLimit Then
    '                                                If Ball(A).Mass > Ball(B).Mass Then
    '                                                    CollideBodies(Ball(A), Ball(B))
    '                                                Else
    '                                                    CollideBodies(Ball(B), Ball(A))
    '                                                End If
    '                                            End If
    '                                        End If
    '                                    End If
    '                                Else
    '                                    '   Debugger.Break()
    '                                End If
    '                                ' StartTimer()

    '                                '  StopTimer()

    '                                'End If
    '                                '  If Ball(A).Mass > 350 And Ball(A).Visible = True Then 'solar wind
    '                                'If InStr(Ball(A).Flags, "S") = 0 Then Ball(A).Flags = Ball(A).Flags + "S"
    '                                'rc = (Ball(B).Size / 4) + (Ball(A).Size / 4)
    '                                'ry = (Ball(B).LocY - Ball(A).LocY) / 2
    '                                'rx = (Ball(B).LocX - Ball(A).LocX) / 2
    '                                'd = Sqrt(rx * rx + ry * ry)
    '                                'If d < 500 Then
    '                                '    Dim m As Double, SlX As Double, SlY As Double
    '                                '    Dim VecX As Double, VecY As Double
    '                                '    Dim C As Double, S As Double
    '                                '    Dim Dis As Double, DisSqr As Double, F As Double, Lx As Double, Ly As Double
    '                                '    SlX = Ball(B).LocX - Ball(A).LocX
    '                                '    SlY = Ball(B).LocY - Ball(A).LocY
    '                                '    m = SlY / SlX
    '                                '    a = Math.Atan2(Ball(B).LocY - Ball(A).LocY, Ball(B).LocX - Ball(A).LocX)
    '                                '    C = Cos(a)
    '                                '    S = Sin(a)
    '                                '    VecX = (Ball(B).LocX + Ball(B).Size * C) - Ball(B).LocX
    '                                '    VecY = (Ball(B).LocY + Ball(B).Size * S) - Ball(B).LocY
    '                                '    Lx = Ball(B).LocX - Ball(A).LocX
    '                                '    Ly = Ball(B).LocY - Ball(A).LocY
    '                                '    Dis = (Lx * Lx) + (Ly * Ly)
    '                                '    DisSqr = Sqrt(Dis)
    '                                '    F = (Ball(A).Mass ^ 2) / (Dis * DisSqr)
    '                                '    Ball(B).SpeedX = Ball(B).SpeedX + F * VecX
    '                                '    Ball(B).SpeedY = Ball(B).SpeedY + F * VecY
    '                            End If
    '                        End If
    '                        ' UpdateBody(Ball(A))
    '                    Next B
    '                End If
    '            End If
    '            Ball(A).LocX = Ball(A).LocX + (StepMulti * Ball(A).SpeedX)
    '            Ball(A).LocY = Ball(A).LocY + (StepMulti * Ball(A).SpeedY)
    '        Next A
    '    End If
    '    ''If UBound(Ball) > 10000 Then
    '    ''    ShrinkBallArray()
    '    ''End If
    '    If NewBalls.Count > 0 Then
    '        AddNewBalls(NewBalls)
    '    End If
    '    '*******  PhysicsWorker.ReportProgress(1, Ball) ******
    '    'EndTick = Now.Ticks
    '    'ElapTick = EndTick - StartTick
    '    'FPS = 10000000 / ElapTick
    '    'If FPS > intTargetFPS Then
    '    '    intDelay = intDelay + 1
    '    'Else
    '    '    If intDelay > 0 Then
    '    '        intDelay = intDelay - 1
    '    '    Else
    '    '        intDelay = 0
    '    '    End If
    '    'End If
    '    '   StopTimer()
    '    '  Loop
    'End Function
    Private Sub UpdateBody(ByRef Body As BallParms)

        Body.SpeedX += StepMulti * Body.ForceX / Body.Mass
        Body.SpeedY += StepMulti * Body.ForceY / Body.Mass

        Body.LocX += StepMulti * Body.SpeedX
        Body.LocY += StepMulti * Body.SpeedY

    End Sub
    'Private Sub CollideBodies(ByRef Master As BallParms, ByRef Slave As BallParms)
    '    Dim VeKY As Double
    '    Dim VekX As Double
    '    Dim V1x As Double
    '    Dim V2x As Double
    '    Dim M1 As Double
    '    Dim M2 As Double
    '    Dim V1y As Double
    '    Dim V2y As Double

    '    ' Dim NewVelX1, NewVelY1, NewVelX2, NewVelY2 As Double


    '    Dim V1 As Double
    '    Dim V2 As Double
    '    Dim U2 As Double
    '    Dim U1 As Double
    '    Dim DistX As Double
    '    Dim DistY As Double
    '    Dim Dist As Double
    '    Dim DistSqrt As Double
    '    Dim PrevSpdX, PrevSpdY As Double

    '    Dim Area1 As Double, Area2 As Double

    '    DistX = Slave.LocX - Master.LocX
    '    DistY = Slave.LocY - Master.LocY
    '    Dist = (DistX * DistX) + (DistY * DistY)
    '    DistSqrt = Sqrt(Dist)
    '    ' Debug.Print("Col dist:" & DistSqrt)
    '    If DistSqrt > 0 Then
    '        If Not Master.IsFragment Then

    '            Slave.Visible = False
    '            ' Master.IsFragment = False
    '            V1x = Master.SpeedX
    '            V1y = Master.SpeedY
    '            V2x = Slave.SpeedX
    '            V2y = Slave.SpeedY

    '            M1 = Master.Mass
    '            M2 = Slave.Mass

    '            VekX = DistX / 2 ' (Ball(A).LocX - Ball(B).LocX) / 2
    '            VeKY = DistY / 2 '(Ball(A).LocY - Ball(B).LocY) / 2

    '            VekX = VekX / (DistSqrt / 2) 'LenG
    '            VeKY = VeKY / (DistSqrt / 2) 'LenG

    '            V1 = VekX * V1x + VeKY * V1y
    '            V2 = VekX * V2x + VeKY * V2y

    '            U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
    '            '   U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)


    '            PrevSpdX = Master.SpeedX
    '            PrevSpdY = Master.SpeedY


    '            Master.SpeedX = Master.SpeedX + (U1 - V1) * VekX
    '            Master.SpeedY = Master.SpeedY + (U1 - V1) * VeKY

    '            'If Abs(Master.SpeedX - PrevSpdX) > 100 Or Abs(Master.SpeedY - PrevSpdY) > 100 Then

    '            '    Debugger.Break()

    '            'End If


    '            Area1 = PI * (Master.Size ^ 2)
    '            Area2 = PI * (Slave.Size ^ 2)
    '            Area1 = Area1 + Area2
    '            Master.Size = Sqrt(Area1 / PI)
    '            Master.Mass = Master.Mass + Slave.Mass 'Sqr(Ball(B).Mass)






    '            'If Sqrt(Master.Mass) * 3 > 350 Then Master.Color = System.Drawing.Color.Red
    '            'If Sqrt(Master.Mass) * 3 > 400 Then Master.Color = System.Drawing.Color.Yellow
    '            'If Sqrt(Master.Mass) * 3 > 500 Then Master.Color = System.Drawing.Color.White
    '            'If Sqrt(Master.Mass) * 3 > 600 Then Master.Color = System.Drawing.Color.LightCyan
    '            'If Sqrt(Master.Mass) * 3 > 700 Then Master.Color = System.Drawing.Color.LightBlue
    '            'If Sqrt(Master.Mass) * 3 > 1000 Then
    '            '    Master.Color = Color.Black
    '            '    Master.Size = 20
    '            '    If InStr(1, Master.Flags, "BH") = 0 Then Master.Flags = Master.Flags + "BH"
    '            'End If


    '            If Master.Flags.Contains("BH") Or Master.Mass >= TypicalSolarMass * 18 Then
    '                Master.Color = Color.Black
    '                Master.Size = 15
    '                If InStr(1, Master.Flags, "BH") = 0 Then Master.Flags = Master.Flags + "BH"
    '            End If


    '            If Master.Mass >= TypicalSolarMass * 0.3 Then Master.Color = System.Drawing.Color.Red
    '            If Master.Mass >= TypicalSolarMass * 0.8 Then Master.Color = System.Drawing.Color.Gold
    '            If Master.Mass >= TypicalSolarMass Then Master.Color = System.Drawing.Color.GhostWhite
    '            If Master.Mass >= TypicalSolarMass * 1.7 Then Master.Color = System.Drawing.Color.CornflowerBlue
    '            If Master.Mass >= TypicalSolarMass * 3.2 Then Master.Color = System.Drawing.Color.DeepSkyBlue

    '        End If

    '    Else ' if bodies are at exact same position

    '            'Master.SpeedX = Master.SpeedX + (U1 - V1) * VekX
    '            ' Master.SpeedY = Master.SpeedY + (U1 - V1) * VeKY

    '            If Master.Mass > Slave.Mass Then

    '            Area1 = PI * (Master.Size ^ 2)
    '            Area2 = PI * (Slave.Size ^ 2)
    '            Area1 = Area1 + Area2
    '            Master.Size = Sqrt(Area1 / PI)
    '            Master.Mass = Master.Mass + Slave.Mass 'Sqr(Ball(B).Mass)
    '            Slave.Visible = False
    '        Else

    '            Area1 = PI * (Master.Size ^ 2)
    '            Area2 = PI * (Slave.Size ^ 2)
    '            Area1 = Area1 + Area2
    '            Slave.Size = Sqrt(Area1 / PI)
    '            Slave.Mass = Slave.Mass + Master.Mass 'Sqr(Ball(B).Mass)
    '            Master.Visible = False
    '        End If




    '    End If


    'End Sub
    'Private Sub AddNewBalls(ByRef NewBalls As List(Of BallParms))

    '    For Each AddBall As BallParms In NewBalls
    '        ReDim Preserve Ball(UBound(Ball) + 1)
    '        Dim u As Integer = UBound(Ball)

    '        Ball(u) = AddBall



    '    Next

    '    NewBalls.Clear()
    'End Sub


    Private Sub tmrRender_Tick(sender As Object, e As EventArgs) Handles tmrRender.Tick

        If bolStop Then
            'If bolPlaying Then
            '    '    Ball = ConvertFrame(CompRecBodies(SeekIndex)) 'RecordedBodies(SeekIndex)
            'End If
            Drawr(Ball)
            SetUIInfo()
        End If

    End Sub
    Private Sub TotalMassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TotalMassToolStripMenuItem.Click
        Dim TotalMass As Double
        For i = 1 To UBound(Ball)
            If Ball(i).Visible Then TotalMass = TotalMass + Ball(i).Mass
        Next i
        Debug.Print("Total Mass: " & TotalMass)
        MsgBox("Total Mass: " & TotalMass)
    End Sub
    Private Sub BallLines_Click(sender As Object, e As EventArgs) Handles BallLines.Click
        bolLines = BallLines.Checked
    End Sub
    Private Sub FBallSOI_Click(sender As Object, e As EventArgs) Handles FBallSOI.Click
        bolSOI = FBallSOI.Checked
    End Sub
    Private Sub Trails_Click(sender As Object, e As EventArgs) Handles Trails.Click
        bolTrails = Trails.Checked
    End Sub
    Private Sub Draw_Click(sender As Object, e As EventArgs) Handles Draw.Click
        bolDraw = Draw.Checked
    End Sub
    Private Sub AntiA_Click(sender As Object, e As EventArgs) Handles AntiA.Click
        bolAntiAliasing = AntiA.Checked
    End Sub
    Private Sub Invert_Click(sender As Object, e As EventArgs) Handles Invert.Click
        bolInvert = Invert.Checked
        If bolInvert Then
            colBackColor = Color.White
            colControlForeColor = Color.Black
        Else
            colBackColor = Color.Black
            colControlForeColor = Color.White
        End If
        SetColors(Me)
        'Me.BackColor = colBackColor
        'For Each ctl As Control In Me.Controls
        '    If Not TypeOf ctl Is Label Then
        '        ctl.ForeColor = colControlForeColor
        '        ctl.BackColor = colBackColor
        '    End If
        'Next
    End Sub
    Private Sub UI_Worker_DoWork(sender As Object, e As DoWorkEventArgs) Handles UI_Worker.DoWork
        Do Until bolStopWorker
            Thread.Sleep(100)
            Do While bolStopLoop
                Thread.Sleep(100)
            Loop

            UI_Worker.ReportProgress(1)


        Loop




    End Sub
    Private Sub UI_Worker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles UI_Worker.ProgressChanged
        SetUIInfo()
    End Sub
    Private Sub SetUIInfo()

        'If Me.lblFPS.InvokeRequired Then
        '    Dim d As New SetTextCallback(AddressOf SetUIInfo)
        '    Me.Invoke(d, New Object() {Round(FPS, 0)})
        'Else
        lblFPS.Text = "FPS: " & Round(FPS, 0)
        ' End If

        lblRenTime.Text = "RTime(ms): " & Round(RenderTime, 1)
        'lblFPS.Text = "FPS: " & Round(FPS, 0) ' * 8
        lblDelay.Text = "Delay: " & intDelay
        lblBalls.Text = "Balls: " & UBound(Ball)
        UpDown1.Maximum = UBound(Ball)
        'TrueFPS = FPS * 8
        'If FPS * 8 > intTargetFPS Then
        '    intDelay = intDelay + 1
        'Else
        '    If intDelay > 0 Then
        '        intDelay = intDelay - 1
        '    Else
        '        intDelay = 0
        '    End If
        'End If
        '  
        ' FPS = 0
        lblVisBalls.Text = "Visible: " & VisibleBalls()
        lblScale.Text = "Scale: " & Round(pic_scale, 2)


        If bolStoring Then
            lblRecFrames.Visible = True
            lblRecFrames.Text = "RecFrames: " & CompRecBodies.Count.ToString
            lblRecSize.Visible = True

            lblRecSize.Text = "Size (KB): " & RecSize()
        Else
            lblRecFrames.Visible = False
            lblRecSize.Visible = False
        End If



        ScreenCenterX = Me.Render.Width / 2
        ScreenCenterY = Me.Render.Height / 2
        ScaleOffset.X = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).X
        ScaleOffset.Y = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).Y
        RenderWindowDims.X = CInt(Me.Render.Width)
        RenderWindowDims.Y = CInt(Me.Render.Height)
        If UBound(Ball) > 0 And Not PhysicsWorker.IsBusy And Not bolStopWorker Then
            PhysicsWorker.RunWorkerAsync()
        End If



    End Sub
    Private Function RecSize() As Double
        Dim Bodies As Integer
        For i As Integer = 0 To UBound(CompRecBodies.ToArray)

            Bodies += CompRecBodies(i).Count


        Next

        Bodies *= 43
        Bodies \= 1000

        Return Bodies

    End Function
    Private Sub cmdTrails_Click(sender As Object, e As EventArgs) Handles cmdTrails.Click
        bolTrails = Not bolTrails
        Select Case bolTrails
            Case True
                cmdTrails.BackColor = Color.DarkGreen
            Case False
                cmdTrails.BackColor = Color.DarkRed
        End Select
    End Sub

    Private Sub TimeStep_ValueChanged(sender As Object, e As EventArgs) Handles TimeStep.ValueChanged
        StepMulti = TimeStep.Value
    End Sub

    Private Sub tsmCull_Click(sender As Object, e As EventArgs) Handles tsmCull.Click
        bolCulling = tsmCull.Checked
    End Sub

    'Private Sub Button6_Click(sender As Object, e As EventArgs)
    '    AddNewBalls(FractureBall(lngFollowBall))
    'End Sub

    Private Sub Label12_DoubleClick(sender As Object, e As EventArgs) Handles Label12.DoubleClick
        If bGrav = 1 Then
            Label12.Text = "G: Off" : bGrav = 0
        Else
            Label12.Text = "G: On" : bGrav = 1
        End If
    End Sub

    Private Sub tsmSave_Click(sender As Object, e As EventArgs) Handles tsmSave.Click
        Dim SaveDialog As New SaveFileDialog()
        SaveDialog.Filter = "Body State|*.sta"
        SaveDialog.Title = "Save State File"
        SaveDialog.ShowDialog()

        'Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        'Dim fStream As New FileStream(SaveDialog.FileName, FileMode.OpenOrCreate)

        'bf.Serialize(fStream, Ball)

        If SaveDialog.FileName <> "" Then
            Dim fStream As New FileStream(SaveDialog.FileName, FileMode.OpenOrCreate)
            ProtoBuf.Serializer.Serialize(fStream, Ball)
            ' bf.Serialize(fStream, RecordedBodies)
        End If

    End Sub

    Private Sub tsmLoad_Click(sender As Object, e As EventArgs) Handles tsmLoad.Click
        MsgBox("Disabled")
        'Dim OpenDialog As New OpenFileDialog()
        'OpenDialog.Filter = "Body State File|*.sta"
        'OpenDialog.Title = "Open State File"
        'If OpenDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '    ' Dim sr As New System.IO.StreamReader(OpenFileDialog1.FileName)
        '    Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        '    Dim fStream As New FileStream(OpenDialog.FileName, FileMode.OpenOrCreate)

        '    'fStream.Position = 0 ' reset stream pointer
        '    ' Ball = bf.Deserialize(fStream) ' read from file

        '    Ball = ProtoBuf.Serializer.Deserialize(Of BallParms())(fStream)
        '    'sr.Close()
        'End If

    End Sub

    Private Sub NumThreads_ValueChanged(sender As Object, e As EventArgs) Handles NumThreads.ValueChanged
        ' ThreadNum = NumThreads.Value
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '  On Error Resume Next
        RecordedBodies.Clear()
        Dim OpenDialog As New OpenFileDialog()
        OpenDialog.Filter = "Recording File|*.dat"
        OpenDialog.Title = "Open Rendered Recording"
        If OpenDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ' Dim sr As New System.IO.StreamReader(OpenFileDialog1.FileName)
            Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            Dim fStream As New FileStream(OpenDialog.FileName, FileMode.OpenOrCreate)

            ' fStream.Position = 0 ' reset stream pointer



            '_nestedArrayForProtoBuf = ProtoBuf.Serializer.Deserialize(Of List(Of ProtobufArray(Of BallParms)))(fStream) 'bf.Deserialize(fStream) ' read from file
            _nestedArrayForProtoBuf = ProtoBuf.Serializer.Deserialize(Of List(Of ProtobufArray(Of Body_Rec_Parms)))(fStream) 'bf.Deserialize(fStream) ' read from file
            'RecordedBodies = ConvertRecording(CompRecBodies)
            'sr.Close()
        End If

        SeekBar.Maximum = CompRecBodies.Count - 1 'RecordedBodies.Count - 1
        SeekBar.Value = 1





        bolPlaying = Not bolPlaying
        If Not PhysicsWorker.IsBusy And Not bolStopWorker Then
            PhysicsWorker.RunWorkerAsync()
        End If



    End Sub

    Private Sub SeekBar_Scroll(sender As Object, e As EventArgs) Handles SeekBar.Scroll
        'Prev_SeekIndex = SeekBar.Value
        '  SeekIndex = SeekBar.Value
    End Sub

    Private Sub cmdStor_Click(sender As Object, e As EventArgs) Handles cmdStor.Click
        bolStoring = Not bolStoring
        If bolStoring Then
            cmdStor.Text = "Stop Recording"
            cmdStor.BackColor = Color.Red





        Else
            cmdStor.Text = "Record"
            cmdStor.BackColor = colBackColor
            cmdStor.ForeColor = colControlForeColor
            Debug.Print(CompRecBodies.Count)


            ' CompRecBodies = CompressRecording(RecordedBodies)


            Dim SaveDialog As New SaveFileDialog()
            SaveDialog.Filter = "Record File|*.dat"
            SaveDialog.Title = "Save Rendered Recording"
            SaveDialog.ShowDialog()


            RecordFileName = SaveDialog.FileName
            '  Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            If SaveDialog.FileName <> "" Then

                Dim fStream As New FileStream(SaveDialog.FileName, FileMode.OpenOrCreate)
                ProtoBuf.Serializer.Serialize(fStream, _nestedArrayForProtoBuf)

                ' bf.Serialize(fStream, RecordedBodies)
                CompRecBodies.Clear()
            End If


        End If


    End Sub
    Private Function CompressRecording(RawRecording As List(Of BallParms())) As List(Of Body_Rec_Parms())
        Dim CompBodies As New List(Of Body_Rec_Parms)
        Dim CompFrames As New List(Of Body_Rec_Parms())

        For Each Frame() As BallParms In RecordedBodies
            Dim tmpFrame(0) As Body_Rec_Parms
            Dim i As Integer = 0
            For Each body As BallParms In Frame

                If body.Visible Then

                    tmpFrame(i).Size = body.Size
                    tmpFrame(i).LocX = body.LocX
                    tmpFrame(i).LocY = body.LocY
                    tmpFrame(i).Visible = body.Visible
                    tmpFrame(i).Flags = body.Flags
                    tmpFrame(i).Color = body.Color
                    ReDim Preserve tmpFrame(UBound(tmpFrame) + 1)
                    i += 1
                    'CompBodies.Add(tmpFrame)
                End If
            Next
            CompFrames.Add(tmpFrame)

        Next

        Return CompFrames
    End Function
    Public Function ConvertRecording(CompressRec As List(Of Body_Rec_Parms())) As List(Of BallParms())
        Dim Bodies As New List(Of BallParms)
        Dim Frames As New List(Of BallParms())

        For Each Frame() As Body_Rec_Parms In CompressRec
            Dim tmpFrame(0) As BallParms
            Dim i As Integer = 0
            For Each body As Body_Rec_Parms In Frame

                If body.Visible Then

                    tmpFrame(i).Size = body.Size
                    tmpFrame(i).LocX = body.LocX
                    tmpFrame(i).LocY = body.LocY
                    tmpFrame(i).Visible = body.Visible
                    tmpFrame(i).Flags = body.Flags
                    tmpFrame(i).Color = body.Color
                    ReDim Preserve tmpFrame(UBound(tmpFrame) + 1)
                    i += 1
                    'CompBodies.Add(tmpFrame)
                End If
            Next
            Frames.Add(tmpFrame)

        Next

        Return Frames





    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If UBound(Ball) > 0 Then
            If Not bolLoopRunning Then
                StartCalc()
                UI_Worker.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Debug.Print(RecordedBodies.Count)

        bolStoring = False

        Dim SaveDialog As New SaveFileDialog()
        SaveDialog.Filter = "Record File|*.dat"
        SaveDialog.Title = "Save Rendered Recording"
        SaveDialog.ShowDialog()


        RecordFileName = SaveDialog.FileName
        '  Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        If SaveDialog.FileName <> "" Then
            Dim fStream As New FileStream(SaveDialog.FileName, FileMode.OpenOrCreate)
            ProtoBuf.Serializer.Serialize(fStream, _nestedArrayForProtoBuf)
            ' bf.Serialize(fStream, RecordedBodies)
        End If

    End Sub

    Private Sub PhysicsWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles PhysicsWorker.RunWorkerCompleted

    End Sub

    Private Sub PhysicsWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles PhysicsWorker.DoWork
        InitGPU()

        Do Until bolStopWorker
            '  bolRendering = True


            If Not bolRendering Then StartCalc()



            ' bolRendering = False
            ' If Not bolDrawing Then Drawr(Ball)
            CalcDelay()

            PhysicsWorker.ReportProgress(1, Ball)
            ExecDelay()
        Loop

    End Sub

    Private Sub tsmShowAll_Click(sender As Object, e As EventArgs) Handles tsmShowAll.Click
        bolShowAll = tsmShowAll.Checked
    End Sub
End Class
