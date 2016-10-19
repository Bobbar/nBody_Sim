﻿


Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

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
    Public Sub DrawLoop()

        On Error Resume Next

        Do Until bolStopLoop
            'If Not bolStop Then Matrix.RunWorkerAsync()

            If chkTrails.Checked Then
                For i = 1 To UBound(Ball)

                    If Ball(i).Visible And Ball(i).LocX > 0 And Ball(i).LocX < Render.Width And Ball(i).LocY > 0 And Ball(i).LocY < Render.Height Then
                        'g = g + 1
                        ' If g > 4 Then g = 0


                        ' e.Graphics.FillEllipse(Brushes.LightBlue, ball_loc_x(i) - 1, ball_loc_y(i) - 1, BallSize(i) + 2, BallSize(i) + 2)
                        ' e.Graphics.FillEllipse(Brushes.Blue, ball_loc_x(i), ball_loc_y(i), BallSize(i), BallSize(i))

                        Dim myBrush As New SolidBrush(Ball(i).Color)
                        Render.CreateGraphics.SmoothingMode = SmoothingMode.AntiAlias
                        ' Render.CreateGraphics.FillEllipse(Brushes.Black, Ball(i).LocX - Ball(i).Size / 2 - 1, Ball(i).LocY - Ball(i).Size / 2 - 1, Ball(i).Size + 2, Ball(i).Size + 2)
                        Render.CreateGraphics.FillEllipse(myBrush, Ball(i).LocX - Ball(i).Size / 2, Ball(i).LocY - Ball(i).Size / 2, Ball(i).Size, Ball(i).Size)

                    End If

                Next

            End If

            'If bolBallsRemoved Then ShrinkBallArray()
            If Not chkTrails.Checked Then
                If chkDraw.Checked Then
                    Render.Refresh()
                Else
                    'Render.e
                End If
            End If
            'Application.DoEvents()
        Loop
    End Sub




    Private Sub MainLoop2()
        Dim VeKY As Single
        Dim VekX As Single

        Dim LenG As Single

        Dim V1x As Single
        Dim V2x As Single
        Dim M1 As Single
        Dim M2 As Single


        Dim V1y As Single
        Dim V2y As Single

        Dim a As Single
        Dim b As Single
        Dim Abstand As Single
        Dim rc As Single
        Dim rx As Single
        Dim ry As Single
        Dim d As Single
        Dim V1 As Single
        Dim V2 As Single
        Dim U2 As Single
        Dim U1 As Single
        Dim ClsSpeedX As Single
        Dim ClsSpeedy As Single
        Dim ClsSpeed As Single
        Dim ClsForce As Single
        Dim NewBallSize As Single
        Dim NewBallMass As Single
        Dim Divisor As Single
        Dim PrevSize As Single
        Dim PrevMass As Single
        Dim ForceX As Single
        Dim ForceY As Single
        Dim TotMass As Single
        Dim LoccX As Single
        Dim LoccY As Single
        Dim Veck As Single
        Dim VeckSqr As Single
        Dim Force As Double
        Dim Its As Long

        Dim X As Long
        Its = 1
        Dim i As Long

        ' i = Loop2I

        ' On Error Resume Next
