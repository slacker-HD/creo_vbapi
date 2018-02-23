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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_symbolwithleader = New System.Windows.Forms.Button()
        Me.Btn_symbolNomal = New System.Windows.Forms.Button()
        Me.Btn_symbolFree = New System.Windows.Forms.Button()
        Me.Btn_new = New System.Windows.Forms.Button()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_symbolwithleader, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_symbolNomal, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_symbolFree, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(4, 4)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 6
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(383, 318)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 260)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(377, 58)
        Me.Label1.TabIndex = 8
        Me.Label1.Tag = ""
        Me.Label1.Text = "请将工程中msg.txt文件以及standard1.sym放置到App.config文件对应正确的位置！"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Btn_symbolwithleader
        '
        Me.Btn_symbolwithleader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_symbolwithleader.Enabled = False
        Me.Btn_symbolwithleader.Location = New System.Drawing.Point(4, 212)
        Me.Btn_symbolwithleader.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_symbolwithleader.Name = "Btn_symbolwithleader"
        Me.Btn_symbolwithleader.Size = New System.Drawing.Size(375, 44)
        Me.Btn_symbolwithleader.TabIndex = 7
        Me.Btn_symbolwithleader.Text = "插入带引线的粗糙度符号，设置值为6.3"
        Me.Btn_symbolwithleader.UseVisualStyleBackColor = True
        '
        'Btn_symbolNomal
        '
        Me.Btn_symbolNomal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_symbolNomal.Enabled = False
        Me.Btn_symbolNomal.Location = New System.Drawing.Point(4, 160)
        Me.Btn_symbolNomal.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_symbolNomal.Name = "Btn_symbolNomal"
        Me.Btn_symbolNomal.Size = New System.Drawing.Size(375, 44)
        Me.Btn_symbolNomal.TabIndex = 3
        Me.Btn_symbolNomal.Text = "插入垂直于表面的粗糙度符号，设置值为6.3"
        Me.Btn_symbolNomal.UseVisualStyleBackColor = True
        '
        'Btn_symbolFree
        '
        Me.Btn_symbolFree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_symbolFree.Enabled = False
        Me.Btn_symbolFree.Location = New System.Drawing.Point(4, 108)
        Me.Btn_symbolFree.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_symbolFree.Name = "Btn_symbolFree"
        Me.Btn_symbolFree.Size = New System.Drawing.Size(375, 44)
        Me.Btn_symbolFree.TabIndex = 2
        Me.Btn_symbolFree.Text = "插入自由放置的粗糙度符号，设置值为6.3"
        Me.Btn_symbolFree.UseVisualStyleBackColor = True
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(4, 4)
        Me.Btn_new.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(375, 44)
        Me.Btn_new.TabIndex = 0
        Me.Btn_new.Text = "启动新会话"
        Me.Btn_new.UseVisualStyleBackColor = True
        '
        'Btn_Connect
        '
        Me.Btn_Connect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_Connect.Location = New System.Drawing.Point(4, 56)
        Me.Btn_Connect.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Connect.Name = "Btn_Connect"
        Me.Btn_Connect.Size = New System.Drawing.Size(375, 44)
        Me.Btn_Connect.TabIndex = 1
        Me.Btn_Connect.Text = "连接现有会话"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(391, 326)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 326)
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
    Friend WithEvents Btn_symbolNomal As Button
    Friend WithEvents Btn_symbolFree As Button
    Friend WithEvents Btn_symbolwithleader As Button
    Friend WithEvents Label1 As Label
End Class
