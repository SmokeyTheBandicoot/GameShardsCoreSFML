Option Strict On
Option Explicit On

Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports System.Math
Imports GameShardsCore2.Geometry.Geometry2D


Public Class ShapeDrawer

    Public Class Hexagon

        Implements SFML.Graphics.Drawable
        Public Property Position As Vector2f
        Public Property Center As Vector2f
        Public Property Radius As UInteger
        Public Property Fillcolor As Color
        Public Property Bordercolor As Color
        Public Property Bordersize As UInteger
        Public Property Rotation As Single


        Public Sub Draw(target As RenderTarget, states As RenderStates) Implements Drawable.Draw
            Dim Vert As New VertexArray
            Dim Bord As New VertexArray

            Dim tempX As Single = Position.X + Center.X + Radius
            Dim tempy As Single = Position.Y + Center.Y + Radius

            Vert.Append(New Vertex(New Vector2f(tempX, 0), Fillcolor))
            Vert.Append(New Vertex(New Vector2f(CSng(tempX * Sin(60)), CSng(tempy * Sin(60))), Fillcolor))
            Vert.Append(New Vertex(New Vector2f(CSng(tempX * Sin(120)), CSng(tempy * Sin(120))), Fillcolor))
            Vert.Append(New Vertex(New Vector2f(-tempX, 0), Fillcolor))
            Vert.Append(New Vertex(New Vector2f(CSng(tempX * Sin(-120)), CSng(tempy * Sin(-120))), Fillcolor))
            Vert.Append(New Vertex(New Vector2f(CSng(tempX * Sin(-60)), CSng(tempy * Sin(-60))), Fillcolor))

            tempX = Position.X + Center.X + Radius - Bordersize
            tempy = Position.Y + Center.Y + Radius - Bordersize

            Bord.Append(New Vertex(New Vector2f(tempX, 0), Bordercolor))
            Bord.Append(New Vertex(New Vector2f(CSng(tempX * Sin(60)), CSng(tempy * Sin(60))), Bordercolor))
            Bord.Append(New Vertex(New Vector2f(CSng(tempX * Sin(120)), CSng(tempy * Sin(120))), Bordercolor))
            Bord.Append(New Vertex(New Vector2f(-tempX, 0), Bordercolor))
            Bord.Append(New Vertex(New Vector2f(CSng(tempX * Sin(-120)), CSng(tempy * Sin(-120))), Bordercolor))
            Bord.Append(New Vertex(New Vector2f(CSng(tempX * Sin(-60)), CSng(tempy * Sin(-60))), Bordercolor))

            For x As UInteger = 0 To 5 'Hardcoded, an hexagon always has 6 vertexes. Improves performance
                Vert.Item(x) = New Vertex(New Vector2f(CSng(Vert(x).Position.X * Cos(Rotation) - Vert(x).Position.Y * Sin(Rotation)), CSng(Vert(x).Position.X * Sin(Rotation) + Vert(x).Position.Y * Cos(Rotation))), Fillcolor)
                Bord.Item(x) = New Vertex(New Vector2f(CSng(Bord(x).Position.X * Cos(Rotation) - Bord(x).Position.Y * Sin(Rotation)), CSng(Bord(x).Position.X * Sin(Rotation) + Bord(x).Position.Y * Cos(Rotation))), Bordercolor)
            Next

            target.Draw(Vert)
            target.Draw(Bord)

        End Sub

        Public Shared Sub DrawHexagonGrid(Target As RenderTarget, TopLeftStartPoint As Vector2f, Width As Integer, Height As Integer, Hex As Hexagon)

            Dim Upordown As Integer = 1
            Hex.Position = TopLeftStartPoint

            For x As Integer = 0 To Width
                For y As Integer = 0 To Height
                    Hex.Position = New Vector2f(Hex.Position.X, CSng(Hex.Position.Y + 2 * Hex.Radius * HexagonApothem))
                    Target.Draw(Hex)

                Next

                Hex.Position = New Vector2f(CSng(Hex.Position.X + Hex.Radius + Hex.Radius * Cos(60)), CSng(Hex.Position.Y + HexagonApothem) * Upordown)
                Upordown *= -1
            Next


        End Sub



    End Class




End Class
