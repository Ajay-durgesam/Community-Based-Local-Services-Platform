﻿Imports System.Text.RegularExpressions
Imports System.IO

Public Class Edit_Profile_Customer

    Dim isStrongPassword As Boolean = False
    Public imageBytes As Byte()
    Private Sub registerLocation_Click(sender As Object, e As EventArgs) Handles registerLocation.Click

    End Sub

    Private Sub RemovePreviousForm()
        ' Check if any form is already in Panel5
        If SessionManager.Panel3.Controls.Count > 0 Then
            ' Remove the first control (form) from Panel5
            SessionManager.Panel3.Controls.Clear()
        End If
    End Sub

    Private Sub Email_Text_TextChanged(sender As Object, e As EventArgs) Handles email_Text.TextChanged
        ' Call the function to validate the email format
        ValidateEmailFormat(email_Text.Text)
        SessionManager.mailVerified = False
    End Sub

    Private Sub phone_Text_KeyPress(sender As Object, e As KeyPressEventArgs) Handles phone_Text.KeyPress
        ' Check if the pressed key is a number or the '+' symbol
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            ' If the pressed key is not a number or '+', suppress it
            e.Handled = True
        End If

        ' Check if the length of the text exceeds 13 characters
        If phone_Text.Text.Length >= 10 AndAlso Not Char.IsControl(e.KeyChar) Then
            ' If the length exceeds 13 characters and the pressed key is not a control character, suppress it
            e.Handled = True
        End If
    End Sub


    Private Sub ValidateEmailFormat(email As String)
        ' Define a regular expression pattern for email validation
        Dim emailPattern As String = "^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
        Dim regex As New Regex(emailPattern)

        ' Check if the email matches the pattern
        If regex.IsMatch(email) Then
            ' Email format is correct
            emailValidationLabel.Text = "Valid email format"
            emailValidationLabel.ForeColor = Color.Green
        Else
            ' Email format is incorrect
            emailValidationLabel.Text = "Invalid email format"
            emailValidationLabel.ForeColor = Color.Red
        End If
    End Sub
    Private Function ValidateEmailFormatforSubmit(email As String) As Boolean
        ' Define a regular expression pattern for email validation
        Dim emailPattern As String = "^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
        Dim regex As New Regex(emailPattern)

        ' Check if the email matches the pattern
        If regex.IsMatch(email) Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            OpenFileDialogRegister.Title = "Open Picture"
            OpenFileDialogRegister.Filter = "JPEG Files|*.jpg;*.jpeg|PNG Files|*.png|All Files|*.*"
            OpenFileDialogRegister.ShowDialog()
            If OpenFileDialogRegister.FileName <> "" Then
                registerProfilePic.Image = System.Drawing.Image.FromFile(OpenFileDialogRegister.FileName)
                ' Read the selected image file into a byte array
                imageBytes = File.ReadAllBytes(OpenFileDialogRegister.FileName)
            End If
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Function ContainsUpperCase(ByVal inputString As String) As Boolean
        For Each character As Char In inputString
            If Char.IsUpper(character) Then
                Return True
            End If
        Next
        Return False
    End Function

    Function ContainsLowerCase(ByVal inputString As String) As Boolean
        For Each character As Char In inputString
            If Char.IsLower(character) Then
                Return True
            End If
        Next
        Return False
    End Function

    Function ContainsDigit(ByVal inputString As String) As Boolean
        For Each character As Char In inputString
            If Char.IsDigit(character) Then
                Return True
            End If
        Next
        Return False
    End Function

    Function ContainsSpecialCharacter(ByVal inputString As String) As Boolean
        For Each character As Char In inputString
            If Not Char.IsLetterOrDigit(character) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub password_Text_TextChanged(sender As Object, e As EventArgs) Handles password_Text.TextChanged
        Dim password As String = password_Text.Text

        Dim checksPassed As Integer = 0

        ' Checking length criteria
        If password.Length >= 8 Then
            checksPassed += 1
            labelSize.Text = "✓" & labelSize.Text.Substring(1)
            labelSize.ForeColor = Color.Green
        Else
            labelSize.Text = "✗" & labelSize.Text.Substring(1)
            labelSize.ForeColor = Color.Red
        End If

        ' Checking if the password has a digit
        If ContainsDigit(password) Then
            checksPassed += 1
            labelNumbers.Text = "✓" & labelNumbers.Text.Substring(1)
            labelNumbers.ForeColor = Color.Green
        Else
            labelNumbers.Text = "✗" & labelNumbers.Text.Substring(1)
            labelNumbers.ForeColor = Color.Red
        End If

        ' Checking if the password has a lowercase letter
        If ContainsLowerCase(password) Then
            checksPassed += 1
            labelLower.Text = "✓" & labelLower.Text.Substring(1)
            labelLower.ForeColor = Color.Green
        Else
            labelLower.Text = "✗" & labelLower.Text.Substring(1)
            labelLower.ForeColor = Color.Red
        End If

        ' Checking if the password has a special character
        If ContainsSpecialCharacter(password) Then
            checksPassed += 1
            labelSpecial.Text = "✓" & labelSpecial.Text.Substring(1)
            labelSpecial.ForeColor = Color.Green
        Else
            labelSpecial.Text = "✗" & labelSpecial.Text.Substring(1)
            labelSpecial.ForeColor = Color.Red
        End If

        ' Checking if the password has an uppercase letter
        If ContainsUpperCase(password) Then
            checksPassed += 1
            labelUpper.Text = "✓" & labelUpper.Text.Substring(1)
            labelUpper.ForeColor = Color.Green
        Else
            labelUpper.Text = "✗" & labelUpper.Text.Substring(1)
            labelUpper.ForeColor = Color.Red
        End If

        If checksPassed < 5 Then
            Return
        End If

        isStrongPassword = True
    End Sub

    Private Sub RegisterSubmitBtn_Click(sender As Object, e As EventArgs) Handles RegisterSubmitBtn.Click

        ' Check if any of the textboxes are empty
        If String.IsNullOrWhiteSpace(email_Text.Text) OrElse
        String.IsNullOrWhiteSpace(name_Text.Text) OrElse
        String.IsNullOrWhiteSpace(phone_Text.Text) OrElse
        String.IsNullOrWhiteSpace(password_Text.Text) OrElse
        String.IsNullOrWhiteSpace(confirm_Text.Text) OrElse
        String.IsNullOrWhiteSpace(address.Text) OrElse
        String.IsNullOrWhiteSpace(locationDropdown.Text) Then
            ' If any field is empty, display error message
            MessageBox.Show("Please fill in all fields.")
            Return ' Exit the event handler
        End If

        If Not ValidateEmailFormatforSubmit(email_Text.Text) Then

            ' Email format is invalid, display error message
            MessageBox.Show("Invalid email format. Please enter a valid email.")
            Return ' Exit the event handler
        End If

        If password_Text.Text <> confirm_Text.Text Then
            ' Passwords don't match, display error message
            MessageBox.Show("Passwords do not match. Please re-enter the passwords.")
            ' Clear the text in the textboxes to allow the user to re-enter the passwords
            password_Text.Clear()
            confirm_Text.Clear()

            isStrongPassword = False
            Return

        End If

        If Not isStrongPassword Then
            MessageBox.Show("Enter a strong password.")
            Return
        End If



        Dim connectionString As String = SessionManager.connectionString
        Dim query As String = "UPDATE users AS u
                          INNER JOIN contactDetails AS cd ON u.userID = cd.userID
                          SET u.userName = @userName, 
                              u.email = @email, 
                              u.password = @password,
                              cd.location = @location, 
                              cd.address = @address, 
                              cd.mobileNumber = @mobile,
                              u.userPhoto = @userPhoto
                          WHERE u.userID = @userID"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                ' Set parameters with the new values from the text boxes
                command.Parameters.AddWithValue("@userName", name_Text.Text)
                command.Parameters.AddWithValue("@email", email_Text.Text)
                command.Parameters.AddWithValue("@password", password_Text.Text)
                command.Parameters.AddWithValue("@location", locationDropdown.Text)
                command.Parameters.AddWithValue("@address", address.Text)
                command.Parameters.AddWithValue("@mobile", phone_Text.Text)
                ' Set the userID parameter to identify the user to update
                command.Parameters.AddWithValue("@userID", SessionManager.userID)

                command.Parameters.AddWithValue("@userPhoto", imageBytes)

                Try
                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("User details updated successfully.")
                    Else
                        MessageBox.Show("No changes were made.")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error updating user details: " & ex.Message)
                Finally
                    connection.Close()
                End Try


            End Using
        End Using


        RemovePreviousForm()
        Profile_Customer.Margin = New Padding(0, 0, 0, 0)
        With Profile_Customer
            .TopLevel = False
            .Dock = DockStyle.Fill
            SessionManager.Panel3.Controls.Add(Profile_Customer)
            .BringToFront()
            .Show()

        End With

    End Sub
    Private Sub Edit_Profile_Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Assuming you have the userID stored in SessionManager.userID
        'LoadUserDetails(15)
        If SessionManager.userID <> 0 Then
            LoadUserDetails(SessionManager.userID)
        Else
            MessageBox.Show("User ID not available.")
        End If
    End Sub

    Private Sub LoadUserDetails(userId As Integer)
        Dim connectionString As String = SessionManager.connectionString
        Dim query As String = "SELECT  users.password, users.userName,users.userPhoto, users.email AS user_email, contactDetails.email AS contact_email, contactDetails.mobileNumber AS contact_mobile, contactDetails.address, contactDetails.location 
