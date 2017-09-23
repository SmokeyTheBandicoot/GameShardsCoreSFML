Imports System.Drawing
Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D
Imports GameShardsCoreSFML

Public Class SFMLPanel
    Inherits Panel
    Implements ISFMLControl

    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _BorderColorDisabled As New SFML.Graphics.Color(64, 64, 64)
    Private _ContentColor As New SFML.Graphics.Color(255, 255, 255)
    Private _ContentColorDisabled As New SFML.Graphics.Color(196, 196, 196)
    Private _OutlineThickness As Integer = -1
    Private _UseSprite As Boolean = False

    Dim r As RectangleShape

    Private _SpriteNormal As New Sprite

    Public Property UseSprite As Boolean
        Get
            Return _UseSprite
        End Get
        Set(value As Boolean)
            _UseSprite = value
        End Set
    End Property
    Public Property OutlineThickness As Integer
        Get
            Return _OutlineThickness
        End Get
        Set(value As Integer)
            _OutlineThickness = value
        End Set
    End Property
    Public Property ContentColorDisabled() As SFML.Graphics.Color
        Get
            Return _ContentColorDisabled
        End Get
        Set(value As SFML.Graphics.Color)
            _ContentColorDisabled = value
        End Set
    End Property

    Public Property ContentColor() As SFML.Graphics.Color
        Get
            Return _ContentColor
        End Get
        Set(value As SFML.Graphics.Color)
            _ContentColor = value
        End Set
    End Property

    Public Property BorderColorDisabled() As SFML.Graphics.Color
        Get
            Return _BorderColorDisabled
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderColorDisabled = value
        End Set
    End Property

    Public Property BorderColor() As SFML.Graphics.Color
        Get
            Return _BorderColor
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderColor = value
        End Set
    End Property

    Public Property SpriteNormal() As Sprite
        Get
            Return _SpriteNormal
        End Get
        Set(value As Sprite)
            If Not value Is Nothing Then
                UseSprite = True
            Else
                UseSprite = False
            End If
            _SpriteNormal = value
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
    Private Sub ISFMLControl_Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then

            r = New RectangleShape(New Vector2f(Size.Width, Size.Height))
            r.Position = New Vector2f(Location.X, Location.Y)
            r.OutlineThickness = OutlineThickness

            If Enabled Then
                r.FillColor = ContentColor
                r.OutlineColor = BorderColor
            Else
                r.FillColor = ContentColorDisabled
                r.OutlineColor = BorderColorDisabled
            End If

            w.Draw(r)

            If UseSprite Then
                SpriteNormal.Scale = New Vector2f(Width / SpriteNormal.Texture.Size.X, Height / SpriteNormal.Texture.Size.Y)
                SpriteNormal.Position = New Vector2f(Location.X, Location.Y)
                w.Draw(SpriteNormal)
            End If
        End If
    End Sub


End Class

