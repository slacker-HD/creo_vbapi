Imports System.Configuration

Public Class Frm_load

    Private Sub Btn_Connect_Click(sender As Object, e As EventArgs) Handles Btn_Connect.Click
        If Creo_Connect() <> True Then
            MsgBox("无法连接CREO对话！")
        Else
            Btn_symbolFree.Enabled = True
            Btn_symbolNomal.Enabled = True
            Btn_symbolwithleader.Enabled = True
        End If
    End Sub

    Private Sub Btn_new_Click(sender As Object, e As EventArgs) Handles Btn_new.Click
        If Creo_New() <> True Then
            MsgBox("无法新建CREO对话！")
        Else
            Btn_symbolFree.Enabled = True
            Btn_symbolNomal.Enabled = True
            Btn_symbolwithleader.Enabled = True
        End If
    End Sub

    Private Sub Frm_load_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '设置消息文件路径，在App.config增加新配置了
        Msg_file = ConfigurationManager.AppSettings("Messagefile").ToString()
        '设置符号文件目录，在App.config增加新配置了
        Symbolpath = ConfigurationManager.AppSettings("SymbolFilePath").ToString()
    End Sub

    Private Sub Btn_symbolFree_Click(sender As Object, e As EventArgs) Handles Btn_symbolFree.Click
        Dim Texts As New Dictionary(Of String, String) From {
            {"roughness_height", "6.3"}
        }
        PlaceSymbol("standard1", pfcls.EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_FREE, Texts)
    End Sub

    Private Sub Btn_symbolNomal_Click(sender As Object, e As EventArgs) Handles Btn_symbolNomal.Click
        Dim Texts As New Dictionary(Of String, String) From {
            {"roughness_height", "6.3"}
        }
        PlaceSymbol("standard1", pfcls.EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_NORMAL_TO_ITEM, Texts)
    End Sub

    Private Sub Btn_symbolwithleader_Click(sender As Object, e As EventArgs) Handles Btn_symbolwithleader.Click
        Dim Texts As New Dictionary(Of String, String) From {
            {"roughness_height", "6.3"}
            }
        PlaceSymbol("standard1", pfcls.EpfcSymbolDefAttachmentType.EpfcSYMDEFATTACH_RADIAL_LEADER, Texts)
    End Sub

End Class