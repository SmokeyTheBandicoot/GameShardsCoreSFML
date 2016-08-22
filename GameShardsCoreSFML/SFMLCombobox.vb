Imports System.Drawing
Imports System.Windows.Forms
Imports SFML.System
Imports GameShardsCoreSFML
Imports SFML.Graphics
Imports GameShardsCore.Base.Geometry
Imports System.Math

Public Class SFMLCombobox
    Inherits ComboBox
    Implements ISFMLControl

    Dim GGeom As New Geometry

    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _BackColor As New SFML.Graphics.Color(161, 215, 236)
    Private _ColorTop As New SFML.Graphics.Color(161, 215, 236)
    Private _ColorBottom As New SFML.Graphics.Color(255, 255, 255)
    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16
    Private _IsActive As Boolean = False

    Private MaxFontSize As Integer

    Dim ItemWidth As New List(Of Integer)

    Public Property IsActive As Boolean
        Get
            Return _IsActive
        End Get
        Set(value As Boolean)
            _IsActive = value
            If value = True Then
                Size = New Size(Size.Width, MaxFontSize * (Items.Count + 1))
            Else
                Size = New Size(Size.Width, MaxFontSize)
            End If
        End Set
    End Property

    Public Property BorderColor() As SFML.Graphics.Color
        Get
            Return _BorderColor
        End Get
        Set(value As SFML.Graphics.Color)
            _BorderColor = value
        End Set
    End Property
    Public Property ContentBackColor() As SFML.Graphics.Color
        Get
            Return _BackColor
        End Get
        Set(value As SFML.Graphics.Color)
            _BackColor = value
        End Set
    End Property
    Public Property ColorTop() As SFML.Graphics.Color
        Get
            Return _ColorTop
        End Get
        Set(value As SFML.Graphics.Color)
            _ColorTop = value
        End Set
    End Property
    Public Property ColorBottom() As SFML.Graphics.Color
        Get
            Return _ColorBottom
        End Get
        Set(value As SFML.Graphics.Color)
            _ColorBottom = value
        End Set
    End Property

    Public Property SFMLFont() As SFML.Graphics.Font
        Get
            Return _SFMLFont
        End Get
        Set(value As SFML.Graphics.Font)
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

    Public Property Z As Integer Implements ISFMLControl.Z
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Sub Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then
            ItemWidth.Clear()

            Dim tt As New Text("@", SFMLFont, SFMLFontSize)
            MaxFontSize = tt.GetGlobalBounds.Height * 1.25
            'MaxFontSize = tt.CharacterSize '* 1.25

            If IsActive Then

                For x = 0 To Items.Count - 1
                    Dim r As New RectangleShape
                    Dim t As New Text
                    t.Font = SFMLFont
                    t.CharacterSize = SFMLFontSize
                    t.DisplayedString = Items(x).ToString
                    t.Color = Utils.ConvertColor(ForeColor)
                    ItemWidth.Add(t.GetGlobalBounds.Width)

                    Dim re, gr, bl, tr As Integer
                    re = 0
                    gr = 0
                    bl = 0
                    tr = 0

                    If Not Items.Count = 0 Then

                        If ColorTop.R > ColorBottom.R Then
                            re = ColorBottom.R + x * Abs((ColorTop.R - ColorBottom.R) / Items.Count)
                        Else
                            re = ColorTop.R + x * Abs((ColorBottom.R - ColorTop.R) / Items.Count)
                        End If

                        If ColorTop.G > ColorBottom.G Then
                            gr = ColorBottom.G + x * Abs((ColorTop.G - ColorBottom.G) / Items.Count)
                        Else
                            gr = ColorTop.G + x * Abs((ColorBottom.G - ColorTop.G) / Items.Count)
                        End If

                        If ColorTop.B > ColorBottom.B Then
                            bl = ColorBottom.B + x * Abs((ColorTop.B - ColorBottom.B) / Items.Count)
                        Else
                            bl = ColorTop.B + x * Abs((ColorBottom.B - ColorTop.B) / Items.Count)
                        End If

                        If ColorTop.A > ColorBottom.A Then
                            tr = ColorBottom.A + x * Abs((ColorTop.A - ColorBottom.A) / Items.Count)
                        Else
                            tr = ColorTop.A + x * Abs((ColorBottom.A - ColorTop.A) / Items.Count)
                        End If

                        r.FillColor = New SFML.Graphics.Color(re, gr, bl, tr)
                        r.OutlineColor = BorderColor
                        r.OutlineThickness = -1

                        r.Position = New Vector2f(Location.X, Location.Y + MaxFontSize * (x + 1))
                        r.Size = New Vector2f(Size.Width, MaxFontSize)

                        t.Position = New Vector2f(Location.X + 2, 1 + Location.Y + MaxFontSize * (x + 1))
                        't.Position = Common.GetPosition(texta)

                        w.Draw(r)
                        w.Draw(t)
                    End If
                Next
            End If

            Dim rr As New RectangleShape
            rr.FillColor = ContentBackColor
            rr.OutlineColor = BorderColor
            rr.OutlineThickness = -1
            rr.Position = New Vector2f(Location.X, Location.Y)
            rr.Size = New Vector2f(Size.Width, MaxFontSize)

            tt.DisplayedString = Text
            tt.Color = Utils.ConvertColor(ForeColor)
            tt.Position = New Vector2f(Location.X, Location.Y)

            Size = New Size(Size.Width, MaxFontSize * (Items.Count + 1))

            w.Draw(rr)
                w.Draw(tt)
            End If
    End Sub

    Private Sub ISFMLControl_CheckHover(p As Point) Implements ISFMLControl.CheckHover
        If GGeom.CheckIfRectangleIntersectsPoint(New Rectangle(Location.X, Location.Y, Size.Width, Size.Height), p) Then
            MyBase.OnMouseHover(New EventArgs)
        End If
    End Sub

    Private Sub ISFMLControl_CheckClick(p As Point) Implements ISFMLControl.CheckClick
        If GGeom.CheckIfRectangleIntersectsPoint(New Rectangle(Location.X, Location.Y, Size.Width, Size.Height + Abs(IsActive * MaxFontSize * (Items.Count))), p) Then
            'If Not ((Abs(p.Y - Location.Y) - MaxFontSize) / MaxFontSize = -1) Then
            '    SelectedIndex = (Abs(p.Y - Location.Y) - MaxFontSize) / MaxFontSize
            '    SelectedItem = Items(SelectedIndex)
            'End If
            If Not (Ceiling((p.Y - Location.Y) / MaxFontSize) - 2 = -1) Then
                'MsgBox((Ceiling((p.Y - Location.Y) / MaxFontSize) - 2))

                SelectedIndex = (Ceiling((p.Y - Location.Y) / MaxFontSize) - 2)
                SelectedItem = Items(SelectedIndex)
                Text = Items(SelectedIndex).ToString
            End If

            IsActive = (Not IsActive)
            MyBase.OnSelectedIndexChanged(New EventArgs)
            MyBase.OnSelectedItemChanged(New EventArgs)
            MyBase.OnClick(New EventArgs)
        End If
    End Sub
End Class