FROM users 
INNER JOIN contactDetails ON users.userID = contactDetails.userID 
WHERE users.userID = @userID


"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@userID", userId)

                Try
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        name_Text.Text = reader.GetString("userName")
                        email_Text.Text = reader.GetString("user_email")
                        phone_Text.Text = reader.GetString("contact_mobile")
                        address.Text = reader.GetString("address")
                        locationDropdown.Text = reader.GetString("location")
                        password_Text.Text = reader.GetString("password")
                        confirm_Text.Text = reader.GetString("password")

                        ' Retrieve the user photo byte array from the database
                        If Not reader.IsDBNull(reader.GetOrdinal("userPhoto")) Then
                            Dim userPhoto As Byte() = DirectCast(reader("userPhoto"), Byte())

                            ' Check if user photo is not null
                            If userPhoto IsNot Nothing AndAlso userPhoto.Length > 0 Then
                                ' Convert byte array to image and display it in the picture box
                                Using ms As New MemoryStream(userPhoto)
                                    registerProfilePic.SizeMode = PictureBoxSizeMode.StretchImage
                                    registerProfilePic.Image = Image.FromStream(ms)
                                End Using
                            Else
                                ' If user photo is null, set a default image or display a placeholder
                                Profile_Customer.customerProfilePicture.Image = My.Resources.Resource1.displayPicture
                            End If
                        End If

                    Else
                        MessageBox.Show("User not found.")
                    End If

                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error loading user details: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub PopulateCountriesDropdown()
        ' Add countries manually to the dropdown list
        Dim megacitiesOfIndia As String() = {
                                        "Mumbai",
                                        "Delhi",
                                        "Bangalore",
                                        "Kolkata",
                                        "Chennai",
                                        "Hyderabad",
                                        "Ahmedabad",
                                        "Pune"
                                    }

        'locationDropdown.Items.Clear()
        locationDropdown.Items.AddRange(megacitiesOfIndia)

    End Sub
End Class