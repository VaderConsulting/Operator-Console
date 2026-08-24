Public Class frmPhones

    Public UserPath As String = ""
    Private m_ThisUser As DirectoryServices.DirectoryEntry
    Private m_Phones() As String

    Private Sub frmPhones_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        m_ThisUser.Close()
        m_ThisUser = Nothing
    End Sub

    Private Sub frmPhones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToParent()

        m_ThisUser = New DirectoryServices.DirectoryEntry(UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, DirectoryServices.AuthenticationTypes.ServerBind)

        m_ThisUser.RefreshCache()

        Dim GivenName As String = "[No given name]"
        Dim Surname As String = "[No Surname]"

        Try
            GivenName = m_ThisUser.Properties("givenName").Value.ToString
        Catch ex As Exception

        End Try

        Try
            Surname = m_ThisUser.Properties("sn").Value.ToString
        Catch ex As Exception

        End Try

        Me.Text = "Phones assigned to " & GivenName & " " & Surname

        GetPhoneList()
        
    End Sub

    Private Sub GetPhoneList()
        Dim PhoneCount As Integer = 0

        For Each Phone In m_ThisUser.Properties("directReports")
            Dim PhoneObject As New DirectoryServices.DirectoryEntry("LDAP://" & gFunctions.CurrentDirectoryServer & Phone.ToString, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, DirectoryServices.AuthenticationTypes.ServerBind)
            ReDim Preserve m_Phones(PhoneCount)

            m_Phones(PhoneCount) = PhoneObject.Path

            Try
                lstPhones.Items.Add(PhoneObject.Properties("telephoneNumber")(0).ToString)
            Catch
                lstPhones.Items.Add("[No Number]")
            Finally
                PhoneObject.Close()
                PhoneObject = Nothing
            End Try

            PhoneCount += 1

        Next
    End Sub

    Private Sub frmPhones_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case Chr(e.KeyCode)
            Case Convert.ToChar(113) ' F2
                ' AllowEdit
            Case Convert.ToChar(27) ' Esc
                Me.Close()
        End Select
    End Sub

    Private Sub lstPhones_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstPhones.MouseDoubleClick
        Dim PhoneDetailsForm As New frmPhoneDetails

        PhoneDetailsForm.UserPath = m_Phones(lstPhones.SelectedIndex)
        PhoneDetailsForm.ParentPhonesForm = Me
        PhoneDetailsForm.Phonename = lstPhones.SelectedItem.ToString

        PhoneDetailsForm.Show()
    End Sub

    Private Sub lstPhones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPhones.SelectedIndexChanged
        'Dim PhoneDetailsForm As New frmPhoneDetails

        'PhoneDetailsForm.UserPath = m_Phones(lstPhones.SelectedIndex)

        'PhoneDetailsForm.Show()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub btnAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        Dim PhoneSearchForm As New frmPhoneSearch

        Dim Result As DialogResult = PhoneSearchForm.ShowDialog()

        If Result = Windows.Forms.DialogResult.OK Then
            If PhoneSearchForm.PhonePath.Length > 0 Then
                DoAssignPhone(PhoneSearchForm.PhonePath)
            End If
        End If

    End Sub

    Private Sub DoAssignPhone(ByVal PhonePath As String)
        Dim UserObject As New DirectoryServices.DirectoryEntry(UserPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, DirectoryServices.AuthenticationTypes.ServerBind)
        Dim PhoneObject As New DirectoryServices.DirectoryEntry(PhonePath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, DirectoryServices.AuthenticationTypes.ServerBind)

        gFunctions.SetManager(gFunctions.RemoveServerFromDN(PhoneObject.Path), gFunctions.RemoveServerFromDN(UserObject.Path))

        UserObject.Close()
        PhoneObject.Close()

        MsgBox("Operation Complete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Assign Phone")

        UserObject = Nothing
        PhoneObject = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

End Class