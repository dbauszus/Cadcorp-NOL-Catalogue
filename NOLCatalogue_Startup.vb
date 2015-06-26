Imports Cadcorp.SIS.GisLink.Library
Imports Cadcorp.SIS.GisLink.Library.Constants

<GisLinkProgram("NOLCatalogue")> _
Public Class NOLCatalogue_Startup

    Private SISApplication As SisApplication


    Public Sub New(ByVal sis As SisApplication)

        SISApplication = sis
        Dim ToolsMenu As New SisMenuItem("Tools")
        Dim NOLCatalogueMenu As New SisMenuItem("NOL Catalogue", New SisClickHandler(AddressOf NOLCatalogue_sub))

        NOLCatalogueMenu.Help = "Exchange database server connections"
        NOLCatalogueMenu.MinStatus = SIS_HITTABLE
        NOLCatalogueMenu.MinSelection = 0
        NOLCatalogueMenu.MaxSelection = -1
        NOLCatalogueMenu.Filter = ""
        NOLCatalogueMenu.Locus = ""
        NOLCatalogueMenu.Class = ""
        NOLCatalogueMenu.Image = My.Resources.NOLCatalogue

        ToolsMenu.MenuItems.Add(NOLCatalogueMenu)
        SISApplication.MainMenu.MenuItems.Add(ToolsMenu)

    End Sub


    Private Sub NOLCatalogue_sub(ByVal sender As Object, ByVal args As SisClickArgs)
        Dim SIS As MapManager = args.MapEditor
        Dim dialog_NOL As New Dialog_NOL(SIS)

        With dialog_NOL.tvwNols
            .PathSeparator = Chr(9) '"."
            Dim oMainNode As New TreeNode
            oMainNode.Name = "Libraries"
            oMainNode.Text = "Libraries"
            oMainNode.ImageIndex = 0
            .Nodes.Add(oMainNode)
            Dim iNumNols As Integer = SIS.GetNumNol
            For iNol As Integer = 0 To iNumNols - 1
                Dim sName As String = SIS.GetStr(SIS_OT_NOL, iNol, "_name$")
                Dim oNolNode As New TreeNode
                oNolNode.Name = sName
                oNolNode.Text = sName
                oNolNode.ImageIndex = 1
                oNolNode.SelectedImageIndex = 2
                oMainNode.Nodes.Add(oNolNode)

                Dim sBrushes As String = SIS.GetStr(SIS_OT_NOL, iNol, "_listBrush$")
                If sBrushes <> "" Then
                    Dim oItemNode As New TreeNode
                    oItemNode.Name = "Brush"
                    oItemNode.Text = "Brush"
                    oItemNode.ImageIndex = 5
                    oItemNode.SelectedImageIndex = 5
                    oNolNode.Nodes.Add(oItemNode)
                    Dim asBrushes() As String = sBrushes.Split(Chr(9))

                    For Each sBrush As String In asBrushes
                        If sBrush = "" Then Exit For
                        If sBrush.Contains(".") Then
                            AddTreeFolder(oItemNode, sBrush, "Brush")
                        Else
                            Dim oBrush As New TreeNode
                            oBrush.Name = sBrush
                            oBrush.Text = sBrush
                            oBrush.ImageIndex = 5
                            oBrush.SelectedImageIndex = 5
                            oItemNode.Nodes.Add(oBrush)
                        End If
                    Next
                End If

                Dim sPens As String = SIS.GetStr(SIS_OT_NOL, iNol, "_listPen$")
                If sPens <> "" Then
                    Dim oItemNode As New TreeNode
                    oItemNode.Name = "Pen"
                    oItemNode.Text = "Pen"
                    oItemNode.ImageIndex = 6
                    oItemNode.SelectedImageIndex = 6
                    oNolNode.Nodes.Add(oItemNode)
                    Dim asPens() As String = sPens.Split(Chr(9))

                    For Each sPen As String In asPens
                        If sPen = "" Then Exit For
                        If sPen.Contains(".") Then
                            AddTreeFolder(oItemNode, sPen, "Pen")
                        Else
                            Dim oPen As New TreeNode
                            oPen.Name = sPen
                            oPen.Text = sPen
                            oPen.ImageIndex = 6
                            oPen.SelectedImageIndex = 6
                            oItemNode.Nodes.Add(oPen)
                        End If
                    Next
                End If

                Dim sShapes As String = SIS.GetStr(SIS_OT_NOL, iNol, "_listShape$")
                If sShapes <> "" Then
                    Dim oItemNode As New TreeNode
                    oItemNode.Name = "Shape"
                    oItemNode.Text = "Shape"
                    oItemNode.ImageIndex = 7
                    oItemNode.SelectedImageIndex = 7
                    oNolNode.Nodes.Add(oItemNode)
                    Dim asShapes() As String = sShapes.Split(Chr(9))

                    For Each sShape As String In asShapes
                        If sShape = "" Then Exit For
                        If sShape.Contains(".") Then
                            AddTreeFolder(oItemNode, sShape, "Shape")
                        Else
                            Dim oShape As New TreeNode
                            oShape.Name = sShape
                            oShape.Text = sShape
                            oShape.ImageIndex = 7
                            oShape.SelectedImageIndex = 7
                            oItemNode.Nodes.Add(oShape)
                        End If
                    Next
                End If
            Next

            oMainNode.Expand()

        End With

        dialog_NOL.ShowDialog()

        SIS.Dispose()

    End Sub


    Private Sub AddTreeFolder(ByRef MyNode As TreeNode, ByVal NolItem As String, ByVal ItemType As String)
        Dim iDotPos As Integer = NolItem.IndexOf(".")
        Dim sFolder As String = NolItem.Substring(0, iDotPos)
        Dim sRemainder As String = NolItem.Substring(iDotPos + 1)
        Dim oNode As New TreeNode
        Dim iIcon As Integer
        Select Case ItemType
            Case "Brush"
                iIcon = 5
            Case "Pen"
                iIcon = 6
            Case "Shape"
                iIcon = 7
        End Select
        Dim aNodes() As TreeNode = MyNode.Nodes.Find(sFolder, False)
        If aNodes.Length = 0 Then
            oNode.Name = sFolder
            oNode.Text = sFolder
            oNode.ImageIndex = 3
            oNode.SelectedImageIndex = 4
            MyNode.Nodes.Add(oNode)
        Else
            oNode = aNodes(0)
        End If
        If sRemainder.Contains(".") Then
            AddTreeFolder(oNode, sRemainder, ItemType)
        Else
            Dim oNolItem As New TreeNode
            oNolItem.Name = sRemainder
            oNolItem.Text = sRemainder
            oNolItem.ImageIndex = iIcon
            oNolItem.SelectedImageIndex = iIcon
            oNode.Nodes.Add(oNolItem)
        End If
    End Sub

End Class