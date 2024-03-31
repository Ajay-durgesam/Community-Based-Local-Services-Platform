﻿Imports System.Text.RegularExpressions
Imports System.Data.SqlClient

Public Class Register1
    Dim labelfont As New Font(SessionManager.font_family, 13, FontStyle.Regular)
    Private Sub Register1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateCountriesDropdown()
        Me.Size = New Size(1200, 700)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.WindowState = FormWindowState.Normal
        Me.BackColor = ColorTranslator.FromHtml("#FFFFFF")
        Me.CenterToParent()
        email_Text.BackColor = ColorTranslator.FromHtml("#F9F9F9")
        name_Text.BackColor = ColorTranslator.FromHtml("#F9F9F9")
        phone_Text.BackColor = ColorTranslator.FromHtml("#F9F9F9")
        password_Text.BackColor = ColorTranslator.FromHtml("#F9F9F9")
        confirm_Text.BackColor = ColorTranslator.FromHtml("#F9F9F9")
        locationDropdown.BackColor = ColorTranslator.FromHtml("#F9F9F9")
        address.BackColor = ColorTranslator.FromHtml("#F9F9F9")
        registerProfilePic.BackColor = ColorTranslator.FromHtml("#F9F9F9")
        signUp.Location = New Point(138, 69)
        registerEmailLabel.Location = New Point(138, 124)
        email_Text.Location = New Point(138, 152)
        registerName.Location = New Point(138, 214)
        name_Text.Location = New Point(138, 244)
        registerNumber.Location = New Point(138, 308)
        phone_Text.Location = New Point(138, 336)
        registerPassword.Location = New Point(138, 400)
        password_Text.Location = New Point(138, 431)
        registerConfirm.Location = New Point(138, 499)
        confirm_Text.Location = New Point(138, 530)
        registerProfilePic.Location = New Point(702, 107)
        registerLocation.Location = New Point(702, 339)
        registerLocation.Size = New Size(332, 45)
        locationDropdown.Location = New Point(702, 370)
        registerAddress.Location = New Point(702, 420)
        address.Location = New Point(702, 452)
        RegisterSubmitBtn.Location = New Point(901, 552)
        RegisterSubmitBtn.Size = New Size(150, 41)
        address.Size = New Size(332, 73)
        address.BorderStyle = BorderStyle.FixedSingle
        registerEmailLabel.Font = labelfont
        registerName.Font = labelfont
        registerNumber.Font = labelfont
        registerPassword.Font = labelfont
        registerConfirm.Font = labelfont
        registerLocation.Font = labelfont
        registerAddress.Font = labelfont
        RegisterSubmitBtn.Font = labelfont
        email_Text.Font = labelfont
        name_Text.Font = labelfont
        phone_Text.Font = labelfont
        password_Text.Font = labelfont
        confirm_Text.Font = labelfont
        locationDropdown.Font = labelfont
        address.Font = labelfont

        confirm_Text.PasswordChar = "*"

    End Sub
    Private Sub Email_Text_TextChanged(sender As Object, e As EventArgs) Handles email_Text.TextChanged
        ' Call the function to validate the email format
        ValidateEmailFormat(email_Text.Text)
    End Sub

    Private Sub phone_Text_KeyPress(sender As Object, e As KeyPressEventArgs) Handles phone_Text.KeyPress
        ' Check if the pressed key is a number or the '+' symbol
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "+" AndAlso Not Char.IsControl(e.KeyChar) Then
            ' If the pressed key is not a number or '+', suppress it
            e.Handled = True
        End If

        ' Check if the length of the text exceeds 13 characters
        If phone_Text.Text.Length >= 13 AndAlso Not Char.IsControl(e.KeyChar) Then
            ' If the length exceeds 13 characters and the pressed key is not a control character, suppress it
            e.Handled = True
        End If
    End Sub



    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles registerProfilePic.Click
        Try
            OpenFileDialogRegister.Title = "Open Picture"
            OpenFileDialogRegister.Filter = "JPEG Files|*.jpg;*.jpeg|PNG Files|*.png|All Files|*.*"
            OpenFileDialogRegister.ShowDialog()
            If OpenFileDialogRegister.FileName <> "" Then
                registerProfilePic.Image = System.Drawing.Image.FromFile(OpenFileDialogRegister.FileName)
            End If
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("Error: " & ex.Message)
        End Try
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

    Private Sub RegisterSubmitBtn_Click(sender As Object, e As EventArgs) Handles RegisterSubmitBtn.Click
        ' Check if the text in the password_Text and confirm_Text textboxes match
        ' Check if any of the textboxes are empty
        If String.IsNullOrWhiteSpace(email_Text.Text) OrElse
       String.IsNullOrWhiteSpace(name_Text.Text) OrElse
       String.IsNullOrWhiteSpace(phone_Text.Text) OrElse
       String.IsNullOrWhiteSpace(password_Text.Text) OrElse
       String.IsNullOrWhiteSpace(confirm_Text.Text) Then
            ' If any field is empty, display error message
            MessageBox.Show("Please fill in all fields.")
            Return ' Exit the event handler
        End If

        If Not ValidateEmailFormatforSubmit(email_Text.Text) Then
            ' Email format is invalid, display error message
            MessageBox.Show("Invalid email format. Please enter a valid email.")
            Return ' Exit the event handler
        End If

        If password_Text.Text = confirm_Text.Text Then
            ' Passwords match, proceed with registration process
            ' Add your registration logic here
            'MessageBox.Show("Passwords match. Proceed with registration.")
        Else
            ' Passwords don't match, display error message
            MessageBox.Show("Passwords do not match. Please re-enter the passwords.")
            ' Clear the text in the textboxes to allow the user to re-enter the passwords
            password_Text.Clear()
            confirm_Text.Clear()

        End If

        Dim customerID As Integer = -1
        Dim connectionString As String = "server=172.16.114.199;userid=admin;password=istrator;database=communityservice;sslmode=none"

    End Sub
    Private Sub PopulateCountriesDropdown()
        ' Add countries manually to the dropdown list
        locationDropdown.Items.Add("Afghanistan")
        locationDropdown.Items.Add("Albania")
        locationDropdown.Items.Add("Algeria")
        locationDropdown.Items.Add("Andorra")
        locationDropdown.Items.Add("Angola")
        locationDropdown.Items.Add("Antigua and Barbuda")
        locationDropdown.Items.Add("Argentina")
        locationDropdown.Items.Add("Armenia")
        locationDropdown.Items.Add("Australia")
        locationDropdown.Items.Add("Austria")
        locationDropdown.Items.Add("Azerbaijan")
        locationDropdown.Items.Add("Bahamas")
        locationDropdown.Items.Add("Bahrain")
        locationDropdown.Items.Add("Bangladesh")
        locationDropdown.Items.Add("Barbados")
        locationDropdown.Items.Add("Belarus")
        locationDropdown.Items.Add("Belgium")
        locationDropdown.Items.Add("Belize")
        locationDropdown.Items.Add("Benin")
        locationDropdown.Items.Add("Bhutan")
        locationDropdown.Items.Add("Bolivia")
        locationDropdown.Items.Add("Bosnia and Herzegovina")
        locationDropdown.Items.Add("Botswana")
        locationDropdown.Items.Add("Brazil")
        locationDropdown.Items.Add("Brunei")
        locationDropdown.Items.Add("Bulgaria")
        locationDropdown.Items.Add("Burkina Faso")
        locationDropdown.Items.Add("Burundi")
        locationDropdown.Items.Add("Cabo Verde")
        locationDropdown.Items.Add("Cambodia")
        locationDropdown.Items.Add("Cameroon")
        locationDropdown.Items.Add("Canada")
        locationDropdown.Items.Add("Central African Republic")
        locationDropdown.Items.Add("Chad")
        locationDropdown.Items.Add("Chile")
        locationDropdown.Items.Add("China")
        locationDropdown.Items.Add("Colombia")
        locationDropdown.Items.Add("Comoros")
        locationDropdown.Items.Add("Congo")
        locationDropdown.Items.Add("Costa Rica")
        locationDropdown.Items.Add("Croatia")
        locationDropdown.Items.Add("Cuba")
        locationDropdown.Items.Add("Cyprus")
        locationDropdown.Items.Add("Czech Republic")
        locationDropdown.Items.Add("Democratic Republic of the Congo")
        locationDropdown.Items.Add("Denmark")
        locationDropdown.Items.Add("Djibouti")
        locationDropdown.Items.Add("Dominica")
        locationDropdown.Items.Add("Dominican Republic")
        locationDropdown.Items.Add("East Timor")
        locationDropdown.Items.Add("Ecuador")
        locationDropdown.Items.Add("Egypt")
        locationDropdown.Items.Add("El Salvador")
        locationDropdown.Items.Add("Equatorial Guinea")
        locationDropdown.Items.Add("Eritrea")
        locationDropdown.Items.Add("Estonia")
        locationDropdown.Items.Add("Eswatini")
        locationDropdown.Items.Add("Ethiopia")
        locationDropdown.Items.Add("Fiji")
        locationDropdown.Items.Add("Finland")
        locationDropdown.Items.Add("France")
        locationDropdown.Items.Add("Gabon")
        locationDropdown.Items.Add("Gambia")
        locationDropdown.Items.Add("Georgia")
        locationDropdown.Items.Add("Germany")
        locationDropdown.Items.Add("Ghana")
        locationDropdown.Items.Add("Greece")
        locationDropdown.Items.Add("Grenada")
        locationDropdown.Items.Add("Guatemala")
        locationDropdown.Items.Add("Guinea")
        locationDropdown.Items.Add("Guinea-Bissau")
        locationDropdown.Items.Add("Guyana")
        locationDropdown.Items.Add("Haiti")
        locationDropdown.Items.Add("Honduras")
        locationDropdown.Items.Add("Hungary")
        locationDropdown.Items.Add("Iceland")
        locationDropdown.Items.Add("India")
        locationDropdown.Items.Add("Indonesia")
        locationDropdown.Items.Add("Iran")
        locationDropdown.Items.Add("Iraq")
        locationDropdown.Items.Add("Ireland")
        locationDropdown.Items.Add("Israel")
        locationDropdown.Items.Add("Italy")
        locationDropdown.Items.Add("Ivory Coast")
        locationDropdown.Items.Add("Jamaica")
        locationDropdown.Items.Add("Japan")
        locationDropdown.Items.Add("Jordan")
        locationDropdown.Items.Add("Kazakhstan")
        locationDropdown.Items.Add("Kenya")
        locationDropdown.Items.Add("Kiribati")
        locationDropdown.Items.Add("Kuwait")
        locationDropdown.Items.Add("Kyrgyzstan")
        locationDropdown.Items.Add("Laos")
        locationDropdown.Items.Add("Latvia")
        locationDropdown.Items.Add("Lebanon")
        locationDropdown.Items.Add("Lesotho")
        locationDropdown.Items.Add("Liberia")
        locationDropdown.Items.Add("Libya")
        locationDropdown.Items.Add("Liechtenstein")
        locationDropdown.Items.Add("Lithuania")
        locationDropdown.Items.Add("Luxembourg")
        locationDropdown.Items.Add("Madagascar")
        locationDropdown.Items.Add("Malawi")
        locationDropdown.Items.Add("Malaysia")
        locationDropdown.Items.Add("Maldives")
        locationDropdown.Items.Add("Mali")
        locationDropdown.Items.Add("Malta")
        locationDropdown.Items.Add("Marshall Islands")
        locationDropdown.Items.Add("Mauritania")
        locationDropdown.Items.Add("Mauritius")
        locationDropdown.Items.Add("Mexico")
        locationDropdown.Items.Add("Micronesia")
        locationDropdown.Items.Add("Moldova")
        locationDropdown.Items.Add("Monaco")
        locationDropdown.Items.Add("Mongolia")
        locationDropdown.Items.Add("Montenegro")
        locationDropdown.Items.Add("Morocco")
        locationDropdown.Items.Add("Mozambique")
        locationDropdown.Items.Add("Myanmar")
        locationDropdown.Items.Add("Namibia")
        locationDropdown.Items.Add("Nauru")
        locationDropdown.Items.Add("Nepal")
        locationDropdown.Items.Add("Netherlands")
        locationDropdown.Items.Add("New Zealand")
        locationDropdown.Items.Add("Nicaragua")
        locationDropdown.Items.Add("Niger")
        locationDropdown.Items.Add("Nigeria")
        locationDropdown.Items.Add("North Korea")
        locationDropdown.Items.Add("North Macedonia")
        locationDropdown.Items.Add("Norway")
        locationDropdown.Items.Add("Oman")
        locationDropdown.Items.Add("Pakistan")
        locationDropdown.Items.Add("Palau")
        locationDropdown.Items.Add("Palestine")
        locationDropdown.Items.Add("Panama")
        locationDropdown.Items.Add("Papua New Guinea")
        locationDropdown.Items.Add("Paraguay")
        locationDropdown.Items.Add("Peru")
        locationDropdown.Items.Add("Philippines")
        locationDropdown.Items.Add("Poland")
        locationDropdown.Items.Add("Portugal")
        locationDropdown.Items.Add("Qatar")
        locationDropdown.Items.Add("Romania")
        locationDropdown.Items.Add("Russia")
        locationDropdown.Items.Add("Rwanda")
        locationDropdown.Items.Add("Saint Kitts and Nevis")
        locationDropdown.Items.Add("Saint Lucia")
        locationDropdown.Items.Add("Saint Vincent and the Grenadines")
        locationDropdown.Items.Add("Samoa")
        locationDropdown.Items.Add("San Marino")
        locationDropdown.Items.Add("Sao Tome and Principe")
        locationDropdown.Items.Add("Saudi Arabia")
        locationDropdown.Items.Add("Senegal")
        locationDropdown.Items.Add("Serbia")
        locationDropdown.Items.Add("Seychelles")
        locationDropdown.Items.Add("Sierra Leone")
        locationDropdown.Items.Add("Singapore")
        locationDropdown.Items.Add("Slovakia")
        locationDropdown.Items.Add("Slovenia")
        locationDropdown.Items.Add("Solomon Islands")
        locationDropdown.Items.Add("Somalia")
        locationDropdown.Items.Add("South Africa")
        locationDropdown.Items.Add("South Korea")
        locationDropdown.Items.Add("South Sudan")
        locationDropdown.Items.Add("Spain")
        locationDropdown.Items.Add("Sri Lanka")
        locationDropdown.Items.Add("Sudan")
        locationDropdown.Items.Add("Suriname")
        locationDropdown.Items.Add("Sweden")
        locationDropdown.Items.Add("Switzerland")
        locationDropdown.Items.Add("Syria")
        locationDropdown.Items.Add("Taiwan")
        locationDropdown.Items.Add("Tajikistan")
        locationDropdown.Items.Add("Tanzania")
        locationDropdown.Items.Add("Thailand")
        locationDropdown.Items.Add("Togo")
        locationDropdown.Items.Add("Tonga")
        locationDropdown.Items.Add("Trinidad and Tobago")
        locationDropdown.Items.Add("Tunisia")
        locationDropdown.Items.Add("Turkey")
        locationDropdown.Items.Add("Turkmenistan")
        locationDropdown.Items.Add("Tuvalu")
        locationDropdown.Items.Add("Uganda")
        locationDropdown.Items.Add("Ukraine")
        locationDropdown.Items.Add("United Arab Emirates")
        locationDropdown.Items.Add("United Kingdom")
        locationDropdown.Items.Add("United States")
        locationDropdown.Items.Add("Uruguay")
        locationDropdown.Items.Add("Uzbekistan")
        locationDropdown.Items.Add("Vanuatu")
        locationDropdown.Items.Add("Vatican City")
        locationDropdown.Items.Add("Venezuela")
        locationDropdown.Items.Add("Vietnam")
        locationDropdown.Items.Add("Yemen")
        locationDropdown.Items.Add("Zambia")
        locationDropdown.Items.Add("Zimbabwe")
    End Sub


End Class