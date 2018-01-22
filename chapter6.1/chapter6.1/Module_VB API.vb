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

    Public Sub InsertComp()
        Dim model As IpfcModel
        Dim assembly As IpfcAssembly
        Dim modelDesc As IpfcModelDescriptor
        Dim componentModel As IpfcSolid
        Dim fileOpenopts As IpfcFileOpenOptions
        Dim filename As String
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim matrix As New CpfcMatrix3D
        Dim transform3D As IpfcTransform3D
        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                MessageBox.Show("请打开一个装配体！")
                Return
            End If
            If model.Type = EpfcModelType.EpfcMDL_ASSEMBLY Then
                '使用ccpfc类初始化ipfc类，生成creo打开文件的对话框的选项
                fileOpenopts = (New CCpfcFileOpenOptions).Create("*.prt")
                '如果点击取消按钮，会throw一个"pfcExceptions::XToolkitUserAbort" Exception，被下面的catch捕捉
                filename = asyncConnection.Session.UIOpenFile(fileOpenopts)
                modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, Nothing, Nothing)
                modelDesc.Path = filename
                '使用ccpfc类初始化ipfc类，生成IpfcRetrieveModelOptions
                retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create
                retrieveModelOptions.AskUserAboutReps = False
                '加载零件
                componentModel = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
                assembly = CType(model, IpfcAssembly)
                '初始化位姿矩阵，这是默认位置,可以根据位姿矩阵定义自己修改零件初始化插入位置
                For i = 0 To 3
                    For j = 0 To 3
                        If i = j Then
                            matrix.Set(i, j, 1.0)
                        Else
                            matrix.Set(i, j, 0.0)
                        End If
                    Next
                Next
                transform3D = (New CCpfcTransform3D).Create(matrix)
                assembly.AssembleComponent(componentModel, transform3D)
            End If
            asyncConnection.Session.CurrentWindow.Refresh()
        Catch ex As Exception
            If ex.Message <> "pfcExceptions::XToolkitUserAbort" Then
                MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 添加一个MATE_OFF约束，默认OFFSET为100
    ''' </summary>
    Public Sub SetConstrainOffSet()
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectFeats As IpfcSelection
        Dim selectedComponent As IpfcModelItem
        Dim componentFeat As IpfcComponentFeat
        Dim compConstraints As New CpfcComponentConstraints
        Dim compConstraint As IpfcComponentConstraint
        Dim asmReference As IpfcSelection
        Dim compReference As IpfcSelection
        Dim offset As Double = 100 '默认值OFFSET为100，为简单起见，实际应该作为函数的参数
        Try
            '初始化selection选项
            selectionOptions = (New CCpfcSelectionOptions).Create("component") '设置可选特征的类型，这里为特征对象
            selectionOptions.MaxNumSels = 1 '设置一次可选择特征的数量
            selections = asyncConnection.Session.Select(selectionOptions, Nothing)
            '第一步，选择一个零件，确保零件没有约束或者添加的约束不会冲突
            If selections.Count > 0 Then
                selectFeats = selections.Item(0)
                selectedComponent = selectFeats.SelItem
                componentFeat = CType(selectedComponent, IpfcComponentFeat)
                '第二步，选择装配体中其余零件的表面
                selectionOptions = (New CCpfcSelectionOptions).Create("surface")
                selectionOptions.MaxNumSels = 1
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                If selections.Count > 0 Then
                    asmReference = selections.Item(0)
                Else
                    MessageBox.Show("请选择装配体中其余零件的表面！")
                    Return
                End If
                '第三步，选择选中零件的表面
                selectionOptions = (New CCpfcSelectionOptions).Create("surface")
                selectionOptions.MaxNumSels = 1
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                If selections.Count > 0 Then
                    compReference = selections.Item(0)
                Else
                    MessageBox.Show("请选择当前零件的表面！")
                    Return
                End If
                '以上两个选择根据约束的要求可以更换不同的filter获取
                '为简单起见认为是EpfcComponentConstraintType.EpfcASM_CONSTRAINT_MATE_OFF，其实应该作为函数参数传入
                compConstraint = (New CCpfcComponentConstraint).Create(EpfcComponentConstraintType.EpfcASM_CONSTRAINT_MATE_OFF)
                compConstraint.AssemblyReference = asmReference
                compConstraint.ComponentReference = compReference
                compConstraint.Offset = offset
                '完成compConstraint的设置，增加到compConstraints
                compConstraints.Append(compConstraint)
                '实际应该将compConstraints与componentFeat.GetConstraints读到的约束合并，这里仅为做示例默认去除了原有的约束
                componentFeat.SetConstraints(compConstraints, Nothing)
            End If
            '使用函数刷新，也很简单
            asyncConnection.Session.CurrentWindow.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module
