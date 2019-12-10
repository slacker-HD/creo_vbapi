Option Explicit On
Imports pfcls
Module Module_Unit
    ''' <summary>
    ''' 批量获取给定目录下零件单位信息
    ''' </summary>
    ''' <param name="InputPath">给定目录</param>
    ''' <returns>单位信息</returns>
    Public Function GetUnits(ByVal InputPath As String) As Hashtable
        Dim Files As Cstringseq
        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If

        If IsPathExist(InputPath) = False Then
            MessageBox.Show("输入目录不存在！")
            Return Nothing
        End If

        GetUnits = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.prt", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            GetUnits.Add(filename, GetUnit(filename))
        Next
    End Function
    ''' <summary>
    ''' 获得给定文件的单位信息
    ''' </summary>
    ''' <param name="FileName">给定文件路径</param>
    ''' <returns>单位信息</returns>
    Private Function GetUnit(ByVal FileName As String) As String
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim unit As IpfcUnitSystem
        Try
            descmodel = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, "", Nothing)
            descmodel.Path = FileName
            options = (New CCpfcRetrieveModelOptions).Create()
            options.AskUserAboutReps = False
            model = asyncConnection.Session.RetrieveModelWithOpts(descmodel, options)
            unit = CType(model, IpfcSolid).GetPrincipalUnits()
            Return unit.Name
        Catch ex As Exception
            Return ""
        End Try
    End Function
    ''' <summary>
    ''' 批量对给定定对象修改单位
    ''' </summary>
    ''' <param name="Objs">给定的文件和修改方式</param>
    ''' <returns>修改结果</returns>
    Public Function ChangeUnits(ByVal Objs As Hashtable) As Hashtable

        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If
        ChangeUnits = New Hashtable
        For Each item In Objs
            ChangeUnits.Add(item.key, SetommNsUnit(item.key, Objs(item.key)))
        Next
    End Function
    ''' <summary>
    ''' 将给定prt单位转为mmNs
    ''' </summary>
    ''' <param name="Filename">给定prt文件</param>
    ''' <param name="ConvertMethod">转换方法</param>
    ''' <returns>是否成功</returns>
    Private Function SetommNsUnit(ByVal Filename As String, ByVal ConvertMethod As String) As Boolean
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim units As IpfcUnitSystems
        Dim DimensionOption As EpfcUnitDimensionConversion
        Dim i As Integer

        Try
            descmodel = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, "", Nothing)
            descmodel.Path = Filename
            options = (New CCpfcRetrieveModelOptions).Create()
            options.AskUserAboutReps = False
            model = asyncConnection.Session.RetrieveModelWithOpts(descmodel, options)
            units = CType(model, IpfcSolid).ListUnitSystems()
            For i = 0 To units.Count - 1
                If units.Item(i).Name.ToLower.IndexOf("mmns") <> -1 Then
                    If (ConvertMethod = "转换尺寸") Then
                        DimensionOption = EpfcUnitDimensionConversion.EpfcUNITCONVERT_SAME_DIMS
                    Else
                        DimensionOption = EpfcUnitDimensionConversion.EpfcUNITCONVERT_SAME_SIZE
                    End If
                    CType(model, IpfcSolid).SetPrincipalUnits(units.Item(i), (New CCpfcUnitConversionOptions).Create(DimensionOption))
                    model.Save()
                    model.Erase()
                    Return True
                End If
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function
End Module
