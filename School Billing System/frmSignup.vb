Imports MySql.Data.MySqlClient

Public Class frmSignup
    Public i As Integer
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        frmLogin.Show()
        Me.Hide()
    End Sub

    Private Sub signupBtn_Click(sender As Object, e As EventArgs) Handles signupBtn.Click
        If (txtName.Text = "" Or txtUsername.Text = "" Or txtPassword.Text = "" Or txtCPassword.Text = "") Then
            MsgBox("All Fields are required!")
        ElseIf IsNumeric(txtName.Text) Then
            MsgBox("Name Field must be letters only!")
        ElseIf txtPassword.Text <> txtCPassword.Text Then
            MsgBox("Password not matched!")
        Else
            Try
                cn.Open()

                cm = New MySqlCommand("SELECT * FROM tblstaff WHERE username = @username", cn)
                cm.Parameters.AddWithValue("@username", txtUsername.Text)

                dr = cm.ExecuteReader()

                If (dr.HasRows = True) Then
                    MsgBox("Username is Already Exists", vbCritical)
                    dr.Close()
                    cn.Close()
                Else
                    cn.Close()
                    cn.Open()

                    cm = New MySqlCommand("INSERT INTO tblstaff(name, username, password) VALUES(@name,@username,@password)", cn)
                    cm.Parameters.AddWithValue("@name", txtName.Text)
                    cm.Parameters.AddWithValue("@username", txtUsername.Text)
                    cm.Parameters.AddWithValue("@password", encrypt_data(txtPassword.Text))
                    i = cm.ExecuteNonQuery()

                    If i > 0 Then
                        MsgBox("Welcome " & txtName.Text & "!", vbInformation)
                        Clear()
                        frmMain.Show()
                        Me.Hide()
                        cn.Close()

                    End If


                End If


            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If

        cn.Close()
    End Sub

    Sub Clear()
        txtName.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtCPassword.Text = ""
    End Sub

    Private Sub frmSignup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.UseSystemPasswordChar = True
        txtCPassword.UseSystemPasswordChar = True
        txtName.Focus()
    End Sub

    Private Sub closeBtn_Click(sender As Object, e As EventArgs) Handles closeBtn.Click
        If MsgBox("Are You Sure?", vbQuestion + vbYesNo) = vbYes Then
            Me.Close()
        End If
    End Sub
End Class