Public Class Frm_load
    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_rename.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_rename.Enabled = True
        End If
    End Sub

    Private Sub Btn_exporttoJpg_Click(sender As Object, e As EventArgs) Handles Btn_rename.Click
        RenamePrtandDrw("test")
    End Sub
End Class