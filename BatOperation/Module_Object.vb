Option Explicit On
Imports pfcls
Module Module_Object

    Public Function ExportPdfs(ByVal InputPath As String, ByVal OutputPath As String) As Hashtable
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

        If IsPathExist(OutputPath) = False Then
            MessageBox.Show("输出目录不存在！")
            Return Nothing
        End If

        ExportPdfs = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.drw", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            ExportPdfs.Add(filename, ExportPdf(filename, OutputPath))
        Next

        Shell("Explorer " & OutputPath , vbNormalFocus)
    End Function

    Private Function ExportPdf(ByVal Filename As String, ByVal Outputpath As String) As Boolean
        Dim modelDesc As IpfcModelDescriptor
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim pdfinstructions As IpfcPDFExportInstructions
        Dim oripath As String

        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If
        ExportPdf = True
        Try
            '打开文件的例行操作
            modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_DRAWING, Nothing, Nothing)
            modelDesc.Path = Filename
            retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create()
            retrieveModelOptions.AskUserAboutReps = False
            model = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
            asyncConnection.Session.CreateModelWindow(model)

            oripath = asyncConnection.Session.GetCurrentDirectory()
            asyncConnection.Session.ChangeDirectory(Outputpath)
            pdfinstructions = (New CCpfcPDFExportInstructions()).Create()
            model.Export(model.InstanceName + ".pdf", pdfinstructions)
            asyncConnection.Session.ChangeDirectory(oripath)
            model.Erase()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ExportDwgs(ByVal InputPath As String, ByVal OutputPath As String) As Hashtable
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

        If IsPathExist(OutputPath) = False Then
            MessageBox.Show("输出目录不存在！")
            Return Nothing
        End If

        ExportDwgs = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.drw", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            ExportDwgs.Add(filename, ExportDwg(filename, OutputPath))
        Next

        Shell("Explorer " & OutputPath, vbNormalFocus)
    End Function

    Private Function ExportDwg(ByVal Filename As String, ByVal Outputpath As String) As Boolean
        Dim modelDesc As IpfcModelDescriptor
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim dwginstructions As IpfcDWG3DExportInstructions
        Dim oripath As String

        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If
        ExportDwg = True
        Try
            '打开文件的例行操作
            modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_DRAWING, Nothing, Nothing)
            modelDesc.Path = Filename
            retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create()
            retrieveModelOptions.AskUserAboutReps = False
            model = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
            asyncConnection.Session.CreateModelWindow(model)

            oripath = asyncConnection.Session.GetCurrentDirectory()
            asyncConnection.Session.ChangeDirectory(Outputpath)
            dwginstructions = (New CCpfcDWG3DExportInstructions()).Create()
            model.Export(model.InstanceName + ".dwg", dwginstructions)
            asyncConnection.Session.ChangeDirectory(oripath)
            model.Erase()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ExportStps(ByVal InputPath As String, ByVal OutputPath As String) As Hashtable
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

        If IsPathExist(OutputPath) = False Then
            MessageBox.Show("输出目录不存在！")
            Return Nothing
        End If

        ExportStps = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.prt", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            ExportStps.Add(filename, ExportStp(filename, OutputPath))
        Next

        Shell("Explorer " & OutputPath, vbNormalFocus)
    End Function

    Private Function ExportStp(ByVal Filename As String, ByVal Outputpath As String) As Boolean
        Dim modelDesc As IpfcModelDescriptor
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim oripath As String
        Dim stepinstructions As IpfcSTEP3DExportInstructions
        Dim flags As IpfcGeometryFlags

        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If
        ExportStp = True
        Try
            '打开文件的例行操作
            modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, Nothing, Nothing)
            modelDesc.Path = Filename
            retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create()
            retrieveModelOptions.AskUserAboutReps = False
            model = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
            asyncConnection.Session.CreateModelWindow(model)

            oripath = asyncConnection.Session.GetCurrentDirectory()
            asyncConnection.Session.ChangeDirectory(Outputpath)
            flags = (New CCpfcGeometryFlags()).Create()
            flags.AsSolids = True
            stepinstructions = (New CCpfcSTEP3DExportInstructions()).Create(EpfcAssemblyConfiguration.EpfcEXPORT_ASM_MULTI_FILES, flags)
            model.Export(model.InstanceName + ".stp", stepinstructions)
            asyncConnection.Session.ChangeDirectory(oripath)
            model.Erase()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Public Function ExportIgss(ByVal InputPath As String, ByVal OutputPath As String) As Hashtable
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

        If IsPathExist(OutputPath) = False Then
            MessageBox.Show("输出目录不存在！")
            Return Nothing
        End If

        ExportIgss = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.prt", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            ExportIgss.Add(filename, ExportIgs(filename, OutputPath))
        Next

        Shell("Explorer " & OutputPath, vbNormalFocus)
    End Function

    Private Function ExportIgs(ByVal Filename As String, ByVal Outputpath As String) As Boolean
        Dim modelDesc As IpfcModelDescriptor
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim oripath As String
        Dim geometryFlags As IpfcGeometryFlags
        Dim igsinstructions As IpfcIGES3DNewExportInstructions

        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If
        ExportIgs = True
        Try
            '打开文件的例行操作
            modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, Nothing, Nothing)
            modelDesc.Path = Filename
            retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create()
            retrieveModelOptions.AskUserAboutReps = False
            model = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
            asyncConnection.Session.CreateModelWindow(model)

            oripath = asyncConnection.Session.GetCurrentDirectory()
            asyncConnection.Session.ChangeDirectory(Outputpath)
            geometryFlags = (New CCpfcGeometryFlags).Create()
            geometryFlags.AsSolids = True '导出为Solid选项
            '第一个参数应该是EpfcAssemblyConfiguration，帮助文档有误；
            igsinstructions = (New CCpfcIGES3DNewExportInstructions).Create(EpfcAssemblyConfiguration.EpfcEXPORT_ASM_SINGLE_FILE, geometryFlags)
            model.Export(model.InstanceName + ".igs", igsinstructions)
            asyncConnection.Session.ChangeDirectory(oripath)
            model.Erase()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' 转换当前打开文件为Igs,文件保存在Creo工作目录下同名igs文件。
    ''' </summary>
    Public Sub ConvertToIgs()
        Dim model As IpfcModel


        Try
            model = asyncConnection.Session.CurrentModel


        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub


End Module
