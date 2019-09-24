Public Class Frm_load

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_retrieveFamtab.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_retrieveFamtab.Enabled = True
        End If
    End Sub

    Private Sub Btn_retrieveFamtab_Click(sender As Object, e As EventArgs) Handles Btn_retrieveFamtab.Click
        Dim FamColSymbols As ArrayList
        Dim RowCount As Integer
        Dim Rows As ArrayList
        Dim i, j As Integer
        DGV_famtab.Columns.Clear()
        FamColSymbols = GetFamColSymbols()
        If FamColSymbols IsNot Nothing Then
            RowCount = GetFamRowCount()
            For i = 0 To FamColSymbols.Count - 1
                DGV_famtab.Columns.Add(FamColSymbols.Item(i), FamColSymbols.Item(i))
            Next

            For i = 0 To RowCount - 1
                DGV_famtab.Rows.Add()
                Rows = GetFamRow(i)
                For j = 0 To Rows.Count - 1
                    DGV_famtab.Rows(i).Cells(j).Value = Rows.Item(j)
                Next
            Next
        End If
    End Sub
End Class