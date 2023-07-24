Module _login_controller

    Public customer_info As Dictionary(Of String, String)
    Dim f As _login = _login

    Public Sub CreateAccount(account As Dictionary(Of String, String), Optional type As String = "customer")

        Dim fields As String = String.Join(",", account.Keys.ToArray)
        Dim values As String = String.Join(",", account.Values.ToArray)

        InsertInto(type, fields, values)

    End Sub




    Public Function isConnected() As Boolean
        Return CreateConnection()
    End Function

    Public Function userExists(name As String) As Boolean

        Dim findUser As String = $"Select * FROM login WHERE username='{name}'"
        Dim findCustomer As String = $"Select * FROM customer WHERE username='{name}'"


        Dim dt As DataTable = ExecuteQuery(findCustomer)
        If dt Is Nothing Then
            Return False
        End If
        If dt.Rows.Count > 0 Then
            Return True
        End If
        Return False

    End Function

    Public Function isValidUser(username As String, pass As String, Optional table As String = "customer") As Boolean

        Dim dt As DataTable = SelectWhere(table, $"username='{username}'", "username, password")

        If dt.Rows.Count = 0 Then
            MsgBox("User doesn't exist! Please create an account first!.", MsgBoxStyle.Exclamation, "Login error.")
            Return False
        End If


        If dt.Rows(0)("password") <> pass Then
            MsgBox("Incorrect password!", MsgBoxStyle.Exclamation, "Login error.")
            Return False
        End If

        Return True


    End Function




End Module
