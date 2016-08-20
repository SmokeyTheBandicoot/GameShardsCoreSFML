Imports SFML.Graphics
Imports System.Windows.Forms
Imports GameShardsCoreSFML
Imports System.Drawing
Imports GameShardsCore.Base.Geometry
Imports SFML.System

Public Class SFMLProgressBar
    Inherits ProgressBar
    Implements ISFMLControl

    Dim GGeom As New Geometry

    Dim Border As New RectangleShape
    'Dim Content As New RectangleShape
    Dim Content() As Vertex
    Dim ln As New RectangleShape
    Dim t As Text

    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _ContentColor() As SFML.Graphics.Color = {SFML.Graphics.Color.Green, SFML.Graphics.Color.Green, SFML.Graphics.Color.Green, SFML.Graphics.Color.Green}
    Private _ContentBackcolor As New SFML.Graphics.Color(255, 255, 255)


    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 0
    Private _FontAutoSize As Boolean = True
    Private _TextOffset As Vector2f = New Vector2f(0, 0)

    Public Property ContentBackcolor As SFML.Graphics.Color
        Get
            Return _ContentBackcolor
        End Get
        Set(value As SFML.Graphics.Color)
            _ContentBackcolor = value
        End Set
    End Property

    Public Property FontAutoSize As Boolean
        Get
            Return _FontAutoSize
        End Get
        Set(value As Boolean)
            _FontAutoSize = value
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

    Public Property contentColor As SFML.Graphics.Color()
        Get
            Return _ContentColor
        End Get
        Set(value As SFML.Graphics.Color())
            _ContentColor = value
        End Set
    End Property

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

    Public Property TextOffset As Vector2f
        Get
            Return _TextOffset
        End Get
        Set(value As Vector2f)
            _TextOffset = value
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

    Public Sub New()
        Border.OutlineThickness = -1
        'Content.OutlineThickness = -1
        Border.FillColor = SFML.Graphics.Color.Transparent
    End Sub

    Private Function GetFontHeight() As Single
        If FontAutoSize Then
            Return Me.Size.Height * 3 / 4
        Else
            Return SFMLFontSize
        End If
    End Function

    Public Sub Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then
            Border = New RectangleShape
            Content = {New Vertex(New Vector2f(Location.X, Location.Y), contentColor(0)), New Vertex(New Vector2f(Location.X + Size.Width * Value / 100, Location.Y), contentColor(1)), New Vertex(New Vector2f(Location.X, Location.Y + Size.Height), contentColor(2)), New Vertex(New Vector2f(Location.X + Size.Width * Value / 100, Location.Y + Size.Height), contentColor(3))}
            t = New Text

            Border.OutlineColor = BorderColor
            Border.FillColor = ContentBackcolor
            'Content.OutlineColor = contentColor
            'Content.FillColor = contentColor

            t.Color = Utils.ConvertColor(ForeColor)

            t.CharacterSize = GetFontHeight()
            t.Font = SFMLFont
            t.DisplayedString = Text

            Border.Size = New Vector2f(Size.Width, Size.Height)
            'Content.Size = New Vector2f(Size.Width * Value / 100, Size.Height)

            Border.Position = New Vector2f(Location.X, Location.Y)
            'Content.Position = New Vector2f(Location.X, Location.Y)

            t.Position = New Vector2f(Left + Size.Width / 2 - t.GetGlobalBounds.Width / 2 + TextOffset.X, Top + Size.Height / 2 - t.GetGlobalBounds.Height / 2 + TextOffset.Y - GetFontHeight() / 4)

            w.Draw(Border)
            'w.Draw(Content)
            w.Draw(Content, 4, RenderStates.Default)
            w.Draw(t)
        End If
        'OnPaint(New PaintEventArgs())
    End Sub

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
End Class
