Public Class Frm_load
    Private Sub Btn_listFeat_Click(sender As Object, e As EventArgs) Handles Btn_listFeat.Click
        Rtb_featInfo.Text = FeatureTreeInfo()
    End Sub

    Private Sub Btn_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_listFeat.Enabled = True
            Btn_delFeat.Enabled = True
            Btn_suppressFeat.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接到CREO对话！")
        Else
            Btn_listFeat.Enabled = True
            Btn_delFeat.Enabled = True
            Btn_suppressFeat.Enabled = True
        End If
    End Sub

    Private Sub Btn_delFeat_Click(sender As Object, e As EventArgs) Handles Btn_delFeat.Click
        DeleteFeat()
    End Sub

    Private Sub Btn_suppressFeat_Click(sender As Object, e As EventArgs) Handles Btn_suppressFeat.Click
        SuppressFeat()
    End Sub
End Class
