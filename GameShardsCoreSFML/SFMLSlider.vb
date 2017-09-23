Imports System.Drawing
Imports System.Windows.Forms
Imports SFML.Graphics
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D
Imports System.Math
Imports SFML.System
Imports GameShardsCoreSFML

Public Class SFMLSlider
    Inherits TrackBar
    Implements ISFMLControl

    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16
    Private _ShowMinMaxValues As Boolean = True
    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _ContentBackColor As New SFML.Graphics.Color(216, 216, 216)
    Private _DotBorderColor As New SFML.Graphics.Color(0, 0, 0, 0)
    Private _DotBackColor As New SFML.Graphics.Color(128, 128, 255)
    Private _TickOffsetY As Integer = 5 'In Pixels
    Private _TickOffsetX As Integer = 5 'In Pixels
    Private _DefaultValue As Integer = (Maximum - Minimum) / 2
    Private _ShowPercent As Boolean = True
    Private _Text As String


    'Private _ShowMin as Boolean
    'Private _ShowMax as boolean
    'Private _ShowValue as boolean

    Private IsClicking As Boolean = False
    Private PrevPoint As New Point

    Dim track As New RectangleShape
    Dim dot As New RectangleShape
    Dim min As Text
    Dim max As Text
    Dim val As Text
    Dim t As Text

    Public Overrides Property text As String
        Get
            Return _Text
        End Get
        Set(value As String)
            _Text = value
        End Set
    End Property

    Public Property ShowPercent As Boolean
        Get
            Return _ShowPercent
        End Get
        Set(value As Boolean)
            _ShowPercent = value
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
            Return (100 / (Maximum - Minimum) * Value) - (100 / (Maximum - Minimum) * Minimum)
        End Get
    End Property

    Public ReadOnly Property ValuePercentPixel As Single
        Get
            'Return (Size.Width / 100) * ((Maximum - Minimum) / 100) * Value
            Return ((Value / (Maximum - Minimum)) - (Minimum / (Maximum - Minimum))) * Size.Width
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

    Public Function GetValueFromClick(ByVal p As Point) As Integer
        If Orientation = Orientation.Horizontal Then
            Return ((p.X - Left) * 100 / Width) * (Maximum - Minimum) / 100 + Minimum
        Else
            Return ((p.Y - Top) * 100 / Height) * (Maximum - Minimum) / 100 + Minimum
        End If
    End Function

    Private Sub ISFMLControl_CheckHover(p As Point) Implements ISFMLControl.CheckHover
        If CheckIfRectangleIntersectsPoint(New Drawing.Rectangle(Left - 1, Top - 1, Width + 2, Height + 2), p) Then
            If IsClicking Then
                Value = GetValueFromClick(p)
            End If
            MyBase.OnMouseHover(New EventArgs)
        End If
    End Sub

    Private Sub ISFMLControl_CheckClick(p As Point) Implements ISFMLControl.CheckClick
        If CheckIfRectangleIntersectsPoint(New Rectangle(Left - 1, Top - 1, Width + 2, Height + 2), p) Then
            'Main Bar

            IsClicking = True
            Value = GetValueFromClick(p)
            MyBase.OnClick(New EventArgs)
        End If
    End Sub

    Public Sub ISFMLControl_CheckClickUp(p As Point) Implements ISFMLControl.CheckClickUp
        IsClicking = False
        MyBase.OnMouseUp(New MouseEventArgs(MouseButtons.Left, 1, p.X, p.Y, 0))
    End Sub


    Public Sub Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then
            If Orientation = Orientation.Horizontal Then

                t = New Text(Text, SFMLFont, SFMLFontSize)
                t.Color = Utils.ConvertColor(ForeColor)
                t.Position = New Vector2f(Left + Width / 2 - t.GetGlobalBounds.Width / 2, Top + Height / 2 - t.GetGlobalBounds.Height / 2)

                track = New RectangleShape
                track.Position = New Vector2f(Location.X, Location.Y)
                track.Size = New Vector2f(Size.Width, Size.Height)
                track.FillColor = ContentBackColor
                track.OutlineColor = BorderColor
                track.OutlineThickness = -1

                dot = New RectangleShape(New Vector2f(8, Height - 2))
                dot.Position = New Vector2f(Location.X + ValuePercentPixel - 4, Location.Y + 1)
                dot.FillColor = DotBackColor
                dot.OutlineColor = DotBorderColor
                dot.OutlineThickness = -1

                min = New Text(Minimum, SFMLFont, SFMLFontSize)
                min.Color = Utils.ConvertColor(ForeColor)
                min.Position = New Vector2f(Location.X, Location.Y + Size.Height + TickOffsetY + min.GetGlobalBounds.Height + 5)

                max = New Text(Maximum, SFMLFont, SFMLFontSize)
                max.Color = Utils.ConvertColor(ForeColor)
                max.Position = New Vector2f(Location.X + Size.Width - max.GetGlobalBounds.Width, Location.Y + Size.Height + TickOffsetY + min.GetGlobalBounds.Height + 5)

                If ShowPercent Then
                    val = New Text(Value.ToString + " (" + Round(ValuePercent, 1).ToString + "%)", SFMLFont, SFMLFontSize)
                Else
                    val = New Text(Value.ToString, SFMLFont, SFMLFontSize)
                End If

                val.Color = Utils.ConvertColor(ForeColor)
                val.Position = New Vector2f(Location.X + Size.Width / 2 - val.GetGlobalBounds.Width / 2, Location.Y + Size.Height + TickOffsetY + min.GetGlobalBounds.Height + 5)

                w.Draw(track)
                w.Draw(dot)
                w.Draw(min)
                w.Draw(max)
                w.Draw(val)
                w.Draw(t)

            Else



            End If
        End If
    End Sub
End Class

