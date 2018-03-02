Imports System.Configuration
Imports pfcls

Public Class Frm_load

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_GBBallon.Enabled = True
            Btn_horizonBalloon.Enabled = True
            Btn_verticalBalloon.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_GBBallon.Enabled = True
            Btn_horizonBalloon.Enabled = True
            Btn_verticalBalloon.Enabled = True
        End If
    End Sub

    Private Sub Btn_GBBallon_Click(sender As Object, e As EventArgs) Handles Btn_GBBallon.Click
        ChangeSheet("D:\CETC38HFUT\图框\国家标准\htz_wf5_ecriee_a0_m_a.frm")
    End Sub

    Private Sub Btn_horizonBalloon_Click(sender As Object, e As EventArgs) Handles Btn_horizonBalloon.Click
        CreateLine()
    End Sub
End Class