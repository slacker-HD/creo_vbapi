Public Class Frm_load
    Private Sub Btn_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_exportDwg.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接到CREO对话！")
        Else
            Btn_exportDwg.Enabled = True
        End If
    End Sub

    Private Sub Btn_exportDwg_Click(sender As Object, e As EventArgs) Handles Btn_exportDwg.Click
        ConvertToDwg()
    End Sub
End Class
