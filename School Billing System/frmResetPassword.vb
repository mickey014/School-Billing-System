Imports System.Security.Cryptography.X509Certificates
Imports MySql.Data.MySqlClient

Public Class frmResetPassword
    Public username As String
    Public str_pass As String
    Public i As Integer

    Private Sub frmResetPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtOldPassword.UseSystemPasswordChar = True
        txtNewPassword.UseSystemPasswordChar = True
        txtboxCPassword.UseSystemPasswordChar = True
    End Sub

    Private Sub submitBtn_Click(sender As Object, e As EventArgs) Handles submitBtn.Click
        If txtOldPassword.Text = "" Then
            MsgBox("Old Password is Empty", vbCritical)
        Else
            Try
                cn.Open()

                cm = New MySqlCommand("SELECT * FROM tblstaff WHERE username = @username", cn)
                cm.Parameters.AddWithValue("@username", username)

                dr = cm.ExecuteReader()

                While dr.Read
                    str_pass = dr.Item("password").ToString
                End While
                dr.Close()
                cn.Close()

                If txtOldPassword.Text <> decrypt_data(str_pass) Then
                    MsgBox("Old Pasword is Incorrect!", vbCritical)
                ElseIf txtNewPassword.Text = "" Then
                    MsgBox("New Password is required!", vbCritical)
                ElseIf txtboxCPassword.Text = "" Then
                    MsgBox("Confirm Password is required!", vbCritical)
                ElseIf txtboxCPassword.Text <> txtNewPassword.Text Then
                    MsgBox("Password is not Matched!", vbCritical)
                Else

                    cn.Open()

                    cm = New MySqlCommand("UPDATE tblstaff SET password = @password WHERE username = @username", cn)
                    cm.Parameters.AddWithValue("@username", username)
                    cm.Parameters.AddWithValue("@password", encrypt_data(txtNewPassword.Text))

                    i = cm.ExecuteNonQuery()

                    If i > 0 Then
                        MsgBox("Password Reset Successfully!", vbInformation)
                        Clear()
                        frmLogin.Show()
                        Me.Hide()
                        cn.Close()
                    End If

                    cn.Close()
                End If

            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If


    End Sub

    Public Sub Clear()
        txtboxCPassword.Text = ""
        txtNewPassword.Text = ""
        txtOldPassword.Text = ""
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        frmLogin.Show()
        Me.Hide()
    End Sub

    Private Sub closeBtn_Click(sender As Object, e As EventArgs) Handles closeBtn.Click
        If MsgBox("Are You Sure?", vbQuestion + vbYesNo) = vbYes Then
            Me.Close()
        End If
    End Sub
End Class