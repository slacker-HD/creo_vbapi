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
    ''' 根据model包含的external对象以及class名查找并返回IpfcExternalDataClass数据
    ''' </summary>
    ''' <param name="dataAccess">external data对象</param>
    ''' <param name="className">class名</param>
    ''' <returns>以此class名存储的IpfcExternalDataClass数据</returns>
    Private Function GetClassByName(ByVal dataAccess As IpfcExternalDataAccess, ByVal className As String) As IpfcExternalDataClass
        Dim classes As IpfcExternalDataClasses
        Dim i As Integer
        classes = dataAccess.ListClasses()
        For i = 0 To classes.Count - 1
            If classes.Item(i).Name = className Then
                Return (classes.Item(i))
            End If
        Next
        Return Nothing
    End Function
    ''' <summary>
    ''' 获取数据
    ''' </summary>
    ''' <param name="className">class名</param>
    ''' <returns>包含类+类名的一组数据</returns>
    Public Function RetrieveExternalData(ByVal className As String) As Hashtable
        Dim model As IpfcModel
        Dim dataAccess As IpfcExternalDataAccess
        Dim dataClass As IpfcExternalDataClass
        Dim slots As IpfcExternalDataSlots
        Dim i As Integer
        Dim table As Hashtable
        Dim value As Object
        Dim data As IpfcExternalData
        Dim slot As IpfcExternalDataSlot

        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                Return Nothing
            End If
            table = New Hashtable
            '获取external data的对象
            dataAccess = model.AccessExternalData()
            '获取以className名存储的数据
            dataClass = GetClassByName(dataAccess, className)
            If Not dataClass Is Nothing Then
                slots = dataClass.ListSlots()
                '枚举所有slot
                For i = 0 To slots.Count - 1
                    value = Nothing
                    slot = slots.Item(i)
                    data = slot.Value
                    '根据类型判断slot的值
                    Select Case data.discr
                        Case EpfcExternalDataType.EpfcEXTDATA_STRING
                            value = CType(data.StringValue, Object)'装箱保持数据类型一致
                        Case EpfcExternalDataType.EpfcEXTDATA_INTEGER
                            value = CType(data.IntegerValue, Object)'装箱保持数据类型一致
                        Case EpfcExternalDataType.EpfcEXTDATA_DOUBLE
                            value = CType(data.DoubleValue, Object) '装箱保持数据类型一致
                    End Select
                    '用Hashtable记录数据
                    table.Add(slot.Name, value)
                Next
            End If
            Return table
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 根据class和slot名查找值
    ''' </summary>
    ''' <param name="extClass">class</param>
    ''' <param name="slotName">slot名</param>
    ''' <returns>extClass对应slot名的IpfcExternalDataSlots对象，如果不存在则返回Nothing</returns>
    Private Function GetSlotByName(ByVal extClass As IpfcExternalDataClass, ByVal slotName As String) As IpfcExternalDataSlot
        Dim extSlots As IpfcExternalDataSlots
        Dim i As Integer
        extSlots = extClass.ListSlots()
        For i = 0 To extSlots.Count - 1
            If extSlots.Item(i).Name = slotName Then
                Return (extSlots.Item(i))
            End If
        Next
        Return Nothing
    End Function


    ''' <summary>
    ''' 存储数据
    ''' </summary>
    ''' <param name="table">包含类+类名的一组数据</param>
    ''' <param name="className">class名</param>
    Public Sub StoreExternalData(ByVal table As Hashtable, ByVal className As String)
        Dim model As IpfcModel
        Dim dataAccess As IpfcExternalDataAccess
        Dim dataClass As IpfcExternalDataClass
        Dim row As DictionaryEntry
        Dim value As Object
        Dim data As IpfcExternalData
        Dim slot As IpfcExternalDataSlot
        Try
            model = asyncConnection.Session.CurrentModel
            If model Is Nothing Then
                Return
            End If
            '获取external data的对象
            dataAccess = model.AccessExternalData()
            '获取以className名存储的数据
            dataClass = GetClassByName(dataAccess, className)
            '如果没有就新建一个class
            If dataClass Is Nothing Then
                dataClass = dataAccess.CreateClass(className)
            End If
            '循环更新或写入slots
            For Each row In table
                value = row.Value
                '根据类型初始化ExternalData
                If value.GetType.ToString = "System.Int16" Or value.GetType.ToString = "System.Int32" Or value.GetType.ToString = "System.Byte" Then
                    data = (New CMpfcExternal).CreateIntExternalData(value)
                ElseIf value.GetType Is Type.GetType("System.Double") Then
                    data = (New CMpfcExternal).CreateDoubleExternalData(value)
                Else
                    data = (New CMpfcExternal).CreateStringExternalData(value.ToString)
                End If
                '判断是否存在该slot
                slot = GetSlotByName(dataClass, row.Key.ToString)
                '不存在则新建
                If slot Is Nothing Then
                    slot = dataClass.CreateSlot(row.Key.ToString)
                End If
                '更新值
                slot.Value = data
            Next
            model.Save()
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub


End Module