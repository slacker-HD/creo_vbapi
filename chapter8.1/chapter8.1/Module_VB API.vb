Imports System.Configuration
Imports pfcls

Module Module_vbapi
    Public asyncConnection As IpfcAsyncConnection = Nothing '全局变量，用于存储连接会话的句柄
    Private WithEvents EventTimer As Timers.Timer '定时器，用于定时处理asyncConnection.EventProcess，防止Creo无法处理事件导致程序锁死

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
    ''' 处理asyncConnection事件的loop，Full Asynchronous Mode必须
    ''' </summary>
    Public Sub EventProcess()
        EventTimer = New Timers.Timer(100) With {.Enabled = True}
        AddHandler EventTimer.Elapsed, AddressOf TimeElapsed
    End Sub

    ''' <summary>
    ''' 定时器定时处理asyncConnection.EventProcess
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TimeElapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        asyncConnection.EventProcess()
    End Sub

    ''' <summary>
    ''' 添加Model的相关Listener
    ''' </summary>
    Public Sub AddSessionActionListener()
        Dim listenerObj As New SessionActionListener(asyncConnection)
        Try
            asyncConnection.Session.AddActionListener(listenerObj)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 继承自IpfcSessionActionListener，用于Session监听相关事件
    ''' </summary>
    Private Class SessionActionListener
        Implements IpfcSessionActionListener
        Implements ICIPClientObject
        Implements IpfcActionListener
        Private asyncConnection As IpfcAsyncConnection

        Public Sub New(ByRef aC As IpfcAsyncConnection)
            asyncConnection = aC
        End Sub

        Public Function GetClientInterfaceName() As String Implements ICIPClientObject.GetClientInterfaceName
            GetClientInterfaceName = "IpfcSessionActionListener"
        End Function

        Public Sub OnAfterDirectoryChange(_Path As String) Implements IpfcSessionActionListener.OnAfterDirectoryChange
            asyncConnection.Session.UIShowMessageDialog("OnAfterDirectoryChange", Nothing)
        End Sub

        Public Sub OnAfterWindowChange(_NewWindow As Object) Implements IpfcSessionActionListener.OnAfterWindowChange
            asyncConnection.Session.UIShowMessageDialog("OnAfterWindowChange", Nothing)
        End Sub

        Public Sub OnAfterModelDisplay() Implements IpfcSessionActionListener.OnAfterModelDisplay
            asyncConnection.Session.UIShowMessageDialog("OnAfterModelDisplay", Nothing)
        End Sub

        Public Sub OnBeforeModelErase() Implements IpfcSessionActionListener.OnBeforeModelErase
            Dim dialogoption As IpfcMessageDialogOptions
            dialogoption = (New CCpfcMessageDialogOptions).Create()
            dialogoption.DialogLabel = "提示"
            dialogoption.MessageDialogType = EpfcMessageDialogType.EpfcMESSAGE_QUESTION
            dialogoption.Buttons = New CpfcMessageButtons
            dialogoption.Buttons.Append(EpfcMessageButton.EpfcMESSAGE_BUTTON_OK)
            dialogoption.Buttons.Append(EpfcMessageButton.EpfcMESSAGE_BUTTON_CANCEL)
            '这里做了点处理，点击cancel按钮会取消Erase事件
            If asyncConnection.Session.UIShowMessageDialog("监听到了要拭除当前模型!确定要拭除吗？", dialogoption) = 1 Then
                Dim cancelAction As CCpfcXCancelProEAction
                cancelAction = New CCpfcXCancelProEAction
                cancelAction.Throw()
            End If
        End Sub

        Public Sub OnBeforeModelDelete() Implements IpfcSessionActionListener.OnBeforeModelDelete
            asyncConnection.Session.UIShowMessageDialog("OnBeforeModelDelete", Nothing)
        End Sub

        Public Sub OnBeforeModelRename(_Container As IpfcDescriptorContainer2) Implements IpfcSessionActionListener.OnBeforeModelRename
            asyncConnection.Session.UIShowMessageDialog("OnBeforeModelRename", Nothing)
        End Sub

        Public Sub OnBeforeModelSave(_Container As IpfcDescriptorContainer) Implements IpfcSessionActionListener.OnBeforeModelSave
            asyncConnection.Session.UIShowMessageDialog("OnBeforeModelSave", Nothing)
        End Sub

        Public Sub OnBeforeModelPurge(_Container As IpfcDescriptorContainer) Implements IpfcSessionActionListener.OnBeforeModelPurge
            asyncConnection.Session.UIShowMessageDialog("OnBeforeModelPurge", Nothing)
        End Sub

        Public Sub OnBeforeModelCopy(_Container As IpfcDescriptorContainer2) Implements IpfcSessionActionListener.OnBeforeModelCopy
            asyncConnection.Session.UIShowMessageDialog("OnBeforeModelCopy", Nothing)
        End Sub

        Public Sub OnAfterModelPurge(_Desrc As IpfcModelDescriptor) Implements IpfcSessionActionListener.OnAfterModelPurge
            asyncConnection.Session.UIShowMessageDialog("OnAfterModelPurge", Nothing)
        End Sub
    End Class
End Module