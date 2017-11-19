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
        Me.Btn_new = New System.Windows.Forms.Button()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_exportStp = New System.Windows.Forms.Button()
        Me.Btn_exportDwg = New System.Windows.Forms.Button()
        Me.Btn_exportPdf = New System.Windows.Forms.Button()
        Me.Btn_exportIgs = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(4, 4)
        Me.Btn_new.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(241, 57)
        Me.Btn_new.TabIndex = 0
        Me.Btn_new.Text = "启动新会话"
        Me.Btn_new.UseVisualStyleBackColor = True
        '
        'Btn_Connect
        '
        Me.Btn_Connect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_Connect.Location = New System.Drawing.Point(4, 69)
        Me.Btn_Connect.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Connect.Name = "Btn_Connect"
        Me.Btn_Connect.Size = New System.Drawing.Size(241, 57)
        Me.Btn_Connect.TabIndex = 1
        Me.Btn_Connect.Text = "连接现有会话"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_exportStp, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_exportDwg, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_exportPdf, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_exportIgs, 0, 5)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 6
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(249, 390)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'Btn_exportStp
        '
        Me.Btn_exportStp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exportStp.Enabled = False
        Me.Btn_exportStp.Location = New System.Drawing.Point(3, 263)
        Me.Btn_exportStp.Name = "Btn_exportStp"
        Me.Btn_exportStp.Size = New System.Drawing.Size(243, 59)
        Me.Btn_exportStp.TabIndex = 4
        Me.Btn_exportStp.Text = "将当前打开Prt文件导出Stp，确保当前有打开的模型"
        Me.Btn_exportStp.UseVisualStyleBackColor = True
        '
        'Btn_exportDwg
        '
        Me.Btn_exportDwg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exportDwg.Enabled = False
        Me.Btn_exportDwg.Location = New System.Drawing.Point(3, 133)
        Me.Btn_exportDwg.Name = "Btn_exportDwg"
        Me.Btn_exportDwg.Size = New System.Drawing.Size(243, 59)
        Me.Btn_exportDwg.TabIndex = 2
        Me.Btn_exportDwg.Text = "将当前打开Drw文件导出dwg，确保当前有打开的模型"
        Me.Btn_exportDwg.UseVisualStyleBackColor = True
        '
        'Btn_exportPdf
        '
        Me.Btn_exportPdf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exportPdf.Enabled = False
        Me.Btn_exportPdf.Location = New System.Drawing.Point(3, 198)
        Me.Btn_exportPdf.Name = "Btn_exportPdf"
        Me.Btn_exportPdf.Size = New System.Drawing.Size(243, 59)
        Me.Btn_exportPdf.TabIndex = 3
        Me.Btn_exportPdf.Text = "将当前打开Drw文件导出pdf，确保当前打开Drw文件"
        Me.Btn_exportPdf.UseVisualStyleBackColor = True
        '
        'Btn_exportIgs
        '
        Me.Btn_exportIgs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exportIgs.Enabled = False
        Me.Btn_exportIgs.Location = New System.Drawing.Point(3, 328)
        Me.Btn_exportIgs.Name = "Btn_exportIgs"
        Me.Btn_exportIgs.Size = New System.Drawing.Size(243, 59)
        Me.Btn_exportIgs.TabIndex = 5
        Me.Btn_exportIgs.Text = "将当前打开Prt文件导出igs，确保当前有打开的模型"
        Me.Btn_exportIgs.UseVisualStyleBackColor = True
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(249, 390)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Name = "Frm_load"
        Me.Text = "A"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Btn_new As Button
    Friend WithEvents Btn_Connect As Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Btn_exportDwg As Button
    Friend WithEvents Btn_exportPdf As Button
    Friend WithEvents Btn_exportStp As Button
    Friend WithEvents Btn_exportIgs As Button
End Class
