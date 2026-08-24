Imports System.DirectoryServices
Imports Utility
Imports Utility.Functions
Imports Utility.Constants
Imports Utility.Types
Imports Utility.Types.ERRORTYPE

Public Class frmPhoneSearch

    Private m_Phones() As String
    Public PhonePath As String

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        DoPhoneSearch()
    End Sub

    Public Sub DoPhoneSearch()
        Dim SearchLimit As String
        Dim SearchLimitRawData As String

        lstPhones.Items.Clear()

        Cursor.Current = Cursors.WaitCursor

        SearchLimitRawData = gFunctions.CollectionValue("SearchLimit", gSettings).Substring(0, 3)
        If SearchLimitRawData.Contains("|") Then
            SearchLimit = SearchLimitRawData.Substring(0, SearchLimitRawData.IndexOf("|"))
        Else
            SearchLimit = SearchLimitRawData
        End If

        Try
            ' TODO:  This binds to a server, which is ok for ADAM, but possibly not for AD
            Dim TheServerName As String = gFunctions.CurrentDirectoryServer

            If Not TheServerName.StartsWith("LDAP://") Then
                TheServerName = "LDAP://" & TheServerName
            End If

            If Not TheServerName.EndsWith("/") Then
                TheServerName &= "/"
            End If
            Dim root As New DirectoryServices.DirectoryEntry(TheServerName & gFunctions.CollectionValue("DirectoryPhoneRoot", gSettings), gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)
            Dim rootSearch As New DirectorySearcher(root)
            Dim SearchResult As SearchResult
            Dim results As SearchResultCollection

            rootSearch.PropertiesToLoad.Add("telephoneNumber")
            rootSearch.PropertiesToLoad.Add("manager")
            rootSearch.PropertiesToLoad.Add("path")

            If chkUnassigned.Checked Then
                rootSearch.Filter = "(&(ObjectClass=user)(telephoneNumber=" & Me.txtSearch.Text & "*)(!manager=*))"
            Else
                rootSearch.Filter = "(&(ObjectClass=user)(telephoneNumber=" & Me.txtSearch.Text & "*))"
            End If
            rootSearch.SizeLimit = SearchLimit
            rootSearch.CacheResults = True

            results = rootSearch.FindAll

            Dim PhoneCount As Integer

            For Each SearchResult In results
                Try
                    ReDim Preserve m_Phones(PhoneCount)

                    lstPhones.Items.Add(SearchResult.Properties("telephoneNumber")(0).ToString)
                    m_Phones(PhoneCount) = SearchResult.Path
                Catch ex As Exception
                    'lblStatus.Text = ex.Message.ToString
                End Try

                PhoneCount += 1
            Next

            Me.Refresh()

            gFunctions.LastStatus.Clear()
            gFunctions.LastStatus.CADS_ERRORCODE = SUCCESS
        Catch ex As Exception
            Select Case ex.Message.Trim
                Case "The server is not operational."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.SERVER_NOT_FOUND)
                Case "Logon failure: unknown user name or bad password."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.SECURITY, ERROR_SECURITY.AUTHENTICATION_FAILURE)
                Case "A local error has occurred."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
                Case "An operations error occurred."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
                Case "The time limit for this request was exceeded."
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.REQUEST_TIMEOUT)
                Case Else
                    Debug.Print(ex.Message)
                    gFunctions.LastStatus.CADS_ERRORCODE = gFunctions.ErrorResult(ERRORTYPE.DIRECTORY, ERROR_DIRECTORY.DIRECTORY_ERROR)
            End Select
        End Try

        Cursor.Current = Cursors.Default

    End Sub

    Private Sub lstPhones_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstPhones.MouseDoubleClick
        Dim PhoneDetailsForm As New frmPhoneDetails

        PhoneDetailsForm.UserPath = m_Phones(lstPhones.SelectedIndex)
        'PhoneDetailsForm.ParentPhonesForm = Me
        PhoneDetailsForm.Phonename = lstPhones.SelectedItem.ToString

        PhoneDetailsForm.Show()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If lstPhones.SelectedIndex >= 0 Then
            PhonePath = m_Phones(lstPhones.SelectedIndex)
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = Windows.Forms.DialogResult.None
            Close()
        End If
    End Sub

    Private Sub frmPhoneSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToParent()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class