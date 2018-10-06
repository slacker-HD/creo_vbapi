Imports System.Configuration

Public Class Frm_load
    Private Sub Init()
        '开始监听事件，Full Asynchronous Mode需要
        MyEventProcess()
        Btn_AddButton.Enabled = True
        Btn_AddNavButton.Enabled = True
        Btn_AddPopupMenu.Enabled = True
    End Sub
    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Init()
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Init()
        End If
    End Sub

    Private Sub Frm_load_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '设置消息文件路径，在App.config增加新配置了
        Msg_file = ConfigurationManager.AppSettings("Messagefile").ToString()
        '设置导航器栏地址，在App.config增加新配置了
        Url = ConfigurationManager.AppSettings("Url").ToString()
        '设置导航器栏图标，在App.config增加新配置了
        IconFile = ConfigurationManager.AppSettings("IconFile").ToString()
    End Sub

    Private Sub Btn_AddButton_Click(sender As Object, e As EventArgs) Handles Btn_AddButton.Click
        AddPushButton()
    End Sub

    Private Sub Btn_AddNavButton_Click(sender As Object, e As EventArgs) Handles Btn_AddNavButton.Click
        AddNavPane("我的博客", IconFile, Url)
    End Sub

    Private Sub Btn_AddPopupMenu_Click(sender As Object, e As EventArgs) Handles Btn_AddPopupMenu.Click
        AddPopupMenu()
    End Sub
End Class