restart:
        wait(intDelay)




        Do Until bolStopLoop
            SyncLock LoopLockObject
                If UBound(Ball) > 1 Then




                    'If PubIndex + 1 <= UBound(Ball) - 1 Then
                    '    PubIndex = PubIndex + 1
                    'ElseIf PubIndex + 1 > UBound(Ball) - 1 Then
                    '    GoTo finished
                    'ElseIf PubIndex = UBound(Ball) Then
                    '    i = PubIndex
                    'Else
                    '    GoTo finished
                    'End If




                    If PubIndex + 1 < UBound(Ball) Then

                        If Ball(PubIndex + 1).Visible = False Then

                            If PubIndex + 1 < UBound(Ball) Then
                                PubIndex = PubIndex + 1
                                Do Until PubIndex + 1 >= UBound(Ball)
                                    PubIndex = PubIndex + 1
                                    If Ball(PubIndex).Visible = True Then Exit Do

                                Loop
                            ElseIf PubIndex + 1 = UBound(Ball) Then
                                PubIndex = PubIndex + 1
                            End If


                        Else
                            PubIndex = PubIndex + 1
                        End If



                    Else
                        PubIndex = 1
                        GoTo finished
                    End If
                    i = PubIndex


                    Thread2Done = False
                    bolStart2 = False

                    ' wait(1)







                    '   'If bolBallsRemoved Then
                    '    ShrinkBallArray()
                    '    bolBallsRemoved = False
                    'End If




                    '  For i = Int((UBound(Ball) / 2) + 1) To UBound(Ball)
                    ' For i = 1 To UBound(Ball)





                    'If Loop1I = i And i + 1 < UBound(Ball) Then
                    '    i = i + 1
                    '    Loop2I = i
                    'ElseIf Loop1I = i And i + 1 > UBound(Ball) Then
                    '    GoTo finished
                    'ElseIf Loop1I > i And Loop1I + 1 < UBound(Ball) Then
                    '    i = Loop1I + 1
                    '    Loop2I = i

                    'End If

                    '  Debug.Print("Loop2: " & Loop2I)

                    If Ball(i).Visible Then
                        If Ball(i).MovinG = False Then






                            '// Moving Routines

                            'Ball(i).SpeedY = Ball(i).SpeedY + Gravity
                            'Ball(i).SpeedY = Ball(i).SpeedY + Ball(i).GravY
                            'Ball(i).SpeedX = Ball(i).SpeedX + Ball(i).GravX

                            ' Ball(i).SpeedY = Ball(i).SpeedY * friction
                            ' Ball(i).SpeedX = Ball(i).SpeedX * friction
                            'Ball(i).PrevSpeedX = Ball(i).SpeedX
                            'Ball(i).PrevSpeedY = Ball(i).SpeedY

                            'Ball(i).LocX = Ball(i).LocX + (Ball(i).SpeedX * StepMulti)
                            'Ball(i).LocY = Ball(i).LocY + (Ball(i).SpeedY * StepMulti)

                            '// Collision with other Balls:



                            For X = 1 To UBound(Ball)
                                If Ball(X).Visible Then


                                    'If InStr(1, Ball(i).Flags, "S") Then
                                    '    Dim m As Single, SlX As Single, SlY As Single
                                    '    SlX = Ball(X).LocX - Ball(i).LocX
                                    '    SlY = Ball(X).LocY - Ball(i).LocY

                                    '    m = SlY / SlX

                                    '    Ball(X).ShadAngle = Math.Atan2(Ball(X).LocY - Ball(i).LocY, Ball(X).LocX - Ball(i).LocX)   'Math.Tan(SlX / SlY) 'Math.Atan2(SlY, SlX) * 180 / PI 'Math.Atan(SlX / SlY) * 180 / PI

                                    '    'Debug.Print(Ball(X).ShadAngle)


                                    '    '

                                    'End If




                                    If Ball(X).LocX = Ball(i).LocX And Ball(X).LocY = Ball(i).LocY And X <> i Then
                                        Ball(X).LocX = Ball(X).LocX + Ball(X).Size
                                        Ball(X).LocY = Ball(X).LocY + Ball(X).Size
                                    End If

                                    a = (Ball(i).LocX - (Ball(X).LocX)) / 2  '+ ball(x).size) ' / 2)
                                    b = (Ball(i).LocY - (Ball(X).LocY)) / 2 '+ ball(x).size) '/ 2)

                                    If a = 0 Or b = 0 Then

                                        If b = 0 Then Abstand = Abs(a)
                                        If a = 0 Then Abstand = Abs(b)

                                    Else
                                        Abstand = Sqrt((Abs(a) ^ 2) + (Abs(b) ^ 2))
                                    End If




                                    '// Collision Reaction (Vektors)

                                    If bGrav = 0 Then






                                        If Abs(Abstand) < ((Ball(i).Size / 2) + (Ball(X).Size / 2)) And Abstand <> 0 Then

                                            V1x = Ball(i).SpeedX
                                            V1y = Ball(i).SpeedY
                                            V2x = Ball(X).SpeedX
                                            V2y = Ball(X).SpeedY
                                            M1 = Ball(i).Mass / 4 ' * 4 ' ^ 2
                                            M2 = Ball(X).Mass / 4 ' * 4 ' ^ 2

                                            VeKY = (Ball(i).LocY - Ball(X).LocY) / 2
                                            VekX = (Ball(i).LocX - Ball(X).LocX) / 2

                                            LenG = Sqrt(VeKY * VeKY + VekX * VekX)
                                            VekX = VekX / LenG
                                            VeKY = VeKY / LenG

                                            V1 = VekX * V1x + VeKY * V1y
                                            V2 = VekX * V2x + VeKY * V2y

                                            If V1 - V2 < 0 Then

                                                U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
                                                U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)

                                                'If Ball(i).Flags <> "B" Then
                                                Ball(i).SpeedX = Ball(i).SpeedX + (U1 - V1) * VekX
                                                Ball(i).SpeedY = Ball(i).SpeedY + (U1 - V1) * VeKY
                                                'ElseIf Ball(X).Flags <> "B" Then
                                                Ball(X).SpeedX = Ball(X).SpeedX + (U2 - V2) * VekX
                                                Ball(X).SpeedY = Ball(X).SpeedY + (U2 - V2) * VeKY
                                                'End If

                                            End If
                                        End If
                                    Else

                                        If Not Ball(i).Locked Then


                                            If X <> i Then
                                                M1 = Ball(i).Mass ^ 2 ' * 2
                                                M2 = Ball(X).Mass ^ 2 ' * 2
                                                TotMass = M1 * M2
                                                LoccX = Ball(X).LocX - Ball(i).LocX
                                                LoccY = Ball(X).LocY - Ball(i).LocY
                                                Veck = (LoccX * LoccX) + (LoccY * LoccY)
                                                VeckSqr = Sqrt(Veck)
                                                Force = TotMass / (Veck * VeckSqr)

                                                'If Double.IsNaN(Force) Then
                                                '    Debug.Print(Force)
                                                '    Stop

                                                'End If


                                                ForceX = Force * LoccX
                                                ForceY = Force * LoccY

                                                Ball(i).SpeedX = Ball(i).SpeedX + ForceX / M1
                                                Ball(i).SpeedY = Ball(i).SpeedY + ForceY / M1
                                                'If InStr(1, Ball(i).Flags, "P") Then
                                                '    Debug.Print("P!!!!")
                                                '    ' Stop
                                                'End If

                                                If Force > (Ball(i).Mass ^ 3) And Ball(X).Mass > Ball(i).Mass * 5 And Ball(i).Size > 1 And InStr(1, Ball(i).Flags, "P") = 0 And UBound(Ball) < 5100 And InStr(1, Ball(i).Flags, "P") < 3 Then  ' And Not InStr(1, Bll(i).Flags, "B")

                                                    Dim TotBMass As Double
                                                    Dim Area As Double
                                                    Dim RadUPX As Double, RadDNX As Double, RadUPY As Double, RadDNY As Double

                                                    Divisor = Int(Ball(i).Size)
                                                    If Divisor <= 1 Then Divisor = 2

                                                    PrevSize = Ball(i).Size
                                                    PrevMass = Ball(i).Mass

                                                    'PrevSize = Sqr(PrevSize / pi)
                                                    Area = PI * (Ball(i).Size ^ 2)
                                                    Area = Area / Divisor

                                                    NewBallSize = fnRadius(Area)  'fnRadius(fnArea(Ball(i).Size) / 2)  'Sqr(Area / pi) 'ball(i).Size / Divisor
                                                    NewBallMass = PrevMass / Divisor  '(Ball(i).Mass / Divisor)

                                                    Ball(i).Visible = False
                                                    '
                                                    '                                            Ball(i).Size = NewBallSize
                                                    '                                            Ball(i).Mass = NewBallMass
                                                    '                                            Ball(i).Flags = "B"
                                                    RadUPX = Ball(i).LocX + PrevSize
                                                    RadDNX = Ball(i).LocX - PrevSize
                                                    RadUPY = Ball(i).LocY + PrevSize
                                                    RadDNY = Ball(i).LocY - PrevSize


                                                    Dim u As Long

                                                    For h = 1 To Divisor


                                                        ReDim Preserve Ball(UBound(Ball) + 1)
                                                        u = UBound(Ball)

                                                        Ball(u).Size = NewBallSize '(2 * Rnd()) + 0.2
                                                        Ball(u).Mass = NewBallMass
                                                        TotBMass = TotBMass + NewBallMass

                                                        Ball(u).SpeedX = Ball(i).SpeedX
                                                        Ball(u).SpeedY = Ball(i).SpeedY

                                                        'If Not InStr(1, Ball(i).Flags, "R") Then Ball(u).Flags = Ball(i).Flags + "R"
                                                        Ball(u).Flags = Ball(i).Flags + "R"
                                                        Ball(u).Color = Ball(i).Color 'vbWhite
                                                        Ball(u).Visible = True


                                                        '  Ball(u).LocY = Ball(i).LocY + Ball(u).Size * 2



                                                        Ball(u).LocX = GetRandomNumber((RadDNX), RadUPX)

                                                        Ball(u).LocY = GetRandomNumber((RadDNY), RadUPY)

                                                    Next h


                                                End If




                                            End If

                                            'End If ' **

                                        End If

                                    End If
                                    If X <> i And Ball(i).Visible = True Then


                                        rc = (Ball(i).Size / 4) + (Ball(X).Size / 4)
                                        ry = (Ball(i).LocY - Ball(X).LocY) / 2
                                        rx = (Ball(i).LocX - Ball(X).LocX) / 2


                                        d = Sqrt(rx * rx + ry * ry)

                                        Dim Multi As Single
                                        Multi = 0.7
                                        If d < (Ball(i).Size + Ball(X).Size) Then
                                            Dim BlahX As Double
                                            Dim BlahY As Double

                                            BlahX = (Ball(i).LocX + (Ball(i).LocX + Ball(i).SpeedX) / 2)
                                            BlahY = (Ball(i).LocY + (Ball(i).LocY + Ball(i).SpeedY) / 2)



                                            If d < rc Then
                                                'Perlenkettenproblem liegt hier:

                                                V1x = Ball(i).SpeedX
                                                V1y = Ball(i).SpeedY
                                                V2x = Ball(X).SpeedX
                                                V2y = Ball(X).SpeedY

                                                ClsSpeedX = V1x - V2x
                                                ClsSpeedy = V1y - V2y
                                                ClsSpeed = Abs(ClsSpeedX) + Abs(ClsSpeedy)

                                                M1 = Ball(i).Mass / 2 ' * 4 '^ 2
                                                M2 = Ball(X).Mass / 2 ' * 4 '^ 2
                                                ClsForce = ClsSpeed * (M1 + M2)
                                                VekX = (Ball(i).LocX - Ball(X).LocX) / 2
                                                VeKY = (Ball(i).LocY - Ball(X).LocY) / 2

                                                LenG = Sqrt(VeKY * VeKY + VekX * VekX)

                                                VekX = VekX / LenG
                                                VeKY = VeKY / LenG

                                                V1 = VekX * V1x + VeKY * V1y
                                                V2 = VekX * V2x + VeKY * V2y

                                                If V1 - V2 < 0 Then

                                                    U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
                                                    U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)
                                                    'Debug.Print ClsForce
                                                    If 1 = 1 Then ' Force < (Ball(i).Mass ^ 2)
                                                        Dim Area1 As Double, Area2 As Double



                                                        If Ball(i).Mass > Ball(X).Mass Then

                                                            If Ball(X).Origin <> i And InStr(1, Ball(X).Flags, "P") = 0 Then

                                                                Ball(i).Flags = Replace$(Ball(i).Flags, "R", "")
                                                                Ball(i).SpeedX = Ball(i).SpeedX + (U1 - V1) * VekX
                                                                Ball(i).SpeedY = Ball(i).SpeedY + (U1 - V1) * VeKY
                                                                ' If Not InStr(1, Ball(i).Flags, "BH") Then


                                                                Area1 = PI * (Ball(i).Size ^ 2)
                                                                Area2 = PI * (Ball(X).Size ^ 2)
                                                                'AreaDiff = Area1 - Area2
                                                                Area1 = Area1 + Area2
                                                                Ball(i).Size = Sqrt(Area1 / PI)

                                                                'Ball(i).Size = Ball(i).Size + (Ball(X).Size / 8)

                                                                ' End If

                                                                Ball(i).Mass = Ball(i).Mass + Ball(X).Mass 'Sqr(Ball(X).Mass)
                                                            End If

                                                            Ball(X).Visible = False



                                                        Else
                                                            If Ball(i).Origin <> X And InStr(1, Ball(i).Flags, "P") = 0 Then
                                                                Ball(i).Flags = Replace$(Ball(i).Flags, "R", "")
                                                                Ball(X).SpeedX = Ball(X).SpeedX + (U2 - V2) * VekX
                                                                Ball(X).SpeedY = Ball(X).SpeedY + (U2 - V2) * VeKY
                                                                'If Not InStr(1, Ball(i).Flags, "BH") Then

                                                                Area1 = PI * (Ball(X).Size ^ 2)
                                                                Area2 = PI * (Ball(i).Size ^ 2)

                                                                Area1 = Area1 + Area2
                                                                Ball(X).Size = Sqrt(Area1 / PI)

                                                                'Ball(X).Size = Ball(X).Size + (Ball(i).Size / 8)

                                                                'End If

                                                                Ball(X).Mass = Ball(X).Mass + Ball(i).Mass 'Sqr(Ball(i).Mass)
                                                            End If
                                                            Ball(i).Visible = False


                                                        End If
                                                        If Ball(i).Mass > 350 Then Ball(i).Color = System.Drawing.Color.Red



                                                        If Ball(i).Mass > 400 Then Ball(i).Color = System.Drawing.Color.Yellow
                                                        If Ball(i).Mass > 500 Then Ball(i).Color = System.Drawing.Color.White
                                                        If Ball(i).Mass > 600 Then Ball(i).Color = System.Drawing.Color.LightCyan
                                                        If Ball(i).Mass > 700 Then Ball(i).Color = System.Drawing.Color.LightBlue
                                                        'If Ball(i).Mass > 1000 Then
                                                        '    Ball(i).Color = System.Drawing.Color.Black
                                                        '    Ball(i).Size = 15
                                                        '    If InStr(1, Ball(i).Flags, "BH") = 0 Then Ball(i).Flags = Ball(i).Flags + "BH"
                                                        'End If

                                                        bolBallsRemoved = True

                                                    Else
                                                        'If Ball(i).Flags <> "B" Then
                                                        Ball(i).SpeedX = Ball(i).SpeedX + (U1 - V1) * VekX ' * 0.7
                                                        Ball(i).SpeedY = Ball(i).SpeedY + (U1 - V1) * VeKY ' * 0.7

                                                        Ball(X).SpeedX = Ball(X).SpeedX + (U2 - V2) * VekX ' * 0.7
                                                        Ball(X).SpeedY = Ball(X).SpeedY + (U2 - V2) * VeKY ' * 0.7
                                                        ' End If

                                                    End If

                                                End If

                                            End If
                                        End If




                                        If Ball(i).Mass > 350 And Ball(i).Visible = True Then 'solar wind
                                            If InStr(Ball(i).Flags, "S") = 0 Then Ball(i).Flags = Ball(i).Flags + "S"
                                            rc = (Ball(X).Size / 4) + (Ball(i).Size / 4)
                                            ry = (Ball(X).LocY - Ball(i).LocY) / 2
                                            rx = (Ball(X).LocX - Ball(i).LocX) / 2
                                            d = Sqrt(rx * rx + ry * ry)

                                            If d < 500 Then


                                                Dim m As Single, SlX As Single, SlY As Single
                                                Dim VecX As Double, VecY As Double
                                                Dim C As Double, S As Double
                                                Dim Dis As Single, DisSqr As Single, F As Single, Lx As Single, Ly As Single


                                                SlX = Ball(X).LocX - Ball(i).LocX
                                                SlY = Ball(X).LocY - Ball(i).LocY

                                                m = SlY / SlX

                                                a = Math.Atan2(Ball(X).LocY - Ball(i).LocY, Ball(X).LocX - Ball(i).LocX)

                                                C = Cos(a)
                                                S = Sin(a)

                                                VecX = (Ball(X).LocX + Ball(X).Size * C) - Ball(X).LocX
                                                VecY = (Ball(X).LocY + Ball(X).Size * S) - Ball(X).LocY


                                                Lx = Ball(X).LocX - Ball(i).LocX
                                                Ly = Ball(X).LocY - Ball(i).LocY
                                                Dis = (Lx * Lx) + (Ly * Ly)
                                                DisSqr = Sqrt(Dis)
                                                F = (Ball(i).Mass * 100) / (Dis * DisSqr)

                                                Ball(X).SpeedX = Ball(X).SpeedX + F * VecX
                                                Ball(X).SpeedY = Ball(X).SpeedY + F * VecY

                                            End If


                                        End If




                                    End If


                                End If








                            Next X

                            '// Collision with Walls

                            If bGrav = 0 Then
                                right_side = Render.Width - Ball(i).Size
                                bottom_side = Render.Height - Ball(i).Size

                                If Ball(i).LocY > bottom_side Then
                                    Ball(i).LocY = bottom_side

                                    Ball(i).SpeedY = Ball(i).SpeedY * -1


                                ElseIf Ball(i).LocY < Ball(i).Size Then


                                    Ball(i).LocY = Ball(i).Size
                                    Ball(i).SpeedY = Ball(i).SpeedY * -1


                                End If

                                If Ball(i).LocX > right_side Then
                                    Ball(i).LocX = right_side
                                    Ball(i).SpeedX = Ball(i).SpeedX * -1
                                ElseIf Ball(i).LocX < Ball(i).Size Then
                                    Ball(i).LocX = Ball(i).Size
                                    Ball(i).SpeedX = Ball(i).SpeedX * -1
                                End If
                            End If

                        End If

                    End If

                    'Next i




                    'Ball(i).LocX = Ball(i).LocX + (Ball(i).SpeedX * StepMulti)
                    'Ball(i).LocY = Ball(i).LocY + (Ball(i).SpeedY * StepMulti)





                End If
                ' Debug.Print("Thread2: " & i)
                '  If i = Int((UBound(Ball) / 2) + 1) Then GoTo finished
            End SyncLock
        Loop

