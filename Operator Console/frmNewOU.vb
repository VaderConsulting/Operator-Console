Imports Utility
Imports System.DirectoryServices

Public Class frmNewOU

    Public OUParentPath As String = ""
    Public ParentOUName As String = ""
    Public ParentTreeNode As TreeNode = Nothing

    Private Sub frmNewOU_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "New OU of " & ParentOUName
        txtName.Focus()
        Me.Height = 118
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim OU As New DirectoryServices.DirectoryEntry(OUParentPath, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword, AuthenticationTypes.ServerBind)

        Dim NewOU As DirectoryEntry = OU.Children.Add("OU=" & txtName.Text, "organizationalUnit")
        'OU.CommitChanges()

        NewOU.CommitChanges()

        If txtCode.Text.Length > 0 Then
            NewOU.Properties("description").Add(txtCode.Text)
            NewOU.CommitChanges()
        End If

        OU.RefreshCache()
        NewOU.Close()
        NewOU = Nothing
        OU.Close()
        OU = Nothing

        Me.Close()

    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged, txtCode.TextChanged
        
        For Each Treeviewnode As TreeNode In ParentTreeNode.Nodes
            If Treeviewnode.Text.ToLower = txtName.Text.ToLower Then
                btnOK.Enabled = False
                txtName.ForeColor = Color.Red
            Else
                btnOK.Enabled = True
                txtName.ForeColor = Color.Black
            End If
        Next

        If txtName.Text.Trim.Length = 0 Then
            btnOK.Enabled = False
        Else
            btnOK.Enabled = True
        End If

    End Sub

    Private Sub btnExpandDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpandDown.Click
        Me.Height = 167
        btnExpandDown.Enabled = False
        txtCode.Enabled = True
        txtCode.Focus()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class