Public Class frmCheckPassword

    Private Sub btnSimpleDecrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpleDecrypt.Click
        Me.txtClearText1.Text = gFunctions.SimpleCrypt(Me.txtEncrypted1.Text)
    End Sub

    Private Sub btnDecrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecrypt.Click
        Me.txtClearText2.Text = gFunctions.XMLDecrypt(Me.txtEncrypted2.Text, Me.txtClearText1.Text)
    End Sub

    Private Sub txtEncrypted2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEncrypted2.TextChanged
        If txtEncrypted2.Text.Length > 0 Then
            btnDecrypt.Enabled = True
        Else
            btnDecrypt.Enabled = False
        End If
    End Sub
End Class