Imports MySql.Data.MySqlClient
Public Class frmStudentSearch
    Private Sub frmStudent_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub Loadrecords()
        Try
            DataGridView1.Rows.Clear()
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select lrn, concat(lname, ', ' , fname, ' ' , mname) as fullname from tblstudent where concat(lname, ', ' , fname, ' ' , mname) like '%" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("lrn").ToString, dr.Item("fullname").ToString)
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
            Dashboard()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Loadrecords()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colname = "colView" Then
                With frmPayment
                    cn.Open()
                    cm = New MySqlCommand("select * from tblstudent where lrn like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        ._lrn = dr.Item("lrn").ToString
                        .txtStudent.Text = dr.Item("lname").ToString & ", " & dr.Item("fname").ToString & " " & dr.Item("mname").ToString
                    End If
                    dr.Close()
                    cn.Close()
                    frmPayment.LoadRecords()
                    Me.Dispose()
                End With
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
End Class