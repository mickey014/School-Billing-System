Imports MySql.Data.MySqlClient
Public Class frmMaintenance
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub


    Sub LoadSection()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblsection", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("id").ToString, dr.Item("grade").ToString, dr.Item("section").ToString)
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colName As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colName = "colEdit" Then
                With frmSection
                    ._id = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
                    .cboGrade.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                    .txtSection.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
                    .btnSave.Enabled = False
                    .btnUpdate.Enabled = True
                    .ShowDialog()
                End With
            ElseIf colName = "colDelete" Then
                If MsgBox("Delete this record?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("delete from tblsection where id like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Record has been successfully deleted!", vbInformation)
                    LoadSection()
                End If
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frmFee
            .btnSave.Enabled = True
            .btnUpdate.Enabled = False
            .ShowDialog()
        End With
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With frmScholar
            .btnSave.Enabled = True
            .btnUpdate.Enabled = False
            .ShowDialog()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Dispose()
    End Sub
End Class