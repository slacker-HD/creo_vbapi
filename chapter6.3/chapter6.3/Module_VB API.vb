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

    Public Function GetGlobalInterferences() As String
        Dim asm As IpfcModel
        Dim globalEvaluator As IpfcGlobalEvaluator
        Dim globalInterferences As IpfcGlobalInterferences
        Dim selParts As IpfcSelectionPair
        Dim sel1, sel2 As IpfcSelection
        Dim selItem1, selItem2 As IpfcModel
        Dim volume As IpfcInterferenceVolume
        Dim ret As String = ""
        Try
            asm = asyncConnection.Session.CurrentModel
            If asm.Type = EpfcModelType.EpfcMDL_ASSEMBLY Then

                globalEvaluator = (New CMpfcInterference).CreateGlobalEvaluator(CType(asm, IpfcAssembly))
                globalInterferences = globalEvaluator.ComputeGlobalInterference(True)

                If Not (globalInterferences Is Nothing) Then
                    For Each interference As IpfcGlobalInterference In globalInterferences
                        selParts = interference.SelParts
                        sel1 = selParts.Sel1
                        sel2 = selParts.Sel2
                        selItem1 = sel1.SelModel
                        selItem2 = sel2.SelModel
                        volume = interference.Volume
                        ret = ret + selItem1.InstanceName + "和" + selItem2.InstanceName + "发生干涉，干涉量为：" + volume.ComputeVolume.ToString() + Chr(13)
                    Next
                Else
                    ret = asm.InstanceName + "未发生干涉."
                End If
            End If
        Catch ex As Exception
            ret = ex.Message.ToString + Chr(13) + ex.StackTrace.ToString
        End Try
        Return ret
    End Function


    Public Function GetSelInterferences() As String
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectionspair As IpfcSelectionPair
        Dim selectionEvaluator As IpfcSelectionEvaluator
        Dim asm As IpfcModel
        Dim interferenceVolume As IpfcInterferenceVolume

        Dim ret As String = ""
        Try
            asm = asyncConnection.Session.CurrentModel
            If asm.Type = EpfcModelType.EpfcMDL_ASSEMBLY Then
                '初始化selection选项
                selectionOptions = (New CCpfcSelectionOptions).Create("part") '设置可选特征的类型，这里为零件
                selectionOptions.MaxNumSels = 2 '设置一次可选择特征的数量，这里判断两个零件的干涉，所以为2
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                '确定选择了两个对象
                If selections.Count = 2 Then
                    selectionspair = (New CCpfcSelectionPair).Create(selections.Item(0), selections.Item(1))
                    selectionEvaluator = (New CMpfcInterference).CreateSelectionEvaluator(selectionspair)
                    interferenceVolume = selectionEvaluator.ComputeInterference(True)
                    ret = "干涉量为：" + interferenceVolume.ComputeVolume().ToString() + Chr(13)
                Else
                    ret = "用户未完成选择！"
                End If
            End If
            '使用函数刷新，也很简单
            asyncConnection.Session.CurrentWindow.Refresh()
        Catch ex As Exception
            ret = ex.Message.ToString + Chr(13) + ex.StackTrace.ToString
        End Try
        Return ret
    End Function


End Module