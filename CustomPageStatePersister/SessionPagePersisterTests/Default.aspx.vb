
Partial Class SessionPagePersisterTests_Default
    Inherits SessionPersisterBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdateSessionDisplay()
    End Sub

    Private Sub UpdateSessionDisplay()
        SessionOutput.Text = String.Empty
        For Each key As String In Session.Keys
            Dim val As Object = Session(key)
            If val Is Nothing Then
                SessionOutput.Text &= String.Format("<b>{0}:</b> - <i>Nothing</i><br />", key)
            Else
                SessionOutput.Text &= String.Format("<b>{0}:</b> - {1}<br />", key, val.ToString)
            End If

            If TypeOf val Is Queue Then
                'Enumerate queue
                Dim q As Queue = CType(val, Queue)
                Dim contents() As Object = q.ToArray()
                SessionOutput.Text &= "<ul>"
                For Each item As Object In contents
                    SessionOutput.Text &= String.Format("<li>{0}</li>", item.ToString())
                Next
                SessionOutput.Text &= "</ul>"
            ElseIf TypeOf val Is Pair Then
                SessionOutput.Text &= DeserializeViewState.Deserialize(val)
            End If
        Next
    End Sub

    Protected Sub btnAddItems_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddItems.Click
        For i As Integer = 0 To 5
            ddl.Items.Add(New ListItem("Item " & i, i))
        Next
    End Sub
End Class
