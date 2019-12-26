Imports System.Data.OleDb
Public Class ConnectMe
    'Connection
    Public conn As New OleDbConnection("Provider=MSDAORA;user id=khyn;password=khynlolpo")
    Public dr As OleDbDataReader
    Public stmt As OleDbCommand

    'Sql Data
    Public sqlda As OleDbDataAdapter
    Public dataset As DataSet

    'Query Parameters
    Public params As New List(Of OleDbParameter)

    'Query Statistics
    Public recordCount As Integer
    Public exception As String

    Public Sub execQuery(query As String)

        Try
            conn.Open()

            'Create a sql command
            stmt = New OleDbCommand(query, conn)

            'Load Paramaters
            params.ForEach(Sub(x) stmt.Parameters.Add(x))

            'Limpyohon ang sagbot sa param list
            params.Clear()

            'Fill the dataset then execute the command
            dataset = New DataSet
            sqlda = New OleDbDataAdapter(stmt)


            'Store how many records it finds or updates
            recordCount = sqlda.Fill(dataset)

            conn.Close()

        Catch ex As Exception
            exception = ex.Message
        End Try

        If conn.State = ConnectionState.Open Then conn.Close()

    End Sub

End Class
