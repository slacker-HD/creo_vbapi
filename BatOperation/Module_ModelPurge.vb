Option Explicit On
Imports pfcls
Module Module_ModelPurge
    ''' <summary>
    ''' 清空目录下的asm、prt和drw的旧版本
    ''' </summary>
    ''' <param name="InputPath">输入目录</param>
    ''' <returns>操作结果</returns>
    Public Function PurgeModels(ByVal InputPath As String) As Hashtable
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
        PurgeModels = New Hashtable
        asyncConnection.Session.ChangeDirectory(InputPath)

        PurgeModelByType("*.asm", PurgeModels)
        PurgeModelByType("*.prt", PurgeModels)
        PurgeModelByType("*.drw", PurgeModels)

        Shell("Explorer " & InputPath, vbNormalFocus)
    End Function
    ''' <summary>
    ''' 按照文件类型清空旧版本
    ''' </summary>
    ''' <param name="Filetype">文件类型</param>
    ''' <param name="Opresults">操作结果</param>
    Private Sub PurgeModelByType(ByVal Filetype As String, ByRef Opresults As Hashtable)
        Dim AllFiles As Cstringseq
        Dim LatestFiles As Cstringseq
        Try
            AllFiles = CType(asyncConnection.Session, IpfcBaseSession).ListFiles(Filetype, EpfcFileListOpt.EpfcFILE_LIST_ALL, asyncConnection.Session.GetCurrentDirectory)
            LatestFiles = CType(asyncConnection.Session, IpfcBaseSession).ListFiles(Filetype, EpfcFileListOpt.EpfcFILE_LIST_LATEST, asyncConnection.Session.GetCurrentDirectory)
            For Each file In AllFiles
                Dim DelAct As Boolean = True
                For Each latestfile In LatestFiles
                    If file = latestfile Then
                        DelAct = False
                        Exit For
                    End If
                Next
                If DelAct = True Then
                    Opresults.Add(file, Recycle(file))
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message + Chr(13) + ex.StackTrace)
        End Try
    End Sub
End Module
