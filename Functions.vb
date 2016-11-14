Imports System.Math
Module Functions
    Public bolTrails As Boolean = False
    Public bolDraw As Boolean = True
    Public bolLines As Boolean = False
    Public bolSOI As Boolean = False
    Public bolAntiAliasing As Boolean = True
    Public bolInvert As Boolean = False
    Public bolShawdow As Boolean = False
    Public bolStopWorker As Boolean = False
    Public stpw As New Stopwatch
    Public Function ScaleMousePosRelative(MousePos As SPoint) As SPoint
        Dim CorrectedPos As New SPoint((MousePos.X / pic_scale) - RelBallPosMod.X - ScaleOffset.X, (MousePos.Y / pic_scale) - RelBallPosMod.Y - ScaleOffset.Y)
        Return CorrectedPos
    End Function
    Public Function ScaleMousePosExact(MousePos As SPoint) As SPoint
        Dim CorrectedPos As New SPoint((MousePos.X / pic_scale), (MousePos.Y / pic_scale))
        Return CorrectedPos
    End Function
    Public Function GetDistance(PointA As SPoint, PointB As SPoint) As Single
        Return Math.Sqrt((PointA.X - PointB.X) ^ 2 + (PointA.Y - PointB.Y) ^ 2)
    End Function
    Public Function GetDistanceOfBalls(BallA As BallParms, BallB As BallParms) As Single
        Return Math.Sqrt((BallA.LocX - BallB.LocX) ^ 2 + (BallA.LocY - BallB.LocY) ^ 2)
    End Function
    Public Sub StartTimer()
        stpw.Stop()
        stpw.Reset()
        stpw.Start()
    End Sub
    Public Sub StopTimer()
        stpw.Stop()
        Debug.Print("Stopwatch: MS:" & stpw.ElapsedMilliseconds & " Ticks: " & stpw.ElapsedTicks)
    End Sub
    Public Sub SetColors(parent As Control)
        parent.BackColor = colBackColor
        For Each ctl As Control In parent.Controls
            If Not TypeOf ctl Is Label Then
                If TypeOf ctl Is Panel Then SetColors(ctl)
                ctl.ForeColor = colControlForeColor
                ctl.BackColor = colBackColor
            End If
        Next
    End Sub
    Public Function ObjectInsideTarget(Target_Loc As SPoint, Target_Size As Single, Object_Loc As SPoint) As Boolean
        Dim DistX As Double
        Dim DistY As Double
        Dim Dist As Double
        Dim DistSqrt As Double

        DistX = Target_Loc.X - Object_Loc.X
        DistY = Target_Loc.Y - Object_Loc.Y
        Dist = (DistX * DistX) + (DistY * DistY)
        DistSqrt = Sqrt(Dist)

        If DistSqrt <= Target_Size Then Return True
        Return False
    End Function
End Module
