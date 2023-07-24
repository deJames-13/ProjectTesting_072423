Module WindowPageManager
    Public activeForeColor As Color = Color.Black
    Public inactiveForeColor As Color = Color.White
    Public Function validNumber(str As String) As Boolean
        If IsNumeric(str) Then
            Return True
        End If
        Return False
    End Function

    Public Function isEmptyInput(ByRef input As TextBox, Optional message As String = "Input Required!") As Boolean
        If String.IsNullOrEmpty(input.Text.Trim) Then
            MsgBox(message, MsgBoxStyle.Exclamation, "Empty Input!")
            Return True
        End If
        Return False
    End Function

    Public Function validInputs(ByRef texts As TextBox())
        For Each t In texts
            Dim message As String = $"Input Required for {t.Name.ToUpper.Replace("_", " ")}"
            If isEmptyInput(t, message) Then
                Return False
            End If
        Next
        Return True
    End Function





    '######################################################################
    '#  Takes a list of textbox and iterates it to clear each instance
    '######################################################################
    Public Sub ClearAll(ts As TextBox())
        For Each t In ts
            t.ForeColor = Color.Black
            t.Clear()
        Next
    End Sub
    '######################################################################

    '######################################################################
    '#  Manges the page switch by swapping the existing panel
    '######################################################################
    Public currentPage As Panel = Nothing
    Public Sub switchPanel(ByRef newPage As Panel, ByRef current As Panel)
        If newPage Is current Then
            Return
        End If
        current.Hide()
        current.ForeColor = inactiveForeColor

        newPage.Parent = current.Parent
        newPage.Dock = DockStyle.Fill
        newPage.Show()
        newPage.Focus()
        newPage.ForeColor = activeForeColor

        currentPage = newPage
    End Sub

    Dim toggleHover As Boolean = False
    Public Sub HoverEvents(sender As Object, e As EventArgs)
        toggleHover = Not toggleHover
        If sender.GetType() Is GetType(Button) Then
            HoverOnButton(CType(sender, Button), toggleHover)
        End If
    End Sub

    Public Sub HoverOnButton(button As Button, state As Boolean)
        Try
            Select Case state
                Case True
                    button.BackColor = Color.Azure
                    button.FlatAppearance.BorderSize += 1
                Case False

                    button.BackColor = Color.Transparent
                    button.FlatAppearance.BorderSize -= 1
            End Select
        Catch ex As Exception

        End Try
    End Sub



    '######################################################################
End Module
