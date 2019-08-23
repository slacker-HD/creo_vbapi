Public Class Frm_load

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_retrieveExternalData.Enabled = True
            Btn_storeExternalData.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_retrieveExternalData.Enabled = True
            Btn_storeExternalData.Enabled = True
        End If
    End Sub

    Private Sub Btn_storeExternalData_Click(sender As Object, e As EventArgs) Handles Btn_storeExternalData.Click
        Dim table As New Hashtable From {
            {"Ref", RTB_ref.Text}
        }
        StoreExternalData(table, "IMI")
        MsgBox("数据已保存。")
    End Sub

    Private Sub Btn_retrieveExternalData_Click(sender As Object, e As EventArgs) Handles Btn_retrieveExternalData.Click
        Dim table As Hashtable
        table = RetrieveExternalData("IMI")
        If table.Count = 1 Then
            RTB_ref.Text = table.Item("Ref").ToString()
        Else
            RTB_ref.Text = ""
        End If
    End Sub
End Class