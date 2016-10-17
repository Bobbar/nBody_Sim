
Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Module PhysicsLoop
    Public pic_scale As Single
    Public RelBallPosMod As New Point


    Public gravity As Single = 0.0
    Public friction As Single = 0.99
    Public right_side As Integer
    Public bottom_side As Integer
    Public g As Integer
    Public Sel As Integer, MoV As Integer


    Public bGrav As Integer

    'Public BallSize(0 To 1000) As Single

    'Public speedX(0 To 1000) As Single
    'Public speedY(0 To 1000) As Single
    'Public ax(0 To 1000) As Single
    'Public ay(0 To 1000) As Single
    'Public ball_loc_x(0 To 1000) As Single
    'Public ball_loc_y(0 To 1000) As Single
    'Public MoVinG(0 To 1000) As Long
    'Public old_loc_x(0 To 1000) As Single
    'Public old_loc_y(0 To 1000) As Single




    'Public BposX As Single
    'Public BPosY As Single

    'Public bCenterX(0 To 1000) As Single
    'Public bCenterY(0 To 1000) As Single


    'Public w1 As Single
    'Public w2 As Single

    'Public w As Single
    'Public u As Single
    'Public r As Single



    ' Public Collision, ColB







    'Public aN As Single
    'Public bN As Single
    'Public BSize As Single
    'Public Hsize As Single

    Public FPS As Single

    ' Public rd As Single
    Public bolStop As Boolean

    '// Some Variables are not used by my code, forget them. I didnt have the time to make my code clean....

    Public Structure BallParms
        Public Size As Single
        Public LocX As Single
        Public LocY As Single
        Public SpeedX As Single
        Public SpeedY As Single
        Public PrevSpeedX As Single
        Public PrevSpeedY As Single
        Public PrevLocX As Single
        Public PrevLocY As Single
        Public GravX As Single
        Public GravY As Single
        Public MovinG As Boolean
        Public Old_LocX As Single
        Public Old_LocY As Single


        Public ShadAngle As Single

        Public Origin As Long


        Public Locked As Boolean
        Public Visible As Boolean
        Public Mass As Double

        Public Color As Color
        Public Flags As String

    End Structure


    Public Ball() As BallParms



    Dim PrevX As Single
    Dim PrevY As Single
    Dim CurrentPX As Single
    Dim CurrentPY As Single


    Public lngFollowBall As Long

    Public StepMulti As Double
    Public bolBallsRemoved As Boolean
    Public bolStopDraw As Boolean

    Public MouseDnX As Double
    Public MouseDnY As Double
    Public MouseUpX As Double
    Public MouseUpY As Double

    Public bolShiftDown As Boolean
    Public bolAltDown As Boolean
    Public bolFollow As Boolean
    Public PubSel As Long
    Public SpeedChange As Double
    Public MouseIndex As Long
    Public FollowX As Double, FollowY As Double

    Public bolStopLoop As Boolean

    Public t As Threading.Thread
    Public t2 As Threading.Thread
    Public t3 As Threading.Thread
    Public s As Threading.Thread
    Public m As Threading.Thread

    Public objRandom As New System.Random(CType(System.DateTime.Now.Ticks Mod System.Int32.MaxValue, Integer))
    Public DistanceX As Single, DistanceY As Single
    Public Const NotQuiteZero As Single = 0.00001
    Public Const Ninety As Long = 90
    Public Const TwoSeventy As Long = 270
    Public Const RadsToDegs As Single = 57.29578


    Public UpDownVal As Long
    Public Loop1I As Integer, Loop2I As Integer, Loop3I As Integer, PubIndex As Integer


    Public LoopLockObject As New Object
    Public Thread1Done As Boolean, Thread2Done As Boolean, Thread3Done As Boolean, bolStart1 As Boolean, bolStart2 As Boolean, bolStart3 As Boolean
    Public intDelay As Integer
    Public Time As Long
    Public TestCount As Long

    Public bolShadows As Boolean
    Public intTargetFPS As Integer

    Public Sub wait(ByVal interval As Integer)
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            ' Allows UI to remain responsive
            Application.DoEvents()
        Loop
        sw.Stop()
    End Sub
    Public Sub MainLoop()
        Dim VeKY As Double
        Dim VekX As Double

        Dim LenG As Double

        Dim V1x As Double
        Dim V2x As Double
        Dim M1 As Double
        Dim M2 As Double


        Dim V1y As Double
        Dim V2y As Double

        Dim a As Double
        Dim b As Double
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

        Dim X As Long

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





        Do Until bolStopLoop
            wait(intDelay)

            If UBound(Ball) > 1 Then
                BUB = UBound(Ball)



                Parallel.For(1, BUB + 1, options, Sub(i)


                                                      SyncLock LoopLockObject
                                                          If Ball(i).Visible Then
                                                              If Ball(i).MovinG = False Then







                                                                  For X = 1 To BUB
                                                                      If Ball(X).Visible Then

                                                                          If Form1.chkShadow.Checked Then
                                                                              If InStr(1, Ball(i).Flags, "S") Then
                                                                                  Dim m As Double, SlX As Double, SlY As Double
                                                                                  SlX = Ball(X).LocX - Ball(i).LocX
                                                                                  SlY = Ball(X).LocY - Ball(i).LocY

                                                                                  m = SlY / SlX

                                                                                  Ball(X).ShadAngle = Math.Atan2(Ball(X).LocY - Ball(i).LocY, Ball(X).LocX - Ball(i).LocX)   'Math.Tan(SlX / SlY) 'Math.Atan2(SlY, SlX) * 180 / PI 'Math.Atan(SlX / SlY) * 180 / PI

                                                                                  'Debug.Print(Ball(X).ShadAngle)


                                                                                  '

                                                                              End If
                                                                          End If




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

                                                                                      If Force > (Ball(i).Mass ^ 3) And Ball(X).Mass > Ball(i).Mass * 5 And Ball(i).Size > 1 And Ball(i).Visible = True Then  ' And Not InStr(1, Bll(i).Flags, "B")

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
                                                                                          RadUPX = (Ball(i).LocX) + PrevSize / 2 + Ball(i).SpeedX * StepMulti
                                                                                          RadDNX = (Ball(i).LocX) - PrevSize / 2 + Ball(i).SpeedX * StepMulti
                                                                                          RadUPY = (Ball(i).LocY) + PrevSize / 2 + Ball(i).SpeedY * StepMulti
                                                                                          RadDNY = (Ball(i).LocY) - PrevSize / 2 + Ball(i).SpeedY * StepMulti
                                                                                          'RadUPX = (Ball(i).LocX) + PrevSize / 2
                                                                                          'RadDNX = (Ball(i).LocX) - PrevSize / 2
                                                                                          'RadUPY = (Ball(i).LocY) + PrevSize / 2
                                                                                          'RadDNY = (Ball(i).LocY) - PrevSize / 2

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


                                                                                              ' Ball(u).LocY = Ball(i).LocY + Ball(u).Size * h
                                                                                              'Ball(u).LocX = Ball(i).LocX + Ball(u).Size * h


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

                                                                              Dim Multi As Double
                                                                              Multi = 0.7
                                                                              If d < (Ball(i).Size + Ball(X).Size) Then
                                                                                  Dim BlahX As Double
                                                                                  Dim BlahY As Double
                                                                                  Dim ClsForce2 As Double

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
                                                                                      ' Debug.Print(ClsSpeed)
                                                                                      M1 = Ball(i).Mass / 1000 ' * 4 '^ 2
                                                                                      M2 = Ball(X).Mass / 1000 ' * 4 '^ 2
                                                                                      'ClsForce = ClsSpeed * (M1 + M2)

                                                                                      ClsForce2 = ClsSpeed * M2

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
                                                                                          'Debug.Print(ClsForce)
                                                                                          If 1 = 1 Then ' ClsForce > (Ball(i).Mass / 4 + Ball(X).Mass / 4)



                                                                                              'If Ball(i).Mass > Ball(X).Mass And ClsSpeed * (Ball(X).Mass) > ((Ball(i).Mass * 2) ^ 2) And Ball(i).Size > 1 And Ball(X).Size > 1 Then
                                                                                              'If ClsSpeed * (Ball(X).Mass) > ((Ball(i).Mass * 4) ^ 2) And Ball(i).Size > 1 And Ball(X).Size > 1 Then
                                                                                              '    'Debug.Print(ClsForce2)


                                                                                              '    V1x = Ball(i).SpeedX
                                                                                              '    V1y = Ball(i).SpeedY
                                                                                              '    V2x = Ball(X).SpeedX
                                                                                              '    V2y = Ball(X).SpeedY

                                                                                              '    M1 = Ball(i).Mass / 1000 ' * 4 '^ 2
                                                                                              '    M2 = Ball(X).Mass / 1000 ' * 4 '^ 2


                                                                                              '    VekX = (Ball(i).LocX - Ball(X).LocX) / 2
                                                                                              '    VeKY = (Ball(i).LocY - Ball(X).LocY) / 2

                                                                                              '    LenG = Sqrt(VeKY * VeKY + VekX * VekX)

                                                                                              '    VekX = VekX / LenG
                                                                                              '    VeKY = VeKY / LenG

                                                                                              '    V1 = VekX * V1x + VeKY * V1y
                                                                                              '    V2 = VekX * V2x + VeKY * V2y

                                                                                              '    U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
                                                                                              '    U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)

                                                                                              '    Ball(i).SpeedX = Ball(i).SpeedX + (U1 - V1) * VekX * 0.5
                                                                                              '    Ball(i).SpeedY = Ball(i).SpeedY + (U1 - V1) * VeKY * 0.5

                                                                                              '    Ball(X).SpeedX = Ball(X).SpeedX + (U1 - V1) * -VekX * 0.5
                                                                                              '    Ball(X).SpeedY = Ball(X).SpeedY + (U1 - V1) * -VeKY * 0.5

                                                                                              '    FractureBall(i)
                                                                                              '    FractureBall(X)
                                                                                              '    'GoTo here

                                                                                              'End If

                                                                                              'ElseIf Ball(i).Mass < Ball(X).Mass And ClsSpeed * (Ball(i).Mass) > ((Ball(X).Mass * 2) ^ 2) And Ball(i).Size > 1 And Ball(X).Size > 1 Then
                                                                                              '    ' Debug.Print(ClsSpeed & " " & ClsSpeed * (Ball(i).Mass / 2) & " " & Ball(X).Mass ^ 10)

                                                                                              '    V1x = Ball(i).SpeedX
                                                                                              '    V1y = Ball(i).SpeedY
                                                                                              '    V2x = Ball(X).SpeedX
                                                                                              '    V2y = Ball(X).SpeedY

                                                                                              '    M1 = Ball(i).Mass / 1000 ' * 4 '^ 2
                                                                                              '    M2 = Ball(X).Mass / 1000 ' * 4 '^ 2


                                                                                              '    VekX = (Ball(i).LocX - Ball(X).LocX) / 2
                                                                                              '    VeKY = (Ball(i).LocY - Ball(X).LocY) / 2

                                                                                              '    LenG = Sqrt(VeKY * VeKY + VekX * VekX)

                                                                                              '    VekX = VekX / LenG
                                                                                              '    VeKY = VeKY / LenG

                                                                                              '    V1 = VekX * V1x + VeKY * V1y
                                                                                              '    V2 = VekX * V2x + VeKY * V2y

                                                                                              '    U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
                                                                                              '    U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)

                                                                                              '    ' Ball(i).SpeedX = Ball(i).SpeedX + (U1 - V1) * VekX
                                                                                              '    ' Ball(i).SpeedY = Ball(i).SpeedY + (U1 - V1) * VeKY

                                                                                              '    Ball(X).SpeedX = Ball(X).SpeedX + (U1 - V1) * VekX * 0.5
                                                                                              '    Ball(X).SpeedY = Ball(X).SpeedY + (U1 - V1) * VeKY * 0.5

                                                                                              '    '  FractureBall(i)
                                                                                              '    FractureBall(X)
                                                                                              '    'GoTo here

                                                                                              'End If


                                                                                              If Ball(X).Mass < Ball(i).Mass And ClsForce2 > (Ball(i).Mass) And Ball(i).Size > 1 And Ball(X).Size > 1 Then 'And InStr(1, Ball(i).Flags, "R") = 0 And InStr(1, Ball(i).Flags, "S") = 0
                                                                                                  Debug.Print(Ball(X).Mass)
                                                                                                  FractureBall(i)
                                                                                                  FractureBall(X)
                                                                                                  GoTo here

                                                                                              End If


                                                                                              If InStr(1, Ball(X).Flags, "R") > 0 And Force < (Ball(i).Mass ^ 3) Or InStr(1, Ball(X).Flags, "R") = 0 Then
                                                                                                  Dim Area1 As Double, Area2 As Double



                                                                                                  If Ball(i).Mass > Ball(X).Mass Then



                                                                                                      If Ball(X).Origin <> i Then

                                                                                                          Ball(i).Flags = Replace$(Ball(i).Flags, "R", "")
                                                                                                          Ball(i).SpeedX = Ball(i).SpeedX + (U1 - V1) * VekX
                                                                                                          Ball(i).SpeedY = Ball(i).SpeedY + (U1 - V1) * VeKY



                                                                                                          Area1 = PI * (Ball(i).Size ^ 2)
                                                                                                          Area2 = PI * (Ball(X).Size ^ 2)

                                                                                                          Area1 = Area1 + Area2
                                                                                                          Ball(i).Size = Sqrt(Area1 / PI)



                                                                                                          Ball(i).Mass = Ball(i).Mass + Ball(X).Mass 'Sqr(Ball(X).Mass)
                                                                                                          Ball(X).Visible = False

                                                                                                      End If






                                                                                                  Else
                                                                                                      If Ball(i).Origin <> X Then
                                                                                                          Ball(i).Flags = Replace$(Ball(i).Flags, "R", "")
                                                                                                          Ball(X).SpeedX = Ball(X).SpeedX + (U2 - V2) * VekX
                                                                                                          Ball(X).SpeedY = Ball(X).SpeedY + (U2 - V2) * VeKY

                                                                                                          Area1 = PI * (Ball(X).Size ^ 2)
                                                                                                          Area2 = PI * (Ball(i).Size ^ 2)

                                                                                                          Area1 = Area1 + Area2
                                                                                                          Ball(X).Size = Sqrt(Area1 / PI)



                                                                                                          Ball(X).Mass = Ball(X).Mass + Ball(i).Mass 'Sqr(Ball(i).Mass)
                                                                                                          Ball(i).Visible = False

                                                                                                      End If



                                                                                                  End If

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
here:
                                                                                          End If

                                                                                      End If

                                                                                  End If
                                                                              End If




                                                                              If Ball(i).Mass > 350 And Ball(i).Visible = True Then 'solar wind
                                                                                  If InStr(Ball(i).Flags, "S") = 0 Then Ball(i).Flags = Ball(i).Flags + "S"
                                                                                  'rc = (Ball(X).Size / 4) + (Ball(i).Size / 4)
                                                                                  'ry = (Ball(X).LocY - Ball(i).LocY) / 2
                                                                                  'rx = (Ball(X).LocX - Ball(i).LocX) / 2
                                                                                  'd = Sqrt(rx * rx + ry * ry)

                                                                                  'If d < 500 Then


                                                                                  '    Dim m As Double, SlX As Double, SlY As Double
                                                                                  '    Dim VecX As Double, VecY As Double
                                                                                  '    Dim C As Double, S As Double
                                                                                  '    Dim Dis As Double, DisSqr As Double, F As Double, Lx As Double, Ly As Double


                                                                                  '    SlX = Ball(X).LocX - Ball(i).LocX
                                                                                  '    SlY = Ball(X).LocY - Ball(i).LocY

                                                                                  '    m = SlY / SlX

                                                                                  '    a = Math.Atan2(Ball(X).LocY - Ball(i).LocY, Ball(X).LocX - Ball(i).LocX)

                                                                                  '    C = Cos(a)
                                                                                  '    S = Sin(a)

                                                                                  '    VecX = (Ball(X).LocX + Ball(X).Size * C) - Ball(X).LocX
                                                                                  '    VecY = (Ball(X).LocY + Ball(X).Size * S) - Ball(X).LocY


                                                                                  '    Lx = Ball(X).LocX - Ball(i).LocX
                                                                                  '    Ly = Ball(X).LocY - Ball(i).LocY
                                                                                  '    Dis = (Lx * Lx) + (Ly * Ly)
                                                                                  '    DisSqr = Sqrt(Dis)
                                                                                  '    F = (Ball(i).Mass ^ 2) / (Dis * DisSqr)


                                                                                  '    Ball(X).SpeedX = Ball(X).SpeedX + F * VecX
                                                                                  '    Ball(X).SpeedY = Ball(X).SpeedY + F * VecY

                                                                                  'End If


                                                                              End If




                                                                          End If


                                                                      End If








                                                                  Next X

                                                                  '// Collision with Walls

                                                                  If bGrav = 0 Then
                                                                      right_side = Form1.Render.Width - Ball(i).Size
                                                                      bottom_side = Form1.Render.Height - Ball(i).Size

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

                                                          Ball(i).LocX = Ball(i).LocX + (Ball(i).SpeedX * StepMulti)
                                                          Ball(i).LocY = Ball(i).LocY + (Ball(i).SpeedY * StepMulti)

                                                      End SyncLock
                                                  End Sub)



            End If

            Application.DoEvents()



            If UBound(Ball) > 5000 And bolBallsRemoved Then
                s = New Threading.Thread(AddressOf Form1.ShrinkBallArray)
                s.Start()
                s.Join()


            End If



            If bolFollow Then
                Dim DiffX As Double, DiffY As Double

                If Ball(lngFollowBall).LocX <> FollowX Or Ball(lngFollowBall).LocY <> FollowY Then

                    DiffX = Ball(lngFollowBall).LocX - (Form1.Render.Width / 2) 'FollowX
                    DiffY = Ball(lngFollowBall).LocY - (Form1.Render.Height / 2) 'FollowY

                    For i = 1 To UBound(Ball)

                        Ball(i).LocX = Ball(i).LocX - DiffX
                        Ball(i).LocY = Ball(i).LocY - DiffY

                    Next

                    FollowX = Ball(lngFollowBall).LocX
                    FollowY = Ball(lngFollowBall).LocY

                End If

            End If



            If Form1.chkTrails.Checked And s.ThreadState <> Threading.ThreadState.Running Then
                For i = 1 To UBound(Ball)

                    If Ball(i).Visible And Ball(i).LocX > 0 And Ball(i).LocX < Form1.Render.Width And Ball(i).LocY > 0 And Ball(i).LocY < Form1.Render.Height And s.ThreadState <> Threading.ThreadState.Running Then
                        'g = g + 1
                        ' If g > 4 Then g = 0


                        ' e.Graphics.FillEllipse(Brushes.LightBlue, ball_loc_x(i) - 1, ball_loc_y(i) - 1, BallSize(i) + 2, BallSize(i) + 2)
                        ' e.Graphics.FillEllipse(Brushes.Blue, ball_loc_x(i), ball_loc_y(i), BallSize(i), BallSize(i))

                        ' Dim myBrush As New SolidBrush(Ball(i).Color)
                        Dim myPen As New Pen(Ball(i).Color)

                        'Render.CreateGraphics.SmoothingMode = SmoothingMode.AntiAlias
                        ' Render.CreateGraphics.FillEllipse(Brushes.Black, Ball(i).LocX - Ball(i).Size / 2 - 1, Ball(i).LocY - Ball(i).Size / 2 - 1, Ball(i).Size + 2, Ball(i).Size + 2)
                        'Render.CreateGraphics.FillEllipse(myBrush, Ball(i).LocX - Ball(i).Size / 2, Ball(i).LocY - Ball(i).Size / 2, Ball(i).Size, Ball(i).Size)
                        Form1.Render.CreateGraphics.DrawEllipse(myPen, Ball(i).LocX - Ball(i).Size / 2, Ball(i).LocY - Ball(i).Size / 2, Ball(i).Size, Ball(i).Size)

                    End If

                Next i

            End If

            'If bolBallsRemoved Then ShrinkBallArray()

            If Not Form1.chkTrails.Checked And s.ThreadState <> Threading.ThreadState.Running Then
                'Render.Refresh()

                Dim bm As New Bitmap(CInt(Form1.Render.Width), CInt(Form1.Render.Height)) '(CInt(pic_scale * Render.Width), CInt(pic_scale * Render.Height))
                Using gr As Graphics = Graphics.FromImage(bm)
                    gr.Clear(Color.White)
                    gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                    gr.ScaleTransform(pic_scale, pic_scale)
                    Drawr(gr)
                End Using
                Form1.Render.Image = bm
            End If





            FPS = FPS + 1




        Loop








    End Sub
    Public Function RandomRGBColor() As Color
        Dim m_Rnd As New Random
        Return Color.FromArgb(255, objRandom.Next(0, 255), objRandom.Next(0, 255), objRandom.Next(0, 255))

    End Function
    Public Function fnMass(Radius As Double) As Double

        fnMass = 0

        fnMass = Sqrt(PI * (Radius ^ 2))


    End Function


    Public Function GetRandomNumber(ByVal Low As Double, ByVal High As Double) As Double
        ' Returns a random number,
        ' between the optional Low and High parameters
        Return objRandom.Next(Low, High + 1)
    End Function
    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Dim Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function
    Public Function fnRadius(Area As Double) As Double
        fnRadius = 0

        fnRadius = Sqrt(Area / PI)



    End Function
    Public Sub FractureBall(ByVal i As Long)
        Dim NewBallSize As Single
        Dim NewBallMass As Single
        Dim Divisor As Single
        Dim PrevSize As Single
        Dim PrevMass As Single
        Dim TotBMass As Double
        Dim Area As Double
        Dim RadUPX As Double, RadDNX As Double, RadUPY As Double, RadDNY As Double

        ' i = UBound(Ball)

        If Ball(i).Visible = True And Ball(i).Size > 1 And InStr(1, Ball(i).Flags, "R") = 0 Then
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
            RadUPX = (Ball(i).LocX) + PrevSize / 2 + Ball(i).SpeedX * StepMulti
            RadDNX = (Ball(i).LocX) - PrevSize / 2 + Ball(i).SpeedX * StepMulti
            RadUPY = (Ball(i).LocY) + PrevSize / 2 + Ball(i).SpeedY * StepMulti
            RadDNY = (Ball(i).LocY) - PrevSize / 2 + Ball(i).SpeedY * StepMulti


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
                'Ball(u).Flags = Ball(i).Flags + "R"
                Ball(u).Color = Ball(i).Color 'vbWhite
                Ball(u).Visible = True


                '  Ball(u).LocY = Ball(i).LocY + Ball(u).Size * 2



                Ball(u).LocX = GetRandomNumber((RadDNX), RadUPX)

                Ball(u).LocY = GetRandomNumber((RadDNY), RadUPY)

            Next h
        End If

    End Sub
End Module
