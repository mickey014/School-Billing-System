Imports MySql.Data.MySqlClient
Imports DGV2Printer
Public Class frmPaymentList
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frmPayment
            .TopLevel = False
            frmMain.Panel5.Controls.Add(frmPayment)
            .txtAY.Text = frmDashboard.lblAY.Text
            .GenerateRefNo()
            .BringToFront()
            .Show()
        End With
    End Sub

    Sub LoadRecords()
        Try
            DataGridView1.Rows.Clear()

            cn.Open()
            cm = New MySql.Data.MySqlClient.MySqlCommand("select p.*, concat(s.lname, ', ' , s.fname, ' ' , s.mname) as fullname from tblpayment as p inner join tblstudent as s on s.lrn = p.lrn where  ay like '" & frmDashboard.lblAY.Text & "' and concat(s.lname, ', ' , s.fname, ' ' , s.mname) like '" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("id").ToString, dr.Item("refno").ToString, dr.Item("lrn").ToString, dr.Item("fullname").ToString, dr.Item("ay").ToString, dr.Item("period").ToString, dr.Item("particular").ToString, Format(CDbl(dr.Item("payment").ToString), "#,##0.00"), CDate(dr.Item("pdate").ToString).ToShortDateString)
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub



    Private Sub printBtn_Click(sender As Object, e As EventArgs) Handles printBtn.Click

        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.PrintPreviewControl.Zoom = 1
        PrintPreviewDialog1.ShowDialog()

    End Sub


    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        '1203, 661
        Dim obj = New frmMain
        Dim imagebmp As New Bitmap(obj.Panel5.Width, obj.Panel5.Height)
        DataGridView1.DrawToBitmap(imagebmp, New Rectangle(0, 0, Me.DataGridView1.Width, Me.DataGridView1.Height))
        e.Graphics.DrawImage(imagebmp, 0, 0)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        DataGridView1.Rows.Clear()

        Try
            cn.Open()
            cm = New MySql.Data.MySqlClient.MySqlCommand("select p.*, concat(s.lname, ', ' , s.fname, ' ' , s.mname) as fullname from tblpayment as p inner join tblstudent as s on s.lrn = p.lrn where  ay like '" & frmDashboard.lblAY.Text & "' and p.lrn like '" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("id").ToString, dr.Item("refno").ToString, dr.Item("lrn").ToString, dr.Item("fullname").ToString, dr.Item("ay").ToString, dr.Item("period").ToString, dr.Item("particular").ToString, Format(CDbl(dr.Item("payment").ToString), "#,##0.00"), CDate(dr.Item("pdate").ToString).ToShortDateString)
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
            txtSearch.Text = ""
        Catch ex As Exception
            cn.Close()
        MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub refreshBtn_Click(sender As Object, e As EventArgs) Handles refreshBtn.Click
        LoadRecords()
    End Sub
End Class