Imports System.Configuration
Imports pfcls

Public Class Frm_load

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_layerCreate.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_layerCreate.Enabled = True
        End If
    End Sub

    Private Sub Frm_load_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '设置消息文件路径，在App.config增加新配置了
        Msg_file = ConfigurationManager.AppSettings("Messagefile").ToString()
        '设置符号文件目录，在App.config增加新配置了
        Symbolpath = ConfigurationManager.AppSettings("SymbolFilePath").ToString()
    End Sub

    Private Sub Btn_layerCreate_Click(sender As Object, e As EventArgs) Handles Btn_layerCreate.Click
        CreateLayers()
    End Sub
End Class