finished:


        ' StartMeasuring()
        Thread2Done = True
        bolStart2 = False

        Do Until bolStart2

            If Thread2Done And Thread3Done Then

                'If bolFollow Then
                '    Dim DiffX As Double, DiffY As Double

                '    If Ball(lngFollowBall).LocX <> FollowX Or Ball(lngFollowBall).LocY <> FollowY Then

                '        DiffX = Ball(lngFollowBall).LocX - FollowX
                '        DiffY = Ball(lngFollowBall).LocY - FollowY

                '        For i = 1 To UBound(Ball)

                '            Ball(i).LocX = Ball(i).LocX - DiffX
                '            Ball(i).LocY = Ball(i).LocY - DiffY

                '        Next

                '        FollowX = Ball(lngFollowBall).LocX
                '        FollowY = Ball(lngFollowBall).LocY

                '    End If

                'End If

                If UBound(Ball) > 5000 And bolBallsRemoved Then
                    s = New Threading.Thread(AddressOf Me.ShrinkBallArray)
                    s.Start()
                    s.Join()


                End If




                FPS = FPS + 1


                PubIndex = 0





                bolStart2 = True : bolStart3 = True

            End If


        Loop
        ' wait(intDelay)
        ' StopMeasuring()



        For i = 1 To UBound(Ball) / 2

            Ball(i).LocX = Ball(i).LocX + (Ball(i).SpeedX * StepMulti)
            Ball(i).LocY = Ball(i).LocY + (Ball(i).SpeedY * StepMulti)

        Next i


        GoTo restart



    End Sub
    Private Sub MainLoop3()
        Dim VeKY As Single
        Dim VekX As Single

        Dim LenG As Single

        Dim V1x As Single
        Dim V2x As Single
        Dim M1 As Single
        Dim M2 As Single


        Dim V1y As Single
        Dim V2y As Single

        Dim a As Single
        Dim b As Single
        Dim Abstand As Single
        Dim rc As Single
        Dim rx As Single
        Dim ry As Single
        Dim d As Single
        Dim V1 As Single
        Dim V2 As Single
        Dim U2 As Single
        Dim U1 As Single
        Dim ClsSpeedX As Single
        Dim ClsSpeedy As Single
        Dim ClsSpeed As Single
        Dim ClsForce As Single
        Dim NewBallSize As Single
        Dim NewBallMass As Single
        Dim Divisor As Single
        Dim PrevSize As Single
        Dim PrevMass As Single
        Dim ForceX As Single
        Dim ForceY As Single
        Dim TotMass As Single
        Dim LoccX As Single
        Dim LoccY As Single
        Dim Veck As Single
        Dim VeckSqr As Single
        Dim Force As Double

        Dim X As Long


        Dim Its As Long
        Its = 1
        ' On Error Resume Next
        Dim i As Integer

        ' i = Loop1I
