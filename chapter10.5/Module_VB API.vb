Imports System.Configuration
Imports System.IO
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
    ''' 判断是否为打开的是否为零件
    ''' </summary>
    ''' <returns>是否为零件</returns>
    Private Function IsPrt() As Boolean
        Try
            If asyncConnection.Session.CurrentModel Is Nothing Then
                IsPrt = False
            ElseIf asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_PART Then
                IsPrt = True
            Else
                IsPrt = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            IsPrt = False
        End Try
    End Function
    ''' <summary>
    ''' 修改当前打开零件名称，如果有零件目录下存在同名绘图文件，也一并修改
    ''' </summary>
    ''' <param name="NewName">新名称</param>
    Public Sub RenamePrtandDrw(NewName As String)
        Dim oldname As String
        Dim model, drw As IpfcModel
        Dim currentpath As String = ""
        Dim modelDesc As IpfcModelDescriptor
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Try
            If IsPrt() Then
                currentpath = asyncConnection.Session.GetCurrentDirectory()
                model = asyncConnection.Session.CurrentModel
                oldname = model.InstanceName
                '如果前后名字一样会出ToolkitGeneralError错
                If (oldname <> NewName.ToUpper) Then
                    '打开同名图纸的准备
                    asyncConnection.Session.ChangeDirectory(Path.GetDirectoryName(model.Origin))
                    modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_DRAWING, Nothing, Nothing)
                    modelDesc.Path = Path.GetDirectoryName(model.Origin) + "\" + oldname
                    retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create
                    retrieveModelOptions.AskUserAboutReps = False
                    Try
                        '必须打开drw先保存
                        drw = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
                        drw.Rename(NewName, True)
                        drw.Save()
                    Catch ex As Exception
                        If ex.Message = "pfcExceptions::XToolkitNotFound" Then
                            MsgBox("未发现同名工程图")
                        Else
                            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
                        End If
                    End Try
                    '再保存prt
                    model.Rename(NewName, True)
                    model.Save()

                    asyncConnection.Session.ChangeDirectory(currentpath)
                Else
                    MsgBox("当前打开模型名称已是test，无需修改")
                End If
            Else
                MsgBox("请打开一个零件进行操作。")
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            asyncConnection.Session.ChangeDirectory(currentpath)
        End Try
    End Sub
End Module