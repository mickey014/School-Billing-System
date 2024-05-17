Imports MySql.Data.MySqlClient
Public Class frmStudent
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        With frmSection

            .btnUpdate.Enabled = False
            .ShowDialog()
        End With
    End Sub

    Private Sub cboSection_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboSection.KeyPress, cboGrade.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtLRN) = True Then Return
            If IS_EMPTY(txtLRN) = True Then Return
            If IS_EMPTY(txtFname) = True Then Return
            If IS_EMPTY(txtAddress) = True Then Return
            If IS_EMPTY(txtContact) = True Then Return
            If IS_EMPTY(txtEmail) = True Then Return
            If IS_EMPTY(cboGrade) = True Then Return
            If IS_EMPTY(cboSection) = True Then Return
            If MsgBox("Save this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("insert into tblstudent(lrn, lname, fname, mname, address, contact, email, age, sex, grade, section)values(@lrn, @lname, @fname, @mname, @address, @contact, @email, @age, @sex, @grade, @section)", cn)
                With cm
                    .Parameters.AddWithValue("lrn", txtLRN.Text)
                    .Parameters.AddWithValue("lname", txtLname.Text)
                    .Parameters.AddWithValue("fname", txtFname.Text)
                    .Parameters.AddWithValue("mname", txtMI.Text)
                    .Parameters.AddWithValue("address", txtAddress.Text)
                    .Parameters.AddWithValue("contact", txtContact.Text)
                    .Parameters.AddWithValue("email", txtEmail.Text)
                    .Parameters.AddWithValue("age", txtAge.Text)
                    .Parameters.AddWithValue("sex", cboSex.Text)
                    .Parameters.AddWithValue("grade", cboGrade.Text)
                    .Parameters.AddWithValue("section", cboSection.Text)
                    .ExecuteNonQuery()
                End With
                cn.Close()
                MsgBox("Record has been successfully saved!", vbInformation)
                Clear()
                frmStudentList.Loadrecords()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        txtAddress.Clear()
        txtContact.Clear()
        txtEmail.Clear()
        txtAge.Clear()
        txtFname.Clear()
        txtLname.Clear()
        txtLRN.Clear()
        txtMI.Clear()
        cboSection.Text = ""
        cboGrade.Text = ""
        cboSex.Text = ""
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        txtLRN.Enabled = True
        txtLRN.Focus()
    End Sub

    Sub LoadSection()
        Try
            cboSection.Items.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblsection where grade like '" & cboGrade.Text & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                cboSection.Items.Add(dr.Item("section").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub cboGrade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGrade.SelectedIndexChanged
        LoadSection()
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(txtLRN) = True Then Return
            If IS_EMPTY(txtLRN) = True Then Return
            If IS_EMPTY(txtFname) = True Then Return
            If IS_EMPTY(txtAddress) = True Then Return
            If IS_EMPTY(txtContact) = True Then Return
            If IS_EMPTY(txtEmail) = True Then Return
            If IS_EMPTY(txtAge) = True Then Return
            If IS_EMPTY(cboSex) = True Then Return
            If IS_EMPTY(cboGrade) = True Then Return
            If IS_EMPTY(cboSection) = True Then Return
            If MsgBox("Update this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tblstudent set lname=@lname, fname=@fname, mname=@mname, address=@address, contact=@contact, email=@email, age=@age, sex=@sex,grade=@grade, section=@section where lrn = @lrn", cn)
                With cm
                    .Parameters.AddWithValue("lname", txtLname.Text)
                    .Parameters.AddWithValue("fname", txtFname.Text)
                    .Parameters.AddWithValue("mname", txtMI.Text)
                    .Parameters.AddWithValue("address", txtAddress.Text)
                    .Parameters.AddWithValue("contact", txtContact.Text)
                    .Parameters.AddWithValue("email", txtEmail.Text)
                    .Parameters.AddWithValue("age", txtAge.Text)
                    .Parameters.AddWithValue("sex", cboSex.Text)
                    .Parameters.AddWithValue("grade", cboGrade.Text)
                    .Parameters.AddWithValue("section", cboSection.Text)
                    .Parameters.AddWithValue("lrn", txtLRN.Text)
                    .ExecuteNonQuery()
                End With
                cn.Close()
                MsgBox("Record has been successfully updated!", vbInformation)
                Clear()
                frmStudentList.Loadrecords()

            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub frmStudentList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmStudentList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = (Me.Width - Panel1.Width) / 2
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
    End Sub
End Class