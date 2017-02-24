Imports Cudafy
Imports Cudafy.Host
Imports Cudafy.Translator
Imports System
Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports System.Math

'<Cudafy>
Public Module CUDA
    Public Const ThreadsPerBlock As Integer = 256
    Public Const Blocks = 1024 * 1024 / ThreadsPerBlock
    Public gpu As GPGPU
    ' Private dBall() As Prim_Struct

    Public Structure FoundGpu
        Public GPUType As eGPUType
        Public Index As Integer
    End Structure
    <Cudafy>
    Public Structure Prim_Struct
        Public LocX As Double
        Public LocY As Double
        Public Mass As Double
        Public SpeedX As Double
        Public SpeedY As Double
        Public ForceX As Double
        Public ForceY As Double





    End Structure

    Public Sub InitGPU()
        Dim GPUIndex As Integer = 3

        Dim CUDAmodule As CudafyModule

        gpu = CudafyHost.GetDevice(eGPUType.OpenCL, GPUIndex)
        CudafyTranslator.Language = eLanguage.OpenCL
        CUDAmodule = CudafyTranslator.Cudafy()
        gpu.LoadModule(CUDAmodule)

        '  System.IO.File.WriteAllText("OpenCl_" + GPUIndex + ".cpp", CUDAmodule.CudaSourceCode)


    End Sub

    Public ThreadNum As Integer ' = 1
    Public StartTick, EndTick, ElapTick As Long
    Public SeekIndex As Integer = 0
    Public Prev_SeekIndex As Integer = 0
    ' <Cudafy>
    Public Sub StartCalc()

        '  Try
        InitGPU()

        Dim BodyDiv As Integer


        Dim RunThreads As Integer
        ' Dim PlayArray() As BallParms
        ' Do Until bolStopWorker

        bolRendering = True
        '    If RunThreads <> ThreadNum Then RunThreads = ThreadNum

        'Do While bolStopLoop
        '    Thread.Sleep(100)
        'Loop
        'StartTick = Now.Ticks
        'Thread.Sleep(intDelay)

        ' ExecDelay()

        'Start loop
        'Calc Splits
        ' Ball = CullBodies(Ball)
        If Not bolPlaying Then
            'IF NOT PLAYING THEN RENDER NORMALLY*****************


            '    ' If UBound(Ball) > (VisibleBalls() * 2) 
            '    If (UBound(Ball) - VisibleBalls()) > 1000 Then
            '        Ball = CullBodies(Ball)
            '    End If

            '' BodyDiv = Int(UBound(Ball) / RunThreads)
            'Dim ExtraBodys As Integer = UBound(Ball) - (BodyDiv * RunThreads)
            '    ' Debug.Print(ExtraBodys)
            '    Dim Threads As New List(Of PhysicsChunk)
            '    Dim UB, LB As Integer
            'For i As Integer = 1 To RunThreads
            '    If i = 1 Then
            '        Threads.Add(New PhysicsChunk(BodyDiv, 0, Ball))
            '    Else
            '        LB = (BodyDiv * (i - 1)) + 1
            '        UB = BodyDiv * (i)
            '        If i = RunThreads Then UB += ExtraBodys
            '        Threads.Add(New PhysicsChunk(UB, LB, Ball))
            '    End If
            'Next

            'Dim rThreads As New List(Of Thread)
            'For Each trd As PhysicsChunk In Threads
            '    rThreads.Add(New Thread(New ThreadStart(AddressOf trd.CalcPhysics)))
            'Next



            'For Each rtrd As Thread In rThreads
            '    rtrd.Start()
            'Next

            'Dim bolThreadsDone As Boolean = False
            'Do Until bolThreadsDone 'rThread1.ThreadState = ThreadState.Stopped And rThread2.ThreadState = ThreadState.Stopped And rThread3.ThreadState = ThreadState.Stopped And rThread4.ThreadState = ThreadState.Stopped And rThread5.ThreadState = ThreadState.Stopped And rThread6.ThreadState = ThreadState.Stopped And rThread7.ThreadState = ThreadState.Stopped And rThread8.ThreadState = ThreadState.Stopped
            '    Thread.Sleep(1)
            '    Dim CompleteThreads As Integer = 0
            '    For Each rtrd As Thread In rThreads
            '        If rtrd.ThreadState = ThreadState.Stopped Then CompleteThreads += 1
            '    Next
            '    If CompleteThreads = RunThreads Then bolThreadsDone = True
            'Loop
            Dim mb As Integer = 1024 * 1024
            Dim inBall() As Prim_Struct = CopyToPrim(Ball)
            'Dim chunk As New PhysicsChunk(UBound(Ball), 0, Ball)
            Dim gMemIn, gMemOut
            ' gMemIn = gpu.Allocate(inBall) 'Of Single)(mb)
            gMemOut = gpu.Allocate(inBall) 'Of Single)(mb)
            Dim outBall(inBall.Count - 1) As Prim_Struct
            ' Dim cMemout() As Prim_Struct

            Dim gpuinBall() As Prim_Struct = gpu.CopyToDevice(inBall)
            gpu.Launch(1, 1, "CalcPhysics", gpuinBall, UBound(Ball))
            gpu.Synchronize()


            ' Dim bal
            gpu.CopyFromDevice(gpuinBall, inBall)


            ' Ball = outBall


            Ball = CopyToBallParm(inBall)

            '  Dim AllBodys As New List(Of BallParms)
            'For Each trd As PhysicsChunk In Threads
            '    AllBodys.AddRange(trd.MyBodys)
            'Next

            '  Ball = AllBodys.ToArray
            '  Debug.Print(UBound(Ball))

            'If bolStoring Then
            '    '  Dim BodyFrame
            '    'RecordedBodies.Add(Ball)
            '    '   StartTimer()
            '    RecordFrame(Ball)
            '    ' StopTimer()
            '    'Using s As Stream = New MemoryStream()

            '    '    ' Dim formatter As ProtoBuf.Serializer 'System.Runtime.Serialization.Formatters.Binary.BinaryFormatter

            '    '    ProtoBuf.Serializer.Serialize(s, RecordedBodies.Item(1))
            '    '    Debug.Print(RecordedBodies.Count & " - " & s.Length / 1024)
            '    'End Using
            'End If

            ' PhysicsWorker.ReportProgress(1, Ball)

            'ElseIf bolPlaying Then
            '    'ELSE IF IS PLAYING THEN CYCLE REPLAY

            '    Dim PlayArray()() As Body_Rec_Parms = CompRecBodies.ToArray 'BallParms = RecordedBodies.ToArray



            '    For i = 0 To UBound(PlayArray(1)) 'Each b() As BallParms In RecordedBodies
            '        ExecDelay()
            '        If SeekIndex <> Prev_SeekIndex Then
            '            i = SeekIndex
            '            Prev_SeekIndex = SeekIndex
            '        End If
            '        Ball = ConvertFrame(PlayArray(i))

            '        PhysicsWorker.ReportProgress(i, Ball)

            '        CalcDelay()
            '        bolRendering = False
            '    Next i


            'For Each b() As BallParms In RecordedBodies
            '    ExecDelay()
            '    Ball = b

            '    PhysicsWorker.ReportProgress(RecordedBodies.IndexOf(b), Ball)

            '    CalcDelay()
            'Next
        End If

        'END PLAYING CONDITION




        'End loop
        'EndTick = Now.Ticks
        'ElapTick = EndTick - StartTick
        'RenderTime = ElapTick / 10000
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
        '  CalcDelay()



        bolRendering = False
        Drawr(Ball)

        '  Loop



        'Catch ex As Exception
        '    Debug.Print(ex.Message)
        'End Try

    End Sub
    Private Function CopyToPrim(BallArr() As BallParms) As Prim_Struct()
        Dim prim_ball(BallArr.Count - 1) As Prim_Struct
        For i = 0 To UBound(BallArr)
            prim_ball(i).ForceX = BallArr(i).ForceX
            prim_ball(i).ForceY = BallArr(i).ForceY
            prim_ball(i).LocX = BallArr(i).LocX
            prim_ball(i).LocY = BallArr(i).LocY
            prim_ball(i).Mass = BallArr(i).Mass
            prim_ball(i).SpeedX = BallArr(i).SpeedX
            prim_ball(i).SpeedY = BallArr(i).SpeedY





        Next


        Return prim_ball
    End Function
    Private Function CopyToBallParm(BallArr() As Prim_Struct) As BallParms()
        Dim BallPar(BallArr.Count - 1) As BallParms
        For i = 0 To UBound(BallArr)
            BallPar(i).ForceX = BallArr(i).ForceX
            BallPar(i).ForceY = BallArr(i).ForceY
            BallPar(i).LocX = BallArr(i).LocX
            BallPar(i).LocY = BallArr(i).LocY
            BallPar(i).Mass = BallArr(i).Mass
            BallPar(i).SpeedX = BallArr(i).SpeedX
            BallPar(i).SpeedY = BallArr(i).SpeedY
            BallPar(i).Visible = True
            BallPar(i).Size = 3
            BallPar(i).Color = Color.Orange



        Next


        Return BallPar
    End Function
    <Cudafy>
    Public Sub CalcPhysics(Body() As Prim_Struct, nBodies As Integer)

        ' Dim Body() As Prim_Struct = dball
        Dim TotMass As Double
        Dim Force As Double
        Dim ForceX As Double
        Dim ForceY As Double
        Dim DistX As Double
        Dim DistY As Double
        Dim Dist As Double
        Dim DistSqrt As Double
        Dim M1, M2 As Double
        Dim MyStep As Double = 0.03

        '   Dim tmpBodys As New List(Of BallParms)
        '  Dim DistArray As New List(Of String)

        ' Do Until bolStopWorker
        Dim EPS As Double = 1.02
        ' Dim OuterBody As BallParms() = MyBodys.ToArray

        'Do While bolStopLoop
        ' Thread.Sleep(100)
        'Loop
        '  StartTick = Now.Ticks
        '  Thread.Sleep(intDelay)
        '  StartTimer()
        '  If UBound(OuterBody) > 1 Then
        ' BUB = uBoundBody
        Dim UB As Integer = nBodies 'Body.Count - 1
        For A = 1 To UB ' Each OuterBody(A) As BallParms In MyBodys 'A = lBoundBody To uBoundBody
            Body(A).ForceX = 0
            Body(A).ForceY = 0
            '   Body(A).ForceTot = 0
            '  If OuterBody(A).Visible Then
            '  If OuterBody(A).MovinG = False Then
            For B = 1 To UB
                If A <> B Then
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
                    'If Body(B).LocX = Body(A).LocX And Body(B).LocY = Body(A).LocY Then
                    '    '  CollideBodies(OuterBody(A), Bodys(B))
                    'End If
                    'If bGrav = 0 Then
                    'Else

                    DistX = Body(B).LocX - Body(A).LocX
                    DistY = Body(B).LocY - Body(A).LocY
                    Dist = (DistX * DistX) + (DistY * DistY)
                    DistSqrt = Sqrt(Dist)
                    If DistSqrt > 0 Then 'Gravity reaction
                        '   If DistSqrt < (OuterBody(A).Size / 2) + (Bodys(B).Size / 2) Then DistSqrt = (OuterBody(A).Size / 2) + (Bodys(B).Size / 2) 'prevent screamers
                        M1 = Body(A).Mass '^ 2
                        M2 = Body(B).Mass ' ^ 2
                        TotMass = M1 * M2
                        Force = TotMass / (DistSqrt * DistSqrt + EPS * EPS) ' (Dist * DistSqrt)


                        ForceX = Force * DistX / DistSqrt
                        ForceY = Force * DistY / DistSqrt
                        ' Body(A).ForceTot += Force


                        Body(A).ForceX += ForceX
                        Body(A).ForceY += ForceY

                        'OuterBody(A).SpeedX += MyStep * ForceX / M1
                        'OuterBody(A).SpeedY += MyStep * ForceY / M1

                        'If DistSqrt < 100 Then


                        '        If DistSqrt <= (OuterBody(A).Size / 2) + (Bodys(B).Size / 2) Then 'Collision reaction



                        '            CollideBodies(OuterBody(A), Bodys(B), DistSqrt, DistX, DistY, ForceX, ForceY)

                        '        End If
                        '    End If
                    Else
                    End If
                    '  End If
                End If
                ' UpdateBody(OuterBody(A))


            Next B
            '  End If
            ' End If

            'OuterBody(A).LocX = OuterBody(A).LocX + (MyStep * OuterBody(A).SpeedX)
            'OuterBody(A).LocY = OuterBody(A).LocY + (MyStep * OuterBody(A).SpeedY)
            ' tmpBodys.Add(OuterBody(A))
            'CalcColor = ColorsRGB - (OuterBody(A).ForceTot * RGBMulti)
            'If OuterBody(A).ForceTot > OuterBody(A).Mass * 4 And Not OuterBody(A).Flags.Contains("BH") Then ' And OuterBody(A).Size < 10 
            '    OuterBody(A).InRoche = True
            '    NewBalls.AddRange(FractureBall(OuterBody(A)))
            'ElseIf (OuterBody(A).ForceTot * 2) < OuterBody(A).Mass * 4 Then ' And OuterBody(A).Size > 10
            '    OuterBody(A).InRoche = False

            'End If
            '    DistArray.Add(A.ToString + " - " + DistSqrt.ToString)

        Next A
        ' End If

        ''    ShrinkBallArray()
        ''If UBound(Ball) > 10000 Then
        ''End If
        ' UpdateBodies(Body)

        For i As Integer = 0 To nBodies 'UBound(Bodies)
            Body(i).SpeedX += MyStep * Body(i).ForceX / Body(i).Mass
            Body(i).SpeedY += MyStep * Body(i).ForceY / Body(i).Mass
            Body(i).LocX += MyStep * Body(i).SpeedX
            Body(i).LocY += MyStep * Body(i).SpeedY

        Next






        'result = Body


        'dball = Body
        '   BodyOut = Body
        'If NewBalls.Count > 0 Then
        '    MyBodys.Clear()
        '    MyBodys.AddRange(OuterBody)

        '    MyBodys.AddRange(NewBalls)
        '    '  OuterBody = MyBodys.ToArray
        '    'AddNewBalls(NewBalls)
        '    '  tmpBodys.AddRange(NewBalls)
        'Else
        '    MyBodys.Clear()
        '    MyBodys.AddRange(OuterBody)
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

        '  MyBodys = OuterBody 'tmpBodys
    End Sub
    '<Cudafy>
    'Private Function UpdateBodies(Bodies() As Prim_Struct) As Prim_Struct()
    '    Dim MyStep As Double = 0.03
    '    ' Dim Bodies() As Prim_Struct = dBall
    '    For i As Integer = 0 To 499 'UBound(Bodies)
    '        Bodies(i).SpeedX += MyStep * Bodies(i).ForceX / Bodies(i).Mass
    '        Bodies(i).SpeedY += MyStep * Bodies(i).ForceY / Bodies(i).Mass
    '        Bodies(i).LocX += MyStep * Bodies(i).SpeedX
    '        Bodies(i).LocY += MyStep * Bodies(i).SpeedY

    '    Next
    '    Return Bodies

    'End Function


End Module
