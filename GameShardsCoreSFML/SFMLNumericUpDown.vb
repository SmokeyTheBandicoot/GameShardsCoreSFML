Imports System.Drawing
Imports System.Windows.Forms
Imports SFML.System
Imports SFML.Graphics
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D

Public Class SFMLNumericUpDown
    Inherits NumericUpDown
    Implements ISFMLControl

    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16

    Dim Box As RectangleShape
    Dim t As Text
    Dim Up1 As Text
    Dim Down1 As Text
    Dim Up2 As Text
    Dim Down2 As Text
    Dim Up3 As Text
    Dim Down3 As Text

    Public Property BorderColor As SFML.Graphics.Color
        Get
            Return _BorderColor
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderColor = value
        End Set
    End Property

    Public Property SFMLFont() As SFML.Graphics.Font
        Get
            Return _SFMLFont
        End Get
        Set(value As SFML.Graphics.Font)
            _SFMLFont = value
        End Set
    End Property

    Public Property SFMLFontSize As Single
        Get
            Return _SFMLFontSize
        End Get
        Set(value As Single)
            _SFMLFontSize = value
        End Set
    End Property

    Public Property Z As Integer Implements ISFMLControl.Z
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Private ReadOnly Property ISFMLControl_size As Size Implements ISFMLControl.size
        Get
            Return New Size(Size.Width, Size.Height)
        End Get
    End Property

    Private ReadOnly Property ISFMLControl_location As Point Implements ISFMLControl.location
        Get
            Return New Point(Location.X, Location.Y)
        End Get
    End Property

    Private Sub ISFMLControl_CheckHover(p As Point) Implements ISFMLControl.CheckHover
        If CheckIfRectangleIntersectsPoint(New Drawing.Rectangle(Left, Top, Width, Height), p) Then
            MyBase.OnMouseHover(New EventArgs)
        End If
    End Sub

    Private Sub ISFMLControl_CheckClick(p As Point) Implements ISFMLControl.CheckClick
        If CheckIfRectangleIntersectsPoint(New Rectangle(Left, Top, Width, Height), p) Then
            MyBase.OnClick(New EventArgs)
        End If
    End Sub

    Public Sub CheckClickUp(p As Point) Implements ISFMLControl.CheckClickUp
        MyBase.OnMouseUp(New MouseEventArgs(MouseButtons.Left, 1, p.X, p.Y, 0))
    End Sub

    Public Sub Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        Dim r As New RectangleShape(New Vector2f(Size.Width, Size.Height))
    End Sub
End Class
