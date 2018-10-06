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
    Public Sub MyEventProcess()
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

#Region "这里定义所有的ActionListener类"
    ''' <summary>
    ''' UICommandActionListener,定义点击按钮后的执行的操作
    ''' </summary>
    Private Class MyUICommandActionListener
        Implements IpfcUICommandActionListener
        Implements IpfcActionListener
        Implements ICIPClientObject

        Public Function GetClientInterfaceName() As String Implements ICIPClientObject.GetClientInterfaceName
            GetClientInterfaceName = "IpfcUICommandActionListener"
        End Function

        ''' <summary>
        ''' 点击按钮后的执行的操作
        ''' </summary>
        Public Sub OnCommand() Implements IpfcUICommandActionListener.OnCommand
            asyncConnection.Session.UIShowMessageDialog("触发了自定义命令。", Nothing)
        End Sub

    End Class

    ''' <summary>
    ''' 判断按钮是否可用的UICommandAccessListener，这里只是简单的判断必须打开的是Drawing才可用
    ''' </summary>
    Private Class MyUICommandAccessListener
        Implements ICIPClientObject
        Implements IpfcUICommandAccessListener
        Implements IpfcActionListener

        Dim asyncConnection As IpfcAsyncConnection

        Public Sub New(ByRef aC As IpfcAsyncConnection)
            asyncConnection = aC
        End Sub

        Public Function GetClientInterfaceName() As String Implements ICIPClientObject.GetClientInterfaceName
            GetClientInterfaceName = "IpfcUICommandAccessListener"
        End Function

        ''' <summary>
        ''' 判断按钮在当前会话是否可用，这里只是简单判断必须是打开PRT才可用
        ''' </summary>
        ''' <param name="_AllowErrorMessages"></param>
        ''' <returns></returns>
        Public Function OnCommandAccess(ByVal _AllowErrorMessages As Boolean) As Integer Implements IpfcUICommandAccessListener.OnCommandAccess
            Dim model As IpfcModel
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing OrElse (Not model.Type = EpfcModelType.EpfcMDL_DRAWING) Then
                Return EpfcCommandAccess.EpfcACCESS_UNAVAILABLE
            End If
            Return EpfcCommandAccess.EpfcACCESS_AVAILABLE
        End Function

    End Class

    ''' <summary>
    ''' IpfcAsyncConnection层级的listener
    ''' </summary>
    Private Class MyAsyncActionListener
        Implements IpfcAsyncActionListener
        Implements ICIPClientObject
        Implements IpfcActionListener

        Public Function GetClientInterfaceName() As String Implements ICIPClientObject.GetClientInterfaceName
            GetClientInterfaceName = "IpfcAsyncActionListener"
        End Function

        ''' <summary>
        ''' 如果Creo退出了，不管是不是正常退出，退出本程序。需要根据实际进行修改，这里简化了不考虑各种状态
        ''' </summary>
        ''' <param name="_Status"></param>
        Public Sub OnTerminate(_Status As Integer) Implements IpfcAsyncActionListener.OnTerminate
            Application.Exit()
        End Sub

    End Class

    ''' <summary>
    ''' IpfcPopupmenuListener类
    ''' </summary>
    Private Class MyPopupmenuListener
        Implements IpfcActionListener
        Implements ICIPClientObject
        Implements IpfcPopupmenuListener

        Dim asyncConnection As IpfcAsyncConnection

        Public Function GetClientInterfaceName() As String Implements ICIPClientObject.GetClientInterfaceName
            GetClientInterfaceName = "IpfcPopupmenuListener"
        End Function

        Public Sub New(ByRef aC As IpfcAsyncConnection)
            asyncConnection = aC
        End Sub
        ''' <summary>
        ''' 创建菜单，只有在Drawing中点击右键才能触发
        ''' </summary>
        ''' <param name="_Menu"></param>
        Public Sub OnPopupmenuCreate(ByVal _Menu As IpfcPopupmenu) Implements IpfcPopupmenuListener.OnPopupmenuCreate
            Dim command As IpfcUICommand
            Dim options As IpfcPopupmenuOptions
            Dim cmdString As String
            Dim helpString As String
            'Drawing的右键菜单名，调试时在这里打断点监视_Menu.Name可以得到
            If _Menu.Name = "Drawing Popup Menu" Then
                command = asyncConnection.Session.UIGetCommand("TEST2")
                If Not command Is Nothing Then
                    options = (New CCpfcPopupmenuOptions).Create("POPUPTEST2")
                    cmdString = asyncConnection.Session.GetMessageContents(Msg_file, "MyPushButton", Nothing)
                    helpString = asyncConnection.Session.GetMessageContents(Msg_file, "MyPushButtonHelp", Nothing)
                    options.Helptext = helpString
                    options.Label = cmdString
                    _Menu.AddButton(command, options)
                Else
                    asyncConnection.Session.UIShowMessageDialog("无法启动自定义命令。", Nothing)
                End If
            End If
        End Sub
    End Class

