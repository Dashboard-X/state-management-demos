
Partial Class DisplaySessionState
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Display Session values
        NewFirstNameTextBox.Text = Session("FirstName").ToString()
        NewLastNameTextBox.Text = Session("LastName").ToString()

        ' Display Session Details
        CookieMode.Text = Session.CookieMode.ToString()
        Count.Text = Session.Count.ToString()
        IsCookieless.Text = Session.IsCookieless.ToString()
        IsNewSession.Text = Session.IsNewSession.ToString()
        IsReadOnly.Text = Session.IsReadOnly.ToString()
        isSynchronized.Text = Session.IsSynchronized.ToString()
        Keys.Text = Session.Keys(0).ToString()
        LCID.Text = Session.LCID.ToString()
        Mode.Text = Session.Mode.ToString()
        Timeout.Text = Session.Timeout.ToString()
    End Sub
End Class
