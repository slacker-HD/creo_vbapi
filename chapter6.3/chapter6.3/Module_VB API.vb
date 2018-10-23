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
    ''' 获取装配树结构
    ''' </summary>
    ''' <returns>返回装配树结构描述字符串</returns>
    Public Function ComponentFeatTreeInfo() As String
        Dim model As IpfcModel
        Dim solid As IpfcSolid
        Dim components As IpfcFeatures
        Dim modelItem As IpfcModelItem
        Dim componentFeat As IpfcComponentFeat
        Dim i As Integer = 0
        Dim info As String = ""
        Try
            model = asyncConnection.Session.CurrentModel
            solid = CType(model, IpfcSolid)
            components = solid.ListFeaturesByType(True, EpfcFeatureType.EpfcFEATTYPE_COMPONENT)
            For Each component As IpfcFeature In components
                modelItem = CType(component, IpfcModelItem)
                componentFeat = CType(component, IpfcComponentFeat)
                '如5.1所述，getname是基本无效的，还好可以通过model的instantname实现
                info += "序号：" + (i + 1).ToString() + "  ID:" + modelItem.Id.ToString() + "  名称：" + componentFeat.ModelDescr.InstanceName + "  类型：" + componentFeat.ModelDescr.GetExtension()
                info += "(" + Constrains(componentFeat) + ")" + Chr(13)
                i = i + 1
                If componentFeat.ModelDescr.Type = EpfcModelType.EpfcMDL_ASSEMBLY Then
                    info += GetSubassemblyinfo(i, componentFeat.ModelDescr, 0)
                End If
            Next
        Catch ex As Exception
            info = ex.Message.ToString + Chr(13) + ex.StackTrace.ToString
        End Try
        Return info
    End Function

    ''' <summary>
    ''' 递归获取子装配体的信息
    ''' </summary>
    ''' <param name="i">序号，byref传递保证序号对应</param>
    ''' <param name="ModelDescr">模型描述</param>
    ''' <param name="level">装配树level</param>
    ''' <returns>子装配体树结构</returns>
    Private Function GetSubassemblyinfo(ByRef i As Integer, ByVal ModelDescr As IpfcModelDescriptor, ByVal level As Integer) As String
        Dim model As IpfcModel
        Dim solid As IpfcSolid
        Dim components As IpfcFeatures
        Dim info As String = ""
        Dim j As Integer
        Dim tabstr As String = ""
        Dim modelItem As IpfcModelItem
        Dim componentFeat As IpfcComponentFeat
        model = asyncConnection.Session.GetModelFromDescr(ModelDescr)
        solid = CType(model, IpfcSolid)
        components = solid.ListFeaturesByType(True, EpfcFeatureType.EpfcFEATTYPE_COMPONENT)
        For j = 0 To level
            tabstr += vbTab
        Next
        For Each component As IpfcFeature In components
            modelItem = CType(component, IpfcModelItem)
            componentFeat = CType(component, IpfcComponentFeat)
            info += tabstr + "序号：" + (i + 1).ToString() + "  ID:" + modelItem.Id.ToString() + "  名称：" + componentFeat.ModelDescr.InstanceName + "  类型：" + componentFeat.ModelDescr.GetExtension()
            info += "(" + Constrains(componentFeat) + ")" + Chr(13)
            i = i + 1
            If componentFeat.ModelDescr.Type = EpfcModelType.EpfcMDL_ASSEMBLY Then
                info += GetSubassemblyinfo(i, componentFeat.ModelDescr, level + 1)
            End If
        Next
        Return info
    End Function
    ''' <summary>
    ''' 获取子装配体的装配约束
    ''' </summary>
    ''' <returns></returns>
    Private Function Constrains(ByVal Component As IpfcComponentFeat) As String
        Constrains = ""
        If Component.GetConstraints() IsNot Nothing Then
            For Each constrain As IpfcComponentConstraint In Component.GetConstraints()
                Constrains += "约束类型：" + constrain.Type.ToString() + ", "
            Next
        End If
        Return Constrains
    End Function
End Module