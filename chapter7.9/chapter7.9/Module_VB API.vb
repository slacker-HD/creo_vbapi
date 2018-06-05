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
    ''' 添加一个直线草绘
    ''' </summary>
    Public Sub CreateLine()
        Dim model As IpfcModel
        Dim linecolor As IpfcColorRGB
        Dim drawing As IpfcDrawing
        Dim currSheet As Integer
        Dim view As IpfcView2D
        Dim mouse1 As IpfcMouseStatus
        Dim mouse2 As IpfcMouseStatus
        Dim start As IpfcPoint3D
        Dim finish As IpfcPoint3D
        Dim geom As IpfcLineDescriptor
        Dim lineInstructions As IpfcDetailEntityInstructions
        Try
            If Isdrawding() Then
                model = asyncConnection.Session.CurrentModel
                drawing = CType(model, IpfcDrawing)
                currSheet = drawing.CurrentSheetNumber
                view = drawing.GetSheetBackgroundView(currSheet)
                '鼠标左键点击获取起点和终点
                mouse1 = asyncConnection.Session.UIGetNextMousePick(EpfcMouseButton.EpfcMOUSE_BTN_LEFT)
                start = mouse1.Position
                mouse2 = asyncConnection.Session.UIGetNextMousePick(EpfcMouseButton.EpfcMOUSE_BTN_LEFT)
                finish = mouse2.Position
                '初始化IpfcLineDescriptor
                geom = (New CCpfcLineDescriptor).Create(start, finish)
                '设点线颜色
                linecolor = asyncConnection.Session.GetRGBFromStdColor(EpfcStdColor.EpfcCOLOR_CURVE)
                '初始化IpfcDetailEntityInstructions
                lineInstructions = (New CCpfcDetailEntityInstructions).Create(geom, view)
                lineInstructions.Color = linecolor
                '创建并显示直线草绘
                drawing.CreateDetailItem(lineInstructions)
                Reg_Csheet()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module