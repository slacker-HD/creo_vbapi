Imports System.Configuration
Imports pfcls

Module Module_vbapi
    Public asyncConnection As IpfcAsyncConnection = Nothing '全局变量，用于存储连接会话的句柄
    Public Enum ExportType
        jpg = 1
        bmp = 2
        tif = 3
        eps = 4
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
    Public Sub ExporttoImg(ByVal FilePath As String, ByVal Type As ExportType)
        Dim currentwindow As IpfcWindow
        Dim jpegoption As IpfcJPEGImageExportInstructions
        Dim bmpoption As IpfcBitmapImageExportInstructions
        Dim tifoption As IpfcTIFFImageExportInstructions
        Dim epsoption As IpfcEPSImageExportInstructions
        Try
            If IsPrtorAsm() = True Then
                currentwindow = CType(asyncConnection.Session, IpfcBaseSession).CurrentWindow
                '调整为默认视图
                CType(CType(asyncConnection.Session, IpfcBaseSession).CurrentModel, IpfcViewOwner).GetCurrentView().Reset()
                Reset()
                Select Case Type
                    Case ExportType.jpg
                        '设置导出文件的宽度和高度
                        jpegoption = (New CCpfcJPEGImageExportInstructions).Create(currentwindow.GraphicsAreaWidth * 10, currentwindow.GraphicsAreaHeight * 10)
                        '设置导出jpg文件的dpi
                        jpegoption.DotsPerInch = EpfcDotsPerInch.EpfcRASTERDPI_600
                        '设置导出jpg文件的像素
                        jpegoption.ImageDepth = EpfcRasterDepth.EpfcRASTERDEPTH_24
                        currentwindow.ExportRasterImage(FilePath, jpegoption)
                    Case ExportType.bmp
                        '设置导出文件的宽度和高度
                        bmpoption = (New CCpfcBitmapImageExportInstructions).Create(currentwindow.GraphicsAreaWidth * 10, currentwindow.GraphicsAreaHeight * 10)
                        '设置导出jpg文件的dpi
                        bmpoption.DotsPerInch = EpfcDotsPerInch.EpfcRASTERDPI_600
                        '设置导出jpg文件的像素
                        bmpoption.ImageDepth = EpfcRasterDepth.EpfcRASTERDEPTH_24
                        currentwindow.ExportRasterImage(FilePath, bmpoption)
                    Case ExportType.tif
                        '设置导出文件的宽度和高度
                        tifoption = (New CCpfcTIFFImageExportInstructions).Create(currentwindow.GraphicsAreaWidth * 10, currentwindow.GraphicsAreaHeight * 10)
                        '设置导出jpg文件的dpi
                        tifoption.DotsPerInch = EpfcDotsPerInch.EpfcRASTERDPI_600
                        '设置导出jpg文件的像素
                        tifoption.ImageDepth = EpfcRasterDepth.EpfcRASTERDEPTH_24
                        currentwindow.ExportRasterImage(FilePath, tifoption)
                    Case ExportType.eps
                        '设置导出文件的宽度和高度
                        epsoption = (New CCpfcEPSImageExportInstructions).Create(currentwindow.GraphicsAreaWidth * 10, currentwindow.GraphicsAreaHeight * 10)
                        '设置导出jpg文件的dpi
                        epsoption.DotsPerInch = EpfcDotsPerInch.EpfcRASTERDPI_600
                        '设置导出jpg文件的像素
                        epsoption.ImageDepth = EpfcRasterDepth.EpfcRASTERDEPTH_24
                        currentwindow.ExportRasterImage(FilePath, epsoption)
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module