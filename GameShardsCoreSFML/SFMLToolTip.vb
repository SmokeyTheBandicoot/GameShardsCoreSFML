Imports System.Drawing
Imports System.Windows.Forms
Imports SFML.Graphics
Imports GameShardsCore.Base.Geometry
Imports System.Math
Imports SFML.System
Imports GameShardsCoreSFML

Public Class SFMLToolTip
    Inherits ToolTip
    Implements ISFMLControl

    Public ReadOnly Property location As Point Implements ISFMLControl.location
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property size As Size Implements ISFMLControl.size
        Get
            Throw New NotImplementedException()
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

    Public Sub CheckClick(p As Point) Implements ISFMLControl.CheckClick
        Throw New NotImplementedException()
    End Sub

    Public Sub CheckClickUp(p As Point) Implements ISFMLControl.CheckClickUp
        Throw New NotImplementedException()
    End Sub

    Public Sub CheckHover(p As Point) Implements ISFMLControl.CheckHover
        Throw New NotImplementedException()
    End Sub

    Private Sub ISFMLControl_Draw(ByRef w As RenderWindow) Implements ISFMLControl.Draw
        Throw New NotImplementedException()
    End Sub
End Class
