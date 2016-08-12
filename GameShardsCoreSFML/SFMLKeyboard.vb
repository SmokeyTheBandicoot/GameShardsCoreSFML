Imports System.Windows.Forms
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports GameShardsCore.Base.Geometry

Public Class SFMLKeyboard
    Inherits Panel

    Private Ut As New Utils
    Dim GGeom As New Geometry

    Private _SizeWH As New Size(32 * 15, 32 * 5)
    Private _Font As New SFML.Graphics.Font("crash-a-like.ttf")
    Private _DifferentColor As Boolean = False
    Private _Colors As New List(Of SFML.Graphics.Color)
    Private _KeyPadding As Byte = 0

    Private _TextColorNormal As New SFML.Graphics.Color(0, 0, 0)
    Private _TextColorToggled As New SFML.Graphics.Color(48, 48, 48)
    Private _TextColorDisabled As New SFML.Graphics.Color(64, 64, 64)
    Private _ColorNormal As New SFML.Graphics.Color(255, 255, 255)
    Private _ColorToggled As New SFML.Graphics.Color(196, 196, 196)
    Private _ColorDisabled As New SFML.Graphics.Color(128, 128, 128)
    Private _BorderColorNormal As New SFML.Graphics.Color(0, 0, 0)
    Private _BorderColorToggled As New SFML.Graphics.Color(0, 0, 0)
    Private _BorderColorDisabled As New SFML.Graphics.Color(0, 0, 0)

    Public ReadOnly KeySize As Size

    Dim Keys As New List(Of SFMLButton)
    Public Shared chars As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList

    Public Property TextColorNormal As SFML.Graphics.Color
        Get
            Return _TextColorNormal
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _TextColorNormal = value
        End Set
    End Property

    Public Property TextColorToggled As SFML.Graphics.Color
        Get
            Return _TextColorToggled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _TextColorToggled = value
        End Set
    End Property

    Public Property TextColorDisabled As SFML.Graphics.Color
        Get
            Return _TextColorDisabled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _TextColorDisabled = value
        End Set
    End Property


    Public Property ColorNormal As SFML.Graphics.Color
        Get
            Return _ColorNormal
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _ColorNormal = value
        End Set
    End Property

    Public Property ColorToggled As SFML.Graphics.Color
        Get
            Return _ColorToggled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _ColorToggled = value
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


    Public Property BorderColorToggled As SFML.Graphics.Color
        Get
            Return _BorderColorToggled
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _BorderColorToggled = value
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

    Public Property BorderColorNormal As SFML.Graphics.Color
        Get
            Return _BorderColorNormal
        End Get
        Set(ByVal value As SFML.Graphics.Color)
            _BorderColorNormal = value
        End Set
    End Property

    Public Property SizeWH As Size
        Get
            Return _SizeWH
        End Get
        Set(ByVal value As Size)
            _SizeWH = value
        End Set
    End Property

    Public Property SFMLFont As SFML.Graphics.Font
        Get
            Return _Font
        End Get
        Set(ByVal value As SFML.Graphics.Font)
            _Font = value
        End Set
    End Property

    Public Property DifferentColor As Boolean
        Get
            Return _DifferentColor
        End Get
        Set(ByVal value As Boolean)
            _DifferentColor = value
        End Set
    End Property

    Public Property Colors As List(Of SFML.Graphics.Color)
        Get
            Return _Colors
        End Get
        Set(ByVal value As List(Of SFML.Graphics.Color))
            _Colors = value
        End Set
    End Property

    Public Property KeyPadding As Byte
        Get
            Return _KeyPadding
        End Get
        Set(ByVal value As Byte)
            _KeyPadding = value
        End Set
    End Property

    Public Sub New(location As Vector2f, size As Vector2f)
        For y = 0 To 4
            For x = 0 To 14
                Dim b As New SFMLButton
                With b
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = chars(y * 14 + x)
                    '.SFMLFont = New SFML.Graphics.Font(Font)
                    .SFMLFont = Ut.ConvertFont(New Drawing.Font("arial", 11))
                    .SFMLFontSize = Font.SizeInPoints
                    '.Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
                    .Toggleable = True
                    .ToggleChangesSprite = False
                    .ToggleChangesColor = True
                    .Size = New Size(CalculateKeySize.Width, CalculateKeySize.Height)
                    .Location = New Point(location.X + CalculateKeySize.Width * x + x * KeyPadding, location.Y + CalculateKeySize.Height * y + y * KeyPadding)
                    Keys.Add(b)
                End With
            Next
        Next
    End Sub

    Public Function CalculateKeySize() As Size
        Return New Size(((SizeWH.Width - 14 * KeyPadding) \ 15), (SizeWH.Height - 4 * KeyPadding) \ 5)
    End Function

    Public Sub SetKeyToggled(ByVal p As Point)
        For x = 0 To Keys.Count - 1
            Keys(x).IsToggled = False
            If GGeom.CheckIfRectangleIntersectsPoint(Keys(x).Bounds, p) Then
                Keys(x).IsToggled = True
            End If
        Next

    End Sub

    Public Sub draw(ByRef w As RenderWindow)
        'Buttons

        'SFMLFont = New SFML.Graphics.Font(SFMLFont)
        Dim ft As New SFML.Graphics.Font(Ut.ConvertFont(Font))
        For x = 0 To Keys.Count - 1
            Dim r As New RectangleShape
            Dim t As New Text(chars(x), ft)

            t.CharacterSize = Font.SizeInPoints
            t.Position = New Vector2f(Keys(x).Location.X + 2, Keys(x).Location.Y + 2)
            r.Size = Ut.PointToVector2F(New Point(Keys(x).Size.Width, Keys(x).Size.Height))
            r.Position = New Vector2f(Keys(x).Location.X, Keys(x).Location.Y)
            r.OutlineThickness = 1

            If Enabled Then
                If Keys(x).IsToggled Then
                    r.FillColor = ColorToggled
                    r.OutlineColor = BorderColorToggled
                    t.Color = TextColorToggled
                Else
                    r.FillColor = ColorNormal
                    r.OutlineColor = BorderColorNormal
                    t.Color = TextColorNormal
                End If
            Else
                r.FillColor = ColorDisabled
                r.OutlineColor = BorderColorDisabled
                t.Color = TextColorDisabled
            End If

            w.Draw(r)
            w.Draw(t)
        Next
    End Sub
End Class
