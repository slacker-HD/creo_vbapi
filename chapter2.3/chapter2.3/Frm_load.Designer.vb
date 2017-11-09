<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_load
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Btn_new = New System.Windows.Forms.Button()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_select = New System.Windows.Forms.Button()
        Me.Btn_selectionget = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(4, 4)
        Me.Btn_new.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(274, 55)
        Me.Btn_new.TabIndex = 0
        Me.Btn_new.Text = "启动新会话"
        Me.Btn_new.UseVisualStyleBackColor = True
        '
        'Btn_Connect
        '
        Me.Btn_Connect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_Connect.Location = New System.Drawing.Point(4, 67)
        Me.Btn_Connect.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Connect.Name = "Btn_Connect"
        Me.Btn_Connect.Size = New System.Drawing.Size(274, 55)
        Me.Btn_Connect.TabIndex = 1
        Me.Btn_Connect.Text = "连接现有会话"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_select, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_selectionget, 0, 3)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(282, 253)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'Btn_select
        '
        Me.Btn_select.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_select.Enabled = False
        Me.Btn_select.Location = New System.Drawing.Point(3, 129)
        Me.Btn_select.Name = "Btn_select"
        Me.Btn_select.Size = New System.Drawing.Size(276, 57)
        Me.Btn_select.TabIndex = 5
        Me.Btn_select.Text = "主动选择特征"
        Me.Btn_select.UseVisualStyleBackColor = True
        '
        'Btn_selectionget
        '
        Me.Btn_selectionget.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_selectionget.Enabled = False
        Me.Btn_selectionget.Location = New System.Drawing.Point(3, 192)
        Me.Btn_selectionget.Name = "Btn_selectionget"
        Me.Btn_selectionget.Size = New System.Drawing.Size(276, 58)
        Me.Btn_selectionget.TabIndex = 6
        Me.Btn_selectionget.Text = "读取已选择特征"
        Me.Btn_selectionget.UseVisualStyleBackColor = True
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(282, 253)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Name = "Frm_load"
        Me.Text = "启动界面"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Btn_new As Button
    Friend WithEvents Btn_Connect As Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Btn_select As Button
    Friend WithEvents Btn_selectionget As Button
End Class
