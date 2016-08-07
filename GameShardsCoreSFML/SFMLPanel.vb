Imports System.Windows.Forms
Imports SFML.Graphics
Imports SFML.System

Public Class SFMLPanel
    Inherits Panel

    Dim ut As New Utils

    Private _SpriteNormal As New Sprite

    Public Property SpriteNormal() As Sprite
        Get
            Return _SpriteNormal
        End Get
        Set(value As Sprite)
            _SpriteNormal = value
        End Set
    End Property




    Public Sub Draw(ByRef w As RenderWindow)
        If Visible Then

            SpriteNormal.Scale = New Vector2f(Width / SpriteNormal.Texture.Size.X, Height / SpriteNormal.Texture.Size.Y)
            SpriteNormal.Position = New Vector2f(Location.X, Location.Y)

            w.Draw(SpriteNormal)
        End If

    End Sub
End Class

