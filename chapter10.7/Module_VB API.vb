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
    ''' 判断是否为打开的是否为零件或装配体
    ''' </summary>
    ''' <returns>是否为零件或装配体</returns>
    Private Function IsPrtorAsm() As Boolean
        Try
            If asyncConnection.Session.CurrentModel Is Nothing Then
                IsPrtorAsm = False
            ElseIf (asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_ASSEMBLY Or asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_PART) Then
                IsPrtorAsm = True
            Else
                IsPrtorAsm = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            IsPrtorAsm = False
        End Try
    End Function
    ''' <summary>
    ''' 计算默认坐标系下零件的outline
    ''' </summary>
    ''' <returns></returns>
    Public Function CurrentOutline() As Double()
        Dim solid As IpfcSolid
        Dim outline(3) As Double
        Dim x1, x2, y1, y2, z1, z2 As Double

        Try
            If IsPrtorAsm() Then
                solid = CType(asyncConnection.Session.CurrentModel, IpfcSolid)
                x2 = solid.GeomOutline.Item(1).Item(0)
                x1 = solid.GeomOutline.Item(0).Item(0)
                y2 = solid.GeomOutline.Item(1).Item(1)
                y1 = solid.GeomOutline.Item(0).Item(1)
                z2 = solid.GeomOutline.Item(1).Item(2)
                z1 = solid.GeomOutline.Item(0).Item(2)
                outline(0) = Math.Abs(x2 - x1)
                outline(1) = Math.Abs(y2 - y1)
                outline(2) = Math.Abs(z2 - z1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
        Return outline
    End Function
    ''' <summary>
    ''' 计算指定坐标系下零件的outline
    ''' </summary>
    ''' <returns></returns>
    Public Function CurrentOutlineCustom() As Double()
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim coord As IpfcCoordSystem
        Try
            MessageBox.Show("请在模型图中选择一个坐标系用以计算。")
            selectionOptions = (New CCpfcSelectionOptions).Create("csys")
            selectionOptions.MaxNumSels = 1
            selections = asyncConnection.Session.Select(selectionOptions, Nothing)
            If selections.Count > 0 Then
                coord = CType(selections.Item(0).SelItem, IpfcCoordSystem)
                Return _CurrentOutlineCustom(coord.CoordSys)
            End If
        Catch
            Return Nothing
        End Try
        Return Nothing
    End Function
    ''' <summary>
    ''' 计算指定坐标系下零件的outline
    ''' </summary>
    ''' <returns>outline</returns>
    Private Function _CurrentOutlineCustom(ByVal trf As IpfcTransform3D) As Double()
        Dim solid As IpfcSolid
        Dim outline(3) As Double
        Dim outline3d As IpfcOutline3D
        Dim excludeTypes As IpfcModelItemTypes

        excludeTypes = New CpfcModelItemTypes
        excludeTypes.Append(EpfcModelItemType.EpfcITEM_AXIS)
        excludeTypes.Append(EpfcModelItemType.EpfcITEM_COORD_SYS)
        Try
            If IsPrtorAsm() Then
                solid = CType(asyncConnection.Session.CurrentModel, IpfcSolid)
                outline3d = solid.EvalOutline(trf, excludeTypes)
                outline(0) = Math.Abs(outline3d.Item(1).Item(0) - outline3d.Item(0).Item(0))
                outline(1) = Math.Abs(outline3d.Item(1).Item(1) - outline3d.Item(0).Item(1))
                outline(2) = Math.Abs(outline3d.Item(1).Item(2) - outline3d.Item(0).Item(2))
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
        Return outline
    End Function
End Module