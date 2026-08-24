Public Class frmMain

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSecurityLevels()
        LoadSecurityValues()
    End Sub

    Private Sub LoadSecurityLevels()
        Dim ServerConfigCollection As Collections.SortedList = New Collections.SortedList

        ' Remote Config Server
        Dim ConfigServer As New ConfigService.ConfigClient

        ServerConfigCollection = ConfigServer.GetConfigCollection("securitylevels")
        If ServerConfigCollection.Count > 0 Then
            For i As Integer = 0 To ServerConfigCollection.Count - 1
                lstLevels.Items.Add(ServerConfigCollection.GetKey(i).ToString())
            Next
        End If

    End Sub

    Private Sub LoadSecurityValues()
        Dim ServerConfigCollection As Collections.SortedList = New Collections.SortedList
        Dim AutoLogonResult As Boolean = False
        Dim UserAccessLevel As String = ""
        Dim dt As New DataTable()
        Dim dr As DataRow
        'Dim AttributeSetting() As String
        'Dim Read As Boolean
        'Dim Modify As Boolean
        'Dim Add As Boolean
        'Dim Delete As Boolean

        dt.Columns.Add(New DataColumn("Attribute", GetType(String)))
        dt.Columns.Add(New DataColumn("Read", GetType(Boolean)))
        dt.Columns.Add(New DataColumn("Modify", GetType(Boolean)))
        dt.Columns.Add(New DataColumn("Add", GetType(Boolean)))
        dt.Columns.Add(New DataColumn("Delete", GetType(Boolean)))

        ' Remote Config Server
        Dim ConfigServer As New ConfigService.ConfigClient

        ServerConfigCollection = ConfigServer.GetConfigCollection("securityattributes")

        If ServerConfigCollection.Count > 0 Then
            For i As Integer = 0 To ServerConfigCollection.Count - 1
                dr = dt.NewRow()
                dr(0) = ServerConfigCollection.GetKey(i).ToString
                'AttributeSetting = Split(ServerConfigCollection.GetByIndex(i), ";")
                'Read = AttributeSetting(0)
                'Modify = AttributeSetting(1)
                'Add = AttributeSetting(2)
                'Delete = AttributeSetting(3)

                'dr(1) = Read And 1

                dt.Rows.Add(dr)
            Next

            dt.TableName = "SecurityAttributes"
            grdSecurity.DataSource = dt
        Else
            Debug.Print("An error occured retrieving the config items.")
        End If

        grdSecurity.Columns(0).Width = 150
        grdSecurity.Columns(1).Width = 50
        grdSecurity.Columns(2).Width = 50
        grdSecurity.Columns(3).Width = 50
        grdSecurity.Columns(4).Width = 50
        grdSecurity.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

    End Sub

    Private Sub lstLevels_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstLevels.SelectedIndexChanged
        lblDefault.Text = "Default values for " & lstLevels.Text
    End Sub
End Class
