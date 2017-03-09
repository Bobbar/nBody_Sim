Imports Cudafy
Imports Cudafy.Host
Imports Cudafy.Translator
Imports Cudafy.Atomics
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
    Public VisBalls As Integer
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
        Public ThreadID As Integer
        Public BlockID As Integer
        Public BlockDIM As Integer
        Public LastColID As Integer
        Public Counter As UInteger
    End Structure
    '<Cudafy>
    Public Ball() As Prim_Struct 'BallParms


    '<Cudafy>
    'Public Structure Debug_Struct
    '    Public UB As Integer
    '    Public LB As Integer
    '    Public Other As Integer
    '    Public Other2 As Integer

    'End Structure

    Public Sub InitGPU()
        Dim GPUIndex As Integer = 2 '2

        Dim CUDAmodule As CudafyModule

        gpu = CudafyHost.GetDevice(eGPUType.OpenCL, GPUIndex) 'eGPUType.Cuda, GPUIndex)
        CudafyTranslator.Language = eLanguage.OpenCL 'eLanguage.Cuda 
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
            VisBalls = UBound(Ball)
            '  Dim inBall() As Prim_Struct = CopyToPrim(Ball)
            ' Dim outBall() As Prim_Struct
            'Dim chunk As New PhysicsChunk(UBound(Ball), 0, Ball)
            '  Dim gMemIn, gMemOut
            ' gMemIn = gpu.Allocate(inBall) 'Of Single)(mb)


            Dim CalcBall() As Prim_Struct = Ball

            Dim gpuInBall() As Prim_Struct = gpu.Allocate(CalcBall) 'Of Single)(mb)
            Dim OutBall() As Prim_Struct = New Prim_Struct(CalcBall.Length - 1) {}

            Dim gpuOutBall() As Prim_Struct = gpu.Allocate(OutBall)
            '     Dim outBall() As Prim_Struct = inBall
            ' Dim cMemout() As Prim_Struct

            ' Dim gpuinBall() As Prim_Struct = gpu.CopyToDevice(Ball)
            gpu.CopyToDevice(Ball, gpuInBall)
            ' gpu.Allocate(outBall)

            ' Dim gpuOutBall() As Prim_Struct = gpu.CopyToDevice(outBall)
            'Dim N As Integer = 20

            Dim threads As Integer = 256

            Dim gpuCounter() As UInteger = gpu.Allocate(Of UInteger)(256)

            gpu.Set(gpuCounter)

            '  Dim DBVar(threads) As Debug_Struct
            'gpu.Allocate(DBVar)
            ' Dim OutDBVar() As Debug_Struct = gpu.CopyToDevice(DBVar)

            Dim nBlocks As Integer = (UBound(Ball) + threads - 1) / threads
            ' StartTimer()

            gpu.Launch(nBlocks, threads).CalcPhysics(gpuInBall, StepMulti, gpuOutBall, gpuCounter) ', OutDBVar)
            '  StopTimer()


            gpu.Synchronize()


            ' Dim bal

            gpu.CopyFromDevice(gpuOutBall, CalcBall)

            '  Dim HostCounter(256) As UInteger
            '   gpu.CopyFromDevice(gpuCounter, HostCounter)

            ' Dim leng As Integer = Ball.Length
            '   gpu.CopyFromDevice(gpuOutBall, outBall)
            '   gpu.CopyFromDevice(OutDBVar, DBVar)


            ' Ball = outBall

            ' Debug.Print(inBall(0).ForceX)

            gpu.FreeAll()

            gpuInBall = gpu.Allocate(CalcBall) 'Of Single)(mb)
            OutBall = New Prim_Struct(CalcBall.Length - 1) {}

            gpuOutBall = gpu.Allocate(OutBall)
            '     Dim outBall() As Prim_Struct = inBall
            ' Dim cMemout() As Prim_Struct

            ' Dim gpuinBall() As Prim_Struct = gpu.CopyToDevice(Ball)
            gpu.CopyToDevice(CalcBall, gpuInBall)


            gpu.Launch(nBlocks, threads).CollideBodies(gpuInBall, gpuOutBall, StepMulti) ', OutDBVar)
            '  StopTimer()


            gpu.Synchronize()

            gpu.CopyFromDevice(gpuOutBall, CalcBall)

            Ball = CalcBall
            CalcBall = Nothing
            UpdateBodies(Ball)


            'wait(1000)

            Dim NewBalls As New List(Of Prim_Struct)
            For a As Integer = 0 To UBound(Ball)

                If Ball(a).ForceTot > Ball(a).Mass * 4 And Ball(a).BlackHole = 0 Then ' And OuterBody(A).Size < 10 
                    Ball(a).InRoche = 1

                    NewBalls.AddRange(FractureBall(Ball(a)))
                ElseIf (Ball(a).ForceTot * 2) < Ball(a).Mass * 4 Then ' And OuterBody(A).Size > 10
                    Ball(a).InRoche = 0

                End If


            Next

            If NewBalls.Count > 0 Then
                Dim origLen As Integer = Ball.Length
                Array.Resize(Ball, (origLen + NewBalls.Count))
                Array.Copy(NewBalls.ToArray, 0, Ball, origLen, (NewBalls.Count))
            End If



            ' inBall = MyBodys.ToArray


            ' Ball = CopyToBallParm(MyBodys.ToArray)

            gpu.FreeAll()

                'For i As Integer = 1 To Ball.Length - 1

                '    Debug.Assert(Ball(i).Size > 0)
                'Next


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
        For i As Integer = 0 To UBound(Body)
            If Body(i).Visible = 1 Then
                Body(i).SpeedX += StepMulti * Body(i).ForceX / Body(i).Mass
                Body(i).SpeedY += StepMulti * Body(i).ForceY / Body(i).Mass
                Body(i).LocX += StepMulti * Body(i).SpeedX
                Body(i).LocY += StepMulti * Body(i).SpeedY
            End If

        Next
    End Sub

    <Cudafy>
    Public Sub CalcPhysics(gpThread As GThread, Body() As Prim_Struct, TimeStep As Double, OutBody() As Prim_Struct, gpuCounter() As UInteger) ', DebugStuff() As Debug_Struct) ', LB As Integer, UB As Integer)


        Dim A As Integer = gpThread.blockDim.x * gpThread.blockIdx.x + gpThread.threadIdx.x

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
        '   For A = LB To UB ' Each OuterOutBody(A) As BallParms In MyBodys 'A = lBoundBody To uBoundBody
        'OutBody(A).WhoTouchedMe = tid
        '  OutBody(A).TimesTouched += 1



        ' OutBody(A).ThreadID = gpThread.threadIdx.x
        '  If OuterOutBody(A).Visible Then
        '  If OuterOutBody(A).MovinG = False Then

        If A <= Body.Length - 1 And Body(A).Visible = 1 Then

            OutBody(A) = Body(A)
            OutBody(A).ThreadID = gpThread.threadIdx.x
            OutBody(A).BlockID = gpThread.blockIdx.x
            OutBody(A).BlockDIM = gpThread.blockDim.x

            OutBody(A).ForceX = 0
            OutBody(A).ForceY = 0
            OutBody(A).ForceTot = 0



            For B = 0 To Body.Length - 1
                If A <> B And Body(B).Visible = 1 Then
                    'If OuterOutBody(A).Index = 792 And Bodys(B).Index = 2002 Then
                    '    Debug.Print(DistSqrt & " - " & OuterOutBody(A).Size & " - " & Bodys(B).Size)
                    'End If


                    'If bolShawdow Then
                    '    If InStr(1, OuterOutBody(A).Flags, "S") Then
                    '        Dim m As Double, SlX As Double, SlY As Double
                    '        SlX = Bodys(B).LocX - OuterOutBody(A).LocX
                    '        SlY = Bodys(B).LocY - OuterOutBody(A).LocY
                    '        m = SlY / SlX
                    '        Bodys(B).ShadAngle = Math.Atan2(Bodys(B).LocY - OuterOutBody(A).LocY, Bodys(B).LocX - OuterOutBody(A).LocX)   'Math.Tan(SlX / SlY) 'Math.Atan2(SlY, SlX) * 180 / PI 'Math.Atan(SlX / SlY) * 180 / PI
                    '    End If
                    'End If
                    'If Body(B).LocX = OutBody(A).LocX And Body(B).LocY = OutBody(A).LocY Then
                    '    '  CollideBodies(OuterOutBody(A), Bodys(B))
                    'End If
                    'If bGrav = 0 Then
                    'Else

                    DistX = Body(B).LocX - OutBody(A).LocX
                    DistY = Body(B).LocY - OutBody(A).LocY
                    Dist = (DistX * DistX) + (DistY * DistY)
                    DistSqrt = Sqrt(Dist)
                    If DistSqrt > 0 Then 'Gravity reaction
                        '   If DistSqrt < (OuterOutBody(A).Size / 2) + (Bodys(B).Size / 2) Then DistSqrt = (OuterOutBody(A).Size / 2) + (Bodys(B).Size / 2) 'prevent screamers
                        M1 = OutBody(A).Mass '^ 2
                        M2 = Body(B).Mass ' ^ 2
                        TotMass = M1 * M2
                        Force = TotMass / (DistSqrt * DistSqrt + EPS * EPS) ' (Dist * DistSqrt)


                        ForceX = Force * DistX / DistSqrt
                        ForceY = Force * DistY / DistSqrt
                        OutBody(A).ForceTot += Force


                        OutBody(A).ForceX += ForceX
                        OutBody(A).ForceY += ForceY

                        'OuterOutBody(A).SpeedX += MyStep * ForceX / M1
                        'OuterOutBody(A).SpeedY += MyStep * ForceY / M1

                        ' If DistSqrt < 100 Then



                        'Do Until gpuCounter(1) = Body.Length - 1

                        'Loop


                        '''''If DistSqrt <= (OutBody(A).Size / 2) + (Body(B).Size / 2) Then 'Collision reaction




                        '''''    OutBody(A).LastColID = B
                        '''''    OutBody = CollideBodies(OutBody, A, B, DistSqrt, DistX, DistY, ForceX, ForceY)

                        '''''    '  gpThread.SyncThreads()
                        '''''End If


                        'End If
                    Else
                    End If
                    '  End If
                End If
                ' UpdateBody(OuterOutBody(A))







            Next B

            'OutBody(A).SpeedX += TimeStep * OutBody(A).ForceX / OutBody(A).Mass
            'OutBody(A).SpeedY += TimeStep * OutBody(A).ForceY / OutBody(A).Mass
            'OutBody(A).LocX += TimeStep * OutBody(A).SpeedX
            'OutBody(A).LocY += TimeStep * OutBody(A).SpeedY

            gpThread.SyncThreads()



            'gpThread.atomicIncEx(gpuCounter(1))
            'OutBody(A).Counter = gpuCounter(1)


            'Do Until gpuCounter(1) = Body.Length - 1


            'Loop


            'For i As Integer = 0 To Body.Length - 1
            '    If A <> i And Body(i).Visible = 1 Then
            '        DistX = Body(i).LocX - OutBody(A).LocX
            '        DistY = Body(i).LocY - OutBody(A).LocY
            '        Dist = (DistX * DistX) + (DistY * DistY)
            '        DistSqrt = Sqrt(Dist)


            '        If DistSqrt <= (OutBody(A).Size / 2) + (Body(i).Size / 2) Then


            '            M1 = OutBody(A).Mass '^ 2
            '            M2 = Body(i).Mass ' ^ 2
            '            TotMass = M1 * M2
            '            Force = TotMass / (DistSqrt * DistSqrt + EPS * EPS) ' (Dist * DistSqrt)
            '            ForceX = Force * DistX / DistSqrt
            '            ForceY = Force * DistY / DistSqrt


            '            OutBody(A).LastColID = i
            '            OutBody = CollideBodies(OutBody, A, i, DistSqrt, DistX, DistY, ForceX, ForceY)


            '        End If

            '    End If

            'Next




            'OutBody(A).SpeedX += MyStep * OutBody(A).ForceX / OutBody(A).Mass
            'OutBody(A).SpeedY += MyStep * OutBody(A).ForceY / OutBody(A).Mass
            'OutBody(A).LocX += MyStep * OutBody(A).SpeedX
            'OutBody(A).LocY += MyStep * OutBody(A).SpeedY




            ' gpThread.atomicIncEx(temp(1))

            'gpuCounter(gpThread.threadIdx.x)

        End If

        '  End If
        ' End If

        'OuterOutBody(A).LocX = OuterOutBody(A).LocX + (MyStep * OuterOutBody(A).SpeedX)
        'OuterOutBody(A).LocY = OuterOutBody(A).LocY + (MyStep * OuterOutBody(A).SpeedY)
        ' tmpBodys.Add(OuterOutBody(A))
        'CalcColor = ColorsRGB - (OuterOutBody(A).ForceTot * RGBMulti)
        ''If OutBody(A).ForceTot > OutBody(A).Mass * 4 And OutBody(A).BlackHole = 0 Then ' And OuterOutBody(A).Size < 10 
        ''    OutBody(A).InRoche = True
        ''    NewBalls.AddRange(FractureBall(OutBody(A)))
        ''ElseIf (OutBody(A).ForceTot * 2) < OutBody(A).Mass * 4 Then ' And OuterOutBody(A).Size > 10
        ''    OutBody(A).InRoche = False

        ''End If
        '    DistArray.Add(A.ToString + " - " + DistSqrt.ToString)

        ' Next A
        ' End If

        ''    ShrinkBallArray()
        ''If UBound(Ball) > 10000 Then
        ''End If
        ' UpdateBodies(Body)




        'Dim temp() As UInteger = gpThread.AllocateShared(Of UInteger)("temp", 256)
        '  temp(1) = 0

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
    '<Cudafy>
    'Public Function CollideBodies(ColBody() As Prim_Struct, Master As Integer, Slave As Integer, DistSqrt As Double, DistX As Double, DistY As Double, ForceX As Double, ForceY As Double) As Prim_Struct()
    '    Dim VeKY As Double
    '    Dim VekX As Double
    '    Dim V1x As Double
    '    Dim V2x As Double
    '    Dim M1 As Double
    '    Dim M2 As Double
    '    Dim V1y As Double
    '    Dim V2y As Double
    '    Dim EPS As Double = 2
    '    Dim V1 As Double
    '    Dim V2 As Double
    '    Dim U2 As Double
    '    Dim U1 As Double
    '    Dim PrevSpdX, PrevSpdY As Double
    '    Dim Area1 As Double, Area2 As Double
    '    If DistSqrt > 0 Then
    '        V1x = ColBody(Master).SpeedX
    '        V1y = ColBody(Master).SpeedY
    '        V2x = ColBody(Slave).SpeedX
    '        V2y = ColBody(Slave).SpeedY
    '        M1 = ColBody(Master).Mass
    '        M2 = ColBody(Slave).Mass
    '        VekX = DistX / 2 ' (Ball(A).LocX - Ball(B).LocX) / 2
    '        VeKY = DistY / 2 '(Ball(A).LocY - Ball(B).LocY) / 2
    '        VekX = VekX / (DistSqrt / 2) 'LenG
    '        VeKY = VeKY / (DistSqrt / 2) 'LenG
    '        V1 = VekX * V1x + VeKY * V1y
    '        V2 = VekX * V2x + VeKY * V2y
    '        U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
    '        U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)
    '        If ColBody(Master).InRoche = 0 And ColBody(Slave).InRoche = 1 Then
    '            If ColBody(Master).Mass > ColBody(Slave).Mass Then
    '                PrevSpdX = ColBody(Master).SpeedX
    '                PrevSpdY = ColBody(Master).SpeedY
    '                ColBody(Master).SpeedX = ColBody(Master).SpeedX + (U1 - V1) * VekX
    '                ColBody(Master).SpeedY = ColBody(Master).SpeedY + (U1 - V1) * VeKY
    '                ColBody(Slave).Visible = 0
    '                Area1 = PI * (ColBody(Master).Size ^ 2)
    '                Area2 = PI * (ColBody(Slave).Size ^ 2)
    '                Area1 = Area1 + Area2
    '                ColBody(Master).Size = Sqrt(Area1 / PI)
    '                ColBody(Master).Mass = ColBody(Master).Mass + ColBody(Slave).Mass 'Sqr(Ball(B).Mass)
    '            ElseIf ColBody(Master).Mass = ColBody(Slave).Mass Then
    '                If ColBody(Master).UID > ColBody(Slave).UID Then
    '                    PrevSpdX = ColBody(Master).SpeedX
    '                    PrevSpdY = ColBody(Master).SpeedY
    '                    ColBody(Master).SpeedX = ColBody(Master).SpeedX + (U1 - V1) * VekX
    '                    ColBody(Master).SpeedY = ColBody(Master).SpeedY + (U1 - V1) * VeKY
    '                    ColBody(Slave).Visible = 0
    '                    Area1 = PI * (ColBody(Master).Size ^ 2)
    '                    Area2 = PI * (ColBody(Slave).Size ^ 2)
    '                    Area1 = Area1 + Area2
    '                    ColBody(Master).Size = Sqrt(Area1 / PI)
    '                    ColBody(Master).Mass = ColBody(Master).Mass + ColBody(Slave).Mass 'Sqr(Ball(B).Mass)
    '                Else
    '                    ColBody(Master).Visible = 0
    '                End If
    '                '''''''''  ColBody(Master).Visible = False
    '            End If
    '        ElseIf ColBody(Master).InRoche = 0 And ColBody(Slave).InRoche = 0 Then
    '            If ColBody(Master).Mass > ColBody(Slave).Mass Then
    '                PrevSpdX = ColBody(Master).SpeedX
    '                PrevSpdY = ColBody(Master).SpeedY
    '                ColBody(Master).SpeedX = ColBody(Master).SpeedX + (U1 - V1) * VekX
    '                ColBody(Master).SpeedY = ColBody(Master).SpeedY + (U1 - V1) * VeKY
    '                ColBody(Slave).Visible = 0
    '                Area1 = PI * (ColBody(Master).Size ^ 2)
    '                Area2 = PI * (ColBody(Slave).Size ^ 2)
    '                Area1 = Area1 + Area2
    '                ColBody(Master).Size = Sqrt(Area1 / PI)
    '                ColBody(Master).Mass = ColBody(Master).Mass + ColBody(Slave).Mass 'Sqr(Ball(B).Mass)
    '            ElseIf ColBody(Master).Mass = ColBody(Slave).Mass Then
    '                If ColBody(Master).UID > ColBody(Slave).UID Then
    '                    PrevSpdX = ColBody(Master).SpeedX
    '                    PrevSpdY = ColBody(Master).SpeedY
    '                    ColBody(Master).SpeedX = ColBody(Master).SpeedX + (U1 - V1) * VekX
    '                    ColBody(Master).SpeedY = ColBody(Master).SpeedY + (U1 - V1) * VeKY
    '                    ColBody(Slave).Visible = 0
    '                    Area1 = PI * (ColBody(Master).Size ^ 2)
    '                    Area2 = PI * (ColBody(Slave).Size ^ 2)
    '                    Area1 = Area1 + Area2
    '                    ColBody(Master).Size = Sqrt(Area1 / PI)
    '                    ColBody(Master).Mass = ColBody(Master).Mass + ColBody(Slave).Mass 'Sqr(Ball(B).Mass)
    '                Else
    '                    ColBody(Master).Visible = 0
    '                End If
    '            Else
    '                ColBody(Master).Visible = 0
    '            End If
    '        ElseIf ColBody(Master).InRoche = 1 And ColBody(Slave).InRoche = 1 Then
    '            Dim multi As Integer = 20
    '            ColBody(Master).ForceX -= ForceX * multi
    '            ColBody(Master).ForceY -= ForceY * multi
    '            'ColBody(Slave).ForceX -= ForceX * multi
    '            'ColBody(Slave).ForceY -= ForceY * multi
    '            Dim Friction As Double = 0.8
    '            ColBody(Master).SpeedX += (U1 - V1) * VekX * Friction
    '            ColBody(Master).SpeedY += (U1 - V1) * VeKY * Friction
    '            'ColBody(Slave).SpeedX += (U2 - V2) * VekX * Friction
    '            'ColBody(Slave).SpeedY += (U2 - V2) * VeKY * Friction
    '        ElseIf ColBody(Master).InRoche = 1 And ColBody(Slave).InRoche = 0 Then
    '            ColBody(Master).Visible = 0
    '        End If
    '        ' End If
    '    Else ' if bodies are at exact same position
    '        If ColBody(Master).Mass > ColBody(Slave).Mass Then
    '            Area1 = PI * (ColBody(Master).Size ^ 2)
    '            Area2 = PI * (ColBody(Slave).Size ^ 2)
    '            Area1 = Area1 + Area2
    '            ColBody(Master).Size = Sqrt(Area1 / PI)
    '            ColBody(Master).Mass = ColBody(Master).Mass + ColBody(Slave).Mass 'Sqr(Ball(B).Mass)
    '            ColBody(Slave).Visible = 0
    '        Else
    '            'Area1 = PI * (ColBody(Master).Size ^ 2)
    '            'Area2 = PI * (ColBody(Slave).Size ^ 2)
    '            'Area1 = Area1 + Area2
    '            'ColBody(Slave).Size = Sqrt(Area1 / PI)
    '            'ColBody(Slave).Mass = ColBody(Slave).Mass + ColBody(Master).Mass 'Sqr(Ball(B).Mass)
    '            ColBody(Master).Visible = 0
    '        End If
    '    End If
    '    Return ColBody
    'End Function

    <Cudafy>
    Public Sub CollideBodies(gpThread As GThread, Body() As Prim_Struct, ColBody() As Prim_Struct, TimeStep As Double) 'Master As Integer, Slave As Integer, DistSqrt As Double, DistX As Double, DistY As Double, ForceX As Double, ForceY As Double) As Prim_Struct()
        Dim VeKY As Double
        Dim VekX As Double
        Dim V1x As Double
        Dim V2x As Double
        Dim M1 As Double
        Dim M2 As Double
        Dim V1y As Double
        Dim V2y As Double
        Dim EPS As Double = 2
        Dim V1 As Double
        Dim V2 As Double
        Dim U2 As Double
        Dim U1 As Double
        Dim PrevSpdX, PrevSpdY As Double
        Dim Area1 As Double, Area2 As Double

        Dim TotMass As Double
        Dim Force As Double
        Dim ForceX As Double
        Dim ForceY As Double
        Dim DistX As Double
        Dim DistY As Double
        Dim Dist As Double
        Dim DistSqrt As Double
        'Dim M1, M2 As Double



        Dim Master As Integer = gpThread.blockDim.x * gpThread.blockIdx.x + gpThread.threadIdx.x

        If Master <= Body.Length - 1 And Body(Master).Visible = 1 Then


            ColBody(Master) = Body(Master)


            For Slave As Integer = 0 To Body.Length - 1

                If Master <> Slave And Body(Slave).Visible = 1 Then
                    DistX = Body(Slave).LocX - ColBody(Master).LocX
                    DistY = Body(Slave).LocY - ColBody(Master).LocY
                    Dist = (DistX * DistX) + (DistY * DistY)
                    DistSqrt = Sqrt(Dist)




                    If DistSqrt <= (ColBody(Master).Size / 2) + (Body(Slave).Size / 2) Then
                        If DistSqrt > 0 Then

                            V1x = ColBody(Master).SpeedX
                            V1y = ColBody(Master).SpeedY
                            V2x = Body(Slave).SpeedX
                            V2y = Body(Slave).SpeedY
                            M1 = ColBody(Master).Mass
                            M2 = Body(Slave).Mass
                            VekX = DistX / 2 ' (Ball(A).LocX - Ball(B).LocX) / 2
                            VeKY = DistY / 2 '(Ball(A).LocY - Ball(B).LocY) / 2
                            VekX = VekX / (DistSqrt / 2) 'LenG
                            VeKY = VeKY / (DistSqrt / 2) 'LenG
                            V1 = VekX * V1x + VeKY * V1y
                            V2 = VekX * V2x + VeKY * V2y
                            U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
                            U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)
                            If ColBody(Master).InRoche = 0 And Body(Slave).InRoche = 1 Then
                                If ColBody(Master).Mass > Body(Slave).Mass Then
                                    PrevSpdX = ColBody(Master).SpeedX
                                    PrevSpdY = ColBody(Master).SpeedY
                                    ColBody(Master).SpeedX = ColBody(Master).SpeedX + (U1 - V1) * VekX
                                    ColBody(Master).SpeedY = ColBody(Master).SpeedY + (U1 - V1) * VeKY
                                    Body(Slave).Visible = 0
                                    Area1 = PI * (ColBody(Master).Size ^ 2)
                                    Area2 = PI * (Body(Slave).Size ^ 2)
                                    Area1 = Area1 + Area2
                                    ColBody(Master).Size = Sqrt(Area1 / PI)
                                    ColBody(Master).Mass = ColBody(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)
                                ElseIf ColBody(Master).Mass = Body(Slave).Mass Then
                                    If ColBody(Master).UID > Body(Slave).UID Then
                                        PrevSpdX = ColBody(Master).SpeedX
                                        PrevSpdY = ColBody(Master).SpeedY
                                        ColBody(Master).SpeedX = ColBody(Master).SpeedX + (U1 - V1) * VekX
                                        ColBody(Master).SpeedY = ColBody(Master).SpeedY + (U1 - V1) * VeKY
                                        Body(Slave).Visible = 0
                                        Area1 = PI * (ColBody(Master).Size ^ 2)
                                        Area2 = PI * (Body(Slave).Size ^ 2)
                                        Area1 = Area1 + Area2
                                        ColBody(Master).Size = Sqrt(Area1 / PI)
                                        ColBody(Master).Mass = ColBody(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)
                                    Else
                                        ColBody(Master).Visible = 0
                                    End If
                                    '''''''''  ColBody(Master).Visible = False
                                End If
                            ElseIf ColBody(Master).InRoche = 0 And Body(Slave).InRoche = 0 Then
                                If ColBody(Master).Mass > Body(Slave).Mass Then
                                    PrevSpdX = ColBody(Master).SpeedX
                                    PrevSpdY = ColBody(Master).SpeedY
                                    ColBody(Master).SpeedX = ColBody(Master).SpeedX + (U1 - V1) * VekX
                                    ColBody(Master).SpeedY = ColBody(Master).SpeedY + (U1 - V1) * VeKY
                                    Body(Slave).Visible = 0
                                    Area1 = PI * (ColBody(Master).Size ^ 2)
                                    Area2 = PI * (Body(Slave).Size ^ 2)
                                    Area1 = Area1 + Area2
                                    ColBody(Master).Size = Sqrt(Area1 / PI)
                                    ColBody(Master).Mass = ColBody(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)
                                ElseIf ColBody(Master).Mass = Body(Slave).Mass Then
                                    If ColBody(Master).UID > Body(Slave).UID Then
                                        PrevSpdX = ColBody(Master).SpeedX
                                        PrevSpdY = ColBody(Master).SpeedY
                                        ColBody(Master).SpeedX = ColBody(Master).SpeedX + (U1 - V1) * VekX
                                        ColBody(Master).SpeedY = ColBody(Master).SpeedY + (U1 - V1) * VeKY
                                        Body(Slave).Visible = 0
                                        Area1 = PI * (ColBody(Master).Size ^ 2)
                                        Area2 = PI * (Body(Slave).Size ^ 2)
                                        Area1 = Area1 + Area2
                                        ColBody(Master).Size = Sqrt(Area1 / PI)
                                        ColBody(Master).Mass = ColBody(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)
                                    Else
                                        ColBody(Master).Visible = 0
                                    End If
                                Else
                                    ColBody(Master).Visible = 0
                                End If
                            ElseIf ColBody(Master).InRoche = 1 And Body(Slave).InRoche = 1 Then


                                M1 = ColBody(Master).Mass '^ 2
                                M2 = Body(Slave).Mass ' ^ 2
                                TotMass = M1 * M2
                                Force = TotMass / (DistSqrt * DistSqrt + EPS * EPS) ' (Dist * DistSqrt)


                                ForceX = Force * DistX / DistSqrt
                                ForceY = Force * DistY / DistSqrt



                                Dim multi As Integer = 20
                                ColBody(Master).ForceX -= ForceX * multi
                                ColBody(Master).ForceY -= ForceY * multi
                                'Body(Slave).ForceX -= ForceX * multi
                                'Body(Slave).ForceY -= ForceY * multi
                                Dim Friction As Double = 0.8
                                ColBody(Master).SpeedX += (U1 - V1) * VekX * Friction
                                ColBody(Master).SpeedY += (U1 - V1) * VeKY * Friction
                                'Body(Slave).SpeedX += (U2 - V2) * VekX * Friction
                                'Body(Slave).SpeedY += (U2 - V2) * VeKY * Friction
                            ElseIf ColBody(Master).InRoche = 1 And Body(Slave).InRoche = 0 Then
                                ColBody(Master).Visible = 0
                            End If
                            ' End If
                        Else ' if bodies are at exact same position
                            If ColBody(Master).Mass > Body(Slave).Mass Then
                                Area1 = PI * (ColBody(Master).Size ^ 2)
                                Area2 = PI * (Body(Slave).Size ^ 2)
                                Area1 = Area1 + Area2
                                ColBody(Master).Size = Sqrt(Area1 / PI)
                                ColBody(Master).Mass = ColBody(Master).Mass + Body(Slave).Mass 'Sqr(Ball(B).Mass)
                                ' Body(Slave).Visible = 0
                            Else
                                'Area1 = PI * (ColBody(Master).Size ^ 2)
                                'Area2 = PI * (Body(Slave).Size ^ 2)
                                'Area1 = Area1 + Area2
                                'Body(Slave).Size = Sqrt(Area1 / PI)
                                'Body(Slave).Mass = Body(Slave).Mass + ColBody(Master).Mass 'Sqr(Ball(B).Mass)
                                ColBody(Master).Visible = 0
                            End If
                        End If
                    End If
                End If

            Next


        End If

        'OutBody(A).SpeedX += TimeStep * OutBody(A).ForceX / OutBody(A).Mass
        'OutBody(A).SpeedY += TimeStep * OutBody(A).ForceY / OutBody(A).Mass
        'OutBody(A).LocX += TimeStep * OutBody(A).SpeedX
        'OutBody(A).LocY += TimeStep * OutBody(A).SpeedY

        gpThread.SyncThreads()

        'Return ColBody
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
