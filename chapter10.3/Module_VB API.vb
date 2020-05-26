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
    ''' 设定当前打开零件的材料
    ''' </summary>
    ''' <param name="MtlPath">材料文件所在路径</param>
    ''' <param name="MtlName">材料名称</param>
    Public Sub SetMaterial(ByVal MtlPath As String, ByVal MtlName As String)
        Dim model As IpfcModel
        Dim part As IpfcPart
        Dim material As IpfcMaterial
        Dim currentpath As String
        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                Return
            End If
            If model.Type <> EpfcModelType.EpfcMDL_PART Then
                Return
            End If

            currentpath = asyncConnection.Session.GetCurrentDirectory()
            '打开材料文件时必须将工作目录设置为材料文件所在目录
            asyncConnection.Session.ChangeDirectory(MtlPath)
            part = CType(model, IpfcPart)
            '材料文件加载到零件中
            material = part.RetrieveMaterial(MtlName)
            '设定材料
            part.CurrentMaterial = material
            model.Save()
            '工作目录改回去
            asyncConnection.Session.ChangeDirectory(currentpath)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' 获取打开零件包含的材料信息
    ''' </summary>
    ''' <returns>零件包含的材料</returns>
    Public Function GetInnerMaterial() As String
        Dim model As IpfcModel
        Dim part As IpfcPart
        Dim materials As IpfcMaterials
        Dim i As Integer
        GetInnerMaterial = ""
        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                Return ""
            End If
            If model.Type <> EpfcModelType.EpfcMDL_PART Then
                Return ""
            End If
            part = CType(model, IpfcPart)
            materials = part.ListMaterials()
            For i = 0 To materials.Count - 1
                GetInnerMaterial += materials.Item(i).Name + ","
            Next
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            Return ""
        End Try
    End Function
    ''' <summary>
    ''' 获取打开零件设定的材料信息
    ''' </summary>
    ''' <returns>零件当期设定的材料</returns>
    Public Function GetMaterial() As String
        Dim model As IpfcModel
        Dim part As IpfcPart
        Dim material As IpfcMaterial
        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                Return ""
            End If
            If model.Type <> EpfcModelType.EpfcMDL_PART Then
                Return ""
            End If
            part = CType(model, IpfcPart)
            material = part.CurrentMaterial
            GetMaterial = material.Name
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            Return ""
        End Try
    End Function
End Module