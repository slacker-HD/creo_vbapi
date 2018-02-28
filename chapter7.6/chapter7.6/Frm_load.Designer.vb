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
        Me.Btn_setTableCell = New System.Windows.Forms.Button()
        Me.Btn_listTables = New System.Windows.Forms.Button()
        Me.Btn_new = New System.Windows.Forms.Button()
        Me.Btn_Connect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_getTableCell = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_getTableCell, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_setTableCell, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_listTables, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
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
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(184, 336)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'Btn_setTableCell
        '
        Me.Btn_setTableCell.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_setTableCell.Enabled = False
        Me.Btn_setTableCell.Location = New System.Drawing.Point(3, 204)
        Me.Btn_setTableCell.Name = "Btn_setTableCell"
        Me.Btn_setTableCell.Size = New System.Drawing.Size(178, 61)
        Me.Btn_setTableCell.TabIndex = 11
        Me.Btn_setTableCell.Text = "修改选中表格第一行第一列单元格内容为""这是修改后的文字"""
        Me.Btn_setTableCell.UseVisualStyleBackColor = True
        '
        'Btn_listTables
        '
        Me.Btn_listTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_listTables.Enabled = False
        Me.Btn_listTables.Location = New System.Drawing.Point(4, 138)
        Me.Btn_listTables.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_listTables.Name = "Btn_listTables"
        Me.Btn_listTables.Size = New System.Drawing.Size(176, 59)
        Me.Btn_listTables.TabIndex = 7
        Me.Btn_listTables.Text = "列举所有表格"
        Me.Btn_listTables.UseCompatibleTextRendering = True
        Me.Btn_listTables.UseVisualStyleBackColor = True
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(4, 4)
        Me.Btn_new.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(176, 59)
        Me.Btn_new.TabIndex = 0
        Me.Btn_new.Text = "启动新会话"
        Me.Btn_new.UseVisualStyleBackColor = True
        '
        'Btn_Connect
        '
        Me.Btn_Connect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_Connect.Location = New System.Drawing.Point(4, 71)
        Me.Btn_Connect.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Connect.Name = "Btn_Connect"
        Me.Btn_Connect.Size = New System.Drawing.Size(176, 59)
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(192, 344)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'Btn_getTableCell
        '
        Me.Btn_getTableCell.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_getTableCell.Enabled = False
        Me.Btn_getTableCell.Location = New System.Drawing.Point(3, 271)
        Me.Btn_getTableCell.Name = "Btn_getTableCell"
        Me.Btn_getTableCell.Size = New System.Drawing.Size(178, 62)
        Me.Btn_getTableCell.TabIndex = 12
        Me.Btn_getTableCell.Text = "读取选中表格第一行第一列单元格内容"
        Me.Btn_getTableCell.UseVisualStyleBackColor = True
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(192, 344)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "Frm_load"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Btn_new As Button
    Friend WithEvents Btn_Connect As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Btn_listTables As Button
    Friend WithEvents Btn_setTableCell As Button
    Friend WithEvents Btn_getTableCell As Button
End Class
