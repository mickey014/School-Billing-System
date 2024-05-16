Imports MySql.Data.MySqlClient
Public Class frmAY
    Sub LoadAY()
        Try
            DataGridView4.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblay", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView4.Rows.Add(dr.Item("ay").ToString, dr.Item("status").ToString)
            End While
            dr.Close()
            cn.Close()
            DataGridView4.ClearSelection()
            Dashboard()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtAY) = True Then Return
            If MsgBox("Save new academic year?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tblay set status = 'CLOSE'", cn)
                cm.ExecuteNonQuery()
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("insert into  tblay (ay) values(@ay)", cn)
                cm.Parameters.AddWithValue("@ay", txtAY.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Record has been successfully saved!", vbInformation)
                LoadAY()
                Dashboard()
                txtAY.Clear()
            End If

        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub DataGridView4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellContentClick
        Try
            Dim colname = DataGridView4.Columns(e.ColumnIndex).Name
            If colname = "colOpen" Then
                If MsgBox("Do you want to open this academic year?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("update tblay set status = 'CLOSE'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()

                    cn.Open()
                    cm = New MySqlCommand("update tblay set status = 'OPEN' where ay like '" & DataGridView4.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Academic year has been successfully opened!", vbInformation)
                    LoadAY()
                End If
            ElseIf colname = "colClose" Then
                If MsgBox("Do you want to close this academic year?", vbYesNo + vbQuestion) = vbYes Then

                    cn.Open()
                    cm = New MySqlCommand("update tblay set status = 'CLOSE' where ay like '" & DataGridView4.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Academic year has been successfully closed!", vbInformation)
                    LoadAY()
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
    End Sub
End Class