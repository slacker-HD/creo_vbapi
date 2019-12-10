Option Explicit On
Imports pfcls
Module Module_Param

    ''' <summary>
    ''' 清空指定目录下所有prt的参数
    ''' </summary>
    ''' <param name="InputPath">目录</param>
    ''' <returns>操作结果</returns>
    Public Function ClearParams(ByVal InputPath As String) As Hashtable
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

        ClearParams = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.prt", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            ClearParams.Add(filename, ClearParas(filename))
        Next
        Shell("Explorer " & InputPath, vbNormalFocus)
    End Function

    ''' <summary>
    ''' 清空参数
    ''' </summary>
    ''' <param name="FileName">指定prt文件路径</param>
    ''' <returns>是否操作成功</returns>
    Private Function ClearParas(ByVal FileName As String) As Boolean
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim params As IpfcParameters
        Dim param As IpfcParameter
        Try
            descmodel = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, "", Nothing)
            descmodel.Path = FileName
            options = (New CCpfcRetrieveModelOptions).Create()
            options.AskUserAboutReps = False
            model = asyncConnection.Session.RetrieveModelWithOpts(descmodel, options)
        Catch ex As Exception
            Return False
        End Try
        Try
            params = CType(model, IpfcParameterOwner).ListParams()
            ClearParas = True
            For i = params.Count - 1 To 0 Step -1
                Try
                    param = params.Item(i)
                    param.Delete()
                Catch ex As Exception
                    ClearParas = False
                End Try
            Next
            model.Save()
            model.Erase()
        Catch ex As Exception
            ClearParas = False
        End Try
    End Function
    ''' <summary>
    ''' 批量对参数打勾
    ''' </summary>
    ''' <param name="InputPath">输入目录</param>
    ''' <returns>操作结果</returns>
    Public Function DesignateParams(ByVal InputPath As String) As Hashtable
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

        DesignateParams = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.prt", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            DesignateParams.Add(filename, DesignateParas(filename))
        Next
        Shell("Explorer " & InputPath, vbNormalFocus)
    End Function
    ''' <summary>
    ''' 指定零件的所有参数
    ''' </summary>
    ''' <param name="FileName">文件路径</param>
    ''' <returns>是否成功</returns>
    Private Function DesignateParas(ByVal FileName As String) As Boolean
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim params As IpfcParameters
        Dim param As IpfcParameter
        Try
            descmodel = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, "", Nothing)
            descmodel.Path = FileName
            options = (New CCpfcRetrieveModelOptions).Create()
            options.AskUserAboutReps = False
            model = asyncConnection.Session.RetrieveModelWithOpts(descmodel, options)
        Catch ex As Exception
            Return False
        End Try
        Try
            params = CType(model, IpfcParameterOwner).ListParams()
            DesignateParas = True
            For i = params.Count - 1 To 0 Step -1
                Try
                    param = params.Item(i)
                    param.IsDesignated = True
                Catch ex As Exception
                    DesignateParas = False
                End Try
            Next
            model.Save()
            model.Erase()
        Catch ex As Exception
            DesignateParas = False
        End Try
    End Function
    ''' <summary>
    ''' 批量添加修改参数
    ''' </summary>
    ''' <param name="InputPath">输入目录</param>
    ''' <param name="DataSource">包含参数的BindingSource</param>
    ''' <returns></returns>
    Public Function AddParams(ByVal InputPath As String, ByVal DataSource As BindingSource) As Hashtable
        Dim params As New Hashtable
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
        If CreateParamList(DataSource, params) <> True Then
            MessageBox.Show("读取参数文件有错，无法完成添加修改操作！")
            Return Nothing
        End If

        AddParams = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.prt", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            AddParams.Add(filename, AddParam(filename, params))
        Next

        Shell("Explorer " & InputPath, vbNormalFocus)

    End Function
    ''' <summary>
    ''' 对零件添加参数
    ''' </summary>
    ''' <param name="FileName">零件路径</param>
    ''' <param name="Parameters">参数对应的表</param>
    ''' <returns>是否成功</returns>
    Private Function AddParam(ByVal FileName As String, Parameters As Hashtable) As Boolean
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim param As IpfcParameter
        Try
            descmodel = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, "", Nothing)
            descmodel.Path = FileName
            options = (New CCpfcRetrieveModelOptions).Create()
            options.AskUserAboutReps = False
            model = asyncConnection.Session.RetrieveModelWithOpts(descmodel, options)
        Catch ex As Exception
            Return False
        End Try

        Try
            AddParam = True
            For Each paramvalue In Parameters
                Try
                    param = CType(model, IpfcParameterOwner).GetParam(paramvalue.key)
                    If param Is Nothing Then
                        CType(model, IpfcParameterOwner).CreateParam(paramvalue.key, Parameters(paramvalue.key))
                    Else
                        param.SetScaledValue(Parameters(paramvalue.key), Nothing)
                    End If

                Catch ex As Exception
                    AddParam = False
                End Try
            Next
            model.Save()
            model.Erase()
        Catch ex As Exception
            AddParam = False
        End Try
    End Function
    ''' <summary>
    ''' 根据给定表格创建记录参数的Hashtable
    ''' </summary>
    ''' <param name="DataSource">给定表格</param>
    ''' <param name="Params">返回的参数的Hashtable</param>
    ''' <returns>是否成功</returns>
    Private Function CreateParamList(ByVal DataSource As BindingSource, ByRef Params As Hashtable) As Boolean
        Dim paramType, paramValue, paramName As String
        Try
            CreateParamList = True
            If DataSource.Count = 0 Then
                Return False
            End If
            If DataSource(0).Row.ItemArray.Length <> 10 Then
                Return False
            End If
            For i = 0 To DataSource.Count - 1
                Dim iParamValue As IpfcParamValue
                paramName = DataSource(i).Row.ItemArray(0)
                paramType = DataSource(i).Row.ItemArray(1)
                paramValue = DataSource(i).Row.ItemArray(2)

                If (paramType = "实数" Or paramType = "Real Number") Then
                    iParamValue = (New CMpfcModelItem).CreateDoubleParamValue(Double.Parse(paramValue))
                ElseIf (paramType = "整数" Or paramType = "Integer") Then
                    iParamValue = (New CMpfcModelItem).CreateIntParamValue(Int32.Parse(paramValue))
                ElseIf (paramType = "字符串" Or paramType = "String") Then
                    iParamValue = (New CMpfcModelItem).CreateStringParamValue(paramValue)
                ElseIf (paramType = "是/否" Or paramType = "Yes No") Then
                    If paramValue.ToLower = "yes" Then
                        iParamValue = (New CMpfcModelItem).CreateBoolParamValue(True)
                    Else
                        iParamValue = (New CMpfcModelItem).CreateBoolParamValue(False)
                    End If
                Else
                    Params = Nothing
                    Return False
                End If
                Params.Add(paramName, iParamValue)
            Next
        Catch ex As Exception
            Return False
        End Try
    End Function

End Module