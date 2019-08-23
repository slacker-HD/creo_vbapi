<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_load
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_storeExternalData = New System.Windows.Forms.Button()
        Me.Btn_retrieveExternalData = New System.Windows.Forms.Button()
        Me.Btn_new = New System.Windows.Forms.Button()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RTB_ref = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_storeExternalData, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_retrieveExternalData, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(163, 350)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'Btn_storeExternalData
        '
        Me.Btn_storeExternalData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_storeExternalData.Enabled = False
        Me.Btn_storeExternalData.Location = New System.Drawing.Point(3, 264)
        Me.Btn_storeExternalData.Name = "Btn_storeExternalData"
        Me.Btn_storeExternalData.Size = New System.Drawing.Size(157, 83)
        Me.Btn_storeExternalData.TabIndex = 8
        Me.Btn_storeExternalData.Text = "写入数据"
        Me.Btn_storeExternalData.UseCompatibleTextRendering = True
        Me.Btn_storeExternalData.UseVisualStyleBackColor = True
        '
        'Btn_retrieveExternalData
        '
        Me.Btn_retrieveExternalData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_retrieveExternalData.Enabled = False
        Me.Btn_retrieveExternalData.Location = New System.Drawing.Point(3, 177)
        Me.Btn_retrieveExternalData.Name = "Btn_retrieveExternalData"
        Me.Btn_retrieveExternalData.Size = New System.Drawing.Size(157, 81)
        Me.Btn_retrieveExternalData.TabIndex = 7
        Me.Btn_retrieveExternalData.Text = "读取数据"
        Me.Btn_retrieveExternalData.UseCompatibleTextRendering = True
        Me.Btn_retrieveExternalData.UseVisualStyleBackColor = True
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(3, 3)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(157, 81)
        Me.Btn_new.TabIndex = 0
        Me.Btn_new.Text = "启动新会话"
        Me.Btn_new.UseVisualStyleBackColor = True
        '
        'Btn_Connect
        '
        Me.Btn_Connect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_Connect.Location = New System.Drawing.Point(3, 90)
        Me.Btn_Connect.Name = "Btn_Connect"
        Me.Btn_Connect.Size = New System.Drawing.Size(157, 81)
        Me.Btn_Connect.TabIndex = 1
        Me.Btn_Connect.Text = "连接现有会话"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(566, 356)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.RTB_ref, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(171, 2)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(393, 352)
        Me.TableLayoutPanel3.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(387, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "个性化备注说明（存储class名为IMI，slot名为Ref）："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RTB_ref
        '
        Me.RTB_ref.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RTB_ref.Location = New System.Drawing.Point(3, 17)
        Me.RTB_ref.Name = "RTB_ref"
        Me.RTB_ref.Size = New System.Drawing.Size(387, 332)
        Me.RTB_ref.TabIndex = 1
        Me.RTB_ref.Text = ""
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(566, 356)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Frm_load"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Btn_new As Button
    Friend WithEvents Btn_Connect As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Btn_retrieveExternalData As Button
    Friend WithEvents Btn_storeExternalData As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents RTB_ref As RichTextBox
End Class
