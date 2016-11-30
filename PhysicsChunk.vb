Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.IO
Imports System.Numerics

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
        Dim tmpBodys As New List(Of BallParms)
        ' Do Until bolStopWorker
        Dim EPS As Double = 2
        Dim OuterBody As BallParms() = MyBodys.ToArray

        'Do While bolStopLoop
        ' Thread.Sleep(100)
        'Loop
        '  StartTick = Now.Ticks
        '  Thread.Sleep(intDelay)
        '  StartTimer()
        If UBound(OuterBody) > 1 Then
            BUB = uBoundBody
            For A = 1 To UBound(OuterBody) ' Each OuterBody(A) As BallParms In MyBodys 'A = lBoundBody To uBoundBody
                OuterBody(A).ForceX = 0
                OuterBody(A).ForceY = 0
                OuterBody(A).ForceTot = 0
                If OuterBody(A).Visible Then
                    If OuterBody(A).MovinG = False Then
                        For B = 1 To UBound(Bodys)
                            If OuterBody(A).UID <> Bodys(B).UID And Bodys(B).Visible Then
                                'If OuterBody(A).Index = 792 And Bodys(B).Index = 2002 Then
                                '    Debug.Print(DistSqrt & " - " & OuterBody(A).Size & " - " & Bodys(B).Size)
                                'End If


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
                                    '  CollideBodies(OuterBody(A), Bodys(B))
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
                                        Force = TotMass / (DistSqrt * DistSqrt + EPS * EPS) ' (Dist * DistSqrt)


                                        ForceX = Force * DistX / DistSqrt
                                        ForceY = Force * DistY / DistSqrt
                                        OuterBody(A).ForceTot += Force


                                        OuterBody(A).ForceX += ForceX
                                        OuterBody(A).ForceY += ForceY

                                        'OuterBody(A).SpeedX += StepMulti * ForceX / M1
                                        'OuterBody(A).SpeedY += StepMulti * ForceY / M1

                                        If DistSqrt < 100 Then


                                            If DistSqrt <= (OuterBody(A).Size / 2) + (Bodys(B).Size / 2) Then 'Collision reaction



                                                CollideBodies(OuterBody(A), Bodys(B))

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

                'OuterBody(A).LocX = OuterBody(A).LocX + (StepMulti * OuterBody(A).SpeedX)
                'OuterBody(A).LocY = OuterBody(A).LocY + (StepMulti * OuterBody(A).SpeedY)
                ' tmpBodys.Add(OuterBody(A))

                If OuterBody(A).ForceTot > OuterBody(A).Mass * 4 And Not OuterBody(A).Flags.Contains("BH") Then ' And OuterBody(A).Size < 10 
                    OuterBody(A).InRoche = True
                    NewBalls.AddRange(FractureBall(OuterBody(A)))
                ElseIf (OuterBody(A).ForceTot * 2) < OuterBody(A).Mass * 4 Then ' And OuterBody(A).Size > 10
                    OuterBody(A).InRoche = False

                End If

            Next A
        End If

        ''    ShrinkBallArray()
        ''If UBound(Ball) > 10000 Then
        ''End If
        UpdateBodies(OuterBody)
        If NewBalls.Count > 0 Then
            MyBodys.Clear()
            MyBodys.AddRange(OuterBody)

            MyBodys.AddRange(NewBalls)
            '  OuterBody = MyBodys.ToArray
            'AddNewBalls(NewBalls)
            '  tmpBodys.AddRange(NewBalls)
        Else
            MyBodys.Clear()
            MyBodys.AddRange(OuterBody)
        End If



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

        '  MyBodys = OuterBody 'tmpBodys
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
        Dim TotMass As Double
        Dim Force, ForceX, ForceY As Double
        Dim EPS As Double = 2

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
            'If Not Master.InRoche Then


            '  If Master.Mass <> Slave.Mass Then
            If Not Master.InRoche And Slave.InRoche Then

                If Master.Mass > Slave.Mass Then


                    PrevSpdX = Master.SpeedX
                    PrevSpdY = Master.SpeedY

                    Master.SpeedX = Master.SpeedX + (U1 - V1) * VekX
                    Master.SpeedY = Master.SpeedY + (U1 - V1) * VeKY
                    Slave.Visible = False



                    Area1 = PI * (Master.Size ^ 2)
                    Area2 = PI * (Slave.Size ^ 2)
                    Area1 = Area1 + Area2
                    Master.Size = Sqrt(Area1 / PI)
                    Master.Mass = Master.Mass + Slave.Mass 'Sqr(Ball(B).Mass)

                ElseIf Master.Mass = Slave.Mass Then

                    If UIDtoInt(Master.UID) > UIDtoInt(Slave.UID) Then

                        PrevSpdX = Master.SpeedX
                        PrevSpdY = Master.SpeedY

                        Master.SpeedX = Master.SpeedX + (U1 - V1) * VekX
                        Master.SpeedY = Master.SpeedY + (U1 - V1) * VeKY
                        Slave.Visible = False



                        Area1 = PI * (Master.Size ^ 2)
                        Area2 = PI * (Slave.Size ^ 2)
                        Area1 = Area1 + Area2
                        Master.Size = Sqrt(Area1 / PI)
                        Master.Mass = Master.Mass + Slave.Mass 'Sqr(Ball(B).Mass)
                    Else
                        Master.Visible = False

                    End If




                    '  Master.Visible = False


                End If
            ElseIf Not Master.InRoche And Not Slave.InRoche Then
                If Master.Mass > Slave.Mass Then

                    PrevSpdX = Master.SpeedX
                    PrevSpdY = Master.SpeedY

                    Master.SpeedX = Master.SpeedX + (U1 - V1) * VekX
                    Master.SpeedY = Master.SpeedY + (U1 - V1) * VeKY
                    Slave.Visible = False



                    Area1 = PI * (Master.Size ^ 2)
                    Area2 = PI * (Slave.Size ^ 2)
                    Area1 = Area1 + Area2
                    Master.Size = Sqrt(Area1 / PI)
                    Master.Mass = Master.Mass + Slave.Mass 'Sqr(Ball(B).Mass)
                ElseIf Master.Mass = Slave.Mass Then

                    '  If Master.Index > Slave.Index Then
                    If UIDtoInt(Master.UID) > UIDtoInt(Slave.UID) Then
                        'Debug.Print(UIDtoInt(Master.UID).ToString)
                        'Debug.Print(UIDtoInt(Slave.UID).ToString)
                        PrevSpdX = Master.SpeedX
                        PrevSpdY = Master.SpeedY

                        Master.SpeedX = Master.SpeedX + (U1 - V1) * VekX
                        Master.SpeedY = Master.SpeedY + (U1 - V1) * VeKY
                        Slave.Visible = False



                        Area1 = PI * (Master.Size ^ 2)
                        Area2 = PI * (Slave.Size ^ 2)
                        Area1 = Area1 + Area2
                        Master.Size = Sqrt(Area1 / PI)
                        Master.Mass = Master.Mass + Slave.Mass 'Sqr(Ball(B).Mass)
                    Else
                        Master.Visible = False

                    End If

                Else


                    Master.Visible = False

                End If


            ElseIf Master.InRoche And Slave.InRoche Then

                TotMass = Master.Mass * Slave.Mass
                Force = TotMass / (DistSqrt * DistSqrt + EPS * EPS)
                ForceX = Force * DistX / DistSqrt
                ForceY = Force * DistY / DistSqrt
                Master.ForceX -= ForceX
                Master.ForceY -= ForceY
                Slave.ForceX -= ForceX
                Slave.ForceY -= ForceY


                Dim Friction As Double = 0.8
                Master.SpeedX += (U1 - V1) * VekX * Friction
                Master.SpeedY += (U1 - V1) * VeKY * Friction


                Slave.SpeedX += (U2 - V2) * VekX * Friction
                Slave.SpeedY += (U2 - V2) * VeKY * Friction

            ElseIf Master.InRoche And Not Slave.InRoche Then

                Master.Visible = False





            End If

            ' End If





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
    Private Function UIDtoInt(UID As String) As BigInteger
        Dim bytGUIDBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(UID) 'adUserDirectoryRecord.Guid.ToByteArray()
        Array.Resize(bytGUIDBytes, 17)

        Return New BigInteger(bytGUIDBytes)
    End Function
    Private Sub UpdateBodies(ByRef Bodies() As BallParms)

        For i As Integer = 0 To UBound(Bodies)
            Bodies(i).SpeedX += StepMulti * Bodies(i).ForceX / Bodies(i).Mass
            Bodies(i).SpeedY += StepMulti * Bodies(i).ForceY / Bodies(i).Mass
            Bodies(i).LocX += StepMulti * Bodies(i).SpeedX
            Bodies(i).LocY += StepMulti * Bodies(i).SpeedY

        Next


    End Sub
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

    Private Function FractureBall(ByRef Body As BallParms) As List(Of BallParms)
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
                tmpBall.Index = UBound(Ball) + 1
                tmpBall.UID = Guid.NewGuid.ToString
                tmpBall.IsFragment = True
                tmpBall.InRoche = True
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
