Imports System.Drawing
Imports System.Reflection
Imports SFML.Graphics
Imports SFML.System
Public Class Utils
    Public Shared Function RectToIntRect(ByVal rect As Rectangle) As IntRect
        Return New IntRect(rect.Left, rect.Top, rect.Width, rect.Height)
    End Function

    Public Shared Function RectToFloatRect(ByVal rect As Rectangle) As FloatRect
        Return New FloatRect(rect.Left, rect.Top, rect.Width, rect.Height)
    End Function

    Public Shared Function FloatRectToRect(ByVal rect As FloatRect) As Rectangle
        Return New Rectangle(rect.Left, rect.Top, rect.Width, rect.Height)
    End Function

    Public Shared Function PointToVector2F(ByVal point As Point) As Vector2f
        Return New Vector2f(point.X, point.Y)
    End Function

    Public Shared Function ConvertColor(ByVal color As Drawing.Color) As SFML.Graphics.Color
        Return New SFML.Graphics.Color(color.R, color.G, color.B, color.A)
    End Function

    Public Shared Function ConvertFont(font As Drawing.Font, Optional ByRef result As Boolean = False) As SFML.Graphics.Font
        'Try
        '    IO.File.Copy("C:\\Windows\Fonts\" + font.FontFamily, Assembly.GetExecutingAssembly().Location + "\" + font.FontFamily)
        'Catch ex As Exception

        'End Try
        Return New SFML.Graphics.Font(font.FontFamily.Name.ToString + ".ttf")
    End Function

    Public Shared Function InverseConvertFont(ByVal font As SFML.Graphics.Font, ByVal SFMLFontSize As Integer) As Drawing.Font
        Return New Drawing.Font(font.ToString, SFMLFontSize * 3 / 4)
    End Function
End Class