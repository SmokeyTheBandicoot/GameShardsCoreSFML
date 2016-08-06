Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System

Public Class SFMLButton
    Inherits Button

    Dim ut As New Utils

    Private _Toggleable As Boolean = False
    Private _IsToggled As Boolean = False
    Private _ToggleChangesSprite As Boolean = False
    Private _SpriteNormal As New Sprite
    Private _SpriteToggled As New Sprite
    Private _DisplayText As New Text()
    Private _SFMLFont As Font
    Private _SFMLFontSize As Single

    Public Property ToggleChangesSprite() As Boolean
        Get
            Return _ToggleChangesSprite
        End Get
        Set(value As Boolean)
            _ToggleChangesSprite = value
        End Set
    End Property
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

    Public Property SpriteNormal() As Sprite
        Get
            Return _SpriteNormal
        End Get
        Set(value As Sprite)
            _SpriteNormal = value
        End Set
    End Property

    Public Property SpriteToggled() As Sprite
        Get
            Return _SpriteToggled
        End Get
        Set(value As Sprite)
            _SpriteToggled = value
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
        _SpriteToggled.Position = ut.PointToVector2F(Location)
        _SpriteNormal.Position = ut.PointToVector2F(Location)
        If Toggleable Then
            If IsToggled Then

                w.Draw(SpriteToggled)
            Else
                w.Draw(SpriteNormal)
            End If
        Else
            w.Draw(SpriteNormal)
        End If

        'Select Case TextAlign
        '    Case Else
        DisplayText = New Text(Text, SFMLFont, _SFMLFontSize)
        DisplayText.Position = New Vector2f(Left + Padding.Left, Top + Height / 2 - Font.Size / 2)
        DisplayText.Color = New Color(ut.ConvertColor(ForeColor))
        w.Draw(DisplayText)
        'End Select
    End Sub
End Class
