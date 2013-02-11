Imports Microsoft.VisualBasic

Public Class SessionPersisterBasePage
    Inherits System.Web.UI.Page

    Private _persister As PageStatePersister

    Protected Overrides ReadOnly Property PageStatePersister() As System.Web.UI.PageStatePersister
        Get
            If _persister Is Nothing Then
                _persister = New System.Web.UI.SessionPageStatePersister(Me)
            End If

            Return _persister
        End Get
    End Property
End Class
