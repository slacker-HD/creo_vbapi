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
        Me.Btn_exporttoJpg = New System.Windows.Forms.Button()
        Me.Btn_new = New System.Windows.Forms.Button()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.SFD = New System.Windows.Forms.SaveFileDialog()
        Me.Btn_exporttoEps = New System.Windows.Forms.Button()
        Me.Btn_exporttoBmp = New System.Windows.Forms.Button()
        Me.Btn_exporttoTif = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_exporttoEps, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_exporttoJpg, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_exporttoBmp, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_exporttoTif, 0, 4)
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
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(313, 316)
        Me.TableLayoutPanel2.TabIndex = 6
        '
        'Btn_exporttoJpg
        '
        Me.Btn_exporttoJpg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exporttoJpg.Enabled = False
        Me.Btn_exporttoJpg.Location = New System.Drawing.Point(4, 108)
        Me.Btn_exporttoJpg.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_exporttoJpg.Name = "Btn_exporttoJpg"
        Me.Btn_exporttoJpg.Size = New System.Drawing.Size(305, 44)
        Me.Btn_exporttoJpg.TabIndex = 7
        Me.Btn_exporttoJpg.Text = "导出jpg图像文件"
        Me.Btn_exporttoJpg.UseCompatibleTextRendering = True
        Me.Btn_exporttoJpg.UseVisualStyleBackColor = True
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(4, 4)
        Me.Btn_new.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(305, 44)
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
        Me.Btn_Connect.Size = New System.Drawing.Size(305, 44)
        Me.Btn_Connect.TabIndex = 1
        Me.Btn_Connect.Text = "连接现有会话"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'Btn_exporttoEps
        '
        Me.Btn_exporttoEps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exporttoEps.Enabled = False
        Me.Btn_exporttoEps.Location = New System.Drawing.Point(4, 264)
        Me.Btn_exporttoEps.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_exporttoEps.Name = "Btn_exporttoEps"
        Me.Btn_exporttoEps.Size = New System.Drawing.Size(305, 48)
        Me.Btn_exporttoEps.TabIndex = 8
        Me.Btn_exporttoEps.Text = "导出Eps图像文件"
        Me.Btn_exporttoEps.UseCompatibleTextRendering = True
        Me.Btn_exporttoEps.UseVisualStyleBackColor = True
        '
        'Btn_exporttoBmp
        '
        Me.Btn_exporttoBmp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exporttoBmp.Enabled = False
        Me.Btn_exporttoBmp.Location = New System.Drawing.Point(3, 159)
        Me.Btn_exporttoBmp.Name = "Btn_exporttoBmp"
        Me.Btn_exporttoBmp.Size = New System.Drawing.Size(307, 46)
        Me.Btn_exporttoBmp.TabIndex = 9
        Me.Btn_exporttoBmp.Text = "导出bmp图像文件"
        Me.Btn_exporttoBmp.UseVisualStyleBackColor = True
        '
        'Btn_exporttoTif
        '
        Me.Btn_exporttoTif.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_exporttoTif.Enabled = False
        Me.Btn_exporttoTif.Location = New System.Drawing.Point(3, 211)
        Me.Btn_exporttoTif.Name = "Btn_exporttoTif"
        Me.Btn_exporttoTif.Size = New System.Drawing.Size(307, 46)
        Me.Btn_exporttoTif.TabIndex = 10
        Me.Btn_exporttoTif.Text = "导出Tif图像文件"
        Me.Btn_exporttoTif.UseVisualStyleBackColor = True
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(313, 316)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Frm_load"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Btn_exporttoJpg As Button
    Friend WithEvents Btn_new As Button
    Friend WithEvents Btn_Connect As Button
    Friend WithEvents SFD As SaveFileDialog
    Friend WithEvents Btn_exporttoEps As Button
    Friend WithEvents Btn_exporttoBmp As Button
    Friend WithEvents Btn_exporttoTif As Button
End Class
