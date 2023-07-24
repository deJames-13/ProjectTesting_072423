Public Class _shoppage
    Private Sub windowWrapper_VisibleChanged(sender As Object, e As EventArgs) Handles windowWrapper.ForeColorChanged

        If windowWrapper.ForeColor <> activeForeColor Then Return


        RemoveHandler btnSearch.Click, AddressOf btnSearch_Click
        'RemoveHandler btnFilter.Click, AddressOf btnFilter_Click

        AddHandler btnSearch.Click, AddressOf btnSearch_Click
        'AddHandler btnFilter.Click, AddressOf btnFilter_Click



    End Sub
End Class