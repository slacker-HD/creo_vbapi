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
    ''' 判断是否为打开的是否为零件或装配体
    ''' </summary>
    ''' <returns>是否为零件或装配体</returns>
    Private Function IsPrtorAsm() As Boolean
        Try
            If asyncConnection.Session.CurrentModel Is Nothing Then
                IsPrtorAsm = False
            ElseIf (asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_ASSEMBLY Or asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_PART) Then
                IsPrtorAsm = True
            Else
                IsPrtorAsm = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            IsPrtorAsm = False
        End Try
    End Function
    ''' <summary>
    ''' 根据指定轴和角度旋转当前视图
    ''' </summary>
    ''' <param name="Axis">旋转轴</param>
    ''' <param name="Angle">角度</param>
    Public Sub RotateView(ByVal Axis As EpfcCoordAxis, ByVal Angle As Double)
        Dim model As IpfcModel
        Try
            If IsPrtorAsm() Then
                model = asyncConnection.Session.CurrentModel
                '旋转角度
                CType(model, IpfcViewOwner).GetCurrentView.Rotate(Axis, Angle)
                asyncConnection.Session.CurrentWindow.Refresh()
                asyncConnection.Session.CurrentWindow.Repaint()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' 根据指定位姿矩阵设定视图方向
    ''' </summary>
    ''' <param name="TransForm">位姿矩阵信息</param>
    Private Sub RotateViewByMatrix(ByVal TransForm As IpfcTransform3D)
        Dim model As IpfcModel
        Try
            If IsPrtorAsm() Then
                model = asyncConnection.Session.CurrentModel
                '设定矩阵
                CType(model, IpfcViewOwner).GetCurrentView.Transform = TransForm
                asyncConnection.Session.CurrentWindow.Refresh()
                asyncConnection.Session.CurrentWindow.Repaint()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' 设置FRONT视图
    ''' </summary>
    Public Sub ChangeToFrontView()
        Dim transform As IpfcTransform3D
        transform = (New CCpfcTransform3D).Create(Nothing)
        For i = 0 To 3
            For j = 0 To 3
                transform.Matrix.Set(i, j, 0)
            Next
        Next
        transform.Matrix.Set(3, 3, 1)
        transform.Matrix.Set(0, 0, 1)
        transform.Matrix.Set(1, 1, 1)
        transform.Matrix.Set(2, 2, 1)

        RotateViewByMatrix(transform)
    End Sub
End Module