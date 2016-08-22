Imports System.Drawing
Imports System.Windows.Forms
Imports SFML.Graphics
Imports GameShardsCore.Base.Geometry
Imports System.Math
Imports SFML.System

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



    'Private _ShowMin as Boolean
    'Private _ShowMax as boolean
    'Private _ShowValue as boolean

    Dim track As New RectangleShape
    Dim dot As New CircleShape
    Dim min As Text
    Dim max As Text
    Dim val As Text
    'Dim tick As RectangleShape

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
            Return Ceiling(((Maximum - Minimum) / TickFrequency))
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
            If Orientation = Orientation.Horizontal Then

                If TickStyle = TickStyle.Both Or TickStyle.BottomRight Then
                    For x = 0 To TickNumber - 1
                        Dim tick As New RectangleShape
                        tick.Position = New Vector2f(Location.X + x * Ceiling(((Maximum - Minimum) / TickFrequency)), Location.Y + Size.Height + TickOffsetY)
                    Next
                ElseIf TickStyle = TickStyle.Both Or TickStyle.TopLeft Then

                End If

            Else

                If TickStyle = TickStyle.Both Or TickStyle.BottomRight Then

                ElseIf TickStyle = TickStyle.Both Or TickStyle.TopLeft Then

                End If
            End If
        End If
    End Sub
End Class
