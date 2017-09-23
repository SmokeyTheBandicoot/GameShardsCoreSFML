Imports SFML.Graphics
Imports SFML.System
Imports System.Drawing
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D
Imports System.Windows.Forms

Public Class SFMLGroupbox
    Inherits GroupBox
    Implements ISFMLControl

    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _BorderColorDisabled As New SFML.Graphics.Color(128, 128, 128)
    Private _BoxColor As New SFML.Graphics.Color(0, 0, 0, 0)
    Private _BoxColorDisabled As New SFML.Graphics.Color(128, 128, 128, 0)
    Private _ControlBackColor As New SFML.Graphics.Color(255, 255, 255)
    Private _ControlBackColorDisabled As New SFML.Graphics.Color(64, 64, 64)
    Private _BoxBackColor As New SFML.Graphics.Color(255, 255, 255)
    Private _BoxBackColorDisabled As New SFML.Graphics.Color(189, 189, 189)
    Private _Forecolor As New SFML.Graphics.Color(0, 0, 0)
    Private _ForecolorDisabled As New SFML.Graphics.Color

    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16

    Private _ControlOutlineTickness As Integer = -1
    Private _BoxOutlineTickness As Integer = -1

    Dim control As RectangleShape
    Dim box As RectangleShape
    Dim t As New Text

    Public Property SFMLFont As SFML.Graphics.Font
        Get
            Return _SFMLFont
        End Get
        Set(ByVal value As SFML.Graphics.Font)
            _SFMLFont = value
        End Set
    End Property

    Public Property SFMLFontSize As Single
        Get
            Return _SFMLFontSize
        End Get
        Set(ByVal value As Single)
            _SFMLFontSize = value
        End Set
    End Property

    Public Property ControlOutlineTickness As Integer
        Get
            Return _ControlOutlineTickness
        End Get
        Set(ByVal value As Integer)
            _ControlOutlineTickness = value
        End Set
    End Property

    Public Property BoxOutlineTickness As Integer
        Get
            Return _BoxOutlineTickness
        End Get
        Set(ByVal value As Integer)
            _BoxOutlineTickness = value
        End Set
    End Property

    Public Property BorderColor As SFML.Graphics.Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _BorderColor = value
        End Set
    End Property

    Public Property BorderColorDisabled As SFML.Graphics.Color
        Get
            Return _BorderColorDisabled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _BorderColorDisabled = value
        End Set
    End Property

    Public Property BoxColor As SFML.Graphics.Color
        Get
            Return _BoxColor
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _BoxColor = value
        End Set
    End Property

    Public Property BoxColorDisabled As SFML.Graphics.Color
        Get
            Return _BoxColorDisabled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _BoxColorDisabled = value
        End Set
    End Property

    Public Property ControlBackColor As SFML.Graphics.Color
        Get
            Return _ControlBackColor
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _ControlBackColor = value
        End Set
    End Property

    Public Property ControlBackColorDisabled As SFML.Graphics.Color
        Get
            Return _ControlBackColorDisabled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _ControlBackColorDisabled = value
        End Set
    End Property

    Public Property BoxBackColor As SFML.Graphics.Color
        Get
            Return _BoxBackColor
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _BoxBackColor = value
        End Set
    End Property

    Public Property BoxBackColorDisabled As SFML.Graphics.Color
        Get
            Return _BoxBackColorDisabled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _BoxBackColorDisabled = value
        End Set
    End Property

    Public Shadows Property Forecolor As SFML.Graphics.Color
        Get
            Return _Forecolor
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _Forecolor = value
        End Set
    End Property

    Public Property ForecolorDisabled As SFML.Graphics.Color
        Get
            Return _ForecolorDisabled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _ForecolorDisabled = value
        End Set
    End Property


    Private _Z As Integer
    Public Property Z As Integer Implements ISFMLControl.Z
        Get
            Return _Z
        End Get
        Set(value As Integer)
            _Z = value
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
        If Visible Then

            control = New RectangleShape
            box = New RectangleShape

            t = New Text(Text, SFMLFont, SFMLFontSize)
            t.Color = New SFML.Graphics.Color(Forecolor)

            'DisplayText.Position = New Vector2f(Location.X, Location.Y)

            control.OutlineThickness = ControlOutlineTickness
            box.OutlineThickness = BoxOutlineTickness

            If Enabled Then
                control.FillColor = ControlBackColor
                control.OutlineColor = BorderColor
                box.FillColor = BoxBackColor
                box.OutlineColor = BoxColor
            Else
                control.FillColor = ControlBackColorDisabled
                control.OutlineColor = BorderColorDisabled
                box.FillColor = BoxBackColorDisabled
                box.OutlineColor = BoxColorDisabled
            End If

            'box.FillColor = BoxColor

            t.Position = New Vector2f(Location.X + 16, Location.Y) 'Common.GetPosition(TextAlign, DisplayText.GetGlobalBounds, New FloatRect(r.Position.X + r.Size.X, r.Position.Y, DisplayText.GetGlobalBounds.Width, DisplayText.GetGlobalBounds.Height), New Vector2f(0 + TextOffset.X + BoxSize.Width+3, 0 + TextOffset.Y))

            'Size = New Size(Size.Width, Size.Height)

            box.Size = New Vector2f(t.GetGlobalBounds.Width + 4, t.GetGlobalBounds.Height + 4)
            box.Position = New Vector2f(Location.X + 14, Location.Y - 2)

            control.Size = New Vector2f(Size.Width, Size.Height - box.Size.Y / 2)
            control.Position = New Vector2f(Location.X, Location.Y + box.Size.Y / 2)

            w.Draw(control)
            w.Draw(box)
            w.Draw(t)

        End If
    End Sub
End Class
