Imports System.Configuration

Public Class Frm_load
    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
        End If
    End Sub

    Private Sub Frm_load_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '设置proe公差表文件的路径，在App.config增加新配置了
        Tol_path = ConfigurationManager.AppSettings("TolFilepath").ToString()
    End Sub

    Private Sub Btn_Symmetrical_Click(sender As Object, e As EventArgs) Handles Btn_Symmetrical.Click
        Symmetrical_tolerance（0.2）
    End Sub

    Private Sub Btn_Plusminus_Click(sender As Object, e As EventArgs) Handles Btn_PlusMinus.Click
        Plusminus_tolerance(0.2, -0.1)
    End Sub

    Private Sub Btn_TolTable_Click(sender As Object, e As EventArgs) Handles Btn_TolTable.Click
        Table_tolerance("h6", True)
    End Sub

    Private Sub Btn_TolFit_Click(sender As Object, e As EventArgs) Handles Btn_TolFit.Click
        Fit_tolerance("H7/h6", True)
    End Sub
End Class
