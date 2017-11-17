Public Class Frm_load
    Private Sub Btn_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_delRel.Enabled = True
            Btn_addRel.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接到CREO对话！")
        Else
            Btn_delRel.Enabled = True
            Btn_addRel.Enabled = True
        End If
    End Sub



    Private Sub Btn_delRel_Click(sender As Object, e As EventArgs) Handles Btn_delRel.Click
        DelRelations()
    End Sub

    Private Sub Btn_addRel_Click(sender As Object, e As EventArgs) Handles Btn_addRel.Click
        For Each line As String In Rtb_rel.Lines
            AddRelations(line)
        Next
        MessageBox.Show("关系已全部添加。")
    End Sub
End Class
