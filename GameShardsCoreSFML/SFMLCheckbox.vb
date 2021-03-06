﻿Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D
Imports System.Drawing

Public Class SFMLCheckbox
    Inherits CheckBox
    Implements ISFMLControl

    Private _OutlineTickness As Integer = -1
    Private _DisplayText As New Text
    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16
    Private _ID As Long
    Private _IDStr As String
    Private _TextOffset As Vector2f = New Vector2f(0, 0)

    Private _CheckSpriteNormal As New Sprite
    Private _CheckSpriteUnchecked As New Sprite
    Private _CheckColorNormal As New SFML.Graphics.Color(255, 255, 255, 128)
    Private _CheckColorHover As New SFML.Graphics.Color(64, 128, 64)
    Private _BorderColorNormal As New SFML.Graphics.Color(128, 0, 0)
    Private _BorderColorHover As New SFML.Graphics.Color(0, 0, 128)
    Private _Autosize As Boolean

    Private _CycleIndeterminate As Boolean = False
    Private _BoxSize As New Drawing.Size(15, 15)
    Private _AutoScale As Boolean = True
    Private _SpriteNormalScale As New Vector2f(1, 1)
    Private _SpriteIndeterminateScale As New Vector2f(1, 1)
    Private _Z As Integer

    'TO DO: Draw box on the right

    Private IsHovered As Boolean = False

    Dim r As RectangleShape

    Public Property Z As Integer Implements ISFMLControl.Z
        Get
            Return _Z
        End Get
        Set(value As Integer)
            _Z = value
        End Set
    End Property

    Public Property CycleIndeterminate As Boolean
        Get
            Return _CycleIndeterminate
        End Get
        Set(value As Boolean)
            _CycleIndeterminate = value
        End Set
    End Property
    Public Property BoxSize As Drawing.Size
        Get
            Return _BoxSize
        End Get
        Set(value As Drawing.Size)
            _BoxSize = value

            If _AutoScale Then
                _CheckSpriteNormal.Scale = New Vector2f(BoxSize.Width / _CheckSpriteNormal.Texture.Size.X, BoxSize.Height / _CheckSpriteNormal.Texture.Size.Y)
            Else
                _CheckSpriteNormal.Scale = SpriteNormalScale
            End If

            If _AutoScale Then
                _CheckSpriteUnchecked.Scale = New Vector2f(BoxSize.Width / _CheckSpriteUnchecked.Texture.Size.X, BoxSize.Height / _CheckSpriteUnchecked.Texture.Size.Y)
            Else
                _CheckSpriteUnchecked.Scale = SpriteNormalScale
            End If
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

    Public Property CheckSpriteNormal As Sprite
        Get
            Return _CheckSpriteNormal
        End Get
        Set(ByVal value As Sprite)
            _CheckSpriteNormal = value
            If _AutoScale Then
                _CheckSpriteNormal.Scale = New Vector2f(BoxSize.Width / value.Texture.Size.X, BoxSize.Height / value.Texture.Size.Y)
            Else
                _CheckSpriteNormal.Scale = SpriteNormalScale
            End If
        End Set
    End Property

    Public Property CheckSpriteUnchecked As Sprite
        Get
            Return _CheckSpriteUnchecked
        End Get
        Set(ByVal value As Sprite)
            _CheckSpriteUnchecked = value
            If _AutoScale Then
                _CheckSpriteUnchecked.Scale = New Vector2f(BoxSize.Width / value.Texture.Size.X, BoxSize.Height / value.Texture.Size.Y)
            Else
                _CheckSpriteUnchecked.Scale = SpriteNormalScale
            End If
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

    Public Sub ChangeCheckedState()

        'If CheckState = CheckState.Checked Then
        '    CheckState = CheckState.Unchecked
        'ElseIf CheckState = CheckState.Unchecked Then
        '    If CycleIndeterminate Then
        '        CheckState = CheckState.Indeterminate
        '    Else
        '        CheckState = CheckState.Checked
        '    End If
        'Else
        '    CheckState = CheckState.Checked
        'End If

        If CycleIndeterminate Then
            If Me.CheckState = CheckState.Unchecked Then
                Me.CheckState = CheckState.Indeterminate
            End If
        ElseIf CheckState = CheckState.Indeterminate Then
            Me.CheckState = CheckState.Unchecked
        End If

    End Sub

    Private Sub ISFMLControl_Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then

            r = New RectangleShape

            DisplayText = New Text(Text, SFMLFont, SFMLFontSize)
            DisplayText.Color = New SFML.Graphics.Color(Utils.ConvertColor(ForeColor))

            'DisplayText.Position = New Vector2f(Location.X, Location.Y)

            r.FillColor = New SFML.Graphics.Color(Utils.ConvertColor(BackColor))

            If IsHovered Then
                r.OutlineColor = New SFML.Graphics.Color(BorderColorHover)
            Else
                r.OutlineColor = New SFML.Graphics.Color(BorderColornormal)
            End If

            r.OutlineThickness = OutlineTickness

            If IsHovered Then
                CheckSpriteNormal.Color = CheckColorHover
                CheckSpriteUnchecked.Color = CheckColorHover
            Else
                CheckSpriteNormal.Color = CheckColorNormal
                CheckSpriteUnchecked.Color = CheckColorNormal
            End If

            DisplayText.Position = New Vector2f(Location.X + BoxSize.Width + 3, Location.Y) '+ BoxSize.Height / 2 - DisplayText.GetGlobalBounds.Height / 2) 'Common.GetPosition(TextAlign, DisplayText.GetGlobalBounds, New FloatRect(r.Position.X + r.Size.X, r.Position.Y, DisplayText.GetGlobalBounds.Width, DisplayText.GetGlobalBounds.Height), New Vector2f(0 + TextOffset.X + BoxSize.Width+3, 0 + TextOffset.Y))

            If AutoSize Then
                Size = New Size(DisplayText.GetGlobalBounds.Width + BoxSize.Width + 4, DisplayText.GetGlobalBounds.Height)
            Else
                Size = New Size(Size.Width, DisplayText.GetGlobalBounds.Height) '+ SFMLFontSize / 4)
            End If

            r.Size = New Vector2f(BoxSize.Width, BoxSize.Height)
            r.Position = New Vector2f(Location.X, Location.Y)

            'Dim rr As New RectangleShape
            'rr.OutlineColor = SFML.Graphics.Color.Black
            'rr.FillColor = SFML.Graphics.Color.Transparent
            'rr.OutlineThickness = -1
            'rr.Size = New Vector2f(Size.Width + BoxSize.Width + 4, Size.Height)
            'rr.Position = New Vector2f(Location.X, Location.Y)
            'w.Draw(rr)

            w.Draw(r)
            w.Draw(DisplayText)

            Select Case CheckState
                Case CheckState.Checked
                    CheckSpriteNormal.Position = New Vector2f(Location.X, Location.Y)
                    w.Draw(CheckSpriteNormal)
                Case CheckState.Indeterminate
                    CheckSpriteUnchecked.Position = New Vector2f(Location.X, Location.Y)
                    w.Draw(CheckSpriteUnchecked)
                Case CheckState.Unchecked
                    'Do nothing for now, leave blank
            End Select


        End If
    End Sub

    Private Sub ISFMLControl_CheckHover(p As Point) Implements ISFMLControl.CheckHover
        If CheckIfRectangleIntersectsPoint(New Drawing.Rectangle(Location.X, Location.Y, Size.Width, Size.Height), p) Then
            IsHovered = True
        Else
            IsHovered = False
        End If

        MyBase.OnMouseHover(New EventArgs)
    End Sub

    Private Sub ISFMLControl_CheckClick(p As Point) Implements ISFMLControl.CheckClick
        If CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(r.GetGlobalBounds), p) Or CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(DisplayText.GetGlobalBounds), p) Then 'Or CheckIfRectangleIntersectsPoint(New Rectangle(r.Position.X, r.Position.Y, r.Size.X, r.Size.Y), p) Then
            'ChangeCheckedState(p)
            MyBase.OnClick(New EventArgs)
            'ChangeCheckedState()
        End If
    End Sub

    Public Sub CheckClickUp(p As Point) Implements ISFMLControl.CheckClickUp
        MyBase.OnMouseUp(New MouseEventArgs(MouseButtons.Left, 1, p.X, p.Y, 0))
    End Sub
End Class
