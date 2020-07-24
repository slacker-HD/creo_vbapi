Public Class Frm_load
    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_calcOutline.Enabled = True
            Btn_calcOutlineCustom.Enabled = True

        End If
    End Sub

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_calcOutline.Enabled = True
            Btn_calcOutlineCustom.Enabled = True
        End If
    End Sub

    Private Sub Btn_calcOutline_Click(sender As Object, e As EventArgs) Handles Btn_calcOutline.Click
        Dim outline() As Double = CurrentOutline()
        MessageBox.Show("x:" + outline(0).ToString() + Chr(13) + "y:" + outline(1).ToString() + Chr(13) + "z:" + outline(2).ToString())
    End Sub

    Private Sub Btn_calcOutlineCustom_Click(sender As Object, e As EventArgs) Handles Btn_calcOutlineCustom.Click
        Dim outline() As Double = CurrentOutlineCustom()
        MessageBox.Show("x:" + outline(0).ToString() + Chr(13) + "y:" + outline(1).ToString() + Chr(13) + "z:" + outline(2).ToString())
    End Sub
End Class