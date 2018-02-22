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
        Me.Btn_noteWithLeader = New System.Windows.Forms.Button()
        Me.Btn_noteNoLeader = New System.Windows.Forms.Button()
        Me.Btn_new = New System.Windows.Forms.Button()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Rtb_note = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_noteWithLeader, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_noteNoLeader, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 4)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(4, 4)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(306, 317)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'Btn_noteWithLeader
        '
        Me.Btn_noteWithLeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_noteWithLeader.Enabled = False
        Me.Btn_noteWithLeader.Location = New System.Drawing.Point(4, 193)
        Me.Btn_noteWithLeader.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_noteWithLeader.Name = "Btn_noteWithLeader"
        Me.Btn_noteWithLeader.Size = New System.Drawing.Size(298, 55)
        Me.Btn_noteWithLeader.TabIndex = 3
        Me.Btn_noteWithLeader.Text = "插入有引线的注解"
        Me.Btn_noteWithLeader.UseVisualStyleBackColor = True
        '
        'Btn_noteNoLeader
        '
        Me.Btn_noteNoLeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_noteNoLeader.Enabled = False
        Me.Btn_noteNoLeader.Location = New System.Drawing.Point(4, 130)
        Me.Btn_noteNoLeader.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_noteNoLeader.Name = "Btn_noteNoLeader"
        Me.Btn_noteNoLeader.Size = New System.Drawing.Size(298, 55)
        Me.Btn_noteNoLeader.TabIndex = 2
        Me.Btn_noteNoLeader.Text = "插入自由放置的注解"
        Me.Btn_noteNoLeader.UseVisualStyleBackColor = True
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(4, 4)
        Me.Btn_new.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(298, 55)
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
        Me.Btn_Connect.Size = New System.Drawing.Size(298, 55)
        Me.Btn_Connect.TabIndex = 1
        Me.Btn_Connect.Text = "连接现有会话"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 252)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(300, 65)
        Me.Label1.TabIndex = 6
        Me.Label1.Tag = ""
        Me.Label1.Text = "请将工程中msg.txt文件放置到App.config文件对应正确的位置！"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Rtb_note, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(628, 325)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'Rtb_note
        '
        Me.Rtb_note.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Rtb_note.Location = New System.Drawing.Point(317, 3)
        Me.Rtb_note.Name = "Rtb_note"
        Me.Rtb_note.Size = New System.Drawing.Size(308, 319)
        Me.Rtb_note.TabIndex = 6
        Me.Rtb_note.Text = "技术要求" & Global.Microsoft.VisualBasic.ChrW(10) & "1.焊后加工" & Global.Microsoft.VisualBasic.ChrW(10) & "2.退火去应力"
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 325)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "Frm_load"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Btn_new As Button
    Friend WithEvents Btn_Connect As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Btn_noteWithLeader As Button
    Friend WithEvents Btn_noteNoLeader As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Rtb_note As RichTextBox
End Class
