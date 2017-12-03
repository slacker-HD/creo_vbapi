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
    ''' 列出零件包含的所有特征
    ''' </summary>
    ''' <returns>特征数据</returns>
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
    ''' <summary>
    ''' 删除选中特征
    ''' </summary>
    Public Sub DeleteFeat()
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectFeats As IpfcSelection
        Dim selectedfeat As IpfcModelItem
        Dim feature As IpfcFeature
        Dim model As IpfcModel
        Dim solid As IpfcSolid
        Dim featureOperations As New CpfcFeatureOperations '应该是CpfcFeatureOperations！帮助文档有误
        Dim deleteOperation As IpfcDeleteOperation
        Dim regenInstructions As IpfcRegenInstructions
        Try
            '初始化selection选项
            selectionOptions = (New CCpfcSelectionOptions).Create("feature") '设置可选特征的类型，这里为特征对象
            selectionOptions.MaxNumSels = 1 '设置一次可选择特征的数量
            selections = asyncConnection.Session.Select(selectionOptions, Nothing)
            '确定选择了一个对象
            If selections.Count > 0 Then
                selectFeats = selections.Item(0)
                selectedfeat = selectFeats.SelItem
                '由于选择项确定为feature类型，所以这里可以安全的将父类转化为子类
                feature = CType(selectedfeat, IpfcFeature)
                '删除需要通过IpfcSolid进行，由于我们在操作过程中保证是打开了Prt，所以这里可以安全的将父类转化为子类（asm也是一样的处理）
                model = asyncConnection.Session.CurrentModel
                solid = CType(model, IpfcSolid)
                '生成删除选项
                deleteOperation = feature.CreateDeleteOp()
                deleteOperation.Clip = True '是否删除该特征后的所有选项，本例设置为真。其余的删除选项请查看帮助文档。
                featureOperations.Append(deleteOperation)
                '生产删除操作的重生选项
                regenInstructions = (New CCpfcRegenInstructions).Create(True, True, True)
                regenInstructions.UpdateInstances = False '是否更新内存。其余的选项请查看帮助文档。
                solid.ExecuteFeatureOps（featureOperations, regenInstructions） 'regenInstructions是可选选项，也可以直接设置为Nothing
            End If
            '使用函数刷新，也很简单
            asyncConnection.Session.CurrentWindow.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 隐含选中特征
    ''' </summary>
    Public Sub SuppressFeat()
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectFeats As IpfcSelection
        Dim selectedfeat As IpfcModelItem
        Dim feature As IpfcFeature
        Dim model As IpfcModel
        Dim solid As IpfcSolid
        Dim featureOperations As New CpfcFeatureOperations '应该是CpfcFeatureOperations！帮助文档有误
        Dim suppressOperation As IpfcSuppressOperation

        Try
            '初始化selection选项
            selectionOptions = (New CCpfcSelectionOptions).Create("feature") '设置可选特征的类型，这里为特征对象
            selectionOptions.MaxNumSels = 1 '设置一次可选择特征的数量
            selections = asyncConnection.Session.Select(selectionOptions, Nothing)
            '确定选择了一个对象
            If selections.Count > 0 Then
                selectFeats = selections.Item(0)
                selectedfeat = selectFeats.SelItem
                '由于选择项确定为feature类型，所以这里可以安全的将父类转化为子类
                feature = CType(selectedfeat, IpfcFeature)
                '隐含需要通过IpfcSolid进行，由于我们在操作过程中保证是打开了Prt，所以这里可以安全的将父类转化为子类（asm也是一样的处理）
                model = asyncConnection.Session.CurrentModel
                solid = CType(model, IpfcSolid)
                '生成隐含选项
                suppressOperation = feature.CreateSuppressOp()
                suppressOperation.Clip = True '是否隐含该特征后的所有选项，本例设置为真。其余的隐含选项请查看帮助文档。
                featureOperations.Append(suppressOperation)

                solid.ExecuteFeatureOps（featureOperations, Nothing） '也可以直接设置为Nothing使用默认选项
            End If
            '使用函数刷新，也很简单
            asyncConnection.Session.CurrentWindow.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 导入一个step特征
    ''' </summary>
    ''' <param name="stepfile">step文件路径</param>
    Public Sub CreateImportFeatureFromDataFile(ByVal stepfile As String)
        Dim datasource As IpfcIntfStep
        Dim featattr As IpfcImportFeatAttr
        Dim model As IpfcModel
        Dim solid As IpfcSolid
        Dim coordsystem As IpfcCoordSystem
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectCoordsystems As IpfcSelection
        Dim selectedcoordsystem As IpfcModelItem
        Try
            model = asyncConnection.Session.CurrentModel
            solid = CType(model, IpfcSolid)
            '初始化selection选项
            selectionOptions = (New CCpfcSelectionOptions).Create("csys") '设置可选特征的类型，这里为坐标系对象
            selectionOptions.MaxNumSels = 1 '设置一次可选择特征的数量
            selections = asyncConnection.Session.Select(selectionOptions, Nothing)
            '确定选择了一个对象
            If selections.Count > 0 Then
                selectCoordsystems = selections.Item(0)
                selectedcoordsystem = selectCoordsystems.SelItem
                '设置插入的step对象所在坐标系
                coordsystem = CType(selectedcoordsystem, IpfcCoordSystem)
                '初始化插入的step对象
                datasource = (New CCpfcIntfStep).Create(stepfile)
                '设置特征的属性
                featattr = (New CCpfcImportFeatAttr).Create()
                featattr.JoinSurfs = True
                featattr.MakeSolid = True
                featattr.Operation = EpfcOperationType.EpfcADD_OPERATION
                '插入特征
                solid.CreateImportFeat(datasource, coordsystem, featattr)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module
