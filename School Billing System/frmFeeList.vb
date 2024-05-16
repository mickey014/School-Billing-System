Imports MySql.Data.MySqlClient
Public Class frmFeeList
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
    End Sub

    Sub LoadFee()
        Try
            DataGridView2.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblfee", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView2.Rows.Add(dr.Item("id").ToString, dr.Item("details").ToString, dr.Item("amount").ToString)
            End While
            dr.Close()
            cn.Close()
            DataGridView2.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        Try
            Dim colname = DataGridView2.Columns(e.ColumnIndex).Name
            If colname = "colEFee" Then
                With frmFee
                    ._id = DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString
                    .txtDetails.Text = DataGridView2.Rows(e.RowIndex).Cells(1).Value.ToString
                    .txtAmount.Text = DataGridView2.Rows(e.RowIndex).Cells(2).Value.ToString
                    .btnSave.Enabled = False
                    .btnUpdate.Enabled = True
                    .ShowDialog()
                End With
            ElseIf colname = "colDFee" Then
                If MsgBox("Do you want to delete record?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("delete from tblfee where id like '" & DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Record has been successfully closed!", vbInformation)
                    LoadFee()
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frmFee
            .btnSave.Enabled = True
            .btnUpdate.Enabled = False
            .ShowDialog()
        End With
    End Sub
End Class