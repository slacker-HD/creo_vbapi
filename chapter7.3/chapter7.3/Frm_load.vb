Imports System.Configuration

Public Class Frm_load
    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_noteNoLeader.Enabled = True
            Btn_noteWithLeader.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_noteNoLeader.Enabled = True
            Btn_noteWithLeader.Enabled = True
        End If
    End Sub

    Private Sub Frm_load_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '设置消息文件路径，在App.config增加新配置了
        Msg_file = ConfigurationManager.AppSettings("Messagefile").ToString()
    End Sub

    Private Sub Btn_noteNoLeader_Click(sender As Object, e As EventArgs) Handles Btn_noteNoLeader.Click
        CreateNoteWithoutLeader(Rtb_note.Text)
    End Sub

    Private Sub Btn_noteWithLeader_Click(sender As Object, e As EventArgs) Handles Btn_noteWithLeader.Click
        CreateNoteWithLeader(Rtb_note.Text)
    End Sub
End Class
