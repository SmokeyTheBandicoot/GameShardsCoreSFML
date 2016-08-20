Imports GameShardsCore.Base.Geometry

Public NotInheritable Class ControlUtils

    'Public members
    Private Shared GGeom As New Geometry

    Public Class TextboxUtils
        Public Shared Sub UpdateTextboxes(ByRef GUI As GUI, ByVal T As String)
            For x = 0 To GUI.Controls.Count - 1
                If TypeOf GUI.Controls(x) Is SFMLTextbox AndAlso DirectCast(GUI.Controls(x), SFMLTextbox).IsActive Then
                    DirectCast(GUI.Controls(x), SFMLTextbox).Text += T
                End If
            Next
        End Sub
    End Class

    Public Class RadioButtonUtils
        Public Shared Sub CheckRadiobuttons(ByRef gui As GUI, ByRef Rad As SFMLRadioButton, ByVal group As String, ByVal p As Drawing.Point)
            For x = 0 To (gui.Controls.Count - 1)
                If TypeOf gui.Controls(x) Is SFMLRadioButton Then
                    Dim r As New SFMLRadioButton
                    r = DirectCast(gui.Controls(x), SFMLRadioButton)
                    If r.Group = group Then
                        r.Checked = False
                    End If

                    'MsgBox(DirectCast(gui.Controls(x), SFMLRadioButton).Text + " Was set to unchecked")
                End If
            Next

            For x = 0 To (gui.Controls.Count - 1)
                If TypeOf gui.Controls(x) Is SFMLRadioButton AndAlso DirectCast(gui.Controls(x), SFMLRadioButton).Group = group Then
                    If GGeom.CheckIfRectangleIntersectsPoint(DirectCast(gui.Controls(x), SFMLRadioButton).Bounds, p) Then
                        Dim r As New SFMLRadioButton
                        r = DirectCast(gui.Controls(x), SFMLRadioButton)
                        r.Checked = True
                        'MsgBox("Found!: " + r.Text)
                    End If
                End If
            Next
        End Sub
    End Class

End Class
