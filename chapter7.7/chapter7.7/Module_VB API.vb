Imports System.Configuration
Imports pfcls

Module Module_vbapi
    Public asyncConnection As IpfcAsyncConnection = Nothing '全局变量，用于存储连接会话的句柄

    ''' <summary>
    ''' 排列方式
    ''' </summary>
    Private Enum Placement

        ''' <summary>
        ''' 垂直排列
        ''' </summary>
        Vertical = 0

        ''' <summary>
        ''' 水平排列
        ''' </summary>
        Horizon = 1

    End Enum

    ''' <summary>
    ''' 连接现有会话
    ''' </summary>
    ''' <returns>是否连接成功</returns>
    Public Function Creo_Connect() As Boolean
        Try
            If asyncConnection Is Nothing OrElse Not asyncConnection.IsRunning Then
                asyncConnection = (New CCpfcAsyncConnection).Connect(Nothing, Nothing, Nothing, Nothing)
                Creo_Connect = True
            Else
                Return False
            End If
        Catch ex As Exception
            Creo_Connect = False
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Function

    ''' <summary>
    ''' 打开新会话
    ''' </summary>
    ''' <returns>新建会话是否成功</returns>
    Public Function Creo_New() As Boolean
        Try
            Dim CmdLine As String = ConfigurationManager.AppSettings("CmdLine").ToString()
            Dim TextPath As String = ConfigurationManager.AppSettings("TextPath").ToString()
            asyncConnection = (New CCpfcAsyncConnection).Start(CmdLine, TextPath)
            '''''''''''''''''''''''补充之前的问题，使用config文件'''''''''''''''''''''''''''''''''
            asyncConnection.Session.LoadConfigFile(ConfigurationManager.AppSettings("Configfile").ToString())
            '''''''''''''''''''''''选择工作目录'''''''''''''''''''''''''''''''''
            asyncConnection.Session.ChangeDirectory(ConfigurationManager.AppSettings("WorkDirectory").ToString())
            Creo_New = True
        Catch ex As Exception
            Creo_New = False
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Function

    ''' <summary>
    ''' 重生Drawing
    ''' </summary>
    Private Sub Reg_Csheet()
        Dim drawing As IpfcDrawing
        drawing = asyncConnection.Session.CurrentModel
        If Isdrawding() = True Then
            drawing = CType(drawing, IpfcDrawing)
            drawing.RegenerateSheet(drawing.CurrentSheetNumber)
        End If
    End Sub

    ''' <summary>
    ''' 判断是否为打开的是否为工程图
    ''' </summary>
    ''' <returns>是否为工程图</returns>
    Private Function Isdrawding() As Boolean
        Try
            If asyncConnection.Session.CurrentModel Is Nothing Then
                Isdrawding = False
            ElseIf (asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_DRAWING) Then
                Isdrawding = True
            Else
                Isdrawding = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            Isdrawding = False
        End Try
    End Function

    ''' <summary>
    ''' 国标球标
    ''' </summary>
    Public Sub GBBalloon()
        Dim Selection As IpfcSelection
        Dim tableOwner As IpfcTableOwner
        Try
            tableOwner = CType(asyncConnection.Session.CurrentModel, IpfcTableOwner)
            asyncConnection.Session.CurrentSelectionBuffer.Clear()
            Selection = (New CMpfcSelect).CreateModelItemSelection(tableOwner.GetTable(TableIDwithBom()), Nothing)
            asyncConnection.Session.CurrentSelectionBuffer.AddSelection(Selection)
            asyncConnection.Session.RunMacro("GBBALLOON ~ Command `ProCmdDwgTblProperties` ;~ Select `Odui_Dlg_00` `pg_vis_tab` 1 `tab_2`;~ Open `Odui_Dlg_00` `t2.opt_balloon_type`;~ Trigger `Odui_Dlg_00` `t2.opt_balloon_type` `simple`;~ Trigger `Odui_Dlg_00` `t2.opt_balloon_type` `quantity`;~ Trigger `Odui_Dlg_00` `t2.opt_balloon_type` `custom`;~ Close `Odui_Dlg_00` `t2.opt_balloon_type`;~ Select `Odui_Dlg_00` `t2.opt_balloon_type` 1 `custom`;~ FocusOut `Odui_Dlg_00` `t2.opt_balloon_type`;~ Activate `Odui_Dlg_00` `t2.push_browse_balloon_sym`;~ Trail `UI Desktop` `UI Desktop` `DLG_PREVIEW_POST` `file_open`;~ Select `file_open` `Ph_list.Filelist` 1 `GBqiubiao.sym`;~ Command `ProFileSelPushOpen@context_dlg_open_cmd` ;~ Activate `Odui_Dlg_00` `stdbtn_1`;")
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 垂直排列国标球标，将选择的国标化球标的横坐标定在其平均值位置
    ''' </summary>
    Public Sub VerticalBallon()
        PlaceBallon(Placement.Vertical)
    End Sub

    ''' <summary>
    ''' 水平排列国标球标
    ''' </summary>
    Public Sub HorizonBallon()
        PlaceBallon(Placement.Horizon)
    End Sub

    ''' <summary>
    ''' 返回鼠标点中的位置
    ''' </summary>
    ''' <returns>鼠标点击的位置</returns>
    Private Function MousePoint() As CpfcPoint3D
        Dim mouse As IpfcMouseStatus
        mouse = asyncConnection.Session.UIGetNextMousePick(EpfcMouseButton.EpfcMOUSE_BTN_LEFT)
        MousePoint = mouse.Position
        Return MousePoint
    End Function

    ''' <summary>
    ''' 排列球标符号
    ''' </summary>
    ''' <param name="palcement">排列方式，水平还是垂直</param>
    Private Sub PlaceBallon(ByVal palcement As Placement)
        Dim modelitem As IpfcModelItem
        Dim selections As IpfcSelectionBuffer
        Dim selectBalloon As IpfcSelection
        Dim point As CpfcPoint3D
        Dim i As Integer
        Dim item As IpfcDetailSymbolInstItem
        Dim detailSymbolDefInstructions As IpfcDetailSymbolInstInstructions
        Dim leaders As IpfcDetailLeaders
        Dim attachment As IpfcFreeAttachment
        Dim detailItemOwner As IpfcDetailItemOwner
        Try
            If Isdrawding() = True Then
                detailItemOwner = CType(asyncConnection.Session.CurrentModel, IpfcDetailItemOwner)
                '鼠标点选一个点作为垂直排列球标的横坐标
                point = MousePoint()
                '获取所有选定的对象，确保选中的都是球标
                selections = asyncConnection.Session.CurrentSelectionBuffer
                For i = 0 To selections.Contents.Count - 1
                    selectBalloon = selections.Contents.Item(i)
                    modelitem = selectBalloon.SelItem
                    '国标化球标将球标变成了一个可以访问的DTL_SYM_INSTANCE，未国标化操作的球标无法完成以下操作
                    If modelitem Is Nothing Then
                        Continue For
                    End If
                    If modelitem.Type = EpfcModelItemType.EpfcITEM_DTL_SYM_INSTANCE Then
                        item = CType(modelitem, IpfcDetailSymbolInstItem)
                        '获得原始球标放置的信息
                        detailSymbolDefInstructions = item.GetInstructions(True)
                        leaders = detailSymbolDefInstructions.InstAttachment
                        attachment = leaders.ItemAttachment
                        '修改放置位置横坐标值为选中的坐标
                        If palcement = Placement.Horizon Then
                            attachment.AttachmentPoint.Set(1, point.Item(1))
                        Else
                            attachment.AttachmentPoint.Set(0, point.Item(0))
                        End If
                        leaders.ItemAttachment = attachment
                        detailSymbolDefInstructions.InstAttachment = leaders
                        '球标重新放置放置
                        item.Modify(detailSymbolDefInstructions)
                    End If
                Next
                Reg_Csheet()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 判断当前打开绘图是否包含表格
    ''' </summary>
    ''' <returns>当前打开绘图是否包含表格</returns>
    Private Function HasTable() As Boolean
        Dim model As IpfcModel
        Dim tableOwner As IpfcTableOwner
        Dim tables As IpfcTables
        HasTable = False
        model = asyncConnection.Session.CurrentModel
        tableOwner = CType(model, IpfcTableOwner)
        tables = tableOwner.ListTables()
        If tables Is Nothing Then
            Return False
        End If
        If tables.Count = 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 返回包含&&rpt.index的表格ID，即包含BOM的表格
    ''' </summary>
    ''' <returns>返回包含包含BOM的表格</returns>
    Private Function TableIDwithBom() As Integer
        Dim model As IpfcModel
        Dim tableOwner As IpfcTableOwner
        Dim tables As IpfcTables
        Dim table As IpfcTable
        Dim tableCell As IpfcTableCell
        Dim cellnote As IpfcModelItem
        Dim detailNoteItem As IpfcDetailNoteItem
        Dim detailNoteInstructions As IpfcDetailNoteInstructions
        Dim i, j As Integer
        'CREO表格的序号从1开始
        TableIDwithBom = Integer.MinValue
        Try
            If Isdrawding() = True Then
                If HasTable() = True Then
                    model = asyncConnection.Session.CurrentModel
                    tableOwner = CType(model, IpfcTableOwner)
                    tables = tableOwner.ListTables()
                    tableCell = (New CCpfcTableCell).Create(1, 1)
                    For Each table In tables
                        For i = 1 To table.GetRowCount()
                            For j = 1 To table.GetColumnCount()
                                tableCell.RowNumber = i
                                tableCell.ColumnNumber = j
                                cellnote = table.GetCellNote(tableCell)
                                If cellnote IsNot Nothing Then
                                    If cellnote.Type = EpfcModelItemType.EpfcITEM_DTL_NOTE Then
                                        detailNoteItem = CType(cellnote, IpfcDetailNoteItem)
                                        detailNoteInstructions = detailNoteItem.GetInstructions(True)
                                        If detailNoteInstructions.TextLines.Item(0).Texts.Count > 0 Then
                                            If (detailNoteInstructions.TextLines.Item(0).Texts.Item(0).Text = "&rpt.index") Then
                                                TableIDwithBom = table.Id
                                                Return TableIDwithBom
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        Next
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
        Return TableIDwithBom
    End Function

End Module