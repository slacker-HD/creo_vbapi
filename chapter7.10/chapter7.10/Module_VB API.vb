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
    ''' 重生Drawing
    ''' </summary>
    Private Sub Reg_Csheet()
        Dim drawing As IpfcDrawing
        drawing = asyncConnection.Session.CurrentModel
        If Isdrawding() = True Then
            drawing = CType(drawing, IpfcDrawing)
            drawing.RegenerateSheet(drawing.CurrentSheetNumber)
        End If
    End Sub

    ''' <summary>
    ''' 判断是否为打开的是否为工程图
    ''' </summary>
    ''' <returns>是否为工程图</returns>
    Private Function Isdrawding() As Boolean
        Try
            If asyncConnection.Session.CurrentModel Is Nothing Then
                Isdrawding = False
            ElseIf (asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_DRAWING) Then
                Isdrawding = True
            Else
                Isdrawding = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            Isdrawding = False
        End Try
    End Function


    ''' <summary>
    ''' 创建国标倒角标注
    ''' </summary>
    Public Sub GBCharmfer()
        Dim model As IpfcModel
        Dim selections As CpfcSelections
        Dim selectionOptions As IpfcSelectionOptions
        Dim selectFeature As IpfcSelection
        Dim chamfer As IpfcFeature
        Dim modelItems As IpfcModelItems
        Dim texts(1) As String
        Try
            If Isdrawding() Then
                model = asyncConnection.Session.CurrentModel
                selectionOptions = (New CCpfcSelectionOptions).Create("feature")
                selectionOptions.MaxNumSels = 1
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                If selections Is Nothing Then
                    Throw New Exception("请选择一个倒角特征元素！")
                End If
                If selections.Count < 1 Then
                    Throw New Exception("请选择一个倒角特征元素！")
                End If
                selectFeature = selections.Item(0)
                If selectFeature.SelItem.Type <> EpfcModelItemType.EpfcITEM_FEATURE Then
                    Throw New Exception("请选择一个倒角特征元素！")
                End If
                chamfer = selectFeature.SelItem
                If chamfer.FeatType <> EpfcFeatureType.EpfcFEATTYPE_CHAMFER Then
                    Throw New Exception("请选择一个倒角特征元素！")
                End If

                modelItems = chamfer.ListSubItems(EpfcModelItemType.EpfcITEM_DIMENSION)
                '根据倒角的类型设定不同的显示文字,DXD,DXANGULAR等
                If modelItems.Count = 1 Then
                    texts(0) = "C" + "&" + modelItems(0).GetName.ToString
                Else
                    If CType(modelItems(0), IpfcBaseDimension).DimType = EpfcDimensionType.EpfcDIM_ANGULAR Then
                        texts(0) = "&" + modelItems(1).GetName.ToString
                        texts(1) = "&" + modelItems(0).GetName.ToString
                    ElseIf CType(modelItems(1), IpfcBaseDimension).DimType = EpfcDimensionType.EpfcDIM_ANGULAR Then
                        texts(0) = "&" + modelItems(0).GetName.ToString
                        texts(1) = "&" + modelItems(1).GetName.ToString
                    Else
                        texts(0) = "&" + modelItems(0).GetName.ToString
                        texts(1) = "&" + modelItems(1).GetName.ToString
                    End If
                    texts(0) = texts(0) + " x " + texts(1)
                End If
                '用宏完成标注
                texts(0) = "imicharmfer ~ Activate `main_dlg_cur` `page_Annotate_control_btn`0 ;~ Command `ProCmdDwgCreateNote` ;#ISO LEADER;#ENTER;#HORIZONTAL;#TANGENT LEADER;#DEFAULT;#MAKE NOTE;#NO ARROW;@PAUSE_FOR_SCREEN_PICK;#PICK PNT;@PAUSE_FOR_SCREEN_PICK;" & texts(0) & ";;#DONE/RETURN;"
                asyncConnection.Session.RunMacro(texts(0))
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module