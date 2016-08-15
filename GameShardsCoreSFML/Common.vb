Imports SFML.System
Imports SFML.Graphics
Imports System.Drawing

Public Class Common

    Public Shared Function GetPosition(ByVal TextAlign As ContentAlignment, ByVal Textsize As FloatRect, ByVal ControlSize As IntRect, ByVal Offset As Vector2f) As Vector2f
        Dim pos As New Vector2f
        With ControlSize
            Select Case True
                Case TextAlign = ContentAlignment.MiddleLeft
                    pos = New Vector2f(.Left, (.Top + .Height / 2) - Textsize.Height / 2)
                Case TextAlign = ContentAlignment.MiddleCenter
                    pos = New Vector2f((.Left + .Width / 2) - Textsize.Width / 2, (.Top + .Height / 2) - Textsize.Height / 2)
                Case TextAlign = ContentAlignment.MiddleRight
                    pos = New Vector2f(((.Left + .Width) - Textsize.Width), (.Top + .Height / 2) - Textsize.Height / 2)
            End Select
        End With

        Return New Vector2f(pos.X + Offset.X, pos.Y + Offset.Y)
    End Function

    Public Shared Function GetPosition(ByVal TextAlign As ContentAlignment, ByVal Textsize As FloatRect, ByVal ControlSize As Rectangle, ByVal Offset As Vector2f) As Vector2f
        Dim pos As New Vector2f
        With ControlSize
            Select Case True
                Case TextAlign = ContentAlignment.MiddleLeft
                    pos = New Vector2f(.Left, (.Top + (.Height / 2)) - Textsize.Height / 2)
                Case TextAlign = ContentAlignment.MiddleRight
                    pos = New Vector2f(((.Left + .Width) - Textsize.Width), (.Top + .Height / 2) - Textsize.Height / 2)
                Case TextAlign = ContentAlignment.MiddleCenter
                    pos = New Vector2f((.Left + .Width / 2) - Textsize.Width / 2, (.Top + .Height / 2) - Textsize.Height / 2)
            End Select
        End With

        Return New Vector2f(pos.X + Offset.X, pos.Y + Offset.Y)
    End Function
End Class