restart:


        wait(intDelay)

        Do Until bolStopLoop
            SyncLock LoopLockObject
                If UBound(Ball) > 1 Then


                    'If PubIndex + 1 <= UBound(Ball) - 1 Then
                    '    PubIndex = PubIndex + 1
                    'ElseIf PubIndex + 1 > UBound(Ball) - 1 Then
                    '    GoTo finished
                    'ElseIf PubIndex = UBound(Ball) Then
                    '    i = PubIndex
                    'Else
                    '    GoTo finished
                    'End If
                    'i = PubIndex




                    If PubIndex + 1 <= UBound(Ball) Then ' find next available index

                        If Ball(PubIndex + 1).Visible = False Then

                            If PubIndex + 1 < UBound(Ball) Then
                                PubIndex = PubIndex + 1
                                Do Until PubIndex + 1 >= UBound(Ball)
                                    PubIndex = PubIndex + 1
                                    If Ball(PubIndex).Visible = True Then Exit Do

                                Loop
                            ElseIf PubIndex + 1 = UBound(Ball) Then
                                PubIndex = PubIndex + 1
                            End If


                        Else
                            PubIndex = PubIndex + 1
                        End If



                    Else


                        PubIndex = 1
                        GoTo finished
                    End If

                    i = PubIndex



                    Thread3Done = False
                    bolStart3 = False

                    ' For i = 1 To Int(UBound(Ball) / 2)



                    If Ball(i).Visible Then
                        If Ball(i).MovinG = False Then






                            '// Moving Routines

                            'Ball(i).SpeedY = Ball(i).SpeedY + Gravity
                            'Ball(i).SpeedY = Ball(i).SpeedY + Ball(i).GravY
                            'Ball(i).SpeedX = Ball(i).SpeedX + Ball(i).GravX

                            ' Ball(i).SpeedY = Ball(i).SpeedY * friction
                            ' Ball(i).SpeedX = Ball(i).SpeedX * friction
                            'Ball(i).PrevSpeedX = Ball(i).SpeedX
                            'Ball(i).PrevSpeedY = Ball(i).SpeedY

                            'Ball(i).LocX = Ball(i).LocX + (Ball(i).SpeedX * StepMulti)
                            'Ball(i).LocY = Ball(i).LocY + (Ball(i).SpeedY * StepMulti)

                            '// Collision with other Balls:



                            For X = 1 To UBound(Ball)
                                If Ball(X).Visible Then


                                    'If InStr(1, Ball(i).Flags, "S") Then
                                    '    Dim m As Single, SlX As Single, SlY As Single
                                    '    SlX = Ball(X).LocX - Ball(i).LocX
                                    '    SlY = Ball(X).LocY - Ball(i).LocY

                                    '    m = SlY / SlX

                                    '    Ball(X).ShadAngle = Math.Atan2(Ball(X).LocY - Ball(i).LocY, Ball(X).LocX - Ball(i).LocX)   'Math.Tan(SlX / SlY) 'Math.Atan2(SlY, SlX) * 180 / PI 'Math.Atan(SlX / SlY) * 180 / PI

                                    '    'Debug.Print(Ball(X).ShadAngle)


                                    '    '

                                    'End If




                                    If Ball(X).LocX = Ball(i).LocX And Ball(X).LocY = Ball(i).LocY And X <> i Then
                                        Ball(X).LocX = Ball(X).LocX + Ball(X).Size
                                        Ball(X).LocY = Ball(X).LocY + Ball(X).Size
                                    End If

                                    a = (Ball(i).LocX - (Ball(X).LocX)) / 2  '+ ball(x).size) ' / 2)
                                    b = (Ball(i).LocY - (Ball(X).LocY)) / 2 '+ ball(x).size) '/ 2)

                                    If a = 0 Or b = 0 Then

                                        If b = 0 Then Abstand = Abs(a)
                                        If a = 0 Then Abstand = Abs(b)

                                    Else
                                        Abstand = Sqrt((Abs(a) ^ 2) + (Abs(b) ^ 2))
                                    End If




                                    '// Collision Reaction (Vektors)

                                    If bGrav = 0 Then






                                        If Abs(Abstand) < ((Ball(i).Size / 2) + (Ball(X).Size / 2)) And Abstand <> 0 Then

                                            V1x = Ball(i).SpeedX
                                            V1y = Ball(i).SpeedY
                                            V2x = Ball(X).SpeedX
                                            V2y = Ball(X).SpeedY
                                            M1 = Ball(i).Mass / 4 ' * 4 ' ^ 2
                                            M2 = Ball(X).Mass / 4 ' * 4 ' ^ 2

                                            VeKY = (Ball(i).LocY - Ball(X).LocY) / 2
                                            VekX = (Ball(i).LocX - Ball(X).LocX) / 2

                                            LenG = Sqrt(VeKY * VeKY + VekX * VekX)
                                            VekX = VekX / LenG
                                            VeKY = VeKY / LenG

                                            V1 = VekX * V1x + VeKY * V1y
                                            V2 = VekX * V2x + VeKY * V2y

                                            If V1 - V2 < 0 Then

                                                U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
                                                U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)

                                                'If Ball(i).Flags <> "B" Then
                                                Ball(i).SpeedX = Ball(i).SpeedX + (U1 - V1) * VekX
                                                Ball(i).SpeedY = Ball(i).SpeedY + (U1 - V1) * VeKY
                                                'ElseIf Ball(X).Flags <> "B" Then
                                                Ball(X).SpeedX = Ball(X).SpeedX + (U2 - V2) * VekX
                                                Ball(X).SpeedY = Ball(X).SpeedY + (U2 - V2) * VeKY
                                                'End If

                                            End If
                                        End If
                                    Else

                                        If Not Ball(i).Locked Then


                                            If X <> i Then
                                                M1 = Ball(i).Mass ^ 2 ' * 2
                                                M2 = Ball(X).Mass ^ 2 ' * 2
                                                TotMass = M1 * M2
                                                LoccX = Ball(X).LocX - Ball(i).LocX

                                                LoccY = Ball(X).LocY - Ball(i).LocY
                                                Veck = (LoccX * LoccX) + (LoccY * LoccY)
                                                VeckSqr = Sqrt(Veck)
                                                Force = TotMass / (Veck * VeckSqr)

                                                'If Double.IsNaN(Force) Then
                                                '    Debug.Print(Force)
                                                '    Stop
                                                'End If
                                                ForceX = Force * LoccX
                                                ForceY = Force * LoccY
                                                Ball(i).SpeedX = Ball(i).SpeedX + ForceX / M1
                                                Ball(i).SpeedY = Ball(i).SpeedY + ForceY / M1
                                                'If InStr(1, Ball(i).Flags, "P") Then
                                                '    Debug.Print("P!!!!")
                                                '    ' Stop
                                                'End If

                                                If Force > (Ball(i).Mass ^ 3) And Ball(X).Mass > Ball(i).Mass * 5 And Ball(i).Size > 1 And InStr(1, Ball(i).Flags, "P") = 0 And UBound(Ball) < 5100 Then  ' And Not InStr(1, Bll(i).Flags, "B")

                                                    Dim TotBMass As Double
                                                    Dim Area As Double
                                                    Dim RadUPX As Double, RadDNX As Double, RadUPY As Double, RadDNY As Double

                                                    Divisor = Int(Ball(i).Size)
                                                    If Divisor <= 1 Then Divisor = 2

                                                    PrevSize = Ball(i).Size
                                                    PrevMass = Ball(i).Mass

                                                    'PrevSize = Sqr(PrevSize / pi)
                                                    Area = PI * (Ball(i).Size ^ 2)
                                                    Area = Area / Divisor

                                                    NewBallSize = fnRadius(Area)  'fnRadius(fnArea(Ball(i).Size) / 2)  'Sqr(Area / pi) 'ball(i).Size / Divisor
                                                    NewBallMass = PrevMass / Divisor  '(Ball(i).Mass / Divisor)

                                                    Ball(i).Visible = False
                                                    '
                                                    '                                            Ball(i).Size = NewBallSize
                                                    '                                            Ball(i).Mass = NewBallMass
                                                    '                                            Ball(i).Flags = "B"
                                                    RadUPX = Ball(i).LocX + PrevSize
                                                    RadDNX = Ball(i).LocX - PrevSize
                                                    RadUPY = Ball(i).LocY + PrevSize
                                                    RadDNY = Ball(i).LocY - PrevSize


                                                    Dim u As Long

                                                    For h = 1 To Divisor


                                                        ReDim Preserve Ball(UBound(Ball) + 1)
                                                        u = UBound(Ball)

                                                        Ball(u).Size = NewBallSize '(2 * Rnd()) + 0.2
                                                        Ball(u).Mass = NewBallMass
                                                        TotBMass = TotBMass + NewBallMass

                                                        Ball(u).SpeedX = Ball(i).SpeedX
                                                        Ball(u).SpeedY = Ball(i).SpeedY

                                                        'If Not InStr(1, Ball(i).Flags, "R") Then Ball(u).Flags = Ball(i).Flags + "R"
                                                        Ball(u).Flags = Ball(i).Flags + "R"
                                                        Ball(u).Color = Ball(i).Color 'vbWhite
                                                        Ball(u).Visible = True


                                                        '  Ball(u).LocY = Ball(i).LocY + Ball(u).Size * 2



                                                        Ball(u).LocX = GetRandomNumber((RadDNX), RadUPX)

                                                        Ball(u).LocY = GetRandomNumber((RadDNY), RadUPY)

                                                    Next h

                                                End If




                                            End If

                                            'End If ' **

                                        End If

                                    End If
                                    If X <> i And Ball(i).Visible = True Then


                                        rc = (Ball(i).Size / 4) + (Ball(X).Size / 4)
                                        ry = (Ball(i).LocY - Ball(X).LocY) / 2
                                        rx = (Ball(i).LocX - Ball(X).LocX) / 2


                                        d = Sqrt(rx * rx + ry * ry)

                                        Dim Multi As Single
                                        Multi = 0.7
                                        If d < (Ball(i).Size + Ball(X).Size) Then
                                            Dim BlahX As Double
                                            Dim BlahY As Double

                                            BlahX = (Ball(i).LocX + (Ball(i).LocX + Ball(i).SpeedX) / 2)
                                            BlahY = (Ball(i).LocY + (Ball(i).LocY + Ball(i).SpeedY) / 2)



                                            If d < rc Then
                                                'Perlenkettenproblem liegt hier:

                                                V1x = Ball(i).SpeedX
                                                V1y = Ball(i).SpeedY
                                                V2x = Ball(X).SpeedX
                                                V2y = Ball(X).SpeedY

                                                ClsSpeedX = V1x - V2x
                                                ClsSpeedy = V1y - V2y
                                                ClsSpeed = Abs(ClsSpeedX) + Abs(ClsSpeedy)

                                                M1 = Ball(i).Mass ' * 4 '^ 2
                                                M2 = Ball(X).Mass ' * 4 '^ 2
                                                ClsForce = ClsSpeed * (M1 + M2)
                                                VekX = (Ball(i).LocX - Ball(X).LocX) / 2
                                                VeKY = (Ball(i).LocY - Ball(X).LocY) / 2

                                                LenG = Sqrt(VeKY * VeKY + VekX * VekX)

                                                VekX = VekX / LenG
                                                VeKY = VeKY / LenG

                                                V1 = VekX * V1x + VeKY * V1y
                                                V2 = VekX * V2x + VeKY * V2y

                                                If V1 - V2 < 0 Then

                                                    U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
                                                    U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)
                                                    'Debug.Print ClsForce
                                                    If 1 = 1 Then ' Force < (Ball(i).Mass ^ 2)
                                                        Dim Area1 As Double, Area2 As Double

                                                        'If InStr(1, Ball(X).Flags, "R") > 0 And Force < (Ball(i).Mass ^ 2) Then

                                                        If Ball(i).Mass > Ball(X).Mass Then



                                                            If Ball(X).Origin <> i And InStr(1, Ball(X).Flags, "P") = 0 Then

                                                                Ball(i).Flags = Replace$(Ball(i).Flags, "R", "")
                                                                Ball(i).SpeedX = Ball(i).SpeedX + (U1 - V1) * VekX
                                                                Ball(i).SpeedY = Ball(i).SpeedY + (U1 - V1) * VeKY
                                                                ' If Not InStr(1, Ball(i).Flags, "BH") Then


                                                                Area1 = PI * (Ball(i).Size ^ 2)
                                                                Area2 = PI * (Ball(X).Size ^ 2)
                                                                'AreaDiff = Area1 - Area2
                                                                Area1 = Area1 + Area2
                                                                Ball(i).Size = Sqrt(Area1 / PI)

                                                                'Ball(i).Size = Ball(i).Size + (Ball(X).Size / 8)

                                                                ' End If

                                                                Ball(i).Mass = Ball(i).Mass + Ball(X).Mass 'Sqr(Ball(X).Mass)
                                                            End If

                                                            Ball(X).Visible = False



                                                        Else
                                                            If Ball(i).Origin <> X And InStr(1, Ball(i).Flags, "P") = 0 Then
                                                                Ball(i).Flags = Replace$(Ball(i).Flags, "R", "")
                                                                Ball(X).SpeedX = Ball(X).SpeedX + (U2 - V2) * VekX
                                                                Ball(X).SpeedY = Ball(X).SpeedY + (U2 - V2) * VeKY
                                                                'If Not InStr(1, Ball(i).Flags, "BH") Then

                                                                Area1 = PI * (Ball(X).Size ^ 2)
                                                                Area2 = PI * (Ball(i).Size ^ 2)

                                                                Area1 = Area1 + Area2
                                                                Ball(X).Size = Sqrt(Area1 / PI)

                                                                'Ball(X).Size = Ball(X).Size + (Ball(i).Size / 8)

                                                                'End If

                                                                Ball(X).Mass = Ball(X).Mass + Ball(i).Mass 'Sqr(Ball(i).Mass)
                                                            End If
                                                            Ball(i).Visible = False


                                                        End If
                                                        If Ball(i).Mass > 350 Then Ball(i).Color = System.Drawing.Color.Red



                                                        If Ball(i).Mass > 400 Then Ball(i).Color = System.Drawing.Color.Yellow
                                                        If Ball(i).Mass > 500 Then Ball(i).Color = System.Drawing.Color.White
                                                        If Ball(i).Mass > 600 Then Ball(i).Color = System.Drawing.Color.LightCyan
                                                        If Ball(i).Mass > 700 Then Ball(i).Color = System.Drawing.Color.LightBlue
                                                        'If Ball(i).Mass > 1000 Then
                                                        '    Ball(i).Color = System.Drawing.Color.Black
                                                        '    Ball(i).Size = 15
                                                        '    If InStr(1, Ball(i).Flags, "BH") = 0 Then Ball(i).Flags = Ball(i).Flags + "BH"
                                                        'End If

                                                        bolBallsRemoved = True

                                                    Else
                                                        'If Ball(i).Flags <> "B" Then
                                                        Ball(i).SpeedX = Ball(i).SpeedX + (U1 - V1) * VekX ' * 0.7
                                                        Ball(i).SpeedY = Ball(i).SpeedY + (U1 - V1) * VeKY ' * 0.7

                                                        Ball(X).SpeedX = Ball(X).SpeedX + (U2 - V2) * VekX ' * 0.7
                                                        Ball(X).SpeedY = Ball(X).SpeedY + (U2 - V2) * VeKY ' * 0.7
                                                        ' End If

                                                    End If

                                                End If

                                            End If
                                        End If




                                        If Ball(i).Mass > 350 And Ball(i).Visible = True Then 'solar wind
                                            If InStr(Ball(i).Flags, "S") = 0 Then Ball(i).Flags = Ball(i).Flags + "S"
                                            rc = (Ball(X).Size / 4) + (Ball(i).Size / 4)
                                            ry = (Ball(X).LocY - Ball(i).LocY) / 2
                                            rx = (Ball(X).LocX - Ball(i).LocX) / 2
                                            d = Sqrt(rx * rx + ry * ry)

                                            If d < 500 Then


                                                Dim m As Single, SlX As Single, SlY As Single
                                                Dim VecX As Double, VecY As Double
                                                Dim C As Double, S As Double
                                                Dim Dis As Single, DisSqr As Single, F As Single, Lx As Single, Ly As Single


                                                SlX = Ball(X).LocX - Ball(i).LocX
                                                SlY = Ball(X).LocY - Ball(i).LocY

                                                m = SlY / SlX

                                                a = Math.Atan2(Ball(X).LocY - Ball(i).LocY, Ball(X).LocX - Ball(i).LocX)

                                                C = Cos(a)
                                                S = Sin(a)

                                                VecX = (Ball(X).LocX + Ball(X).Size * C) - Ball(X).LocX
                                                VecY = (Ball(X).LocY + Ball(X).Size * S) - Ball(X).LocY


                                                Lx = Ball(X).LocX - Ball(i).LocX
                                                Ly = Ball(X).LocY - Ball(i).LocY
                                                Dis = (Lx * Lx) + (Ly * Ly)
                                                DisSqr = Sqrt(Dis)
                                                F = (Ball(i).Mass * 100) / (Dis * DisSqr)


                                                Ball(X).SpeedX = Ball(X).SpeedX + F * VecX
                                                Ball(X).SpeedY = Ball(X).SpeedY + F * VecY

                                            End If


                                        End If




                                    End If


                                End If








                            Next X

                            '// Collision with Walls

                            If bGrav = 0 Then
                                right_side = Render.Width - Ball(i).Size
                                bottom_side = Render.Height - Ball(i).Size

                                If Ball(i).LocY > bottom_side Then
                                    Ball(i).LocY = bottom_side

                                    Ball(i).SpeedY = Ball(i).SpeedY * -1


                                ElseIf Ball(i).LocY < Ball(i).Size Then

                                    Ball(i).LocY = Ball(i).Size
                                    Ball(i).SpeedY = Ball(i).SpeedY * -1


                                End If

                                If Ball(i).LocX > right_side Then
                                    Ball(i).LocX = right_side
                                    Ball(i).SpeedX = Ball(i).SpeedX * -1
                                ElseIf Ball(i).LocX < Ball(i).Size Then
                                    Ball(i).LocX = Ball(i).Size
                                    Ball(i).SpeedX = Ball(i).SpeedX * -1
                                End If
                            End If

                        End If

                    End If

                    'Next i



                End If

                '  Debug.Print("Thread3: " & i)

                ' If i = Int(UBound(Ball) / 2) Then GoTo finished
            End SyncLock
        Loop

