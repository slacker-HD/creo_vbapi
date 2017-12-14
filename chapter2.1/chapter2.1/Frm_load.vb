Public Class Frm_load
    Private Sub Btn_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        End If
    End Sub

    Private Sub Btn_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        End If
    End Sub
End Class
