Public Class Frm_load
    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_changeView.Enabled = True
            Btn_changeMatrix.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_changeView.Enabled = True
            Btn_changeMatrix.Enabled = True
        End If
    End Sub

    Private Sub Btn_changeView_Click(sender As Object, e As EventArgs) Handles Btn_changeView.Click
        RotateView(pfcls.EpfcCoordAxis.EpfcCOORD_AXIS_Z, 90)
    End Sub

    Private Sub Btn_changeMatrix_Click(sender As Object, e As EventArgs) Handles Btn_changeMatrix.Click
        ChangeToFrontView()
    End Sub
End Class