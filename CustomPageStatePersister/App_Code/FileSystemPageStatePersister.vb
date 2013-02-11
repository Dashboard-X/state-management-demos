Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Web.UI

Public Class FileSystemPageStatePersister
    Inherits PageStatePersister

    Private Const ViewStateFormFieldID As String = "__SKM_VIEWSTATEID"
    Private Const StateFileFolderPath As String = "~/StateFiles/"

    Sub New(ByVal p As Page)
        MyBase.New(p)
    End Sub

    Public Overrides Sub Load()
        'Determine if we have a __SKM_VIEWSTATEID
        Dim stateIdentifierValue As String = HttpContext.Current.Request.Form(ViewStateFormFieldID)
        If stateIdentifierValue.Length > 0 Then
            Dim fileName As String = stateIdentifierValue
            Dim filePath = HttpContext.Current.Server.MapPath(StateFileFolderPath & fileName)
            Dim contents As String = File.ReadAllText(filePath)

            Dim state As Pair = TryCast(MyBase.StateFormatter.Deserialize(contents), Pair)
            If state IsNot Nothing Then
                MyBase.ViewState = state.First
                MyBase.ControlState = state.Second
            End If
        End If
    End Sub

    Public Overrides Sub Save()
        If MyBase.Page.Form IsNot Nothing AndAlso (MyBase.ControlState IsNot Nothing OrElse MyBase.ViewState IsNot Nothing) Then
            'Save state to file
            Dim fileName As String = String.Concat(Convert.ToString(DateTime.Now.Ticks, 16), "-", HttpContext.Current.Session.SessionID, ".vs")
            Dim filePath As String = HttpContext.Current.Server.MapPath(StateFileFolderPath & fileName)
            Dim p As New Pair(MyBase.ViewState, MyBase.ControlState)
            File.WriteAllText(filePath, MyBase.StateFormatter.Serialize(p))

            Dim stateField As String = String.Format("{0}<input type=""hidden"" name=""{1}"" value=""{2}"" />{0}{0}", System.Environment.NewLine, ViewStateFormFieldID, fileName)
            MyBase.Page.Form.Controls.AddAt(0, New LiteralControl(stateField))
        End If
    End Sub
End Class
