Public Class frmMain

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connection()
        With frmDashboard
            .TopLevel = False
            Panel5.Controls.Add(frmDashboard)
            .BringToFront()
            .Show()
        End With
        Dashboard()
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
        'Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
        'Me.Width = intX
        'Me.Height = intY - 40
        'Me.Left = 0
        'Me.Top = 0
    End Sub

    Private Sub btnStudent_Click(sender As Object, e As EventArgs) Handles btnStudent.Click
        With frmStudentList
            .TopLevel = False
            Panel5.Controls.Add(frmStudentList)
            .Loadrecords()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnBilling_Click(sender As Object, e As EventArgs) Handles btnBilling.Click
        With frmBillingList
            .TopLevel = False
            Panel5.Controls.Add(frmBillingList)
            .LoadRecords()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnAY_Click(sender As Object, e As EventArgs) Handles btnAY.Click
        With frmAY
            .TopLevel = False
            Panel5.Controls.Add(frmAY)
            .LoadAY()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnSection_Click(sender As Object, e As EventArgs) Handles btnSection.Click
        With frmSectionList
            .TopLevel = False
            Panel5.Controls.Add(frmSectionList)
            .LoadSection()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnScholar_Click(sender As Object, e As EventArgs) Handles btnScholar.Click
        With frmSchorlarList
            .TopLevel = False
            Panel5.Controls.Add(frmSchorlarList)
            .LoadScholar()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        With frmPaymentList
            .TopLevel = False
            Panel5.Controls.Add(frmPaymentList)
            .LoadRecords()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        With frmFeeList
            .TopLevel = False
            Panel5.Controls.Add(frmFeeList)
            .LoadFee()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MsgBox("Are You Sure?", vbQuestion + vbYesNo) = vbYes Then
            Me.Close()
            frmLogin.Show()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frmDashboard
            .TopLevel = False
            Panel5.Controls.Add(frmDashboard)
            .BringToFront()
            .Show()
        End With
    End Sub
End Class
