Imports SFML.Graphics

Public Class GUI

    Private _Controls As New List(Of ISFMLControl)
    Private _Sprites As New List(Of Sprite)

    ''' <summary>
    ''' Add here buttons, labels, pictureboxes
    ''' </summary>
    ''' <returns></returns>
    Public Property Controls() As List(Of ISFMLControl)
        Get
            Return _Controls
        End Get
        Set(value As List(Of ISFMLControl))
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
            Controls(x).Draw(window)
        Next
        '    If TypeOf Controls(x) Is SFMLButton Then
        '        DirectCast(Controls(x), SFMLButton).Draw(window)
        '    ElseIf TypeOf Controls(x) Is SFMLTextbox Then
        '        DirectCast(Controls(x), SFMLTextbox).draw(window)
        '    ElseIf TypeOf Controls(x) Is SFMLKeyboard Then
        '        DirectCast(Controls(x), SFMLKeyboard).Draw(window)
        '    ElseIf TypeOf Controls(x) Is SFMLCheckbox Then
        '        DirectCast(Controls(x), SFMLCheckbox).Draw(window)
        '    End If
        'Next
    End Sub
End Class

