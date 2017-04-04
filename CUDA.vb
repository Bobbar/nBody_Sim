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

    'Public Const ThreadsPerBlock As Integer = 256
    ' Public Const Blocks = 1024 * 1024 / ThreadsPerBlock
    Public gpu As GPGPU
    Public bolLoopRunning As Boolean = False
    Public VisBalls As Integer
    Public PrevMass As Double
    Public mDelta As Double
    Public Const threads As Integer = 256
    ' Private dBall() As Prim_Struct

    Public Structure FoundGpu
        Public GPUType As eGPUType
        Public Index As Integer
    End Structure
    <Cudafy>
    Public Structure Prim_Struct
        Public LocX As Double
        Public LocY As Double
        Public Mass As Single
        Public SpeedX As Single
        Public SpeedY As Single
        Public ForceX As Single
        Public ForceY As Single
        Public ForceTot As Single
        Public Color As Integer
        Public Size As Double
        Public Visible As Integer
        Public InRoche As Integer
        Public BlackHole As Integer
        Public UID As Long
        'Public ThreadID As Integer
        'Public BlockID As Integer
        'Public BlockDIM As Integer
        'Public LastColID As Integer
    End Structure
    <ProtoBuf.ProtoContract>
    Public Structure Serial_Prim_Struct
        <ProtoBuf.ProtoMember(1)>
        Public LocX As Double
        <ProtoBuf.ProtoMember(2)>
        Public LocY As Double
        <ProtoBuf.ProtoMember(3)>
        Public Mass As Double
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
        Public Color As Integer
        <ProtoBuf.ProtoMember(10)>
        Public Size As Double
        <ProtoBuf.ProtoMember(11)>
        Public Visible As Integer
        <ProtoBuf.ProtoMember(12)>
        Public InRoche As Integer
        <ProtoBuf.ProtoMember(13)>
        Public BlackHole As Integer
        <ProtoBuf.ProtoMember(14)>
        Public UID As Long
        '   Public ThreadID As Integer
        'Public BlockID As Integer
        'Public BlockDIM As Integer
        'Public LastColID As Integer
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


        Dim CUDAmodule As CudafyModule = CudafyModule.TryDeserialize()
        If CUDAmodule Is Nothing OrElse Not CUDAmodule.TryVerifyChecksums() Then
            CudafyTranslator.Language = eLanguage.OpenCL
            CUDAmodule = CudafyTranslator.Cudafy(ePlatform.x64)
            CUDAmodule.Serialize()
        End If


        ' Dim CUDAmodule As CudafyModule
        gpu = CudafyHost.GetDevice(eGPUType.OpenCL, GPUIndex) 'eGPUType.Cuda, GPUIndex)
        ' CudafyTranslator.Language = eLanguage.OpenCL 'eLanguage.Cuda 
        'CUDAmodule = CudafyTranslator.Cudafy()
        gpu.LoadModule(CUDAmodule)
        Dim gpuProps As GPGPUProperties = gpu.GetDeviceProperties
        '  System.IO.File.WriteAllText("OpenCl_" + GPUIndex + ".cpp", CUDAmodule.CudaSourceCode)


    End Sub

    Public ThreadNum As Integer ' = 1
    Public StartTick, EndTick, ElapTick As Long
    Public SeekIndex As Integer = 0
    Public Prev_SeekIndex As Integer = 0
    ' <Cudafy>
    Public Sub StartCalc()

        bolRendering = True
        ' If Not bolPlaying Then
        ' Dim Ball() As Prim_Struct ' = Ball
        If ((Ball.Length - 1) - VisibleBalls()) > 400 Then
            ' StartTimer()
            Ball = CullBodies(Ball)
            ' StopTimer()
        End If
        '  VisBalls = UBound(Ball)

        PrevMass = TotalMass()

        'Make a local copy of the body array.
        'This is done so that changes can be updated to the rest of the program one frame at a time.
        ' Dim Ball() As Prim_Struct = Ball

        'Allocate the input array and collect the pointer.
        'This is the array the threads will read from, but not ever write to.


        Dim gpuInBall() As Prim_Struct = gpu.Allocate(Ball)

        'Declare and init the output array.
        Dim OutBall() As Prim_Struct = Ball 'New Prim_Struct(Ball.Length - 1) {}

        'Allocate the output array.
        'This is the array the threads will write to. Each thread works one element of this array.
        Dim gpuOutBall() As Prim_Struct = gpu.Allocate(OutBall)

        'Copy the body array to the device
        gpu.CopyToDevice(Ball, gpuInBall)

        'Number of threads per block
        ' Dim threads As Integer = 256 '256 is max for OpenCL AMD device.

        'Calc number of blocks needed based on number of bodys and threads.
        Dim nBlocks As Integer = ((Ball.Length - 1) + threads - 1) / threads
        '  StartTimer()
        'Launch the kernel to calculate body forces.
        gpu.Launch(nBlocks, threads).CalcPhysics(gpuInBall, Convert.ToSingle(StepMulti), gpuOutBall)

        'Wait until all threads finish, copy the device output array back to host array, then free device memory.
        'gpu.Synchronize()

        '  gpu.CopyFromDevice(gpuOutBall, Ball)
        ' gpu.FreeAll()

        'Allocate the updated body array and perform another kernel execution for collisions.


        '  gpuInBall = gpu.Allocate(Ball)
        ' OutBall = Ball 'New Prim_Struct(Ball.Length - 1) {}
        ' gpuOutBall = gpu.Allocate(Ball)
        ' gpu.CopyToDevice(Ball, gpuInBall)
        gpu.Launch(nBlocks, threads).CollideBodies(gpuOutBall, gpuInBall, Convert.ToSingle(StepMulti))
        gpu.Synchronize()
        gpu.CopyFromDevice(gpuInBall, Ball)
        gpu.FreeAll()



        'Copy the new data back into the public array


        'Integrate the body forces
        'UpdateBodies(Ball)

        'Iterate through body and determine if they are within a hypothetical Roche limit.
        'Bodies within Roche are broken into smaller bodies and added to the main body array.
        Dim NewBalls As New List(Of Prim_Struct)
        For a As Integer = 0 To Ball.Length - 1
            If Ball(a).Visible = 1 Then
                '  If Ball(a).ForceTot > Ball(a).Mass * 4 And Ball(a).BlackHole = 0 Then ' And OuterBody(A).Size < 10 
                'Ball(a).InRoche = 1
                'Ball(a).ForceTot = 0
                If Ball(a).InRoche = 1 And Ball(a).BlackHole = 0 Then
                    If Ball(a).BlackHole <> 2 Then NewBalls.AddRange(FractureBall(Ball(a)))
                End If


                'ElseIf (Ball(a).ForceTot * 2) < Ball(a).Mass * 4 Then ' And OuterBody(A).Size > 10
                '    Ball(a).InRoche = 0
                '    Ball(a).ForceTot = 0
                'ElseIf Ball(a).BlackHole = 2 Then
                '    Ball(a).InRoche = 1
                '    Ball(a).ForceTot = 0
                'End If
                If Ball(a).BlackHole = 2 Then Ball(a).InRoche = 1
                If Ball(a).BlackHole = 1 Then Ball(a).Size = 3
            End If
        Next
        If NewBalls.Count > 0 Then
            Dim origLen As Integer = Ball.Length
            Array.Resize(Ball, (origLen + NewBalls.Count))
            Array.Copy(NewBalls.ToArray, 0, Ball, origLen, (NewBalls.Count))
        End If

        'Ball = Ball

        '  End If

        mDelta += Round((TotalMass() - PrevMass), 5)

        bolRendering = False

    End Sub
    Private Sub DebugVis()
        '  Debug.Print(Ball(1483).Visible)
    End Sub
    Private Sub UpdateBodies(ByRef Body() As Prim_Struct)

        'Dim tmpBody() As Prim_Struct = Body
        'Parallel.For(0, UBound(Body),
        '             Sub(i As Integer)
        '                 If tmpBody(i).Visible = 1 Then
        '                     tmpBody(i).SpeedX += StepMulti * tmpBody(i).ForceX / tmpBody(i).Mass
        '                     tmpBody(i).SpeedY += StepMulti * tmpBody(i).ForceY / tmpBody(i).Mass
        '                     tmpBody(i).LocX += StepMulti * tmpBody(i).SpeedX
        '                     tmpBody(i).LocY += StepMulti * tmpBody(i).SpeedY
        '                 End If
        '             End Sub)
        'Body = tmpBody

        For i As Integer = 0 To Body.Length - 1
            If Body(i).Visible = 1 Then
                Body(i).SpeedX += StepMulti * Body(i).ForceX / Body(i).Mass
                Body(i).SpeedY += StepMulti * Body(i).ForceY / Body(i).Mass
                Body(i).LocX += StepMulti * Body(i).SpeedX
                Body(i).LocY += StepMulti * Body(i).SpeedY
            End If

        Next
    End Sub

    <Cudafy>
    Public Sub CalcPhysics(gpThread As GThread, Body() As Prim_Struct, TimeStep As Single, OutBody() As Prim_Struct) ', DebugStuff() As Debug_Struct) ', LB As Integer, UB As Integer)


        Dim A As Integer = gpThread.blockDim.x * gpThread.blockIdx.x + gpThread.threadIdx.x

        Dim TotMass As Single
        Dim Force As Single
        Dim ForceX As Single
        Dim ForceY As Single
        Dim DistX As Single
        Dim DistY As Single
        Dim Dist As Single
        Dim DistSqrt As Single
        Dim M1, M2 As Single
        Dim EPS As Single = 2

        Dim MyForceX, MyForceY, MyForceTot, MyLocX, MyLocY, MyMass, MySize, MySizeB As Single






        If A <= Body.Length - 1 Then

            OutBody(A) = Body(A)

            MyForceX = Body(A).ForceX
            MyForceY = Body(A).ForceY
            MyForceTot = Body(A).ForceTot
            MyLocX = Body(A).LocX
            MyLocY = Body(A).LocY
            MyMass = Body(A).Mass
            MySize = Body(A).Size / 2

            If Body(A).Visible = 1 Then



                'OutBody(A).ThreadID = gpThread.threadIdx.x
                'OutBody(A).BlockID = gpThread.blockIdx.x
                'OutBody(A).BlockDIM = gpThread.blockDim.x

                'OutBody(A).ForceX = 0
                'OutBody(A).ForceY = 0
                'OutBody(A).ForceTot = 0

                MyForceX = 0
                MyForceY = 0
                MyForceTot = 0

                For B = 0 To Body.Length - 1
                    MySizeB = Body(B).Size / 2

                    If A <> B And Body(B).Visible = 1 Then
                        DistX = Body(B).LocX - MyLocX
                        DistY = Body(B).LocY - MyLocY
                        Dist = (DistX * DistX) + (DistY * DistY)
                        DistSqrt = Sqrt(Dist)
                        If DistSqrt > 0 Then 'Gravity reaction


                            M1 = MyMass  'OutBody(A).Mass '^ 2
                                M2 = Body(B).Mass ' ^ 2
                                TotMass = M1 * M2
                                Force = TotMass / (DistSqrt * DistSqrt + EPS * EPS)

                                ForceX = Force * DistX / DistSqrt
                                ForceY = Force * DistY / DistSqrt

                                MyForceTot += Force
                            If DistSqrt > MySize + MySizeB Then
                                MyForceX += ForceX
                                MyForceY += ForceY
                            End If


                        Else
                        End If

                    End If


                Next B





                OutBody(A).ForceX = MyForceX
                OutBody(A).ForceY = MyForceY
                OutBody(A).ForceTot = MyForceTot



                If OutBody(A).ForceTot > OutBody(A).Mass * 4 And OutBody(A).BlackHole = 0 Then ' And OuterBody(A).Size < 10 
                    OutBody(A).InRoche = 1
                    ' OutBody(A).ForceTot = 0

                    'NewBalls.AddRange(FractureBall(OutBody(A)))
                    '  OutBody(A).Visible = 0
                ElseIf (OutBody(A).ForceTot * 2) < OutBody(A).Mass * 4 Then ' And OuterBody(A).Size > 10
                    OutBody(A).InRoche = 0
                    ' OutBody(A).ForceTot = 0
                ElseIf OutBody(A).BlackHole = 2 Then
                    OutBody(A).InRoche = 1
                    ' OutBody(A).ForceTot = 0
                End If

                'OutBody(A).SpeedX += TimeStep * OutBody(A).ForceX / OutBody(A).Mass
                'OutBody(A).SpeedY += TimeStep * OutBody(A).ForceY / OutBody(A).Mass
                'OutBody(A).LocX += TimeStep * OutBody(A).SpeedX
                'OutBody(A).LocY += TimeStep * OutBody(A).SpeedY


                '''''MyForceX = Body(A).ForceX
                '''''MyForceY = Body(A).ForceY
                '''''MyForceTot = Body(A).ForceTot
                '''''MyLocX = Body(A).LocX
                '''''MyLocY = Body(A).LocY
                '''''MyMass = Body(A).Mass




                gpThread.SyncThreads()
            End If
        End If

    End Sub
    <Cudafy>
    Public Sub CollideBodies(gpThread As GThread, Body() As Prim_Struct, ColBody() As Prim_Struct, TimeStep As Single) 'Master As Integer, Slave As Integer, DistSqrt As Double, DistX As Double, DistY As Double, ForceX As Double, ForceY As Double) As Prim_Struct()
        Dim VeKY As Single
        Dim VekX As Single
        Dim V1x As Single
        Dim V2x As Single
        Dim M1 As Single
        Dim M2 As Single
        Dim V1y As Single
        Dim V2y As Single

        Dim V1 As Single
        Dim V2 As Single
        Dim U2 As Single
        Dim U1 As Single
        'Dim PrevSpdX, PrevSpdY As Single
        Dim Area1 As Single, Area2 As Single
        Dim TotMass As Single
        Dim Force As Single
        Dim ForceX As Single
        Dim ForceY As Single
        Dim DistX As Single
        Dim DistY As Single
        Dim Dist As Single
        Dim DistSqrt As Single


        Dim MyLocX, MyLocY, MyMass, MySpeedX, MySpeedY, MySize As Single 'MyForceX, MyForceY
        Dim MyInRoche As Integer
        Dim MyUID As Long

        Dim Master As Integer = gpThread.blockDim.x * gpThread.blockIdx.x + gpThread.threadIdx.x
        If Master <= Body.Length - 1 And Body(Master).Visible = 1 Then
            ColBody(Master) = Body(Master)

            MyLocX = Body(Master).LocX
            MyLocY = Body(Master).LocY
            MySpeedX = Body(Master).SpeedX
            MySpeedY = Body(Master).SpeedY
            'MyForceX = Body(Master).ForceX
            'MyForceY = Body(Master).ForceY
            MyMass = Body(Master).Mass
            MyInRoche = Body(Master).InRoche
            MyUID = Body(Master).UID
            MySize = Body(Master).Size






            For Slave As Integer = 0 To Body.Length - 1


                Dim MySlaveLocX, MySlaveLocY, MySlaveMass, MySlaveSpeedX, MySlaveSpeedY, MySlaveSize As Single
                Dim MySlaveInRoche As Integer
                Dim MySlaveUID As Long


                MySlaveLocX = Body(Slave).LocX
                MySlaveLocY = Body(Slave).LocY
                MySlaveSpeedX = Body(Slave).SpeedX
                MySlaveSpeedY = Body(Slave).SpeedY
                MySlaveMass = Body(Slave).Mass
                MySlaveInRoche = Body(Slave).InRoche
                MySlaveUID = Body(Slave).UID
                MySlaveSize = Body(Slave).Size



                If Master <> Slave And Body(Slave).Visible = 1 Then
                    DistX = MySlaveLocX - MyLocX
                    DistY = MySlaveLocY - MyLocY
                    Dist = (DistX * DistX) + (DistY * DistY)
                    DistSqrt = Sqrt(Dist)

                    If DistSqrt <= (MySize / 2) + (MySlaveSize / 2) Then
                        ' ColBody(Master).LastColID = Slave
                        If DistSqrt > 0 Then

                            V1x = MySpeedX
                            V1y = MySpeedY
                            V2x = MySlaveSpeedX
                            V2y = MySlaveSpeedY
                            M1 = MyMass
                            M2 = MySlaveMass
                            VekX = DistX / 2 ' (Ball(A).LocX - Ball(B).LocX) / 2
                            VeKY = DistY / 2 '(Ball(A).LocY - Ball(B).LocY) / 2
                            VekX = VekX / (DistSqrt / 2) 'LenG
                            VeKY = VeKY / (DistSqrt / 2) 'LenG
                            V1 = VekX * V1x + VeKY * V1y
                            V2 = VekX * V2x + VeKY * V2y
                            U1 = (M1 * V1 + M2 * V2 - M2 * (V1 - V2)) / (M1 + M2)
                            U2 = (M1 * V1 + M2 * V2 - M1 * (V2 - V1)) / (M1 + M2)
                            If MyInRoche = 0 And MySlaveInRoche = 1 Then
                                If MyMass > MySlaveMass Then
                                    MySpeedX = MySpeedX + (U1 - V1) * VekX
                                    MySpeedY = MySpeedY + (U1 - V1) * VeKY
                                    'Body(Slave).Visible = 0
                                    Area1 = PI * (ColBody(Master).Size ^ 2)
                                    Area2 = PI * (Body(Slave).Size ^ 2)
                                    Area1 = Area1 + Area2
                                    ColBody(Master).Size = Sqrt(Area1 / PI)
                                    MyMass = MyMass + MySlaveMass 'Sqr(Ball(B).Mass)
                                ElseIf MyMass = MySlaveMass Then
                                    If MyUID > MySlaveUID Then
                                        MySpeedX = MySpeedX + (U1 - V1) * VekX
                                        MySpeedY = MySpeedY + (U1 - V1) * VeKY
                                        ' Body(Slave).Visible = 0
                                        Area1 = PI * (ColBody(Master).Size ^ 2)
                                        Area2 = PI * (Body(Slave).Size ^ 2)
                                        Area1 = Area1 + Area2
                                        ColBody(Master).Size = Sqrt(Area1 / PI)
                                        MyMass = MyMass + MySlaveMass 'Sqr(Ball(B).Mass)
                                    Else
                                        ColBody(Master).Visible = 0
                                    End If
                                Else
                                    ColBody(Master).Visible = False
                                End If
                            ElseIf MyInRoche = 0 And MySlaveInRoche = 0 Then
                                If MyMass > MySlaveMass Then
                                    MySpeedX = MySpeedX + (U1 - V1) * VekX
                                    MySpeedY = MySpeedY + (U1 - V1) * VeKY
                                    'Body(Slave).Visible = 0
                                    Area1 = PI * (ColBody(Master).Size ^ 2)
                                    Area2 = PI * (Body(Slave).Size ^ 2)
                                    Area1 = Area1 + Area2
                                    ColBody(Master).Size = Sqrt(Area1 / PI)
                                    MyMass = MyMass + MySlaveMass 'Sqr(Ball(B).Mass)
                                ElseIf MyMass = MySlaveMass Then
                                    If MyUID > MySlaveUID Then
                                        MySpeedX = MySpeedX + (U1 - V1) * VekX
                                        MySpeedY = MySpeedY + (U1 - V1) * VeKY
                                        '  Body(Slave).Visible = 0
                                        Area1 = PI * (ColBody(Master).Size ^ 2)
                                        Area2 = PI * (Body(Slave).Size ^ 2)
                                        Area1 = Area1 + Area2
                                        ColBody(Master).Size = Sqrt(Area1 / PI)
                                        MyMass = MyMass + MySlaveMass 'Sqr(Ball(B).Mass)
                                    Else
                                        ColBody(Master).Visible = 0
                                    End If
                                Else
                                    ColBody(Master).Visible = 0
                                End If




                            ElseIf MyInRoche = 1 And MySlaveInRoche = 1 Then

                                'Lame Spring force attempt. It's literally a reversed gravity force that's increased with a multiplier.
                                M1 = MyMass
                                M2 = MySlaveMass
                                TotMass = M1 * M2 'M1 * M2
                                ' TotMass = 100
                                Dim EPS As Double = 0.1
                                Force = TotMass / ((DistSqrt * DistSqrt) + EPS) '(ColBody(Master).Size / 2 + Body(Slave).Size / 2)) 'EPS) 'EPS * EPS)
                                ForceX = Force * DistX / DistSqrt
                                ForceY = Force * DistY / DistSqrt
                                Dim multi As Integer = 20 ' - (Sqrt(MyMass) * 2)  - (TimeStep * 1000) '(Sqrt(TimeStep) * 100)
                                ColBody(Master).ForceX -= ForceX * multi
                                ColBody(Master).ForceY -= ForceY * multi

                                Dim Friction As Double = 0.3
                                MySpeedX += (U1 - V1) * VekX * Friction
                                MySpeedY += (U1 - V1) * VeKY * Friction

                            ElseIf MyInRoche = 1 And MySlaveInRoche = 0 Then
                                ColBody(Master).Visible = 0
                            End If
                            ' End If
                        Else ' if bodies are at exact same position
                            If MyMass > MySlaveMass Then
                                Area1 = PI * (ColBody(Master).Size ^ 2)
                                Area2 = PI * (Body(Slave).Size ^ 2)
                                Area1 = Area1 + Area2
                                ColBody(Master).Size = Sqrt(Area1 / PI)
                                MyMass = MyMass + MySlaveMass 'Sqr(Ball(B).Mass)
                                ' Body(Slave).Visible = 0
                            Else
                                'Area1 = PI * (ColBody(Master).Size ^ 2)
                                'Area2 = PI * (Body(Slave).Size ^ 2)
                                'Area1 = Area1 + Area2
                                'Body(Slave).Size = Sqrt(Area1 / PI)
                                'MySlaveMass = MySlaveMass + MyMass 'Sqr(Ball(B).Mass)
                                ColBody(Master).Visible = 0
                            End If
                        End If
                    End If
                End If

            Next




            MySpeedX += TimeStep * ColBody(Master).ForceX / MyMass
            MySpeedY += TimeStep * ColBody(Master).ForceY / MyMass
            MyLocX += TimeStep * MySpeedX
            MyLocY += TimeStep * MySpeedY

            ColBody(Master).SpeedX = MySpeedX
                ColBody(Master).SpeedY = MySpeedY
                ColBody(Master).LocX = MyLocX
                ColBody(Master).LocY = MyLocY
                ColBody(Master).Mass = MyMass


                'MyLocX = Body(Master).LocX
                'MyLocY = Body(Master).LocY
                'MySpeedX = Body(Master).SpeedX
                'MySpeedY = Body(Master).SpeedY
                'MyMass = Body(Master).Mass
                'MyInRoche = Body(Master).InRoche
                'MyUID = Body(Master).UID





            End If


            gpThread.SyncThreads()

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
        'Dim px, py As Double
        'Dim RadUPX As Double, RadDNX As Double, RadUPY As Double, RadDNY As Double
        '' i = UBound(Ball)
        If Body.Visible = 1 And Body.Size > 1 Then
            Area = PI * ((Body.Size / 2) ^ 2)
            'Divisor = Int(Body.Size)
            Divisor = Int(Area)
            If Divisor <= 1 Then Divisor = 2
            PrevSize = Body.Size
            PrevMass = Body.Mass
            'PrevSize = Sqr(PrevSize / pi)
            ' Area = PI * (Body.Size ^ 2)
            Area = Area / Divisor
            NewBallSize = fnRadius(Area)  'fnRadius(fnArea(Body.Size) / 2)  'Sqr(Area / pi) 'Body.Size / Divisor

            NewBallMass = PrevMass / Divisor  '(Body.Mass / Divisor)
            Body.Visible = 0
            '
            '                                            Body.Size = NewBallSize
            '                                            Body.Mass = NewBallMass
            '                                            Body.Flags = "B"
            'RadUPX = (Body.LocX) + PrevSize / 2 + Body.SpeedX * StepMulti
            'RadDNX = (Body.LocX) - PrevSize / 2 + Body.SpeedX * StepMulti
            'RadUPY = (Body.LocY) + PrevSize / 2 + Body.SpeedY * StepMulti
            'RadDNY = (Body.LocY) - PrevSize / 2 + Body.SpeedY * StepMulti

            'RadUPX = (Body.LocX) + PrevSize + Body.SpeedX * StepMulti
            'RadDNX = (Body.LocX) - PrevSize + Body.SpeedX * StepMulti
            'RadUPY = (Body.LocY) + PrevSize + Body.SpeedY * StepMulti
            'RadDNY = (Body.LocY) - PrevSize + Body.SpeedY * StepMulti


            'Dim CenterPoint As New Point(Body.LocX, Body.LocY)
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
                tmpBall.UID = RndIntUID(h) 'Now.Ticks 'Guid.NewGuid.ToString
                '  tmpBall.IsFragment = True
                tmpBall.InRoche = 1
                tmpBall.Visible = 1
                '  Ball(u).LocY = Body.LocY + Ball(u).Size * 2



                tmpBall.LocX = RandomSingle(Body.LocX - Body.Size / 2, Body.LocX + Body.Size / 2) 'GetRandomNumber(Body.LocX - Body.Size, Body.LocX + Body.Size)
                tmpBall.LocY = RandomSingle(Body.LocY - Body.Size / 2, Body.LocY + Body.Size / 2) 'GetRandomNumber(Body.LocY - Body.Size, Body.LocY + Body.Size)

                If h > 1 Then
                    If Not ObjectInsideTarget(New SPoint(Body.LocX, Body.LocY), Body.Size, New SPoint(tmpBall.LocX, tmpBall.LocY)) Then 'newEllipse.Location, newEllipse.Size, New SPoint(px, py)) Then

                        Do Until ObjectInsideTarget(New SPoint(Body.LocX, Body.LocY), Body.Size, New SPoint(tmpBall.LocX, tmpBall.LocY)) ' And Not DupLoc(tmpBallList, tmpBall)

                            tmpBall.LocX = RandomSingle(Body.LocX - Body.Size / 2, Body.LocX + Body.Size / 2) 'GetRandomNumber(Body.LocX - Body.Size, Body.LocX + Body.Size)
                            tmpBall.LocY = RandomSingle(Body.LocY - Body.Size / 2, Body.LocY + Body.Size / 2) 'GetRandomNumber(Body.LocY - Body.Size, Body.LocY + Body.Size)

                        Loop



                    End If
                End If


                'tmpBall.LocX = px
                'tmpBall.LocY = py





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
        'If LstBodies.Count < 1 Then Return False
        'For Each bdy As Prim_Struct In LstBodies
        '    If Body.LocX = bdy.LocX And Body.LocY = bdy.LocY Then Return True
        'Next

        Dim DistX, DistY, Dist, DistSqrt As Double



        If LstBodies.Count < 1 Then Return False
        For Each bdy As Prim_Struct In LstBodies
            DistX = bdy.LocX - Body.LocX
            DistY = bdy.LocY - Body.LocY
            Dist = (DistX * DistX) + (DistY * DistY)
            DistSqrt = Sqrt(Dist)
            If DistSqrt <= (Body.Size / 2) + (bdy.Size / 2) Then
                Return True
            End If

            'If Body.LocX = bdy.LocX And Body.LocY = bdy.LocY Then Return True
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
