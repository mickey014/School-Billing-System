Imports MySql.Data.MySqlClient
Public Class frmFee
    Public _id As String
    Private Sub frmFee_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmFee_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtDetails) = True Then Return
            If IS_EMPTY(txtAmount) = True Then Return
            If MsgBox("Save this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("insert into tblfee (details, amount)values(@details, @amount)", cn)
                With cm
                    .Parameters.AddWithValue("@details", txtDetails.Text)
                    .Parameters.AddWithValue("@amount", CDbl(txtAmount.Text))
                    .ExecuteNonQuery()
                End With
                cn.Close()
                MsgBox("Record has been successfully saved!", vbInformation)
                Clear()
                frmFeeList.LoadFee()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
    Sub Clear()
        txtDetails.Clear()
        txtAmount.Text = ""
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        txtDetails.Focus()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(txtDetails) = True Then Return
            If IS_EMPTY(txtAmount) = True Then Return
            If MsgBox("Update this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tblfee set details=@details, amount=@amount where id =@id", cn)
                With cm
                    .Parameters.AddWithValue("@details", txtDetails.Text)
                    .Parameters.AddWithValue("@amount", CDbl(txtAmount.Text))
                    .Parameters.AddWithValue("@id", _id)
                    .ExecuteNonQuery()
                End With
                cn.Close()
                MsgBox("Record has been successfully updated!", vbInformation)
                Clear()
                frmFeeList.LoadFee()
                Me.Dispose()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clear()
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 46
            Case 8
            Case Else
                e.Handled = True
        End Select
    End Sub
End Class