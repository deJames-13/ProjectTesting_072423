
Public Class _cartpage



    Private Sub windowWrapper_VisibleChanged(sender As Object, e As EventArgs) Handles windowWrapper.ForeColorChanged

        If windowWrapper.ForeColor <> activeForeColor Then Return

        cartButtons.Clear()
        summaries.Clear()
        itemContainer.Controls.Clear()
        itemContainer.Hide()


        RemoveHandler btnEdit.Click, AddressOf btnEdit_Click
        AddHandler btnEdit.Click, AddressOf btnEdit_Click



        CartFromSource()
        itemContainer.Show()
        btnEdit.BackgroundImage = My.Resources.icons8_edit_20
        btnEdit.ForeColor = Color.Red
        SetButtonState(My.Resources.icons8_unchecked_checkbox_30, Color.Black)

    End Sub

    Private Sub btnCheckOut_Click(sender As Object, e As EventArgs) Handles btnCheckOut.Click
        If transactionQuery.Count > 0 AndAlso transactionQuery("name") = "insert" Then CompleteTransaction()
        MsgBox("Items checked out successfully!", MsgBoxStyle.Information, "Success")
        switchPanel(_orderpage.windowWrapper, currentPage)
    End Sub
End Class