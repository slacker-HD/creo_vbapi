Option Explicit On
Imports System.IO
Imports pfcls

Module Module_Famtab
    ''' <summary>
    ''' 导出族表文件到单独实例
    ''' </summary>
    ''' <param name="InputFile">输入文件</param>
    ''' <param name="OutputPath">输出目录</param>
    Public Function ExportFamtableinstances(ByVal InputFile As String, ByVal OutputPath As String) As List(Of String)

        Dim modelDesc As IpfcModelDescriptor
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim model, instmodel As IpfcModel
        Dim solid As IpfcSolid
        Dim familyTableRows As IpfcFamilyTableRows
        Dim familyTableRow As IpfcFamilyTableRow
        Dim i As Integer

        If asyncConnection Is Nothing Then
            If (CreateConnection() = False) Then
                MessageBox.Show("无法创建Creo会话,请确认Creo环境正确配置！")
                Return Nothing
            End If
        End If

        If IsFileExist(InputFile) = False Then
            MessageBox.Show("输入模型文件不存在！")
            Return Nothing
        End If

        If IsPathExist(OutputPath) = False Then
            MessageBox.Show("输出目录不存在！")
            Return Nothing
        End If

        ExportFamtableinstances = New List(Of String)

        Try
            '先拷贝到输出目录
            FileCopy(InputFile, OutputPath + "\" + "ORI_" + Path.GetFileName(InputFile))
            '打开文件的例行操作
            modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, Nothing, Nothing)
            modelDesc.Path = OutputPath + "\" + "ORI_" + Path.GetFileName(InputFile)
            retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create()
            retrieveModelOptions.AskUserAboutReps = False
            model = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)

            asyncConnection.Session.ChangeDirectory(OutputPath)

            solid = CType(model, IpfcSolid)
            familyTableRows = CType(solid, IpfcFamilyMember).ListRows()

            For i = 0 To familyTableRows.Count - 1
                familyTableRow = familyTableRows.Item(i)
                instmodel = familyTableRow.CreateInstance()
                instmodel.Copy(instmodel.InstanceName + ".prt", Nothing)
                ExportFamtableinstances.Add(instmodel.InstanceName)
            Next
            Shell("Explorer " & OutputPath, vbNormalFocus)
        Catch ex As Exception
            MessageBox.Show(ex.Message + Chr(13) + ex.StackTrace)
            Return Nothing
        End Try
    End Function
End Module
