Imports System.Drawing
Imports System.Windows.Forms
Imports SFML.Graphics
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D
Imports SFML.System

Public Class SFMLToolTip
    Inherits ToolTip
    Implements ISFMLControl

    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _ContentColor As New SFML.Graphics.Color(255, 255, 255)
    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16

    Private ActiveControl As Integer = -1
    Private MouseLoc As Point

    Dim RectList As New List(Of Rectangle)
    Dim StringList As New List(Of String)

    Dim r As RectangleShape
    Dim t As Text

    'Public Sub New()
    '    RectList.Clear()
    '    StringList.Clear()
    'End Sub

    Public Property BorderColor() As SFML.Graphics.Color
        Get
            Return _BorderColor
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderColor = value
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

    Public Overloads Sub setTooltip(Rect As FloatRect, Caption As String)
        RectList.Add(Utils.FloatRectToRect(Rect))
        StringList.Add(Caption)
    End Sub

    Public Overloads Sub setTooltip(Rect As Rectangle, Caption As String)
        RectList.Add(Rect)
        StringList.Add(Caption)
    End Sub

    Public ReadOnly Property location As Point Implements ISFMLControl.location
        Get
            'Do nothing
        End Get
    End Property

    Public ReadOnly Property size As Size Implements ISFMLControl.size
        Get
            'Do nothing
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

    Public Sub CheckClick(p As Point) Implements ISFMLControl.CheckClick
        'Do nothing
    End Sub

    Public Sub CheckClickUp(p As Point) Implements ISFMLControl.CheckClickUp
        'Do nothing
    End Sub

    Public Sub CheckHover(p As Point) Implements ISFMLControl.CheckHover
        If RectList.Count > 0 Then
            For x = 0 To RectList.Count - 1
                If CheckIfRectangleIntersectsPoint(RectList(x), p) Then
                    ActiveControl = x
                    MouseLoc = New Point(p.X, p.Y)
                    Exit For
                Else
                    ActiveControl = -1
                End If
            Next
        End If
    End Sub

    Private Sub ISFMLControl_Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Not ActiveControl = -1 Then
            r = New RectangleShape
            t = New Text(StringList(ActiveControl), SFMLFont, SFMLFontSize)
            't = New Text("ToolTip", SFMLFont, SFMLFontSize)
            t.Color = Utils.ConvertColor(ForeColor)
            t.Position = New Vector2f(MouseLoc.X + 2 + Cursor.Current.Size.Width, MouseLoc.Y + 2)
            r.Position = New Vector2f(MouseLoc.X + Cursor.Current.Size.Width, MouseLoc.Y)
            r.Size = New Vector2f(t.GetGlobalBounds.Width + 4, t.GetGlobalBounds.Height + 8)
            r.FillColor = ContentColor
            r.OutlineColor = BorderColor
            r.OutlineThickness = -1

            w.Draw(r)
            w.Draw(t)
        End If
    End Sub
End Class
