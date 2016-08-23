Imports SFML.Graphics
Imports System.Drawing

Public Interface ISFMLControl
    Sub Draw(ByRef w As RenderWindow)
    Sub CheckHover(ByVal p As point)
    Sub CheckClick(ByVal p As Point)
    Sub CheckClickUp(ByVal p As Point)

    ReadOnly Property size As Size

    ReadOnly Property location As Point

    Property Z As Integer
End Interface
