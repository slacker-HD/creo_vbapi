Public Class Frm_load
    Private Sub Btn_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_exportDwg.Enabled = True
            Btn_exportPdf.Enabled = True
            Btn_exportStp.Enabled = True
            Btn_exportIgs.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接到CREO对话！")
        Else
            Btn_exportDwg.Enabled = True
            Btn_exportPdf.Enabled = True
            Btn_exportStp.Enabled = True
            Btn_exportIgs.Enabled = True
        End If
    End Sub

    Private Sub Btn_exportDwg_Click(sender As Object, e As EventArgs) Handles Btn_exportDwg.Click
        ConvertToDwg()
    End Sub

    Private Sub Btn_exportPdf_Click(sender As Object, e As EventArgs) Handles Btn_exportPdf.Click
        ConvertToPdf()
    End Sub

    Private Sub Btn_exportStp_Click(sender As Object, e As EventArgs) Handles Btn_exportStp.Click
        ConvertToStp()
    End Sub

    Private Sub Btn_exportPlt_Click(sender As Object, e As EventArgs) Handles Btn_exportIgs.Click
        ConvertToIgs()
    End Sub
End Class
