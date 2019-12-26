Public Class mainForm

    Private cme

    Private Sub mainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cme = New ConnectMe

        'Execute a query and populate grid
        cme.execQuery("SELECT * FROM books ORDER BY book_id")

        LoadGrid()

    End Sub

    Public Sub LoadGrid()
        'If data is returned popular grid and build update command
        If cme.recordCount > 0 Then

            'Renaming the columns' name
            cme.dataset.Tables(0).Columns(0).ColumnName = "ID"
            cme.dataset.Tables(0).Columns(1).ColumnName = "Book Name"
            cme.dataset.Tables(0).Columns(2).ColumnName = "Author"
            cme.dataset.Tables(0).Columns(3).ColumnName = "Genre"
            cme.dataset.Tables(0).Columns(4).ColumnName = "Page Number"
            cme.dataset.Tables(0).Columns(5).ColumnName = "Price"

            dgBooks.DataSource = cme.dataset.Tables(0)

            cme.sqlda.UpdateCommand = New OleDb.OleDbCommandBuilder(cme.sqlda).GetUpdateCommand

        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Manager.Visible = True
        Manager.lblTitle.Text = "Add a book"
        Manager.btnAction.Text = "Add"
        Manager.txtId.Enabled = False

        'Manglimpyo sa ta
        Manager.txtId.Text = ""
        Manager.txtName.Text = ""
        Manager.txtAuthor.Text = ""
        Manager.txtGenre.Text = ""
        Manager.txtPageNumber.Text = ""
        Manager.txtPrice.Text = ""

        Visible = False
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Manager.Visible = True
        Manager.lblTitle.Text = "Edit a book"
        Manager.btnAction.Text = "Edit"
        Visible = False
        Manager.txtId.Enabled = False

        Dim id As Object = dgBooks(0, dgBooks.CurrentCell.RowIndex).Value.ToString
        Dim name As Object = dgBooks(1, dgBooks.CurrentCell.RowIndex).Value.ToString
        Dim author As Object = dgBooks(2, dgBooks.CurrentCell.RowIndex).Value.ToString
        Dim genre As Object = dgBooks(3, dgBooks.CurrentCell.RowIndex).Value.ToString
        Dim pagenum As Object = dgBooks(4, dgBooks.CurrentCell.RowIndex).Value.ToString
        Dim price As Object = dgBooks(5, dgBooks.CurrentCell.RowIndex).Value.ToString

        Manager.txtId.Text = CType(id, String)
        Manager.txtName.Text = CType(name, String)
        Manager.txtAuthor.Text = CType(author, String)
        Manager.txtGenre.Text = CType(genre, String)
        Manager.txtPageNumber.Text = CType(pagenum, String)
        Manager.txtPrice.Text = CType(price, String)

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim id As Object = dgBooks(0, dgBooks.CurrentCell.RowIndex).Value.ToString
        Dim result As Integer = MessageBox.Show("Are you sure you want to delete this?", "Warning", MessageBoxButtons.YesNo)
        'The row is deleted if the user clicks yes
        Select Case result
            Case DialogResult.Yes
                cme.execQuery("DELETE FROM books WHERE book_id = " + CType(id, String))
                dgRefresh()
        End Select
    End Sub

    Private Sub dgBooks_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgBooks.RowsRemoved
        SaveChanges()
    End Sub



    Public Sub SaveChanges()
        'Save updates to the database
        cme.sqlda.Update(cme.dataset)

        'Refresh The Grid
        LoadGrid()

    End Sub

    Private Sub dgBooks_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgBooks.RowsAdded
        SaveChanges()
    End Sub

    Public Sub dgRefresh()
        'Execute a query and populate grid
        cme.execQuery("SELECT * FROM books ORDER BY book_id")

        LoadGrid()
    End Sub
End Class
