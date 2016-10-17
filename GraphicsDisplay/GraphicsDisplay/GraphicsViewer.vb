Imports System
Imports System.IO
Imports System.Drawing.Drawing2D
Imports GeomLib.GeomLib

Public Class GraphicsViewer
    ' data members
    Private arlPoints As New ArrayList
    Private UpperLimit As New Point3D
    Private LowerLimit As New Point3D
    Private TransMat As New Matrix3D

    Private Sub OpenDataFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenDataFileToolStripMenuItem.Click
        TransMat.SetToIdentity()
        ' open the points data file
        ' read the points and store them in the data member
        OpenFileDialog1.Title = "Open Point Data File"
        OpenFileDialog1.Filter = "Point Data Files (*.txt)|*.txt|All Files(*.*)|*.*"
        OpenFileDialog1.FileName = ""
        Dim strFileName As String
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strFileName = OpenFileDialog1.FileName
        Else
            Exit Sub ' if user has cancelled
        End If
        Dim blnFileRead As Boolean
        blnFileRead = PointReader(strFileName)

        If Not blnFileRead Then Exit Sub ' if not a valid data file
        ' set the upper and lower limits 
        LLimit()
        ULimit()
        ' set the default view to 3D
        View3DToolStripMenuItem_Click(sender, e)
        ' refresh the drwing area
        PanelDraw.Invalidate()
    End Sub

    Private Sub ViewFromXToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewFromXToolStripMenuItem.Click
        ' set the drawing tp initial position (looking from Z)
        Dim Mat As New Matrix3D
        Mat = TransMat.GetInverse
        TransformPoints(Mat)
        TransMat.PostMultiplyBY(Mat)
        Mat.SetToIdentity()
        ' create a matrix with the required rotation
        Mat.SetToRotation(90 * Math.PI / 180.0, New Vector3D(0, 1, 0))
        ' and transorm the points
        TransformPoints(Mat)
        ' update the matrix data member
        TransMat.PostMultiplyBY(Mat)
        ' refresh the drwing area
        PanelDraw.Invalidate()
    End Sub

    Private Sub ViewFromYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewFromYToolStripMenuItem.Click
        ' set the drawing tp initial position (looking from Z)
        Dim Mat As New Matrix3D
        Mat = TransMat.GetInverse
        TransformPoints(Mat)
        TransMat.PostMultiplyBY(Mat)
        Mat.SetToIdentity()
        ' create a matrix with the required rotation
        Mat.SetToRotation(90 * Math.PI / 180.0, New Vector3D(1, 0, 0))
        ' and transorm the points
        TransformPoints(Mat)
        ' update the matrix data member
        TransMat.PostMultiplyBY(Mat)
        ' refresh the drwing area
        PanelDraw.Invalidate()
    End Sub

    Private Sub ViewFromZToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewFromZToolStripMenuItem.Click
        ' set the drawing tp initial position (looking from Z)
        Dim Mat As New Matrix3D
        Mat = TransMat.GetInverse
        ' and transorm the points
        TransformPoints(Mat)
        ' update the matrix data member
        TransMat.PostMultiplyBY(Mat)
        Mat.SetToIdentity()
        ' create a matrix with the required rotation
        Mat.SetToRotation(-90 * Math.PI / 180.0, New Vector3D(0, 0, 1))
        ' and transorm the points
        TransformPoints(Mat)
        ' update the matrix data member
        TransMat.PostMultiplyBY(Mat)
        ' refresh the drwing area
        PanelDraw.Invalidate()
    End Sub

    Private Sub View3DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles View3DToolStripMenuItem.Click
        ' set the drawing tp initial position (looking from Z)
        Dim Mat As New Matrix3D
        Mat = TransMat.GetInverse
        TransformPoints(Mat)
        TransMat.PostMultiplyBY(Mat)
        ' create the required rotation matrix, transform the points and 
        ' refresh the drawing area
        Dim xMat As New Matrix3D
        Dim temp As New Matrix3D
        temp.SetToRotation(45 * Math.PI / 180.0, New Vector3D(1, 0, 0))
        xMat.PostMultiplyBY(temp)
        temp.SetToIdentity()
        temp.SetToRotation(135 * Math.PI / 180.0, New Vector3D(0, 1, 0))
        xMat.PostMultiplyBY(temp)
        temp.SetToIdentity()
        temp.SetToRotation(-45 * Math.PI / 180.0, New Vector3D(0, 0, 1))
        xMat.PostMultiplyBY(temp)
        TransformPoints(xMat)
        TransMat.PostMultiplyBY(xMat)
        PanelDraw.Invalidate()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Function PointReader(ByVal FileName As String) As Boolean
        'read the points from the data file and set the points
        ' to the array variable (data member)
        arlPoints.Clear()
        If File.Exists(FileName) Then
            Dim ioFile As New StreamReader(FileName)
            Dim ioLine As String
            ioLine = ioFile.ReadLine
            ' if file exists and a valid file...
            If InStr(ioLine, " ") = 0 Then
                MsgBox("Not a Valid File.", MsgBoxStyle.Information, "Open Data File")
                PointReader = False
                Exit Function
            Else
                If Not (IsNumeric(Trim(Mid(ioLine, 1, InStr(ioLine, " "))))) Then
                    MsgBox("Not a Valid File.", MsgBoxStyle.Information, "Open Data File")
                    PointReader = False
                    Exit Function
                End If
            End If
            Me.Cursor = Cursors.WaitCursor
            ' read line by line and set the points
            While ioLine <> ""
                Dim ptX, ptY, ptZ As Double
                ReadPointFromString(ioLine, ptX, ptY, ptZ)
                Dim Pnt As New Point3D
                Pnt.X = ptX
                Pnt.Y = ptY
                Pnt.Z = ptZ
                arlPoints.Add(Pnt)
                ioLine = ioFile.ReadLine
            End While
            Me.Cursor = Cursors.Default
            Return True
        Else
            Me.Cursor = Cursors.Default
            Return False
        End If
        Me.Cursor = Cursors.Default
        Return False
    End Function

    Private Sub ReadPointFromString(ByVal strPoint As String, ByRef ptX As Double, ByRef ptY As Double, ByRef ptZ As Double)
        ' take a string that consists of x, y and z points
        ' example 50.0  25.0  35.0 (x, y & z separated by spaces)
        Dim intSpacePos As Integer
        intSpacePos = InStr(strPoint, " ")
        ptX = Trim(Mid(strPoint, 1, intSpacePos))
        strPoint = Trim(Mid(strPoint, Trim(intSpacePos)))
        intSpacePos = InStr(strPoint, " ")
        ptY = Trim(Mid(strPoint, 1, intSpacePos))
        strPoint = Trim(Mid(strPoint, Trim(intSpacePos)))
        ptZ = Trim(strPoint)
    End Sub

    Private Sub PanelDraw_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelDraw.Paint
        ' set the lower limit and upper limit of the bounding box
        LLimit()
        ULimit()
        Dim SF As Single
        Dim X1, X2, Y1, Y2 As Single
        X1 = LowerLimit.X * 1.5
        Y1 = LowerLimit.Y * 1.5
        X2 = UpperLimit.X * 1.5
        Y2 = UpperLimit.Y * 1.5
        Dim CX, CY As Single
        CX = X1 + (X2 - X1) / 2
        CY = Y1 + (Y2 - Y1) / 2

        ' calculate the scale factor of the drawing, that is to be displayed
        ' based on the bounding box
        If PanelDraw.Width / (X2 - X1) < PanelDraw.Height / (Y2 - Y1) Then
            SF = Fix((PanelDraw.Width / (X2 - X1)))
            If X2 = 0.0 And X1 = 0.0 Then SF = 1.0
        Else
            SF = Fix((PanelDraw.Height / (Y2 - Y1)))
            If Y2 = 0.0 And Y1 = 0.0 Then SF = 1.0
        End If
        If SF = 0.0 Then SF = 1.0

        Dim sTransform As New Matrix
        ' center the drawing
        e.Graphics.TranslateTransform((PanelDraw.Width / 2 + CX), (PanelDraw.Height / 2 + CY))
        ' we want Y upwards. by default Y points downwards from top left corner
        ' change that to point upwards from bottom left corner
        e.Graphics.RotateTransform(180)
        ' transform to scale
        e.Graphics.ScaleTransform(SF, SF)

        ' draw the required lines
        Dim Pt As New Point3D
        Dim i As Integer = 1
        Dim Pt1 As New Point3D
        For i = 0 To arlPoints.Count - 2
            Pt = arlPoints(i)
            Pt1 = arlPoints(i + 1)
            e.Graphics.DrawLine(Pens.AliceBlue, CSng(Pt.X), CSng(Pt.Y), CSng(Pt1.X), CSng(Pt1.Y))
        Next

    End Sub

    ' find the lower limit of the bounding box (lower left corner)
    Public Sub LLimit()
        Dim Pt As New Point3D
        Dim i As Integer = 0
        For Each Pt In arlPoints
            If i = 0 Then
                LowerLimit.SetPoint(Pt.X, Pt.Y, Pt.Z)
            End If
            If Pt.X < LowerLimit.X Then LowerLimit.X = Pt.X
            If Pt.Y < LowerLimit.Y Then LowerLimit.Y = Pt.Y
            If Pt.Z < LowerLimit.Z Then LowerLimit.Z = Pt.Z
            i += 1
        Next
    End Sub

    ' find the upper limit of the bounding box (upper right corner)
    Public Sub ULimit()
        Dim Pt As New Point3D
        Dim i As Integer = 0
        For Each Pt In arlPoints
            If i = 0 Then
                UpperLimit.SetPoint(Pt.X, Pt.Y, Pt.Z)
            End If
            If Pt.X > UpperLimit.X Then UpperLimit.X = Pt.X
            If Pt.Y > UpperLimit.Y Then UpperLimit.Y = Pt.Y
            If Pt.Z > UpperLimit.Z Then UpperLimit.Z = Pt.Z
            i += 1
        Next
    End Sub

    ' transform the points by the given matrix
    Public Sub TransformPoints(ByVal Mat As Matrix3D)
        Dim Pt As New Point3D
        For Each Pt In arlPoints
            Pt.TransformBy(Mat)
        Next
    End Sub

End Class
