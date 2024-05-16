Imports MySql.Data.MySqlClient
Public Class frmSchorlarList
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frmScholar
            .btnSave.Enabled = True
            .btnUpdate.Enabled = False
            .ShowDialog()
        End With
    End Sub

    Sub LoadScholar()
        Try
            DataGridView3.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblScholar", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView3.Rows.Add(dr.Item("id").ToString, dr.Item("details").ToString, dr.Item("amount").ToString)
            End While
            dr.Close()
            cn.Close()
            DataGridView3.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick
        Try
            Dim colname = DataGridView3.Columns(e.ColumnIndex).Name
            If colname = "colEScholar" Then
                With frmScholar
                    ._id = DataGridView3.Rows(e.RowIndex).Cells(0).Value.ToString
                    .txtDetails.Text = DataGridView3.Rows(e.RowIndex).Cells(1).Value.ToString
                    .txtAmount.Text = DataGridView3.Rows(e.RowIndex).Cells(2).Value.ToString
                    .btnSave.Enabled = False
                    .btnUpdate.Enabled = True
                    .ShowDialog()
                End With
            ElseIf colname = "colDScholar" Then
                If MsgBox("Do you want to delete record?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("delete from tblscholar where id like '" & DataGridView3.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Record has been successfully closed!", vbInformation)
                    LoadScholar()
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
End Class