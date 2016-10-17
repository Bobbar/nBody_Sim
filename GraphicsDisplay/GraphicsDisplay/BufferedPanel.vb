' This class is used as a picture box for drawing the entities
' in graphics viewer.
' this class is derived from panel control.
' double buffering used to avoid flickering
Public Class BufferedPanel
    Inherits Panel

    Public Sub New()
        MyBase.New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                 ControlStyles.DoubleBuffer Or _
                 ControlStyles.ResizeRedraw Or _
                 ControlStyles.UserPaint, _
                 True)
    End Sub
End Class
