Imports Cadcorp.SIS.GisLink.Library
Imports Cadcorp.SIS.GisLink.Library.Constants

Public Class SisControl

    Private Sub SisControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
        AxSis.SetStr(SIS_OT_SYSTEM, 0, "_LicenceNumber$", "66-80-0-255-7f8b7aca-666")
        AxSis.SetStr(SIS_OT_SYSTEM, 0, "_LicenceCompany$", "NolCatalog")
        AxSis.SetStr(SIS_OT_SYSTEM, 0, "_LicenceUser$", "NolCatalog")
        AxSis.Level = SIS_LEVEL_MODELLER
        AxSis.SetDefaultPrj("Paper")
    End Sub

End Class