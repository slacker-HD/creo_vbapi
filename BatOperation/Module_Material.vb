Option Explicit On
Imports pfcls
Module Module_Material
    ''' <summary>
    ''' 获取指定目录下所有mtl文件
    ''' </summary>
    ''' <param name="MtlPath">包含mtl文件的目录</param>
    ''' <returns>mtl文件列表</returns>
    Public Function GetMtls(ByVal MtlPath As String) As String()
        Dim Files As Cstringseq
        Dim i As Integer = 1
        Dim mtls(0) As String
        mtls(0) = "不修改"
        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If

        If IsPathExist(MtlPath) = False Then
            MessageBox.Show("输入目录不存在！")
            Return Nothing
        End If

        asyncConnection.Session.ChangeDirectory(MtlPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.mtl", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)
        If (Files.Count > 0) Then
            ReDim Preserve mtls(Files.Count)

            For Each filename In Files
                mtls(i) = IO.Path.GetFileNameWithoutExtension(filename)
                i += 1
            Next
        End If
        GetMtls = mtls
    End Function

    ''' <summary>
    ''' 批量获取给定目录下零件材料信息
    ''' </summary>
    ''' <param name="InputPath">给定目录</param>
    ''' <returns>零件材料信息</returns>
    Public Function GetMaterials(ByVal InputPath As String) As Hashtable
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

        GetMaterials = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.prt", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            GetMaterials.Add(filename, GetMaterial(filename))
        Next
    End Function

    ''' <summary>
    ''' 获得给定文件的材料信息
    ''' </summary>
    ''' <param name="FileName">给定文件路径</param>
    ''' <returns>材料信息</returns>
    Private Function GetMaterial(ByVal FileName As String) As String
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim part As IpfcPart
        Dim material As IpfcMaterial
        Try
            descmodel = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, "", Nothing)
            descmodel.Path = FileName
            options = (New CCpfcRetrieveModelOptions).Create()
            options.AskUserAboutReps = False
            model = asyncConnection.Session.RetrieveModelWithOpts(descmodel, options)
            part = CType(model, IpfcPart)
            material = part.CurrentMaterial
            If material IsNot Nothing Then
                GetMaterial = material.Name
            Else
                GetMaterial = ""
            End If

        Catch ex As Exception
            Return ""
        End Try
    End Function
    ''' <summary>
    ''' 批量设定材料
    ''' </summary>
    ''' <param name="Objs">待设定材料列表</param>
    ''' <param name="MtlPath">mtl文件路径</param>
    Public Sub ChangeMaterials(ByVal Objs As Hashtable, ByVal MtlPath As String)
        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return
            End If
        End If
        For Each item In Objs
            SetMaterial(item.key, Objs(item.key), MtlPath)
        Next
    End Sub
    ''' <summary>
    ''' 设定模型材料
    ''' </summary>
    ''' <param name="ModelPath">模型路径</param>
    ''' <param name="MtlName">材料名称</param>
    ''' <param name="MtlPath">材料文件所在路径</param>
    Private Sub SetMaterial(ByVal ModelPath As String, ByVal MtlName As String, ByVal MtlPath As String)
        Dim model As IpfcModel
        Dim part As IpfcPart
        Dim material As IpfcMaterial
        Dim currentpath As String
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Try
            descmodel = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, "", Nothing)
            descmodel.Path = ModelPath
            options = (New CCpfcRetrieveModelOptions).Create()
            options.AskUserAboutReps = False
            model = asyncConnection.Session.RetrieveModelWithOpts(descmodel, options)

            currentpath = asyncConnection.Session.GetCurrentDirectory()
            '打开材料文件时必须将工作目录设置为材料文件所在目录
            asyncConnection.Session.ChangeDirectory(MtlPath)
            part = CType(model, IpfcPart)
            '材料文件加载到零件中
            material = part.RetrieveMaterial(MtlName)
            '设定材料
            part.CurrentMaterial = material
            model.Save()
            model.Erase()
            '工作目录改回去
            asyncConnection.Session.ChangeDirectory(currentpath)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module

