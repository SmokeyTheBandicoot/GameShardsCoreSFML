Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System
Imports GameShardsCore.Base.Geometry
Imports System.Drawing
Imports GameShardsCoreSFML

Public Class SFMLTextbox
    Inherits TextBox
    Implements ISFMLControl

    Dim GGeom As New Geometry
    Dim r As New RectangleShape

    'Used for max length
    Dim c() As Char = Text.ToCharArray

    'Properties
    Private _OutlineTickness As Integer = -1
    Private _IsActive As Boolean = False
    Private _ColorReadonly As New SFML.Graphics.Color(196, 196, 196)
    Private _ColorDisabled As New SFML.Graphics.Color(128, 128, 128)
    Private _Backcolor As New SFML.Graphics.Color(255, 255, 255)
    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _BorderColorFocused As New SFML.Graphics.Color(128, 128, 128)
    Private _DisplayText As New Text
    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single
    Private _ID As Long
    Private _IDStr As String
    Private _BoundKeyboard As SFMLKeyboard
    Private _TextOffset As Vector2f = New Vector2f(0, 0)

    Public Property OutlineTickness As Integer
        Get
            Return _OutlineTickness
        End Get
        Set(value As Integer)
            _OutlineTickness = value
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

    Public Property BoundKeyboard As SFMLKeyboard
        Get
            Return _BoundKeyboard
        End Get
        Set(ByVal value As SFMLKeyboard)
            _BoundKeyboard = value
        End Set
    End Property

    Public Property IsActive As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
        End Set
    End Property

    Public Property ColorReadonly As SFML.Graphics.Color
        Get
            Return _ColorReadonly
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _ColorReadonly = value
        End Set
    End Property

    Public Property ColorDisabled As SFML.Graphics.Color
        Get
            Return _ColorDisabled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _ColorDisabled = value
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

    Sub SetActive(ByVal p As Drawing.Point)
        If GGeom.CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(r.GetGlobalBounds), p) OrElse (GGeom.CheckIfRectangleIntersectsPoint(BoundKeyboard.Bounds, p) AndAlso IsActive = True) Then
            IsActive = True
        Else
            IsActive = False
        End If
    End Sub

    Private Sub ISFMLControl_Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then

            r = New RectangleShape

            If MaxLength > -1 AndAlso Text.Length > MaxLength Then
                'ReDim Preserve c(MaxLength - 1)
                'Text = (String.Join("", c))
                Text = Text.Remove(MaxLength)
            End If

            DisplayText = New Text(Text, SFMLFont, _SFMLFontSize)

            If Enabled Then
                If IsActive Then
                    DisplayText.Color = ColorReadonly
                Else
                    DisplayText.Color = New SFML.Graphics.Color(Utils.ConvertColor(ForeColor))
                End If
            Else
                DisplayText.Color = New SFML.Graphics.Color(Utils.ConvertColor(ForeColor))
            End If



            'Dim textSize As New FloatRect()
            'textSize = 

            'Select Case True
            '    Case TextAlign = Drawing.ContentAlignment.MiddleLeft
            '        DisplayText.Position = New Vector2f(Left, (Top + Height / 2) - textSize.Height / 2)
            '    Case TextAlign = Drawing.ContentAlignment.MiddleCenter
            '        DisplayText.Position = New Vector2f((Left + Width / 2) - textSize.Width / 2, (Top + Height / 2) - textSize.Height / 2)
            '    Case TextAlign = Drawing.ContentAlignment.MiddleRight
            '        DisplayText.Position = New Vector2f((Right) - textSize.Width, (Top + Height / 2) - textSize.Height / 2)
            'End Select



            'DisplayText.Position = New Vector2f(Location.X, Location.Y)

            r.FillColor = SFML.Graphics.Color.Transparent
            r.OutlineThickness = OutlineTickness
            If IsActive Then
                r.OutlineColor = _BorderColorFocused
            Else
                r.OutlineColor = _BorderColor
            End If

            'DisplayText.Position = Common.GetPositionHorizontal(TextAlign, DisplayText.GetLocalBounds, New FloatRect(Left, Top, Width, Height), New Vector2f(0 + TextOffset.X, -DisplayText.GetGlobalBounds.Height / 4 + TextOffset.Y))
            DisplayText.Position = Common.GetPosition(Common.ConvertHorizontalAlignToContentAlign(TextAlign), DisplayText.GetGlobalBounds, New FloatRect(Left, Top, Width, Height), New Vector2f(0 + TextOffset.X, 0 + TextOffset.Y))

            Size = New Size(Size.Width, DisplayText.GetLocalBounds.Height + SFMLFontSize / 4)

            r.Size = New Vector2f(Size.Width, DisplayText.GetLocalBounds.Height + SFMLFontSize / 4)
            r.Position = New Vector2f(Location.X, Location.Y)

            w.Draw(r)
            w.Draw(DisplayText)

            If IsActive Then
                'MsgBox("is active!")
                BoundKeyboard.DrawToBoundTextbox(w)
            End If
        End If
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
