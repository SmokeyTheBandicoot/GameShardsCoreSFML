Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System
Imports GameShardsCore.Base.Geometry
Imports System.Drawing
Imports GameShardsCoreSFML

Public Class SFMLGrid
    Inherits SFMLPanel
    Implements ISFMLControl

    'Public Backrect As New RectangleShape(New Vector2f(window.Size.X, window.Size.Y))
    'Public GridRect As New RectangleShape(New Vector2f(XBlocks * BlockSize, YBlocks * BlockSize))
    'Public GridColor As New SFML.Graphics.Color
    'Public GridOutlineColor As New SFML.Graphics.Color
    Public r As RectangleShape
    Private _XBlocks As Integer = 25
    Private _YBlocks As Integer = 19
    Private _BlockSize As Size = New Size(32, 32)

    'Dim GridOffSetX As Integer = 6
    'Dim GridOffSetY As Integer = 6

    Dim GGeom As New Geometry

    Public Property XBlocks As Integer
        Get
            Return _XBlocks
        End Get
        Set(value As Integer)
            _XBlocks = value
        End Set
    End Property

    Public Property yBlocks As Integer
        Get
            Return _YBlocks
        End Get
        Set(value As Integer)
            _YBlocks = value
        End Set
    End Property

    Public Property BlockSize As Size
        Get
            Return _BlockSize
        End Get
        Set(value As Size)
            _BlockSize = value
        End Set
    End Property

    Private Sub ISFMLControl_CheckHover(p As Point) Implements ISFMLControl.CheckHover
        If GGeom.CheckIfRectangleIntersectsPoint(New Drawing.Rectangle(Left, Top, Width, Height), p) Then
            MyBase.OnMouseHover(New EventArgs)
        End If
    End Sub

    Private Sub ISFMLControl_CheckClick(p As Point) Implements ISFMLControl.CheckClick
        If GGeom.CheckIfRectangleIntersectsPoint(New Rectangle(Left, Top, Width, Height), p) Then
            MyBase.OnClick(New EventArgs)
        End If
    End Sub

    Public Sub Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then
            For x = 0 To (XBlocks - 1) * BlockSize.Width Step BlockSize.Width
                For y = 0 To (yBlocks - 1) * BlockSize.Height Step BlockSize.Height
                    r = New RectangleShape
                    r.OutlineThickness = OutlineThickness
                    r.Position = New Vector2f(x + Location.X, y + Location.Y)
                    r.Size = New Vector2f(BlockSize.Width, BlockSize.Height)
                    If Enabled Then
                        r.FillColor = ContentColor
                        r.OutlineColor = BorderColor
                    Else
                        r.FillColor = ContentColorDisabled
                        r.OutlineColor = BorderColorDisabled
                    End If

                    w.Draw(r)
                Next
            Next
        End If
    End Sub
End Class
