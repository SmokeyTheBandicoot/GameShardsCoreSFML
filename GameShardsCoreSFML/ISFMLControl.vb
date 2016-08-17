Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports System.Drawing

Public Interface ISFMLControl
    Sub Draw(ByRef w As renderwindow)
    Sub CheckHover(ByVal p As point)
    Sub CheckClick(ByVal p As Point)
    Property Z As Integer


End Interface
