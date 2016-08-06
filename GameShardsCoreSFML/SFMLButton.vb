Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System

Public Class SFMLButton
    Inherits Button

    Dim ut As New Utils

    Private _Toggleable As Boolean = False
    Private _IsToggled As Boolean = False
    Private _ToggleChangesSprite As Boolean = False
    Private _ToggleChangesColor As Boolean = True
    Private _SpriteNormal As New Sprite
    Private _SpriteToggled As New Sprite
    Private _ColorNormal As New Color(255, 255, 255, 0)
    Private _ColorToggled As New Color(128, 128, 128, 0)
    Private _DisplayText As New Text()
    Private _SFMLFont As Font
    Private _SFMLFontSize As Single
    Private _Border As List(Of Integer) = {5, 5, 5, 5}.ToList

    'Public Sub New(text As String, size As Drawing.Size, location As Drawing.Point, Spritenorm As Sprite, spriteToggl As Sprite, colornorm As Color, colortoggl As Color, toggleable As Boolean, togglechangessprite As Boolean, togglechangescolor As Boolean, sfmlfont As Font, sfmlfontsize As Single)
    '    text = text
    '    size = size
    '    location = location

    'End Sub

    Public Property ToggleChangesSprite() As Boolean
        Get
            Return _ToggleChangesSprite
        End Get
        Set(value As Boolean)
            _ToggleChangesSprite = value
        End Set
    End Property

    Public Property ToggleChangesColor() As Boolean
        Get
            Return _ToggleChangesColor
        End Get
        Set(value As Boolean)
            _ToggleChangesColor = value
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

    Public Property ColorNormal() As Color
        Get
            Return _ColorNormal
        End Get
        Set(value As Color)
            _ColorNormal = value
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

    Public Property Border As List(Of Integer)
        Get
            Return _Border
        End Get
        Set(value As List(Of Integer))
            _Border = value
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

        SpriteNormal.Scale = New Vector2f(Width / SpriteNormal.Texture.Size.X, Height / SpriteNormal.Texture.Size.Y)
        SpriteToggled.Scale = New Vector2f(Width / SpriteToggled.Texture.Size.X, Height / SpriteToggled.Texture.Size.Y)

        SpriteToggled.Position = New Vector2f(Location.X, Location.Y)
        SpriteNormal.Position = New Vector2f(Location.X, Location.Y)

        If Toggleable Then
            If ToggleChangesColor Then
                If IsToggled Then
                    _SpriteNormal.Color = ColorNormal
                    _SpriteToggled.Color = ColorNormal
                Else
                    _SpriteNormal.Color = ColorToggled
                    _SpriteToggled.Color = ColorToggled
                End If
            End If

            If ToggleChangesSprite Then
                If IsToggled Then
                    w.Draw(_SpriteToggled)
                Else
                    w.Draw(_SpriteNormal)
                End If
            Else
                w.Draw(_SpriteNormal)
            End If
        Else
            w.Draw(_SpriteNormal)
        End If

        Dim textSize As Drawing.Size = TextRenderer.MeasureText(Text, ut.InverseConvertFont(SFMLFont, SFMLFontSize))
        DisplayText = New Text(Text, SFMLFont, _SFMLFontSize)
        DisplayText.Color = New Color(ut.ConvertColor(ForeColor))

        Select Case True
            Case TextAlign = Drawing.ContentAlignment.MiddleLeft
                DisplayText.Position = New Vector2f(Left + Border(0), (Top + Height / 2) - textSize.Height / 2)
            Case TextAlign = Drawing.ContentAlignment.MiddleCenter
                DisplayText.Position = New Vector2f((Left + Width / 2) - textSize.Width / 2, (Top + Height / 2) - textSize.Height / 2)
            Case TextAlign = Drawing.ContentAlignment.MiddleRight
                DisplayText.Position = New Vector2f((Right - Border(2)) - textSize.Width, (Top + Height / 2) - textSize.Height / 2)
        End Select

        w.Draw(DisplayText)
    End Sub
End Class
