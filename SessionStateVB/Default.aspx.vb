Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If IsPostBack Then
            ' Set Session State values
            Session("FirstName") = FirstNameTextBox.Text
            Session("LastName") = LastNameTextBox.Text

            ' Display button
            ReadBtn.Visible = True
        End If
    End Sub

    Protected Sub ReadSessionStateValues(ByVal sender As Object, ByVal e As System.EventArgs) Handles SavedSessionState.Click
        Response.Redirect("DisplaySessionState.aspx")
    End Sub
End Class