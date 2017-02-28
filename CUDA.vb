﻿Imports Cudafy
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
        Public Color As Integer
        Public Size As Double
        Public Visible As Integer
        Public InRoche As Integer

        '   Public WhoTouchedMe As Integer




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
            Dim inBall() As Prim_Struct = CopyToPrim(Ball)
            ' Dim outBall() As Prim_Struct
            'Dim chunk As New PhysicsChunk(UBound(Ball), 0, Ball)
            '  Dim gMemIn, gMemOut
            ' gMemIn = gpu.Allocate(inBall) 'Of Single)(mb)
            gpu.Allocate(inBall) 'Of Single)(mb)



            '     Dim outBall() As Prim_Struct = inBall
            ' Dim cMemout() As Prim_Struct

            Dim gpuinBall() As Prim_Struct = gpu.CopyToDevice(inBall)

            ' gpu.Allocate(outBall)

            ' Dim gpuOutBall() As Prim_Struct = gpu.CopyToDevice(outBall)
            'Dim N As Integer = 20

            Dim threads As Integer = 256




            '  Dim DBVar(threads) As Debug_Struct
            'gpu.Allocate(DBVar)
            ' Dim OutDBVar() As Debug_Struct = gpu.CopyToDevice(DBVar)

            Dim nBlocks As Integer = (UBound(Ball) + threads - 1) / threads
            StartTimer()

            gpu.Launch(nBlocks, threads, "CalcPhysics", gpuinBall, UBound(Ball)) ', OutDBVar)
            StopTimer()


            gpu.Synchronize()


            ' Dim bal

            gpu.CopyFromDevice(gpuinBall, inBall)

            '   gpu.CopyFromDevice(gpuOutBall, outBall)
            '   gpu.CopyFromDevice(OutDBVar, DBVar)


            ' Ball = outBall

            ' Debug.Print(inBall(0).ForceX)



            For i As Integer = 1 To UBound(inBall)
                inBall(i).SpeedX += StepMulti * inBall(i).ForceX / inBall(i).Mass
                inBall(i).SpeedY += StepMulti * inBall(i).ForceY / inBall(i).Mass
                inBall(i).LocX += StepMulti * inBall(i).SpeedX
                inBall(i).LocY += StepMulti * inBall(i).SpeedY

            Next



            Ball = CopyToBallParm(inBall)

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

    <Cudafy>
    Public Sub CalcPhysics(gpThread As GThread, Body() As Prim_Struct, nBodies As Integer) ', DebugStuff() As Debug_Struct) ', LB As Integer, UB As Integer)
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
        Dim MyStep As Double = 0.05

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
        Body(A).ForceX = 0
        Body(A).ForceY = 0
        '   Body(A).ForceTot = 0
        '  If OuterBody(A).Visible Then
        '  If OuterBody(A).MovinG = False Then

        If A < nBodies Then
            For B = 1 To nBodies
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
        'If OuterBody(A).ForceTot > OuterBody(A).Mass * 4 And Not OuterBody(A).Flags.Contains("BH") Then ' And OuterBody(A).Size < 10 
        '    OuterBody(A).InRoche = True
        '    NewBalls.AddRange(FractureBall(OuterBody(A)))
        'ElseIf (OuterBody(A).ForceTot * 2) < OuterBody(A).Mass * 4 Then ' And OuterBody(A).Size > 10
        '    OuterBody(A).InRoche = False

        'End If
        '    DistArray.Add(A.ToString + " - " + DistSqrt.ToString)

        ' Next A
        ' End If

        ''    ShrinkBallArray()
        ''If UBound(Ball) > 10000 Then
        ''End If
        ' UpdateBodies(Body)

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
    Private Sub CollideBodies(ByRef Master As Prim_Struct, ByRef Slave As Prim_Struct, DistSqrt As Double, DistX As Double, DistY As Double, ForceX As Double, ForceY As Double)
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

        'DistX = Slave.LocX - Master.LocX
        'DistY = Slave.LocY - Master.LocY
        'Dist = (DistX * DistX) + (DistY * DistY)
        'DistSqrt = Sqrt(Dist)
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

                'TotMass = Master.Mass * Slave.Mass
                'Force = TotMass / (DistSqrt * DistSqrt + EPS * EPS)
                'ForceX = Force * DistX / DistSqrt
                'ForceY = Force * DistY / DistSqrt
                Dim multi As Integer = 20
                Master.ForceX -= ForceX * multi
                Master.ForceY -= ForceY * multi
                Slave.ForceX -= ForceX * multi
                Slave.ForceY -= ForceY * multi


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
