Imports Cudafy
Imports Cudafy.Host
Imports Cudafy.Translator
Imports System
Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports System.Math
Imports System.Threading

'<Cudafy>
Public Module CUDA
    Public Const ThreadsPerBlock As Integer = 256
    Public Const Blocks = 1024 * 1024 / ThreadsPerBlock
    Public gpu As GPGPU
    Public bolLoopRunning As Boolean = False
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
        Public ForceTot As Double
        Public Color As Integer
        Public Size As Double
        Public Visible As Integer
        Public InRoche As Integer
        Public BlackHole As Integer
        Public UID As Long
        ' Public TimesTouched As Integer




    End Structure
    <Cudafy>
    Public Structure Debug_Struct
        Public UB As Integer
        Public LB As Integer
        Public Other As Integer
        Public Other2 As Integer

    End Structure

    Public Sub InitGPU()
        Dim GPUIndex As Integer = 2

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
        ' InitGPU()

        ' Dim PlayArray() As BallParms
        ' Do Until bolStopWorker

        Dim BodyDiv As Integer


            Dim RunThreads As Integer

        'bolLoopRunning = True
        bolRendering = True
        '    If RunThreads <> ThreadNum Then RunThreads = ThreadNum

        'Do While bolStopLoop
        '    Thread.Sleep(100)
        'Loop
        ' StartTick = Now.Ticks
        'Thread.Sleep(1000)

        'ExecDelay()

        'Start loop
        'Calc Splits
        ' Ball = CullBodies(Ball)
        If Not bolPlaying Then
#Region "OldShit2"
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
#End Region
            'Dim mb As Integer = 1024 * 1024

            If (UBound(Ball) - VisibleBalls()) > 1000 Then
                Ball = CullBodies(Ball)
            End If

            '  Dim inBall() As Prim_Struct = CopyToPrim(Ball)
            ' Dim outBall() As Prim_Struct
            'Dim chunk As New PhysicsChunk(UBound(Ball), 0, Ball)
            '  Dim gMemIn, gMemOut
            ' gMemIn = gpu.Allocate(inBall) 'Of Single)(mb)
            gpu.Allocate(Ball) 'Of Single)(mb)



            '     Dim outBall() As Prim_Struct = inBall
            ' Dim cMemout() As Prim_Struct

            Dim gpuinBall() As Prim_Struct = gpu.CopyToDevice(Ball)

            ' gpu.Allocate(outBall)

            ' Dim gpuOutBall() As Prim_Struct = gpu.CopyToDevice(outBall)
            'Dim N As Integer = 20

            Dim threads As Integer = 256




            '  Dim DBVar(threads) As Debug_Struct
            'gpu.Allocate(DBVar)
            ' Dim OutDBVar() As Debug_Struct = gpu.CopyToDevice(DBVar)

            Dim nBlocks As Integer = (UBound(Ball) + threads - 1) / threads
            ' StartTimer()

            gpu.Launch(nBlocks, threads, "CalcPhysics", gpuinBall, UBound(Ball), StepMulti) ', OutDBVar)
            '  StopTimer()


            gpu.Synchronize()


            ' Dim bal

            gpu.CopyFromDevice(gpuinBall, Ball)

            '   gpu.CopyFromDevice(gpuOutBall, outBall)
            '   gpu.CopyFromDevice(OutDBVar, DBVar)


            ' Ball = outBall

            ' Debug.Print(inBall(0).ForceX)


            Dim NewBalls As New List(Of Prim_Struct)
            For a As Integer = 1 To UBound(Ball)

                If Ball(a).ForceTot > Ball(a).Mass * 4 And Ball(a).BlackHole = 0 Then ' And OuterBody(A).Size < 10 
                    Ball(a).InRoche = 1

                    NewBalls.AddRange(FractureBall(Ball(a)))
                ElseIf (Ball(a).ForceTot * 2) < Ball(a).Mass * 4 Then ' And OuterBody(A).Size > 10
                    Ball(a).InRoche = 0

                End If


            Next

            'UpdateBodies(inBall)


            ' Dim MyBodys As New List(Of Prim_Struct)
            '  MyBodys.AddRange(inBall)
            If NewBalls.Count > 0 Then
                Dim origLen As Integer = UBound(Ball)
                Array.Resize(Ball, (origLen + NewBalls.Count + 1))
                Array.Copy(NewBalls.ToArray, 0, Ball, origLen + 1, (NewBalls.Count))

                'MyBodys.Clear()


                'MyBodys.AddRange(Ball)

                'MyBodys.AddRange(NewBalls)
                '  OuterBody = MyBodys.ToArray
                'AddNewBalls(NewBalls)
                '  tmpBodys.AddRange(NewBalls)
                ' Else
                '
            End If



            ' inBall = MyBodys.ToArray


            ' Ball = CopyToBallParm(MyBodys.ToArray)

            gpu.FreeAll()

