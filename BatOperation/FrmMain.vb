Option Explicit On
Imports System.Configuration
Public Class FrmMain
    Private _linklabels(6) As LinkLabel
    Private _tableLayoutPanels(6) As TableLayoutPanel

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer

        PanelHelp.Dock = DockStyle.Fill

        _linklabels(0) = LinkLabelParam
        _linklabels(1) = LinkLabelRelation
        _linklabels(2) = LinkLabelObject
        _linklabels(3) = LinkLabelUnit
        _linklabels(4) = LinkLabelModelPurge
        _linklabels(5) = LinkLabelFamTab
        _linklabels(6) = LinkLabelFrm

        _tableLayoutPanels(0) = TableLayoutPanelParam
        _tableLayoutPanels(1) = TableLayoutPanelRelation
        _tableLayoutPanels(2) = TableLayoutPanelObject
        _tableLayoutPanels(3) = TableLayoutPanelUnit
        _tableLayoutPanels(4) = TableLayoutPanelModelPurge
        _tableLayoutPanels(5) = TableLayoutPanelFamTab
        _tableLayoutPanels(6) = TableLayoutPanelFrm

        For i = 0 To _linklabels.Length - 1
            _linklabels(i).Tag = i
            _tableLayoutPanels(i).Tag = i
            AddHandler _linklabels(i).LinkClicked, AddressOf LinkClicked
        Next

    End Sub

    Private Sub LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Dim i As Integer
        i = Int(CType(sender, LinkLabel).Tag)
        _tableLayoutPanels(i).Dock = DockStyle.Fill
        _tableLayoutPanels(i).BringToFront()
    End Sub

    Private Sub BtnChosefileParam_Click(sender As Object, e As EventArgs) Handles BtnChosefileParam.Click
        FolderBrowserDialogCommon.Description = "选择包含要修改参prt的文件夹"
        If (FolderBrowserDialogCommon.ShowDialog = DialogResult.OK) Then
            TextBoxPathParam.Text = FolderBrowserDialogCommon.SelectedPath
        End If
    End Sub

    Private Sub ButtonFamTabFileSelect_Click(sender As Object, e As EventArgs) Handles ButtonFamTabFileSelect.Click
        OpenFileDialogCommon.Filter = "prt零件(*.prt.*)|*.prt*|All files (*.*)|*.*"
        OpenFileDialogCommon.RestoreDirectory = True
        OpenFileDialogCommon.FileName = ""
        If OpenFileDialogCommon.ShowDialog() = DialogResult.OK Then
            TextBoxFamTabFile.Text = OpenFileDialogCommon.FileName
        End If
    End Sub

    Private Sub ButtonFamTabDirSelect_Click(sender As Object, e As EventArgs) Handles ButtonFamTabDirSelect.Click
        FolderBrowserDialogCommon.Description = "选择族表文件要导出的文件夹"
        If (FolderBrowserDialogCommon.ShowDialog = DialogResult.OK) Then
            TextBoxFamTabPath.Text = FolderBrowserDialogCommon.SelectedPath
        End If
    End Sub

    Private Sub ButtonFamTabExport_Click(sender As Object, e As EventArgs) Handles ButtonFamTabExport.Click
        Dim items As List(Of String)
        Dim i As Integer
        items = ExportFamtableinstances(TextBoxFamTabFile.Text, TextBoxFamTabPath.Text)
        ListViewFamTab.Items.Clear()

        If (items IsNot Nothing) Then
            For i = 0 To items.Count - 1
                Dim lstitm As ListViewItem = ListViewFamTab.Items.Add(i)
                lstitm.SubItems.Add(items.Item(i))
                lstitm.SubItems.Add("已导出！")
            Next
        End If
        MessageBox.Show("批量族表实例导出处理操作完毕!")
    End Sub

    Private Sub FrmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        EndConnection()
    End Sub

    Private Sub ButtonModelPurgePathSelect_Click(sender As Object, e As EventArgs) Handles ButtonModelPurgePathSelect.Click
        FolderBrowserDialogCommon.Description = "选择要清空旧版本文件的文件夹"
        If (FolderBrowserDialogCommon.ShowDialog = DialogResult.OK) Then
            TextBoxModelPurge.Text = FolderBrowserDialogCommon.SelectedPath
        End If
    End Sub

    Private Sub LinkLabelConfig_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Dim config As Configuration
        config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim CmdLine As String = config.AppSettings.Settings("CmdLine").Value
        Dim ShowWindow As String = config.AppSettings.Settings("ShowWindow").Value
        Dim Configfile As String = config.AppSettings.Settings("Configfile").Value

        PanelConfig.Dock = DockStyle.Fill
        PanelConfig.BringToFront()
        TextBoxConfigExe.Text = CmdLine
        TextBoxConfigConfig.Text = Configfile
        If ShowWindow = "True" Then
            CheckBoxShowWindow.Checked = True
        Else
            CheckBoxShowWindow.Checked = False
        End If
    End Sub

    Private Sub ButtonConfigExe_Click(sender As Object, e As EventArgs) Handles ButtonConfigExe.Click
        OpenFileDialogCommon.Filter = "parametric.exe(parametric.exe)|parametric.exe"
        OpenFileDialogCommon.RestoreDirectory = True
        OpenFileDialogCommon.FileName = "parametric.exe"
        If OpenFileDialogCommon.ShowDialog() = DialogResult.OK Then
            TextBoxConfigExe.Text = OpenFileDialogCommon.FileName
        End If
    End Sub

    Private Sub ButtonConfigConfig_Click(sender As Object, e As EventArgs) Handles ButtonConfigConfig.Click
        OpenFileDialogCommon.Filter = "Creo配置文件(*.pro)|*.pro"
        OpenFileDialogCommon.RestoreDirectory = True
        OpenFileDialogCommon.FileName = "config.pro"
        If OpenFileDialogCommon.ShowDialog() = DialogResult.OK Then
            TextBoxConfigConfig.Text = OpenFileDialogCommon.FileName
        End If
    End Sub

    Private Sub ButtonConfigSave_Click(sender As Object, e As EventArgs) Handles ButtonConfigSave.Click
        If IsFileExist(TextBoxConfigExe.Text) = False Then
            MessageBox.Show("请输入parametric.exe程序路径！")
            Return
        End If
        If IsFileExist(TextBoxConfigConfig.Text) = False Then
            MessageBox.Show("请输入Creo配置文件路径！")
            Return
        End If
        If SaveConfig(TextBoxConfigExe.Text， TextBoxConfigConfig.Text， CheckBoxShowWindow.Checked) = True Then
            MessageBox.Show("配置已保存，请重启程序以便配置生效！")
        Else
            MessageBox.Show("无法保存配置！")
        End If
    End Sub

    Private Sub ButtonModelPurge_Click(sender As Object, e As EventArgs) Handles ButtonModelPurge.Click
        Dim items As Hashtable
        items = PurgeModels(TextBoxModelPurge.Text)
        Dim i As Integer = 1

        ListViewModelPurge.Items.Clear()

        If items IsNot Nothing Then
            For Each item In items
                Dim lstitm As ListViewItem = ListViewModelPurge.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If items(item.key) = True Then
                    lstitm.SubItems.Add("已删除")
                Else
                    lstitm.SubItems.Add("删除失败")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量旧版本删除操作完毕!")
    End Sub

    Private Sub ButtonRelationTextImport_Click(sender As Object, e As EventArgs) Handles ButtonRelationTextImport.Click
        Dim Relations As String()
        OpenFileDialogCommon.Filter = "文本文件(*.txt)|*.txt"
        OpenFileDialogCommon.RestoreDirectory = True
        OpenFileDialogCommon.FileName = ""
        If OpenFileDialogCommon.ShowDialog() = DialogResult.OK Then
            Relations = GetTexts(OpenFileDialogCommon.FileName)
            RichTextBoxRelation.Clear()
            For Each line In Relations
                RichTextBoxRelation.Text += line + Chr(13)
            Next
        End If
    End Sub

    Private Sub ButtonRelationChoosePath_Click(sender As Object, e As EventArgs) Handles ButtonRelationChoosePath.Click
        FolderBrowserDialogCommon.Description = "选择包含prt的文件夹"
        If (FolderBrowserDialogCommon.ShowDialog = DialogResult.OK) Then
            TextBoxRelation.Text = FolderBrowserDialogCommon.SelectedPath
        End If
    End Sub

    Private Sub ButtonRelationClear_Click(sender As Object, e As EventArgs) Handles ButtonRelationClear.Click
        Dim Items As Hashtable
        Dim i As Integer = 1
        Items = ClearRelations(TextBoxRelation.Text)
        ListViewRelation.Items.Clear()

        If Items IsNot Nothing Then
            For Each item In Items
                Dim lstitm As ListViewItem = ListViewRelation.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If Items(item.key) = True Then
                    lstitm.SubItems.Add("已清空")
                Else
                    lstitm.SubItems.Add("清空失败")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量关系处理操作完毕!")

    End Sub

    Private Sub ButtonRelationAdd_Click(sender As Object, e As EventArgs) Handles ButtonRelationAdd.Click
        Dim Items As Hashtable
        Dim i As Integer = 1
        Items = AddRelations(TextBoxRelation.Text, RichTextBoxRelation.Text.Split(vbLf))
        ListViewRelation.Items.Clear()
        If Items IsNot Nothing Then
            For Each item In Items
                Dim lstitm As ListViewItem = ListViewRelation.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If Items(item.key) = True Then
                    lstitm.SubItems.Add("已添加关系！")
                Else
                    lstitm.SubItems.Add("添加关系失败！")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量关系处理操作完毕!")
    End Sub

    Private Sub ButtonUnitPathChoose_Click(sender As Object, e As EventArgs) Handles ButtonUnitPathChoose.Click
        FolderBrowserDialogCommon.Description = "选择包含prt的文件夹"
        If (FolderBrowserDialogCommon.ShowDialog = DialogResult.OK) Then
            TextBoxUnit.Text = FolderBrowserDialogCommon.SelectedPath
        End If
    End Sub

    Private Sub ButtonUnitRead_Click(sender As Object, e As EventArgs) Handles ButtonUnitRead.Click
        Dim items As New Hashtable
        Dim i As Integer = 1
        items = GetUnits(TextBoxUnit.Text)
        DataGridViewUnit.Rows.Clear()
        If items IsNot Nothing Then
            For Each item In items
                Dim row As New DataGridViewRow
                Dim textboxcell1 As New DataGridViewTextBoxCell
                Dim textboxcell2 As New DataGridViewTextBoxCell
                Dim textboxcell3 As New DataGridViewTextBoxCell
                Dim comboxcell As New DataGridViewComboBoxCell

                textboxcell1.Value = i
                row.Cells.Add(textboxcell1)
                textboxcell1.ReadOnly = True

                textboxcell2.Value = item.key
                row.Cells.Add(textboxcell2)
                textboxcell2.ReadOnly = True

                textboxcell3.Value = items(item.key)
                row.Cells.Add(textboxcell3)
                textboxcell3.ReadOnly = True


                If items(item.key).ToString().ToLower.IndexOf("mmns") = -1 Then
                    comboxcell.Items.AddRange(New Object() {"转换尺寸", "解释尺寸", "不转换"})
                    comboxcell.Value = "转换尺寸"
                    comboxcell.FlatStyle = FlatStyle.Flat
                Else
                    comboxcell.Items.AddRange(New Object() {"/"})
                    comboxcell.Value = "/"
                    comboxcell.FlatStyle = FlatStyle.Standard

                End If

                row.Cells.Add(comboxcell)

                If items(item.key).ToString().ToLower.IndexOf("mmns") = -1 Then
                    comboxcell.ReadOnly = False
                Else
                    comboxcell.ReadOnly = True
                End If

                DataGridViewUnit.Rows.Add(row)
                i += 1
            Next
        End If
    End Sub

    Private Sub ButtonUnitConvert_Click(sender As Object, e As EventArgs) Handles ButtonUnitConvert.Click
        Dim items As New Hashtable
        Dim i As Integer
        For i = 0 To DataGridViewUnit.Rows.Count - 1
            If DataGridViewUnit.Rows(i).Cells(3).ReadOnly = False And DataGridViewUnit.Rows(i).Cells(3).Value <> "不转换" Then
                items.Add(DataGridViewUnit.Rows(i).Cells(1).Value, DataGridViewUnit.Rows(i).Cells(3).Value)
            End If
        Next
        items = ChangeUnits(items)
        ButtonUnitRead.PerformClick()
        MessageBox.Show("批量单位转换操作完毕!")
    End Sub

    Private Sub ButtonImportCSVParam_Click(sender As Object, e As EventArgs) Handles ButtonImportCSVParam.Click
        OpenFileDialogCommon.Filter = "关系零件(*.csv)|*.csv"
        OpenFileDialogCommon.RestoreDirectory = True
        OpenFileDialogCommon.FileName = ""
        If OpenFileDialogCommon.ShowDialog() = DialogResult.OK Then
            Dim SBind As New BindingSource With {
                .DataSource = CSVToDataTable(OpenFileDialogCommon.FileName)
            }
            DataGridViewParam.DataSource = SBind
        End If
    End Sub

    Private Sub ButtonParamDesignate_Click(sender As Object, e As EventArgs) Handles ButtonParamDesignate.Click
        Dim Items As Hashtable
        Dim i As Integer = 1
        Items = DesignateParams(TextBoxPathParam.Text)
        ListViewParam.Items.Clear()

        If Items IsNot Nothing Then
            For Each item In Items
                Dim lstitm As ListViewItem = ListViewParam.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If Items(item.key) = True Then
                    lstitm.SubItems.Add("已全部指定")
                Else
                    lstitm.SubItems.Add("存在部分参数未能成功指定")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量参数指定操作完毕!")
    End Sub

    Private Sub ButtonParamClear_Click(sender As Object, e As EventArgs) Handles ButtonParamClear.Click
        Dim Items As Hashtable
        Dim i As Integer = 1
        Items = ClearParams(TextBoxPathParam.Text)
        ListViewParam.Items.Clear()

        If Items IsNot Nothing Then
            For Each item In Items
                Dim lstitm As ListViewItem = ListViewParam.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If Items(item.key) = True Then
                    lstitm.SubItems.Add("已清空")
                Else
                    lstitm.SubItems.Add("存在受限制或锁定的参数未删除")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量参数清空操作完毕!")
    End Sub

    Private Sub ButtonParamAdd_Click(sender As Object, e As EventArgs) Handles ButtonParamAdd.Click
        Dim Items As Hashtable
        Dim i As Integer = 1
        Items = AddParams(TextBoxPathParam.Text, DataGridViewParam.DataSource)
        ListViewParam.Items.Clear()

        If Items IsNot Nothing Then
            For Each item In Items
                Dim lstitm As ListViewItem = ListViewParam.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If Items(item.key) = True Then
                    lstitm.SubItems.Add("已添加或修改")
                Else
                    lstitm.SubItems.Add("存在受限制或锁定的参数未添加或修改")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量参数添加修改操作完毕!")
    End Sub

    Private Sub ButtonButtonFrmPathSelect_Click(sender As Object, e As EventArgs) Handles ButtonFrminputPathSelect.Click
        FolderBrowserDialogCommon.Description = "选择包含drw的文件夹"
        If (FolderBrowserDialogCommon.ShowDialog = DialogResult.OK) Then
            TextBoxFrmInputPath.Text = FolderBrowserDialogCommon.SelectedPath
        End If
    End Sub

    Private Sub ButtonFrmPathSelect_Click(sender As Object, e As EventArgs) Handles ButtonFrmPathSelect.Click
        FolderBrowserDialogCommon.Description = "选择包含frm的文件夹"
        If (FolderBrowserDialogCommon.ShowDialog = DialogResult.OK) Then
            TextBoxFrmPath.Text = FolderBrowserDialogCommon.SelectedPath
        End If
    End Sub

    Private Sub ButtonFrmInputList_Click(sender As Object, e As EventArgs) Handles ButtonFrmInputList.Click
        Dim i As Integer = 1
        Dim j As Integer = 1
        Dim items As Hashtable = GetDrwList(TextBoxFrmInputPath.Text)
        DataGridViewFrm.Rows.Clear()
        If items IsNot Nothing Then
            For Each item In items
                j = 1
                For Each line In item.Value
                    Dim row As New DataGridViewRow
                    Dim textboxcell1 As New DataGridViewTextBoxCell
                    Dim textboxcell2 As New DataGridViewTextBoxCell
                    Dim textboxcell3 As New DataGridViewTextBoxCell

                    Dim comboxcell1 As New DataGridViewComboBoxCell
                    Dim comboxcell2 As New DataGridViewComboBoxCell

                    textboxcell1.Value = i
                    row.Cells.Add(textboxcell1)
                    textboxcell1.ReadOnly = True

                    textboxcell2.Value = item.key
                    row.Cells.Add(textboxcell2)
                    textboxcell2.ReadOnly = True


                    textboxcell3.Value = j
                    row.Cells.Add(textboxcell3)
                    textboxcell3.ReadOnly = True

                    comboxcell1.Items.AddRange(line)
                    comboxcell1.FlatStyle = FlatStyle.Flat
                    comboxcell1.Value = comboxcell1.Items(0)

                    row.Cells.Add(comboxcell1)

                    comboxcell2.Items.AddRange(New Object() {"删除内建表格", "不删除内建表格"})
                    comboxcell2.FlatStyle = FlatStyle.Flat
                    comboxcell2.Value = comboxcell2.Items(0)

                    row.Cells.Add(comboxcell2)

                    DataGridViewFrm.Rows.Add(row)
                    i += 1
                    j += 1
                Next
            Next
        End If
    End Sub

    Private Sub ButtonFrmList_Click(sender As Object, e As EventArgs) Handles ButtonFrmList.Click
        Dim items As List(Of String)
        Dim i As Integer
        items = GetFrmList(TextBoxFrmPath.Text)
        If items IsNot Nothing Then
            For i = 0 To DataGridViewFrm.Rows.Count - 1
                Dim comboxcell As DataGridViewComboBoxCell
                comboxcell = DataGridViewFrm.Rows(i).Cells(3)
                Dim first = comboxcell.Items(0)
                comboxcell.Items.Clear()
                comboxcell.Items.Add(first)
                For Each item In items
                    comboxcell.Items.Add(item)
                Next
                comboxcell.Value = comboxcell.Items(0)
            Next
        End If
    End Sub

    Private Sub ButtonObjectSourcepathSelect_Click(sender As Object, e As EventArgs) Handles ButtonObjectSourcepathSelect.Click
        FolderBrowserDialogCommon.Description = "选择包含要导出格式文件的文件夹"
        If (FolderBrowserDialogCommon.ShowDialog = DialogResult.OK) Then
            TextBoxObjectSourcepathSelect.Text = FolderBrowserDialogCommon.SelectedPath
        End If
    End Sub

    Private Sub ButtonObjectOutputpathSelect_Click(sender As Object, e As EventArgs) Handles ButtonObjectOutputpathSelect.Click
        FolderBrowserDialogCommon.Description = "选择要导出的文件夹"
        If (FolderBrowserDialogCommon.ShowDialog = DialogResult.OK) Then
            TextBoxObjectOutputpathSelect.Text = FolderBrowserDialogCommon.SelectedPath
        End If
    End Sub

    Private Sub ButtonObjectPdf_Click(sender As Object, e As EventArgs) Handles ButtonObjectPdf.Click
        Dim Items As Hashtable
        Dim i As Integer = 1
        Items = ExportPdfs(TextBoxObjectSourcepathSelect.Text, TextBoxObjectOutputpathSelect.Text)
        ListViewObject.Items.Clear()

        If Items IsNot Nothing Then
            For Each item In Items
                Dim lstitm As ListViewItem = ListViewObject.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If Items(item.key) = True Then
                    lstitm.SubItems.Add("已导出")
                Else
                    lstitm.SubItems.Add("导出失败")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量导出pdf操作完毕!")
    End Sub

    Private Sub ButtonObjectDwg_Click(sender As Object, e As EventArgs) Handles ButtonObjectDwg.Click
        Dim Items As Hashtable
        Dim i As Integer = 1
        Items = ExportDwgs(TextBoxObjectSourcepathSelect.Text, TextBoxObjectOutputpathSelect.Text)
        ListViewObject.Items.Clear()

        If Items IsNot Nothing Then
            For Each item In Items
                Dim lstitm As ListViewItem = ListViewObject.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If Items(item.key) = True Then
                    lstitm.SubItems.Add("已导出")
                Else
                    lstitm.SubItems.Add("导出失败")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量导出dwg操作完毕!")
    End Sub

    Private Sub ButtonObjectStp_Click(sender As Object, e As EventArgs) Handles ButtonObjectStp.Click
        Dim Items As Hashtable
        Dim i As Integer = 1
        Items = ExportStps(TextBoxObjectSourcepathSelect.Text, TextBoxObjectOutputpathSelect.Text)
        ListViewObject.Items.Clear()

        If Items IsNot Nothing Then
            For Each item In Items
                Dim lstitm As ListViewItem = ListViewObject.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If Items(item.key) = True Then
                    lstitm.SubItems.Add("已导出")
                Else
                    lstitm.SubItems.Add("导出失败")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量导出Stp操作完毕!")
    End Sub

    Private Sub ButtonObjectIgs_Click(sender As Object, e As EventArgs) Handles ButtonObjectIgs.Click
        Dim Items As Hashtable
        Dim i As Integer = 1
        Items = ExportIgss(TextBoxObjectSourcepathSelect.Text, TextBoxObjectOutputpathSelect.Text)
        ListViewObject.Items.Clear()

        If Items IsNot Nothing Then
            For Each item In Items
                Dim lstitm As ListViewItem = ListViewObject.Items.Add(i)
                lstitm.SubItems.Add(item.key)
                If Items(item.key) = True Then
                    lstitm.SubItems.Add("已导出")
                Else
                    lstitm.SubItems.Add("导出失败")
                End If
                i += 1
            Next
        End If
        MessageBox.Show("批量导出Igs操作完毕!")
    End Sub

    Private Sub ButtonFrmConvert_Click(sender As Object, e As EventArgs) Handles ButtonFrmConvert.Click
        Dim items As New List(Of FrmPassData)
        Dim i As Integer
        For i = 0 To DataGridViewFrm.Rows.Count - 1
            If DataGridViewFrm.Rows(i).Cells(3).Value.Length > 1 And DataGridViewFrm.Rows(i).Cells(3).Value.Chars(1) = ":" Then
                Dim item As FrmPassData
                item.FileName = DataGridViewFrm.Rows(i).Cells(1).Value
                item.SheetNumber = DataGridViewFrm.Rows(i).Cells(2).Value
                item.FrmName = DataGridViewFrm.Rows(i).Cells(3).Value
                If (DataGridViewFrm.Rows(i).Cells(4).Value = "删除内建表格") Then
                    item.DelTab = True
                Else
                    item.DelTab = False
                End If
                items.Add(item)
            End If
        Next
        ChangeFrms(items)
        ButtonFrmInputList.PerformClick()
        ButtonFrmList.PerformClick()
        MessageBox.Show("批量图框转换操作完毕!")
    End Sub

    Private Sub LinkLabelHelp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        PanelHelp.Dock = DockStyle.Fill
        PanelHelp.BringToFront()
    End Sub
End Class
