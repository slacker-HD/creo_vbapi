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
    ''' 清空模型的关系
    ''' </summary>
    Public Sub DelRelations()
        Dim model As IpfcModel
        Try
            '当前打开的模型，也可以是别的model
            model = asyncConnection.Session.CurrentModel
            If model IsNot Nothing Then
                model.DeleteRelations()
                MessageBox.Show("关系已清空。")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' 添加关系字符串
    ''' </summary>
    ''' <param name="rel">y一行关系字符串</param>
    Public Sub AddRelations(ByVal rel As String)
        Dim model As IpfcModel
        Dim relations As New Cstringseq
        Dim i As Integer
        Try
            '当前打开的模型，也可以是别的model
            model = asyncConnection.Session.CurrentModel
            If model IsNot Nothing Then
                '读取已有的关系
                For i = 0 To model.Relations.Count - 1
                    '子类转父类后读取已有关系
                    relations.Append(CType(model, IpfcRelationOwner).Relations.Item(i))
                Next
                relations.Append(rel)
                CType(model, IpfcRelationOwner).Relations = relations
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module
