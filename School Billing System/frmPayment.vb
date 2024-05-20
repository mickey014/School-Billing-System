Imports MySql.Data.MySqlClient
Public Class frmPayment
    Public _lrn As String
    Dim rnd As New Random
    Private Sub frmPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmPayment_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = (Me.Width - Panel1.Width) / 2
    End Sub

    Sub GenerateRefNo()
        Try
            Dim found As Boolean
            Dim refno As String = rnd.Next(1111111, 9999999)
            cn.Open()
            cm = New MySqlCommand("select * from tblpayment where refno like '" & refno & "'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                found = True
            Else
                found = False
            End If
            dr.Close()
            cn.Close()

            If found = True Then
                GenerateRefNo()
            Else
                txtReference.Text = refno
            End If
        Catch ex As Exception
            cn.Close()
            GenerateRefNo()
        End Try
    End Sub

    Sub LoadRecords()
        Try
            txtBalance.Text = "0.00"
            cn.Open()
            cm = New MySqlCommand("select balance from vwbalance where lrn like '" & _lrn & "' and ay like '" & txtAY.Text & "'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtBalance.Text = Format(CDbl(dr.Item("balance").ToString), "#,##0.00")
            End If
            dr.Close()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 46
            Case 8
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        With frmStudentSearch
            .Loadrecords()
            .ShowDialog()
        End With
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If CDbl(txtAmount.Text) > CDbl(txtBalance.Text) Then
                MsgBox("Please pay correct amount!", vbExclamation)
                Return
            End If
            If IS_EMPTY(txtReference) = True Then Return
            If IS_EMPTY(txtStudent) = True Then Return
            If IS_EMPTY(cboPeriod) = True Then Return
            If IS_EMPTY(txtAmount) = True Then Return
            If MsgBox("Do you want to save this payment?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("insert into tblpayment (refno, lrn, ay, payment, pdate, period)values(@refno, @lrn, @ay, @payment,@pdate,@period)", cn)
                cm.Parameters.AddWithValue("@refno", txtReference.Text)
                cm.Parameters.AddWithValue("@lrn", _lrn)
                cm.Parameters.AddWithValue("@ay", txtAY.Text)
                cm.Parameters.AddWithValue("@payment", CDbl(txtAmount.Text))
                cm.Parameters.AddWithValue("@pdate", Now.ToString("yyyy-MM-dd"))
                cm.Parameters.AddWithValue("@period", cboPeriod.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Payment has been succssfully saved!", vbInformation)
                Clear()
                frmPaymentList.LoadRecords()
                Dashboard()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
    Sub Clear()
        GenerateRefNo()
        cboPeriod.Text = ""
        txtAmount.Clear()
        LoadRecords()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboPeriod.KeyPress
        e.Handled = True
    End Sub
End Class