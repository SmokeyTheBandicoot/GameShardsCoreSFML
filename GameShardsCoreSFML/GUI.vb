Imports SFML.Graphics
Imports System.Windows.Forms

Public Class GUI

    Private _Controls As New List(Of Control)
    Private _Sprites As New List(Of Sprite)

    ''' <summary>
    ''' Add here buttons, labels, pictureboxes
    ''' </summary>
    ''' <returns></returns>
    Public Property Controls() As List(Of Control)
        Get
            Return _Controls
        End Get
        Set(value As List(Of Control))
            _Controls = value
        End Set
    End Property

    ''' <summary>
    ''' Add here the sprites for each control
    ''' </summary>
    ''' <returns></returns>
    Public Property Sprites() As List(Of Sprite)
        Get
            Return _Sprites
        End Get
        Set(value As List(Of Sprite))
            _Sprites = value
        End Set
    End Property

    Public Sub Draw(ByRef window As RenderWindow)
        For x = 0 To Controls.Count - 1
            If TypeOf Controls(x) Is SFMLButton Then
                DirectCast(Controls(x), SFMLButton).Draw(window)
            End If
        Next
    End Sub
End Class

