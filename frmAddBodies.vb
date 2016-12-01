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
    Private Sub AddBodies_Orbit(NumberOfBodies As Integer, MaxSize As Integer, MinSize As Integer, BodyMass As Integer, bolIncludeCenterMass As Boolean, MassOfCenterMass As Integer)
        On Error Resume Next
        'Dim Balls As Long = Number
        ' Dim Radius As Double = 1000
        Dim px, py As Double

        Density = txtDensity.Text
        For i As Integer = 0 To NumberOfBodies
            ReDim Preserve Ball(UBound(Ball) + 1)
            Ball(UBound(Ball)).UID = Guid.NewGuid.ToString
            Ball(UBound(Ball)).Color = RandomRGBColor() 'colDefBodyColor
            Ball(UBound(Ball)).Visible = True

            px = GetRandomNumber(1, Form1.Render.Width / pic_scale) - ScaleOffset.X - RelBallPosMod.X
            py = GetRandomNumber(1, Form1.Render.Height / pic_scale) - ScaleOffset.Y - RelBallPosMod.Y

            Dim magv As Double = circleV(px, py, MassOfCenterMass)

            Dim absangle As Double = Atan(Abs(py / px))
            Dim thetav As Double = PI / 2 - absangle
            Dim phiv As Double = Rnd() * PI
            Dim vx As Double = -1 * Sign(py) * Cos(thetav) * magv
            Dim vy As Double = Sign(px) * Sin(thetav) * magv


            Ball(UBound(Ball)).LocX = px
            Ball(UBound(Ball)).LocY = py

            Ball(UBound(Ball)).SpeedX = vx
            Ball(UBound(Ball)).SpeedY = vy
            Ball(UBound(Ball)).Flags = ""
            Ball(UBound(Ball)).Size = GetRandomNumber(MinSize, MaxSize)
            If BodyMass > 0 Then
                Ball(UBound(Ball)).Mass = BodyMass
            Else
                Ball(UBound(Ball)).Mass = fnMass(Ball(UBound(Ball)).Size) ' * 2
            End If

        Next

        If bolIncludeCenterMass Then
            ReDim Preserve Ball(UBound(Ball) + 1)
            Ball(UBound(Ball)).UID = Guid.NewGuid.ToString
            ' Ball(UBound(Ball)).Index = UBound(Ball)
            Ball(UBound(Ball)).Color = Color.Black 'RandomRGBColor() 'colDefBodyColor
            Ball(UBound(Ball)).Visible = True
            Ball(UBound(Ball)).LocX = Form1.Render.Width / 2 / pic_scale - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
            Ball(UBound(Ball)).LocY = Form1.Render.Height / 2 / pic_scale - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
            Ball(UBound(Ball)).SpeedX = 0
            Ball(UBound(Ball)).SpeedY = 0
            Ball(UBound(Ball)).Flags = "BH"
            Ball(UBound(Ball)).Size = 15
            Ball(UBound(Ball)).Mass = MassOfCenterMass 'fnMass(Ball(UBound(Ball)).Size) ' * 2
        End If


        Debug.Print(UBound(Ball))

    End Sub
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




        For i As Integer = 0 To NumberOfBodies
            ReDim Preserve Ball(UBound(Ball) + 1)
            ' Ball(UBound(Ball)).Index = UBound(Ball)
            Ball(UBound(Ball)).UID = Guid.NewGuid.ToString
            Ball(UBound(Ball)).Color = RandomRGBColor() 'colDefBodyColor
            Ball(UBound(Ball)).Visible = True

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


            Ball(UBound(Ball)).LocX = px
            Ball(UBound(Ball)).LocY = py

            Ball(UBound(Ball)).SpeedX = vx
            Ball(UBound(Ball)).SpeedY = vy
            Ball(UBound(Ball)).Flags = ""
            Ball(UBound(Ball)).Size = GetRandomNumber(MinSize, MaxSize)
            If BodyMass > 0 Then
                Ball(UBound(Ball)).Mass = BodyMass
            Else
                Ball(UBound(Ball)).Mass = fnMass(Ball(UBound(Ball)).Size) ' * 2
            End If

        Next
        If bolIncludeCenterMass Then
            ReDim Preserve Ball(UBound(Ball) + 1)
            Ball(UBound(Ball)).UID = Guid.NewGuid.ToString
            Ball(UBound(Ball)).Color = Color.Black 'RandomRGBColor() 'colDefBodyColor
            Ball(UBound(Ball)).Visible = True
            Ball(UBound(Ball)).LocX = newEllipse.Location.X ' Form1.Render.Width / 2 / pic_scale - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
            Ball(UBound(Ball)).LocY = newEllipse.Location.Y 'Form1.Render.Height / 2 / pic_scale - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
            Ball(UBound(Ball)).SpeedX = 0
            Ball(UBound(Ball)).SpeedY = 0
            Ball(UBound(Ball)).Flags = "BH"
            Ball(UBound(Ball)).Size = 15
            Ball(UBound(Ball)).Mass = MassOfCenterMass 'fnMass(Ball(UBound(Ball)).Size) ' * 2
        End If



        Debug.Print(UBound(Ball))

    End Sub
    Private Sub AddBodies_Stationary(NumberOfBodies As Integer, MaxSize As Integer, MinSize As Integer, BodyMass As Integer)
        Density = txtDensity.Text

        For i As Integer = 0 To NumberOfBodies
            ReDim Preserve Ball(UBound(Ball) + 1)
            Ball(UBound(Ball)).UID = Guid.NewGuid.ToString
            Ball(UBound(Ball)).Color = Color.Orange 'RandomRGBColor() 'colDefBodyColor
            Ball(UBound(Ball)).Visible = True
            Ball(UBound(Ball)).LocX = GetRandomNumber(1, Form1.Render.Width / pic_scale) - ScaleOffset.X - RelBallPosMod.X ' * pic_scale
            Ball(UBound(Ball)).LocY = GetRandomNumber(1, Form1.Render.Height / pic_scale) - ScaleOffset.Y - RelBallPosMod.Y ' * pic_scale
            Ball(UBound(Ball)).SpeedX = 0
            Ball(UBound(Ball)).SpeedY = 0
            Ball(UBound(Ball)).Flags = ""
            Ball(UBound(Ball)).Size = GetRandomNumber(MinSize, MaxSize)
            If BodyMass > 0 Then
                Ball(UBound(Ball)).Mass = BodyMass
            Else
                Ball(UBound(Ball)).Mass = fnMass(Ball(UBound(Ball)).Size) ' * 2
            End If
        Next




    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub cmdAddOrbit_Click(sender As Object, e As EventArgs) Handles cmdAddOrbit.Click
        AddBodies_Orbit2(Int(txtNumOfBodies.Text), Int(txtMaxSize.Text), Int(txtMinSize.Text), Int(txtBodyMass.Text), chkCenterMass.Checked, Int(txtCenterMass.Text))
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

    Private Sub txtNumOfBodies_TextChanged(sender As Object, e As EventArgs) Handles txtNumOfBodies.TextChanged

    End Sub

    Private Sub txtMinSize_TextChanged(sender As Object, e As EventArgs) Handles txtMinSize.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub txtMaxSize_TextChanged(sender As Object, e As EventArgs) Handles txtMaxSize.TextChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub txtBodyMass_TextChanged(sender As Object, e As EventArgs) Handles txtBodyMass.TextChanged

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class