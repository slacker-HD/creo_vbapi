Public Class Frm_load
    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_exporttoJpg.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_exporttoJpg.Enabled = True
        End If
    End Sub

    Private Sub Btn_exporttoJpg_Click(sender As Object, e As EventArgs) Handles Btn_exporttoJpg.Click
        If (SFD.ShowDialog = DialogResult.OK) Then
            ExporttoJpg(SFD.FileName)
        End If
    End Sub
End Class