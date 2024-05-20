Public Class frmBillingList


    Sub LoadRecords()
        Try
            DataGridView1.Rows.Clear()

            cn.Open()
            '  cm = New MySql.Data.MySqlClient.MySqlCommand("select * from vwtuition where (ay like '" & frmDashboard.lblAY.Text & "' or ay like '') and fullname like '%" & txtSearch.Text & "%'", cn)
            cm = New MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM vwtuition AS T INNER JOIN VWBALANCE AS B ON T.LRN = B.LRN AND T.AY = B.AY where (B.ay like '" & frmDashboard.lblAY.Text & "' or B.ay like '') and B.fullname like '%" & txtSearch.Text & "%'", cn)
            'SELECT * FROM vwtuition AS T INNER JOIN VWBALANCE AS B ON T.LRN = B.LRN AND T.AY = B.AY
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("lrn").ToString, dr.Item("fullname").ToString, dr.Item("gradesection").ToString, dr.Item("fee").ToString, dr.Item("less").ToString, dr.Item("TOTAL").ToString, dr.Item("PAYMENT").ToString, dr.Item("BALANCE").ToString, dr.Item("ay").ToString)
            End While
            dr.Close()
            cn.Close()
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
                With frmBilling
                    ._lrn = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
                    .txtStudent.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                    .txtAY.Text = frmDashboard.lblAY.Text
                    .LoadRecords()
                    .LoadFee()
                    .LoadScholar()
                    .TopLevel = False
                    frmMain.Panel5.Controls.Add(frmBilling)
                    .BringToFront()
                    .Show()
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    With frmBilling
    '        If frmDashboard.lblAY.Text = "" Then
    '            MsgBox("No academic year selected!", vbInformation)
    '            Return
    '        Else
    '            .txtAY.Text = frmDashboard.lblAY.Text
    '            .LoadFee()
    '            .LoadScholar()
    '            .ShowDialog()
    '        End If

    '    End With
    'End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub refreshBtn_Click(sender As Object, e As EventArgs) Handles refreshBtn.Click
        LoadRecords()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            DataGridView1.Rows.Clear()

            cn.Open()
            '  cm = New MySql.Data.MySqlClient.MySqlCommand("select * from vwtuition where (ay like '" & frmDashboard.lblAY.Text & "' or ay like '') and fullname like '%" & txtSearch.Text & "%'", cn)
            cm = New MySql.Data.MySqlClient.MySqlCommand("SELECT *, IF(B.balance > 0) FROM vwtuition AS T INNER JOIN VWBALANCE AS B ON T.LRN = B.LRN AND T.AY = B.AY where (B.ay like '" & frmDashboard.lblAY.Text & "' or B.ay like '') and B.LRN like '%" & txtSearch.Text & "%'", cn)
            'SELECT * FROM vwtuition AS T INNER JOIN VWBALANCE AS B ON T.LRN = B.LRN AND T.AY = B.AY
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("lrn").ToString, dr.Item("fullname").ToString, dr.Item("gradesection").ToString, dr.Item("fee").ToString, dr.Item("less").ToString, dr.Item("TOTAL").ToString, dr.Item("PAYMENT").ToString, dr.Item("BALANCE").ToString, dr.Item("ay").ToString)
            End While
            dr.Close()
            cn.Close()
            txtSearch.Text = ""
            Dashboard()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
End Class