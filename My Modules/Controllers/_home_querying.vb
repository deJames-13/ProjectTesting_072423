Module HomeQueries

    Public transactionQuery As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Public current_table As DataTable = Nothing
    Public current_user As DataRow = Nothing

    '
    ' ADD TO CART
    '

    Public Sub CompleteTransaction(Optional showHistory = False)

        If transactionQuery.Count < 1 Then Return
        If showHistory Then If Not OnShowResult() Then Return


        Dim query As String = ""
        transactionQuery("name") = ""
        For Each transaction As KeyValuePair(Of String, String) In transactionQuery
            query += transaction.Value & vbNewLine
        Next

        ExecuteNonQuery(query)
        transactionQuery.Clear()
    End Sub


    Public Sub AddToCart(item As DataRow, quantity As Integer)

        Dim log As String = "Item Saved!" & vbNewLine
        'log += "Item ID: " & item("id") & vbNewLine
        'log += "Item Name: " & item("item_name") & vbNewLine
        'log += "Quantity: " & quantity & vbNewLine
        'log += "Price: " & item("price") & vbNewLine
        MsgBox(log, MsgBoxStyle.Information, "Cart")

        Dim fields As String = "customer_id, product_id, quantity"
        Dim values As String = $"{current_user("id")}, {item("id")}, {quantity}"
        InsertInto("cart", fields, values)


    End Sub


    '
    ' Product rendering
    '
    Public Sub ProductFromSource(Optional source As DataTable = Nothing)

        Dim query As String = "SELECT * FROM product ORDER BY item_name ASC"
        Dim dt As New DataTable
        dt = IIf(source Is Nothing, ExecuteQuery(query), source)
        If dt.Rows.Count = 0 Then Return

        Dim parent As Control = _shoppage.itemsContainer
        Dim index As Integer
        Dim card As ProductCard

        _shoppage.itemsContainer.Controls.Clear()
        _shoppage.itemsContainer.Hide()

        For Each row As DataRow In dt.Rows


            card = New ProductCard(parent, index)
            card.MakeCard()
            card.SetProperties(row)
        Next

        _shoppage.itemsContainer.Show()

        current_table = dt
    End Sub


    '
    ' Orders rendering
    '
    Public Sub OrdersFromSource(Optional source As DataTable = Nothing)
        Dim query As String = $"

        SELECT
        ohp.order_id as id,
        ohp.product_id,
        o.customer_id,
        ohp.quantity,
        p.item_name,
        p.stock_quantity,
        p.price,
        ohp.cost,
        p.image_dir,
        o.shipping_type,
        o.ship_date,
        s.amount as shipping_fee,
        `ohp`.`status`
        FROM order_has_product as ohp
        INNER JOIN `order` as o
        ON ohp.order_id = o.id
        INNER JOIN product as p
        ON ohp.product_id = p.id
        INNER JOIN shipping_info as s
        ON o.shipping_type = s.`type`
        WHERE o.customer_id = {current_user("id")}
        
        "
        _sqlString = query


        Dim dt As New DataTable
        dt = IIf(source Is Nothing, ExecuteQuery(query), source)
        If dt.Rows.Count = 0 Then Return


        Dim order_item As Cart
        Dim parent As Control = _orderpage.order_itemContainer
        _orderpage.order_itemContainer.Controls.Clear()

        For Each row As DataRow In dt.Rows

            Dim i As Integer = row("id")
            order_item = New Cart(parent, i)
            order_item.MakeCartItem()
            order_item.SetProperties(row)
            order_item.SetOrderProperties()

            Select Case _orderpage.btnEditOrders.ForeColor
                Case Color.Red
                    order_item.btnSelect.BackgroundImage = My.Resources.icons8_unchecked_checkbox_30
                    order_item.btnSelect.ForeColor = Color.Black

                Case Color.Black
                    order_item.btnSelect.BackgroundImage = My.Resources.icons8_remove_20
                    order_item.btnSelect.ForeColor = Color.Yellow
            End Select
        Next

        current_table = dt

    End Sub


    '
    ' Cart rendering
    '
    Public Sub CartFromSource(Optional source As DataTable = Nothing)
        Dim query As String = $"
        SELECT 
        c.id,
        c.customer_id,
        cust.username,
        c.product_id,
        p.item_name,
        p.price,
        c.quantity,
        (p.price * c.quantity) as cost_per_item,
        p.image_dir

        FROM cart as c
        INNER JOIN customer as cust
        ON c.customer_id = cust.id

        INNER JOIN product as p
        ON c.product_id = p.id

        WHERE cust.id = {current_user("id")};
;
        "

        Dim dt As New DataTable
        dt = IIf(source Is Nothing, ExecuteQuery(query), source)
        If dt.Rows.Count = 0 Then Return

        Dim new_cart As Cart
        Dim parent As Control = _cartpage.itemContainer
        _cartpage.itemContainer.Controls.Clear()

        For Each row As DataRow In dt.Rows
            Dim i As Integer = row("id")
            new_cart = New Cart(parent, i)
            new_cart.MakeCartItem()
            new_cart.SetProperties(row)

            Select Case _cartpage.btnEdit.ForeColor
                Case Color.Red
                    new_cart.btnSelect.BackgroundImage = My.Resources.icons8_unchecked_checkbox_30
                    new_cart.btnSelect.ForeColor = Color.Black

                Case Color.Black
                    new_cart.btnSelect.BackgroundImage = My.Resources.icons8_remove_20
                    new_cart.btnSelect.ForeColor = Color.Yellow
            End Select
        Next

        current_table = dt
    End Sub






    Public Function OnShowResult() As Boolean
        Dim history As String = "You have pending transaction. Do you want to complete them?" & vbCrLf & vbCrLf

        For Each transaction As KeyValuePair(Of String, String) In transactionQuery
            history += transaction.Key & vbNewLine
        Next

        Dim res As Integer = MsgBox(history, MsgBoxStyle.YesNo, "Confirm Actions!")
        If res = DialogResult.No Then
            transactionQuery.Clear()
        End If

        Return res
    End Function


End Module
