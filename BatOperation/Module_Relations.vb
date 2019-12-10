Option Explicit On
Imports pfcls

Module Module_Relations
    ''' <summary>
    ''' 清空指定目录下所有prt的关系
    ''' </summary>
    ''' <param name="InputPath">目录</param>
    ''' <returns>操作结果</returns>
    Public Function ClearRelations(ByVal InputPath As String) As Hashtable
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

        ClearRelations = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.prt", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            ClearRelations.Add(filename, ClearRel(filename))
        Next
        Shell("Explorer " & InputPath, vbNormalFocus)

    End Function

    ''' <summary>
    ''' 清空关系
    ''' </summary>
    ''' <param name="FileName">指定prt文件路径</param>
    ''' <returns>是否操作成功</returns>
    Private Function ClearRel(ByVal FileName As String) As Boolean
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim model As IpfcModel
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
            CType(model, IpfcRelationOwner).DeleteRelations()
            model.Save()
            model.Erase()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' 向指定目录下所有prt添加关系
    ''' </summary>
    ''' <param name="InputPath">包含prt的目录</param>
    ''' <param name="Relations">包含关系字符串数组</param>
    ''' <returns>操作结果</returns>
    Public Function AddRelations(ByVal InputPath As String, ByVal Relations As String()) As Hashtable
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

        AddRelations = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)
        Files = CType(asyncConnection.Session, IpfcBaseSession).ListFiles("*.prt", EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)

        For Each filename In Files
            AddRelations.Add(filename, AddRel(filename, Relations))
        Next
        Shell("Explorer " & InputPath, vbNormalFocus)
    End Function

    ''' <summary>
    ''' 添加关系
    ''' </summary>
    ''' <param name="FileName">prt文件路径</param>
    ''' <param name="Relations">包含关系的字符串数组</param>
    ''' <returns>是否成功</returns>
    Private Function AddRel(ByVal FileName As String, ByVal Relations() As String) As Boolean
        Dim descmodel As IpfcModelDescriptor
        Dim options As IpfcRetrieveModelOptions
        Dim model As IpfcModel
        Dim rels As New Cstringseq
        Dim i As Integer
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
            For i = 0 To model.Relations.Count - 1
                rels.Append(CType(model, IpfcRelationOwner).Relations.Item(i))
            Next
            For Each line As String In Relations
                rels.Append(line)
            Next
            CType(model, IpfcRelationOwner).Relations = rels
            model.Save()
            model.Erase()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

End Module
