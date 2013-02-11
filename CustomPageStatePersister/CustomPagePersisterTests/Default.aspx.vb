Imports System.IO

Partial Class CustomPagePersisterTests_Default
    Inherits CustomPersisterBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdateStateFilesDisplay()
    End Sub

    Private Sub UpdateStateFilesDisplay()
        Dim dirInfo As New DirectoryInfo(Server.MapPath("~/StateFiles/"))
        Dim fs() As FileInfo = dirInfo.GetFiles()

        StateFiles.DataSource = fs
        StateFiles.DataBind()
    End Sub

    Protected Sub btnAddItems_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddItems.Click
        For i As Integer = 0 To 5
            ddl.Items.Add(New ListItem("Item " & i, i))
        Next
    End Sub

End Class
