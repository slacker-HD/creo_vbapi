Public Class Frm_load
    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_setCurrentModelMaterial.Enabled = True
            Btn_getCurrentModelMaterial.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_setCurrentModelMaterial.Enabled = True
            Btn_getCurrentModelMaterial.Enabled = True
        End If
    End Sub
    Private Sub Btn_retrieveCurrentModelMaterial_Click(sender As Object, e As EventArgs) Handles Btn_setCurrentModelMaterial.Click
        If (OFD.ShowDialog = DialogResult.OK) Then
            SetMaterial(IO.Path.GetDirectoryName(OFD.FileName), IO.Path.GetFileNameWithoutExtension(OFD.FileName))
        End If
    End Sub

    Private Sub Btn_getCurrentModelMaterial_Click(sender As Object, e As EventArgs) Handles Btn_getCurrentModelMaterial.Click
        MsgBox("当前模型包含材料：" + GetInnerMaterial() + Chr(13) + "设定材料为：" + GetMaterial())
    End Sub
End Class