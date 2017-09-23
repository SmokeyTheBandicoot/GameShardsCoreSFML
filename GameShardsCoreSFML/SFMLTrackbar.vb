Imports System.Drawing
Imports System.Windows.Forms
Imports SFML.Graphics
Imports GameShardsCore2
Imports GameShardsCore2.Geometry.Geometry2D
Imports System.Math
Imports SFML.System
Imports GameShardsCoreSFML

Public Class SFMLTrackbar
    Inherits TrackBar
    Implements ISFMLControl

    Private _SFMLFont As SFML.Graphics.Font
    Private _SFMLFontSize As Single = 16
    Private _ShowMinMaxValues As Boolean = True
    Private _BorderColor As New SFML.Graphics.Color(0, 0, 0)
    Private _ContentBackColor As New SFML.Graphics.Color(0, 0, 0, 0)
    Private _DotBorderColor As New SFML.Graphics.Color(0, 0, 0, 0)
    Private _DotBackColor As New SFML.Graphics.Color(128, 128, 255)
    Private _TickOffsetY As Integer = 5 'In Pixels
    Private _TickOffsetX As Integer = 5 'In Pixels
    Private _BarThickNess As Integer = 10
    Private _DefaultValue As Integer = (Maximum - Minimum) / 2
    Private _ShowPercent As Boolean = True


    'Private _ShowMin as Boolean
    'Private _ShowMax as boolean
    'Private _ShowValue as boolean

    Private IsClicking As Boolean = False
    Private PrevPoint As New Point

    Dim rr As RectangleShape
    Dim ll As RectangleShape
    Dim rrrt As CircleShape
    Dim lllt As CircleShape
    Dim rrt As CircleShape
    Dim llt As CircleShape
    Dim rt As CircleShape
    Dim lt As CircleShape

    Dim track As New RectangleShape
    Dim dot As New CircleShape
    Dim min As Text
    Dim max As Text
    Dim val As Text
    'Dim tick As RectangleShape

    Public Property ShowPercent As Boolean
        Get
            Return _ShowPercent
        End Get
        Set(value As Boolean)
            _ShowPercent = value
        End Set
    End Property
    Public Property BarThickNess As Integer
        Get
            Return _BarThickNess
        End Get
        Set(value As Integer)
            _BarThickNess = value
        End Set
    End Property

    Public Property TickOffsetY As Integer
        Get
            Return _TickOffsetY
        End Get
        Set(ByVal value As Integer)
            _TickOffsetY = value
        End Set
    End Property

    Public Property TickOffsetX As Integer
        Get
            Return _TickOffsetX
        End Get
        Set(ByVal value As Integer)
            _TickOffsetX = value
        End Set
    End Property

    Public Property DotBorderColor() As SFML.Graphics.Color
        Get
            Return _DotBorderColor
        End Get
        Set(value As SFML.Graphics.Color)
            _DotBorderColor = value
        End Set
    End Property
    Public Property DotBackColor() As SFML.Graphics.Color
        Get
            Return _DotBackColor
        End Get
        Set(value As SFML.Graphics.Color)
            _DotBackColor = value
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
            Return _ContentBackColor
        End Get
        Set(value As SFML.Graphics.Color)
            _ContentBackColor = value
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

    Public ReadOnly Property TickNumber As Integer
        Get
            Return Ceiling(((Maximum - Minimum) / TickFrequency)) + 1
        End Get
    End Property

    Public ReadOnly Property TickDistance As Integer
        Get
            Return Size.Width / Ceiling(((Maximum - Minimum) / TickFrequency))
        End Get
    End Property

    Public ReadOnly Property ValuePercent As Single
        Get
            'Return (Maximum / 100) / (Value + Minimum)
            Return (100 / (Maximum - Minimum) * Value) - (100 / (Maximum - Minimum) * Minimum)
        End Get
    End Property

    Public ReadOnly Property ValuePercentPixel As Single
        Get
            'Return (Size.Width / 100) * ((Maximum - Minimum) / 100) * Value
            Return ((Value / (Maximum - Minimum)) - (Minimum / (Maximum - Minimum))) * Size.Width
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

    Public Function GetValueFromClick(ByVal p As Point) As Integer
        If Orientation = Orientation.Horizontal Then
            Return ((p.X - Left) * 100 / Width) * (Maximum - Minimum) / 100 + Minimum
        Else
            Return ((p.Y - Top) * 100 / Height) * (Maximum - Minimum) / 100 + Minimum
        End If
    End Function

    Private Sub ISFMLControl_CheckHover(p As Point) Implements ISFMLControl.CheckHover
        If CheckIfRectangleIntersectsPoint(New Drawing.Rectangle(Left - 1, Top - 1, Width + 2, Height + 2), p) Then
            'If IsClicking Then
            '    If PrevPoint = Nothing Then
            '        PrevPoint = p
            '    Else
            '        If Orientation = Orientation.Horizontal Then
            '            Value += (PrevPoint.X - p.X) / ValuePercentPixel
            '        End If

            '    End If
            'End If
            If IsClicking Then
                Value = GetValueFromClick(p)
            End If
            MyBase.OnMouseHover(New EventArgs)
        End If
    End Sub

    Private Sub ISFMLControl_CheckClick(p As Point) Implements ISFMLControl.CheckClick
        If CheckIfRectangleIntersectsPoint(New Rectangle(Left - 1, Top - 1, Width + 2, Height + 2), p) Then
            'Main Bar

            IsClicking = True
            Value = GetValueFromClick(p)
            MyBase.OnClick(New EventArgs)

        ElseIf CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(rt.GetGlobalBounds), p) Then
            If Value + SmallChange > Maximum Then
                Value = Maximum
            Else
                Value += SmallChange
            End If
        ElseIf CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(rrt.GetGlobalBounds), p) Then
            If Value + LargeChange > Maximum Then
                Value = Maximum
            Else
                Value += LargeChange
            End If
        ElseIf CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(rrrt.GetGlobalBounds), p) Then
            Value = Maximum
        ElseIf CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(lt.GetGlobalBounds), p) Then
            If Value - SmallChange < Minimum Then
                Value = Minimum
            Else
                Value -= SmallChange
            End If
        ElseIf CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(llt.GetGlobalBounds), p) Then
            If Value - LargeChange < Minimum Then
                Value = Minimum
            Else
                Value -= LargeChange
            End If
        ElseIf CheckIfRectangleIntersectsPoint(Utils.FloatRectToRect(lllt.GetGlobalBounds), p) Then
            Value = Minimum
        End If
    End Sub

    Public Sub ISFMLControl_CheckClickUp(p As Point) Implements ISFMLControl.CheckClickUp
        IsClicking = False
        MyBase.OnMouseUp(New MouseEventArgs(MouseButtons.Left, 1, p.X, p.Y, 0))
    End Sub


    Public Sub Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        If Visible Then
            If Orientation = Orientation.Horizontal Then

                'Draw all the left Buttons
                'Declare New
                ll = New RectangleShape

                'First Button
                ll.Size = New Vector2f(BarThickNess, BarThickNess)
                ll.Position = New Vector2f(Location.X - 2 - BarThickNess, Location.Y)

                'First Arrow
                lt = New CircleShape(ll.Size.X / 2, 3)
                lt.Origin = New Vector2f(lt.Origin.X + lt.Radius, lt.Origin.Y + lt.Radius)
                lt.Position = New Vector2f(ll.Position.X + ll.Size.X / 2, ll.Position.Y + ll.Size.Y / 2)
                lt.Rotation = -90
                lt.FillColor = SFML.Graphics.Color.White
                lt.OutlineColor = SFML.Graphics.Color.Black
                lt.OutlineThickness = 1

                'Second Button
                ll.Position = New Vector2f(Location.X - 4 - 2 * BarThickNess, Location.Y)

                'Second Arrow
                llt = New CircleShape(ll.Size.X / 2, 3)
                llt.Origin = New Vector2f(llt.Origin.X + llt.Radius, llt.Origin.Y + llt.Radius)
                llt.Position = New Vector2f(ll.Position.X + ll.Size.X / 2, ll.Position.Y + ll.Size.Y / 2)
                llt.Rotation = -90
                llt.FillColor = New SFML.Graphics.Color(128, 128, 128)
                llt.OutlineColor = SFML.Graphics.Color.Black
                llt.OutlineThickness = 1

                'Third Button
                ll.Position = New Vector2f(Location.X - 6 - 3 * BarThickNess, Location.Y)

                'Third Arrow
                lllt = New CircleShape(ll.Size.X / 2, 3)
                lllt.Origin = New Vector2f(lllt.Origin.X + lllt.Radius, lllt.Origin.Y + lllt.Radius)
                lllt.Position = New Vector2f(ll.Position.X + ll.Size.X / 2, ll.Position.Y + ll.Size.Y / 2)
                lllt.Rotation = -90
                lllt.FillColor = SFML.Graphics.Color.Black
                lllt.OutlineColor = SFML.Graphics.Color.Black
                lllt.OutlineThickness = 1

                'Draw all the Right Button
                'Declare New
                rr = New RectangleShape

                'First button
                rr.Size = New Vector2f(BarThickNess, BarThickNess)
                rr.Position = New Vector2f(Location.X + Size.Width + 2, Location.Y)

                rt = New CircleShape(rr.Size.X / 2, 3)
                rt.Origin = New Vector2f(rt.Origin.X + rt.Radius, rt.Origin.Y + rt.Radius)
                rt.Position = New Vector2f(rr.Position.X + rr.Size.X / 2, rr.Position.Y + rr.Size.Y / 2)
                rt.Rotation = 90
                rt.FillColor = SFML.Graphics.Color.White
                rt.OutlineColor = SFML.Graphics.Color.Black
                rt.OutlineThickness = 1

                'Second Button
                rr.Position = New Vector2f(Location.X + Size.Width + 4 + BarThickNess, Location.Y)

                'Second Arrow
                rrt = New CircleShape(rr.Size.X / 2, 3)
                rrt.Origin = New Vector2f(rrt.Origin.X + rrt.Radius, rrt.Origin.Y + rrt.Radius)
                rrt.Position = New Vector2f(rr.Position.X + rr.Size.X / 2, rr.Position.Y + rr.Size.Y / 2)
                rrt.Rotation = 90
                rrt.FillColor = New SFML.Graphics.Color(128, 128, 128)
                rrt.OutlineColor = SFML.Graphics.Color.Black
                rrt.OutlineThickness = 1

                'Third Button
                rr.Position = New Vector2f(Location.X + Size.Width + 6 + 2 * BarThickNess, Location.Y)

                'Third Arrow
                rrrt = New CircleShape(rr.Size.X / 2, 3)
                rrrt.Origin = New Vector2f(rrrt.Origin.X + rrrt.Radius, rrrt.Origin.Y + rrrt.Radius)
                rrrt.Position = New Vector2f(rr.Position.X + rr.Size.X / 2, rr.Position.Y + rr.Size.Y / 2)
                rrrt.Rotation = 90
                rrrt.FillColor = SFML.Graphics.Color.Black
                rrrt.OutlineColor = SFML.Graphics.Color.Black
                rrrt.OutlineThickness = 1

                track = New RectangleShape
                track.Position = New Vector2f(Location.X, Location.Y)
                track.Size = New Vector2f(Size.Width, BarThickNess)
                track.FillColor = ContentBackColor
                track.OutlineColor = BorderColor
                track.OutlineThickness = -1

                dot = New CircleShape((BarThickNess / 2) + 6, 4)
                dot.Position = New Vector2f(Location.X + ValuePercentPixel - dot.Radius, Location.Y - (dot.Radius - BarThickNess / 2))
                dot.FillColor = DotBackColor
                dot.OutlineColor = DotBorderColor
                dot.OutlineThickness = -1

                min = New Text(Minimum, SFMLFont, SFMLFontSize)
                min.Color = Utils.ConvertColor(ForeColor)
                min.Position = New Vector2f(Location.X, Location.Y + BarThickNess + TickOffsetY + min.GetGlobalBounds.Height + 5)

                max = New Text(Maximum, SFMLFont, SFMLFontSize)
                max.Color = Utils.ConvertColor(ForeColor)
                max.Position = New Vector2f(Location.X + Size.Width - max.GetGlobalBounds.Width, Location.Y + BarThickNess + TickOffsetY + min.GetGlobalBounds.Height + 5)

                If ShowPercent Then
                    val = New Text(Value.ToString + " (" + Round(ValuePercent, 1).ToString + "%)", SFMLFont, SFMLFontSize)
                Else
                    val = New Text(Value.ToString, SFMLFont, SFMLFontSize)
                End If

                val.Color = Utils.ConvertColor(ForeColor)
                val.Position = New Vector2f(Location.X + Size.Width / 2 - val.GetGlobalBounds.Width / 2, Location.Y + BarThickNess + TickOffsetY + min.GetGlobalBounds.Height + 5)

                w.Draw(dot)
                w.Draw(lt)
                w.Draw(llt)
                w.Draw(lllt)
                w.Draw(rt)
                w.Draw(rrt)
                w.Draw(rrrt)
                w.Draw(track)
                w.Draw(min)
                w.Draw(max)
                w.Draw(val)

                If TickStyle = TickStyle.Both Or TickStyle = TickStyle.BottomRight Then
                    For x = 0 To TickNumber - 1
                        Dim tick As New RectangleShape
                        tick.Position = New Vector2f(Location.X + x * TickDistance, Location.Y + BarThickNess + TickOffsetY)
                        tick.FillColor = BorderColor
                        tick.OutlineThickness = -1
                        tick.OutlineColor = BorderColor
                        tick.Size = New Vector2f(1, 5)

                        w.Draw(tick)
                    Next
                End If

                If TickStyle = TickStyle.Both Or TickStyle = TickStyle.TopLeft Then

                    For x = 0 To TickNumber - 1
                        Dim tick As New RectangleShape
                        tick.Position = New Vector2f(Location.X + x * TickDistance, Location.Y - BarThickNess / 2 - TickOffsetY)
                        tick.FillColor = BorderColor
                        tick.OutlineThickness = -1
                        tick.OutlineColor = BorderColor
                        tick.Size = New Vector2f(1, 5)

                        w.Draw(tick)
                    Next

                End If

            Else

                'Draw all the left Buttons
                'Declare New
                ll = New RectangleShape

                'First Button
                ll.Size = New Vector2f(BarThickNess, BarThickNess)
                ll.Position = New Vector2f(Location.X, Location.Y - 2 - BarThickNess)

                'First Arrow
                lt = New CircleShape(ll.Size.Y / 2, 3)
                lt.Origin = New Vector2f(lt.Origin.X + lt.Radius, lt.Origin.Y + lt.Radius)
                lt.Position = New Vector2f(ll.Position.X + ll.Size.X / 2, ll.Position.Y + ll.Size.Y / 2)
                lt.Rotation = 180
                lt.FillColor = SFML.Graphics.Color.White
                lt.OutlineColor = SFML.Graphics.Color.Black
                lt.OutlineThickness = 1

                'Second Button
                ll.Position = New Vector2f(Location.X, Location.Y - 4 - 2 * BarThickNess)

                'Second Arrow
                llt = New CircleShape(ll.Size.Y / 2, 3)
                llt.Origin = New Vector2f(llt.Origin.X + llt.Radius, llt.Origin.Y + llt.Radius)
                llt.Position = New Vector2f(ll.Position.X + ll.Size.X / 2, ll.Position.Y + ll.Size.Y / 2)
                llt.Rotation = 180
                llt.FillColor = New SFML.Graphics.Color(128, 128, 128)
                llt.OutlineColor = SFML.Graphics.Color.Black
                llt.OutlineThickness = 1

                'Third Button
                ll.Position = New Vector2f(Location.X, Location.Y - 6 - 3 * BarThickNess)

                'Third Arrow
                lllt = New CircleShape(ll.Size.Y / 2, 3)
                lllt.Origin = New Vector2f(lllt.Origin.X + lllt.Radius, lllt.Origin.Y + lllt.Radius)
                lllt.Position = New Vector2f(ll.Position.X + ll.Size.X / 2, ll.Position.Y + ll.Size.Y / 2)
                lllt.Rotation = 180
                lllt.FillColor = SFML.Graphics.Color.Black
                lllt.OutlineColor = SFML.Graphics.Color.Black
                lllt.OutlineThickness = 1

                'Draw all the Right Button
                'Declare New
                rr = New RectangleShape

                'First button
                rr.Size = New Vector2f(BarThickNess, BarThickNess)
                rr.Position = New Vector2f(Location.Y, Location.X + Size.Width + 2)

                rt = New CircleShape(rr.Size.X / 2, 3)
                rt.Origin = New Vector2f(rt.Origin.X + rt.Radius, rt.Origin.Y + rt.Radius)
                rt.Position = New Vector2f(rr.Position.X + rr.Size.X / 2, rr.Position.Y + rr.Size.Y / 2)
                rt.Rotation = 0
                rt.FillColor = SFML.Graphics.Color.White
                rt.OutlineColor = SFML.Graphics.Color.Black
                rt.OutlineThickness = 1

                'Second Button
                rr.Position = New Vector2f(Location.X, Location.Y + Size.Height + 4 + BarThickNess)

                'Second Arrow
                rrt = New CircleShape(rr.Size.X / 2, 3)
                rrt.Origin = New Vector2f(rrt.Origin.X + rrt.Radius, rrt.Origin.Y + rrt.Radius)
                rrt.Position = New Vector2f(rr.Position.X + rr.Size.X / 2, rr.Position.Y + rr.Size.Y / 2)
                rrt.Rotation = 0
                rrt.FillColor = New SFML.Graphics.Color(128, 128, 128)
                rrt.OutlineColor = SFML.Graphics.Color.Black
                rrt.OutlineThickness = 1

                'Third Button
                rr.Position = New Vector2f(Location.X, Location.Y + Size.Height + 6 + 2 * BarThickNess)

                'Third Arrow
                rrrt = New CircleShape(rr.Size.X / 2, 3)
                rrrt.Origin = New Vector2f(rrrt.Origin.X + rrrt.Radius, rrrt.Origin.Y + rrrt.Radius)
                rrrt.Position = New Vector2f(rr.Position.X + rr.Size.X / 2, rr.Position.Y + rr.Size.Y / 2)
                rrrt.Rotation = 0
                rrrt.FillColor = SFML.Graphics.Color.Black
                rrrt.OutlineColor = SFML.Graphics.Color.Black
                rrrt.OutlineThickness = 1

                track = New RectangleShape
                track.Position = New Vector2f(Location.X, Location.Y)
                track.Size = New Vector2f(Size.Height, BarThickNess)
                track.FillColor = ContentBackColor
                track.OutlineColor = BorderColor
                track.OutlineThickness = -1

                dot = New CircleShape((BarThickNess / 2) + 6, 4)
                dot.Position = New Vector2f(Location.X - (dot.Radius - BarThickNess / 2), Location.Y + ValuePercentPixel - dot.Radius)
                dot.FillColor = DotBackColor
                dot.OutlineColor = DotBorderColor
                dot.OutlineThickness = -1

                min = New Text(Minimum, SFMLFont, SFMLFontSize)
                min.Color = Utils.ConvertColor(ForeColor)
                min.Position = New Vector2f(Location.X + BarThickNess + TickOffsetX + min.GetGlobalBounds.Height + 5, Location.Y)

                max = New Text(Maximum, SFMLFont, SFMLFontSize)
                max.Color = Utils.ConvertColor(ForeColor)
                max.Position = New Vector2f(Location.X + BarThickNess + TickOffsetX + min.GetGlobalBounds.Height + 5, Location.Y + Size.Height - max.GetGlobalBounds.Height)

                If ShowPercent Then
                    val = New Text(Value.ToString + " (" + Round(ValuePercent, 1).ToString + "%)", SFMLFont, SFMLFontSize)
                Else
                    val = New Text(Value.ToString, SFMLFont, SFMLFontSize)
                End If

                val.Color = Utils.ConvertColor(ForeColor)
                val.Position = New Vector2f(Location.X + BarThickNess + TickOffsetX + min.GetGlobalBounds.Height + 5, Location.Y + Size.Height / 2 - val.GetGlobalBounds.Height / 2)

                w.Draw(dot)
                w.Draw(lt)
                w.Draw(llt)
                w.Draw(lllt)
                w.Draw(rt)
                w.Draw(rrt)
                w.Draw(rrrt)
                w.Draw(track)
                w.Draw(min)
                w.Draw(max)
                w.Draw(val)

                If TickStyle = TickStyle.Both Or TickStyle.BottomRight Then
                    For x = 0 To TickNumber - 1
                        Dim tick As New RectangleShape
                        tick.Position = New Vector2f(Location.X + BarThickNess + TickOffsetX, Location.Y + x * TickDistance)
                        tick.FillColor = BorderColor
                        tick.OutlineThickness = -1
                        tick.OutlineColor = BorderColor
                        tick.Size = New Vector2f(1, 5)

                        w.Draw(tick)
                    Next
                End If

                If TickStyle = TickStyle.Both Or TickStyle.TopLeft Then

                End If
            End If

        End If
    End Sub
End Class
