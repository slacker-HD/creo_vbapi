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
    ''' 加载一个DrawingFormat
    ''' </summary>
    Public Function GetDrawingFormat() As IpfcDrawingFormat
        Dim modelDesc As IpfcModelDescriptor
        Dim fileOpenopts As IpfcFileOpenOptions
        Dim filename As String
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim sheetFormat As IpfcDrawingFormat
        Try
            fileOpenopts = (New CCpfcFileOpenOptions).Create("*.frm")
            '如果点击取消按钮，会throw一个"pfcExceptions::XToolkitUserAbort" Exception，被下面的catch捕捉
            filename = asyncConnection.Session.UIOpenFile(fileOpenopts)
            modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_DWG_FORMAT, Nothing, Nothing)
            modelDesc.Path = filename
            retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create
            retrieveModelOptions.AskUserAboutReps = False
            sheetFormat = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
            Return sheetFormat
        Catch ex As Exception
            If ex.Message <> "pfcExceptions::XToolkitUserAbort" Then
                MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            End If
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 设定图幅
    ''' </summary>
    Public Sub SetFormat()
        Dim model As IpfcModel
        Dim sheetOwner As IpfcSheetOwner
        Dim drawingFormat As IpfcDrawingFormat
        Try
            If Isdrawding() Then
                model = asyncConnection.Session.CurrentModel
                sheetOwner = CType(model, IpfcSheetOwner)
                drawingFormat = GetDrawingFormat()
                If drawingFormat IsNot Nothing Then
                    sheetOwner.SetSheetFormat(model.CurrentSheetNumber, drawingFormat, Nothing, Nothing)
                    Reg_Csheet()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module