finished:

        '



        Thread3Done = True
        bolStart3 = False

        Do Until bolStart3

            ' If Thread2Done And Thread3Done Then


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








            'End If

        Loop


        For i = Int((UBound(Ball) / 2) + 1) To UBound(Ball)

            Ball(i).LocX = Ball(i).LocX + (Ball(i).SpeedX * StepMulti)
            Ball(i).LocY = Ball(i).LocY + (Ball(i).SpeedY * StepMulti)

        Next i

        GoTo restart


    End Sub


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

    Private Sub Matrix_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        FPS = FPS + 1




        If bolFollow Then
            Dim DiffX As Double, DiffY As Double

            If Ball(lngFollowBall).LocX <> FollowX Or Ball(lngFollowBall).LocY <> FollowY Then

                DiffX = Ball(lngFollowBall).LocX - FollowX
                DiffY = Ball(lngFollowBall).LocY - FollowY

                For i = 1 To UBound(Ball)

                    Ball(i).LocX = Ball(i).LocX - DiffX
                    Ball(i).LocY = Ball(i).LocY - DiffY

                Next

                FollowX = Ball(lngFollowBall).LocX
                FollowY = Ball(lngFollowBall).LocY

            End If

        End If

        'If bolBallsRemoved Then ShrinkBallArray()

        ' Render.Refresh()



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
            Ball(UBound(Ball)).Flags = Ball(UBound(Ball)).Flags + "BH"
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


            For i = 1 To UBound(Ball)
                Ball(i).Old_LocX = Ball(i).LocX
                Ball(i).Old_LocY = Ball(i).LocY

            Next
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
                            Ball(Sel).Flags = "BH"
                        End If

                        If bolAltDown Then

                            lngFollowBall = Sel
                            Ball(Sel).Flags = Ball(Sel).Flags + "F"

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





            For i = 0 To UBound(Ball)
                Ball(i).Old_LocX = Ball(i).LocX
                Ball(i).Old_LocY = Ball(i).LocY

            Next
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
        Label10.Text = "FPS: " & FPS * 8


        lblBalls.Text = "Balls: " & UBound(Ball)

        UpDown1.Maximum = UBound(Ball)
        TrueFPS = FPS * 8
        If FPS * 8 > intTargetFPS Then
            intDelay = intDelay + 1
        Else
            If intDelay > 0 Then
                intDelay = intDelay - 1
            Else
                intDelay = 0
            End If

        End If
        lblDelay.Text = "Delay: " & intDelay
        FPS = 0


        ScreenCenterX = Me.Render.Width / 2
        ScreenCenterY = Me.Render.Height / 2



        ScaleOffset.X = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).X
        ScaleOffset.Y = ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).Y


        RenderWindowDims.X = CInt(Me.Render.Width)
        RenderWindowDims.Y = CInt(Me.Render.Height)

        Application.DoEvents()
        'condense array by removing invisible balls
    End Sub

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
        Me.BackColor = colBackColor
        For Each ctl As Control In Me.Controls
            If Not TypeOf ctl Is Label Then
                ctl.ForeColor = colControlForeColor
                ctl.BackColor = colBackColor
            End If
        Next
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
        tmrRender.Enabled = True
        PhysicsWorker.RunWorkerAsync()
        'MainLoop()




    End Sub
    Public Sub MasterLoop()
        'On Error Resume Next
        'Dim OddEven As Integer

        ' OddEven = 0


        Do Until bolStopLoop '

            If Thread2Done And Thread3Done Then




                For i = 1 To UBound(Ball)







                    Ball(i).LocX = Ball(i).LocX + (Ball(i).SpeedX * StepMulti)
                    Ball(i).LocY = Ball(i).LocY + (Ball(i).SpeedY * StepMulti)






                Next i



                If bolFollow Then
                    Dim DiffX As Double, DiffY As Double

                    If Ball(lngFollowBall).LocX <> FollowX Or Ball(lngFollowBall).LocY <> FollowY Then

                        DiffX = Ball(lngFollowBall).LocX - FollowX
                        DiffY = Ball(lngFollowBall).LocY - FollowY

                        For i = 1 To UBound(Ball)

                            Ball(i).LocX = Ball(i).LocX - DiffX
                            Ball(i).LocY = Ball(i).LocY - DiffY

                        Next

                        FollowX = Ball(lngFollowBall).LocX
                        FollowY = Ball(lngFollowBall).LocY

                    End If

                End If

                FPS = FPS + 1

                'If UBound(Ball) > 2000 Then
                '    s = New Threading.Thread(AddressOf Me.ShrinkBallArray)
                '    s.Start()
                '    s.Join()


                'End If

                ' If bolBallsRemoved = True Then ShrinkBallArray()


                PubIndex = 1
                wait(intDelay)
                'bolStart1 = True
                bolStart2 = True : bolStart3 = True




            End If



            'tryagain:

            '                If t.ThreadState <> Threading.ThreadState.Running Then

            '                    Loop1I = i
            '                    t = New Threading.Thread(AddressOf Me.MainLoop)
            '                    t.Start()
            '                ElseIf t2.ThreadState <> Threading.ThreadState.Running Then

            '                    Loop2I = i
            '                    t2 = New Threading.Thread(AddressOf Me.MainLoop)
            '                    t2.Start()

            '                Else
            '                    GoTo tryagain


            '                End If






            'OddEven = OddEven + 1
            'End If


        Loop










        ' wait(1)

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
            MainLoop()


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
        Const Balls As Long = 50
        Dim i As Long

        For i = 0 To Balls


            ReDim Preserve Ball(UBound(Ball) + 1)

            Ball(UBound(Ball)).Color = RandomRGBColor() 'colDefBodyColor
            Ball(UBound(Ball)).Visible = True

            Ball(UBound(Ball)).LocX = GetRandomNumber(1, Render.Width) - ScaleOffset.X - RelBallPosMod.X
            'Ball(UBound(Ball)).LocX = CInt(Int((Render.ScaleWidth * Rnd()) + 1))  '((Render2.ScaleWidth) / 2) ' - 10
            'Randomize(Timer)
            'Ball(UBound(Ball)).LocY = CInt(Int((Render.ScaleHeight * Rnd()) + 1)) '((Render2.ScaleHeight) / 2) '+ Ball(UBound(Ball) - 1).LocY + Ball(UBound(Ball) - 1).Size
            Ball(UBound(Ball)).LocY = GetRandomNumber(1, Render.Height) - ScaleOffset.Y - RelBallPosMod.Y

            Ball(UBound(Ball)).SpeedX = 0
            Ball(UBound(Ball)).SpeedY = 0
            Ball(UBound(Ball)).Flags = ""
            Ball(UBound(Ball)).Size = GetRandomNumber(1, 5)
            Ball(UBound(Ball)).Mass = fnMass(Ball(UBound(Ball)).Size) ' * 2




        Next
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
        bolStopLoop = True
        wait(300)


        Array.Clear(Ball, 0, Ball.Count)

        ReDim Ball(0)
        bolFollow = False


        ' End If


        bolStopLoop = False
        MainLoop()

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


        Me.Render.Image.Save("C:\Imagesss.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

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

            For b = 1 To UBound(Ball)

                Ball(b).LocX = Ball(b).LocX - DiffX
                Ball(b).LocY = Ball(b).LocY - DiffY

            Next b
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

            For b = 1 To UBound(Ball)

                Ball(b).LocX = Ball(b).LocX - DiffX
                Ball(b).LocY = Ball(b).LocY - DiffY

            Next b
            FollowX = Ball(lngFollowBall).LocX
            FollowY = Ball(lngFollowBall).LocY
            bolFollow = True
        End If





    End Sub

    Private Sub chkShadow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShadow.CheckedChanged

    End Sub

    Private Sub chkTrails_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTrails.CheckedChanged

    End Sub

    Private Sub txtFPS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFPS.TextChanged
        If txtFPS.Text = "" Then
            intTargetFPS = 10
        End If

        If CInt(txtFPS.Text) < 10 Then

            intTargetFPS = 10
            Else
                intTargetFPS = txtFPS.Text
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



        Debug.Print("Scale Offset: " + ScaleMousePosExact(New SPoint(ScreenCenterX, ScreenCenterY)).ToString)


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

        'Dim B As Long

        Dim options As New ParallelOptions
        Dim Tasker As TaskScheduler
        options.MaxDegreeOfParallelism = 2
        options.TaskScheduler = Tasker
        Dim Its As Long
        Its = 1
        ' On Error Resume Next
        ' Dim i As Integer

        ' i = Loop1I
restart:





        Do Until 1 = 2 'bolStopLoop

            Do While bolStopLoop
                Thread.Sleep(100)

            Loop
            wait(intDelay)

            If UBound(Ball) > 1 Then
                BUB = UBound(Ball)



                'Parallel.For(1, BUB + 1, options, Sub(A)


                'SyncLock LoopLockObject
                For A = 1 To BUB
                    If Ball(A).Visible Then
                        If Ball(A).MovinG = False Then



                            For B = 1 To BUB
                                If Ball(B).Visible Then

                                    If Me.chkShadow.Checked Then
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
                                                LoccX = Ball(B).LocX - Ball(A).LocX

                                                LoccY = Ball(B).LocY - Ball(A).LocY
                                                Veck = (LoccX * LoccX) + (LoccY * LoccY)
                                                VeckSqr = Sqrt(Veck)
                                                Force = TotMass / (Veck * VeckSqr)

                                                'If Double.IsNaN(Force) Then
                                                '    Debug.Print(Force)
                                                '    Stop
                                                'End If
                                                ForceX = Force * LoccX
                                                ForceY = Force * LoccY
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


                                        rc = (Ball(A).Size / 4) + (Ball(B).Size / 4)
                                        ry = (Ball(A).LocY - Ball(B).LocY) / 2
                                        rx = (Ball(A).LocX - Ball(B).LocX) / 2


                                        d = Sqrt(rx * rx + ry * ry)

                                        Dim Multi As Double
                                        Multi = 0.7
                                        If d < (Ball(A).Size + Ball(B).Size) Then 'Collide
                                            '  Dim BlahX As Double
                                            '    Dim BlahY As Double
                                            Dim ClsForce2 As Double

                                            '  BlahX = (Ball(A).LocX + (Ball(A).LocX + Ball(A).SpeedX) / 2)
                                            '         BlahY = (Ball(A).LocY + (Ball(A).LocY + Ball(A).SpeedY) / 2)

                                            If d < rc Then
                                                'Perlenkettenproblem liegt hier:

                                                V1x = Ball(A).SpeedX
                                                V1y = Ball(A).SpeedY
                                                V2x = Ball(B).SpeedX
                                                V2y = Ball(B).SpeedY

                                                ClsSpeedX = V1x - V2x
                                                ClsSpeedy = V1y - V2y
                                                ClsSpeed = Abs(ClsSpeedX) + Abs(ClsSpeedy)
                                                ' Debug.Print(ClsSpeed)
                                                M1 = Ball(A).Mass / 1000 ' * 4 '^ 2
                                                M2 = Ball(B).Mass / 1000 ' * 4 '^ 2
                                                ' ClsForce = ClsSpeed * (M1 + M2)

                                                ClsForce2 = ClsSpeed * M2

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
                                                    ' If 1 = 1 Then ' ClsForce > (Ball(A).Mass / 4 + Ball(B).Mass / 4)


                                                    If Ball(B).Mass < Ball(A).Mass And ClsForce2 > (Ball(A).Mass) And Ball(A).Size > 1 And Ball(B).Size > 1 Then 'And InStr(1, Ball(A).Flags, "R") = 0 And InStr(1, Ball(A).Flags, "S") = 0
                                                        ' Debug.Print(Ball(B).Mass)
                                                        FractureBall(A)
                                                        FractureBall(B)
                                                        '  GoTo here

                                                    Else


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


                                                            Else
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



                                                            End If

                                                        End If

                                                        If Ball(A).Mass > 350 Then Ball(A).Color = System.Drawing.Color.Red



                                                        If Ball(A).Mass > 400 Then Ball(A).Color = System.Drawing.Color.Yellow
                                                        If Ball(A).Mass > 500 Then Ball(A).Color = System.Drawing.Color.White
                                                        If Ball(A).Mass > 600 Then Ball(A).Color = System.Drawing.Color.LightCyan
                                                        If Ball(A).Mass > 700 Then Ball(A).Color = System.Drawing.Color.LightBlue
                                                        'If Ball(A).Mass > 1000 Then
                                                        '    Ball(A).Color = System.Drawing.Color.Black
                                                        '    Ball(A).Size = 15
                                                        '    If InStr(1, Ball(A).Flags, "BH") = 0 Then Ball(A).Flags = Ball(A).Flags + "BH"
                                                        'End If

                                                        bolBallsRemoved = True


                                                    End If
                                                End If

                                            End If
                                        End If




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


                                        '   End If




                                    End If


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

            '  Application.DoEvents()



            If UBound(Ball) > 5000 And bolBallsRemoved Then
                s = New Threading.Thread(AddressOf Me.ShrinkBallArray)
                s.Start()
                s.Join()


            End If



            If bolFollow Then


                '  If Ball(lngFollowBall).LocX <> FollowX Or Ball(lngFollowBall).LocY <> FollowY Then


                RelBallPosMod.X = -Ball(lngFollowBall).LocX
                RelBallPosMod.Y = -Ball(lngFollowBall).LocY


                ' End If

            End If





            'If s.ThreadState <> Threading.ThreadState.Running And Me.chkDraw.Checked Then

            '    Me.Render.Image = Drawr()
            'End If





            FPS = FPS + 1





        Loop



    End Sub

    Private Sub tmrRender_Tick(sender As Object, e As EventArgs) Handles tmrRender.Tick
        If chkDraw.Checked Then Me.Render.Image = Drawr()
        ' Debug.Print(TrueFPS)
        'If TrueFPS > 1 Then
        '    Dim SyncInterval As Integer = Int(1000 / TrueFPS)
        '    If SyncInterval > 1 And SyncInterval < 1000 Then
        '        tmrRender.Interval = SyncInterval
        '    Else
        '        If SyncInterval < 1 Then SyncInterval = 1
        '        If SyncInterval > 1000 Then SyncInterval = 1000
        '        tmrRender.Interval = SyncInterval
        '    End If
        'End If

    End Sub

End Class
