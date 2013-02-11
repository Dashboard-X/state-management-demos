Imports Microsoft.VisualBasic

Public Class CustomPersisterBasePage
    Inherits System.Web.UI.Page

    Private _persister As PageStatePersister

    Protected Overrides ReadOnly Property PageStatePersister() As System.Web.UI.PageStatePersister
        Get
            If _persister Is Nothing Then
                _persister = New FileSystemPageStatePersister(Me)
            End If

            Return _persister
        End Get
    End Property

End Class
