Imports System.Configuration
Imports pfcls

Module Module_vbapi
    Public asyncConnection As IpfcAsyncConnection = Nothing '全局变量，用于存储连接会话的句柄
    Dim model As IpfcModel
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
    ''' 修改尺寸文字
    ''' </summary>
    ''' <param name="Prefix">要添加的前缀</param>
    ''' <param name="Surffix">要添加的后缀</param>
    ''' <param name="DownText">要添加的尺寸线下方文字</param>
    Public Sub Modify_text(ByVal Prefix As String, ByVal Surffix As String, ByVal DownText As String)
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectDim As IpfcSelection
        Dim bdimesion As IpfcBaseDimension
        Dim TextStrs As Istringseq
        Try
            If Isdrawding() = True Then
                selectionOptions = (New CCpfcSelectionOptions).Create("dimension")
                selectionOptions.MaxNumSels = 1
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                If selections Is Nothing Then
                    Exit Sub
                End If
                If selections.Count < 1 Then
                    Throw New Exception("请选择一个尺寸元素！")
                End If
                '获取文字对象
                selectDim = selections.Item(0)
                bdimesion = selectDim.SelItem
                TextStrs = bdimesion.Texts
                '修改前后缀，只要修改Item（0）即可
                TextStrs.Set(0, Prefix + bdimesion.Texts.Item(0) + Surffix)
                '修改尺寸线下方文字，如果Item（0）存在则直接修改值，不存在添加一个
                If DownText <> "" Then
                    If TextStrs.Count > 1 Then
                        TextStrs.Set(1, DownText)
                    Else
                        TextStrs.Insert(1, DownText)
                    End If
                End If
                '直接设定
                bdimesion.Texts = TextStrs
                Reg_Csheet()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' 重生Drawing
    ''' </summary>
    Private Sub Reg_Csheet()
        Dim drawing As IpfcDrawing
        If Isdrawding() = True Then
            drawing = CType(model, IpfcDrawing)
            drawing.RegenerateSheet(drawing.CurrentSheetNumber)
        End If
    End Sub
    ''' <summary>
    ''' 判断是否为打开的是否为工程图
    ''' </summary>
    ''' <returns>是否为工程图</returns>
    Private Function Isdrawding() As Boolean
        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                Isdrawding = False
            ElseIf (model.Type = EpfcModelType.EpfcMDL_DRAWING) Then
                Isdrawding = True
            Else
                Isdrawding = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            Isdrawding = False
        End Try
    End Function
End Module