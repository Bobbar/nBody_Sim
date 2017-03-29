Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Reflection
Module Functions




    Public bolTrails As Boolean = False
    Public bolDraw As Boolean = True
    Public bolLines As Boolean = False
    Public bolSOI As Boolean = False
    Public bolAntiAliasing As Boolean = True
    Public bolInvert As Boolean = False
    Public bolShawdow As Boolean = False
    Public bolStopWorker As Boolean = False
    Public bolRendering As Boolean
    Public bolStoring As Boolean
    Public bolPlaying As Boolean

    Public TrueFPS As Integer
    Public RenderTime As Double

    Public MatterTypes() As Matter_Props

    ' Public CUDATest As New CUDA

    Public stpw As New Stopwatch
    Public Function ScaleMousePosRelative(MousePos As SPoint) As SPoint
        Dim CorrectedPos As New SPoint((MousePos.X / pic_scale) - RelBallPosMod.X - ScaleOffset.X, (MousePos.Y / pic_scale) - RelBallPosMod.Y - ScaleOffset.Y)
        Return CorrectedPos
    End Function
    Public Function ScaleMousePosExact(MousePos As SPoint) As SPoint
        Dim CorrectedPos As New SPoint((MousePos.X / pic_scale), (MousePos.Y / pic_scale))
        Return CorrectedPos
    End Function
    Public Function GetDistance(PointA As SPoint, PointB As SPoint) As Single
        Return Math.Sqrt((PointA.X - PointB.X) ^ 2 + (PointA.Y - PointB.Y) ^ 2)
    End Function
    Public Function GetDistanceOfBalls(BallA As Prim_Struct, BallB As Prim_Struct) As Single
        Return Math.Sqrt((BallA.LocX - BallB.LocX) ^ 2 + (BallA.LocY - BallB.LocY) ^ 2)
    End Function
    Public Sub StartTimer()
        stpw.Stop()
        stpw.Reset()
        stpw.Start()
    End Sub
    Public Sub StopTimer()
        stpw.Stop()
        Debug.Print("Stopwatch: MS:" & stpw.ElapsedMilliseconds & " Ticks: " & stpw.ElapsedTicks)
    End Sub
    Public Sub SetColors(parent As Control)
        parent.BackColor = colBackColor
        For Each ctl As Control In parent.Controls
            If Not TypeOf ctl Is Label Then
                If TypeOf ctl Is Panel Then SetColors(ctl)
                ctl.ForeColor = colControlForeColor
                ctl.BackColor = colBackColor
            End If
        Next
    End Sub
    Public Function ObjectInsideTarget(Target_Loc As SPoint, Target_Size As Single, Object_Loc As SPoint) As Boolean
        Dim DistX As Double
        Dim DistY As Double
        Dim Dist As Double
        Dim DistSqrt As Double

        DistX = Target_Loc.X - Object_Loc.X
        DistY = Target_Loc.Y - Object_Loc.Y
        Dist = (DistX * DistX) + (DistY * DistY)
        DistSqrt = Sqrt(Dist)

        If DistSqrt <= Target_Size Then Return True
        Return False
    End Function

    Public Sub ExecDelay()
        Do While bolStopLoop
            Thread.Sleep(100)
        Loop
        StartTick = Now.Ticks
        '  intDelay = 200
        Thread.Sleep(intDelay)

    End Sub
    Public Sub CalcDelay()
        EndTick = Now.Ticks
        ElapTick = EndTick - StartTick
        RenderTime = ElapTick / 10000
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

    End Sub
    Public Function VisibleBalls() As Integer
        Dim tot As Integer = 0
        For i As Integer = 0 To UBound(Ball)

            If Ball(i).Visible = 1 And i <= UBound(Ball) Then tot += 1
        Next
        Return tot
        ' Return VisBalls
    End Function
    Public Function TotalMass() As Double
        Dim TotalM As Double
        For i As Integer = 0 To Ball.Length - 1
            If Ball(i).Visible = 1 Then
                TotalM += Ball(i).Mass
            End If
        Next
        Return TotalM
    End Function
    Public Function CullBodies(Bodies() As Prim_Struct) As Prim_Struct()
        ''    StartTimer()


        ''Dim tmpBodies(0) As Prim_Struct
        'Dim tmpBodies As New List(Of Prim_Struct)
        'For i As Integer = 0 To UBound(Bodies)
        '    If Bodies(i).Visible = 1 Then

        '        tmpBodies.Add(Bodies(i))
        '        'ReDim Preserve tmpBodies(UBound(tmpBodies) + 1)
        '        'tmpBodies(UBound(tmpBodies)) = Bodies(i)
        '    End If
        'Next
        ''  StopTimer()
        'Return tmpBodies.ToArray



        Dim tmpBodies(0) As Prim_Struct
        For i As Integer = 0 To Bodies.Length - 1
            If Bodies(i).Visible = 1 Then
                ReDim Preserve tmpBodies(tmpBodies.Length)
                tmpBodies(tmpBodies.Length - 1) = Bodies(i)

            End If




        Next


        Return tmpBodies

    End Function

    Public Function RndIntUID(BdyIndex As Integer) As Integer
        Dim rnd As Random = New Random
        Return rnd.Next + BdyIndex
    End Function
    Public Function CUDAToSerial(Bodies() As Prim_Struct) As Serial_Prim_Struct()

        Dim SerialBody(Bodies.Length - 1) As Serial_Prim_Struct
        For i As Integer = 0 To Bodies.Length - 1

            SerialBody(i).LocX = Bodies(i).LocX
            SerialBody(i).LocY = Bodies(i).LocY
            SerialBody(i).Mass = Bodies(i).Mass
            SerialBody(i).SpeedX = Bodies(i).SpeedX
            SerialBody(i).SpeedY = Bodies(i).SpeedY
            SerialBody(i).ForceX = Bodies(i).ForceX
            SerialBody(i).ForceY = Bodies(i).ForceY
            SerialBody(i).ForceTot = Bodies(i).ForceTot
            SerialBody(i).Color = Bodies(i).Color
            SerialBody(i).Size = Bodies(i).Size
            SerialBody(i).Visible = Bodies(i).Visible
            SerialBody(i).InRoche = Bodies(i).InRoche
            SerialBody(i).BlackHole = Bodies(i).BlackHole
            SerialBody(i).UID = Bodies(i).UID
            ' SerialBody(i).DT = Bodies(i).DT
            'SerialBody(i).ThreadID = Bodies(i).ThreadID
            'SerialBody(i).BlockID = Bodies(i).BlockID
            'SerialBody(i).BlockDIM = Bodies(i).BlockDIM
            'SerialBody(i).LastColID = Bodies(i).LastColID


        Next

        Return SerialBody

    End Function
    Public Function SerialToCUDA(Bodies() As Serial_Prim_Struct) As Prim_Struct()

        Dim CUDABody(Bodies.Length - 1) As Prim_Struct
        For i As Integer = 0 To Bodies.Length - 1

            CUDABody(i).LocX = Bodies(i).LocX
            CUDABody(i).LocY = Bodies(i).LocY
            CUDABody(i).Mass = Bodies(i).Mass
            CUDABody(i).SpeedX = Bodies(i).SpeedX
            CUDABody(i).SpeedY = Bodies(i).SpeedY
            CUDABody(i).ForceX = Bodies(i).ForceX
            CUDABody(i).ForceY = Bodies(i).ForceY
            CUDABody(i).ForceTot = Bodies(i).ForceTot
            CUDABody(i).Color = Bodies(i).Color
            CUDABody(i).Size = Bodies(i).Size
            CUDABody(i).Visible = Bodies(i).Visible
            CUDABody(i).InRoche = Bodies(i).InRoche
            CUDABody(i).BlackHole = Bodies(i).BlackHole
            CUDABody(i).UID = Bodies(i).UID
            'CUDABody(i).DT = Bodies(i).DT
            'CUDABody(i).ThreadID = Bodies(i).ThreadID
            'CUDABody(i).BlockID = Bodies(i).BlockID
            'CUDABody(i).BlockDIM = Bodies(i).BlockDIM
            'CUDABody(i).LastColID = Bodies(i).LastColID

        Next

        Return CUDABody

    End Function




    Public Structure Matter_Props
        Public Density As Integer
        Public Color As Color



    End Structure

    Public Sub InitMatter()
        ReDim MatterTypes(4)
        '  ReDim Preserve MatterTypes(UBound(MatterTypes) + 1)
        MatterTypes(0).Density = 1
        MatterTypes(0).Color = Color.Aqua 'gas


        '  ReDim Preserve MatterTypes(UBound(MatterTypes) + 1)
        MatterTypes(1).Density = 10
        MatterTypes(1).Color = Color.DodgerBlue 'liquid

        ' ReDim Preserve MatterTypes(UBound(MatterTypes) + 1)
        MatterTypes(2).Density = 20
        MatterTypes(2).Color = Color.Goldenrod 'rock

        'ReDim Preserve MatterTypes(UBound(MatterTypes) + 1)
        MatterTypes(3).Density = 30
        MatterTypes(3).Color = Color.SaddleBrown 'metal

        MatterTypes(4).Density = 60
        MatterTypes(4).Color = Color.DarkGray 'heavy metal


    End Sub




End Module
