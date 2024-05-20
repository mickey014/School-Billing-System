Imports MySql.Data.MySqlClient

Public Class frmForgotPassword
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        frmLogin.Show()
    End Sub

    Private Sub submitBtn_Click(sender As Object, e As EventArgs) Handles submitBtn.Click

        If (txtUsername.Text = "") Then
            MsgBox("Username is required!", vbCritical)
        Else
            Try
                cn.Open()

                cm = New MySqlCommand("SELECT * FROM tblstaff WHERE username = @username", cn)
                cm.Parameters.AddWithValue("@username", txtUsername.Text)

                dr = cm.ExecuteReader()
                If (dr.HasRows = False) Then
                    MsgBox(txtUsername.Text + " is not exist!", vbCritical)
                    dr.Close()
                    cn.Close()
                Else
                    cn.Close()
                    frmResetPassword.username = txtUsername.Text
                    Me.Hide()
                    frmResetPassword.Show()
                End If
            Catch ex As Exception
                cn.Close()
                MsgBox(ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Private Sub closeBtn_Click(sender As Object, e As EventArgs) Handles closeBtn.Click
        If MsgBox("Are You Sure?", vbQuestion + vbYesNo) = vbYes Then
            Me.Close()
        End If
    End Sub
End Class