#Region "OldShit1"
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


            '    Next i


            'For Each b() As BallParms In RecordedBodies
            '    ExecDelay()
            '    Ball = b

            '    PhysicsWorker.ReportProgress(RecordedBodies.IndexOf(b), Ball)

            '    CalcDelay()
            'Next
#End Region
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


        'CalcDelay()

        'bolRendering = False
        'If Not bolDrawing Then Drawr(Ball)
        '    Loop



        'Catch ex As Exception
        '    Debug.Print(ex.Message)
        'End Try

    End Sub
    Private Sub UpdateBodies(ByRef Body() As Prim_Struct)
        For i As Integer = 1 To UBound(Body)
            Body(i).SpeedX += StepMulti * Body(i).ForceX / Body(i).Mass
            Body(i).SpeedY += StepMulti * Body(i).ForceY / Body(i).Mass
            Body(i).LocX += StepMulti * Body(i).SpeedX
            Body(i).LocY += StepMulti * Body(i).SpeedY
        Next
    End Sub

    <Cudafy>
    Public Sub CalcPhysics(gpThread As GThread, Body() As Prim_Struct, nBodies As Integer, TimeStep As Double) ', DebugStuff() As Debug_Struct) ', LB As Integer, UB As Integer)
        'Dim STRIDE As Integer = 32
        'Dim elem_per_thread As Integer = nBodies / gpThread.blockIdx.x * gpThread.blockDim.x
        'Dim block_start_idx As Integer = elem_per_thread * gpThread.blockIdx.x * gpThread.blockDim.x
        'Dim thread_start_idx As Integer = block_start_idx + (gpThread.threadIdx.x / STRIDE) * elem_per_thread * STRIDE + ((gpThread.threadIdx.x + 0) Mod STRIDE)
        'Dim thread_end_idx = thread_start_idx + elem_per_thread * STRIDE
        'If thread_end_idx > nBodies Then thread_end_idx = nBodies


#Region "ThreadControl1"
        'Dim BodyDiv As Integer = nBodies / (RunThreads)
        'Dim ExtraBodys As Integer = nBodies - (BodyDiv * (RunThreads))
        ''  Debug.Print(ExtraBodys)
        '' Dim Threads As New List(Of PhysicsChunk)
        'Dim UB, LB As Integer


        'If gpThread.threadIdx.x = RunThreads Then Exit Sub
        '' For i As Integer = 1 To RunThreads
        '    Dim t As Integer = gpThread.threadIdx.x + 1
        'If t = 1 Then
        '    ' Threads.Add(New PhysicsChunk(BodyDiv, 0, Ball))
        '    LB = 1
        '    UB = BodyDiv
        'Else
        '    LB = (BodyDiv * (t - 1)) + 1
        '    UB = BodyDiv * (t)
        '    If t = RunThreads Then UB += ExtraBodys
        '    ' Threads.Add(New PhysicsChunk(UB, LB, Ball))
        'End If
        ''Next
