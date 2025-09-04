Imports MySql.Data.MySqlClient
Public Class Form1

    Private connectionString As String = "server=localhost;user id=root;password=;database=db_records"

    Private Sub test_Click(sender As Object, e As EventArgs) Handles test.Click
        Dim connection As New MySqlConnection(connectionString)

        Try
            connection.Open()

            lblTest.Text = "Connected"
            lblTest.ForeColor = Color.Green
        Catch ex As MySqlException

            lblTest.Text = "Error:  " & ex.Message
            lblTest.ForeColor = Color.Red

        Finally

            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If


        End Try

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If String.IsNullOrWhiteSpace(inputFNAME.Text) OrElse
                String.IsNullOrWhiteSpace(inputLNAME.Text) Then
            MessageBox.Show("First Name and Last Name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim query = "INSERT INTO names (fname, lname) VALUES (@fname, @lname)"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@fname", inputFNAME.Text)
                command.Parameters.AddWithValue("@lname", inputLNAME.Text)

                Try
                    connection.Open()
                    command.ExecuteNonQuery()

                    MessageBox.Show("Record created succesfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    'RetrieveData()

                Catch ex As MySqlException
                    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End Try
            End Using
        End Using

    End Sub
End Class
