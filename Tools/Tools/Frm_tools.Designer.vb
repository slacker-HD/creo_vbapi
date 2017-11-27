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
        Me.Tb_outputDir = New System.Windows.Forms.TextBox()
        Me.Btn_selOutputDir = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Tb_inputDir = New System.Windows.Forms.TextBox()
        Me.Btn_selInputDir = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Tb_exe = New System.Windows.Forms.TextBox()
        Me.Btn_selExe = New System.Windows.Forms.Button()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_exportIges = New System.Windows.Forms.Button()
        Me.Btn_exportStep = New System.Windows.Forms.Button()
        Me.Btn_exportPdf = New System.Windows.Forms.Button()
        Me.Btn_exportDwg = New System.Windows.Forms.Button()
        Me.Ofd = New System.Windows.Forms.OpenFileDialog()
        Me.Fbd = New System.Windows.Forms.FolderBrowserDialog()
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(695, 161)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Tb_outputDir, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Btn_selOutputDir, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 83)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(689, 34)
        Me.TableLayoutPanel4.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(154, 34)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "导出文件存放目录"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tb_outputDir
        '
        Me.Tb_outputDir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_outputDir.Location = New System.Drawing.Point(163, 3)
        Me.Tb_outputDir.Name = "Tb_outputDir"
        Me.Tb_outputDir.Size = New System.Drawing.Size(453, 25)
        Me.Tb_outputDir.TabIndex = 1
        '
        'Btn_selOutputDir
        '
        Me.Btn_selOutputDir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_selOutputDir.Location = New System.Drawing.Point(622, 3)
        Me.Btn_selOutputDir.Name = "Btn_selOutputDir"
        Me.Btn_selOutputDir.Size = New System.Drawing.Size(64, 28)
        Me.Btn_selOutputDir.TabIndex = 2
        Me.Btn_selOutputDir.Text = "选择"
        Me.Btn_selOutputDir.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Tb_inputDir, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Btn_selInputDir, 2, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 43)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(689, 34)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 34)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Creo源文件目录"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tb_inputDir
        '
        Me.Tb_inputDir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_inputDir.Location = New System.Drawing.Point(163, 3)
        Me.Tb_inputDir.Name = "Tb_inputDir"
        Me.Tb_inputDir.Size = New System.Drawing.Size(453, 25)
        Me.Tb_inputDir.TabIndex = 1
        '
        'Btn_selInputDir
        '
        Me.Btn_selInputDir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_selInputDir.Location = New System.Drawing.Point(622, 3)
        Me.Btn_selInputDir.Name = "Btn_selInputDir"
        Me.Btn_selInputDir.Size = New System.Drawing.Size(64, 28)
        Me.Btn_selInputDir.TabIndex = 2
        Me.Btn_selInputDir.Text = "选择"
        Me.Btn_selInputDir.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
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
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(689, 34)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 34)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "parametric.exe路径"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tb_exe
        '
        Me.Tb_exe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_exe.Location = New System.Drawing.Point(163, 3)
        Me.Tb_exe.Name = "Tb_exe"
        Me.Tb_exe.Size = New System.Drawing.Size(453, 25)
        Me.Tb_exe.TabIndex = 1
        '
        'Btn_selExe
        '
        Me.Btn_selExe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_selExe.Location = New System.Drawing.Point(622, 3)
        Me.Btn_selExe.Name = "Btn_selExe"
        Me.Btn_selExe.Size = New System.Drawing.Size(64, 28)
        Me.Btn_selExe.TabIndex = 2
        Me.Btn_selExe.Text = "选择"
        Me.Btn_selExe.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 4
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Btn_exportIges, 3, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Btn_exportStep, 2, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Btn_exportPdf, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Btn_exportDwg, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 123)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(689, 35)
        Me.TableLayoutPanel5.TabIndex = 3
        '
        'Btn_exportIges
        '
        Me.Btn_exportIges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exportIges.Location = New System.Drawing.Point(519, 3)
        Me.Btn_exportIges.Name = "Btn_exportIges"
        Me.Btn_exportIges.Size = New System.Drawing.Size(167, 29)
        Me.Btn_exportIges.TabIndex = 3
        Me.Btn_exportIges.Text = "导出prt到iges"
        Me.Btn_exportIges.UseVisualStyleBackColor = True
        '
        'Btn_exportStep
        '
        Me.Btn_exportStep.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exportStep.Location = New System.Drawing.Point(347, 3)
        Me.Btn_exportStep.Name = "Btn_exportStep"
        Me.Btn_exportStep.Size = New System.Drawing.Size(166, 29)
        Me.Btn_exportStep.TabIndex = 2
        Me.Btn_exportStep.Text = "导出prt到step"
        Me.Btn_exportStep.UseVisualStyleBackColor = True
        '
        'Btn_exportPdf
        '
        Me.Btn_exportPdf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exportPdf.Location = New System.Drawing.Point(175, 3)
        Me.Btn_exportPdf.Name = "Btn_exportPdf"
        Me.Btn_exportPdf.Size = New System.Drawing.Size(166, 29)
        Me.Btn_exportPdf.TabIndex = 1
        Me.Btn_exportPdf.Text = "导出drw到pdf"
        Me.Btn_exportPdf.UseVisualStyleBackColor = True
        '
        'Btn_exportDwg
        '
        Me.Btn_exportDwg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exportDwg.Location = New System.Drawing.Point(3, 3)
        Me.Btn_exportDwg.Name = "Btn_exportDwg"
        Me.Btn_exportDwg.Size = New System.Drawing.Size(166, 29)
        Me.Btn_exportDwg.TabIndex = 0
        Me.Btn_exportDwg.Text = "导出drw到dwg"
        Me.Btn_exportDwg.UseVisualStyleBackColor = True
        '
        'Ofd
        '
        Me.Ofd.FileName = "parametric.exe|parametric.exe"
        '
        'Fbd
        '
        Me.Fbd.ShowNewFolderButton = False
        '
        'Frm_tools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(695, 161)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Frm_tools"
        Me.Text = "批量格式转换工具"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
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
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label3 As Label
    Friend WithEvents Tb_outputDir As TextBox
    Friend WithEvents Btn_selOutputDir As Button
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Btn_exportDwg As Button
    Friend WithEvents Btn_exportStep As Button
    Friend WithEvents Btn_exportPdf As Button
    Friend WithEvents Btn_exportIges As Button
    Friend WithEvents Ofd As OpenFileDialog
    Friend WithEvents Fbd As FolderBrowserDialog
End Class
