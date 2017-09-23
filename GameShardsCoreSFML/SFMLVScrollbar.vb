Imports System.Drawing
Imports GameShardsCoreSFML
Imports SFML.Graphics
Imports System.Windows.Forms
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D
Imports SFML.System

Public Class SFMLVScrollbar
    Inherits VScrollBar
    Implements ISFMLControl

    Private IsClicking As Boolean = False

    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16
    Private _DotColor As New SFML.Graphics.Color(128, 128, 255)
    Private _ArrowColor As New SFML.Graphics.Color(216, 216, 216, 128)
    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _DivideParts As UInteger = 5
    Private _HoverMarginX As Integer = 100
    Private _HoverMarginY As Integer = 100

    Dim UpSq As RectangleShape
    Dim UpArr As CircleShape
    Dim DownSq As RectangleShape
    Dim DownArr As CircleShape
    Dim back As RectangleShape
    Dim dot As RectangleShape

    Public Property HoverMarginX As Integer
        Get
            Return _HoverMarginX
        End Get
        Set(value As Integer)
            _HoverMarginX = value
        End Set
    End Property

    Public Property HoverMarginY As Integer
        Get
            Return _HoverMarginY
        End Get
        Set(value As Integer)
            _HoverMarginY = value
        End Set
    End Property

    Public Property BorderColor As SFML.Graphics.Color
        Get
            Return _BorderColor
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderColor = value
        End Set
    End Property

    Public Property ArrowColor As SFML.Graphics.Color
        Get
            Return _ArrowColor
        End Get
        Set(value As SFML.Graphics.Color)
            _ArrowColor = value
        End Set
    End Property

    Public Property DotColor As SFML.Graphics.Color
        Get
            Return _DotColor
        End Get
        Set(value As SFML.Graphics.Color)
            _DotColor = value
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

    Public Sub New()
        BackColor = Drawing.Color.FromArgb(255, 255, 255, 255)
    End Sub
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

    Public Property Z As Integer Implements ISFMLControl.Z
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    'Should work
    Public ReadOnly Property ValuePercentPixel As Single
        Get
            'Return (Size.Width / 100) * ((Maximum - Minimum) / 100) * Value
            Return ((Value / (Maximum - Minimum)) - (Minimum / (Maximum - Minimum))) * (Size.Height - DotSize)
        End Get
    End Property

    'Works
    Public ReadOnly Property DotSize As Integer
        Get
            Return Height / ((Maximum - Minimum) / Height)
        End Get
    End Property

    Public Function GetValueFromClick(ByVal p As Point) As Integer
        Dim v As Integer
        v = ((p.Y - Top - DotSize / 2) * 100 / (Height - DotSize)) * (Maximum - Minimum) / 100 + Minimum
        If v < Minimum Then
            Return Minimum
        ElseIf v > Maximum Then
            Return Maximum
        Else
            Return v
        End If
    End Function

    Private Sub ISFMLControl_CheckHover(p As Point) Implements ISFMLControl.CheckHover
        If CheckIfRectangleIntersectsPoint(New Rectangle(Left, Top, Width, Height), p) Then
            MyBase.OnMouseHover(New EventArgs)
        End If

        If CheckIfRectangleIntersectsPoint(New Rectangle(Left - HoverMarginX, Top - HoverMarginY, Width + 2 * HoverMarginX, Height + 2 * HoverMarginY), p) Then
            If IsClicking Then
                Value = GetValueFromClick(p)
            End If
        End If
    End Sub

    Private Sub ISFMLControl_CheckClick(p As Point) Implements ISFMLControl.CheckClick
        If CheckIfRectangleIntersectsPoint(New Rectangle(Left - 1, Top - 1, Width + 2, Height + 2), p) Then
            IsClicking = True
            Value = GetValueFromClick(p)
            MyBase.OnClick(New EventArgs)
        End If

        If CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(UpSq.GetGlobalBounds), p) Then
            If My.Computer.Keyboard.ShiftKeyDown Then
                If Value - LargeChange < Minimum Then
                    Value = Minimum
                Else
                    Value -= LargeChange
                End If
            Else
                If Value - SmallChange < Minimum Then
                    Value = Minimum
                Else
                    Value -= SmallChange
                End If
            End If


        ElseIf CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(DownSq.GetGlobalBounds), p) Then
            If My.Computer.Keyboard.ShiftKeyDown Then
                If Value + LargeChange > Maximum Then
                    Value = Maximum
                Else
                    Value += LargeChange
                End If

            Else

                If Value + SmallChange > Maximum Then
                    Value = Maximum
                Else
                    Value += SmallChange
                End If
            End If
        End If
    End Sub

    Public Sub CheckClickUp(p As Point) Implements ISFMLControl.CheckClickUp
        IsClicking = False
        MyBase.OnMouseUp(New MouseEventArgs(MouseButtons.Left, 1, p.X, p.Y, 0))
    End Sub

    Public Sub Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then
            'Orientation is always vertical
            back = New RectangleShape(New Vector2f(Width, Height))
            back.Position = New Vector2f(Left, Top)
            back.FillColor = Utils.ConvertColor(BackColor)
            back.OutlineColor = BorderColor
            back.OutlineThickness = -1

            UpSq = New RectangleShape(New Vector2f(Width, Width))
            UpSq.Position = New Vector2f(Left, Top - Width)
            UpSq.FillColor = ArrowColor
            UpSq.OutlineColor = BorderColor
            UpSq.OutlineThickness = -1

            DownSq = New RectangleShape(New Vector2f(Width, Width))
            DownSq.Position = New Vector2f(Left, Top + Height)
            DownSq.FillColor = ArrowColor
            DownSq.OutlineColor = BorderColor
            DownSq.OutlineThickness = -1

            dot = New RectangleShape(New Vector2f(Width - 2, DotSize))
            dot.Position = New Vector2f(Location.X + 1, Location.Y + ValuePercentPixel)
            dot.FillColor = DotColor
            dot.OutlineColor = BorderColor
            dot.OutlineThickness = -1

            UpArr = New CircleShape(Width / 2, 3)
            UpArr.Origin = New Vector2f(UpArr.Origin.X + UpArr.Radius, UpArr.Origin.Y + UpArr.Radius)
            UpArr.Position = New Vector2f(UpSq.GetGlobalBounds.Left + UpArr.Radius, UpSq.GetGlobalBounds.Top + UpArr.Radius)
            UpArr.FillColor = Utils.ConvertColor(ForeColor)
            UpArr.OutlineThickness = 0
            UpArr.Rotation = 0

            DownArr = New CircleShape(Width / 2, 3)
            DownArr.Origin = New Vector2f(DownArr.Origin.X + DownArr.Radius, DownArr.Origin.Y + DownArr.Radius)
            DownArr.Position = New Vector2f(DownSq.GetGlobalBounds.Left + DownArr.Radius, DownSq.GetGlobalBounds.Top + Height - DownArr.Radius)
            DownArr.FillColor = Utils.ConvertColor(ForeColor)
            DownArr.OutlineThickness = 0
            DownArr.Rotation = 180

            w.Draw(back)
            w.Draw(UpSq)
            w.Draw(DownSq)
            w.Draw(dot)
            w.Draw(UpArr)
            w.Draw(DownArr)
        End If
    End Sub
End Class
