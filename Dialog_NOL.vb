Imports System.Windows.Forms
Imports System.IO
Imports System.Math
Imports Cadcorp.SIS.GisLink.Library
Imports Cadcorp.SIS.GisLink.Library.Constants


Public Class Dialog_NOL

    Private _sis As Cadcorp.SIS.GisLink.Library.MapEditor
    Private mNodeIndex As Integer
    Private mIsBrush As Boolean
    Private mIsPen As Boolean
    Private mIsShape As Boolean
    Private gCancelled As Boolean
    Private iPage As Integer
    Private sLibName As String
    Private sPath As String


    Private Sub tvwNols_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwNols.AfterSelect
        Dim aBrushNodes() As TreeNode = tvwNols.SelectedNode.Nodes.Find("Brush", False)
        Dim aPenNodes() As TreeNode = tvwNols.SelectedNode.Nodes.Find("Pen", False)
        Dim aShapeNodes() As TreeNode = tvwNols.SelectedNode.Nodes.Find("Shape", False)
        mIsBrush = False
        mIsPen = False
        mIsShape = False
        chkBrushes.CheckState = CheckState.Unchecked
        chkPens.CheckState = CheckState.Unchecked
        chkShapes.CheckState = CheckState.Unchecked

        If tvwNols.SelectedNode.Level = 0 Then 'top level
            chkBrushes.Enabled = False
            chkPens.Enabled = False
            chkShapes.Enabled = False

        ElseIf tvwNols.SelectedNode.Level = 1 Then 'nol level
            'look down the tree to see whether brush, pens, or shapes are contained
            If aBrushNodes.Length > 0 Then
                chkBrushes.Enabled = True
            Else
                chkBrushes.Enabled = False
            End If
            If aPenNodes.Length > 0 Then
                chkPens.Enabled = True
            Else
                chkPens.Enabled = False
            End If
            If aShapeNodes.Length > 0 Then
                chkShapes.Enabled = True
            Else
                chkShapes.Enabled = False
            End If

        Else 'look up and down the tree
            GetBrushPath(tvwNols.SelectedNode)
            GetPenPath(tvwNols.SelectedNode)
            GetShapePath(tvwNols.SelectedNode)
            If mIsBrush Or aBrushNodes.Length > 0 Then
                chkBrushes.Enabled = True
            Else
                chkBrushes.Enabled = False
            End If
            If mIsPen Or aPenNodes.Length > 0 Then
                chkPens.Enabled = True
            Else
                chkPens.Enabled = False
            End If
            If mIsShape Or aShapeNodes.Length > 0 Then
                chkShapes.Enabled = True
            Else
                chkShapes.Enabled = False
            End If
        End If

        If chkBrushes.Checked Or chkPens.Checked Or chkShapes.Checked Then
            btnCreate.Enabled = True
        Else
            btnCreate.Enabled = False
        End If

        Try
            GetNolNumber(tvwNols.SelectedNode)
            lblSelected.Text = "Selected Library: " & _sis.GetStr(SIS_OT_NOL, mNodeIndex, "_name$")
        Catch
        End Try

    End Sub


    Private Sub GetNolNumber(ByRef Node As TreeNode)
        If Node.Level = 1 Then
            mNodeIndex = Node.Index
        Else
            GetNolNumber(Node.Parent)
        End If
    End Sub


    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        'debugger break
        'System.Diagnostics.Debugger.Launch()

        GetNolNumber(tvwNols.SelectedNode)
        sLibName = _sis.GetStr(SIS_OT_NOL, mNodeIndex, "_name$")
        Try
            sLibName = System.IO.Path.GetFileNameWithoutExtension(sLibName)
        Catch ex As Exception
        End Try

        'set pdf file name and path
        Dim dlgFile As New SaveFileDialog
        dlgFile.FileName = sLibName & ".pdf"
        dlgFile.AddExtension = True
        dlgFile.DefaultExt = ".pdf"
        dlgFile.Filter = "PDF Files (*.pdf)|*.pdf"
        dlgFile.FilterIndex = 0
        dlgFile.OverwritePrompt = True
        dlgFile.Title = "Create PDF"
        If Not dlgFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If
        Dim sFilename As String = dlgFile.FileName
        Dim fiFileInfo As New FileInfo(sFilename)
        sPath = fiFileInfo.DirectoryName
        My.Computer.FileSystem.CreateDirectory(sPath & "\tmp_nolcatalogue")

        'Create a new A4 page
        DisableButtons()
        iPage = 1
        _sis.SwdNew()
        _sis.SetViewPrj("Paper")
        _sis.SetInt(SIS_OT_WINDOW, 0, "_bRedraw&", False)

        If chkBrushes.Checked Then
            BuildPdfBrushes()
        End If

        If chkPens.Checked Then
            BuildPdfPens()
        End If

        If chkShapes.Checked Then
            BuildPdfSymbols()
        End If

        If gCancelled = False Then
            Dim MergeProcess As PdfMerge = New PdfMerge
            For i As Integer = 1 To iPage - 1
                MergeProcess.AddDocument(sPath & "\tmp_nolcatalogue\" & Str(i) & ".pdf")
            Next
            MergeProcess.Merge(sFilename)
        End If
        gCancelled = False
        My.Computer.FileSystem.DeleteDirectory(sPath & "\tmp_nolcatalogue", FileIO.DeleteDirectoryOption.DeleteAllContents)
        _sis.SwdClose(SIS_NOSAVE)
        btnOK.Enabled = True
        btnCreate.Enabled = True
    End Sub


    Private Sub DisableButtons()
        btnOK.Enabled = False
        btnCreate.Enabled = False
    End Sub


    Private Sub BuildPdfBrushes()
        Dim sBrushes As String = _sis.GetStr(SIS_OT_NOL, mNodeIndex, "_listBrush$")
        Dim asBrushes() As String = sBrushes.Split(Chr(9))
        Dim tx, ty As Double
        Dim iCount As Integer = 0
        Dim iAlternate As Integer = 0

        NewPage()
        ty = 0.29
        tx = 0.02
        prgProgress.Maximum = asBrushes.Length - 1
        prgProgress.Visible = True
        btnCancel.Visible = True

        For iBrush As Integer = 0 To asBrushes.Length - 2
            Application.DoEvents()
            If gCancelled Then
                prgProgress.Visible = False
                btnCancel.Visible = False
                Exit Sub
            End If

            iAlternate = iAlternate + 1
            If iAlternate = 1 Then
                tx = 0.02
                ty = ty - 0.025
            ElseIf iAlternate = 2 Then
                tx = 0.08
            ElseIf iAlternate = 3 Then
                tx = 0.14
                iAlternate = 0
            End If

            'draw brush
            _sis.CreateRectangle(tx, ty, tx + 0.05, ty - 0.015)
            _sis.SetStr(SIS_OT_CURITEM, 0, "_brush$", asBrushes(iBrush))
            _sis.SetStr(SIS_OT_CURITEM, 0, "_pen$", "P_SOLID_0:0:0_0_0")
            _sis.SetInt(SIS_OT_CURITEM, 0, "_level&", 1)

            If asBrushes(iBrush).Length > 40 Then
                _sis.CreateBoxText(tx, ty + 0.0005, 0, 0.0025, asBrushes(iBrush).Substring(0, 40) & "...")
            Else
                _sis.CreateBoxText(tx, ty + 0.0005, 0, 0.0025, asBrushes(iBrush))
            End If
            _sis.SetInt(SIS_OT_CURITEM, 0, "_text_align&", SIS_BOTTOM_LEFT)
            _sis.SetStr(SIS_OT_CURITEM, 0, "_pen$", "<Pen><Colour><RGBA>0 0 0 0</RGBA></Colour></Pen>")
            _sis.SetStr(SIS_OT_CURITEM, 0, "_brush$", "<Brush><Style>Solid</Style><Colour><RGBA>255 255 255 59</RGBA></Colour><BackgroundColour><RGBA>255 255 0 255</RGBA></BackgroundColour></Brush>")
            _sis.SetInt(SIS_OT_CURITEM, 0, "_point_height&", 10)
            _sis.SetInt(SIS_OT_CURITEM, 0, "_text_opaque&", True)
            _sis.SetInt(SIS_OT_CURITEM, 0, "_level&", 2)
            _sis.UpdateItem()

            iCount = iCount + 1
            If iCount = 30 Or iBrush = asBrushes.Length - 2 Then
                _sis.ZoomExtent()
                _sis.ExportPdf(sPath & "\tmp_nolcatalogue\" & Str(iPage) & ".pdf", "A4", "")
                _sis.SelectAll()
                _sis.DoCommand("AComDelete")
                iPage += 1
                NewPage()
                ty = 0.29
                tx = 0.02
                iCount = 0
            End If

            prgProgress.Value = iBrush
        Next

        prgProgress.Visible = False
        btnCancel.Visible = False
    End Sub


    Private Sub BuildPdfPens()
        Dim sPens As String = _sis.GetStr(SIS_OT_NOL, mNodeIndex, "_listPen$")
        Dim asPens() As String = sPens.Split(Chr(9))
        Dim tx, ty As Double
        Dim iCount As Integer = 0
        Dim iAlternate As Integer = 0

        NewPage()
        ty = 0.28
        tx = 0.02
        prgProgress.Maximum = asPens.Length - 1
        prgProgress.Visible = True
        btnCancel.Visible = True

        For iPen As Integer = 0 To asPens.Length - 2
            Application.DoEvents()
            If gCancelled Then
                prgProgress.Visible = False
                btnCancel.Visible = False
                Exit Sub
            End If

            iAlternate = iAlternate + 1
            If iAlternate = 1 Then
                tx = 0.02
                ty = ty - 0.02
            ElseIf iAlternate = 2 Then
                tx = 0.08
            ElseIf iAlternate = 3 Then
                tx = 0.14
                iAlternate = 0
            End If

            _sis.MoveTo(tx, ty, 0)
            _sis.LineTo(tx + 0.05, ty, 0)
            _sis.StoreAsLine()
            _sis.SetStr(SIS_OT_CURITEM, 0, "_pen$", asPens(iPen))

            If asPens(iPen).Length > 40 Then
                _sis.CreateBoxText(tx, ty + 0.005, 0, 0.0025, asPens(iPen).Substring(0, 40) & "...")
            Else
                _sis.CreateBoxText(tx, ty + 0.005, 0, 0.0025, asPens(iPen))
            End If
            _sis.SetInt(SIS_OT_CURITEM, 0, "_text_align&", SIS_BOTTOM_LEFT)
            _sis.SetStr(SIS_OT_CURITEM, 0, "_pen$", "<Pen><Colour><RGBA>0 0 0 0</RGBA></Colour></Pen>")
            _sis.SetStr(SIS_OT_CURITEM, 0, "_brush$", "<Brush><Style>Solid</Style><Colour><RGBA>255 255 255 59</RGBA></Colour><BackgroundColour><RGBA>255 255 0 255</RGBA></BackgroundColour></Brush>")
            _sis.SetInt(SIS_OT_CURITEM, 0, "_point_height&", 10)
            _sis.SetInt(SIS_OT_CURITEM, 0, "_text_opaque&", True)
            _sis.SetInt(SIS_OT_CURITEM, 0, "_level&", 2)
            _sis.UpdateItem()

            iCount = iCount + 1
            If iCount = 39 Or iPen = asPens.Length - 2 Then
                _sis.ZoomExtent()
                _sis.ExportPdf(sPath & "\tmp_nolcatalogue\" & Str(iPage) & ".pdf", "A4", "")
                _sis.SelectAll()
                _sis.DoCommand("AComDelete")
                iPage += 1
                NewPage()
                ty = 0.28
                tx = 0.02
                iCount = 0
            End If

            prgProgress.Value = iPen
        Next

        prgProgress.Visible = False
        btnCancel.Visible = False
    End Sub


    Private Sub BuildPdfSymbols()
        Dim sShapes As String = _sis.GetStr(SIS_OT_NOL, mNodeIndex, "_listShape$")
        Dim asShapes() As String = sShapes.Split(Chr(9))

        Dim iCount As Integer = 0
        Dim iAlternate As Integer = 0

        Dim x1, x2, y1, y2, z1, z2, tx, ty, movex, movey, yshift, scale As Double
        Dim listextent As String

        NewPage()
        prgProgress.Maximum = asShapes.Length - 1
        prgProgress.Visible = True
        btnCancel.Visible = True
        ty = 0.285
        tx = 0.02
        For iShape As Integer = 0 To asShapes.Length - 2
            Application.DoEvents()
            If gCancelled Then
                prgProgress.Visible = False
                btnCancel.Visible = False
                Exit Sub
            End If

            iAlternate = iAlternate + 1
            If iAlternate = 1 Then
                tx = 0.02
                ty = ty - 0.013
            ElseIf iAlternate = 2 Then
                tx = 0.08
            ElseIf iAlternate = 3 Then
                tx = 0.14
                iAlternate = 0
            End If

            'draw shape
            _sis.DeselectAll()
            _sis.CreatePoint(0, 0, 0, asShapes(iShape), 0, 1)
            _sis.SelectItem()
            _sis.DoCommand("AComExplodeShape")
            If _sis.GetNumSel() > 0 Then
                'get shape size
                _sis.CreateListFromSelection("list")
                listextent = _sis.GetListExtent("list")
                _sis.SplitExtent(x1, y1, z1, x2, y2, z2, listextent)
                'resize shape
                scale = 0.005 / (y2 - y1)
                If scale > (0.05 / (x2 - x1)) Then
                    scale = 0.05 / (x2 - x1)
                    yshift = (0.005 - (y2 - y1) * scale) / 2
                End If
                _sis.MoveList("list", 0, 0, 0, 0, scale)
                listextent = _sis.GetListExtent("list")
                _sis.SplitExtent(x1, y1, z1, x2, y2, z2, listextent)

                'move shape
                movex = tx - x1
                movey = ty - y2 - yshift
                _sis.MoveList("list", movex, movey, 0, 0, 1)
                _sis.SetListInt("list", "_level&", 2)
            End If

            'draw text
            If asShapes(iShape).Length > 40 Then
                _sis.CreateBoxText(tx, ty + 0.0005, 0, 0.0025, asShapes(iShape).Substring(0, 40) & "...")
            Else
                _sis.CreateBoxText(tx, ty + 0.0005, 0, 0.0025, asShapes(iShape))
            End If
            _sis.SetInt(SIS_OT_CURITEM, 0, "_text_align&", SIS_BOTTOM_LEFT)
            _sis.SetStr(SIS_OT_CURITEM, 0, "_pen$", "<Pen><Colour><RGBA>0 0 0 0</RGBA></Colour></Pen>")
            _sis.SetStr(SIS_OT_CURITEM, 0, "_brush$", "<Brush><Style>Solid</Style><Colour><RGBA>255 255 255 59</RGBA></Colour><BackgroundColour><RGBA>255 255 0 255</RGBA></BackgroundColour></Brush>")
            _sis.SetInt(SIS_OT_CURITEM, 0, "_point_height&", 10)
            _sis.SetInt(SIS_OT_CURITEM, 0, "_text_opaque&", True)
            _sis.SetInt(SIS_OT_CURITEM, 0, "_text_underlined&", True)
            _sis.SetInt(SIS_OT_CURITEM, 0, "_level&", 2)
            _sis.UpdateItem()

            iCount = iCount + 1
            If iCount = 60 Or iShape = asShapes.Length - 2 Then
                _sis.ZoomExtent()
                _sis.ExportPdf(sPath & "\tmp_nolcatalogue\" & Str(iPage) & ".pdf", "A4", "")
                _sis.SelectAll()
                _sis.DoCommand("AComDelete")
                iPage += 1
                NewPage()
                ty = 0.285
                tx = 0.02
                iCount = 0
            End If

            prgProgress.Value = iShape
        Next

        prgProgress.Visible = False
        btnCancel.Visible = False
    End Sub


    Private Sub NewPage()
        'create a blank A4 frame
        _sis.CreateRectangle(0, 0, 0.21, 0.297)
        _sis.SetStr(SIS_OT_CURITEM, 0, "_pen$", "<Pen><Style>Null</Style><Colour><RGBA>0 0 0 0</RGBA></Colour></Pen>")
        _sis.SetStr(SIS_OT_CURITEM, 0, "_brush$", "<Brush><Style>Hollow</Style><Colour><RGBA>255 255 255 0</RGBA></Colour><BackgroundColour><RGBA>255 255 0 255</RGBA></BackgroundColour></Brush>")
        'create a black border 10mm less than A4
        _sis.CreateRectangle(0.01, 0.01, 0.2, 0.287)
        _sis.SetStr(SIS_OT_CURITEM, 0, "_pen$", "<Pen><Colour><RGBA>0 0 0 0</RGBA></Colour></Pen>")
        _sis.SetStr(SIS_OT_CURITEM, 0, "_brush$", "<Brush><Style>Hollow</Style><Colour><RGBA>255 255 255 0</RGBA></Colour><BackgroundColour><RGBA>255 255 0 255</RGBA></BackgroundColour></Brush>")
        'add title
        _sis.CreateBoxText(0.02, 0.282, 0, 0.003, "Library: " & sLibName)
        _sis.SetInt(SIS_OT_CURITEM, 0, "_text_align&", SIS_TOP_LEFT)
        _sis.SetStr(SIS_OT_CURITEM, 0, "_pen$", "<Pen><Colour><RGBA>0 0 0 0</RGBA></Colour></Pen>")
        _sis.SetInt(SIS_OT_CURITEM, 0, "_point_height&", 12)
        _sis.UpdateItem()
        'add page number
        _sis.CreateBoxText(0.19, 0.282, 0, 0.003, "Page: " & iPage)
        _sis.SetInt(SIS_OT_CURITEM, 0, "_text_align&", SIS_TOP_RIGHT)
        _sis.SetStr(SIS_OT_CURITEM, 0, "_pen$", "<Pen><Colour><RGBA>0 0 0 0</RGBA></Colour></Pen>")
        _sis.SetInt(SIS_OT_CURITEM, 0, "_point_height&", 12)
        _sis.UpdateItem()
        'Dim iDataset As Integer = _sis.GetDataset
        _sis.SetInt(SIS_OT_OVERLAY, 0, "_bScaleOverride&", True)
        _sis.SetFlt(SIS_OT_OVERLAY, 0, "_scale#", 1)
        _sis.SetFlt(SIS_OT_OVERLAY, 0, "GPointScaleOverride#", 0)
    End Sub


    Private Sub GetBrushPath(ByRef Node As TreeNode)
        Try
            If Node.Name = "Brush" Then
                mIsBrush = True
            Else
                GetBrushPath(Node.Parent)
            End If
        Catch
            Exit Sub
        End Try
    End Sub


    Private Sub GetPenPath(ByRef Node As TreeNode)
        Try
            If Node.Name = "Pen" Then
                mIsPen = True
            Else
                GetPenPath(Node.Parent)
            End If
        Catch
            Exit Sub
        End Try
    End Sub


    Private Sub GetShapePath(ByRef Node As TreeNode)
        Try
            If Node.Name = "Shape" Then
                mIsShape = True
            Else
                GetShapePath(Node.Parent)
            End If
        Catch
            Exit Sub
        End Try
    End Sub


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub


    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub


    Private Sub btnCancel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        chkBrushes.Enabled = True
        chkPens.Enabled = True
        chkShapes.Enabled = True
        btnOK.Enabled = True
        btnCreate.Enabled = True
        gCancelled = True
    End Sub


    Private Sub chkBrushes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBrushes.Click
        If chkBrushes.Checked Or chkPens.Checked Or chkShapes.Checked Then
            btnCreate.Enabled = True
        Else
            btnCreate.Enabled = False
        End If
    End Sub


    Private Sub chkPens_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPens.Click
        If chkBrushes.Checked Or chkPens.Checked Or chkShapes.Checked Then
            btnCreate.Enabled = True
        Else
            btnCreate.Enabled = False
        End If
    End Sub


    Private Sub chkShapes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShapes.Click
        If chkBrushes.Checked Or chkPens.Checked Or chkShapes.Checked Then
            btnCreate.Enabled = True
        Else
            btnCreate.Enabled = False
        End If
    End Sub


    Public Sub New(ByVal sis As Cadcorp.SIS.GisLink.Library.MapManager)
        InitializeComponent()
        _sis = sis
    End Sub

End Class