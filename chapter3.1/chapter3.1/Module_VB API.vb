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
    ''' 添加参数
    ''' </summary>
    ''' <param name="ParamName">参数名</param>
    ''' <param name="ParamValue">参数值</param>
    ''' <param name="ParamType">参数类型</param>
    Public Sub AddParam(ByVal ParamName As String, ByVal ParamValue As String， ByVal ParamType As String)
        Dim model As IpfcModel
        Dim iParamValue As IpfcParamValue
        Dim iParameterOwner As IpfcParameterOwner
        Try
            '当前打开的模型，也可以是别的model
            model = asyncConnection.Session.CurrentModel
            If model IsNot Nothing Then
                'Create iParamValue类
                If (ParamType = "浮点型") Then
                    iParamValue = (New CMpfcModelItem).CreateDoubleParamValue(Double.Parse(ParamValue))
                ElseIf (ParamType = "整形") Then
                    iParamValue = (New CMpfcModelItem).CreateIntParamValue(Int32.Parse(ParamValue))
                ElseIf (ParamType = "字符串") Then
                    iParamValue = (New CMpfcModelItem).CreateStringParamValue(ParamValue)
                ElseIf (ParamType = "布尔型") Then
                    iParamValue = (New CMpfcModelItem).CreateBoolParamValue(Boolean.Parse(ParamValue))
                Else
                    iParamValue = (New CMpfcModelItem).CreateNoteParamValue(Long.Parse(ParamValue))
                End If
                '获得IpfcParameterOwner对象，子类转父类
                iParameterOwner = CType(model, IpfcParameterOwner)
                '生成参数并返回IpfcParameter对象，应确保ParamName这个参数不存在。本例中无需对iParameter进行操作，故 iParameterOwner.CreateParam的返回值直接丢弃。
                iParameterOwner.CreateParam(ParamName, iParamValue)
                MessageBox.Show("参数已添加。")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try


    End Sub
    ''' <summary>
    ''' 修改参数
    ''' </summary>
    ''' <param name="ParamName">参数名</param>
    ''' <param name="ParamValue">参数值</param>
    ''' <param name="ParamType">参数类型</param>
    Public Sub ModParam(ByVal ParamName As String, ByVal ParamValue As String， ByVal ParamType As String)
        Dim model As IpfcModel
        Dim iParamValue As IpfcParamValue
        Dim iParameterOwner As IpfcParameterOwner
        Dim iParameter As IpfcParameter
        Try
            '当前打开的模型，也可以是别的model
            model = asyncConnection.Session.CurrentModel
            If model IsNot Nothing Then
                '获得IpfcParameterOwner对象，子类转父类
                iParameterOwner = CType(model, IpfcParameterOwner)
                '获得IpfcParameter对象，应确保ParamName这个参数确实存在
                iParameter = iParameterOwner.GetParam(ParamName)
                '获得IpfcParamValue对象,这里不需要new，直接修改原有的值
                iParamValue = iParameter.GetScaledValue
                '根据类型确定iParamValue值
                If (ParamType = "浮点型") Then
                    iParamValue.DoubleValue = Double.Parse(ParamValue)
                ElseIf (ParamType = "整形") Then
                    iParamValue.IntVale = Int32.Parse(ParamValue)
                ElseIf (ParamType = "字符串") Then
                    iParamValue.StringValue = ParamValue
                ElseIf (ParamType = "布尔型") Then
                    iParamValue.BoolValue = Boolean.Parse(ParamValue)
                Else
                    iParamValue.BoolValue = Long.Parse(ParamValue)
                End If
                iParameter.SetScaledValue(iParamValue, Nothing)
                '重生模型，vbapi 存在局限，必须要设置resolve_mode。应该根据model类型强制转化为其子类IpfcSolid再调用Regenerate方法，但是经测试直接用也可以
                model.Regenerate(Nothing)
                'CType(model, IpfcSolid).Regenerate(Nothing)
                '刷新窗口
                asyncConnection.Session.CurrentWindow.Refresh()
                MessageBox.Show("参数值已修改。")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 删除指定参数
    ''' </summary>
    ''' <param name="ParamName">参数名</param>
    Public Sub DelParam(ByVal ParamName As String)
        Dim model As IpfcModel
        Dim iParameterOwner As IpfcParameterOwner
        Dim iParameter As IpfcParameter
        Try
            '当前打开的模型，也可以是别的model
            model = asyncConnection.Session.CurrentModel
            If model IsNot Nothing Then
                '获得IpfcParameterOwner对象，子类转父类
                iParameterOwner = CType(model, IpfcParameterOwner)
                '获得IpfcParameter对象，应确保ParamName这个参数确实存在
                iParameter = iParameterOwner.GetParam(ParamName)
                '直接删除参数
                iParameter.Delete()
                MessageBox.Show("参数已删除。")

            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module
