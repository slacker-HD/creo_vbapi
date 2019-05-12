Imports pfcls

Public Class VBapitool
    Private asyncConnection As IpfcAsyncConnection = Nothing '用于存储连接会话的句柄
    Private _apppath, _partpath, _outpath As String 'creo路径，要导出的prt文件，导出目录

    ''' <summary>
    ''' 类构造函数
    ''' </summary>
    ''' <param name="APP_path">CREO程序路径</param>
    ''' <param name="PART_path">要导出的prt文件路径</param>
    ''' <param name="OUT_path">导出目录</param>
    Sub New(APP_path As String, PART_path As String, OUT_path As String)
        _apppath = APP_path
        _partpath = PART_path
        _outpath = OUT_path
    End Sub

    ''' <summary>
    ''' 打开新会话
    ''' </summary>
    ''' <returns>新建会话是否成功</returns>
    Public Function NewSession() As Boolean
        Try
            asyncConnection = (New CCpfcAsyncConnection).Start(_apppath, "")
            asyncConnection.Session.LoadConfigFile("D:\ProeRes\config.pro")
            NewSession = True
        Catch ex As Exception
            NewSession = False
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Function

    ''' <summary>
    ''' 导出所有族表实例到单独文件
    ''' </summary>
    Public Sub ExportFamtableinstances()
        Dim modelDesc As IpfcModelDescriptor
        Dim retrieveModelOptions As IpfcRetrieveModelOptions
        Dim model, instmodel As IpfcModel
        Dim solid As IpfcSolid
        Dim familyTableRows As IpfcFamilyTableRows
        Dim familyTableRow As IpfcFamilyTableRow
        Dim i As Integer
        Try
            modelDesc = (New CCpfcModelDescriptor).Create(EpfcModelType.EpfcMDL_PART, Nothing, Nothing)
            modelDesc.Path = _partpath
            retrieveModelOptions = (New CCpfcRetrieveModelOptions).Create()
            retrieveModelOptions.AskUserAboutReps = False
            model = asyncConnection.Session.RetrievemodelWithOpts(modelDesc, retrieveModelOptions)
            asyncConnection.Session.ChangeDirectory(_outpath)
            solid = CType(model, IpfcSolid)
            familyTableRows = CType(solid, IpfcFamilyMember).ListRows()
            For i = 0 To familyTableRows.Count
                familyTableRow = familyTableRows.Item(i)
                instmodel = familyTableRow.CreateInstance()
                instmodel.Copy("IMI_" + instmodel.InstanceName + ".prt", Nothing)
            Next
        Catch ex As Exception
        End Try
    End Sub


    ''' <summary>
    ''' 关闭会话
    ''' </summary>
    Public Sub EndSession()
        Try
            asyncConnection.End()
        Catch ex As Exception

        End Try
    End Sub

End Class