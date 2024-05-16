Imports MySql.Data.MySqlClient
Public Class frmBilling
    Public _lrn As String = ""
    Private Sub frmBilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colname = "colFee" Then
                If _lrn = String.Empty Then
                    MsgBox("Please select student!", vbCritical)
                    Return
                End If
                If MsgBox("Do you want to add this fee?", vbYesNo + vbQuestion) = vbYes Then
                    Dim found As Boolean = False
                    cn.Open()
                    cm = New MySqlCommand("select * from tblbilling where lrn like '" & _lrn & "' and ay like '" & frmDashboard.lblAY.Text & "' and details like '" & DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        found = True
                    End If
                    dr.Close()
                    cn.Close()

                    If found = True Then
                        MsgBox("Duplicate entry!", vbCritical)
                        Return
                    Else
                        cn.Open()
                        cm = New MySqlCommand("insert into tblbilling(lrn, ay, details, fee)values(@lrn, @ay, @details, @fee)", cn)
                        cm.Parameters.AddWithValue("@lrn", _lrn)
                        cm.Parameters.AddWithValue("@ay", frmDashboard.lblAY.Text)
                        cm.Parameters.AddWithValue("@details", DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString)
                        cm.Parameters.AddWithValue("@fee", DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString)
                        cm.ExecuteNonQuery()
                        cn.Close()
                        MsgBox("Succeessfully added!", vbInformation)
                        LoadRecords()
                    End If
                End If
            End If
            frmBillingList.LoadRecords()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        Try
            Dim colname As String = DataGridView2.Columns(e.ColumnIndex).Name
            If colname = "colLess" Then
                If _lrn = String.Empty Then
                    MsgBox("Please select student!", vbCritical)
                    Return
                End If
                If MsgBox("Do you want to add this less?", vbYesNo + vbQuestion) = vbYes Then
                    Dim found As Boolean = False
                    cn.Open()
                    cm = New MySqlCommand("select * from tblbilling where lrn like '" & _lrn & "' and ay like '" & frmDashboard.lblAY.Text & "' and details like '" & DataGridView2.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        found = True
                    End If
                    dr.Close()
                    cn.Close()

                    If found = True Then
                        MsgBox("Duplicate entry!", vbCritical)
                        Return
                    Else
                        cn.Open()
                        cm = New MySqlCommand("insert into tblbilling(lrn, ay, details, less)values(@lrn, @ay, @details, @less)", cn)
                        cm.Parameters.AddWithValue("@lrn", _lrn)
                        cm.Parameters.AddWithValue("@ay", frmDashboard.lblAY.Text)
                        cm.Parameters.AddWithValue("@details", DataGridView2.Rows(e.RowIndex).Cells(1).Value.ToString)
                        cm.Parameters.AddWithValue("@less", DataGridView2.Rows(e.RowIndex).Cells(2).Value.ToString)
                        cm.ExecuteNonQuery()
                        cn.Close()
                        MsgBox("Succeessfully added!", vbInformation)
                        LoadRecords()
                    End If
                End If
            End If
            frmBillingList.LoadRecords()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        With frmStudentSearch
            .Loadrecords()
            .ShowDialog()
        End With
    End Sub

    Sub LoadFee()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblfee", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("id").ToString, dr.Item("details").ToString, dr.Item("amount").ToString)
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub LoadScholar()
        Try
            DataGridView2.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblScholar", cn)
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

    Sub LoadRecords()
        Try
            Dim _total As Double = 0
            DataGridView3.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblbilling where lrn like '" & _lrn & "' and ay like '" & frmDashboard.lblAY.Text & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                _total += CDbl(dr.Item("fee").ToString)
                _total -= CDbl(dr.Item("less").ToString)
                DataGridView3.Rows.Add(dr.Item("id").ToString, dr.Item("details").ToString, dr.Item("fee").ToString, dr.Item("less").ToString)
            End While
            dr.Close()
            cn.Close()
            DataGridView3.ClearSelection()
            txtTotal.Text = Format(_total, "#,##0.00")
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub frmBilling_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = (Me.Width - Panel1.Width) / 2
    End Sub
End Class