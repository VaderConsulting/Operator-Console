Public Class frmLookupSelect

    Public ListName As String = ""
    Public LookupKey As String = ""
    Public LookupValue As ListItem = Nothing

    Private Sub frmLookupSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Lookup Selection - " & ListName

        Dim LookupCollection As New Collections.Specialized.OrderedDictionary

        LookupCollection = gFunctions.GetLookupCollection(gFunctions.CurrentDirectoryServer, ListName, True, gFunctions.TargetDirectoryConfigurationRootDN, gFunctions.TargetDirectoryConnectionUsername, gFunctions.TargetDirectoryConnectionPassword)

        For Each Key As String In LookupCollection.Keys
            Dim Sitename As String = LookupCollection(Key).ToString

            Dim Item As ListItem
            Item.Key = Key
            Item.Value = Sitename
            Item.ListType = ListItem.ItemType.KeyAndValue
            lstLookup.Items.Add(Item)
        Next

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub lstLookup_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstLookup.DoubleClick
        LookupValue = lstLookup.SelectedItem
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub lstLookup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstLookup.SelectedIndexChanged
        LookupValue = lstLookup.SelectedItem
    End Sub

    Public Structure ListItem
        Dim Key As Object
        Dim Value As String

        Public ListType As ItemType

        Public Enum ItemType
            Key = 0
            Value = 1
            KeyAndValue = 2
        End Enum

        Public Overrides Function ToString() As String
            Select Case ListType
                Case ItemType.Key
                    Return Key.ToString
                Case ItemType.Value
                    Return Value
                Case ItemType.KeyAndValue
                    Return Key.ToString & " (" & Value & ")"
                Case Else
                    Return Key.ToString & " (" & Value & ")"
            End Select

        End Function

    End Structure

End Class

