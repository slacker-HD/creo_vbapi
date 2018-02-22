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
    ''' 修改尺寸文字
    ''' </summary>
    ''' <param name="Prefix">要添加的前缀</param>
    ''' <param name="Surffix">要添加的后缀</param>
    ''' <param name="DownText">要添加的尺寸线下方文字</param>
    Public Sub Modify_text(ByVal Prefix As String, ByVal Surffix As String, ByVal DownText As String)
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectDim As IpfcSelection
        Dim bdimesion As IpfcBaseDimension
        Dim TextStrs As Istringseq
        Try
            If Isdrawding() = True Then
                selectionOptions = (New CCpfcSelectionOptions).Create("dimension")
                selectionOptions.MaxNumSels = 1
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                If selections Is Nothing Then
                    Exit Sub
                End If
                If selections.Count < 1 Then
                    Throw New Exception("请选择一个尺寸元素！")
                End If
                '获取文字对象
                selectDim = selections.Item(0)
                bdimesion = selectDim.SelItem
                TextStrs = bdimesion.Texts
                '修改前后缀，只要修改Item（0）即可
                TextStrs.Set(0, Prefix + bdimesion.Texts.Item(0) + Surffix)
                '修改尺寸线下方文字，如果Item（0）存在则直接修改值，不存在添加一个
                If DownText <> "" Then
                    If TextStrs.Count > 1 Then
                        TextStrs.Set(1, DownText)
                    Else
                        TextStrs.Insert(1, DownText)
                    End If
                End If
                '直接设定
                bdimesion.Texts = TextStrs
                Reg_Csheet()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

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
    ''' 选择一个尺寸并设置为对称公差
    ''' </summary>
    ''' <param name="value">公差值</param>
    Public Sub Symmetrical_tolerance(ByVal value As Double)
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectDim As IpfcSelection
        Dim dimension As IpfcDimension
        Dim limits As IpfcDimTolSymmetric
        Dim limitstab As IpfcDimTolISODIN
        Dim TolTableType As EpfcToleranceTableType
        Try
            If asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_DRAWING Then
                selectionOptions = (New CCpfcSelectionOptions).Create("dimension")
                selectionOptions.MaxNumSels = 1
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                If selections Is Nothing OrElse selections.Count = 0 Then
                    MsgBox("请选择一个尺寸！", MsgBoxStyle.OkOnly, "提示")
                Else
                    selectDim = selections.Item(0)
                    dimension = selectDim.SelItem
                    '由于Creo配置的问题，有时候需要设置公差表为无才能正确显示，需要提前在Creo中加载公差表
                    TolTableType = EpfcToleranceTableType.EpfcToleranceTableType_nil
                    limitstab = (New CCpfcDimTolISODIN).Create(TolTableType, "a", 11) '后两个参数为随便设置的临时值
                    dimension.Tolerance = limitstab
                    '这里设置公差
                    limits = (New CCpfcDimTolSymmetric).Create(value)
                    dimension.Tolerance = limits
                End If
                Reg_Csheet()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub


    ''' <summary>
    ''' 选择一个尺寸并设置公差正负值
    ''' </summary>
    ''' <param name="upper">公差上限</param>
    ''' <param name="lower">公差下限</param>
    Public Sub Plusminus_tolerance(ByVal upper As Double, ByVal lower As Double)
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectDim As IpfcSelection
        Dim dimension As IpfcDimension
        Dim limits As IpfcDimTolPlusMinus
        Dim limitstab As IpfcDimTolISODIN
        Dim TolTableType As EpfcToleranceTableType
        Try
            If asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_DRAWING Then
                selectionOptions = (New CCpfcSelectionOptions).Create("dimension")
                selectionOptions.MaxNumSels = 1
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                If selections Is Nothing OrElse selections.Count = 0 Then
                    MsgBox("请选择一个尺寸！", MsgBoxStyle.OkOnly, "提示")
                Else
                    selectDim = selections.Item(0)
                    dimension = selectDim.SelItem
                    '由于Creo配置的问题，有时候需要设置公差表为无才能正确显示，需要提前在Creo中加载公差表
                    TolTableType = EpfcToleranceTableType.EpfcToleranceTableType_nil
                    limitstab = (New CCpfcDimTolISODIN).Create(TolTableType, "a", 11) '后两个参数为随便设置的临时值
                    dimension.Tolerance = limitstab
                    '这里设置公差
                    limits = (New CCpfcDimTolPlusMinus).Create(upper, -lower) '注意lower的正负号
                    dimension.Tolerance = limits
                End If
                Reg_Csheet()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 查询基轴制或基孔制公差并显示
    ''' </summary>
    ''' <param name="table">公差表内容</param>
    ''' <param name="Shownumber">标注时是否显示数值</param>
    Public Sub Table_tolerance(ByVal table As String, ByVal Shownumber As Boolean)
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectDim As IpfcSelection
        Dim dimension As IpfcDimension
        Dim limitstab As IpfcDimTolISODIN
        Dim limitsPM As IpfcDimTolPlusMinus '设置显示后面数字
        Dim limitsS As IpfcDimTolSymmetric '设置不显示后面数字
        Dim TolTableType As EpfcToleranceTableType
        Dim name, column As String
        Try
            If asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_DRAWING Then
                selectionOptions = (New CCpfcSelectionOptions).Create("dimension")
                selectionOptions.MaxNumSels = 1
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                If selections Is Nothing OrElse selections.Count = 0 Then
                    MsgBox("请选择一个尺寸！", MsgBoxStyle.OkOnly, "提示")
                Else
                    selectDim = selections.Item(0)
                    dimension = selectDim.SelItem
                    '设置是否显示后面的数字
                    If Shownumber = True Then
                        limitsPM = (New CCpfcDimTolPlusMinus).Create(0, 0)
                        dimension.Tolerance = limitsPM
                    Else
                        limitsS = (New CCpfcDimTolSymmetric).Create(0)
                        dimension.Tolerance = limitsS
                    End If
                    '设置公差表模式
                    Select Case table.First
                        Case "A" To "Z"
                            TolTableType = EpfcToleranceTableType.EpfcTOLTABLE_HOLES
                        Case "a" To "z"
                            TolTableType = EpfcToleranceTableType.EpfcTOLTABLE_SHAFTS
                    End Select
                    name = ""
                    column = ""
                    Get_tol_table(table, name, column)
                    limitstab = (New CCpfcDimTolISODIN).Create(TolTableType, name, CInt(column))
                    dimension.Tolerance = limitstab
                    '需要这么设置下才能正确显示
                    limitsPM = (New CCpfcDimTolPlusMinus).Create(0, 0)
                    dimension.Tolerance = limitsPM
                End If
                Reg_Csheet()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 查询基轴制或基孔制配合公差并显示
    ''' </summary>
    ''' <param name="TolName">公差表内容</param>
    ''' <param name="Shownumber">标注时是否显示数值</param>
    Public Sub Fit_tolerance(ByVal TolName As String, ByVal ShowNumber As Boolean)
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectDim As IpfcSelection
        Dim dimension As IpfcBaseDimension
        Dim Texts As Istringseq
        Try
            If asyncConnection.Session.CurrentModel.Type = EpfcModelType.EpfcMDL_DRAWING Then
                selectionOptions = (New CCpfcSelectionOptions).Create("dimension")
                selectionOptions.MaxNumSels = 1
                selections = asyncConnection.Session.Select(selectionOptions, Nothing)
                If selections Is Nothing OrElse selections.Count = 0 Then
                    MsgBox("请选择一个尺寸！", MsgBoxStyle.OkOnly, "提示")
                Else
                    selectDim = selections.Item(0)
                    dimension = selectDim.SelItem
                    Texts = dimension.Texts
                    If ShowNumber = True Then
                        Texts.Set(0, "@D" + Fit_tolerance_name(dimension.DimValue, TolName))
                    Else
                        Texts.Set(0, "@D" + TolName)
                    End If
                    dimension.Texts = Texts
                End If
                Reg_Csheet()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

End Module