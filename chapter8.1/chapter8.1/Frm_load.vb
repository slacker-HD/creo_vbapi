Imports System.Configuration
Public Class Frm_load

    Private Sub Init()
        '开始监听事件，Full Asynchronous Mode需要
        EventProcess()
        Btn_AddModelListener.Enabled = True
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

    Private Sub Btn_AddModelListener_Click(sender As Object, e As EventArgs) Handles Btn_AddModelListener.Click
        AddSessionActionListener()
    End Sub
End Class