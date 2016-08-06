Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System

Public Class SFMLLabel
    Inherits Label

    Dim ut As New Utils

    Private _DisplayText As New Text()
    Private _SFMLFont As Font
    Private _SFMLFontSize As Single

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

        Dim textSize As System.Drawing.Size = TextRenderer.MeasureText(Text, ut.InverseConvertFont(SFMLFont, SFMLFontSize))
        DisplayText = New Text(Text, SFMLFont, _SFMLFontSize)
        DisplayText.Color = New Color(ut.ConvertColor(ForeColor))

        Select Case True
            Case TextAlign = Drawing.ContentAlignment.MiddleLeft
                DisplayText.Position = New Vector2f(Left + Padding.Left, Top + Height / 2 - Font.Size / 2)
            Case TextAlign = Drawing.ContentAlignment.MiddleCenter
                DisplayText.Position = New Vector2f(Left + Width / 2 - textSize.Width / 2, Top + Height / 2 - textSize.Height / 2)
            Case Else
                DisplayText.Position = New Vector2f(Left, Top)
                MsgBox(TextAlign.ToString)
        End Select

        w.Draw(DisplayText)
    End Sub
End Class
