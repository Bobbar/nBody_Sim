﻿Imports System.Math
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Drawing.Drawing2D
Public Module Renderer
    Public colBackColor As Color = Color.Black
    ' Public colDefBodyColor As Color = Color.White
    Public colControlForeColor As Color = Color.White
    Public ScreenCenterX As Single
    Public ScreenCenterY As Single
    Public CircleOInfluence As Single = 10000
    Public bolDrawing As Boolean = False
    Public bolCulling As Boolean = False
    Public bolShowAll As Boolean = False
    Public FollowGUID As Long
    Public FinalOffset As SPoint
    ' Public buffBall() As Body_Struct
    Private BHHighlightPen As New Pen(Color.Red)
    ' Private BodyBrush As SolidBrush
    Private FollowLoc As SPoint
    'Public RenderWindowDimsH As Integer
    'Public RenderWindowDimsW As Integer
    Public RenderWindowDims As New Point(Form1.Render.Width, Form1.Render.Height)
    Public bm As New Bitmap(Form1.Render.Width, Form1.Render.Height, Imaging.PixelFormat.Format32bppPArgb) '(CInt(pic_scale * Render.Width), CInt(pic_scale * Render.Height))
    Public gr As Graphics = SetGfx(bm) 'Graphics.FromImage(bm)
    Private Function SetGfx(bm As Bitmap) As Graphics
        Dim gfx As Graphics = Graphics.FromImage(bm)
        gfx.CompositingMode = CompositingMode.SourceCopy
        gfx.CompositingQuality = CompositingQuality.HighSpeed
        gfx.PixelOffsetMode = PixelOffsetMode.None
        'gfx.SmoothingMode = 
        gfx.InterpolationMode = InterpolationMode.Default
        Return gfx
    End Function
    Public Structure extra_render_objects
        Public Location As SPoint
        Public Size As Single

    End Structure
    Public ExtraEllipses As New List(Of extra_render_objects)
    Public Sub UpdateScale()
        '  Debug.Print("Scale Update")
        If RenderWindowDims.X <> bm.Size.Width Or RenderWindowDims.Y <> bm.Size.Height Then
            bm = New Bitmap(RenderWindowDims.X, RenderWindowDims.Y)
            gr = Graphics.FromImage(bm)
        End If
        gr.ResetTransform()
        gr.ScaleTransform(pic_scale, pic_scale)
    End Sub
    Public Sub Drawr(ByVal BallArray() As Body_Struct) ' As Bitmap

        FinalOffset = GetFinalOffset()
        Dim BodyLoc, Body2Loc As SPoint
        Dim BodySize, Body2Size As Single
        'Dim FollowLoc As New SPoint
        If RenderWindowDims.X <> bm.Size.Width Or RenderWindowDims.Y <> bm.Size.Height Then
            UpdateScale()
        End If
        ' Dim myPen As New Pen(Color.Red)



        If bolAntiAliasing Then
            gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Else
            gr.SmoothingMode = Drawing2D.SmoothingMode.None
        End If
        '  Dim myBrush As SolidBrush '(BallArray(i).Color)
        If bolDraw Then
            If Not bolTrails Then gr.Clear(colBackColor)
            bolDrawing = True
            If bolFollow Then
                FollowLoc = FollowBodyLoc(BallArray)
                If FollowLoc Is Nothing Then bolFollow = False
            End If
            For i = 0 To UBound(BallArray)
                ' BodyLoc = New SPoint(Convert.ToSingle(BallArray(i).LocX), Convert.ToSingle(BallArray(i).LocY))
                BodyLoc = New SPoint(BallArray(i).LocX, BallArray(i).LocY)
                BodySize = Convert.ToSingle(BallArray(i).Size)
                If bolStopWorker Then
                    Exit Sub
                End If

                If BallArray(i).Visible = 1 Or bolShowAll Then
                    'If bolCulling AndAlso BodyLoc.X + FinalOffset.X < 0 Or bolCulling And BodyLoc.X + FinalOffset.X > Form1.Render.Width / pic_scale Or bolCulling And BodyLoc.Y + FinalOffset.Y < 0 Or bolCulling And BodyLoc.Y + FinalOffset.Y > Form1.Render.Height / pic_scale Then
                    'Else
                    If Not CullBody(BodyLoc) Then

                        'If bolInvert Then
                        '    BodyBrush = New SolidBrush(Color.Black)
                        'Else
                        '    BodyBrush = New SolidBrush(Color.FromArgb(BallArray(i).Color))
                        'End If



                        If bolFollow Then

                            RelBallPosMod.X = -FollowLoc.X
                            RelBallPosMod.Y = -FollowLoc.Y

                        End If
                        Using BodyBrush = GetBodyBrush(BallArray(i).Color)
                            ' gr.FillEllipse(myBrush, BodyLoc.X - BodySize / 2 + FinalOffset.X, BodyLoc.Y - BodySize / 2 + FinalOffset.Y, BodySize, BodySize)

                            gr.FillEllipse(BodyBrush, BodyLoc.X - BodySize * 0.5F + FinalOffset.X, BodyLoc.Y - BodySize * 0.5F + FinalOffset.Y, BodySize, BodySize)
                        End Using


                        If BallArray(i).BlackHole = 1 Then
                            ' gr.DrawEllipse(BHHighlightPen, BodyLoc.X - BodySize / 2 + FinalOffset.X, BodyLoc.Y - BodySize / 2 + FinalOffset.Y, BodySize, BodySize)
                            gr.DrawEllipse(BHHighlightPen, BodyLoc.X - BodySize * 0.5F + FinalOffset.X, BodyLoc.Y - BodySize * 0.5F + FinalOffset.Y, BodySize, BodySize)
                        End If
                        If bolFollow Then
                            If BallArray(i).UID = FollowGUID Then


                                If bolSOI Then
                                    gr.DrawEllipse(BHHighlightPen, BodyLoc.X - BodySize / 2 + FinalOffset.X - (CircleOInfluence), BodyLoc.Y - BodySize / 2 + FinalOffset.Y - (CircleOInfluence), CircleOInfluence * 2, CircleOInfluence * 2)
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


                    End If
                End If

            Next
        End If


        Form1.Render.Image = bm
        Form1.Render.Invalidate()
        bolDrawing = False

    End Sub
    Private Function GetBodyBrush(aRGBColor As Integer) As SolidBrush
        If bolInvert Then
            Return New SolidBrush(Color.Black)
        Else
            Return New SolidBrush(Color.FromArgb(aRGBColor))
        End If
    End Function


    Private Function CullBody(body As SPoint) As Boolean
        If bolCulling Then
            If body.X + FinalOffset.X < 0 Or body.X + FinalOffset.X > Form1.Render.Width / pic_scale Or body.Y + FinalOffset.Y < 0 Or body.Y + FinalOffset.Y > Form1.Render.Height / pic_scale Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Function FollowBodyLoc(Balls() As Body_Struct) As SPoint
        For Each b As Body_Struct In Balls
            If b.UID = FollowGUID Then
                Return New SPoint(b.LocX, b.LocY)


            End If


        Next
    End Function
    Public Function ScaledPoint(Point As Point, Origin As Point, Optional Scale As Double = 1.0) As Point
        Return New Point(Origin.X + Point.X * Scale, Origin.Y + Point.Y * Scale)
    End Function
    Public Function GetFinalOffset() As SPoint
        Return New SPoint(RelBallPosMod.X + ScaleOffset.X, RelBallPosMod.Y + ScaleOffset.Y)
    End Function
End Module
