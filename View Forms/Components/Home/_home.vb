Public Class _home
    Private Sub home_FormClosed() Handles MyBase.FormClosed
        Application.Exit()
    End Sub

    Private Sub _home_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _profilepage.Label8.Text = current_user("username")


        'AddHandler btnDebug.Click, AddressOf btnDebugger_Click

        AddHandler btnShop.MouseEnter, AddressOf HoverEvents
        AddHandler btnCart.MouseEnter, AddressOf HoverEvents
        AddHandler btnProfile.MouseEnter, AddressOf HoverEvents
        AddHandler btnOrders.MouseEnter, AddressOf HoverEvents
        AddHandler btnSettings.MouseEnter, AddressOf HoverEvents

        AddHandler btnShop.MouseLeave, AddressOf HoverEvents
        AddHandler btnCart.MouseLeave, AddressOf HoverEvents
        AddHandler btnProfile.MouseLeave, AddressOf HoverEvents
        AddHandler btnSettings.MouseLeave, AddressOf HoverEvents
        AddHandler btnOrders.MouseLeave, AddressOf HoverEvents

        AddHandler btnDebugger.Click, AddressOf btnDebugger_Click

    End Sub

    Private Sub selectNav(sender As Object, e As EventArgs) Handles MyBase.Load, btnShop.Click, btnCart.Click, btnProfile.Click, btnSettings.Click, btnOrders.Click



        Select Case sender.name
            Case MyBase.Name
                switchPanel(_shoppage.windowWrapper, windowWrapper)
                ProductFromSource()

            Case "btnShop"
                switchPanel(_shoppage.windowWrapper, currentPage)

            Case "btnCart"
                switchPanel(_cartpage.windowWrapper, currentPage)

            Case "btnOrders"
                switchPanel(_orderpage.windowWrapper, currentPage)

            Case "btnProfile"
                switchPanel(_profilepage.windowWrapper, currentPage)

            Case "btnSettings"
                switchPanel(_settingspage.windowWrapper, currentPage)
        End Select

        If transactionQuery.Count > 0 AndAlso transactionQuery("name") = "delete" Then CompleteTransaction()
    End Sub

End Class
