Public Class Frm_load
    Private Sub Btn_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_open.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接到CREO对话！")
        Else
            Btn_open.Enabled = True
        End If
    End Sub

    Private Sub Btn_open_Click(sender As Object, e As EventArgs) Handles Btn_open.Click
        Openprt()
        Btn_save.Enabled = True
    End Sub

    Private Sub Btn_save_Click(sender As Object, e As EventArgs) Handles Btn_save.Click
        Savepart()
    End Sub

    Private Sub Btn_list_Click(sender As Object, e As EventArgs)

    End Sub
End Class
