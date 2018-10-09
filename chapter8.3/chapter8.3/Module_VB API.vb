Imports System.Configuration
Imports pfcls

Module Module_vbapi
    Public asyncConnection As IpfcAsyncConnection = Nothing '全局变量，用于存储连接会话的句柄
    Private toolkitdll As IpfcDll = Nothing 'toolkit程序

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
    ''' 加载Dll
    ''' </summary>
    Public Sub LoadToolkitDll()
        Try
            toolkitdll = CType(asyncConnection.Session, IpfcBaseSession).LoadProToolkitDll(TKDLLName, DllPath, TextPath, True)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)

        End Try
    End Sub
    ''' <summary>
    ''' 调用Dll里的函数
    ''' </summary>
    ''' <param name="input">输入的数字</param>
    Public Sub ExecuteFunction(ByVal input As Integer)
        Dim arguments As New CpfcArguments
        Dim argument As IpfcArgument
        Dim value As New CpfcArgValue
        Dim ret As IpfcFunctionReturn
        Try
            value = (New CMpfcArgument).CreateIntArgValue(input)
            argument = (New CCpfcArgument).Create("inputvalue", value)
            arguments.Append(argument)
            ret = toolkitdll.ExecuteFunction("MyPow", arguments)
            MsgBox("函数返回错误代码：" + ret.FunctionReturn.ToString)
            If ret.FunctionReturn = 0 Then
                MsgBox("函数返回值：" + ret.OutputArguments(0).Value.IntValue.ToString())
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module