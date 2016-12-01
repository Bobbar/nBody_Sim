Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports System.IO
Public Module PhysicsLoop

    ''' <summary>
    ''' Point With Single Data Types
    ''' </summary>
    Public Class SPoint
        Public X As Single
        Public Y As Single
        Sub New(pnt As Point)
            X = pnt.X
            Y = pnt.Y
        End Sub
        Sub New(Xval As Single, Yval As Single)
            X = Xval
            Y = Yval
        End Sub
        Sub New()
            X = 0
            Y = 0
        End Sub
        Public Overrides Function ToString() As String
            Return "{X=" + X.ToString & ",Y=" + Y.ToString + "}"
        End Function
    End Class
    Public pic_scale As Double
    Public RelBallPosMod As New SPoint
    Public ScaleOffset As New SPoint
    Public gravity As Single = 0.0
    Public friction As Single = 0.99
    Public right_side As Integer
    Public TypicalSolarMass As Integer = 2000
    Public bottom_side As Integer
    Public Density As Double = 5.0
    Public g As Integer
    Public Sel As Integer, MoV As Integer = 0
    Public bGrav As Boolean
    Public FPS As Single
    ' Public rd As Single
    Public bolStop As Boolean
    '// Some Variables are not used by my code, forget them. I didnt have the time to make my code clean....
    <ProtoBuf.ProtoContract>
   Public Structure BallParms

        <ProtoBuf.ProtoMember(1)>
        Public Size As Double
        <ProtoBuf.ProtoMember(2)>
        Public LocX As Double
        <ProtoBuf.ProtoMember(3)>
        Public LocY As Double
        <ProtoBuf.ProtoMember(4)>
        Public SpeedX As Double
        <ProtoBuf.ProtoMember(5)>
        Public SpeedY As Double
        <ProtoBuf.ProtoMember(6)>
        Public ForceX As Double
        <ProtoBuf.ProtoMember(7)>
        Public ForceY As Double
        <ProtoBuf.ProtoMember(8)>
        Public ForceTot As Double
        <ProtoBuf.ProtoMember(9)>
        Public UID As String
        <ProtoBuf.ProtoMember(10)>
        Public MovinG As Boolean
        <ProtoBuf.ProtoMember(11)>
        Public Visible As Boolean
        <ProtoBuf.ProtoMember(12)>
        Public Mass As Double


        Public Color As Color
        <ProtoBuf.ProtoMember(13)>
        Private Property ColorSerialized() As Integer
            Get
                Return Color.ToArgb()
            End Get
            Set
                Color = Color.FromArgb(Value)
            End Set
        End Property


        <ProtoBuf.ProtoMember(14)>
        Public InRoche As Boolean
        <ProtoBuf.ProtoMember(15)>
        Public Flags As String


        ''   Public Index As Integer

        'Public Size As Double
        'Public LocX As Double
        'Public LocY As Double
        'Public SpeedX As Double
        'Public SpeedY As Double
        ''Public PrevSpeedX As Single
        ''Public PrevSpeedY As Single
        'Public ForceX As Double
        'Public ForceY As Double
        'Public ForceTot As Double
        'Public UID As String
        ''Public PrevLocX As Single
        ''Public PrevLocY As Single
        ''Public GravX As Single
        ''Public GravY As Single
        'Public MovinG As Boolean
        ''Public Old_LocX As Single
        ''Public Old_LocY As Single
        ''     Public ShadAngle As Single
        ''Public Origin As Long
        ''   Public Locked As Boolean
        'Public Visible As Boolean
        'Public Mass As Double
        'Public Color As Color
        ''   Public IsFragment As Boolean
        'Public InRoche As Boolean
        'Public Flags As String
        ''    Public Group As List(Of BallParms)
    End Structure
    Public Ball() As BallParms
    Public RecordedBodies As New List(Of BallParms())
    'Private _nestedArray As List(Of BallParms())
    ' The nested array I would like to serialize.
    <ProtoBuf.ProtoMember(1)>
    Public Property _nestedArrayForProtoBuf() As List(Of ProtobufArray(Of BallParms))
        ' Never used elsewhere
        Get
            If RecordedBodies Is Nothing Then
                '  ( _nestedArray == null || _nestedArray.Count == 0 )  if the default constructor instanciate it
                Return Nothing
            End If
            Return RecordedBodies.[Select](Function(p) New ProtobufArray(Of BallParms)(p)).ToList()
        End Get
        Set
            RecordedBodies = Value.[Select](Function(p) p.MyArray).ToList()
        End Set
    End Property


    <ProtoBuf.ProtoContract>
    Public Class ProtobufArray(Of T)
        ' The intermediate type
        <ProtoBuf.ProtoMember(1)>
        Public MyArray As T()

        Public Sub New()
        End Sub
        Public Sub New(array As T())
            MyArray = array
        End Sub
    End Class



    '<ProtoBuf.ProtoContract>
    'Public Class BodyParms
    '    Sub New(body As BallParms)
    '        Size = body.Size
    '        LocX = body.LocX
    '        LocY = body.LocY
    '        UID = body.UID
    '        Visible = body.Visible
    '        Color = body.Color
    '        Flags = body.Flags
    '        'SpeedX = body.SpeedX
    '        'SpeedY = body.SpeedY
    '        'ForceX = body.ForceX
    '        'ForceY = body.ForceY
    '        'ForceTot = body.ForceTot






    '    End Sub


    '    <ProtoBuf.ProtoMember(1)>
    '    Public Size As Double
    '    <ProtoBuf.ProtoMember(2)>
    '    Public LocX As Double
    '    <ProtoBuf.ProtoMember(3)>
    '    Public LocY As Double
    '    <ProtoBuf.ProtoMember(4)>
    '    Public SpeedX As Double
    '    <ProtoBuf.ProtoMember(5)>
    '    Public SpeedY As Double
    '    <ProtoBuf.ProtoMember(6)>
    '    Public ForceX As Double
    '    <ProtoBuf.ProtoMember(7)>
    '    Public ForceY As Double
    '    <ProtoBuf.ProtoMember(8)>
    '    Public ForceTot As Double
    '    <ProtoBuf.ProtoMember(9)>
    '    Public UID As String
    '    <ProtoBuf.ProtoMember(10)>
    '    Public MovinG As Boolean
    '    <ProtoBuf.ProtoMember(11)>
    '    Public Visible As Boolean
    '    <ProtoBuf.ProtoMember(12)>
    '    Public Mass As Double
    '    <ProtoBuf.ProtoMember(13)>
    '    Public Color As Color
    '    <ProtoBuf.ProtoMember(14)>
    '    Public InRoche As Boolean
    '    <ProtoBuf.ProtoMember(15)>
    '    Public Flags As String


    'End Class

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
    'Public t As Threading.Thread
    'Public t2 As Threading.Thread
    'Public t3 As Threading.Thread
    Public s As Threading.Thread
    'Public m As Threading.Thread
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
    Public intDelay As Integer = 10
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
        'Thread.Sleep(interval)
    End Sub
    '    Public Sub MainLoop()
    '        Dim VeKY As Double
    '        Dim VekX As Double
    '        Dim LenG As Double
    '        Dim V1x As Double
    '        Dim V2x As Double
    '        Dim M1 As Double
    '        Dim M2 As Double
    '        Dim V1y As Double
    '        Dim V2y As Double
    '        Dim E As Double
    '        Dim F As Double
    '        Dim Abstand As Double
    '        Dim rc As Double
    '        Dim rx As Double
    '        Dim ry As Double
    '        Dim d As Double
    '        Dim V1 As Double
    '        Dim V2 As Double
    '        Dim U2 As Double
    '        Dim U1 As Double
    '        Dim ClsSpeedX As Double
    '        Dim ClsSpeedy As Double
    '        Dim ClsSpeed As Double
    '        'Dim ClsForce As Double
    '        Dim NewBallSize As Double
    '        Dim NewBallMass As Double
    '        Dim Divisor As Double
    '        Dim PrevSize As Double
    '        Dim PrevMass As Double
    '        Dim ForceX As Double
    '        Dim ForceY As Double
    '        Dim TotMass As Double
    '        Dim LoccX As Double
    '        Dim LoccY As Double
    '        Dim Veck As Double
    '        Dim VeckSqr As Double
    '        Dim Force As Double
    '        Dim BUB As Long
    '        'Dim B As Long
    '        Dim options As New ParallelOptions
    '        Dim Tasker As TaskScheduler
    '        options.MaxDegreeOfParallelism = 2
    '        options.TaskScheduler = Tasker
    '        Dim Its As Long
    '        Its = 1
    '        ' On Error Resume Next
    '        ' Dim i As Integer
    '        ' i = Loop1I
    'restart:
    '        Do Until bolStopLoop
    '            wait(intDelay)
    '            If UBound(Ball) > 1 Then
    '                BUB = UBound(Ball)
    '                Parallel.For(1, BUB + 1, options, Sub(A)
    '                                                      SyncLock LoopLockObject
    '                                                          '   For A = 1 To BUB
    '                                                          If Ball(A).Visible Then
    '                                                              If Ball(A).MovinG = False Then
    '                                                                  For B = 1 To BUB
    '                                                                      If Ball(B).Visible Then
    '                                                                          If bolShawdow Then
    '                                                                              If InStr(1, Ball(A).Flags, "S") Then
    '                                                                                  Dim m As Double, SlX As Double, SlY As Double
    '                                                                                  SlX = Ball(B).LocX - Ball(A).LocX
    '                                                                                  SlY = Ball(B).LocY - Ball(A).LocY
    '                                                                                  m = SlY / SlX
    '                                                                                  Ball(B).ShadAngle = Math.Atan2(Ball(B).LocY - Ball(A).LocY, Ball(B).LocX - Ball(A).LocX)   'Math.Tan(SlX / SlY) 'Math.Atan2(SlY, SlX) * 180 / PI 'Math.Atan(SlX / SlY) * 180 / PI
    '                                                                              End If
    '                                                                          End If
    '                                                                          If Ball(B).LocX = Ball(A).LocX And Ball(B).LocY = Ball(A).LocY And B <> A Then
    '                                                                              Ball(B).LocX = Ball(B).LocX + Ball(B).Size
    '                                                                              Ball(B).LocY = Ball(B).LocY + Ball(B).Size
    '                                                                          End If
    '                                                                          '// Collision Reaction (Vektors)
    '                                                                          If bGrav = 0 Then
    '                                                                          Else
    '                                                                              If Not Ball(A).Locked Then
    '                                                                                  If B <> A Then
    '                                                                                      M1 = Ball(A).Mass ^ 2 ' * 2
    '                                                                                      M2 = Ball(B).Mass ^ 2 ' * 2
    '                                                                                      TotMass = M1 * M2
    '                                                                                      LoccX = Ball(B).LocX - Ball(A).LocX
    '                                                                                      LoccY = Ball(B).LocY - Ball(A).LocY
    '                                                                                      Veck = (LoccX * LoccX) + (LoccY * LoccY)
    '                                                                                      VeckSqr = Sqrt(Veck)
    '                                                                                      Force = TotMass / (Veck * VeckSqr)
    '                                                                                      'If Double.IsNaN(Force) Then
    '                                                                                      '    Debug.Print(Force)
    '                                                                                      '    Stop
    '                                                                                      'End If
    '                                                                                      ForceX = Force * LoccX
    '                                                                                      ForceY = Force * LoccY
    '                                                                                      Ball(A).SpeedX = Ball(A).SpeedX + ForceX / M1
    '                                                                                      Ball(A).SpeedY = Ball(A).SpeedY + ForceY / M1
    '                                                                                      'If InStr(1, Ball(A).Flags, "P") Then
    '                                                                                      '    Debug.Print("P!!!!")
    '                                                                                      '    ' Stop
    '                                                                                      'End If
    '                                                                                      If Force > (Ball(A).Mass ^ 3) And Ball(B).Mass > Ball(A).Mass * 5 And Ball(A).Size > 1 And Ball(A).Visible = True Then  ' And Not InStr(1, Bll(A).Flags, "B")
    '                                                                                          FractureBall(A)
    '                                                                                      End If
    '                                                                                  End If
    '                                                                                  'End If ' **
    '                                                                              End If
    '                                                                          End If
    '                                                                          If A <> B And Ball(A).Visible = True Then
    '                                                                              rc = (Ball(A).Size / 4) + (Ball(B).Size / 4)
    '                                                                              ry = (Ball(A).LocY - Ball(B).LocY) / 2
    '                                                                              rx = (Ball(A).LocX - Ball(B).LocX) / 2
    '                                                                              d = Sqrt(rx * rx + ry * ry)
    '                                                                              Dim Multi As Double
    '                                                                              Multi = 0.7
    '                                                                              If d < (Ball(A).Size + Ball(B).Size) Then 'Collide
    '                                                                                  '  Dim BlahX As Double
    '                                                                                  '    Dim BlahY As Double
    '                                                                                  Dim ClsForce2 As Double
    '                                                                                  '  BlahX = (Ball(A).LocX + (Ball(A).LocX + Ball(A).SpeedX) / 2)
    '                                                                                  '         BlahY = (Ball(A).LocY + (Ball(A).LocY + Ball(A).SpeedY) / 2)
    '                                                                                  If d < rc Then
    '                                                                                      'Perlenkettenproblem liegt hier:
    '                                                                                      V1x = Ball(A).SpeedX
    '                                                                                      V1y = Ball(A).SpeedY
    '                                                                                      V2x = Ball(B).SpeedX
    '                                                                                      V2y = Ball(B).SpeedY
    '                                                                                      ClsSpeedX = V1x - V2x
    '                                                                                      ClsSpeedy = V1y - V2y
    '                                                                                      ClsSpeed = Abs(ClsSpeedX) + Abs(ClsSpeedy)
    '                                                                                      ' Debug.Print(ClsSpeed)
    '                                                                                      M1 = Ball(A).Mass / 1000 ' * 4 '^ 2
    '                                                                                      M2 = Ball(B).Mass / 1000 ' * 4 '^ 2
    '                                                                                      ' ClsForce = ClsSpeed * (M1 + M2)
    '                                                                                      ClsForce2 = ClsSpeed * M2
    '                                                                                      VekX = (Ball(A).LocX - Ball(B).LocX) / 2
    '                                                                                      VeKY = (Ball(A).LocY - Ball(B).LocY) / 2
    '                                                                                      LenG = Sqrt(VeKY * VeKY + VekX * VekX)
    '                                                                                      VekX = VekX / LenG
    '                                                                                      VeKY = VeKY / LenG
    '                                                                                      V1 = VekX * V1x + VeKY * V1y
    '                                                                                      V2 = VekX * V2x + VeKY * V2y
    '                                                                                      If V1 - V2 < 0 Then
    '                                                                                          U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
    '                                                                                          U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)
    '                                                                                          'Debug.Print(ClsForce)
    '                                                                                          ' If 1 = 1 Then ' ClsForce > (Ball(A).Mass / 4 + Ball(B).Mass / 4)
    '                                                                                          If Ball(B).Mass < Ball(A).Mass And ClsForce2 > (Ball(A).Mass) And Ball(A).Size > 1 And Ball(B).Size > 1 Then 'And InStr(1, Ball(A).Flags, "R") = 0 And InStr(1, Ball(A).Flags, "S") = 0
    '                                                                                              ' Debug.Print(Ball(B).Mass)
    '                                                                                              FractureBall(A)
    '                                                                                              FractureBall(B)
    '                                                                                              '  GoTo here
    '                                                                                          Else
    '                                                                                              If InStr(1, Ball(B).Flags, "R") > 0 And Force < (Ball(A).Mass ^ 3) Or InStr(1, Ball(B).Flags, "R") = 0 Then
    '                                                                                                  Dim Area1 As Double, Area2 As Double
    '                                                                                                  If Ball(A).Mass > Ball(B).Mass Then
    '                                                                                                      ' If Ball(B).Origin <> A Then
    '                                                                                                      Ball(A).Flags = Replace$(Ball(A).Flags, "R", "")
    '                                                                                                          Ball(A).SpeedX = Ball(A).SpeedX + (U1 - V1) * VekX
    '                                                                                                          Ball(A).SpeedY = Ball(A).SpeedY + (U1 - V1) * VeKY
    '                                                                                                          Area1 = PI * (Ball(A).Size ^ 2)
    '                                                                                                          Area2 = PI * (Ball(B).Size ^ 2)
    '                                                                                                          Area1 = Area1 + Area2
    '                                                                                                          Ball(A).Size = Sqrt(Area1 / PI)
    '                                                                                                          Ball(A).Mass = Ball(A).Mass + Ball(B).Mass 'Sqr(Ball(B).Mass)
    '                                                                                                          Ball(B).Visible = False
    '                                                                                                      ' End If
    '                                                                                                  Else
    '                                                                                                      '  If Ball(A).Origin <> B Then
    '                                                                                                      Ball(A).Flags = Replace$(Ball(A).Flags, "R", "")
    '                                                                                                          Ball(B).SpeedX = Ball(B).SpeedX + (U2 - V2) * VekX
    '                                                                                                          Ball(B).SpeedY = Ball(B).SpeedY + (U2 - V2) * VeKY
    '                                                                                                          Area1 = PI * (Ball(B).Size ^ 2)
    '                                                                                                          Area2 = PI * (Ball(A).Size ^ 2)
    '                                                                                                          Area1 = Area1 + Area2
    '                                                                                                          Ball(B).Size = Sqrt(Area1 / PI)
    '                                                                                                          Ball(B).Mass = Ball(B).Mass + Ball(A).Mass 'Sqr(Ball(A).Mass)
    '                                                                                                          Ball(A).Visible = False
    '                                                                                                      '   End If
    '                                                                                                  End If
    '                                                                                              End If
    '                                                                                              If Ball(A).Mass > 350 Then Ball(A).Color = System.Drawing.Color.Red
    '                                                                                              If Ball(A).Mass > 400 Then Ball(A).Color = System.Drawing.Color.Yellow
    '                                                                                              If Ball(A).Mass > 500 Then Ball(A).Color = System.Drawing.Color.White
    '                                                                                              If Ball(A).Mass > 600 Then Ball(A).Color = System.Drawing.Color.LightCyan
    '                                                                                              If Ball(A).Mass > 700 Then Ball(A).Color = System.Drawing.Color.LightBlue
    '                                                                                              'If Ball(A).Mass > 1000 Then
    '                                                                                              '    Ball(A).Color = System.Drawing.Color.Black
    '                                                                                              '    Ball(A).Size = 15
    '                                                                                              '    If InStr(1, Ball(A).Flags, "BH") = 0 Then Ball(A).Flags = Ball(A).Flags + "BH"
    '                                                                                              'End If
    '                                                                                              bolBallsRemoved = True
    '                                                                                          End If
    '                                                                                      End If
    '                                                                                  End If
    '                                                                              End If
    '                                                                              '  If Ball(A).Mass > 350 And Ball(A).Visible = True Then 'solar wind
    '                                                                              'If InStr(Ball(A).Flags, "S") = 0 Then Ball(A).Flags = Ball(A).Flags + "S"
    '                                                                              'rc = (Ball(B).Size / 4) + (Ball(A).Size / 4)
    '                                                                              'ry = (Ball(B).LocY - Ball(A).LocY) / 2
    '                                                                              'rx = (Ball(B).LocX - Ball(A).LocX) / 2
    '                                                                              'd = Sqrt(rx * rx + ry * ry)
    '                                                                              'If d < 500 Then
    '                                                                              '    Dim m As Double, SlX As Double, SlY As Double
    '                                                                              '    Dim VecX As Double, VecY As Double
    '                                                                              '    Dim C As Double, S As Double
    '                                                                              '    Dim Dis As Double, DisSqr As Double, F As Double, Lx As Double, Ly As Double
    '                                                                              '    SlX = Ball(B).LocX - Ball(A).LocX
    '                                                                              '    SlY = Ball(B).LocY - Ball(A).LocY
    '                                                                              '    m = SlY / SlX
    '                                                                              '    a = Math.Atan2(Ball(B).LocY - Ball(A).LocY, Ball(B).LocX - Ball(A).LocX)
    '                                                                              '    C = Cos(a)
    '                                                                              '    S = Sin(a)
    '                                                                              '    VecX = (Ball(B).LocX + Ball(B).Size * C) - Ball(B).LocX
    '                                                                              '    VecY = (Ball(B).LocY + Ball(B).Size * S) - Ball(B).LocY
    '                                                                              '    Lx = Ball(B).LocX - Ball(A).LocX
    '                                                                              '    Ly = Ball(B).LocY - Ball(A).LocY
    '                                                                              '    Dis = (Lx * Lx) + (Ly * Ly)
    '                                                                              '    DisSqr = Sqrt(Dis)
    '                                                                              '    F = (Ball(A).Mass ^ 2) / (Dis * DisSqr)
    '                                                                              '    Ball(B).SpeedX = Ball(B).SpeedX + F * VecX
    '                                                                              '    Ball(B).SpeedY = Ball(B).SpeedY + F * VecY
    '                                                                              'End If
    '                                                                              '   End If
    '                                                                          End If
    '                                                                      End If
    '                                                                  Next B
    '                                                              End If
    '                                                          End If
    '                                                          Ball(A).LocX = Ball(A).LocX + (Ball(A).SpeedX * StepMulti)
    '                                                          Ball(A).LocY = Ball(A).LocY + (Ball(A).SpeedY * StepMulti)
    '                                                      End SyncLock
    '                                                  End Sub)
    '                '      Next A
    '            End If
    '            Application.DoEvents()
    '            If UBound(Ball) > 5000 Then
    '                s = New Threading.Thread(AddressOf Form1.ShrinkBallArray)
    '                s.Start()
    '                s.Join()
    '            End If
    '            If bolFollow Then
    '                If Ball(lngFollowBall).LocX <> FollowX Or Ball(lngFollowBall).LocY <> FollowY Then
    '                    RelBallPosMod.X = -Ball(lngFollowBall).LocX
    '                    RelBallPosMod.Y = -Ball(lngFollowBall).LocY
    '                End If
    '            End If
    '            'If s.ThreadState <> Threading.ThreadState.Running And Form1.chkDraw.Checked Then
    '            '    Form1.Render.Image = Drawr()
    '            'End If
    '            FPS = FPS + 1
    '        Loop
    '    End Sub
    Public Function RandomRGBColor() As Color
        Dim m_Rnd As New Random
        Return Color.FromArgb(255, objRandom.Next(0, 255), objRandom.Next(0, 255), objRandom.Next(0, 255))
    End Function
    Public Function fnMass(Radius As Double) As Double


        Return Sqrt(PI * (Radius ^ 2)) * Density '^ 2
    End Function
    Private myRandom As New Random
    Public Function GetRandomNumber(ByVal Low As Double, ByVal High As Double) As Double
        ' Returns a random number,
        ' between the optional Low and High parameters

        'Dim number As Double = myRandom.NextDouble() * (High - Low) + Low
        'Return number


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

    Private Function DupLoc(LstBodies As List(Of BallParms), Body As BallParms) As Boolean
        If LstBodies.Count < 1 Then Return False
        For Each bdy As BallParms In LstBodies
            If Body.LocX = bdy.LocX And Body.LocY = bdy.LocY Then Return True
        Next

        Return False
    End Function
End Module
