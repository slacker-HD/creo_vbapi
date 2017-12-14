Imports pfcls

Public Class Frm_load
    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_assemble.Enabled = True
            Btn_constrainsAdd.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_assemble.Enabled = True
            Btn_constrainsAdd.Enabled = True
        End If
    End Sub

    Private Sub Btn_assemble_Click(sender As Object, e As EventArgs) Handles Btn_assemble.Click
        InsertComp()
    End Sub

    Private Sub Btn_constrainsAdd_Click(sender As Object, e As EventArgs) Handles Btn_constrainsAdd.Click
        SelectFeat()
    End Sub
End Class
