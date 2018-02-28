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
    ''' 列举所有table
    ''' </summary>
    Public Function TablesInfo() As String
        Dim tableOwner As IpfcTableOwner
        Dim model As IpfcModel
        Dim tables As IpfcTables
        Dim table As IpfcTable
        Dim i As Integer
        TablesInfo = "无法获取表格信息"
        Try
            If Isdrawding() = True Then
                tableOwner = CType(asyncConnection.Session.CurrentModel, IpfcTableOwner)
                If HasTable() = True Then
                    model = asyncConnection.Session.CurrentModel
                    tableOwner = CType(model, IpfcTableOwner)
                    tables = tableOwner.ListTables()
                    TablesInfo = "当前绘图包含" + tables.Count.ToString + "个表格。" + Chr(10)
                    For i = 0 To tables.Count - 1
                        table = tables.Item(i)
                        TablesInfo += "第" + (i + 1).ToString() + "个表格为" + table.GetRowCount.ToString() + "X" + table.GetColumnCount.ToString() + "的表格。" + Chr(10)
                    Next
                Else
                    TablesInfo = "当前绘图未包含表格。"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
        Return TablesInfo
    End Function

    ''' <summary>
    ''' 设置选中表格的第row行第col列值，row,col索引从1开始
    ''' </summary>
    ''' <param name="content">文字内容</param>
    ''' <param name="row">行</param>
    ''' <param name="col">列</param>
    Public Sub SetTableInfo(ByVal content As String， ByVal row As Integer, ByVal col As Integer)
        Dim tableOwner As IpfcTableOwner
        Dim table As IpfcTable
        Dim tablecell As IpfcTableCell
        Dim Lines As New Cstringseq
        Try
            If Isdrawding() = True Then
                tableOwner = CType(asyncConnection.Session.CurrentModel, IpfcTableOwner)
                If HasTable() = True Then
                    table = SelectObject("dwg_table").SelItem
                    tablecell = (New CCpfcTableCell).Create(row, col)
                    Lines.Append(content)
                    table.SetText(tablecell, Lines)
                    Reg_Csheet()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 读取选中表格的第row行第col列值
    ''' </summary>
    ''' <param name="row">行</param>
    ''' <param name="col">列</param>
    ''' <returns></returns>
    Public Function GetTableInfo(ByVal row As Integer, ByVal col As Integer) As String
        Dim tableOwner As IpfcTableOwner
        Dim table As IpfcTable
        Dim tablecell As IpfcTableCell
        Dim cellnote As IpfcModelItem
        Dim detailNoteItem As IpfcDetailNoteItem
        Dim detailNoteInstructions As IpfcDetailNoteInstructions
        Dim i As Integer
        GetTableInfo = "未能读取到内容。"
        Try
            If Isdrawding() = True Then
                tableOwner = CType(asyncConnection.Session.CurrentModel, IpfcTableOwner)
                If HasTable() = True Then
                    table = SelectObject("dwg_table").SelItem
                    tablecell = (New CCpfcTableCell).Create(row, col)
                    cellnote = table.GetCellNote(tablecell)
                    If cellnote IsNot Nothing Then
                        If cellnote.Type = EpfcModelItemType.EpfcITEM_DTL_NOTE Then
                            detailNoteItem = CType(cellnote, IpfcDetailNoteItem)
                            detailNoteInstructions = detailNoteItem.GetInstructions(True)
                            GetTableInfo = “”
                            If detailNoteInstructions.TextLines.Item(0).Texts.Count > 0 Then
                                For i = 0 To detailNoteInstructions.TextLines.Count - 1
                                    GetTableInfo += detailNoteInstructions.TextLines.Item(0).Texts.Item(0).Text
                                Next
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
        Return GetTableInfo
    End Function

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
    ''' 选择获取一个对象,这里为简化代码，未进行有效性检测
    ''' </summary>
    ''' <param name="filter">选择对象类型，默认为边</param>
    ''' <returns>选择对象</returns>
    Private Function SelectObject(Optional ByVal filter As String = "edge") As IpfcSelection
        Dim selections As CpfcSelections
        Dim selectionOptions As IpfcSelectionOptions
        '======================================================================
        '这里为简化代码，未对select进行检测
        '======================================================================
        selectionOptions = (New CCpfcSelectionOptions).Create(filter)
        selectionOptions.MaxNumSels = 1
        selections = asyncConnection.Session.Select(selectionOptions, Nothing)
        SelectObject = selections.Item(0)
        Return SelectObject
    End Function

End Module