Public Class Frm_tools

    Private Sub Btn_selExe_Click(sender As Object, e As EventArgs) Handles Btn_selExe.Click
        Ofd.FileName = "parametric.exe"
        Ofd.Filter = "(parametric.exe)|parametric.exe"
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

    Private Sub Btn_selRelFile_Click(sender As Object, e As EventArgs) Handles Btn_selRelFile.Click
        Ofd.FileName = ""
        Ofd.Filter = "(*.txt)|*.txt"
        If (Ofd.ShowDialog = DialogResult.OK) Then
            Tb_relFile.Text = Ofd.FileName
        End If
    End Sub

    Private Sub Btn_addRels_Click(sender As Object, e As EventArgs) Handles Btn_addRels.Click
        Dim p As New Process
        p.StartInfo.CreateNoWindow = True
        Try
            p.Start(Application.StartupPath + "\CreoDirRelAdd.exe", """" + Tb_exe.Text + """" + " """ + Tb_inputDir.Text + """" + " """ + Tb_relFile.Text + """").WaitForExit()
            MessageBox.Show("批量导入完成。")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub

    Private Sub Btn_clearRels_Click(sender As Object, e As EventArgs) Handles Btn_clearRels.Click
        Dim p As New Process
        p.StartInfo.CreateNoWindow = True
        Try
            p.Start(Application.StartupPath + "\CreoDirRelClear.exe", """" + Tb_exe.Text + """" + " """ + Tb_inputDir.Text + """").WaitForExit()
            MessageBox.Show("批量清空完成。")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString + Chr(13) + ex.StackTrace.ToString)
        End Try
    End Sub
End Class