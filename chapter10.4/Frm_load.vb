Public Class Frm_load
    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_exporttoJpg.Enabled = True
            Btn_exporttoBmp.Enabled = True
            Btn_exporttoTif.Enabled = True
            Btn_exporttoEps.Enabled = True
        End If
    End Sub

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_exporttoJpg.Enabled = True
            Btn_exporttoBmp.Enabled = True
            Btn_exporttoTif.Enabled = True
            Btn_exporttoEps.Enabled = True
        End If
    End Sub

    Private Sub Btn_exporttoJpg_Click(sender As Object, e As EventArgs) Handles Btn_exporttoJpg.Click
        SFD.Filter = "jpg图像(* .jpg)|*.jpg"
        If (SFD.ShowDialog = DialogResult.OK) Then
            ExporttoImg(SFD.FileName, ExportType.jpg)
        End If
    End Sub

    Private Sub Btn_exporttoBmp_Click(sender As Object, e As EventArgs) Handles Btn_exporttoBmp.Click
        SFD.Filter = "bmp图像(* .bmp)|*.bmp"
        If (SFD.ShowDialog = DialogResult.OK) Then
            ExporttoImg(SFD.FileName, ExportType.bmp)
        End If
    End Sub

    Private Sub Btn_exporttoTif_Click(sender As Object, e As EventArgs) Handles Btn_exporttoTif.Click
        SFD.Filter = "tif图像(* .tif)|*.tif"
        If (SFD.ShowDialog = DialogResult.OK) Then
            ExporttoImg(SFD.FileName, ExportType.tif)
        End If
    End Sub

    Private Sub Btn_exporttoEps_Click(sender As Object, e As EventArgs) Handles Btn_exporttoEps.Click
        SFD.Filter = "eps图像(* .eps)|*.eps"
        If (SFD.ShowDialog = DialogResult.OK) Then
            ExporttoImg(SFD.FileName, ExportType.eps)
        End If
    End Sub
End Class