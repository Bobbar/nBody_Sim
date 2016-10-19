Module Functions
    Public Function ScaleMousePosRelative(MousePos As SPoint) As SPoint
        Dim CorrectedPos As New SPoint((MousePos.X / pic_scale) - RelBallPosMod.X - ScaleOffset.X, (MousePos.Y / pic_scale) - RelBallPosMod.Y - ScaleOffset.Y)

        Return CorrectedPos
    End Function
    Public Function ScaleMousePosExact(MousePos As SPoint) As SPoint
        Dim CorrectedPos As New SPoint((MousePos.X / pic_scale), (MousePos.Y / pic_scale))

        Return CorrectedPos
    End Function
End Module
