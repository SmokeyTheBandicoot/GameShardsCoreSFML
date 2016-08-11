Public Class TextboxUtils
    Public Sub UpdateTextboxes(ByRef GUI As GUI, ByVal T As String)
        For x = 0 To GUI.Controls.Count - 1
            If TypeOf GUI.Controls(x) Is SFMLTextbox AndAlso DirectCast(GUI.Controls(x), SFMLTextbox).IsActive Then
                DirectCast(GUI.Controls(x), SFMLTextbox).Text += T
            End If
        Next
    End Sub
End Class
