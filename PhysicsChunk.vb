Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.IO
Public NotInheritable Class PhysicsChunk
    Sub New(iUpperBody As Integer, iLowerBody As Integer, MainBodyArray() As BallParms)
        uBoundBody = iUpperBody
        lBoundBody = iLowerBody
        Bodys = MainBodyArray



        For i As Integer = lBoundBody To uBoundBody


            MyBodys.Add(Bodys(i))
        Next


    End Sub
    Private uBoundBody, lBoundBody As Integer
    Private Bodys() As BallParms
    Public MyBodys As New List(Of BallParms)
    Dim M1 As Double
    Dim M2 As Double
    'Dim ClsForce As Double
    Dim ForceX As Double
    Dim ForceY As Double
    Dim TotMass As Double
    Dim Force As Double
    Dim BUB As Long
    Dim StartTick, EndTick, ElapTick As Long
    Dim DistX As Double
    Dim DistY As Double
    Dim Dist As Double
    Dim DistSqrt As Double
    Dim bolRocheLimit As Boolean = False
    Dim NewBalls As New List(Of BallParms)

    Public Function CalcPhysics() ' As List(Of BallParms)
        ' Dim tmpBodys As New List(Of BallParms)
        ' Do Until bolStopWorker
        Dim OuterBody As BallParms() = MyBodys.ToArray

        'Do While bolStopLoop
        ' Thread.Sleep(100)
        'Loop
        '  StartTick = Now.Ticks
        '  Thread.Sleep(intDelay)
        '  StartTimer()
        If UBound(OuterBody) > 1 Then
            BUB = uBoundBody
            For A = 0 To UBound(OuterBody) ' Each OuterBody(A) As BallParms In MyBodys 'A = lBoundBody To uBoundBody
                If OuterBody(A).Visible Then
                    If OuterBody(A).MovinG = False Then
                        For B = 0 To UBound(Bodys)
                            If Bodys(B).Visible And OuterBody(A).Index <> Bodys(B).Index Then
                                'If bolShawdow Then
                                '    If InStr(1, OuterBody(A).Flags, "S") Then
                                '        Dim m As Double, SlX As Double, SlY As Double
                                '        SlX = Bodys(B).LocX - OuterBody(A).LocX
                                '        SlY = Bodys(B).LocY - OuterBody(A).LocY
                                '        m = SlY / SlX
                                '        Bodys(B).ShadAngle = Math.Atan2(Bodys(B).LocY - OuterBody(A).LocY, Bodys(B).LocX - OuterBody(A).LocX)   'Math.Tan(SlX / SlY) 'Math.Atan2(SlY, SlX) * 180 / PI 'Math.Atan(SlX / SlY) * 180 / PI
                                '    End If
                                'End If
                                If Bodys(B).LocX = OuterBody(A).LocX And Bodys(B).LocY = OuterBody(A).LocY Then
                                    CollideBodies(OuterBody(A), Bodys(B))
                                End If
                                If bGrav = 0 Then
                                Else

                                    DistX = Bodys(B).LocX - OuterBody(A).LocX
                                    DistY = Bodys(B).LocY - OuterBody(A).LocY
                                    Dist = (DistX * DistX) + (DistY * DistY)
                                    DistSqrt = Sqrt(Dist)
                                    If DistSqrt > 0 Then 'Gravity reaction
                                        If DistSqrt < (OuterBody(A).Size / 2) + (Bodys(B).Size / 2) Then DistSqrt = (OuterBody(A).Size / 2) + (Bodys(B).Size / 2) 'prevent screamers
                                        M1 = OuterBody(A).Mass '^ 2
                                        M2 = Bodys(B).Mass ' ^ 2
                                        TotMass = M1 * M2
                                        Force = TotMass / (Dist * DistSqrt)


                                        ForceX = Force * DistX
                                        ForceY = Force * DistY

                                        OuterBody(A).SpeedX += StepMulti * ForceX / M1
                                        OuterBody(A).SpeedY += StepMulti * ForceY / M1


                                        If DistSqrt < 40 Then
                                            'If Bodys(B).Mass > OuterBody(A).Mass * 5 Then
                                            '    If Force > OuterBody(A).Mass / 2 Then 'And Bodys(B).Mass > OuterBody(A).Mass * 5 Then
                                            '        ' bolRocheLimit = True
                                            '    ElseIf (Force * 1.5) < OuterBody(A).Mass / 2 Then
                                            '        bolRocheLimit = False
                                            '        OuterBody(A).IsFragment = False

                                            '    End If
                                            '    If bolRocheLimit And OuterBody(A).Size > 1 Then
                                            '        '  NewBalls.AddRange(FractureBall(OuterBody(A)))
                                            '    End If
                                            'Else
                                            '    bolRocheLimit = False
                                            'End If
                                            'If OuterBody(A).Index = 20 And Bodys(B).Index = 3 Then
                                            '    Debug.Print(DistSqrt & " - " & OuterBody(A).Size & " - " & Bodys(B).Size)
                                            'End If

                                            If DistSqrt <= (OuterBody(A).Size / 2) + (Bodys(B).Size / 2) Then 'Collision reaction
                                                ' If Not bolRocheLimit Then
                                                If OuterBody(A).Mass > Bodys(B).Mass Then
                                                    '  Debug.Print(Bodys(B).Visible)
                                                    CollideBodies(OuterBody(A), Bodys(B))
                                                    '   Debug.Print(Bodys(B).Visible)

                                                    If IsInMyBodys(Bodys(B).Index) Then
                                                        OuterBody(TrueIndex(B)).Visible = False
                                                    End If
                                                ElseIf OuterBody(A).Mass < Bodys(B).Mass Then
                                                    '  CollideBodies(Bodys(B), OuterBody(A))
                                                    OuterBody(A).Visible = False
                                                Else
                                                    CollideBodies(OuterBody(A), Bodys(B))

                                                    'If OuterBody(A).Index > Bodys(B).Index Then
                                                    '    CollideBodies(OuterBody(A), Bodys(B))
                                                    '    If IsInMyBodys(Bodys(B).Index) Then
                                                    '        OuterBody(TrueIndex(B)).Visible = False
                                                    '    End If
                                                    'ElseIf OuterBody(A).Index < Bodys(B).Index Then
                                                    '    OuterBody(A).Visible = False
                                                    '    'If IsInMyBodys(Bodys(B).Index) Then
                                                    '    '    OuterBody(TrueIndex(B)).Visible = False
                                                    '    'End If
                                                    'End If

                                                End If
                                                '   End If
                                            End If
                                        End If
                                    Else
                                    End If
                                End If
                            End If
                            ' UpdateBody(OuterBody(A))

                        Next B
                    End If
                End If
                OuterBody(A).LocX = OuterBody(A).LocX + (StepMulti * OuterBody(A).SpeedX)
                OuterBody(A).LocY = OuterBody(A).LocY + (StepMulti * OuterBody(A).SpeedY)
                ' tmpBodys.Add(OuterBody(A))

            Next A
        End If
        ''    ShrinkBallArray()
        ''If UBound(Ball) > 10000 Then
        ''End If
        'If NewBalls.Count > 0 Then
        '    ' AddNewBalls(NewBalls)
        '    tmpBodys.AddRange(NewBalls)
        'End If
        '*******  PhysicsWorker.ReportProgress(1, Ball) ******
        'EndTick = Now.Ticks
        'ElapTick = EndTick - StartTick
        'FPS = 10000000 / ElapTick
        'If FPS > intTargetFPS Then
        '    intDelay = intDelay + 1
        'Else
        '    If intDelay > 0 Then
        '        intDelay = intDelay - 1
        '    Else
        '        intDelay = 0
        '    End If
        'End If
        '   StopTimer()
        '  Loop
        ' Return tmpBodys
        MyBodys.Clear()
        MyBodys.AddRange(OuterBody)
        '  MyBodys = OuterBody 'tmpBodys
    End Function
    Private Function IsInMyBodys(index As Integer) As Boolean
        For Each Body As BallParms In MyBodys

            If Body.Index = index Then Return True
        Next
        Return False

    End Function
    Private Function TrueIndex(index As Integer) As Integer
        Dim i As Integer
        For Each Body As BallParms In MyBodys

            If Body.Index = index Then i = MyBodys.IndexOf(Body) 'Return MyBodys.IndexOf(Body)
        Next
        Return i

    End Function

    Private Function FractureBall(Body As BallParms) As List(Of BallParms)
        Dim NewBallSize As Single
        Dim NewBallMass As Single
        Dim Divisor As Single
        Dim PrevSize As Single
        Dim PrevMass As Single
        Dim TotBMass As Double
        Dim Area As Double
        Dim tmpBallList As New List(Of BallParms)

        Dim RadUPX As Double, RadDNX As Double, RadUPY As Double, RadDNY As Double
        ' i = UBound(Ball)
        If Body.Visible = True And Body.Size > 1 Then
            Divisor = Int(Body.Size)
            If Divisor <= 1 Then Divisor = 2
            PrevSize = Body.Size
            PrevMass = Body.Mass
            'PrevSize = Sqr(PrevSize / pi)
            Area = PI * (Body.Size ^ 2)
            Area = Area / Divisor
            NewBallSize = fnRadius(Area)  'fnRadius(fnArea(Body.Size) / 2)  'Sqr(Area / pi) 'Body.Size / Divisor

            NewBallMass = PrevMass / Divisor  '(Body.Mass / Divisor)
            Body.Visible = False
            '
            '                                            Body.Size = NewBallSize
            '                                            Body.Mass = NewBallMass
            '                                            Body.Flags = "B"
            RadUPX = (Body.LocX) + PrevSize / 2 + Body.SpeedX * StepMulti
            RadDNX = (Body.LocX) - PrevSize / 2 + Body.SpeedX * StepMulti
            RadUPY = (Body.LocY) + PrevSize / 2 + Body.SpeedY * StepMulti
            RadDNY = (Body.LocY) - PrevSize / 2 + Body.SpeedY * StepMulti

            'RadUPX = (Body.LocX) + PrevSize + Body.SpeedX * StepMulti
            'RadDNX = (Body.LocX) - PrevSize + Body.SpeedX * StepMulti
            'RadUPY = (Body.LocY) + PrevSize + Body.SpeedY * StepMulti
            'RadDNY = (Body.LocY) - PrevSize + Body.SpeedY * StepMulti


            Dim CenterPoint As New Point(Body.LocX, Body.LocY)
            Dim u As Long
            For h = 1 To Divisor
                Dim tmpBall As BallParms
                ' ReDim Preserve Ball(UBound(Ball) + 1)
                '  u = UBound(Ball)

                tmpBall.Size = NewBallSize '(2 * Rnd()) + 0.2
                tmpBall.Mass = NewBallMass
                TotBMass = TotBMass + NewBallMass
                tmpBall.SpeedX = Body.SpeedX
                tmpBall.SpeedY = Body.SpeedY
                'If Not InStr(1, Body.Flags, "R") Then Ball(u).Flags = Body.Flags + "R"
                'Ball(u).Flags = Body.Flags + "R"
                tmpBall.Color = Body.Color 'vbWhite
                tmpBall.Flags = ""
                tmpBall.IsFragment = True
                tmpBall.Visible = True
                '  Ball(u).LocY = Body.LocY + Ball(u).Size * 2


                tmpBall.LocX = GetRandomNumber((RadDNX), RadUPX)
                tmpBall.LocY = GetRandomNumber((RadDNY), RadUPY)



                If DupLoc(tmpBallList, tmpBall) Then
                    ' Debug.Print("Dup failure")
                    Do Until Not DupLoc(tmpBallList, tmpBall)


                        tmpBall.LocX = GetRandomNumber((RadDNX), RadUPX)
                        tmpBall.LocY = GetRandomNumber((RadDNY), RadUPY)

                    Loop


                End If


                'Dim tmpLoc As New Point(GetRandomNumber((RadDNX), RadUPX), GetRandomNumber((RadDNY), RadUPY))
                'Dim DistX As Double = CenterPoint.X - tmpLoc.X
                'Dim DistY As Double = CenterPoint.Y - tmpLoc.Y
                'Dim Dist As Double = (DistX * DistX) + (DistY * DistY)
                'Dim DistSqrt = Sqrt(Dist)

                'Do Until DistSqrt <= PrevSize * 2 And DistSqrt <> 0
                '    tmpLoc = New Point(GetRandomNumber((RadDNX), RadUPX), GetRandomNumber((RadDNY), RadUPY))
                '    DistX = CenterPoint.X - tmpLoc.X
                '    DistY = CenterPoint.Y - tmpLoc.Y
                '    Dist = (DistX * DistX) + (DistY * DistY)
                '    DistSqrt = Sqrt(Dist)

                '    'If DistSqrt < PrevSize / 2 And DistSqrt <> 0 Then



                '    'End If
                'Loop
                'tmpBall.LocX = tmpLoc.X
                'tmpBall.LocY = tmpLoc.Y
                tmpBallList.Add(tmpBall)


            Next h
        End If
        Return tmpBallList
    End Function
    Private Function DupLoc(LstBodies As List(Of BallParms), Body As BallParms) As Boolean
        If LstBodies.Count < 1 Then Return False
        For Each bdy As BallParms In LstBodies
            If Body.LocX = bdy.LocX And Body.LocY = bdy.LocY Then Return True
        Next

        Return False
    End Function
    Private Sub CollideBodies(ByRef Master As BallParms, ByRef Slave As BallParms)
        Dim VeKY As Double
        Dim VekX As Double
        Dim V1x As Double
        Dim V2x As Double
        Dim M1 As Double
        Dim M2 As Double
        Dim V1y As Double
        Dim V2y As Double

        ' Dim NewVelX1, NewVelY1, NewVelX2, NewVelY2 As Double


        Dim V1 As Double
        Dim V2 As Double
        Dim U2 As Double
        Dim U1 As Double
        Dim DistX As Double
        Dim DistY As Double
        Dim Dist As Double
        Dim DistSqrt As Double
        Dim PrevSpdX, PrevSpdY As Double

        Dim Area1 As Double, Area2 As Double

        DistX = Slave.LocX - Master.LocX
        DistY = Slave.LocY - Master.LocY
        Dist = (DistX * DistX) + (DistY * DistY)
        DistSqrt = Sqrt(Dist)
        ' Debug.Print("Col dist:" & DistSqrt)
        If DistSqrt > 0 Then
            If Not Master.IsFragment Then

                ' If IsInMyBodys(Slave.Index) Then Slave.Visible = False

                '   Master.Visible = False
                ' Master.IsFragment = False
                V1x = Master.SpeedX
                V1y = Master.SpeedY
                V2x = Slave.SpeedX
                V2y = Slave.SpeedY

                M1 = Master.Mass
                M2 = Slave.Mass

                VekX = DistX / 2 ' (Ball(A).LocX - Ball(B).LocX) / 2
                VeKY = DistY / 2 '(Ball(A).LocY - Ball(B).LocY) / 2

                VekX = VekX / (DistSqrt / 2) 'LenG
                VeKY = VeKY / (DistSqrt / 2) 'LenG

                V1 = VekX * V1x + VeKY * V1y
                V2 = VekX * V2x + VeKY * V2y

                U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)

                U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)


                PrevSpdX = Master.SpeedX
                PrevSpdY = Master.SpeedY

                If Master.Mass <> Slave.Mass Then
                    Master.SpeedX = Master.SpeedX + (U1 - V1) * VekX
                    Master.SpeedY = Master.SpeedY + (U1 - V1) * VeKY
                    Slave.Visible = False
                    'If Abs(Master.SpeedX - PrevSpdX) > 100 Or Abs(Master.SpeedY - PrevSpdY) > 100 Then

                    '    Debugger.Break()

                    'End If


                    Area1 = PI * (Master.Size ^ 2)
                    Area2 = PI * (Slave.Size ^ 2)
                    Area1 = Area1 + Area2
                    Master.Size = Sqrt(Area1 / PI)
                    Master.Mass = Master.Mass + Slave.Mass 'Sqr(Ball(B).Mass)






                    'If Sqrt(Master.Mass) * 3 > 350 Then Master.Color = System.Drawing.Color.Red
                    'If Sqrt(Master.Mass) * 3 > 400 Then Master.Color = System.Drawing.Color.Yellow
                    'If Sqrt(Master.Mass) * 3 > 500 Then Master.Color = System.Drawing.Color.White
                    'If Sqrt(Master.Mass) * 3 > 600 Then Master.Color = System.Drawing.Color.LightCyan
                    'If Sqrt(Master.Mass) * 3 > 700 Then Master.Color = System.Drawing.Color.LightBlue
                    'If Sqrt(Master.Mass) * 3 > 1000 Then
                    '    Master.Color = Color.Black
                    '    Master.Size = 20
                    '    If InStr(1, Master.Flags, "BH") = 0 Then Master.Flags = Master.Flags + "BH"
                    'End If


                    If Master.Flags.Contains("BH") Then ' Or Master.Mass >= TypicalSolarMass * 18 Then
                        Master.Color = Color.Black
                        Master.Size = 15
                        If InStr(1, Master.Flags, "BH") = 0 Then Master.Flags = Master.Flags + "BH"
                    End If


                    'If Master.Mass >= TypicalSolarMass * 0.3 Then Master.Color = System.Drawing.Color.Red
                    'If Master.Mass >= TypicalSolarMass * 0.8 Then Master.Color = System.Drawing.Color.Gold
                    'If Master.Mass >= TypicalSolarMass Then Master.Color = System.Drawing.Color.GhostWhite
                    'If Master.Mass >= TypicalSolarMass * 1.7 Then Master.Color = System.Drawing.Color.CornflowerBlue
                    'If Master.Mass >= TypicalSolarMass * 3.2 Then Master.Color = System.Drawing.Color.DeepSkyBlue
                Else
                    Dim Friction As Double = 0.7


                    Master.SpeedX = Master.SpeedX + (U1 - V1) * VekX * Friction
                    Master.SpeedY = Master.SpeedY + (U1 - V1) * VeKY * Friction


                    Slave.SpeedX = Slave.SpeedX + (U2 - V2) * VekX * Friction
                    Slave.SpeedY = Slave.SpeedY + (U2 - V2) * VeKY * Friction
                    Dim Vec1, Vec2 As SPoint
                    Vec1 = New SPoint((Slave.LocX - Master.LocX), (Slave.LocY - Master.LocY))
                    Vec2 = New SPoint((Master.LocX - Slave.LocX), (Master.LocY - Slave.LocY))
                    Slave.LocX = VecX + ((Slave.Size / 2) + Master.Size / 2)) * 
                    Slave.
                End If

            End If

        Else ' if bodies are at exact same position

            'Master.SpeedX = Master.SpeedX + (U1 - V1) * VekX
            ' Master.SpeedY = Master.SpeedY + (U1 - V1) * VeKY

            If Master.Mass > Slave.Mass Then
                Area1 = PI * (Master.Size ^ 2)
                Area2 = PI * (Slave.Size ^ 2)
                Area1 = Area1 + Area2
                Master.Size = Sqrt(Area1 / PI)
                Master.Mass = Master.Mass + Slave.Mass 'Sqr(Ball(B).Mass)
                Slave.Visible = False
            Else
                Area1 = PI * (Master.Size ^ 2)
                Area2 = PI * (Slave.Size ^ 2)
                Area1 = Area1 + Area2
                Slave.Size = Sqrt(Area1 / PI)
                Slave.Mass = Slave.Mass + Master.Mass 'Sqr(Ball(B).Mass)
                Master.Visible = False
            End If




        End If


    End Sub
    Public Sub ShrinkBallArray()
        'On Error Resume Next
        ' Debug.Print("Cleaning Ball Array")
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
    Private Sub AddNewBalls(ByRef NewBalls As List(Of BallParms))

        For Each AddBall As BallParms In NewBalls
            ReDim Preserve Ball(UBound(Ball) + 1)
            Dim u As Integer = UBound(Ball)

            Ball(u) = AddBall



        Next

        NewBalls.Clear()
    End Sub
    Private Function GetRandomNumber(ByVal Low As Double, ByVal High As Double) As Double
        ' Returns a random number,
        ' between the optional Low and High parameters

        'Dim number As Double = myRandom.NextDouble() * (High - Low) + Low
        'Return number


        Return objRandom.Next(Low, High + 1)

    End Function
End Class
