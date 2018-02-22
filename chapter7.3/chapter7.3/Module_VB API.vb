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
    ''' 插入自由放置的注解
    ''' </summary>
    ''' <param name="Texts">注解文字,多行文字用回车符Chr(10)分割</param>
    Public Sub CreateNoteWithoutLeader(ByVal Texts As String)
        Dim model As IpfcModel
        Dim drawing As IpfcDrawing
        Dim textLines As CpfcDetailTextLines
        Dim noteInstructions As IpfcDetailNoteInstructions
        Dim note As IpfcDetailNoteItem
        Dim position As IpfcFreeAttachment
        Dim allAttachments As IpfcDetailLeaders
        Try
            If Isdrawding() = True Then
                model = asyncConnection.Session.CurrentModel
                drawing = CType(model, IpfcDrawing)
                'String转CpfcDetailTextLines
                textLines = StrstoTextlines(Texts)
                '鼠标左键点选注解放置的位置
                position = MousePosAttatchement()
                '设置Attachments
                allAttachments = SetAttatchements(Nothing, position)
                '设置noteInstructions
                noteInstructions = (New CCpfcDetailNoteInstructions).Create(textLines)
                noteInstructions.Leader = allAttachments
                '创建note并显示
                note = drawing.CreateDetailItem(noteInstructions)
                note.Show()
                Reg_Csheet()
                asyncConnection.Session.UIClearMessage()
            End If
        Catch ex As Exception
            asyncConnection.Session.UIClearMessage()
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 插入带引线的注解
    ''' </summary>
    ''' <param name="Texts">注解文字,多行文字用回车符Chr(10)分割</param>
    Public Sub CreateNoteWithLeader(ByVal Texts As String)
        Dim model As IpfcModel
        Dim drawing As IpfcDrawing
        Dim selectedEdge As IpfcSelection '选择获取一个边
        Dim leader As IpfcParametricAttachment
        Dim allAttachments As IpfcDetailLeaders
        Dim position As IpfcFreeAttachment
        Dim textLines As CpfcDetailTextLines
        Dim noteInstructions As IpfcDetailNoteInstructions
        Dim note As IpfcDetailNoteItem
        Try
            If Isdrawding() = True Then
                model = asyncConnection.Session.CurrentModel
                drawing = CType(model, IpfcDrawing)
                textLines = StrstoTextlines(Texts)
                '动态选择一个边引出引线
                selectedEdge = SelectEdge()
                '初始化leader
                leader = (New CCpfcParametricAttachment).Create(selectedEdge)
                '鼠标左键点选注解放置的位置
                position = MousePosAttatchement()
                '设置Attachments
                allAttachments = SetAttatchements(leader, position)
                '设置noteInstructions
                noteInstructions = (New CCpfcDetailNoteInstructions).Create(textLines)
                noteInstructions.Leader = allAttachments
                '创建note并显示
                note = drawing.CreateDetailItem(noteInstructions)
                note.Show()
                Reg_Csheet()
                asyncConnection.Session.UIClearMessage()
            End If
        Catch ex As Exception
            asyncConnection.Session.UIClearMessage()
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

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
    ''' '选择获取一个边
    ''' </summary>
    ''' <returns>'选择获取一个边,这里为简化代码，未对selectEdge进行检测</returns>
    Private Function SelectEdge() As IpfcSelection
        Dim selections As CpfcSelections
        Dim selectionOptions As IpfcSelectionOptions
        '======================================================================
        '这里为简化代码，未对selectEdge进行检测
        '======================================================================
        asyncConnection.Session.UIDisplayMessage(Msg_file, "GetEdgeForLeader", Nothing) '显示提示，这里注意Msg_file的位置和内容
        selectionOptions = (New CCpfcSelectionOptions).Create("edge")
        selectionOptions.MaxNumSels = 1
        selections = asyncConnection.Session.Select(selectionOptions, Nothing)
        SelectEdge = selections.Item(0)
        Return SelectEdge
    End Function

    ''' <summary>
    ''' 获取鼠标点击位置
    ''' </summary>
    ''' <returns></returns>
    Private Function MousePosAttatchement() As IpfcFreeAttachment
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
    ''' <returns></returns>
    Private Function SetAttatchements(ByVal leader As IpfcParametricAttachment, ByVal position As IpfcFreeAttachment) As IpfcDetailLeaders
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