Public Class Frm_tools

    Private Sub Btn_selExe_Click(sender As Object, e As EventArgs) Handles Btn_selExe.Click
        If (Ofd.ShowDialog = DialogResult.OK) Then
            Tb_exe.Text = Ofd.FileName
        End If
    End Sub

    Private Sub Btn_selInputDir_Click(sender As Object, e As EventArgs) Handles Btn_selInputDir.Click
        Fbd.ShowNewFolderButton = False
        If (Fbd.ShowDialog = DialogResult.OK) Then
            Tb_inputDir.Text = Fbd.SelectedPath
        End If
    End Sub

    Private Sub Btn_selOutputDir_Click(sender As Object, e As EventArgs) Handles Btn_selOutputDir.Click
        Fbd.ShowNewFolderButton = True
        If (Fbd.ShowDialog = DialogResult.OK) Then
            Tb_outputDir.Text = Fbd.SelectedPath
        End If
    End Sub

    Private Sub Btn_exportDwg_Click(sender As Object, e As EventArgs) Handles Btn_exportDwg.Click
        Dim p As New Process
        p.StartInfo.CreateNoWindow = True
        Try
            p.Start(Application.StartupPath + "\CreoExportDwg.exe", """" + Tb_exe.Text + """  """ + Tb_inputDir.Text + """  """ + Tb_outputDir.Text + """").WaitForExit()
            MessageBox.Show("转化完成。")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    Private Sub Btn_exportPdf_Click(sender As Object, e As EventArgs) Handles Btn_exportPdf.Click
        Dim p As New Process
        p.StartInfo.CreateNoWindow = True
        Try
            p.Start(Application.StartupPath + "\CreoDirExportPdf.exe", """" + Tb_exe.Text + """  """ + Tb_inputDir.Text + """  """ + Tb_outputDir.Text + """").WaitForExit()
            MessageBox.Show("转化完成。")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    Private Sub Btn_exportStep_Click(sender As Object, e As EventArgs) Handles Btn_exportStep.Click
        Dim p As New Process
        p.StartInfo.CreateNoWindow = True
        Try
            p.Start(Application.StartupPath + "\CreoDirExportStep.exe", """" + Tb_exe.Text + """  """ + Tb_inputDir.Text + """  """ + Tb_outputDir.Text + """").WaitForExit()
            MessageBox.Show("转化完成。")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    Private Sub Btn_exportIges_Click(sender As Object, e As EventArgs) Handles Btn_exportIges.Click
        Dim p As New Process
        p.StartInfo.CreateNoWindow = True
        Try
            p.Start(Application.StartupPath + "\CreoDirExportIges.exe", """" + Tb_exe.Text + """  """ + Tb_inputDir.Text + """  """ + Tb_outputDir.Text + """").WaitForExit()
            MessageBox.Show("转化完成。")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Class