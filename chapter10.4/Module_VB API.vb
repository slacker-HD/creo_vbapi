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
    ''' 导出打开的模型到jpg
    ''' </summary>
    ''' <param name="FilePath">导出jpg文件路径</param>
    Public Sub ExporttoJpg(ByVal FilePath As String)
        Dim currentwindow As IpfcWindow
        Dim jpegoption As IpfcJPEGImageExportInstructions
        Try
            If IsPrtorAsm() = True Then
                currentwindow = CType(asyncConnection.Session, IpfcBaseSession).CurrentWindow
                '设置导出jpg文件的宽度和高度
                jpegoption = (New CCpfcJPEGImageExportInstructions).Create(currentwindow.GraphicsAreaWidth * 10, currentwindow.GraphicsAreaHeight * 10)
                '设置导出jpg文件的dpi
                jpegoption.DotsPerInch = EpfcDotsPerInch.EpfcRASTERDPI_600
                '设置导出jpg文件的像素
                jpegoption.ImageDepth = EpfcRasterDepth.EpfcRASTERDEPTH_24
                '调整为默认视图
                CType(CType(asyncConnection.Session, IpfcBaseSession).CurrentModel, IpfcViewOwner).GetCurrentView().Reset()
                Reset()
                '显示线框、基准面等，和toolkit一样，这个0、1运行多次效果是切换而不是设定指定值
                asyncConnection.Session.RunMacro("IMICLEARITEM ~ Command `ProCmdEnvDtmDisp` 1; ~ Command `ProCmdEnvAxisDisp` 1; ~ Command `ProCmdViewSpinCntr` 1; ~ Command `ProCmdEnvPntsDisp`  1;~ Command `ProCmdEnvCsysDisp`  1;")
                currentwindow.Refresh()
                currentwindow.Repaint()
                '不显示线框、基准面等，和toolkit一样，这个0、1运行多次效果是切换而不是设定指定值
                asyncConnection.Session.RunMacro("IMICLEARITEM ~ Command `ProCmdEnvDtmDisp` 0; ~ Command `ProCmdEnvAxisDisp` 0; ~ Command `ProCmdViewSpinCntr` 0; ~ Command `ProCmdEnvPntsDisp`  0;~ Command `ProCmdEnvCsysDisp`  0;")
                currentwindow.Refresh()
                currentwindow.Repaint()
                '根据要求导出jpg
                currentwindow.ExportRasterImage(FilePath, jpegoption)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module