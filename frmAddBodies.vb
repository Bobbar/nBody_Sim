Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.IO
Public Class frmAddBodies
    Private solarmass As Double = 30000
    Private Function circleV(rx As Double, ry As Double, CenterMass As Double) As Double
        ' Dim solarmass As Double = solarmass
        Dim r2 As Double = Sqrt(rx * rx + ry * ry)
        Dim numerator As Double = (0.000000977) * 1000000.0 * CenterMass 'solarmass
        Return Sqrt(numerator / r2)


    End Function
    'Private Sub AddBodies_Orbit(NumberOfBodies As Integer, MaxSize As Integer, MinSize As Integer, BodyMass As Integer, bolIncludeCenterMass As Boolean, MassOfCenterMass As Integer)
    '    On Error Resume Next
    '    'Dim Balls As Long = Number
    '    ' Dim Radius As Double = 1000
    '    Dim px, py As Double

    '    Density = txtDensity.Text
    '    For i As Integer = 0 To NumberOfBodies
    '        ReDim Preserve Ball(UBound(Ball) + 1)
    '        Ball(UBound(Ball)).UID = Guid.NewGuid.ToString
    '        Ball(UBound(Ball)).Color = RandomRGBColor() 'colDefBodyColor
    '        Ball(UBound(Ball)).Visible = True

    '        px = GetRandomNumber(1, Form1.Render.Width / pic_scale) - ScaleOffset.X - RelBallPosMod.X
    '        py = GetRandomNumber(1, Form1.Render.Height / pic_scale) - ScaleOffset.Y - RelBallPosMod.Y

    '        Dim magv As Double = circleV(px, py, MassOfCenterMass)

    '        Dim absangle As Double = Atan(Abs(py / px))
    '        Dim thetav As Double = PI / 2 - absangle
    '        Dim phiv As Double = Rnd() * PI
    '        Dim vx As Double = -1 * Sign(py) * Cos(thetav) * magv
    '        Dim vy As Double = Sign(px) * Sin(thetav) * magv


    '        Ball(UBound(Ball)).LocX = px
    '        Ball(UBound(Ball)).LocY = py

    '        Ball(UBound(Ball)).SpeedX = vx
    '        Ball(UBound(Ball)).SpeedY = vy
    '        Ball(UBound(Ball)).Flags = ""
    '        Ball(UBound(Ball)).Size = GetRandomNumber(MinSize, MaxSize)
    '        If BodyMass > 0 Then
    '            Ball(UBound(Ball)).Mass = BodyMass
    '        Else
    '            Ball(UBound(Ball)).Mass = fnMass(Ball(UBound(Ball)).Size) ' * 2
    '        End If

    '    Next

    '    If bolIncludeCenterMass Then
    '        ReDim Preserve Ball(UBound(Ball) + 1)
    '        Ball(UBound(Ball)).UID = Guid.NewGuid.ToString
    '        ' Ball(UBound(Ball)).Index = UBound(Ball)
    '        Ball(UBound(Ball)).Color = Color.Black 'RandomRGBColor() 'colDefBodyColor
    '        Ball(UBound(Ball)).Visible = True
    '        Ball(UBound(Ball)).LocX = Form1.Render.Width / 2 / pic_scale - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
    '        Ball(UBound(Ball)).LocY = Form1.Render.Height / 2 / pic_scale - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
    '        Ball(UBound(Ball)).SpeedX = 0
    '        Ball(UBound(Ball)).SpeedY = 0
    '        Ball(UBound(Ball)).Flags = "BH"
    '        Ball(UBound(Ball)).Size = 15
    '        Ball(UBound(Ball)).Mass = MassOfCenterMass 'fnMass(Ball(UBound(Ball)).Size) ' * 2
    '    End If


    '    Debug.Print(UBound(Ball))

    'End Sub
    Private Sub AddBodies_Orbit2(NumberOfBodies As Integer, MaxSize As Integer, MinSize As Integer, BodyMass As Integer, bolIncludeCenterMass As Boolean, MassOfCenterMass As Integer)
        On Error Resume Next
        'Dim Balls As Long = Number
        Dim Radius As Double = Int(txtRadius.Text) '5000 ' / 2 / pic_scale
        Dim px, py As Double
        Density = txtDensity.Text
        MassOfCenterMass *= Density
        Dim newEllipse As extra_render_objects
        newEllipse.Location = ScaleMousePosRelative(New SPoint(Form1.Render.Width / 2, Form1.Render.Height / 2)) 'ScaleMousePosRelative(New SPoint(0, 0))
        newEllipse.Size = Radius
        ExtraEllipses.Add(newEllipse)


        Dim nGas As Integer = (NumberOfBodies / 8) * 7
        Dim nMinerals As Integer = (NumberOfBodies / 8)

        Dim StartIndex As Integer
        Dim EndIndex As Integer
        If UBound(Ball) <> 0 Then
            StartIndex = UBound(Ball) + 1
            EndIndex = UBound(Ball) + NumberOfBodies ' - 1
        Else
            StartIndex = 0
            EndIndex = NumberOfBodies ' - 1
        End If
        ReDim Preserve Ball(EndIndex) 'UBound(Ball) + NumberOfBodies)
        Dim BodyCount As Integer = 0

        'ReDim Ball(NumberOfBodies)

        For i As Integer = StartIndex To EndIndex - 1 '(UBound(Ball) + NumberOfBodies)' For i As Integer = 0 To NumberOfBodies - 1
            Dim Matter As Matter_Props
            ' ReDim Preserve Ball(UBound(Ball) + 1)
            ' Ball(UBound(Ball)).Index = UBound(Ball)

            If BodyCount <= nGas Then
                Matter = MatterTypes(GetRandomNumber(0, 1))
            ElseIf BodyCount >= nMinerals Then
                Matter = MatterTypes(GetRandomNumber(2, 4))
            End If
            BodyCount += 1

            Ball(i).UID = RndIntUID(i) 'Guid.NewGuid.ToString
            Ball(i).Color = Matter.Color.ToArgb 'RandomRGBColor().ToArgb 'colDefBodyColor
            Ball(i).Visible = 1

            px = GetRandomNumber(newEllipse.Location.X - newEllipse.Size, newEllipse.Location.X + newEllipse.Size) ' - ScaleOffset.X - RelBallPosMod.X
            py = GetRandomNumber(newEllipse.Location.Y - newEllipse.Size, newEllipse.Location.Y + newEllipse.Size) ' - ScaleOffset.Y - RelBallPosMod.Y


            If Not ObjectInsideTarget(newEllipse.Location, newEllipse.Size, New SPoint(px, py)) Then
                Do Until ObjectInsideTarget(newEllipse.Location, newEllipse.Size, New SPoint(px, py))

                    px = GetRandomNumber(newEllipse.Location.X - newEllipse.Size, newEllipse.Location.X + newEllipse.Size) ' - ScaleOffset.X - RelBallPosMod.X
                    py = GetRandomNumber(newEllipse.Location.Y - newEllipse.Size, newEllipse.Location.Y + newEllipse.Size) ' - ScaleOffset.Y - RelBallPosMod.Y

                Loop



            End If

            Dim magv As Double = circleV(px, py, MassOfCenterMass)

            Dim absangle As Double = Atan(Abs(py / px))
            Dim thetav As Double = PI / 2 - absangle
            'Dim phiv As Double = Rnd() * PI
            Dim vx As Double = -1 * Sign(py) * Cos(thetav) * magv
            Dim vy As Double = Sign(px) * Sin(thetav) * magv


            Ball(i).LocX = px
            Ball(i).LocY = py

            Ball(i).SpeedX = vx
            Ball(i).SpeedY = vy
            ' Ball(UBound(Ball)).Flags = ""
            Ball(i).Size = GetRandomNumber(MinSize, MaxSize)
            If BodyMass > 0 Then
                Ball(i).Mass = BodyMass
            Else
                Ball(i).Mass = fnMass(Ball(i).Size, Matter.Density) ' * 2
            End If

        Next
        If bolIncludeCenterMass Then
            '  ReDim Preserve Ball(UBound(Ball) + 1)
            Ball(EndIndex).UID = RndIntUID(EndIndex) 'Guid.NewGuid.ToString
            Ball(EndIndex).Color = Color.Black.ToArgb 'RandomRGBColor() 'colDefBodyColor
            Ball(EndIndex).Visible = 1
            Ball(EndIndex).LocX = newEllipse.Location.X ' Form1.Render.Width / 2 / pic_scale - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
            Ball(EndIndex).LocY = newEllipse.Location.Y 'Form1.Render.Height / 2 / pic_scale - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
            Ball(EndIndex).SpeedX = 0
            Ball(EndIndex).SpeedY = 0
            Ball(EndIndex).BlackHole = 1
            'Ball(UBound(Ball)).Flags = "BH"
            Ball(EndIndex).Size = 3
            Ball(EndIndex).Mass = MassOfCenterMass 'fnMass(Ball(UBound(Ball)).Size) ' * 2
        End If



        Debug.Print(UBound(Ball))

    End Sub
    Private Sub AddBodies_StationaryDisc(NumberOfBodies As Integer, MaxSize As Integer, MinSize As Integer, BodyMass As Integer, bolIncludeCenterMass As Boolean, MassOfCenterMass As Integer)
        On Error Resume Next
        'Dim Balls As Long = Number
        Dim Radius As Double = Int(txtRadius.Text) '5000 ' / 2 / pic_scale
        Dim px, py As Double
        Density = txtDensity.Text
        MassOfCenterMass *= Density
        Dim newEllipse As extra_render_objects
        newEllipse.Location = ScaleMousePosRelative(New SPoint(Form1.Render.Width / 2, Form1.Render.Height / 2)) 'ScaleMousePosRelative(New SPoint(0, 0))
        newEllipse.Size = Radius
        ExtraEllipses.Add(newEllipse)


        Dim nGas As Integer = (NumberOfBodies / 8) * 7
        Dim nMinerals As Integer = (NumberOfBodies / 8)


        Dim StartIndex As Integer
        Dim EndIndex As Integer
        If UBound(Ball) <> 0 Then
            StartIndex = UBound(Ball)
            EndIndex = UBound(Ball) + NumberOfBodies
        Else
            StartIndex = 0
            EndIndex = NumberOfBodies
        End If
        ReDim Preserve Ball(EndIndex) 'UBound(Ball) + NumberOfBodies)
        Dim BodyCount As Integer = 0
        For i As Integer = StartIndex To EndIndex '(UBound(Ball) + NumberOfBodies)
            Dim Matter As Matter_Props

            If BodyCount <= nGas Then
                Matter = MatterTypes(GetRandomNumber(0, 1))
            ElseIf BodyCount >= nMinerals Then
                Matter = MatterTypes(GetRandomNumber(2, 4))
            End If
            BodyCount += 1

            ' ReDim Preserve Ball(UBound(Ball) + 1)
            ' Ball(UBound(Ball)).Index = UBound(Ball)
            Ball(i).UID = RndIntUID(i) 'Guid.NewGuid.ToString
            Ball(i).Color = Matter.Color.ToArgb 'RandomRGBColor().ToArgb 'colDefBodyColor
            Ball(i).Visible = 1

            px = GetRandomNumber(newEllipse.Location.X - newEllipse.Size, newEllipse.Location.X + newEllipse.Size) ' - ScaleOffset.X - RelBallPosMod.X
            py = GetRandomNumber(newEllipse.Location.Y - newEllipse.Size, newEllipse.Location.Y + newEllipse.Size) ' - ScaleOffset.Y - RelBallPosMod.Y


            If Not ObjectInsideTarget(newEllipse.Location, newEllipse.Size, New SPoint(px, py)) Then
                Do Until ObjectInsideTarget(newEllipse.Location, newEllipse.Size, New SPoint(px, py))

                    px = GetRandomNumber(newEllipse.Location.X - newEllipse.Size, newEllipse.Location.X + newEllipse.Size) ' - ScaleOffset.X - RelBallPosMod.X
                    py = GetRandomNumber(newEllipse.Location.Y - newEllipse.Size, newEllipse.Location.Y + newEllipse.Size) ' - ScaleOffset.Y - RelBallPosMod.Y

                Loop



            End If

            'Dim magv As Double = circleV(px, py, MassOfCenterMass)

            'Dim absangle As Double = Atan(Abs(py / px))
            'Dim thetav As Double = PI / 2 - absangle
            ''Dim phiv As Double = Rnd() * PI
            'Dim vx As Double = -1 * Sign(py) * Cos(thetav) * magv
            'Dim vy As Double = Sign(px) * Sin(thetav) * magv


            Ball(i).LocX = px
            Ball(i).LocY = py

            Ball(i).SpeedX = 0
            Ball(i).SpeedY = 0
            ' Ball(UBound(Ball)).Flags = ""
            Ball(i).Size = GetRandomNumber(MinSize, MaxSize)
            If BodyMass > 0 Then
                Ball(i).Mass = BodyMass
            Else
                Ball(i).Mass = fnMass(Ball(i).Size, Matter.Density) ' * 2
            End If

        Next
        'If bolIncludeCenterMass Then
        '    '  ReDim Preserve Ball(UBound(Ball) + 1)
        '    Ball(NumberOfBodies).UID = RndIntUID(NumberOfBodies) 'Guid.NewGuid.ToString
        '    Ball(NumberOfBodies).Color = Color.Black.ToArgb 'RandomRGBColor() 'colDefBodyColor
        '    Ball(NumberOfBodies).Visible = 1
        '    Ball(NumberOfBodies).LocX = newEllipse.Location.X ' Form1.Render.Width / 2 / pic_scale - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
        '    Ball(NumberOfBodies).LocY = newEllipse.Location.Y 'Form1.Render.Height / 2 / pic_scale - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
        '    Ball(NumberOfBodies).SpeedX = 0
        '    Ball(NumberOfBodies).SpeedY = 0
        '    Ball(NumberOfBodies).BlackHole = 1
        '    'Ball(UBound(Ball)).Flags = "BH"
        '    Ball(NumberOfBodies).Size = 15
        '    Ball(NumberOfBodies).Mass = MassOfCenterMass 'fnMass(Ball(UBound(Ball)).Size) ' * 2
        'End If



        'Debug.Print(UBound(Ball))

    End Sub
    Private Sub AddBodies_Stationary(NumberOfBodies As Integer, MaxSize As Integer, MinSize As Integer, BodyMass As Integer)
        Dim bolStoppedToAdd As Boolean = False
        If Not bolStopLoop Then
            bolStoppedToAdd = True
            bolStopLoop = True
            wait(300)
            Density = txtDensity.Text

            Dim DenMin As Integer = 0 'Density
            Dim DenMax As Integer = 3

            'For i As Integer = 0 To NumberOfBodies


            '    Dim Matter As Matter_Props = MatterTypes(GetRandomNumber(DenMin, DenMax))

            '    ReDim Preserve Ball(UBound(Ball) + 1)
            '    Ball(UBound(Ball)).UID = RndIntUID(UBound(Ball)) 'Guid.NewGuid.ToString
            '    '  Thread.Sleep(1)
            '    'Debug.Print(Ball(UBound(Ball)).UID.ToString)

            '    Ball(UBound(Ball)).Color = Matter.Color.ToArgb ' RandomRGBColor().ToArgb 'colDefBodyColor ' Color.Orange.ToArgb
            '    Ball(UBound(Ball)).Visible = 1
            '    Ball(UBound(Ball)).LocX = GetRandomNumber(1, Form1.Render.Width / pic_scale) - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
            '    Ball(UBound(Ball)).LocY = GetRandomNumber(1, Form1.Render.Height / pic_scale) - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
            '    Ball(UBound(Ball)).SpeedX = 0
            '    Ball(UBound(Ball)).SpeedY = 0
            '    ' Ball(UBound(Ball)).bTimestep = StepMulti
            '    ' Ball(UBound(Ball)).Flags = ""
            '    Ball(UBound(Ball)).Size = GetRandomNumber(MinSize, MaxSize)
            '    If BodyMass > 0 Then
            '        Ball(UBound(Ball)).Mass = BodyMass
            '    Else
            '        Ball(UBound(Ball)).Mass = (fnMass(Ball(UBound(Ball)).Size, Matter.Density)) ' * 2
            '    End If
            'Next





            For a As Integer = 0 To (NumberOfBodies / 8) * 7

                Dim Matter As Matter_Props = MatterTypes(GetRandomNumber(0, 1))

                ReDim Preserve Ball(UBound(Ball) + 1)
                Ball(UBound(Ball)).UID = RndIntUID(UBound(Ball)) 'Guid.NewGuid.ToString
                '  Thread.Sleep(1)
                'Debug.Print(Ball(UBound(Ball)).UID.ToString)

                Ball(UBound(Ball)).Color = Matter.Color.ToArgb ' RandomRGBColor().ToArgb 'colDefBodyColor ' Color.Orange.ToArgb
                Ball(UBound(Ball)).Visible = 1
                Ball(UBound(Ball)).LocX = GetRandomNumber(1, Form1.Render.Width / pic_scale) - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
                Ball(UBound(Ball)).LocY = GetRandomNumber(1, Form1.Render.Height / pic_scale) - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
                Ball(UBound(Ball)).SpeedX = 0
                Ball(UBound(Ball)).SpeedY = 0
                ' Ball(UBound(Ball)).bTimestep = StepMulti
                ' Ball(UBound(Ball)).Flags = ""
                Ball(UBound(Ball)).Size = GetRandomNumber(MinSize, MaxSize)
                If BodyMass > 0 Then
                    Ball(UBound(Ball)).Mass = BodyMass
                Else
                    Ball(UBound(Ball)).Mass = (fnMass(Ball(UBound(Ball)).Size, Matter.Density)) ' * 2
                End If
            Next a



            For a As Integer = 0 To (NumberOfBodies / 8) ' * 2

                Dim Matter As Matter_Props = MatterTypes(GetRandomNumber(2, 4))

                ReDim Preserve Ball(UBound(Ball) + 1)
                Ball(UBound(Ball)).UID = RndIntUID(UBound(Ball)) 'Guid.NewGuid.ToString
                '  Thread.Sleep(1)
                'Debug.Print(Ball(UBound(Ball)).UID.ToString)

                Ball(UBound(Ball)).Color = Matter.Color.ToArgb ' RandomRGBColor().ToArgb 'colDefBodyColor ' Color.Orange.ToArgb
                Ball(UBound(Ball)).Visible = 1
                Ball(UBound(Ball)).LocX = GetRandomNumber(1, Form1.Render.Width / pic_scale) - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
                Ball(UBound(Ball)).LocY = GetRandomNumber(1, Form1.Render.Height / pic_scale) - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
                Ball(UBound(Ball)).SpeedX = 0
                Ball(UBound(Ball)).SpeedY = 0
                ' Ball(UBound(Ball)).bTimestep = StepMulti
                ' Ball(UBound(Ball)).Flags = ""
                Ball(UBound(Ball)).Size = GetRandomNumber(MinSize, MaxSize)
                If BodyMass > 0 Then
                    Ball(UBound(Ball)).Mass = BodyMass
                Else
                    Ball(UBound(Ball)).Mass = (fnMass(Ball(UBound(Ball)).Size, Matter.Density)) ' * 2
                End If
            Next a





            If bolStoppedToAdd Then bolStopLoop = False
        End If



    End Sub

    Private Sub cmdAddOrbit_Click(sender As Object, e As EventArgs) Handles cmdAddOrbit.Click
        If chkCenterMass.Checked Then
            AddBodies_Orbit2(Int(txtNumOfBodies.Text), Int(txtMaxSize.Text), Int(txtMinSize.Text), Int(txtBodyMass.Text), chkCenterMass.Checked, Int(txtCenterMass.Text))
        Else
            AddBodies_StationaryDisc(Int(txtNumOfBodies.Text), Int(txtMaxSize.Text), Int(txtMinSize.Text), Int(txtBodyMass.Text), chkCenterMass.Checked, Int(txtCenterMass.Text))
        End If

    End Sub

    Private Sub cmdAddStation_Click(sender As Object, e As EventArgs) Handles cmdAddStation.Click
        AddBodies_Stationary(Int(txtNumOfBodies.Text), Int(txtMaxSize.Text), Int(txtMinSize.Text), Int(txtBodyMass.Text))
    End Sub

    Private Sub frmAddBodies_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDensity.Text = PhysicsLoop.Density
    End Sub

    Private Sub frmAddBodies_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Me.Hide()
    End Sub

End Class