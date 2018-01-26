Public Class Frm_load
    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
        End If
    End Sub

    Private Sub Btn_Prefix_Click(sender As Object, e As EventArgs) Handles Btn_Prefix.Click
        Modify_text("4-", "", "")
    End Sub

    Private Sub Btn_Surffix_Click(sender As Object, e As EventArgs) Handles Btn_Surffix.Click
        Modify_text("", "-6H", "")

    End Sub

    Private Sub Btn_DownText_Click(sender As Object, e As EventArgs) Handles Btn_DownText.Click
        Modify_text("", "", "跨中均布")
    End Sub
End Class
