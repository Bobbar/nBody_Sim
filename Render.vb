Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Public Module Renderer
    Public colBackColor As Color = Color.Black
    Public colDefBodyColor As Color = Color.White
    Public colControlForeColor As Color = Color.White
    Public ScreenCenterX As Single
    Public ScreenCenterY As Single
    Public CircleOInfluence As Single = 10000
    Public bolDrawing As Boolean = False
    Public bolCulling As Boolean = True
    'Public RenderWindowDimsH As Integer
    'Public RenderWindowDimsW As Integer
    Public RenderWindowDims As New Point(Form1.Render.Width, Form1.Render.Height)
    Public bm As New Bitmap(Form1.Render.Width, Form1.Render.Height) '(CInt(pic_scale * Render.Width), CInt(pic_scale * Render.Height))
    Public gr As Graphics = Graphics.FromImage(bm)
    Public Sub UpdateScale()
        '  Debug.Print("Scale Update")
        If RenderWindowDims.X <> bm.Size.Width Or RenderWindowDims.Y <> bm.Size.Height Then
            bm = New Bitmap(RenderWindowDims.X, RenderWindowDims.Y)
            gr = Graphics.FromImage(bm)
        End If
        gr.ResetTransform()
        gr.ScaleTransform(pic_scale, pic_scale)
    End Sub
    Public Sub Drawr(ByVal BallArray() As BallParms) ' As Bitmap
        bolDrawing = True
        Dim BodyLoc, Body2Loc As SPoint
        Dim BodySize, Body2Size As Single

        If RenderWindowDims.X <> bm.Size.Width Or RenderWindowDims.Y <> bm.Size.Height Then
            UpdateScale()
        End If
        Dim myPen As New Pen(Color.Red)
        If Not bolTrails Then gr.Clear(colBackColor)
        If bolAntiAliasing Then
            gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Else
            gr.SmoothingMode = Drawing2D.SmoothingMode.None
        End If
        Dim myBrush As SolidBrush '(BallArray(i).Color)
        If bolDraw Then


            For i = 0 To UBound(BallArray)
                BodyLoc = New SPoint(Convert.ToSingle(BallArray(i).LocX), Convert.ToSingle(BallArray(i).LocY))
                BodySize = Convert.ToSingle(BallArray(i).Size)
                If bolStopWorker Then Exit Sub
                If BallArray(i).Visible Then
                    If bolCulling And BodyLoc.X + FinalOffset.X < 0 Or bolCulling And BodyLoc.X + FinalOffset.X > Form1.Render.Width / pic_scale Or bolCulling And BodyLoc.Y + FinalOffset.Y < 0 Or bolCulling And BodyLoc.Y + FinalOffset.Y > Form1.Render.Height / pic_scale Then
                    Else


                        If bolInvert Then
                            myBrush = New SolidBrush(Color.Black)
                        Else
                            myBrush = New SolidBrush(BallArray(i).Color)
                        End If
                        'If BallArray(i).Flags.IndexOf("S") < 1 And BallArray(i).ShadAngle <> 0 And BallArray(i).Flags.IndexOf("R") < 1 And Form1.chkShadow.Checked Then
                        If bolShawdow Then
                            If InStr(1, BallArray(i).Flags, "S") = False And BallArray(i).ShadAngle <> 0 Then
                                Dim Bx1 As Single, Bx2 As Single, By1 As Single, By2 As Single
                                Bx1 = BodyLoc.X + (BodySize * 2) * Cos(BallArray(i).ShadAngle)
                                By1 = BodyLoc.Y + (BodySize * 2) * Sin(BallArray(i).ShadAngle)
                                Bx2 = BodyLoc.X + (BodySize / 2) * Cos(BallArray(i).ShadAngle) '(BodySize * Cos(BallArray(i).ShadAngle * PI / 180)) + BodyLoc.X 'BodyLoc.X + (BodySize * 2) * Cos(BallArray(i).ShadAngle)
                                By2 = BodyLoc.Y + (BodySize / 2) * Sin(BallArray(i).ShadAngle) '(BodySize * Sin(BallArray(i).ShadAngle * PI / 180)) + BodyLoc.Y 'BodyLoc.Y + (BodySize * 2) * Sin(BallArray(i).ShadAngle)
                                'Debug.Print(BallArray(i).Flags)
                                Dim myBrush2 As New LinearGradientBrush(New Point(Bx1, By1), New Point(Bx2, By2), Color.FromArgb(26, 26, 26, 1), BallArray(i).Color) 'SolidBrush(BallArray(i).Color)
                                gr.FillEllipse(myBrush2, BodyLoc.X - BodySize / 2 + RelBallPosMod.X, BodyLoc.Y - BodySize / 2 + RelBallPosMod.Y, BodySize, BodySize)
                                gr.ScaleTransform(pic_scale, pic_scale)
                            End If
                        Else
                            'If Not Form1.chkTrails.Checked Then
                            '  E.Graphics.ScaleTransform(pic_scale, pic_scale)
                            '  Dim myPen As New Pen(Color.Red)
                            ' Dim myBrush As New SolidBrush(BallArray(i).Color)
                            '    Dim myBrush2 As New SolidBrush(Color.Red)
                            If bolFollow Then
                                RelBallPosMod.X = -BallArray(lngFollowBall).LocX
                                RelBallPosMod.Y = -BallArray(lngFollowBall).LocY
                                ' If BallArray(lngFollowBall).LocY <> Ball(lngFollowBall).LocY Then Debug.Print("Loc ERROR")
                            End If
                            gr.FillEllipse(myBrush, BodyLoc.X - BodySize / 2 + FinalOffset.X, BodyLoc.Y - BodySize / 2 + FinalOffset.Y, BodySize, BodySize)
                            If InStr(1, BallArray(i).Flags, "BH") > 0 Then
                                ' Dim myPen As New Pen(Color.Red)
                                gr.DrawEllipse(myPen, BodyLoc.X - BodySize / 2 + FinalOffset.X, BodyLoc.Y - BodySize / 2 + FinalOffset.Y, BodySize, BodySize)
                            End If
                            If bolFollow Then
                                If lngFollowBall = i Then
                                    '  End If
                                    'gr.DrawEllipse(myPen, BallArray(lngFollowBall).LocX - BodySize / 2 + FinalOffset.X - ScaleMousePosExact(New SPoint(10000, 10000)).X, BallArray(lngFollowBall).LocY - BodySize / 2 + FinalOffset.Y - ScaleMousePosExact(New SPoint(10000, 10000)).Y, 10000, 10000)

                                    If bolSOI Then
                                        gr.DrawEllipse(myPen, BodyLoc.X - BodySize / 2 + FinalOffset.X - (CircleOInfluence), BodyLoc.Y - BodySize / 2 + FinalOffset.Y - (CircleOInfluence), CircleOInfluence * 2, CircleOInfluence * 2)
                                    End If
                                    If bolLines Then
                                        For b As Integer = 0 To UBound(BallArray)
                                            Body2Loc = New SPoint(Convert.ToSingle(BallArray(b).LocX), Convert.ToSingle(BallArray(b).LocY))
                                            Body2Size = Convert.ToSingle(BallArray(b).Size)

                                            If BallArray(b).Visible And GetDistanceOfBalls(BallArray(b), BallArray(i)) < CircleOInfluence Then
                                                Dim myPen2 As New Pen(Color.DarkGreen)
                                                myPen2.Width = 0.5
                                                gr.DrawLine(myPen2, BodyLoc.X + FinalOffset.X, BodyLoc.Y + FinalOffset.Y, Body2Loc.X + FinalOffset.X, Body2Loc.Y + FinalOffset.Y)
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                            'gr.FillEllipse(myBrush2, Convert.ToInt32(ScaleMousePosExact(New SPoint(Form1.Render.Width / 2, Form1.Render.Height / 2).X), Convert.ToInt32(New SPoint(Form1.Render.Width / 2, Form1.Render.Height / 2).Y), 5, 5)
                            'Else
                            '    Dim myPen As New Pen(BallArray(i).Color)
                            '    Form1.Render.CreateGraphics.DrawEllipse(myPen, BodyLoc.X - BodySize / 2 + RelBallPosMod.X, BodyLoc.Y - BodySize / 2 + RelBallPosMod.Y, BodySize, BodySize)
                            'End If
                        End If
                        'e.Graphics.FillEllipse(Brushes.Black, BodyLoc.X - BodySize / 2 - 1, BodyLoc.Y - BodySize / 2 - 1, BodySize + 2, BodySize + 2)

                    End If
                End If

            Next
        End If
        bolDrawing = False
        Form1.Render.Image = bm

        '   Return bm
    End Sub
    Public Function ScaledPoint(Point As Point, Origin As Point, Optional Scale As Double = 1.0) As Point
        Return New Point(Origin.X + Point.X * Scale, Origin.Y + Point.Y * Scale)
    End Function
    Public Function FinalOffset() As SPoint
        Return New SPoint(RelBallPosMod.X + ScaleOffset.X, RelBallPosMod.Y + ScaleOffset.Y)
    End Function
End Module
