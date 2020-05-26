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
        Me.Btn_setCurrentModelMaterial = New System.Windows.Forms.Button()
        Me.Btn_new = New System.Windows.Forms.Button()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.OFD = New System.Windows.Forms.OpenFileDialog()
        Me.Btn_getCurrentModelMaterial = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_getCurrentModelMaterial, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_setCurrentModelMaterial, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(321, 308)
        Me.TableLayoutPanel2.TabIndex = 6
        '
        'Btn_setCurrentModelMaterial
        '
        Me.Btn_setCurrentModelMaterial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_setCurrentModelMaterial.Enabled = False
        Me.Btn_setCurrentModelMaterial.Location = New System.Drawing.Point(4, 158)
        Me.Btn_setCurrentModelMaterial.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_setCurrentModelMaterial.Name = "Btn_setCurrentModelMaterial"
        Me.Btn_setCurrentModelMaterial.Size = New System.Drawing.Size(313, 69)
        Me.Btn_setCurrentModelMaterial.TabIndex = 7
        Me.Btn_setCurrentModelMaterial.Text = "打开并设定当前模型材料"
        Me.Btn_setCurrentModelMaterial.UseCompatibleTextRendering = True
        Me.Btn_setCurrentModelMaterial.UseVisualStyleBackColor = True
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(4, 4)
        Me.Btn_new.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(313, 69)
        Me.Btn_new.TabIndex = 0
        Me.Btn_new.Text = "启动新会话"
        Me.Btn_new.UseVisualStyleBackColor = True
        '
        'Btn_Connect
        '
        Me.Btn_Connect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_Connect.Location = New System.Drawing.Point(4, 81)
        Me.Btn_Connect.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Connect.Name = "Btn_Connect"
        Me.Btn_Connect.Size = New System.Drawing.Size(313, 69)
        Me.Btn_Connect.TabIndex = 1
        Me.Btn_Connect.Text = "连接现有会话"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'OFD
        '
        Me.OFD.Filter = "(*.mtl)|*.mtl"
        '
        'Btn_getCurrentModelMaterial
        '
        Me.Btn_getCurrentModelMaterial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_getCurrentModelMaterial.Enabled = False
        Me.Btn_getCurrentModelMaterial.Location = New System.Drawing.Point(4, 235)
        Me.Btn_getCurrentModelMaterial.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_getCurrentModelMaterial.Name = "Btn_getCurrentModelMaterial"
        Me.Btn_getCurrentModelMaterial.Size = New System.Drawing.Size(313, 69)
        Me.Btn_getCurrentModelMaterial.TabIndex = 8
        Me.Btn_getCurrentModelMaterial.Text = "获取当前模型材料"
        Me.Btn_getCurrentModelMaterial.UseCompatibleTextRendering = True
        Me.Btn_getCurrentModelMaterial.UseVisualStyleBackColor = True
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(321, 308)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Frm_load"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Btn_setCurrentModelMaterial As Button
    Friend WithEvents Btn_new As Button
    Friend WithEvents Btn_Connect As Button
    Friend WithEvents OFD As OpenFileDialog
    Friend WithEvents Btn_getCurrentModelMaterial As Button
End Class
