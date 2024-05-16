Imports System.Text
Imports MySql.Data.MySqlClient
Module Module1
    Public cn As New MySqlConnection
    Public cm As New MySqlCommand
    Public dr As MySqlDataReader

    Sub Connection()
        Try
            cn = New MySqlConnection
            cn.ConnectionString = "server=localhost;user id=root;password=;database=school_billing"
            Dashboard()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Public Function IS_EMPTY(ByVal sText As Object) As Boolean
        On Error Resume Next
        If sText.Text = "" Then
            IS_EMPTY = True
            MsgBox("Warning: Required missing field", vbExclamation)
            sText.BackColor = Color.LemonChiffon
            sText.Focus()
        Else
            IS_EMPTY = False
            sText.BackColor = Color.White
        End If
    End Function

    Sub Dashboard()
        Try



            cn.Open()
            cm = New MySqlCommand("select ay from tblay where status like 'OPEN'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                frmDashboard.lblAY.Text = dr.Item("ay").ToString
            Else
                frmDashboard.lblAY.Text = ""
            End If
            dr.Close()
            cn.Close()

            cn.Open()
            cm = New MySqlCommand("select ifnull(sum(payment),0) from tblpayment where ay like '" & frmDashboard.lblAY.Text & "'", cn)
            frmDashboard.lblPayment.Text = cm.ExecuteScalar
            cn.Close()

            cn.Open()
            cm = New MySqlCommand("select count(*) from tblstudent", cn)
            frmDashboard.lblStudent.Text = cm.ExecuteScalar
            cn.Close()

            cn.Open()
            cm = New MySqlCommand("select ifnull(sum(total),0) from vwtuition where ay like '" & frmDashboard.lblAY.Text & "'", cn)
            frmDashboard.lblBill.Text = cm.ExecuteScalar
            cn.Close()

        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Public Function encrypt_data(encryption As String) As String
        Dim msg As String = String.Empty
        Dim encode As Byte() = New Byte(encryption.Length - 1) {}
        encode = Encoding.UTF8.GetBytes(encryption)
        msg = Convert.ToBase64String(encode)
        Return msg
    End Function

    Public Function decrypt_data(decryption As String) As String
        Dim decrypt_text As String = String.Empty
        Dim encoded_txt As New UTF8Encoding()
        Dim decode As Decoder = encoded_txt.GetDecoder()
        Dim code_byte As Byte() = Convert.FromBase64String(decryption)
        Dim charcount As Integer = decode.GetCharCount(code_byte, 0, code_byte.Length)
        Dim decode_char As Char() = New Char(charcount - 1) {}
        decode.GetChars(code_byte, 0, code_byte.Length, decode_char, 0)
        decrypt_text = New String(decode_char)

        Return decrypt_text

    End Function
End Module
