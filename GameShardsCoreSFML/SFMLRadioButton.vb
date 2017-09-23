Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D
Imports System.Drawing

Public Class SFMLRadioButton
    Inherits RadioButton
    Implements ISFMLControl


    Private _OutlineTickness As Integer = -1
    Private _DisplayText As New Text
    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16
    Private _ID As Long
    Private _IDStr As String
    Private _TextOffset As Vector2f = New Vector2f(0, 0)

    Private _BorderColorNormal As New SFML.Graphics.Color(0, 0, 0)
    Private _BorderBackColorNormal As New SFML.Graphics.Color(255, 255, 255)
    Private _CheckColorNormal As New SFML.Graphics.Color(0, 0, 0)
    Private _CheckBackColorNormal As New SFML.Graphics.Color(128, 128, 255)

    Private _BorderColorHover As New SFML.Graphics.Color(0, 0, 0)
    Private _BorderBackColorHover As New SFML.Graphics.Color(200, 200, 200)
    Private _CheckColorHover As New SFML.Graphics.Color(64, 64, 255)
    Private _CheckBackColorHover As New SFML.Graphics.Color(100, 100, 100)

    Private _Group As String = "MainGroup"
    Private _BoxSize As New Size(20, 20)
    Private _AutoScale As Boolean = True
    Private _SpriteNormalScale As New Vector2f(1, 1)
    Private _SpriteIndeterminateScale As New Vector2f(1, 1)
    Private _Z As Integer

    'TO DO: Draw box on the right

    Private IsHovered As Boolean = False

    Dim Border As CircleShape
    Dim Dot As CircleShape

    Public Property Group As String
        Get
            Return _Group
        End Get
        Set(value As String)
            _Group = value
        End Set
    End Property

    Public Property Z As Integer Implements ISFMLControl.Z
        Get
            Return _Z
        End Get
        Set(value As Integer)
            _Z = value
        End Set
    End Property

    Public Property BoxSize As Drawing.Size
        Get
            Return _BoxSize
        End Get
        Set(value As Drawing.Size)
            _BoxSize = value
        End Set
    End Property

    Public Property Autoscale As Boolean
        Get
            Return _AutoScale
        End Get
        Set(value As Boolean)
            _AutoScale = value
        End Set
    End Property

    Public Property SpriteNormalScale As Vector2f
        Get
            Return _SpriteNormalScale
        End Get
        Set(value As Vector2f)
            _SpriteNormalScale = value
        End Set
    End Property

    Public Property SpriteIndeterminateScale As Vector2f
        Get
            Return _SpriteIndeterminateScale
        End Get
        Set(value As Vector2f)
            _SpriteIndeterminateScale = value
        End Set
    End Property

    Public Property BorderColornormal As SFML.Graphics.Color
        Get
            Return _BorderColorNormal
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderColorNormal = value
        End Set
    End Property

    Public Property BorderColorHover As SFML.Graphics.Color
        Get
            Return _BorderColorHover
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderColorHover = value
        End Set
    End Property

    Public Property CheckColorNormal As SFML.Graphics.Color
        Get
            Return _CheckColorNormal
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _CheckColorNormal = value
        End Set
    End Property

    Public Property CheckColorHover As SFML.Graphics.Color
        Get
            Return _CheckColorHover
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _CheckColorHover = value
        End Set
    End Property

    Public Property BorderBackColornormal As SFML.Graphics.Color
        Get
            Return _BorderBackColorNormal
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderBackColorNormal = value
        End Set
    End Property

    Public Property BorderBackColorHover As SFML.Graphics.Color
        Get
            Return _BorderBackColorHover
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderBackColorHover = value
        End Set
    End Property

    Public Property CheckBackColorNormal As SFML.Graphics.Color
        Get
            Return _CheckBackColorNormal
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _CheckBackColorNormal = value
        End Set
    End Property

    Public Property CheckBackColorHover As SFML.Graphics.Color
        Get
            Return _CheckBackColorHover
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _CheckBackColorHover = value
        End Set
    End Property

    Public Property DisplayText As Text
        Get
            Return _DisplayText
        End Get
        Set(ByVal value As Text)
            _DisplayText = value
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

    Public Property ID As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            _ID = value
        End Set
    End Property

    Public Property IDStr As String
        Get
            Return _IDStr
        End Get
        Set(ByVal value As String)
            _IDStr = value
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

    Public Property OutlineTickness As Integer
        Get
            Return _OutlineTickness
        End Get
        Set(value As Integer)
            _OutlineTickness = value
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

    Public Sub New()
        AutoSize = True
    End Sub

    Public Sub ChangeRadioState()
        Me.Checked = (Not Checked)
    End Sub

    Private Sub ISFMLControl_Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then

            Border = New CircleShape
            Dot = New CircleShape

            DisplayText = New Text(Text, SFMLFont, SFMLFontSize)
            DisplayText.Color = New SFML.Graphics.Color(Utils.ConvertColor(ForeColor))

            Border.FillColor = New SFML.Graphics.Color(Utils.ConvertColor(BackColor))

            If IsHovered Then
                Border.OutlineColor = New SFML.Graphics.Color(BorderColorHover)
            Else
                Border.OutlineColor = New SFML.Graphics.Color(BorderColornormal)
            End If

            Border.OutlineThickness = OutlineTickness

            If IsHovered Then
                Border.FillColor = BorderBackColorHover
                Border.OutlineColor = BorderColorHover
                Dot.FillColor = CheckBackColorHover
                Dot.OutlineColor = CheckColorHover
            Else
                Border.FillColor = BorderBackColornormal
                Border.OutlineColor = BorderColornormal
                Dot.FillColor = CheckBackColorNormal
                Dot.OutlineColor = CheckColorNormal
            End If

            DisplayText.Position = New Vector2f(Location.X + BoxSize.Width + 3, Location.Y + BoxSize.Height / 2 - DisplayText.GetGlobalBounds.Height / 2) 'Common.GetPosition(TextAlign, DisplayText.GetGlobalBounds, New FloatRect(r.Position.X + r.Size.X, r.Position.Y, DisplayText.GetGlobalBounds.Width, DisplayText.GetGlobalBounds.Height), New Vector2f(0 + TextOffset.X + BoxSize.Width+3, 0 + TextOffset.Y))

            If AutoSize Then
                Size = New Size(DisplayText.GetGlobalBounds.Width + BoxSize.Width + 3, DisplayText.GetGlobalBounds.Height)
            Else
                Size = New Size(Size.Width, DisplayText.GetGlobalBounds.Height)
            End If

            Border.Radius = BoxSize.Width / 2
            Border.Position = New Vector2f(Location.X, Location.Y)

            Dot.Radius = BoxSize.Width / 2 - 3
            Dot.Position = New Vector2f(Location.X + 3, Location.Y + 3)

            w.Draw(Border)
            If Checked Then
                w.Draw(Dot)
            End If
            w.Draw(DisplayText)
        End If
    End Sub

    Private Sub ISFMLControl_CheckHover(p As Point) Implements ISFMLControl.CheckHover
        If CheckIfRectangleIntersectsPoint(New Drawing.Rectangle(Location.X, Location.Y, Size.Width, Size.Height), p) Then
            IsHovered = True
            MyBase.OnMouseHover(New EventArgs)
        Else
            IsHovered = False
        End If
    End Sub

    Private Sub ISFMLControl_CheckClick(p As Point) Implements ISFMLControl.CheckClick
        If CheckIfRectangleIntersectsPoint(New Drawing.Rectangle(Location.X, Location.Y, Size.Width, Size.Height), p) Then 'Or CheckIfRectangleIntersectsPoint(New Rectangle(r.Position.X, r.Position.Y, r.Size.X, r.Size.Y), p) Then
            ChangeRadioState()
        End If
    End Sub

    Public Sub CheckClickUp(p As Point) Implements ISFMLControl.CheckClickUp
        MyBase.OnMouseUp(New MouseEventArgs(MouseButtons.Left, 1, p.X, p.Y, 0))
    End Sub
End Class