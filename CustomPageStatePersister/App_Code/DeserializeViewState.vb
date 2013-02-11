Imports Microsoft.VisualBasic
Imports System.Web.UI

Public Class DeserializeViewState
    Public Shared Function Deserialize(ByVal vs As Object) As String
        If vs Is Nothing Then
            Return "<i>Nothing</i>"
        ElseIf TypeOf vs Is Pair Then
            Dim p As Pair = CType(vs, Pair)
            Return String.Format("{0}<ul><li>{1}</li><li>{2}</li></ul>", p.GetType().ToString(), Deserialize(p.First), Deserialize(p.Second))
        ElseIf TypeOf vs Is Triplet Then
            Dim t As Triplet = CType(vs, Triplet)
            Return String.Format("{0}<ul><li>{1}</li><li>{2}</li><li>{3}</ul>", t.GetType().ToString(), Deserialize(t.First), Deserialize(t.Second), Deserialize(t.Third))
        ElseIf TypeOf vs Is IEnumerable AndAlso Not TypeOf vs Is String Then
            Dim output As String = vs.GetType().ToString() & "<ul>"
            For Each o As Object In vs
                output &= String.Format("<li>{0}</li>", Deserialize(o))
            Next
            output &= "</ul>"

            Return output
        ElseIf TypeOf vs Is IndexedString Then
            Return String.Format("{0} ({1})", CType(vs, IndexedString).Value, vs.GetType().ToString())
        ElseIf TypeOf vs Is String Then
            Return String.Format("""{0}"" ({1})", vs.ToString(), vs.GetType().ToString())
        Else
            Return String.Format("{0} ({1})", vs.ToString(), vs.GetType().ToString())
        End If
    End Function
End Class
