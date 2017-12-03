<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_tools
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tb_relFile = New System.Windows.Forms.TextBox()
        Me.Btn_selRelFile = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Tb_inputDir = New System.Windows.Forms.TextBox()
        Me.Btn_selInputDir = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Tb_exe = New System.Windows.Forms.TextBox()
        Me.Btn_selExe = New System.Windows.Forms.Button()
        Me.Ofd = New System.Windows.Forms.OpenFileDialog()
        Me.Fbd = New System.Windows.Forms.FolderBrowserDialog()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_addRels = New System.Windows.Forms.Button()
        Me.Btn_clearRels = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel5, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(841, 153)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Tb_relFile, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Btn_selRelFile, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 79)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(835, 32)
        Me.TableLayoutPanel4.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(194, 32)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "导入关系文件(UTF8编码)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tb_relFile
        '
        Me.Tb_relFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_relFile.Location = New System.Drawing.Point(203, 3)
        Me.Tb_relFile.Name = "Tb_relFile"
        Me.Tb_relFile.Size = New System.Drawing.Size(559, 25)
        Me.Tb_relFile.TabIndex = 1
        '
        'Btn_selRelFile
        '
        Me.Btn_selRelFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_selRelFile.Location = New System.Drawing.Point(768, 3)
        Me.Btn_selRelFile.Name = "Btn_selRelFile"
        Me.Btn_selRelFile.Size = New System.Drawing.Size(64, 26)
        Me.Btn_selRelFile.TabIndex = 2
        Me.Btn_selRelFile.Text = "选择"
        Me.Btn_selRelFile.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Tb_inputDir, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Btn_selInputDir, 2, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 41)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(835, 32)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(194, 32)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Creo源文件目录"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tb_inputDir
        '
        Me.Tb_inputDir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_inputDir.Location = New System.Drawing.Point(203, 3)
        Me.Tb_inputDir.Name = "Tb_inputDir"
        Me.Tb_inputDir.Size = New System.Drawing.Size(559, 25)
        Me.Tb_inputDir.TabIndex = 1
        '
        'Btn_selInputDir
        '
        Me.Btn_selInputDir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_selInputDir.Location = New System.Drawing.Point(768, 3)
        Me.Btn_selInputDir.Name = "Btn_selInputDir"
        Me.Btn_selInputDir.Size = New System.Drawing.Size(64, 26)
        Me.Btn_selInputDir.TabIndex = 2
        Me.Btn_selInputDir.Text = "选择"
        Me.Btn_selInputDir.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Tb_exe, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_selExe, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(835, 32)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "parametric.exe路径"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tb_exe
        '
        Me.Tb_exe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_exe.Location = New System.Drawing.Point(203, 3)
        Me.Tb_exe.Name = "Tb_exe"
        Me.Tb_exe.Size = New System.Drawing.Size(559, 25)
        Me.Tb_exe.TabIndex = 1
        '
        'Btn_selExe
        '
        Me.Btn_selExe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_selExe.Location = New System.Drawing.Point(768, 3)
        Me.Btn_selExe.Name = "Btn_selExe"
        Me.Btn_selExe.Size = New System.Drawing.Size(64, 26)
        Me.Btn_selExe.TabIndex = 2
        Me.Btn_selExe.Text = "选择"
        Me.Btn_selExe.UseVisualStyleBackColor = True
        '
        'Ofd
        '
        Me.Ofd.FileName = "parametric.exe|parametric.exe"
        '
        'Fbd
        '
        Me.Fbd.ShowNewFolderButton = False
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Btn_clearRels, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Btn_addRels, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Label4, 2, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 117)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(835, 33)
        Me.TableLayoutPanel5.TabIndex = 3
        '
        'Btn_addRels
        '
        Me.Btn_addRels.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_addRels.Location = New System.Drawing.Point(3, 3)
        Me.Btn_addRels.Name = "Btn_addRels"
        Me.Btn_addRels.Size = New System.Drawing.Size(94, 27)
        Me.Btn_addRels.TabIndex = 0
        Me.Btn_addRels.Text = "导入关系"
        Me.Btn_addRels.UseVisualStyleBackColor = True
        '
        'Btn_clearRels
        '
        Me.Btn_clearRels.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_clearRels.Location = New System.Drawing.Point(103, 3)
        Me.Btn_clearRels.Name = "Btn_clearRels"
        Me.Btn_clearRels.Size = New System.Drawing.Size(94, 27)
        Me.Btn_clearRels.TabIndex = 1
        Me.Btn_clearRels.Text = "清空关系"
        Me.Btn_clearRels.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(203, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(629, 33)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "注意导入关系的txt文件应为UTF8编码，最简单的方法是从Creo程序里面编辑好后导出。"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_tools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(841, 153)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Frm_tools"
        Me.Text = "批量Prt关系处理工具"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Tb_exe As TextBox
    Friend WithEvents Btn_selExe As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents Tb_inputDir As TextBox
    Friend WithEvents Btn_selInputDir As Button
    Friend WithEvents Ofd As OpenFileDialog
    Friend WithEvents Fbd As FolderBrowserDialog
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label3 As Label
    Friend WithEvents Tb_relFile As TextBox
    Friend WithEvents Btn_selRelFile As Button
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Btn_clearRels As Button
    Friend WithEvents Btn_addRels As Button
    Friend WithEvents Label4 As Label
End Class
