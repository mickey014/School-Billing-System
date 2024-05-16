Imports MySql.Data.MySqlClient
Public Class frmStudentList
    Sub Loadrecords()
        Try
            DataGridView1.Rows.Clear()
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select lrn, concat(lname, ', ' , fname, ' ' , mname) as fullname, address, contact, email, grade, section from tblstudent where concat(lname, ', ' , fname, ' ' , mname) like '%" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("lrn").ToString, dr.Item("fullname").ToString, dr.Item("address").ToString, dr.Item("contact").ToString, dr.Item("email").ToString, dr.Item("grade").ToString, dr.Item("section").ToString)
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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colname = "colView" Then
                With frmStudent
                    .txtLRN.Enabled = False
                    Dim _grade, _section As String
                    cn.Open()
                    cm = New MySqlCommand("select * from tblstudent where lrn like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        .txtLRN.Text = dr.Item("lrn").ToString
                        .txtLname.Text = dr.Item("lname").ToString
                        .txtFname.Text = dr.Item("fname").ToString
                        .txtMI.Text = dr.Item("mname").ToString
                        .txtContact.Text = dr.Item("contact").ToString
                        .txtAddress.Text = dr.Item("address").ToString
                        .txtEmail.Text = dr.Item("email").ToString
                        _grade = dr.Item("grade").ToString
                        _section = dr.Item("section").ToString
                    End If
                    dr.Close()
                    cn.Close()
                    .cboGrade.Text = _grade
                    .cboSection.Text = _section
                    .lblAction.Text = "Update Student Record"
                    .btnUpdate.Visible = True
                    .btnSave.Visible = False
                    .TopLevel = False
                    frmMain.Panel5.Controls.Add(frmStudent)
                    .BringToFront()
                    .Show()
                End With
            ElseIf colname = "colDelete" Then
                If MsgBox("Delete this student?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("delete from tblstudent where lrn like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("record has been successfully deleted!", vbInformation)
                    Loadrecords()
                End If
            End If

        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frmStudent
            .lblAction.Text = "Create New Student"
            .btnUpdate.Visible = False
            .btnSave.Visible = True
            .TopLevel = False
            frmMain.Panel5.Controls.Add(frmStudent)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Loadrecords()
        Try
            DataGridView1.Rows.Clear()
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select lrn, concat(lname, ', ' , fname, ' ' , mname) as fullname, address, contact, email, grade, section from tblstudent where lrn like '%" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("lrn").ToString, dr.Item("fullname").ToString, dr.Item("address").ToString, dr.Item("contact").ToString, dr.Item("email").ToString, dr.Item("grade").ToString, dr.Item("section").ToString)
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
            txtSearch.Text = ""
            Dashboard()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub refreshBtn_Click(sender As Object, e As EventArgs) Handles refreshBtn.Click
        Loadrecords()
    End Sub


End Class