#End Region



        Dim A As Integer = gpThread.blockDim.x * gpThread.blockIdx.x + gpThread.threadIdx.x




        '  Dim tid As Integer = gpThread.threadIdx.x
        'Dim blkid As Integer = gpThread.blockIdx.x

        ''DebugStuff(tid).LB = LB
        ''DebugStuff(tid).UB = UB
        'DebugStuff(tid).Other = tid
        'DebugStuff(tid).Other2 = blkid



        'Dim sharemem() As Prim_Struct = gpThread.AllocateShared(Of Prim_Struct)("sharemem", RunThreads)
        'gpThread.SyncThreads()
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
        ' Dim MyStep As Double = 0.05

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
        'Dim UB As Integer = thread_end_idx 'Body.Count - 1
        'Dim LB As Integer = thread_start_idx
        '   For A = LB To UB ' Each OuterBody(A) As BallParms In MyBodys 'A = lBoundBody To uBoundBody
        'Body(A).WhoTouchedMe = tid
        '  Body(A).TimesTouched += 1
        Body(A).ForceX = 0
        Body(A).ForceY = 0
        Body(A).ForceTot = 0
        '  If OuterBody(A).Visible Then
        '  If OuterBody(A).MovinG = False Then

        If A < nBodies And Body(A).Visible = 1 Then
            For B = 1 To nBodies
                If A <> B And Body(B).Visible = 1 Then
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
                        Body(A).ForceTot += Force


                        Body(A).ForceX += ForceX
                        Body(A).ForceY += ForceY

                        'OuterBody(A).SpeedX += MyStep * ForceX / M1
                        'OuterBody(A).SpeedY += MyStep * ForceY / M1

                        If DistSqrt < 100 Then


                            If DistSqrt <= (Body(A).Size / 2) + (Body(B).Size / 2) Then 'Collision reaction



                                CollideBodies(Body, A, B, DistSqrt, DistX, DistY, ForceX, ForceY)

                            End If
                        End If
                    Else
                    End If
                    '  End If
                End If
                ' UpdateBody(OuterBody(A))







            Next B

            'Body(A).SpeedX += MyStep * Body(A).ForceX / Body(A).Mass
            'Body(A).SpeedY += MyStep * Body(A).ForceY / Body(A).Mass
            'Body(A).LocX += MyStep * Body(A).SpeedX
            'Body(A).LocY += MyStep * Body(A).SpeedY


        End If

        '  End If
        ' End If

        'OuterBody(A).LocX = OuterBody(A).LocX + (MyStep * OuterBody(A).SpeedX)
        'OuterBody(A).LocY = OuterBody(A).LocY + (MyStep * OuterBody(A).SpeedY)
        ' tmpBodys.Add(OuterBody(A))
        'CalcColor = ColorsRGB - (OuterBody(A).ForceTot * RGBMulti)
        ''If Body(A).ForceTot > Body(A).Mass * 4 And Body(A).BlackHole = 0 Then ' And OuterBody(A).Size < 10 
        ''    Body(A).InRoche = True
        ''    NewBalls.AddRange(FractureBall(Body(A)))
        ''ElseIf (Body(A).ForceTot * 2) < Body(A).Mass * 4 Then ' And OuterBody(A).Size > 10
        ''    Body(A).InRoche = False

        ''End If
        '    DistArray.Add(A.ToString + " - " + DistSqrt.ToString)

        ' Next A
        ' End If

        ''    ShrinkBallArray()
        ''If UBound(Ball) > 10000 Then
        ''End If
        ' UpdateBodies(Body)

        Body(A).SpeedX += TimeStep * Body(A).ForceX / Body(A).Mass
        Body(A).SpeedY += TimeStep * Body(A).ForceY / Body(A).Mass
        Body(A).LocX += TimeStep * Body(A).SpeedX
        Body(A).LocY += TimeStep * Body(A).SpeedY

        '''''''For i As Integer = 1 To nBodies 'UBound(Bodies)
        '''''''    Body(i).SpeedX += MyStep * Body(i).ForceX / Body(i).Mass
        '''''''    Body(i).SpeedY += MyStep * Body(i).ForceY / Body(i).Mass
        '''''''    Body(i).LocX += MyStep * Body(i).SpeedX
        '''''''    Body(i).LocY += MyStep * Body(i).SpeedY

        '''''''Next



        'Body(1).Mass = 9999
        '  Body(1).Mass = gpThread.threadIdx.x 'UB 'gpThread.blockIdx.x
        'Body(2).Mass = 'LB 'gpThread.blockDim.x

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
    <Cudafy>
    Public Sub CollideBodies(Body() As Prim_Struct, Master As Integer, Slave As Integer, DistSqrt As Double, DistX As Double, DistY As Double, ForceX As Double, ForceY As Double) ' As Prim_Struct()
        Dim VeKY As Double
        Dim VekX As Double
        Dim V1x As Double
        Dim V2x As Double
        Dim M1 As Double
        Dim M2 As Double
        Dim V1y As Double
        Dim V2y As Double
        'Dim TotMass As Double
        ' Dim Force, ForceX, ForceY As Double
        Dim EPS As Double = 2

        ' Dim NewVelX1, NewVelY1, NewVelX2, NewVelY2 As Double


        Dim V1 As Double
        Dim V2 As Double
        Dim U2 As Double
        Dim U1 As Double
        'Dim DistX As Double
        'Dim DistY As Double
        'Dim Dist As Double
        ' Dim DistSqrt As Double
        Dim PrevSpdX, PrevSpdY As Double

        Dim Area1 As Double, Area2 As Double

        'DistX = Body(Slave).LocX - Body(Master).LocX
        'DistY = Body(Slave).LocY - Body(Master).LocY
        'Dist = (DistX * DistX) + (DistY * DistY)
        'DistSqrt = Sqrt(Dist) 
        ' Debug.Print("Col dist:" & DistSqrt) 
        If DistSqrt > 0 Then

            V1x = Body(Master).SpeedX
            V1y = Body(Master).SpeedY
            V2x = Body(Slave).SpeedX
            V2y = Body(Slave).SpeedY

            M1 = Body(Master).Mass
            M2 = Body(Slave).Mass

            VekX = DistX / 2 ' (Ball(A).LocX - Ball(B).LocX) / 2
            VeKY = DistY / 2 '(Ball(A).LocY - Ball(B).LocY) / 2

            VekX = VekX / (DistSqrt / 2) 'LenG
            VeKY = VeKY / (DistSqrt / 2) 'LenG

            V1 = VekX * V1x + VeKY * V1y
            V2 = VekX * V2x + VeKY * V2y

            U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)

            U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)
            ' If Not Body(Master).InRoche Then


            'If Body(Master).Mass <> Body(Slave).Mass Then
            If Body(Master).InRoche = 0 And Body(Slave).InRoche = 1 Then

                If Body(Master).Mass > Body(Slave).Mass Then


                    PrevSpdX = Body(Master).SpeedX
                    PrevSpdY = Body(Master).SpeedY

                    Body(Master).SpeedX = Body(Master).SpeedX + (U1 - V1) * VekX
                    Body(Master).SpeedY = Body(Master).SpeedY + (U1 - V1) * VeKY
                    Body(Slave).Visible = 0



                    Area1 = PI * (Body(Master).Size ^ 2)
                    Area2 = PI * (Body(Slave).Size ^ 2)
                    Area1 = Area1 + Area2
                    Body(Master).Size = Sqrt(Area1 / PI)
                    Body(Master).Mass = Body(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)

                ElseIf Body(Master).Mass = Body(Slave).Mass Then

                    If Body(Master).UID > Body(Slave).UID Then

                        PrevSpdX = Body(Master).SpeedX
                        PrevSpdY = Body(Master).SpeedY

                        Body(Master).SpeedX = Body(Master).SpeedX + (U1 - V1) * VekX
                        Body(Master).SpeedY = Body(Master).SpeedY + (U1 - V1) * VeKY
                        Body(Slave).Visible = 0



                        Area1 = PI * (Body(Master).Size ^ 2)
                        Area2 = PI * (Body(Slave).Size ^ 2)
                        Area1 = Area1 + Area2
                        Body(Master).Size = Sqrt(Area1 / PI)
                        Body(Master).Mass = Body(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)
                    Else
                        Body(Master).Visible = 0

                    End If




                    '  Body(Master).Visible = False


                End If
            ElseIf Body(Master).InRoche = 0 And Body(Slave).InRoche = 0 Then
                If Body(Master).Mass > Body(Slave).Mass Then

                    PrevSpdX = Body(Master).SpeedX
                    PrevSpdY = Body(Master).SpeedY

                    Body(Master).SpeedX = Body(Master).SpeedX + (U1 - V1) * VekX
                    Body(Master).SpeedY = Body(Master).SpeedY + (U1 - V1) * VeKY
                    Body(Slave).Visible = 0



                    Area1 = PI * (Body(Master).Size ^ 2)
                    Area2 = PI * (Body(Slave).Size ^ 2)
                    Area1 = Area1 + Area2
                    Body(Master).Size = Sqrt(Area1 / PI)
                    Body(Master).Mass = Body(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)
                ElseIf Body(Master).Mass = Body(Slave).Mass Then

                    '  If Body(Master).Index > Body(Slave).Index Then
                    If Body(Master).UID > Body(Slave).UID Then
                        'Debug.Print(UIDtoInt(Body(Master).UID).ToString)
                        'Debug.Print(UIDtoInt(Body(Slave).UID).ToString)
                        PrevSpdX = Body(Master).SpeedX
                        PrevSpdY = Body(Master).SpeedY

                        Body(Master).SpeedX = Body(Master).SpeedX + (U1 - V1) * VekX
                        Body(Master).SpeedY = Body(Master).SpeedY + (U1 - V1) * VeKY
                        Body(Slave).Visible = 0


                        Area1 = PI * (Body(Master).Size ^ 2)
                        Area2 = PI * (Body(Slave).Size ^ 2)
                        Area1 = Area1 + Area2
                        Body(Master).Size = Sqrt(Area1 / PI)
                        Body(Master).Mass = Body(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)
                    Else
                        Body(Master).Visible = 0

                    End If

                Else


                    Body(Master).Visible = 0

                End If


            ElseIf Body(Master).InRoche = 1 And Body(Slave).InRoche = 1 Then

                'TotMass = Body(Master).Mass * Body(Slave).Mass
                'Force = TotMass / (DistSqrt * DistSqrt + EPS * EPS)
                'ForceX = Force * DistX / DistSqrt
                'ForceY = Force * DistY / DistSqrt
                Dim multi As Integer = 20
                Body(Master).ForceX -= ForceX * multi
                Body(Master).ForceY -= ForceY * multi
                Body(Slave).ForceX -= ForceX * multi
                Body(Slave).ForceY -= ForceY * multi


                Dim Friction As Double = 0.8
                Body(Master).SpeedX += (U1 - V1) * VekX * Friction
                Body(Master).SpeedY += (U1 - V1) * VeKY * Friction


                Body(Slave).SpeedX += (U2 - V2) * VekX * Friction
                Body(Slave).SpeedY += (U2 - V2) * VeKY * Friction

            ElseIf Body(Master).InRoche = 1 And Body(Slave).InRoche = 0 Then

                Body(Master).Visible = 0





            End If

            ' End If





        Else ' if bodies are at exact same position

            'Body(Master).SpeedX = Body(Master).SpeedX + (U1 - V1) * VekX
            'Body(Master).SpeedY = Body(Master).SpeedY + (U1 - V1) * VeKY

            If Body(Master).Mass > Body(Slave).Mass Then
                Area1 = PI * (Body(Master).Size ^ 2)
                Area2 = PI * (Body(Slave).Size ^ 2)
                Area1 = Area1 + Area2
                Body(Master).Size = Sqrt(Area1 / PI)
                Body(Master).Mass = Body(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)
                Body(Slave).Visible = 0
            Else
                Area1 = PI * (Body(Master).Size ^ 2)
                Area2 = PI * (Body(Slave).Size ^ 2)
                Area1 = Area1 + Area2
                Body(Slave).Size = Sqrt(Area1 / PI)
                Body(Slave).Mass = Body(Slave).Mass + Body(Master).Mass 'Sqr(Ball(B).Mass)
                Body(Master).Visible = 0
            End If





        End If
        ' Return Body

    End Sub

    Private Function FractureBall(ByRef Body As Prim_Struct) As List(Of Prim_Struct)
        Dim NewBallSize As Single
        Dim NewBallMass As Single
        Dim Divisor As Single
        Dim PrevSize As Single
        Dim PrevMass As Single
        Dim TotBMass As Double
        Dim Area As Double
        Dim tmpBallList As New List(Of Prim_Struct)

        Dim RadUPX As Double, RadDNX As Double, RadUPY As Double, RadDNY As Double
        ' i = UBound(Ball)
        If Body.Visible = 1 And Body.Size > 1 Then
            Divisor = Int(Body.Size)
            If Divisor <= 1 Then Divisor = 2
            PrevSize = Body.Size
            PrevMass = Body.Mass
            'PrevSize = Sqr(PrevSize / pi)
            Area = PI * (Body.Size ^ 2)
            Area = Area / Divisor
            NewBallSize = fnRadius(Area)  'fnRadius(fnArea(Body.Size) / 2)  'Sqr(Area / pi) 'Body.Size / Divisor

            NewBallMass = PrevMass / Divisor  '(Body.Mass / Divisor)
            Body.Visible = 0
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
                Dim tmpBall As Prim_Struct
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
                tmpBall.BlackHole = 0
                '  tmpBall.Index = UBound(Ball) + 1
                tmpBall.UID = Now.Ticks 'Guid.NewGuid.ToString
                '  tmpBall.IsFragment = True
                tmpBall.InRoche = 1
                tmpBall.Visible = 1
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
    Private Function DupLoc(LstBodies As List(Of Prim_Struct), Body As Prim_Struct) As Boolean
        If LstBodies.Count < 1 Then Return False
        For Each bdy As Prim_Struct In LstBodies
            If Body.LocX = bdy.LocX And Body.LocY = bdy.LocY Then Return True
        Next

        Return False
    End Function

    Public Function CopyToPrim(BallArr() As BallParms) As Prim_Struct()
        Dim prim_ball(BallArr.Count - 1) As Prim_Struct
        For i = 0 To UBound(BallArr)
            prim_ball(i).ForceX = BallArr(i).ForceX
            prim_ball(i).ForceY = BallArr(i).ForceY
            prim_ball(i).LocX = BallArr(i).LocX
            prim_ball(i).LocY = BallArr(i).LocY
            prim_ball(i).Mass = BallArr(i).Mass
            prim_ball(i).SpeedX = BallArr(i).SpeedX
            prim_ball(i).SpeedY = BallArr(i).SpeedY
            prim_ball(i).Color = BallArr(i).Color.ToArgb
            prim_ball(i).Size = BallArr(i).Size
            prim_ball(i).Visible = Convert.ToInt32(BallArr(i).Visible)
            prim_ball(i).InRoche = Convert.ToInt32(BallArr(i).InRoche)
            prim_ball(i).UID = BallArr(i).UID
            prim_ball(i).ForceTot = BallArr(i).ForceTot
            If BallArr(i).Flags IsNot Nothing Then
                prim_ball(i).BlackHole = Convert.ToInt32(BallArr(i).Flags.Contains("BH"))
            Else
                prim_ball(i).BlackHole = 0
            End If

        Next


        Return prim_ball
    End Function
    Public Function CopyToBallParm(BallArr() As Prim_Struct) As BallParms()
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
            BallPar(i).Size = BallArr(i).Size
            BallPar(i).Color = Color.FromArgb(BallArr(i).Color)
            BallPar(i).Visible = Convert.ToBoolean(BallArr(i).Visible)
            BallPar(i).InRoche = Convert.ToBoolean(BallArr(i).InRoche)
            BallPar(i).UID = BallArr(i).UID
            If BallArr(i).BlackHole = 1 Then
                BallPar(i).Flags = "BH"
            Else
                BallPar(i).Flags = ""
            End If

        Next


        Return BallPar
    End Function

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
