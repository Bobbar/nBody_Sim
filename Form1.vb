


Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.IO

Public Class Form1

    Public TrueFPS As Integer


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

    Public Sub ShrinkBallArray()
        'On Error Resume Next

        Debug.Print("Cleaning Ball Array")
        Dim TempArray() As BallParms


        '       t.Abort()
        '  bolStopLoop = True

        't.Join()
        '


        ReDim TempArray(UBound(Ball))

        Array.Copy(Ball, TempArray, Ball.Length)


        ReDim Ball(0)


        For i = 0 To UBound(TempArray)


            If TempArray(i).Visible = True Then
                ReDim Preserve Ball(UBound(Ball) + 1)

                Ball(UBound(Ball)) = TempArray(i)
                ' Debug.Print(InStr(1, Ball(UBound(Ball)).Flags, "F"))
                If InStr(1, Ball(UBound(Ball)).Flags, "F") > 0 Then


                    lngFollowBall = UBound(Ball)
                End If

                'Debug.Print lngFollowBall & " " & Ball(lngFollowBall).Flags

            End If






        Next i
        '  bolStopLoop = False

        't.Resume()


        '  t = New Threading.Thread(AddressOf Me.MainLoop)
        '  t.Start()

    End Sub
    'Public Sub DrawLoop()

    '    On Error Resume Next

    '    Do Until bolStopLoop
    '        'If Not bolStop Then Matrix.RunWorkerAsync()

    '        If chkTrails.Checked Then
    '            For i = 1 To UBound(Ball)

    '                If Ball(i).Visible And Ball(i).LocX > 0 And Ball(i).LocX < Render.Width And Ball(i).LocY > 0 And Ball(i).LocY < Render.Height Then
    '                    'g = g + 1
    '                    ' If g > 4 Then g = 0


    '                    ' e.Graphics.FillEllipse(Brushes.LightBlue, ball_loc_x(i) - 1, ball_loc_y(i) - 1, BallSize(i) + 2, BallSize(i) + 2)
    '                    ' e.Graphics.FillEllipse(Brushes.Blue, ball_loc_x(i), ball_loc_y(i), BallSize(i), BallSize(i))

    '                    Dim myBrush As New SolidBrush(Ball(i).Color)
    '                    Render.CreateGraphics.SmoothingMode = SmoothingMode.AntiAlias
    '                    ' Render.CreateGraphics.FillEllipse(Brushes.Black, Ball(i).LocX - Ball(i).Size / 2 - 1, Ball(i).LocY - Ball(i).Size / 2 - 1, Ball(i).Size + 2, Ball(i).Size + 2)
    '                    Render.CreateGraphics.FillEllipse(myBrush, Ball(i).LocX - Ball(i).Size / 2, Ball(i).LocY - Ball(i).Size / 2, Ball(i).Size, Ball(i).Size)

    '                End If

    '            Next

    '        End If

    '        'If bolBallsRemoved Then ShrinkBallArray()
    '        If Not chkTrails.Checked Then
    '            If chkDraw.Checked Then
    '                Render.Refresh()
    '            Else
    '                'Render.e
    '            End If
    '        End If
    '        'Application.DoEvents()
    '    Loop
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
        End
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
        Debug.Print("RenLoc: " & e.Location.ToString)
        If e.Button = Windows.Forms.MouseButtons.Right Then

            bolStopDraw = True

            ReDim Preserve Ball(UBound(Ball) + 1)
            Debug.Print("Index: " & UBound(Ball))

            Ball(UBound(Ball)).Color = RandomRGBColor()
            Ball(UBound(Ball)).LocX = ScaleMousePosRelative(New SPoint(e.Location)).X - (Ball(UBound(Ball)).Size / 2)  ' ScaleMousePosRelative(e.Location).X - Ball(UBound(Ball)).Size / 2
            Ball(UBound(Ball)).LocY = ScaleMousePosRelative(New SPoint(e.Location)).Y - (Ball(UBound(Ball)).Size / 2)
            Ball(UBound(Ball)).SpeedX = 0
            Ball(UBound(Ball)).SpeedY = 0

            Ball(UBound(Ball)).Size = 1
            Ball(UBound(Ball)).Mass = fnMass(Ball(UBound(Ball)).Size)
            Ball(UBound(Ball)).Visible = True
            '  Ball(UBound(Ball)).Flags = Ball(UBound(Ball)).Flags + "BH"
            MouseIndex = UBound(Ball)


            Timer3.Enabled = True

        End If


        If Windows.Forms.MouseButtons.Left Then

            If bolAltDown Then
                tmrFollow.Enabled = False
                bolFollow = False
                Ball(lngFollowBall).Flags = Replace(Ball(lngFollowBall).Flags, "F", "")


                lngFollowBall = 0
            End If


            'For i = 1 To UBound(Ball)
            '    Ball(i).Old_LocX = Ball(i).LocX
            '    Ball(i).Old_LocY = Ball(i).LocY

            'Next
            If Sel = -1 Then
                For i = 1 To UBound(Ball)
                    'Debug.Print "LocX: " & Ball(i).LocX & vbCrLf & "LocY: " & Ball(i).LocY & vbCrLf & "SpeedX: " & Ball(i).SpeedX & vbCrLf & "SpeedY: " & Ball(i).SpeedY


                    If MouseOver(New SPoint(e.Location), Ball(i)) And Ball(i).Visible Then
                        'Debug.Print(Render.PointToClient(New Point(Ball(i).LocX, Ball(i).LocY)).ToString)
                        Debug.Print("BLoc: " & Ball(i).LocX & "-" & Ball(i).LocY)

                        If Not bolAltDown And bolShiftDown Then MoV = 1
                        Sel = i
                        If bolShiftDown Then
                            Ball(Sel).Locked = True
                            '  Ball(Sel).Flags = "BH"
                        End If

                        If bolAltDown Then

                            lngFollowBall = Sel
                            Ball(Sel).Flags = Ball(Sel).Flags + "F"
                            tmrFollow.Enabled = True
                        End If

                        txtSpeedX.Text = Ball(Sel).SpeedX
                        txtSpeedY.Text = Ball(Sel).SpeedY
                        txtSize.Text = Ball(Sel).Size
                        txtMass.Text = Ball(Sel).Mass
                        txtFlag.Text = Ball(Sel).Flags

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

        On Error Resume Next

        If e.Button = Windows.Forms.MouseButtons.Left Then





            'For i = 0 To UBound(Ball)
            '    Ball(i).Old_LocX = Ball(i).LocX
            '    Ball(i).Old_LocY = Ball(i).LocY

            'Next
            If Sel = -1 Then
                For i = 0 To UBound(Ball)

                    'ScaleMousePos(e.X) > Ball(i).LocX And ScaleMousePos(e.X) < Ball(i).LocX + Ball(i).Size And ScaleMousePos(e.Y) > Ball(i).LocY And ScaleMousePos(e.Y) < Ball(i).LocY + Ball(i).Size And Not bolShiftDown Then
                    If MouseOver(New SPoint(e.Location), Ball(i)) And bolShiftDown Then
                        MoV = 1
                        Sel = i
                    End If


                Next
            End If

            If MoV = 1 Then





                Ball(Sel).MovinG = True
                Ball(Sel).LocX = ScaleMousePosRelative(New SPoint(e.Location)).X
                Ball(Sel).LocY = ScaleMousePosRelative(New SPoint(e.Location)).Y
                'ax(Sel) = 0
                'ay(Sel) = 0
                'bCenterX(Sel) = e.X
                'bCenterY(Sel) = e.Y




                If Ball(Sel).Old_LocX < Ball(Sel).LocX - Ball(Sel).Size / 2 Then Ball(Sel).SpeedX = (Ball(Sel).LocX - Ball(Sel).Old_LocX) / 2 'To the right
                If Ball(Sel).Old_LocX > Ball(Sel).LocX - Ball(Sel).Size / 2 Then Ball(Sel).SpeedX = (Ball(Sel).LocX - Ball(Sel).Old_LocX) / 2 'To the left

                If Ball(Sel).Old_LocY < Ball(Sel).LocY - Ball(Sel).Size / 2 Then Ball(Sel).SpeedY = (Ball(Sel).LocY - Ball(Sel).Old_LocY) / 2 'Up
                If Ball(Sel).Old_LocY > Ball(Sel).LocY - Ball(Sel).Size / 2 Then Ball(Sel).SpeedY = (Ball(Sel).LocY - Ball(Sel).Old_LocY) / 2 'Down

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
    End Sub

    Private Function MouseOver(MousePos As SPoint, Ball As BallParms) As Boolean
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


        For i = 0 To UBound(Ball)
            Ball(i).MovinG = False
        Next
        Sel = -1
        MoV = 0

        If Windows.Forms.MouseButtons.Left And bolShiftDown Then
            'Dim MouseXDiff As Double
            'Dim MouseYDiff As Double

            'MouseUpX = e.X
            'MouseUpY = e.Y

            'MouseXDiff = MouseUpX - MouseDnX
            'MouseYDiff = MouseUpY - MouseDnY


            'RelBallPosMod.X = RelBallPosMod.X + MouseXDiff
            'RelBallPosMod.Y = RelBallPosMod.Y + MouseYDiff
            Debug.Print(RelBallPosMod.ToString)
            'For i = 1 To UBound(Ball)

            '    Ball(i).LocX = Ball(i).LocX + MouseXDiff
            '    Ball(i).LocY = Ball(i).LocY + MouseYDiff

            'Next

        End If


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

    Private Sub Timer2_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        Label10.Text = "FPS: " & Round(FPS, 0) ' * 8
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

        ScreenCenterX = Me.Render.Width / 2
        ScreenCenterY = Me.Render.Height / 2

        ScaleOffset.X = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).X
        ScaleOffset.Y = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).Y

        RenderWindowDims.X = CInt(Me.Render.Width)
        RenderWindowDims.Y = CInt(Me.Render.Height)



        If UBound(Ball) > 1 And Not PhysicsWorker.IsBusy And Not bolStopWorker Then
            PhysicsWorker.RunWorkerAsync()
        End If
        'If RenderWindowDims.X <> bm.Size.Width Or RenderWindowDims.Y <> bm.Size.Height Then
        '    UpdateScale()
        'End If

        '  Application.DoEvents()

    End Sub
    Private Function VisibleBalls() As Integer
        Dim tot As Integer = 0
        For i As Integer = 0 To UBound(Ball)
            If Ball(i).Visible Then tot += 1
        Next
        Return tot
    End Function
    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click
        If bGrav = 1 Then

            Label12.Text = "G: Off" : bGrav = 0
        Else

            Label12.Text = "G: On" : bGrav = 1
        End If



    End Sub
    Private Sub Form1_Leave(sender As Object, e As EventArgs) Handles Me.Leave


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

        StepMulti = txtStep.Text
        'fOn = 0
        ReDim Ball(0)
        ' Timer1.Enabled = True
        bolStop = False
        Me.Show()

        'MainLoop()
        ' MasterLoop()


        bolStopLoop = False

        't = New Threading.Thread(AddressOf Me.MainLoop)
        ' t2 = New Threading.Thread(AddressOf Me.MainLoop2)
        ' t3 = New Threading.Thread(AddressOf Me.MainLoop3)
        '    m = New Threading.Thread(AddressOf Me.MasterLoop)
        s = New Threading.Thread(AddressOf Me.ShrinkBallArray)



        Thread1Done = False
        Thread2Done = False
        Thread3Done = False

        bolStart1 = False
        bolStart2 = False
        bolStart3 = False

        PubIndex = 1

        Timer2.Enabled = True
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

    Private Sub Render_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Render.Click

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
            Button3.Text = "Start"
            Timer2.Enabled = False
            tmrFollow.Enabled = False
            't.Suspend()
            ' t2.Suspend()
            ' t3.Suspend()
            bolStopLoop = True


        Else
            Button3.Text = "Stop"
            Timer2.Enabled = True
            tmrFollow.Enabled = True
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
        txtFlag.Text = Ball(lngFollowBall).Flags
        'End If
        'If bolBallsRemoved Then ShrinkBallArray()

    End Sub

    Private Sub butAddBall_Click(sender As Object, e As EventArgs) Handles butAddBall.Click
        On Error Resume Next
        Const Balls As Long = 100
        Dim i As Long
        For i = 0 To Balls
            ReDim Preserve Ball(UBound(Ball) + 1)
            Ball(UBound(Ball)).Color = RandomRGBColor() 'colDefBodyColor
            Ball(UBound(Ball)).Visible = True
            Ball(UBound(Ball)).LocX = GetRandomNumber(1, Render.Width / pic_scale) - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
            Ball(UBound(Ball)).LocY = GetRandomNumber(1, Render.Height / pic_scale) - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
            Ball(UBound(Ball)).SpeedX = 0
            Ball(UBound(Ball)).SpeedY = 0
            Ball(UBound(Ball)).Flags = ""
            Ball(UBound(Ball)).Size = GetRandomNumber(1, 5)
            Ball(UBound(Ball)).Mass = fnMass(Ball(UBound(Ball)).Size) ' * 2
        Next

        'ReDim Preserve Ball(UBound(Ball) + 1)
        'Ball(UBound(Ball)).Color = Color.Red 'RandomRGBColor()
        'Ball(UBound(Ball)).Visible = True
        'Ball(UBound(Ball)).LocX = 500 - ScaleOffset.X - RelBallPosMod.X
        'Ball(UBound(Ball)).LocY = 500 - ScaleOffset.Y - RelBallPosMod.Y
        'Ball(UBound(Ball)).SpeedX = 0
        'Ball(UBound(Ball)).SpeedY = 0
        'Ball(UBound(Ball)).Flags = ""
        'Ball(UBound(Ball)).Size = 100
        'Ball(UBound(Ball)).Mass = 4000

        'ReDim Preserve Ball(UBound(Ball) + 1)
        'Ball(UBound(Ball)).Color = Color.Red 'RandomRGBColor()
        'Ball(UBound(Ball)).Visible = True
        'Ball(UBound(Ball)).LocX = 10500 - ScaleOffset.X - RelBallPosMod.X
        'Ball(UBound(Ball)).LocY = 400 - ScaleOffset.Y - RelBallPosMod.Y
        'Ball(UBound(Ball)).SpeedX = 0
        'Ball(UBound(Ball)).SpeedY = 0
        'Ball(UBound(Ball)).Flags = ""
        'Ball(UBound(Ball)).Size = 100
        'Ball(UBound(Ball)).Mass = 4000


    End Sub

    Private Sub Render_ParentChanged(sender As Object, e As EventArgs) Handles Render.ParentChanged

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

            '   PhysicsWorker.RunWorkerAsync()
        End If

        'MainLoop()

        'Erase Ball

    End Sub


    Private Sub butUpdate_Click(sender As Object, e As EventArgs) Handles butUpdate.Click
        Ball(PubSel).SpeedX = txtSpeedX.Text
        Ball(PubSel).SpeedY = txtSpeedY.Text
        Ball(PubSel).Size = txtSize.Text
        Ball(PubSel).Mass = txtMass.Text
        Ball(PubSel).Flags = txtFlag.Text

    End Sub








    Private Sub Painter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)



    End Sub

    Private Sub Render_PaddingChanged(sender As Object, e As EventArgs) Handles Render.PaddingChanged

    End Sub

    Private Sub txtStep_GotFocus(sender As Object, e As EventArgs) Handles txtStep.GotFocus
        bolStop = True
    End Sub

    Private Sub txtStep_HelpRequested(sender As Object, hlpevent As HelpEventArgs) Handles txtStep.HelpRequested

    End Sub

    Private Sub txtStep_LostFocus(sender As Object, e As EventArgs) Handles txtStep.LostFocus
        bolStop = False

    End Sub

    Private Sub txtStep_TextChanged(sender As Object, e As EventArgs) Handles txtStep.TextChanged
        StepMulti = txtStep.Text
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim TotalMass As Double

        For i = 1 To UBound(Ball)
            If Ball(i).Visible Then TotalMass = TotalMass + Ball(i).Mass




        Next i
        Debug.Print("Total Mass: " & TotalMass)
        MsgBox("Total Mass: " & TotalMass)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Dim cap_image As Image


        Me.Render.Image.Save(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Screenshot-" & Now.ToString("_hhmmss") & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

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


            Ball(lngFollowBall).Flags = Replace$(Ball(lngFollowBall).Flags, "F", "")



            Sel = UpDown1.Value

            If Ball(Sel).Visible = False Or InStr(1, Ball(Sel).Flags, "P") > 0 Or InStr(1, Ball(Sel).Flags, "R") > 0 Then
                Do Until Ball(Sel).Visible And InStr(1, Ball(Sel).Flags, "P") = 0 And InStr(1, Ball(Sel).Flags, "R") = 0 Or Sel = UpDown1.Minimum
                    Sel = Sel - 1



                Loop

            End If

            UpDown1.Value = Sel

            lngFollowBall = Sel
            Ball(lngFollowBall).Flags = Ball(lngFollowBall).Flags + "F"

            txtSpeedX.Text = Ball(Sel).SpeedX
            txtSpeedY.Text = Ball(Sel).SpeedY
            txtSize.Text = Ball(Sel).Size
            txtMass.Text = Ball(Sel).Mass
            txtFlag.Text = Ball(Sel).Flags

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
            Ball(lngFollowBall).Flags = Replace$(Ball(lngFollowBall).Flags, "F", "")

            Sel = UpDown1.Value

            If Ball(Sel).Visible = False Or InStr(1, Ball(Sel).Flags, "P") > 0 Or InStr(1, Ball(Sel).Flags, "R") > 0 Then
                Do Until Ball(Sel).Visible And InStr(1, Ball(Sel).Flags, "P") = 0 And InStr(1, Ball(Sel).Flags, "R") = 0 Or Sel = UpDown1.Maximum
                    Sel = Sel + 1




                Loop

            End If

            UpDown1.Value = Sel


            lngFollowBall = Sel
            Ball(lngFollowBall).Flags = Ball(lngFollowBall).Flags + "F"
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
        If txtFPS.Text = "" Then
            intTargetFPS = 10
        Else
            If CInt(txtFPS.Text) < 10 Then

                intTargetFPS = 10
            ElseIf CInt(txtFPS.Text) > 400 Then
                intTargetFPS = 400

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
        Debug.Print(pic_scale)


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
        Debug.Print(RelBallPosMod.ToString)
    End Sub

    Private Sub Render_Move(sender As Object, e As EventArgs) Handles Render.Move

    End Sub

    Private Sub Form1_RightToLeftChanged(sender As Object, e As EventArgs) Handles Me.RightToLeftChanged

    End Sub

    Private Sub Form1_Scroll(sender As Object, e As ScrollEventArgs) Handles Me.Scroll

    End Sub

    Private Sub PhysicsWorker_DoWork(sender As Object, events As DoWorkEventArgs) Handles PhysicsWorker.DoWork
        Dim VeKY As Double
        Dim VekX As Double
        Dim LenG As Double
        Dim V1x As Double
        Dim V2x As Double
        Dim M1 As Double
        Dim M2 As Double
        Dim V1y As Double
        Dim V2y As Double
        Dim E As Double
        Dim F As Double
        Dim Abstand As Double
        Dim rc As Double
        Dim rx As Double
        Dim ry As Double
        Dim d As Double
        Dim V1 As Double
        Dim V2 As Double
        Dim U2 As Double
        Dim U1 As Double
        Dim ClsSpeedX As Double
        Dim ClsSpeedy As Double
        Dim ClsSpeed As Double
        'Dim ClsForce As Double
        Dim NewBallSize As Double
        Dim NewBallMass As Double
        Dim Divisor As Double
        Dim PrevSize As Double
        Dim PrevMass As Double
        Dim ForceX As Double
        Dim ForceY As Double
        Dim TotMass As Double
        Dim LoccX As Double
        Dim LoccY As Double
        Dim Veck As Double
        Dim VeckSqr As Double
        Dim Force As Double
        Dim BUB As Long
        Dim StartTick, EndTick, ElapTick As Long


        Dim Dist As Double
        Dim DistSqrt As Double

        'Dim B As Long
        'Dim options As New ParallelOptions
        'Dim Tasker As TaskScheduler
        'options.MaxDegreeOfParallelism = 2
        'options.TaskScheduler = Tasker
        Dim Its As Long
        Its = 1
        ' On Error Resume Next
        ' Dim i As Integer
        ' i = Loop1I
restart:
        Do Until bolStopWorker
            Do While bolStopLoop
                Thread.Sleep(100)
            Loop
            'wait(intDelay)
            StartTick = Now.Ticks
            Thread.Sleep(intDelay)

            If UBound(Ball) > 1 Then
                BUB = UBound(Ball)
                'Parallel.For(1, BUB + 1, options, Sub(A)
                'SyncLock LoopLockObject
                For A = 1 To BUB
                    If Ball(A).Visible Then
                        If Ball(A).MovinG = False Then
                            For B = 1 To BUB
                                If Ball(B).Visible Then
                                    ' If GetDistance(New SPoint(Ball(A).LocX, Ball(A).LocY), New SPoint(Ball(B).LocX, Ball(B).LocY)) < CircleOInfluence Then

                                    If bolShawdow Then
                                        If InStr(1, Ball(A).Flags, "S") Then
                                            Dim m As Double, SlX As Double, SlY As Double
                                            SlX = Ball(B).LocX - Ball(A).LocX
                                            SlY = Ball(B).LocY - Ball(A).LocY
                                            m = SlY / SlX
                                            Ball(B).ShadAngle = Math.Atan2(Ball(B).LocY - Ball(A).LocY, Ball(B).LocX - Ball(A).LocX)   'Math.Tan(SlX / SlY) 'Math.Atan2(SlY, SlX) * 180 / PI 'Math.Atan(SlX / SlY) * 180 / PI
                                        End If
                                    End If
                                    If Ball(B).LocX = Ball(A).LocX And Ball(B).LocY = Ball(A).LocY And B <> A Then
                                        Ball(B).LocX = Ball(B).LocX + Ball(B).Size
                                        Ball(B).LocY = Ball(B).LocY + Ball(B).Size
                                    End If
                                    '// Collision Reaction (Vektors)
                                    If bGrav = 0 Then
                                    Else
                                        If Not Ball(A).Locked Then
                                            If B <> A Then
                                                M1 = Ball(A).Mass ^ 2 ' * 2
                                                M2 = Ball(B).Mass ^ 2 ' * 2
                                                TotMass = M1 * M2
                                                Dim DistX As Double = Ball(B).LocX - Ball(A).LocX
                                                Dim DistY As Double = Ball(B).LocY - Ball(A).LocY

                                                Dist = (DistX * DistX) + (DistY * DistY)
                                                DistSqrt = Sqrt(Dist)
                                                ' Veck = (LoccX * LoccX) + (LoccY * LoccY)
                                                ' VeckSqr = Sqrt(Veck)
                                                Force = TotMass / (Dist * DistSqrt)

                                                '(Veck * VeckSqr)
                                                'If Double.IsNaN(Force) Then
                                                '    Debug.Print(Force)
                                                '    Stop
                                                'End If
                                                ForceX = Force * DistX ' / Dist
                                                ForceY = Force * DistY ' / Dist
                                                Ball(A).SpeedX = Ball(A).SpeedX + ForceX / M1
                                                Ball(A).SpeedY = Ball(A).SpeedY + ForceY / M1
                                                'If InStr(1, Ball(A).Flags, "P") Then
                                                '    Debug.Print("P!!!!")
                                                '    ' Stop
                                                'End If
                                                If Force > (Ball(A).Mass ^ 3) And Ball(B).Mass > Ball(A).Mass * 5 And Ball(A).Size > 1 And Ball(A).Visible = True Then  ' And Not InStr(1, Bll(A).Flags, "B")
                                                    FractureBall(A)
                                                End If
                                            End If
                                            'End If ' **
                                        End If
                                    End If
                                    If A <> B And Ball(A).Visible = True Then
                                        'rc = (Ball(A).Size / 4) + (Ball(B).Size / 4)
                                        'ry = (Ball(A).LocY - Ball(B).LocY) / 2
                                        'rx = (Ball(A).LocX - Ball(B).LocX) / 2
                                        'd = Sqrt(rx * rx + ry * ry)



                                        'Dim Multi As Double
                                        'Multi = 0.7
                                        ' If DistSqrt / 2 < (Ball(A).Size + Ball(B).Size) Then 'Collide
                                        '  Dim BlahX As Double
                                        '    Dim BlahY As Double
                                        '  Dim ClsForce2 As Double
                                        '  BlahX = (Ball(A).LocX + (Ball(A).LocX + Ball(A).SpeedX) / 2)
                                        '         BlahY = (Ball(A).LocY + (Ball(A).LocY + Ball(A).SpeedY) / 2)
                                        If DistSqrt < (Ball(A).Size / 2) + (Ball(B).Size / 2) Then
                                            'Perlenkettenproblem liegt hier:
                                            V1x = Ball(A).SpeedX
                                            V1y = Ball(A).SpeedY
                                            V2x = Ball(B).SpeedX
                                            V2y = Ball(B).SpeedY
                                            ClsSpeedX = V1x - V2x
                                            ClsSpeedy = V1y - V2y
                                            ClsSpeed = Abs(ClsSpeedX) + Abs(ClsSpeedy)
                                            ' Debug.Print(ClsSpeed)
                                            M1 = Ball(A).Mass ' / 1000 ' * 4 '^ 2
                                            M2 = Ball(B).Mass ' / 1000 ' * 4 '^ 2
                                            ' ClsForce = ClsSpeed * (M1 + M2)
                                            '  ClsForce2 = ClsSpeed * M2
                                            VekX = (Ball(A).LocX - Ball(B).LocX) / 2
                                            VeKY = (Ball(A).LocY - Ball(B).LocY) / 2
                                            LenG = Sqrt(VeKY * VeKY + VekX * VekX)
                                            VekX = VekX / LenG
                                            VeKY = VeKY / LenG
                                            V1 = VekX * V1x + VeKY * V1y
                                            V2 = VekX * V2x + VeKY * V2y
                                            If V1 - V2 < 0 Then
                                                U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
                                                U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)
                                                'Debug.Print(ClsForce)
                                                '' If 1 = 1 Then ' ClsForce > (Ball(A).Mass / 4 + Ball(B).Mass / 4)
                                                'If Ball(B).Mass < Ball(A).Mass And ClsForce2 > (Ball(A).Mass) And Ball(A).Size > 1 And Ball(B).Size > 1 Then 'And InStr(1, Ball(A).Flags, "R") = 0 And InStr(1, Ball(A).Flags, "S") = 0
                                                '    ' Debug.Print(Ball(B).Mass)
                                                '    '  FractureBall(A)
                                                '    '   FractureBall(B)
                                                '    '  GoTo here
                                                'Else
                                                If InStr(1, Ball(B).Flags, "R") > 0 And Force < (Ball(A).Mass ^ 3) Or InStr(1, Ball(B).Flags, "R") = 0 Then
                                                    Dim Area1 As Double, Area2 As Double

                                                    If Ball(A).Mass > Ball(B).Mass Then
                                                        If Ball(B).Origin <> A Then
                                                            Ball(A).Flags = Replace$(Ball(A).Flags, "R", "")
                                                            Ball(A).SpeedX = Ball(A).SpeedX + (U1 - V1) * VekX
                                                            Ball(A).SpeedY = Ball(A).SpeedY + (U1 - V1) * VeKY
                                                            Area1 = PI * (Ball(A).Size ^ 2)
                                                            Area2 = PI * (Ball(B).Size ^ 2)
                                                            Area1 = Area1 + Area2
                                                            Ball(A).Size = Sqrt(Area1 / PI)
                                                            Ball(A).Mass = Ball(A).Mass + Ball(B).Mass 'Sqr(Ball(B).Mass)
                                                            Ball(B).Visible = False
                                                        End If
                                                    Else 'If Ball(A).Mass < Ball(B).Mass Then
                                                        If Ball(A).Origin <> B Then
                                                            Ball(A).Flags = Replace$(Ball(A).Flags, "R", "")
                                                            Ball(B).SpeedX = Ball(B).SpeedX + (U2 - V2) * VekX
                                                            Ball(B).SpeedY = Ball(B).SpeedY + (U2 - V2) * VeKY
                                                            Area1 = PI * (Ball(B).Size ^ 2)
                                                            Area2 = PI * (Ball(A).Size ^ 2)
                                                            Area1 = Area1 + Area2
                                                            Ball(B).Size = Sqrt(Area1 / PI)
                                                            Ball(B).Mass = Ball(B).Mass + Ball(A).Mass 'Sqr(Ball(A).Mass)
                                                            Ball(A).Visible = False
                                                        End If
                                                        'ElseIf Ball(A).Mass = Ball(B).Mass Then
                                                        '    If Ball(A).Origin <> B Then
                                                        '        Ball(A).Flags = Replace$(Ball(A).Flags, "R", "")
                                                        '        Ball(B).SpeedX = Ball(B).SpeedX + (U2 - V2) * VekX + Ball(A).SpeedX + (U1 - V1) * VekX
                                                        '        Ball(B).SpeedY = Ball(B).SpeedY + (U2 - V2) * VeKY + Ball(A).SpeedY + (U1 - V1) * VeKY
                                                        '        Ball(B).LocX = (Ball(B).LocX + Ball(A).LocX) / 2
                                                        '        Ball(B).LocY = (Ball(B).LocY + Ball(A).LocY) / 2
                                                        '        Area1 = PI * (Ball(B).Size ^ 2)
                                                        '        Area2 = PI * (Ball(A).Size ^ 2)
                                                        '        Area1 = Area1 + Area2
                                                        '        Ball(B).Size = Sqrt(Area1 / PI)
                                                        '        Ball(B).Mass = Ball(B).Mass + Ball(A).Mass 'Sqr(Ball(A).Mass)
                                                        '        Ball(A).Visible = False

                                                        ' End If







                                                    End If


                                                End If
                                                If Ball(A).Mass > 350 Then Ball(A).Color = System.Drawing.Color.Red
                                                If Ball(A).Mass > 400 Then Ball(A).Color = System.Drawing.Color.Yellow
                                                If Ball(A).Mass > 500 Then Ball(A).Color = System.Drawing.Color.White
                                                If Ball(A).Mass > 600 Then Ball(A).Color = System.Drawing.Color.LightCyan
                                                If Ball(A).Mass > 700 Then Ball(A).Color = System.Drawing.Color.LightBlue
                                                If Ball(A).Mass > 1000 Then
                                                    Ball(A).Color = Color.Black
                                                    Ball(A).Size = 20
                                                    If InStr(1, Ball(A).Flags, "BH") = 0 Then Ball(A).Flags = Ball(A).Flags + "BH"
                                                End If
                                                'If Ball(A).Mass > 1000 Then
                                                '    Ball(A).Color = System.Drawing.Color.Black
                                                '    Ball(A).Size = 15
                                                '    If InStr(1, Ball(A).Flags, "BH") = 0 Then Ball(A).Flags = Ball(A).Flags + "BH"
                                                'End If
                                                bolBallsRemoved = True
                                                'End If
                                            End If
                                        End If
                                            'End If
                                            '  If Ball(A).Mass > 350 And Ball(A).Visible = True Then 'solar wind
                                            'If InStr(Ball(A).Flags, "S") = 0 Then Ball(A).Flags = Ball(A).Flags + "S"
                                            'rc = (Ball(B).Size / 4) + (Ball(A).Size / 4)
                                            'ry = (Ball(B).LocY - Ball(A).LocY) / 2
                                            'rx = (Ball(B).LocX - Ball(A).LocX) / 2
                                            'd = Sqrt(rx * rx + ry * ry)
                                            'If d < 500 Then
                                            '    Dim m As Double, SlX As Double, SlY As Double
                                            '    Dim VecX As Double, VecY As Double
                                            '    Dim C As Double, S As Double
                                            '    Dim Dis As Double, DisSqr As Double, F As Double, Lx As Double, Ly As Double
                                            '    SlX = Ball(B).LocX - Ball(A).LocX
                                            '    SlY = Ball(B).LocY - Ball(A).LocY
                                            '    m = SlY / SlX
                                            '    a = Math.Atan2(Ball(B).LocY - Ball(A).LocY, Ball(B).LocX - Ball(A).LocX)
                                            '    C = Cos(a)
                                            '    S = Sin(a)
                                            '    VecX = (Ball(B).LocX + Ball(B).Size * C) - Ball(B).LocX
                                            '    VecY = (Ball(B).LocY + Ball(B).Size * S) - Ball(B).LocY
                                            '    Lx = Ball(B).LocX - Ball(A).LocX
                                            '    Ly = Ball(B).LocY - Ball(A).LocY
                                            '    Dis = (Lx * Lx) + (Ly * Ly)
                                            '    DisSqr = Sqrt(Dis)
                                            '    F = (Ball(A).Mass ^ 2) / (Dis * DisSqr)
                                            '    Ball(B).SpeedX = Ball(B).SpeedX + F * VecX
                                            '    Ball(B).SpeedY = Ball(B).SpeedY + F * VecY
                                            'End If
                                            '  End If
                                        End If
                                    ' End If
                                End If
                            Next B
                        End If
                    End If
                    Ball(A).LocX = Ball(A).LocX + (Ball(A).SpeedX * StepMulti)
                    Ball(A).LocY = Ball(A).LocY + (Ball(A).SpeedY * StepMulti)
                    'End SyncLock
                    'End Sub)
                Next A
            End If

            If UBound(Ball) > 5000 And bolBallsRemoved Then
                's = New Threading.Thread(AddressOf Me.ShrinkBallArray)
                's.Start()
                's.Join()
                ShrinkBallArray()
            End If
            'If bolFollow Then

            '    RelBallPosMod.X = -Ball(lngFollowBall).LocX
            '    RelBallPosMod.Y = -Ball(lngFollowBall).LocY

            'End If
            'If s.ThreadState <> Threading.ThreadState.Running And Me.chkDraw.Checked Then
            '    Me.Render.Image = Drawr()
            'End If
            'FPS = FPS + 1
            PhysicsWorker.ReportProgress(1, Ball)
            EndTick = Now.Ticks
            ElapTick = EndTick - StartTick
            FPS = 10000000 / ElapTick
            If FPS > intTargetFPS Then
                intDelay = intDelay + 1
            Else
                If intDelay > 0 Then
                    intDelay = intDelay - 1
                Else
                    intDelay = 0
                End If
            End If

        Loop
    End Sub
    Private Sub PhysicsWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles PhysicsWorker.ProgressChanged
        ' Debug.Print("Render complete " & Now.Ticks)

        Dim PassBall() As BallParms = e.UserState
        If bolDraw Then Me.Render.Image = Drawr(PassBall)

    End Sub
    Private Sub tmrRender_Tick(sender As Object, e As EventArgs) Handles tmrRender.Tick
        ' If bolDraw Then Me.Render.Image = Drawr()


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


End Class
