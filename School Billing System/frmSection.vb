Imports MySql.Data.MySqlClient
Public Class frmSection
    Public _id As String
    Private Sub cboGrade_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboGrade.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(cboGrade) = True Then Return
            If IS_EMPTY(txtSection) = True Then Return
            If MsgBox("Save this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("insert into tblsection (grade, section)values(@grade, @section)", cn)
                With cm
                    .Parameters.AddWithValue("@grade", cboGrade.Text)
                    .Parameters.AddWithValue("@section", txtSection.Text)
                    .ExecuteNonQuery()
                End With
                cn.Close()
                MsgBox("Record has been successfully saved!", vbInformation)
                Clear()
                frmSectionList.LoadSection()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        txtSection.Clear()
        cboGrade.Text = ""
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        cboGrade.Focus()
    End Sub


    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(cboGrade) = True Then Return
            If IS_EMPTY(txtSection) = True Then Return
            If MsgBox("Update this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tblsection set grade=@grade, section=@section where id =@id", cn)
                With cm
                    .Parameters.AddWithValue("@grade", cboGrade.Text)
                    .Parameters.AddWithValue("@section", txtSection.Text)
                    .Parameters.AddWithValue("@id", _id)
                    .ExecuteNonQuery()
                End With
                cn.Close()
                MsgBox("Record has been successfully updated!", vbInformation)
                Clear()
                frmSectionList.LoadSection()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub frmSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmSection_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
End Class