Public Class Frm_load
    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_globalInterferenceInfo.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_globalInterferenceInfo.Enabled = True
        End If
    End Sub

    Private Sub Btn_globalInterferenceInfo_Click(sender As Object, e As EventArgs) Handles Btn_globalInterferenceInfo.Click
        Rtb_info.Text = GetGlobalInterferences()
    End Sub

    Private Sub Btn_interferenceSel_Click(sender As Object, e As EventArgs) Handles Btn_interferenceSel.Click
        MessageBox.Show(GetSelInterferences())
    End Sub
End Class
