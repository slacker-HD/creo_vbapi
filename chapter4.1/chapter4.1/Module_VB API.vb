Imports pfcls
Imports System.Configuration

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
    ''' 转换当前打开文件为dwg,文件保存在Creo工作目录下同名Dwg文件。
    ''' </summary>
    Public Sub ConvertToDwg(）
        Dim model As IpfcModel

        Dim dwginstructions As IpfcDWG3DExportInstructions
        Try
            model = asyncConnection.Session.CurrentModel

            dwginstructions = (New CCpfcDWG3DExportInstructions()).Create()
            model.Export(model.InstanceName + ".dwg", dwginstructions)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' 转换当前打开文件为pdf,文件保存在Creo工作目录下同名pdf文件。
    ''' </summary>
    Public Sub ConvertToPdf()
        Dim model As IpfcModel
        Dim pdfinstructions As IpfcPDFExportInstructions
        Try
            model = asyncConnection.Session.CurrentModel

            pdfinstructions = (New CCpfcPDFExportInstructions()).Create()
            model.Export(model.InstanceName + ".pdf", pdfinstructions)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' 转换当前打开文件为stp,文件保存在Creo工作目录下同名stp文件。
    ''' </summary>
    Public Sub ConvertToStp()
        Dim model As IpfcModel
        Dim stepinstructions As IpfcSTEP3DExportInstructions
        Dim flags As IpfcGeometryFlags

        Try
            model = asyncConnection.Session.CurrentModel

            flags = (New CCpfcGeometryFlags()).Create()
            flags.AsSolids = True

            stepinstructions = (New CCpfcSTEP3DExportInstructions()).Create(EpfcAssemblyConfiguration.EpfcEXPORT_ASM_MULTI_FILES, flags)
            model.Export(model.InstanceName + ".pdf", stepinstructions)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 转换当前打开文件为Igs,文件保存在Creo工作目录下同名igs文件。
    ''' </summary>
    Public Sub ConvertToIgs()
        Dim model As IpfcModel
        Dim geometryFlags As IpfcGeometryFlags
        Dim igsinstructions As IpfcIGES3DNewExportInstructions

        Try
            model = asyncConnection.Session.CurrentModel


            geometryFlags = (New CCpfcGeometryFlags).Create()
            geometryFlags.AsSolids = True '导出为Solid选项

            '第一个参数应该是EpfcAssemblyConfiguration，帮助文档有误；
            igsinstructions = (New CCpfcIGES3DNewExportInstructions).Create(EpfcAssemblyConfiguration.EpfcEXPORT_ASM_SINGLE_FILE, geometryFlags)
            model.Export(model.InstanceName + ".igs", igsinstructions)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

End Module
