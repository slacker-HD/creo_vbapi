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

    Public Function FeatureTreeInfo() As String
        Dim info As String
        Dim model As IpfcModel
        Dim solid As IpfcSolid
        Dim features As IpfcFeatures
        Dim modelItem As IpfcModelItem
        Dim i As Integer
        info = ""
        Try
            model = asyncConnection.Session.CurrentModel
            solid = CType(model, IpfcSolid)

            features = solid.ListFeaturesByType(False, EpfcFeatureType.EpfcFeatureType_nil)

            For i = 0 To features.Count - 1
                modelItem = CType(features.Item(i), IpfcModelItem)
                info += "序号：" + (i + 1).ToString() + "  ID:" + modelItem.Id.ToString() + "  名称：" + modelItem.GetName() + "  类型：" + features.Item(i).FeatTypeName + Chr(13)
            Next
        Catch ex As Exception
            info = ex.Message.ToString + Chr(13) + ex.StackTrace.ToString
        End Try
        Return info
    End Function

End Module
