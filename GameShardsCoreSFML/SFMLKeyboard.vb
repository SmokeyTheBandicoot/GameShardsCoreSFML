Imports System.Windows.Forms
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports GameShardsCore.Base.Geometry

Public Class SFMLKeyboard
    Inherits Panel

    Private Ut As New Utils
    Dim GGeom As New Geometry

    'General
    Private _SizeWH As New Size(32 * 16, 32 * 5)
    Private _Font As New SFML.Graphics.Font("crash-a-like.ttf")
    Private _DifferentColor As Boolean = False
    Private _Colors As New List(Of SFML.Graphics.Color)
    Private _KeyPadding As Byte = 0
    Private _CurCharset As New List(Of String)
    Private _BoundToTextBox As Boolean = False

    'UI and keys
    Private _UI As KeyBoardUI = 0
    Private _EnableSpacebar As Boolean = True
    Private _EnableEnter As Boolean = True
    Private _EnableMAIUSC As Boolean = True
    Private _IsUpper As Boolean = False

    'Colors
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
    'Public Shared DefaultCharsLowerCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "!", "[", "]", "è", "°", "a", "s", "d", "f", "g", "h", "j", "k", "l", "_", ":", "{", "}", "ì", "#", "z", "x", "c", "v", "b", "n", "m", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    'Public Shared DefaultCharsUpperCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared DefaultCharsLowerCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "!", "[", "]", "è", "°", "MAIUSC", "s", "d", "f", "g", "h", "j", "k", "l", "_", ":", "{", "}", "ì", "#", "z", "x", "c", "v", "b", "n", "m", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared DefaultCharsUpperCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "MAIUSC", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared SymbolsDownLowerCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared SymbolsDownUpperCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared AlphanumericLowerCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared AlphanumericUpperCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared NumPad As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared LettersOnlyLowerCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared LettersOnlyUpperCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared AlphaNumExtLowerCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared AlphaNumExtUpperCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared LettersOnlyExtLowerCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared LettersOnlyExtUpperCase As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList
    Public Shared UtilitiesOnly As List(Of String) = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "?", "(", ")", "à", "@", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "!", "[", "]", "è", "°", "A", "S", "D", "F", "G", "H", "J", "K", "L", "_", ":", "{", "}", "ì", "#", "Z", "X", "C", "V", "B", "N", "M", ".", ",", "-", ";", "<", ">", "ò", "§", "€", """", "£", "$", "%", "&", "/", "\", "|", "=", "+", "*", "^", "ù", "é"}.ToList

    Public Enum KeyBoardUI As Byte
        ''' <summary>
        ''' All chars
        ''' </summary>
        DefaultUI = 0

        ''' <summary>
        ''' symbols will be drawn between the last letter row and the spacebar row
        ''' </summary>
        SymbolsDown = 1

        ''' <summary>
        ''' draw letters and numbers only, no symbols or utilities
        ''' </summary>
        Alphanumeric = 2

        ''' <summary>
        ''' Numbers only
        ''' </summary>
        NumPad = 3

        ''' <summary>
        ''' Letters Only
        ''' </summary>
        LettersOnly

        ''' <summary>
        ''' Letters, numbers, utilities but no symbols
        ''' </summary>
        AlphanumericExtended

        ''' <summary>
        ''' Numbers and utilities
        ''' </summary>
        NumPadExtended

        ''' <summary>
        ''' Letters and utilities
        ''' </summary>
        LettersOnlyExtended

        ''' <summary>
        ''' Only draw utilities (Copy, Paste,
        ''' </summary>
        UtilitiesOnly

    End Enum

    Public Property BoundToTextbox
        Get
            Return _BoundToTextBox
        End Get
        Set(value)
            _BoundToTextBox = value
        End Set
    End Property
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

    Public Property CurCharset As List(Of String)
        Get
            Return _CurCharset
        End Get
        Set(ByVal value As List(Of String))
            _CurCharset = value
        End Set
    End Property

    Public Property UI As KeyBoardUI
        Get
            Return _UI
        End Get
        Set(ByVal value As KeyBoardUI)
            _UI = value
        End Set
    End Property

    Public Property EnableSpacebar As Boolean
        Get
            Return _EnableSpacebar
        End Get
        Set(ByVal value As Boolean)
            _EnableSpacebar = value
        End Set
    End Property

    Public Property EnableEnter As Boolean
        Get
            Return _EnableEnter
        End Get
        Set(ByVal value As Boolean)
            _EnableEnter = value
        End Set
    End Property

    Public Property EnableMAIUSC As Boolean
        Get
            Return _EnableMAIUSC
        End Get
        Set(ByVal value As Boolean)
            _EnableMAIUSC = value
        End Set
    End Property

    Public Property IsUpper As Boolean
        Get
            Return _IsUpper
        End Get
        Set(ByVal value As Boolean)
            _IsUpper = value
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

    Public Sub New(locat As Vector2f, size As Vector2f, fnt As SFML.Graphics.Font)
        SetKeys(locat, size, 0, fnt)
    End Sub

    Public Sub New(fnt As SFML.Graphics.Font)
        SetKeys(New Vector2f(0, 0), New Vector2f(32 * 15, 32 * 5), 0, fnt)
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

    ''' <summary>
    ''' Checks for the keys and does its work. Remember to do this only if the bound textbox is active. (to set in the program code for each keyboard)
    ''' </summary>
    ''' <param name="p"></param>
    ''' <param name="container"></param>
    Public Sub SetKeyPressed(ByVal p As Point, ByRef container As String)
        For x = 0 To Keys.Count - 1 'Keys.Count - 1
            If GGeom.CheckIfRectangleIntersectsPoint(Keys(x).Bounds, p) Then
                Select Case True
                    Case Keys(x).Text.ToUpper = "BACKSPACE" Or Keys(x).Text = "<--"
                        If container.Length > 0 Then
                            container.Remove(container.Count - 2)
                        End If
                    Case Keys(x).Text.ToUpper = "MAIUSC"
                        IsUpper = (Not IsUpper)
                        SetKeys(New Vector2f(Location.X, Location.Y), New Vector2f(SizeWH.Width, SizeWH.Height), KeyPadding, SFMLFont)
                    Case Keys(x).Text = " " Or Keys(x).Text.ToUpper = "SPACEBAR" Or Keys(x).Text.ToUpper = "SPACE"
                        If EnableSpacebar Then
                            container += " "
                        End If
                    Case Else
                        container += Keys(x).Text
                End Select
            End If
        Next
    End Sub

    Sub SetKeys(locat As Vector2f, sz As Vector2f, padd As Integer, fnt As SFML.Graphics.Font)
        SizeWH = New Size(sz.X, sz.Y)
        Location = New Point(locat.X, locat.Y)
        KeyPadding = padd
        Keys.Clear()
        Keys = Nothing
        Keys = New List(Of SFMLButton)
        CurCharset = SetCharset()
        For y = 0 To 4
            For x = 0 To 14
                Dim b As New SFMLButton
                With b
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = CurCharset(y * 15 + x)
                    '.SFMLFont = New SFML.Graphics.Font(Font)
                    .SFMLFont = fnt
                    .SFMLFontSize = Font.SizeInPoints
                    '.Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
                    .Toggleable = True
                    .ToggleChangesSprite = False
                    .ToggleChangesColor = True
                    .Size = New Size(CalculateKeySize.Width, CalculateKeySize.Height)
                    .Location = New Point(locat.X + CalculateKeySize.Width * x + x * KeyPadding, locat.Y + CalculateKeySize.Height * y + y * KeyPadding)
                    Keys.Add(b)
                End With
            Next
        Next
    End Sub

    Public Function SetCharset() As List(Of String)
        Select Case UI
            Case KeyBoardUI.DefaultUI
                If IsUpper Then
                    Return DefaultCharsUpperCase
                Else
                    Return DefaultCharsLowerCase
                End If
            Case Else
                Return DefaultCharsUpperCase
        End Select
    End Function

    Public Sub Draw(ByRef w As RenderWindow)
        'Update Bounds
        Bounds = New Rectangle(Location.X, Location.Y, SizeWH.Width, SizeWH.Height)

        'SFMLFont = New SFML.Graphics.Font(SFMLFont)
        If Visible AndAlso (Not BoundToTextbox) Then
            Dim ft As New SFML.Graphics.Font(Utils.ConvertFont(Font))
            For x = 0 To Keys.Count - 1
                Dim r As New RectangleShape
                Dim t As New Text(CurCharset(x), ft)

                t.CharacterSize = Font.SizeInPoints
                t.Position = New Vector2f(Keys(x).Location.X + 2, Keys(x).Location.Y + 2)
                r.Size = Utils.PointToVector2F(New Point(Keys(x).Size.Width, Keys(x).Size.Height))
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
        End If
    End Sub

    Public Sub DrawToBoundTextbox(ByVal w As RenderWindow)
        If Visible Then
            Dim ft As New SFML.Graphics.Font(Utils.ConvertFont(Font))
            For x = 0 To Keys.Count - 1
                Dim r As New RectangleShape
                Dim t As New Text(CurCharset(x), ft)

                t.CharacterSize = Font.SizeInPoints
                t.Position = New Vector2f(Keys(x).Location.X + 2, Keys(x).Location.Y + 2)
                r.Size = Utils.PointToVector2F(New Point(Keys(x).Size.Width, Keys(x).Size.Height))
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
        End If
    End Sub
End Class
