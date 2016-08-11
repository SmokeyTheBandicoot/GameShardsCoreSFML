Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System

Public Class SFMLTextbox
    Inherits TextBox

    Dim ut As New Utils
    Dim r As New RectangleShape

    Private _IsActive As Boolean = False
    Private _ColorReadonly As New Color(196, 196, 196)
    Private _ColorDisabled As New Color(128, 128, 128)
    Private _Backcolor As New Color(255, 255, 255)
    Private _BorderColor As New Color(0, 0, 0)
    Private _BorderColorFocused As New Color(128, 128, 128)
    Private _DisplayText As New Text
    Private _SFMLFont As Font
    Private _SFMLFontSize As Single
    Private _ID As Long
    Private _IDStr As String


    Public Property IsActive As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
        End Set
    End Property

    Public Property ColorReadonly As Color
        Get
            Return _ColorReadonly
        End Get
        Set(ByVal value As Color)
            _ColorReadonly = value
        End Set
    End Property

    Public Property ColorDisabled As Color
        Get
            Return _ColorDisabled
        End Get
        Set(ByVal value As Color)
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

    Public Property SFMLFont As Font
        Get
            Return _SFMLFont
        End Get
        Set(ByVal value As Font)
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

    Public Sub draw(ByRef w As RenderWindow)
        If Visible Then


            If Enabled Then
                If [ReadOnly] Then
                    DisplayText.Color = ColorReadonly
                Else
                    DisplayText.Color = New Color(ut.ConvertColor(ForeColor))
                End If
            Else
                DisplayText.Color = New Color(ut.ConvertColor(ForeColor))
            End If

            Dim textSize As Drawing.Size = TextRenderer.MeasureText(Text, ut.InverseConvertFont(SFMLFont, SFMLFontSize))
            DisplayText = New Text(Text, SFMLFont, _SFMLFontSize)

            Select Case True
                Case TextAlign = Drawing.ContentAlignment.MiddleLeft
                    DisplayText.Position = New Vector2f(Left, (Top + Height / 2) - textSize.Height / 2)
                Case TextAlign = Drawing.ContentAlignment.MiddleCenter
                    DisplayText.Position = New Vector2f((Left + Width / 2) - textSize.Width / 2, (Top + Height / 2) - textSize.Height / 2)
                Case TextAlign = Drawing.ContentAlignment.MiddleRight
                    DisplayText.Position = New Vector2f((Right) - textSize.Width, (Top + Height / 2) - textSize.Height / 2)
            End Select

            r.FillColor = Color.Transparent
            If IsActive Then
                r.OutlineColor = _BorderColorFocused
            Else
                r.OutlineColor = _BorderColor
            End If
            r.Size = New Vector2f(textSize.Width + 6, textSize.Height + 4)
            r.Position = New Vector2f(Location.X, Location.Y)

            w.Draw(r)
            w.Draw(DisplayText)
        End If
    End Sub

    'Public Sub UpdateText(ByVal e As SFML.Window.KeyEventArgs)
    '    Text += e.Code.ToString
    '    If e.Code = SFML.Window.Keyboard.Key.LControl Then
    '        Text = String.Empty
    '    End If
    'End Sub

End Class
