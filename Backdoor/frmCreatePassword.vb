Public Class frmCreatePassword

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        txtEncryptionKey.Text = gFunctions.SimpleCrypt(txtEncryptionKey.Text)
    End Sub

    Private Sub btnEncrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEncrypt.Click
        Me.txtEncrypted.Text = gFunctions.XMLEncrypt(Me.txtCleartext.Text, Me.txtEncryptionKey.Text)
    End Sub
End Class