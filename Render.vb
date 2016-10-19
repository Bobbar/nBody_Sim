Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Public Module Renderer
    Public ScreenCenterX As Single
    Public ScreenCenterY As Single
    'Public RenderWindowDimsH As Integer
    'Public RenderWindowDimsW As Integer
    Public RenderWindowDims As New Point(Form1.Render.Width, Form1.Render.Height)
    Public bm As New Bitmap(Form1.Render.Width, Form1.Render.Height) '(CInt(pic_scale * Render.Width), CInt(pic_scale * Render.Height))
    Public gr As Graphics = Graphics.FromImage(bm)
    Public Sub UpdateScale()
        gr.ResetTransform()
        gr.ScaleTransform(pic_scale, pic_scale)
        If RenderWindowDims.X <> bm.Size.Width Or RenderWindowDims.Y <> bm.Size.Height Then
            bm = New Bitmap(RenderWindowDims.X, RenderWindowDims.Y)
            gr = Graphics.FromImage(bm)
        End If
    End Sub
    Public Function Drawr() As Bitmap
        If RenderWindowDims.X <> bm.Size.Width Or RenderWindowDims.Y <> bm.Size.Height Then
            UpdateScale()
        End If
        Dim Origin As New SPoint
        If pic_scale <> 1 Then
            '  Origin = New Point(-((Form1.Render.Width / 2)), -((Form1.Render.Height / 2)))
        Else
            Origin = New SPoint(0, 0)
        End If

        If Not Form1.chkTrails.Checked Then gr.Clear(Color.White)
        gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias


        If Form1.chkDraw.Checked Then
            For i = 0 To UBound(Ball)

                If Ball(i).Visible Then ' And Ball(i).LocX > 0 And Ball(i).LocX < Render.Width And Ball(i).LocY > 0 And Ball(i).LocY < Render.Height Then
                    'g = g + 1
                    ' If g > 4 Then g = 0


                    ' e.Graphics.FillEllipse(Brushes.LightBlue, ball_loc_x(i) - 1, ball_loc_y(i) - 1, BallSize(i) + 2, BallSize(i) + 2)
                    ' e.Graphics.FillEllipse(Brushes.Blue, ball_loc_x(i), ball_loc_y(i), BallSize(i), BallSize(i))

                    If InStr(1, Ball(i).Flags, "S") = False And Ball(i).ShadAngle <> 0 And InStr(1, Ball(i).Flags, "R") = False And Form1.chkShadow.Checked Then
                        Dim Bx1 As Single, Bx2 As Single, By1 As Single, By2 As Single



                        Bx1 = Ball(i).LocX + (Ball(i).Size * 2) * Cos(Ball(i).ShadAngle)
                        By1 = Ball(i).LocY + (Ball(i).Size * 2) * Sin(Ball(i).ShadAngle)
                        Bx2 = Ball(i).LocX + (Ball(i).Size / 2) * Cos(Ball(i).ShadAngle) '(Ball(i).Size * Cos(Ball(i).ShadAngle * PI / 180)) + Ball(i).LocX 'Ball(i).LocX + (Ball(i).Size * 2) * Cos(Ball(i).ShadAngle)
                        By2 = Ball(i).LocY + (Ball(i).Size / 2) * Sin(Ball(i).ShadAngle) '(Ball(i).Size * Sin(Ball(i).ShadAngle * PI / 180)) + Ball(i).LocY 'Ball(i).LocY + (Ball(i).Size * 2) * Sin(Ball(i).ShadAngle)
                        'Debug.Print(Ball(i).Flags)

                        Dim myBrush2 As New LinearGradientBrush(New Point(Bx1, By1), New Point(Bx2, By2), Color.FromArgb(26, 26, 26, 1), Ball(i).Color) 'SolidBrush(Ball(i).Color)
                        gr.FillEllipse(myBrush2, Ball(i).LocX - Ball(i).Size / 2 + RelBallPosMod.X, Ball(i).LocY - Ball(i).Size / 2 + RelBallPosMod.Y, Ball(i).Size, Ball(i).Size)
                        gr.ScaleTransform(pic_scale, pic_scale)


                    Else
                        'If Not Form1.chkTrails.Checked Then
                        '  E.Graphics.ScaleTransform(pic_scale, pic_scale)
                        '  Dim myPen As New Pen(Color.Red)
                        Dim myBrush As New SolidBrush(Ball(i).Color)
                        Dim myBrush2 As New SolidBrush(Color.Red)
                        gr.FillEllipse(myBrush, Ball(i).LocX - Ball(i).Size / 2 + FinalOffset.X, Ball(i).LocY - Ball(i).Size / 2 + FinalOffset.Y, Ball(i).Size, Ball(i).Size)


                        'gr.FillEllipse(myBrush2, Convert.ToInt32(ScaleMousePosExact(New SPoint(Form1.Render.Width / 2, Form1.Render.Height / 2).X), Convert.ToInt32(New SPoint(Form1.Render.Width / 2, Form1.Render.Height / 2).Y), 5, 5)
                        'Else
                        '    Dim myPen As New Pen(Ball(i).Color)

                        '    Form1.Render.CreateGraphics.DrawEllipse(myPen, Ball(i).LocX - Ball(i).Size / 2 + RelBallPosMod.X, Ball(i).LocY - Ball(i).Size / 2 + RelBallPosMod.Y, Ball(i).Size, Ball(i).Size)
                        'End If

                    End If
                    'e.Graphics.FillEllipse(Brushes.Black, Ball(i).LocX - Ball(i).Size / 2 - 1, Ball(i).LocY - Ball(i).Size / 2 - 1, Ball(i).Size + 2, Ball(i).Size + 2)



                End If

            Next

        End If
        Return bm

    End Function
    Public Function ScaledPoint(Point As Point, Origin As Point, Optional Scale As Double = 1.0) As Point
        Return New Point(Origin.X + Point.X * Scale, Origin.Y + Point.Y * Scale)
    End Function
    Public Function FinalOffset() As SPoint

        Return New SPoint(RelBallPosMod.X + ScaleOffset.X, RelBallPosMod.Y + ScaleOffset.Y)
    End Function
End Module
