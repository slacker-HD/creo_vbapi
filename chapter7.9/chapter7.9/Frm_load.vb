Imports System.Configuration
Imports pfcls

Public Class Frm_load

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_CreateLine.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_CreateLine.Enabled = True
        End If
    End Sub

    Private Sub Btn_CreateLine_Click(sender As Object, e As EventArgs) Handles Btn_CreateLine.Click
        CreateLine()
    End Sub
End Class