#End Region

    ''' <summary>
    ''' 添加一个菜单项
    ''' </summary>
    Public Sub AddPushButton()
        Dim UICommand As IpfcUICommand
        Dim UICommandActionListener As IpfcUICommandActionListener
        Dim UICommandAccessListener As IpfcUICommandAccessListener
        Dim AsyncActionListener As IpfcAsyncActionListener
        Try
            '整个过程与Toolkit添加菜单按钮的过程类似
            '创建IpfcUICommandActionListener对象
            UICommandActionListener = New MyUICommandActionListener()
            '添加Command
            UICommand = asyncConnection.Session.UICreateCommand("TEST1", UICommandActionListener)
            '创建UICommandAccessListener
            UICommandAccessListener = New MyUICommandAccessListener(asyncConnection)
            '判断按钮是否可用
            UICommand.AddActionListener(UICommandAccessListener)
            '添加自定义菜单按钮
            asyncConnection.Session.UIAddButton(UICommand, "Windows", Nothing, "MyPushButton", "MyPushButtonHelp", Msg_file)
            '设定pfcAsyncConnection层级的listener，必须有，不然会死
            AsyncActionListener = New MyAsyncActionListener()

            asyncConnection.AddActionListener(AsyncActionListener)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 添加一个导航器栏
    ''' </summary>
    ''' <param name="name">导航器栏名称</param>
    ''' <param name="icon_path">图标</param>
    ''' <param name="url">网址</param>
    Public Sub AddNavPane(ByVal name As String, ByVal icon_path As String, ByVal url As String)
        Try
            If (Not IO.File.Exists(icon_path)) Then
                icon_path = Nothing
            End If
            asyncConnection.Session.NavigatorPaneBrowserAdd(name, icon_path, url)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' 添加一个右键菜单项
    ''' </summary>
    Public Sub AddPopupMenu()
        Dim UICommand As IpfcUICommand
        Dim UICommandActionListener As IpfcUICommandActionListener
        Dim PopupmenuListener As IpfcPopupmenuListener
        Dim UICommandAccessListener As IpfcUICommandAccessListener
        Dim AsyncActionListener As IpfcAsyncActionListener
        Try
            '整个过程与Toolkit添加右键菜单按钮的过程类似
            '以下添加按钮，各函数的参数与toolkit的ProMenubarmenuPushbuttonAdd类似
            UICommandActionListener = New MyUICommandActionListener()
            '添加Command
            UICommand = asyncConnection.Session.UICreateCommand("TEST2", UICommandActionListener)
            '创建UICommandAccessListener
            UICommandAccessListener = New MyUICommandAccessListener(asyncConnection)
            '判断按钮是否可用
            UICommand.AddActionListener(UICommandAccessListener)
            '添加自定义菜单按钮
            asyncConnection.Session.UIAddButton(UICommand, "ActionMenu", Nothing, "MyPopupButton", "MyPopupButtonHelp", Msg_file)
            '添加右键菜单菜弹出规则
            PopupmenuListener = New MyPopupmenuListener(asyncConnection)
            asyncConnection.Session.AddActionListener(PopupmenuListener)

            '设定pfcAsyncConnection层级的listener，必须有，不然会死
            AsyncActionListener = New MyAsyncActionListener()
            asyncConnection.AddActionListener(AsyncActionListener)
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

End Module