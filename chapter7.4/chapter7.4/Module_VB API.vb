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
    ''' 放置符号
    ''' </summary>
    ''' <param name="Symbolfile">符号文件</param>
    ''' <param name="AttachOnDefType">放置方式</param>
    ''' <param name="Texts">符号的文本内容</param>
    ''' <param name="Selectfilter">放置连接的对象，如果有的话默认为边</param>
    ''' <param name="height">符号高度，默认为3.5</param>
    Public Sub PlaceSymbol(ByVal Symbolfile As String, ByVal AttachOnDefType As EpfcSymbolDefAttachmentType， Optional ByVal Texts As Dictionary(Of String, String) = Nothing, Optional ByVal Selectfilter As String = "edge", Optional ByVal height As Double = 3.5)
        Dim model As IpfcModel
        Dim drawing As IpfcDrawing
        Dim symbolDefinition As IpfcDetailSymbolDefItem
        Dim symbolDefAttachment As IpfcSymbolDefAttachment = Nothing
        Dim symInstructions As IpfcDetailSymbolInstInstructions
        Dim position As IpfcAttachment
        Dim leader As IpfcParametricAttachment = Nothing
        Dim allAttachments As IpfcDetailLeaders = Nothing
        Dim symItem As IpfcDetailSymbolInstItem
        Dim selectedObj As IpfcSelection
        Try
            If Isdrawding() = True Then
                model = asyncConnection.Session.CurrentModel
                drawing = CType(model, IpfcDrawing)

                Select Case AttachOnDefType
                    Case EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_FREE '自由放置，所以不需要选择边等对象操作，但是需要鼠标点击选择
                        '鼠标左键点选符号放置的位置
                        position = MousePosAttatchement()
                    Case EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_LEFT_LEADER, EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_RIGHT_LEADER, EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_RADIAL_LEADER
                        '动态选择一个边,或者作为引出线，或者作为垂直于图元上的点
                        selectedObj = SelectObject()
                        '鼠标左键点选符号放置的位置
                        position = MousePosAttatchement()
                        '初始化leader
                        leader = (New CCpfcParametricAttachment).Create(selectedObj)
                        '设置SymbolDefAttachment
                        symbolDefAttachment = SetSymbolDefAttachment(EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_NORMAL_TO_ITEM, selectedObj)
                    Case EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_ON_ITEM, EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_NORMAL_TO_ITEM
                        '动态选择一个边,或者作为引出线，或者作为垂直于图元上的点
                        selectedObj = SelectObject()
                        '设置位置
                        position = (New CCpfcParametricAttachment).Create(selectedObj)
                        CType(position, IpfcParametricAttachment).AttachedGeometry = selectedObj '为了代码通用性，使用父类IpfcAttachment定义position，这里应该用子类IpfcParametricAttachment，故强制类型转化下
                        '设置SymbolDefAttachment
                        symbolDefAttachment = SetSymbolDefAttachment(EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_NORMAL_TO_ITEM, selectedObj)
                    Case Else
                        Throw New NotImplementedException() '其余的未处理
                End Select
                '设置Attachments
                allAttachments = SetAttatchements(leader, position)
                '加载符号文件，注意这里没有进行校验
                symbolDefinition = drawing.RetrieveSymbolDefinition(Symbolfile, CObj(Symbolpath), Nothing, True)
                '初始化并设置symInstructions的值
                symInstructions = (New CCpfcDetailSymbolInstInstructions).Create(symbolDefinition)
                '设置文字
                symInstructions.TextValues = SetDetailVariantTexts(Texts)
                '设置高度
                symInstructions.ScaledHeight = 3.5
                '设置三个显示方式的重要属性
                symInstructions.DefAttachment = symbolDefAttachment
                symInstructions.InstAttachment = allAttachments
                symInstructions.AttachOnDefType = AttachOnDefType
                '创建symItem并显示
                symItem = drawing.CreateDetailItem(symInstructions)
                symItem.Show()
                Reg_Csheet()
                asyncConnection.Session.UIClearMessage()
            End If
        Catch ex As Exception
            asyncConnection.Session.UIClearMessage()
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 设置IpfcDetailSymbolInstInstructions的SymbolDefAttachment
    ''' </summary>
    ''' <param name="AttachOnDefType">符号放置方式</param>
    ''' <param name="selectedObject">放置点</param>
    ''' <returns>SymbolDefAttachment对象</returns>
    Private Function SetSymbolDefAttachment(ByVal AttachOnDefType As EpfcSymbolDefAttachmentType, ByVal selectedObject As IpfcSelection) As IpfcSymbolDefAttachment
        Return (New CCpfcSymbolDefAttachment).Create(AttachOnDefType, selectedObject.Point)
    End Function

    ''' <summary>
    ''' 设置符号中的文字，定义一个字典类型记录文字，key表示可变文本名称，value为可变文本值
    ''' </summary>
    ''' <param name="Dicts">可变文本</param>
    ''' <returns>可变文本CpfcDetailVariantTexts对象</returns>
    Private Function SetDetailVariantTexts(ByVal Dicts As Dictionary(Of String, String)) As CpfcDetailVariantTexts
        Dim varText As IpfcDetailVariantText
        If Dicts.Count > 0 Then
            SetDetailVariantTexts = New CpfcDetailVariantTexts
            For Each text As KeyValuePair(Of String, String) In Dicts
                varText = (New CCpfcDetailVariantText).Create(text.Key, text.Value)
                SetDetailVariantTexts.Append(varText)
            Next
        Else
            SetDetailVariantTexts = Nothing
        End If
        Return SetDetailVariantTexts
    End Function

    ''' <summary>
    ''' String转CpfcDetailTexts
    ''' </summary>
    ''' <param name="Texts">Strings，Chr(10)分割行</param>
    ''' <returns>CpfcDetailTexts对象</returns>
    Private Function StrstoTextlines(ByVal Texts As String) As CpfcDetailTextLines
        Dim detailText As IpfcDetailText
        Dim detailTexts As CpfcDetailTexts
        Dim textLine As IpfcDetailTextLine
        Dim i As Integer
        Dim Strs() As String
        '将String赋值给textLines
        StrstoTextlines = New CpfcDetailTextLines
        Strs = Split(Texts, Chr(10)) '根据回车符分割确定行数
        '根据行数创建对象并添加内容
        For i = 0 To Strs.Length - 1
            detailText = (New CCpfcDetailText).Create(Strs(Strs.Length - i - 1)) '注意顺序
            detailTexts = New CpfcDetailTexts
            detailTexts.Insert(0, detailText)
            textLine = (New CCpfcDetailTextLine).Create(detailTexts)
            StrstoTextlines.Insert(0, textLine)
        Next
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
        '这里为简化代码，未对selectEdge进行检测
        '======================================================================
        asyncConnection.Session.UIDisplayMessage(Msg_file, "GetEdgeForLeader", Nothing) '显示提示，这里注意Msg_file的位置和内容
        selectionOptions = (New CCpfcSelectionOptions).Create(filter)
        selectionOptions.MaxNumSels = 1
        selections = asyncConnection.Session.Select(selectionOptions, Nothing)
        SelectObject = selections.Item(0)
        Return SelectObject
    End Function

    ''' <summary>
    ''' 获取鼠标点击位置
    ''' </summary>
    ''' <returns></returns>
    Private Function MousePosAttatchement() As IpfcAttachment
        Dim point As CpfcPoint3D
        Dim mouse As IpfcMouseStatus
        '鼠标左键点选注解放置的位置
        asyncConnection.Session.UIDisplayMessage(Msg_file, "GetNotePos", Nothing) '显示提示，这里注意Msg_file的位置和内容
        point = New CpfcPoint3D
        mouse = asyncConnection.Session.UIGetNextMousePick(EpfcMouseButton.EpfcMOUSE_BTN_LEFT)
        point = mouse.Position
        MousePosAttatchement = (New CCpfcFreeAttachment).Create(point)
        Return MousePosAttatchement
    End Function

    ''' <summary>
    ''' 生成Leaders
    ''' </summary>
    ''' <param name="leader"></param>
    ''' <param name="position"></param>
    ''' <returns>生成的leaders</returns>
    Private Function SetAttatchements(ByVal leader As IpfcAttachment, ByVal position As IpfcAttachment) As IpfcDetailLeaders
        Dim attachments As CpfcAttachments
        SetAttatchements = (New CCpfcDetailLeaders).Create()
        SetAttatchements.ItemAttachment = position
        SetAttatchements.ElbowLength = Nothing
        If (leader IsNot Nothing) Then
            attachments = New CpfcAttachments
            attachments.Insert(0, leader)
            SetAttatchements.Leaders = attachments
        End If
        Return SetAttatchements
    End Function

End Module