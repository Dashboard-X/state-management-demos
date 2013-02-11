Public Class sessionretrive
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserName") IsNot Nothing Then

            Label1.Text = "Welcome : " + Session("UserName")
        Else
        End If
    End Sub
End Class