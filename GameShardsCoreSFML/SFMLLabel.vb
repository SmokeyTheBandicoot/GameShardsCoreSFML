Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System

Public Class SFMLLabel
    Inherits Label

    Dim ut As New Utils

    Public Shadows Autosize As Boolean = False
    Private _Toggleable As Boolean = False
    Private _IsToggled As Boolean = False
    Private _ColorToggled As New Color(128, 128, 128, 0)
    Private _DisplayText As New Text()
    Private _SFMLFont As Font
    Private _SFMLFontSize As Single

    Public Property Toggleable() As Boolean
        Get
            Return _Toggleable
        End Get
        Set(value As Boolean)
            _Toggleable = value
        End Set
    End Property

    Public Property IsToggled() As Boolean
        Get
            Return _IsToggled
        End Get
        Set(value As Boolean)
            _IsToggled = value
        End Set
    End Property

    Public Property ColorToggled() As Color
        Get
            Return _ColorToggled
        End Get
        Set(value As Color)
            _ColorToggled = value
        End Set
    End Property

    Public Property DisplayText() As Text
        Get
            Return _DisplayText
        End Get
        Set(value As Text)
            _DisplayText = value
        End Set
    End Property

    Public Property SFMLFont() As Font
        Get
            Return _SFMLFont
        End Get
        Set(value As Font)
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

    Public Sub Draw(ByRef w As RenderWindow)

        If Visible Then

            Dim textSize As Drawing.Size = TextRenderer.MeasureText(Text, ut.InverseConvertFont(SFMLFont, SFMLFontSize))
            DisplayText = New Text(Text, SFMLFont, _SFMLFontSize)

            If Toggleable Then
                If IsToggled Then
                    DisplayText.Color = ColorToggled
                Else
                    DisplayText.Color = New Color(ut.ConvertColor(ForeColor))
                End If
            End If


            Select Case True
                Case TextAlign = Drawing.ContentAlignment.MiddleLeft
                    DisplayText.Position = New Vector2f(Left, (Top + Height / 2) - textSize.Height / 2)
                Case TextAlign = Drawing.ContentAlignment.MiddleCenter
                    DisplayText.Position = New Vector2f((Left + Width / 2) - textSize.Width / 2, (Top + Height / 2) - textSize.Height / 2)
                Case TextAlign = Drawing.ContentAlignment.MiddleRight
                    DisplayText.Position = New Vector2f(Right - textSize.Width, (Top + Height / 2) - textSize.Height / 2)
            End Select


            w.Draw(DisplayText)
        End If

    End Sub
End Class
