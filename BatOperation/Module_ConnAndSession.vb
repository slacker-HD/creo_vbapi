Option Explicit On
Imports System.Configuration
Imports pfcls

Module Module_ConnAndSession
    Public asyncConnection As IpfcAsyncConnection = Nothing '全局变量，用于存储连接会话的句柄
    ''' <summary>
    ''' 创建Creo会话连接
    ''' </summary>
    ''' <returns>是否成功</returns>
    Public Function CreateConnection() As Boolean
        Try
            Dim config As Configuration
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

            Dim CmdLine As String = config.AppSettings.Settings("CmdLine").Value
            Dim ShowWindow As String = config.AppSettings.Settings("ShowWindow").Value
            Dim Configfile As String = config.AppSettings.Settings("Configfile").Value
            If ShowWindow = False Then
                CmdLine += " -g:no_graphics -i:rpc_input"
            End If
            asyncConnection = (New CCpfcAsyncConnection).Start(CmdLine, "")
            asyncConnection.Session.LoadConfigFile(Configfile)
            CreateConnection = True
        Catch ex As Exception
            CreateConnection = False
            MessageBox.Show(ex.Message + Chr(13) + ex.StackTrace)
        End Try
    End Function
    ''' <summary>
    ''' 终止会话
    ''' </summary>
    Public Sub EndConnection()
        Try
            asyncConnection.End()
        Catch ex As Exception

        End Try
    End Sub

End Module
