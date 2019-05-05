Imports pfcls
Imports System.Configuration

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
            Creo_New = True
        Catch ex As Exception
            Creo_New = False
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Function

    ''' <summary>
    ''' 选择特征
    ''' </summary>
    Public Sub SelectFeat()
        Dim selectionOptions As IpfcSelectionOptions
        Dim selections As CpfcSelections
        Dim selectFeats As IpfcSelection
        Dim selectedfeat As IpfcModelItem
        Try
            '初始化selection选项
            selectionOptions = (New CCpfcSelectionOptions).Create("feature") '设置可选特征的类型，这里为特征对象
            selectionOptions.MaxNumSels = 1 '设置一次可选择特征的数量
            selections = asyncConnection.Session.Select(selectionOptions, Nothing)
            '确定选择了一个对象
            If selections.Count > 0 Then
                selectFeats = selections.Item(0)
                selectedfeat = selectFeats.SelItem
                MessageBox.Show(String.Format("内部特征ID : {0}", selectedfeat.Id))
            End If
            '使用函数刷新，也很简单
            asyncConnection.Session.CurrentWindow.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
    Public Sub Selectionget()
        Dim Selections As IpfcSelections
        Selections = asyncConnection.Session.CurrentSelectionBuffer.Contents
        Try
            If (Selections.Count > 0) Then
                For i = 0 To Selections.Count - 1
                    MessageBox.Show(String.Format("内部特征ID : {0}", Selections.Item(i).SelItem.Id))
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Module
