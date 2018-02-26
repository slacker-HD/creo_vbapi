Imports System.Configuration
Imports pfcls

Module Module_vbapi
    Public asyncConnection As IpfcAsyncConnection = Nothing '全局变量，用于存储连接会话的句柄

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
    ''' 创建三个层，分别包含所有的表格、尺寸、图形符号、文字注解
    ''' </summary>
    Public Sub CreateLayers()
        Dim model As IpfcModel
        Dim drawing As IpfcDrawing
        Dim model2D As IpfcModel2D
        Dim tables, layers, notes, symbols As IpfcModelItems
        Dim detailSymbols As IpfcDetailItems
        Dim models As IpfcModels
        Dim dimensions As IpfcDimension2Ds '显示尺寸有点特殊,无法通过MODELITEM的ListItems方法获取，而是通过IpfcModel2D的ListShownDimensions方法获取
        Dim layer As IpfcLayer
        Dim i As Integer
        Try
            If Isdrawding() = True Then
                model = asyncConnection.Session.CurrentModel
                drawing = CType(model, IpfcDrawing)
                model2D = CType(model, IpfcModel2D)
                '列举现有的所有层
                layers = CType(model, IpfcModelItemOwner).ListItems(EpfcModelItemType.EpfcITEM_LAYER)
                '如果是NOTE，DIMENSION或者SYMBOL就先删除
                For i = 0 To layers.Count - 1
                    If layers.Item(i).GetName = "TABLE" Or layers.Item(i).GetName = "NOTE" Or layers.Item(i).GetName = "DIMENSION" Or layers.Item(i).GetName = "SYMBOL" Then
                        layer = CType(layers.Item(i), IpfcLayer)
                        layer.Delete()
                    End If
                Next
                '=========================================================================================================================================================
                '下面为添加layer并增加元素
                '为方便描述定义多个变量，同时没有用函数封装
                '=========================================================================================================================================================
                '添加包含所有表格的层
                layer = model.CreateLayer("TABLE")
                tables = CType(model, IpfcModelItemOwner).ListItems(EpfcModelItemType.EpfcITEM_TABLE)
                For i = 0 To tables.Count - 1
                    layer.AddItem(tables.Item(i))
                Next
                '添加包含所有注解的层
                layer = model.CreateLayer("NOTE")
                notes = CType(model, IpfcModelItemOwner).ListItems(EpfcModelItemType.EpfcITEM_DTL_NOTE)
                For i = 0 To notes.Count - 1
                    layer.AddItem(notes.Item(i))
                Next
                '添加包含所有符号的层
                layer = model.CreateLayer("SYMBOL")
                symbols = CType(model, IpfcModelItemOwner).ListItems(EpfcModelItemType.EpfcITEM_DTL_SYM_INSTANCE)
                For i = 0 To symbols.Count - 1
                    layer.AddItem(symbols.Item(i))
                Next
                'detailSymbols = CType(drawing, IpfcDetailItemOwner).ListDetailItems(EpfcDetailType.EpfcDETAIL_SYM_INSTANCE, drawing.CurrentSheetNumber)
                'For i = 0 To detailSymbols.Count - 1
                '    layer.AddItem(detailSymbols.Item(i))
                'Next
                '添加包含所有尺寸的层
                layer = model.CreateLayer("DIMENSION")
                models = model2D.ListModels()
                For Each refmodel As IpfcModel In models
                    '添加该model的所有尺寸
                    dimensions = model2D.ListShownDimensions(refmodel, EpfcModelItemType.EpfcITEM_DIMENSION)
                    For i = 0 To dimensions.Count - 1
                        layer.AddItem(dimensions.Item(i))
                    Next
                    '添加该model的所有参考尺寸
                    dimensions = model2D.ListShownDimensions(refmodel, EpfcModelItemType.EpfcITEM_REF_DIMENSION)
                    For i = 0 To dimensions.Count - 1
                        layer.AddItem(dimensions.Item(i))
                    Next
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub


End Module