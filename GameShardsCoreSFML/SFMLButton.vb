﻿Imports System.Windows.Forms
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
    Private _ColorDisabled As New Color(50, 50, 50, 0)
    Private _DrawBorder As Boolean = False
    Private _BorderColorNormal As New Color(0, 0, 0)
    Private _BorderColorToggled As New Color(0, 0, 0)
    Private _BorderColorDisabled As New Color(0, 0, 0)
    Dim r As New RectangleShape
    Private _DisplayText As New Text()
    Private _SFMLFont As Font
    Private _SFMLFontSize As Single
    'Private _TextOffset As Vector2f = New Vector2f(0, SFMLFontSize / 2)
    ''Private _Border As List(Of Integer) = {5, 5, 5, 5}.ToList
    ''Private _AutoPadding As Boolean
    Private _ID As Long
    Private _IDStr As String
    Private _TextOffset As Vector2f = New Vector2f(0, -SFMLFontSize / 2)

    Public Property TextOffset As Vector2f
        Get
            Return _TextOffset
        End Get
        Set(value As Vector2f)
            _TextOffset = value
        End Set
    End Property

    Public Property DrawBorder As Boolean
        Get
            Return _DrawBorder
        End Get
        Set(ByVal value As Boolean)
            _DrawBorder = value
        End Set
    End Property

    Public Property BorderColorNormal As Color
        Get
            Return _BorderColorNormal
        End Get
        Set(ByVal value As Color)
            _BorderColorNormal = value
        End Set
    End Property

    Public Property BorderColorToggled As Color
        Get
            Return _BorderColorToggled
        End Get
        Set(ByVal value As Color)
            _BorderColorToggled = value
        End Set
    End Property

    Public Property BorderColorDisabled As Color
        Get
            Return _BorderColorDisabled
        End Get
        Set(ByVal value As Color)
            _BorderColorDisabled = value
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

    'Public Property AutoPadding() As Boolean
    '    Get
    '        Return _AutoPadding
    '    End Get
    '    Set(value As Boolean)
    '        _AutoPadding = value
    '    End Set
    'End Property

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

    Public Property ColorDisabled() As Color
        Get
            Return _ColorDisabled
        End Get
        Set(value As Color)
            _ColorDisabled = value
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

    'Public Property Border As List(Of Integer)
    '    Get
    '        Return _Border
    '    End Get
    '    Set(value As List(Of Integer))
    '        _Border = value
    '    End Set
    'End Property

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

    Public Function CalculateSizeFromText() As Drawing.Size
        Return TextRenderer.MeasureText(Text, Font)
    End Function

    Public Sub Draw(ByRef w As RenderWindow)
        If Visible Then

            Bounds = New Drawing.Rectangle(Location.X, Location.Y, Size.Width, Size.Height)

            r = New RectangleShape(New Vector2f(Width, Height))
            r.Position = New Vector2f(Location.X, Location.Y)


            SpriteNormal.Scale = New Vector2f(Width / SpriteNormal.Texture.Size.X, Height / SpriteNormal.Texture.Size.Y)
            SpriteToggled.Scale = New Vector2f(Width / SpriteToggled.Texture.Size.X, Height / SpriteToggled.Texture.Size.Y)

            SpriteToggled.Position = New Vector2f(Location.X, Location.Y)
            SpriteNormal.Position = New Vector2f(Location.X, Location.Y)

            If Toggleable Then
                If Enabled Then

                    If ToggleChangesColor Then
                        If IsToggled Then
                            _SpriteNormal.Color = ColorNormal
                            _SpriteToggled.Color = ColorNormal
                            r.OutlineColor = _BorderColorNormal
                        Else
                            _SpriteNormal.Color = ColorToggled
                            _SpriteToggled.Color = ColorToggled
                            r.OutlineColor = _BorderColorToggled
                        End If
                    End If
                Else
                    _SpriteNormal.Color = ColorDisabled
                    _SpriteToggled.Color = ColorDisabled
                    r.OutlineColor = _BorderColorDisabled
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

            If _DrawBorder Then
                w.Draw(r)
            End If

            DisplayText = New Text(Text, SFMLFont, _SFMLFontSize)
            DisplayText.Color = New Color(ut.ConvertColor(ForeColor))

            Dim textSize As New FloatRect()
            textSize = DisplayText.GetLocalBounds

            'If AutoPadding Then
            '    Border = {CInt(SFMLFontSize), 5, CInt(-SFMLFontSize \ 2), 5}.ToList
            'End If


            'Select Case True
            '    Case TextAlign = Drawing.ContentAlignment.MiddleLeft
            '        DisplayText.Position = New Vector2f(Left, (Top + Height / 2) - textSize.Height / 2)
            '    Case TextAlign = Drawing.ContentAlignment.MiddleCenter
            '        DisplayText.Position = New Vector2f((Left + Width / 2) - textSize.Width / 2, (Top + Height / 2) - textSize.Height / 2)
            '    Case TextAlign = Drawing.ContentAlignment.MiddleRight
            '        DisplayText.Position = New Vector2f((Right) - textSize.Width, (Top + Height / 2) - textSize.Height / 2)
            'End Select

            DisplayText.Position = Common.GetPosition(TextAlign, DisplayText.GetLocalBounds, Bounds, TextOffset)

            w.Draw(DisplayText)
        End If
    End Sub
End Class
