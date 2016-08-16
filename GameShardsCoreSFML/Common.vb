Imports SFML.System
Imports SFML.Graphics
Imports System.Drawing
Imports System.Windows.Forms

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
                Case TextAlign = HorizontalAlignment.Left
                    pos = New Vector2f(.Left, (.Top + (.Height / 2)) - Textsize.Height / 2)
                Case TextAlign = HorizontalAlignment.Right
                    pos = New Vector2f(((.Left + .Width) - Textsize.Width), (.Top + .Height / 2) - Textsize.Height / 2)
                Case TextAlign = HorizontalAlignment.Center
                    pos = New Vector2f((.Left + .Width / 2) - Textsize.Width / 2, (.Top + .Height / 2) - Textsize.Height / 2)
            End Select
        End With

        Return New Vector2f(pos.X + Offset.X, pos.Y + Offset.Y)
    End Function

    Public Shared Function GetPositionHorizontal(ByVal TextAlign As HorizontalAlignment, ByVal Textsize As FloatRect, ByVal ControlSize As IntRect, ByVal Offset As Vector2f) As Vector2f
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

    Public Shared Function ConvertContentAlignToHorizontalAlign(ByVal cont As ContentAlignment) As HorizontalAlignment
        Select Case cont
            Case ContentAlignment.BottomLeft Or ContentAlignment.MiddleLeft Or ContentAlignment.TopLeft
                Return HorizontalAlignment.Left
            Case ContentAlignment.BottomRight Or ContentAlignment.MiddleRight Or ContentAlignment.TopRight
                Return HorizontalAlignment.Right
            Case Else
                Return HorizontalAlignment.Center
        End Select
    End Function

    Public Shared Function ConvertHorizontalAlignToContentAlign(ByVal hor As HorizontalAlignment) As ContentAlignment
        Select Case hor
            Case HorizontalAlignment.Left
                Return ContentAlignment.MiddleLeft
            Case HorizontalAlignment.Right
                Return ContentAlignment.MiddleRight
            Case Else
                Return ContentAlignment.MiddleCenter
        End Select
    End Function
End Class
