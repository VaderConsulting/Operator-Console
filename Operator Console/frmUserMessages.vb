Public Class frmUserMessages

    Private Sub txtMessage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMessage.TextChanged
        Me.lblRemaining.Text = Me.txtMessage.MaxLength - Me.txtMessage.TextLength & " Characters remaining"

        Select Case Me.txtMessage.MaxLength - Me.txtMessage.TextLength
            Case Is > 640
                Me.lblRemaining.ForeColor = Color.Green
            Case Is < 80
                Me.lblRemaining.ForeColor = Color.Red
            Case Is < 160
                Me.lblRemaining.ForeColor = Color.Orange
            Case Is < 321
                Me.lblRemaining.ForeColor = Color.YellowGreen
        End Select
    End Sub

    Private Sub btnNewMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewMessage.Click
        lstMessageID.Items.Add(Date.Now & " (" & gFunctions.InteractiveUserUsername & ")")
        Me.txtMessage.ReadOnly = False
        Me.txtMessage.Focus()
    End Sub

    Private Sub frmUserMessages_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Me.lblRemaining.ForeColor = Color.Green
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class