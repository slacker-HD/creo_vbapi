Imports System.Configuration
Imports pfcls

Public Class Frm_load

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_changeSheet.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_changeSheet.Enabled = True
        End If
    End Sub

    Private Sub Btn_GBBallon_Click(sender As Object, e As EventArgs) Handles Btn_changeSheet.Click

        OFD.Filter = "图框文件(*.frm)|*.frm"
        OFD.FilterIndex = 1
        If (OFD.ShowDialog() = DialogResult.OK) Then
            ChangeSheet(OFD.FileName)

        End If
    End Sub

End Class