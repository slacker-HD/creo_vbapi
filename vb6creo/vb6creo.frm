VERSION 5.00
Begin VB.Form Frm_Load 
   Caption         =   "启动Creo"
   ClientHeight    =   3015
   ClientLeft      =   120
   ClientTop       =   465
   ClientWidth     =   4560
   LinkTopic       =   "vb6creo"
   MaxButton       =   0   'False
   ScaleHeight     =   3015
   ScaleWidth      =   4560
   StartUpPosition =   3  '窗口缺省
   Begin VB.CommandButton Cmd_Start 
      Caption         =   "点击启动Creo"
      Height          =   1575
      Left            =   1080
      TabIndex        =   0
      Top             =   1200
      Width           =   2295
   End
End
Attribute VB_Name = "Frm_Load"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
    Dim cAC As CCpfcAsyncConnection
    Dim asyncConnection As IpfcAsyncConnection

Private Sub Cmd_Start_Click()
    Set cAC = New CCpfcAsyncConnection
    Set asyncConnection = cAC.Start("C:\PTC\Creo 2.0\Parametric\bin\parametric.exe", "")
End Sub
