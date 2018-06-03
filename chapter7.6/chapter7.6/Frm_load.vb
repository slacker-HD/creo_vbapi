Public Class Frm_load

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_listTables.Enabled = True
            Btn_setTableCell.Enabled = True
            Btn_getTableCell.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_listTables.Enabled = True
            Btn_setTableCell.Enabled = True
            Btn_getTableCell.Enabled = True
        End If
    End Sub

    Private Sub Btn_listTables_Click(sender As Object, e As EventArgs) Handles Btn_listTables.Click
        MessageBox.Show(TablesInfo())
    End Sub

    Private Sub Btn_setTableCell_Click(sender As Object, e As EventArgs) Handles Btn_setTableCell.Click
        SetTableInfo("这是修改后的文字", 1, 1)
    End Sub

    Private Sub Btn_getTableCell_Click(sender As Object, e As EventArgs) Handles Btn_getTableCell.Click
        MessageBox.Show(GetTableInfo(2, 1))
    End Sub

End Class