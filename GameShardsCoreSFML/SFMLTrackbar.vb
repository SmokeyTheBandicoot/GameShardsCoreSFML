Imports System.Drawing
Imports System.Windows.Forms
Imports SFML.Graphics
Imports GameShardsCore.Base.Geometry
Imports System.Math
Imports SFML.System
Imports GameShardsCoreSFML

Public Class SFMLTrackbar
    Inherits TrackBar
    Implements ISFMLControl

    Dim GGeom As New Geometry

    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16
    Private _ShowMinMaxValues As Boolean = True
    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _ContentBackColor As New SFML.Graphics.Color(0, 0, 0, 0)
    Private _DotBorderColor As New SFML.Graphics.Color(0, 0, 0, 0)
    Private _DotBackColor As New SFML.Graphics.Color(128, 128, 255)
    Private _TickOffsetY As Integer = 5 'In Pixels
    Private _TickOffsetX As Integer = 5 'In Pixels
    Private _BarThickNess As Integer = 10


    'Private _ShowMin as Boolean
    'Private _ShowMax as boolean
    'Private _ShowValue as boolean

    Private IsClicking As Boolean = False
    Private PrevPoint As New Point

    Dim track As New RectangleShape
    Dim dot As New CircleShape
    Dim min As Text
    Dim max As Text
    Dim val As Text
    'Dim tick As RectangleShape

    Public Property BarThickNess As Integer
        Get
            Return _BarThickNess
        End Get
        Set(value As Integer)
            _BarThickNess = value
        End Set
    End Property

    Public Property TickOffsetY As Integer
        Get
            Return _TickOffsetY
        End Get
        Set(ByVal value As Integer)
            _TickOffsetY = value
        End Set
    End Property

    Public Property TickOffsetX As Integer
        Get
            Return _TickOffsetX
        End Get
        Set(ByVal value As Integer)
            _TickOffsetX = value
        End Set
    End Property

    Public Property DotBorderColor() As SFML.Graphics.Color
        Get
            Return _DotBorderColor
        End Get
        Set(value As SFML.Graphics.Color)
            _DotBorderColor = value
        End Set
    End Property
    Public Property DotBackColor() As SFML.Graphics.Color
        Get
            Return _DotBackColor
        End Get
        Set(value As SFML.Graphics.Color)
            _DotBackColor = value
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
    Public Property ContentBackColor() As SFML.Graphics.Color
        Get
            Return _ContentBackColor
        End Get
        Set(value As SFML.Graphics.Color)
            _ContentBackColor = value
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

    Public ReadOnly Property TickNumber As Integer
        Get
            Return Ceiling(((Maximum - Minimum) / TickFrequency)) + 1
        End Get
    End Property

    Public ReadOnly Property TickDistance As Integer
        Get
            Return Size.Width / Ceiling(((Maximum - Minimum) / TickFrequency))
        End Get
    End Property

    Public ReadOnly Property ValuePercent As Single
        Get
            'Return (Maximum / 100) / (Value + Minimum)
            Return ((Maximum - Minimum) / 100) * Value
        End Get
    End Property

    Public ReadOnly Property ValuePercentPixel As Single
        Get
            Return (Size.Width / 100) * ((Maximum - Minimum) / 100) * Value
        End Get
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
        If GGeom.CheckIfRectangleIntersectsPoint(New Drawing.Rectangle(Left, Top, Width, Height), p) Then
            If IsClicking Then
                If PrevPoint = Nothing Then
                    PrevPoint = p
                Else
                    If Orientation = Orientation.Horizontal Then
                        Value += (PrevPoint.X - p.X) / ValuePercentPixel
                    End If

                End If
            End If

            MyBase.OnMouseHover(New EventArgs)
        End If
    End Sub

    Private Sub ISFMLControl_CheckClick(p As Point) Implements ISFMLControl.CheckClick
        If GGeom.CheckIfRectangleIntersectsPoint(New Rectangle(Left, Top, Width, Height), p) Then
            IsClicking = True
            MyBase.OnClick(New EventArgs)
        End If
    End Sub

    Public Sub CheckClickUp(p As Point) Implements ISFMLControl.CheckClickUp
        IsClicking = False
        MyBase.OnMouseUp(New MouseEventArgs(MouseButtons.Left, 1, p.X, p.Y, 0))
    End Sub


    Public Sub Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then
            If Orientation = Orientation.Horizontal Then

                track = New RectangleShape
                track.Position = New Vector2f(Location.X, Location.Y)
                track.Size = New Vector2f(Size.Width, BarThickNess)
                track.FillColor = ContentBackColor
                track.OutlineColor = BorderColor
                track.OutlineThickness = -1

                dot = New CircleShape((BarThickNess / 2) + 6, 4)
                dot.Position = New Vector2f(Location.X + ValuePercentPixel - dot.Radius, Location.Y - (dot.Radius - BarThickNess / 2))
                dot.FillColor = ContentBackColor
                dot.OutlineColor = BorderColor
                dot.OutlineThickness = -1

                min = New Text(ValuePercent, SFMLFont, SFMLFontSize)
                min.Color = Utils.ConvertColor(ForeColor)
                min.Position = New Vector2f(Location.X, Location.Y + BarThickNess + TickOffsetY + min.GetGlobalBounds.Height + 5)

                w.Draw(track)
                w.Draw(dot)
                w.Draw(min)

                If TickStyle = TickStyle.Both Or TickStyle = TickStyle.BottomRight Then
                    For x = 0 To TickNumber - 1
                        Dim tick As New RectangleShape
                        tick.Position = New Vector2f(Location.X + x * TickDistance, Location.Y + BarThickNess + TickOffsetY)
                        tick.FillColor = BorderColor
                        tick.OutlineThickness = -1
                        tick.OutlineColor = BorderColor
                        tick.Size = New Vector2f(1, 5)

                        w.Draw(tick)
                    Next

                ElseIf TickStyle = TickStyle.Both Or TickStyle = TickStyle.TopLeft Then

                End If

            Else

                If TickStyle = TickStyle.Both Or TickStyle.BottomRight Then

                ElseIf TickStyle = TickStyle.Both Or TickStyle.TopLeft Then

                End If
            End If
        End If
    End Sub
End Class
