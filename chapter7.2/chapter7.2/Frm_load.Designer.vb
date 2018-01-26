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
        Me.Btn_TolTable = New System.Windows.Forms.Button()
        Me.Btn_PlusMinus = New System.Windows.Forms.Button()
        Me.Btn_Symmetrical = New System.Windows.Forms.Button()
        Me.Btn_new = New System.Windows.Forms.Button()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_TolFit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_TolFit, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_TolTable, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_PlusMinus, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Symmetrical, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 6)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(4, 4)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 7
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(233, 438)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'Btn_TolTable
        '
        Me.Btn_TolTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_TolTable.Location = New System.Drawing.Point(4, 252)
        Me.Btn_TolTable.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_TolTable.Name = "Btn_TolTable"
        Me.Btn_TolTable.Size = New System.Drawing.Size(225, 54)
        Me.Btn_TolTable.TabIndex = 4
        Me.Btn_TolTable.Text = "设置基轴制公差为h6并显示数值"
        Me.Btn_TolTable.UseVisualStyleBackColor = True
        '
        'Btn_PlusMinus
        '
        Me.Btn_PlusMinus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_PlusMinus.Location = New System.Drawing.Point(4, 190)
        Me.Btn_PlusMinus.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_PlusMinus.Name = "Btn_PlusMinus"
        Me.Btn_PlusMinus.Size = New System.Drawing.Size(225, 54)
        Me.Btn_PlusMinus.TabIndex = 3
        Me.Btn_PlusMinus.Text = "设置公差为：（+0.2,-0.1）"
        Me.Btn_PlusMinus.UseVisualStyleBackColor = True
        '
        'Btn_Symmetrical
        '
        Me.Btn_Symmetrical.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_Symmetrical.Location = New System.Drawing.Point(4, 128)
        Me.Btn_Symmetrical.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Symmetrical.Name = "Btn_Symmetrical"
        Me.Btn_Symmetrical.Size = New System.Drawing.Size(225, 54)
        Me.Btn_Symmetrical.TabIndex = 2
        Me.Btn_Symmetrical.Text = "设置公差为：±0.2"
        Me.Btn_Symmetrical.UseVisualStyleBackColor = True
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(4, 4)
        Me.Btn_new.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(225, 54)
        Me.Btn_new.TabIndex = 0
        Me.Btn_new.Text = "启动新会话"
        Me.Btn_new.UseVisualStyleBackColor = True
        '
        'Btn_Connect
        '
        Me.Btn_Connect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_Connect.Location = New System.Drawing.Point(4, 66)
        Me.Btn_Connect.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Connect.Name = "Btn_Connect"
        Me.Btn_Connect.Size = New System.Drawing.Size(225, 54)
        Me.Btn_Connect.TabIndex = 1
        Me.Btn_Connect.Text = "连接现有会话"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(241, 446)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'Btn_TolFit
        '
        Me.Btn_TolFit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_TolFit.Location = New System.Drawing.Point(4, 314)
        Me.Btn_TolFit.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_TolFit.Name = "Btn_TolFit"
        Me.Btn_TolFit.Size = New System.Drawing.Size(225, 54)
        Me.Btn_TolFit.TabIndex = 5
        Me.Btn_TolFit.Text = "设置基轴配合公差为H7/h6并显示数值"
        Me.Btn_TolFit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 372)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(227, 66)
        Me.Label1.TabIndex = 6
        Me.Label1.Tag = ""
        Me.Label1.Text = "在设定公差时请先设定公差模式为ISO并加载公差表！！"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(241, 446)
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
    Friend WithEvents Btn_TolTable As Button
    Friend WithEvents Btn_PlusMinus As Button
    Friend WithEvents Btn_Symmetrical As Button
    Friend WithEvents Btn_TolFit As Button
    Friend WithEvents Label1 As Label
End Class
