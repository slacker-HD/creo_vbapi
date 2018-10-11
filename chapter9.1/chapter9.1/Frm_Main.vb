Public Class Frm_Main
    Private Sub Frm_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '初始化，必须步骤
        Axpview_Main.renderatstartup = "True"
        '不显示工具栏
        Axpview_Main.thumbnailView = "True"
    End Sub

    Private Sub Btn_OpenFile_Click(sender As Object, e As EventArgs) Handles Btn_OpenFile.Click
        If OFD.ShowDialog = DialogResult.OK Then
            '预览文件
            Axpview_Main.sourceUrl = OFD.FileName
        End If
    End Sub
End Class
