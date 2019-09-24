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
    ''' 返回表头列表
    ''' </summary>
    ''' <returns>表头列表</returns>
    Public Function GetFamColSymbols() As ArrayList
        Dim model As IpfcModel
        Dim FamMember As IpfcFamilyMember
        Dim FamMemberCols As IpfcFamilyTableColumns
        Dim i As Integer
        Dim ColSymbols As New ArrayList
        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                Return Nothing
            End If
            If (Not model.Type = EpfcModelType.EpfcMDL_PART) And (Not model.Type = EpfcModelType.EpfcMDL_ASSEMBLY) Then
                Return Nothing
            End If
            FamMember = CType(model, IpfcFamilyMember)
            FamMemberCols = FamMember.ListColumns()
            If FamMemberCols.Count = 0 Then
                Return Nothing
            End If
            ColSymbols.Add("实例名")
            For i = 0 To FamMemberCols.Count - 1
                ColSymbols.Add(FamMemberCols.Item(i).Symbol)
            Next
            Return ColSymbols
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 返回族表行数
    ''' </summary>
    ''' <returns>行数</returns>
    Public Function GetFamRowCount() As Integer
        Dim model As IpfcModel
        Dim FamMember As IpfcFamilyMember
        Dim FamMemberRows As IpfcFamilyTableRows
        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                Return Nothing
            End If
            If (Not model.Type = EpfcModelType.EpfcMDL_PART) And (Not model.Type = EpfcModelType.EpfcMDL_ASSEMBLY) Then
                Return Nothing
            End If
            FamMember = CType(model, IpfcFamilyMember)
            FamMemberRows = FamMember.ListRows()
            Return FamMemberRows.Count
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetFamRow(ByVal row As Integer) As ArrayList
        Dim model As IpfcModel
        Dim FamMember As IpfcFamilyMember
        Dim FamMemberCols As IpfcFamilyTableColumns
        Dim FamMemberRows As IpfcFamilyTableRows
        Dim Value As IpfcParamValue
        Dim instmodel As IpfcModel
        Dim i As Integer
        Dim FamRow As New ArrayList
        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                Return Nothing
            End If
            If (Not model.Type = EpfcModelType.EpfcMDL_PART) And (Not model.Type = EpfcModelType.EpfcMDL_ASSEMBLY) Then
                Return Nothing
            End If
            FamMember = CType(model, IpfcFamilyMember)
            FamMemberCols = FamMember.ListColumns()
            FamMemberRows = FamMember.ListRows()
            If FamMemberCols.Count = 0 Then
                Return Nothing
            End If

            instmodel = FamMemberRows.Item(row).CreateInstance()
            FamRow.Add(instmodel.InstanceName)
            For i = 0 To FamMemberCols.Count - 1
                Value = FamMember.GetCell(FamMemberCols.Item(i), FamMemberRows.Item(row))
                Select Case Value.discr
                    Case EpfcParamValueType.EpfcPARAM_BOOLEAN
                        FamRow.Add(CStr(Value.BoolValue))
                    Case EpfcParamValueType.EpfcPARAM_DOUBLE
                        FamRow.Add(CStr(Value.DoubleValue))
                    Case EpfcParamValueType.EpfcPARAM_INTEGER
                        FamRow.Add(CStr(Value.IntValue))
                    Case EpfcParamValueType.EpfcPARAM_STRING
                        FamRow.Add(Value.StringValue)
                    Case Else
                        FamRow.Add("")
                End Select
            Next
            FamMemberRows.Item(row).Erase()
            Return FamRow
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function



End Module