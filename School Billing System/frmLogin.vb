Imports System.Drawing.Drawing2D
Imports System.Linq.Expressions
Imports MySql.Data.MySqlClient
Public Class frmLogin
    Dim str As String
    Private Sub loginBtn_Click(sender As Object, e As EventArgs) Handles loginBtn.Click

        If (txtUsername.Text = "" Or txtPassword.Text = "") Then
            MsgBox("Incorrect Username or Password", vbCritical)
        ElseIf txtCaptcha.Text <> str Then
            MsgBox("Captcha is Incorrect", vbCritical)
        Else
            Try
                Dim str_name As String = ""
                Dim str_username As String = ""
                Dim str_pass As String = ""

                cn.Open()
                cm = New MySqlCommand("SELECT * FROM tblstaff WHERE username = @username", cn)
                cm.Parameters.AddWithValue("@username", txtUsername.Text)
                dr = cm.ExecuteReader

                While dr.Read
                    str_name = dr.Item("name").ToString
                    str_username = dr.Item("username").ToString
                    str_pass = dr.Item("password").ToString
                End While
                dr.Close()
                If txtUsername.Text = str_username And txtPassword.Text = decrypt_data(str_pass) Then
                    cn.Close()
                    MessageBox.Show("Welcome " + str_name + "!")
                    frmMain.lblName.Text = "Hello " + str_name + "!"
                    Me.Hide()
                    frmMain.Show()
                    Clear()
                Else
                    MsgBox("Incorrect Username or Password", vbCritical)
                    txtUsername.Focus()
                    cn.Close()
                End If




            Catch ex As Exception
                cn.Close()
            End Try
            cn.Close()
        End If

        cn.Close()
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connection()
        txtPassword.UseSystemPasswordChar = True
        LoadCaptcha()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        frmSignup.Show()
        Me.Hide()
    End Sub

    Private Sub Clear()
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtCaptcha.Text = ""
        LoadCaptcha()
    End Sub

    Private Sub LoadCaptcha()
        Dim NumCaptcha As String = "123456789QWERTYUIOPASDFHGJKLZXCVBNMqwertyuiopasdfgjhklzxcvbnm"
        str = ""

        Dim R As New Random

        For i As Integer = 0 To 5
            str = str + NumCaptcha(R.Next(0, 60))
        Next

        Dim b As New Bitmap(274, 71, Imaging.PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim Hb As New HatchBrush(HatchStyle.DottedDiamond, Color.FromArgb(255, 128, 0), Color.Black)
        g.DrawString(Str, New Font("Curlz MT", 40, FontStyle.Strikeout, GraphicsUnit.Point), Brushes.White, 5, 5)
        picCaptcha.Image = b

    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        LoadCaptcha()
    End Sub

    Private Sub forgotBtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles forgotBtn.LinkClicked
        Me.Hide()
        frmForgotPassword.Show()
    End Sub

    Private Sub closeBtn_Click(sender As Object, e As EventArgs) Handles closeBtn.Click
        If MsgBox("Are You Sure?", vbQuestion + vbYesNo) = vbYes Then
            Me.Close()
        End If
    End Sub
End Class