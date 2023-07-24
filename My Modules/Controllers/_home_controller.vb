Imports System.Reflection

Module _home_controller

    'Dim log As String = ""


    Public Sub btnDebugger_Click(sender As Object, e As EventArgs)
        'LoadFromSource()
        'CreateCartItem()
    End Sub


    ' SET CURRENT USER
    Public Sub SetCurrentUser(username As String)
        Dim dt As DataTable = SelectWhere("customer", $"username='{username}'")
        If dt Is Nothing Then Return
        current_user = dt.Rows(0)
    End Sub


    '
    ' SHOP PAGE AND CONTROLS
    '
    Public Sub btnSearch_Click(sender As Object, e As EventArgs)
        Dim search As String = _shoppage.txtSearch.Text
        If String.IsNullOrEmpty(search) Then Return

        Dim dt As DataTable = FilterFrom("product", "description", search)
        If dt Is Nothing Then Return

        ProductFromSource(dt)
    End Sub

    '
    ' CART PAGE AND SUMMARY
    '
    Public cartButtons As New Dictionary(Of String, Button())
    Public summaries As New Dictionary(Of String, Panel)

    Public Sub btnEdit_Click(sender As Object, e As EventArgs)



        Select Case sender.ForeColor

            Case Color.Blue
                sender.BackgroundImage = My.Resources.icons8_remove_20
                sender.ForeColor = Color.LightCoral

                CompleteTransaction(1)
                SetButtonState(My.Resources.icons8_unchecked_checkbox_30, Color.Blue)

            Case Color.LightCoral
                sender.BackgroundImage = My.Resources.icons8_edit_20
                sender.ForeColor = Color.Blue

                SetButtonState(My.Resources.icons8_remove_20, Color.LightCoral)

            Case Color.Black
                sender.BackgroundImage = My.Resources.icons8_edit_20
                sender.ForeColor = Color.Red

                CompleteTransaction()
                SetButtonState(My.Resources.icons8_unchecked_checkbox_30, Color.Black)

            Case Color.Red
                sender.BackgroundImage = My.Resources.icons8_select_all_20__1_
                sender.ForeColor = Color.Black

                SetButtonState(My.Resources.icons8_remove_20, Color.Yellow)

        End Select

    End Sub



    Public Sub SetButtonState(image As Bitmap, c As Color)

        Dim i As Integer
        For Each btn As Button() In cartButtons.Values

            i = cartButtons.Values.ToList.IndexOf(btn)

            btn(0).BackgroundImage = image
            btn(0).ForeColor = c

            Select Case c
                Case Color.Blue
                    btn(0).Hide()
                    Continue For
                Case Color.LightCoral
                    btn(0).Show()
                    Continue For
                Case Color.Black
                    btn(1).Hide()
                    btn(2).Hide()
                Case Color.Red
                    btn(1).Show()
                    btn(2).Show()
            End Select


            Dim q As Label = btn(1).Parent.Parent.Controls(0).Controls(0)
            Dim quantity = current_table.Rows(i)("quantity")


            If Not quantity = q.Text Then
                transactionQuery.Add(
                            $"[UPDATED QUANTITY] {current_table.Rows(i)("item_name")} = {q}",
                            $"UPDATE cart SET quantity={q.Text} WHERE id={current_table.Rows(i)("id")};"
                            )
            End If
        Next



        CompleteTransaction()
        For Each summary As Panel In summaries.Values
            If summary.Parent IsNot Nothing Then summary.Parent.Controls.Remove(summary)
        Next

    End Sub

End Module
