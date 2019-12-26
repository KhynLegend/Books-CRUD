
Public Class Manager

    Private cme As New ConnectMe

    Private Sub btnAction_Click(sender As Object, e As EventArgs) Handles btnAction.Click

        Select Case btnAction.Text
            Case "Add"
                Add()
            Case "Edit"
                Edit()
        End Select

    End Sub

    Private Sub Add()

        cme.execQuery("INSERT INTO books (book_name,book_author,book_genre,book_pagenumber,book_price) VALUES ('" + txtName.Text + "','" + txtAuthor.Text + "','" + txtGenre.Text + "'," + txtPageNumber.Text + "," + txtPrice.Text + ")")

        Visible = False
        mainForm.SaveChanges()
        mainForm.dgRefresh()
        mainForm.Visible = True

    End Sub

    Private Sub Edit()

        cme.execQuery("UPDATE books SET book_name = '" & txtName.Text & "', book_author = '" & txtAuthor.Text & "', book_genre = '" & txtGenre.Text & "', book_pagenumber = " & txtPageNumber.Text & ", book_price = " & txtPrice.Text & " WHERE book_id = " & txtId.Text & "")
        mainForm.SaveChanges()
        mainForm.dgRefresh()
        mainForm.Visible = True
        Visible = False

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Visible = False
        mainForm.Visible = True
        mainForm.dgRefresh()
    End Sub
End Class