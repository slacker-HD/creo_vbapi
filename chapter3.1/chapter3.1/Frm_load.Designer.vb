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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_addParam = New System.Windows.Forms.Button()
        Me.Tb_paramValueAdd = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Tb_paramNameAdd = New System.Windows.Forms.TextBox()
        Me.Cmb_paramTypeAdd = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Tb_paramNameDel = New System.Windows.Forms.TextBox()
        Me.Btn_delParam = New System.Windows.Forms.Button()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Tb_paramNameMod = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Btn_modParam = New System.Windows.Forms.Button()
        Me.Tb_paramValueMod = New System.Windows.Forms.TextBox()
        Me.Cmb_paramTypeMod = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_new
        '
        Me.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_new.Location = New System.Drawing.Point(4, 4)
        Me.Btn_new.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_new.Name = "Btn_new"
        Me.Btn_new.Size = New System.Drawing.Size(681, 82)
        Me.Btn_new.TabIndex = 0
        Me.Btn_new.Text = "启动新会话"
        Me.Btn_new.UseVisualStyleBackColor = True
        '
        'Btn_Connect
        '
        Me.Btn_Connect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_Connect.Location = New System.Drawing.Point(4, 94)
        Me.Btn_Connect.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Connect.Name = "Btn_Connect"
        Me.Btn_Connect.Size = New System.Drawing.Size(681, 82)
        Me.Btn_Connect.TabIndex = 1
        Me.Btn_Connect.Text = "连接现有会话"
        Me.Btn_Connect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox3, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox2, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_new, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Connect, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox1, 0, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(689, 553)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(3, 431)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(683, 119)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "删除一个参数，确保当前模型包含这个参数名"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TableLayoutPanel4)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 307)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(683, 118)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "修改一个参数值，确保当前模型包含这个参数名"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 183)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(683, 118)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "添加一个参数，确保当前模型没有这个参数名"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Btn_addParam, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Tb_paramValueAdd, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Tb_paramNameAdd, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Cmb_paramTypeAdd, 2, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 21)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(677, 94)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Btn_addParam
        '
        Me.Btn_addParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_addParam.Enabled = False
        Me.Btn_addParam.Location = New System.Drawing.Point(510, 3)
        Me.Btn_addParam.Name = "Btn_addParam"
        Me.Btn_addParam.Size = New System.Drawing.Size(164, 41)
        Me.Btn_addParam.TabIndex = 8
        Me.Btn_addParam.Text = "添加"
        Me.Btn_addParam.UseVisualStyleBackColor = True
        '
        'Tb_paramValueAdd
        '
        Me.Tb_paramValueAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_paramValueAdd.Location = New System.Drawing.Point(172, 50)
        Me.Tb_paramValueAdd.Name = "Tb_paramValueAdd"
        Me.Tb_paramValueAdd.Size = New System.Drawing.Size(163, 25)
        Me.Tb_paramValueAdd.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(341, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(163, 47)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "参数类型"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(172, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(163, 47)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "参数值"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(163, 47)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "参数名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tb_paramNameAdd
        '
        Me.Tb_paramNameAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_paramNameAdd.Location = New System.Drawing.Point(3, 50)
        Me.Tb_paramNameAdd.Name = "Tb_paramNameAdd"
        Me.Tb_paramNameAdd.Size = New System.Drawing.Size(163, 25)
        Me.Tb_paramNameAdd.TabIndex = 4
        '
        'Cmb_paramTypeAdd
        '
        Me.Cmb_paramTypeAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Cmb_paramTypeAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_paramTypeAdd.FormattingEnabled = True
        Me.Cmb_paramTypeAdd.Items.AddRange(New Object() {"浮点型", "整形", "布尔型", "字符串"})
        Me.Cmb_paramTypeAdd.Location = New System.Drawing.Point(341, 50)
        Me.Cmb_paramTypeAdd.Name = "Cmb_paramTypeAdd"
        Me.Cmb_paramTypeAdd.Size = New System.Drawing.Size(163, 23)
        Me.Cmb_paramTypeAdd.TabIndex = 6
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Tb_paramNameDel, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Btn_delParam, 3, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 21)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(677, 95)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(163, 47)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "参数名"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tb_paramNameDel
        '
        Me.Tb_paramNameDel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_paramNameDel.Location = New System.Drawing.Point(3, 50)
        Me.Tb_paramNameDel.Name = "Tb_paramNameDel"
        Me.Tb_paramNameDel.Size = New System.Drawing.Size(163, 25)
        Me.Tb_paramNameDel.TabIndex = 1
        '
        'Btn_delParam
        '
        Me.Btn_delParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_delParam.Enabled = False
        Me.Btn_delParam.Location = New System.Drawing.Point(510, 3)
        Me.Btn_delParam.Name = "Btn_delParam"
        Me.Btn_delParam.Size = New System.Drawing.Size(164, 41)
        Me.Btn_delParam.TabIndex = 3
        Me.Btn_delParam.Text = "删除"
        Me.Btn_delParam.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 4
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Tb_paramNameMod, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label6, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label7, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Btn_modParam, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Tb_paramValueMod, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Cmb_paramTypeMod, 2, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 21)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(677, 94)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(163, 47)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "参数名"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tb_paramNameMod
        '
        Me.Tb_paramNameMod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_paramNameMod.Location = New System.Drawing.Point(3, 50)
        Me.Tb_paramNameMod.Name = "Tb_paramNameMod"
        Me.Tb_paramNameMod.Size = New System.Drawing.Size(163, 25)
        Me.Tb_paramNameMod.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Location = New System.Drawing.Point(172, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(163, 47)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "参数值"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Location = New System.Drawing.Point(341, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(163, 47)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "参数类型"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Btn_modParam
        '
        Me.Btn_modParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Btn_modParam.Enabled = False
        Me.Btn_modParam.Location = New System.Drawing.Point(510, 3)
        Me.Btn_modParam.Name = "Btn_modParam"
        Me.Btn_modParam.Size = New System.Drawing.Size(164, 41)
        Me.Btn_modParam.TabIndex = 8
        Me.Btn_modParam.Text = "修改"
        Me.Btn_modParam.UseVisualStyleBackColor = True
        '
        'Tb_paramValueMod
        '
        Me.Tb_paramValueMod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_paramValueMod.Location = New System.Drawing.Point(172, 50)
        Me.Tb_paramValueMod.Name = "Tb_paramValueMod"
        Me.Tb_paramValueMod.Size = New System.Drawing.Size(163, 25)
        Me.Tb_paramValueMod.TabIndex = 9
        '
        'Cmb_paramTypeMod
        '
        Me.Cmb_paramTypeMod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Cmb_paramTypeMod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_paramTypeMod.FormattingEnabled = True
        Me.Cmb_paramTypeMod.Items.AddRange(New Object() {"浮点型", "整形", "布尔型", "字符串"})
        Me.Cmb_paramTypeMod.Location = New System.Drawing.Point(341, 50)
        Me.Cmb_paramTypeMod.Name = "Cmb_paramTypeMod"
        Me.Cmb_paramTypeMod.Size = New System.Drawing.Size(163, 23)
        Me.Cmb_paramTypeMod.TabIndex = 10
        '
        'Frm_load
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(689, 553)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Name = "Frm_load"
        Me.Text = "启动界面"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Btn_new As Button
    Friend WithEvents Btn_Connect As Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Tb_paramValueAdd As TextBox
    Friend WithEvents Tb_paramNameAdd As TextBox
    Friend WithEvents Cmb_paramTypeAdd As ComboBox
    Friend WithEvents Btn_addParam As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Label4 As Label
    Friend WithEvents Tb_paramNameDel As TextBox
    Friend WithEvents Btn_delParam As Button
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Tb_paramNameMod As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Btn_modParam As Button
    Friend WithEvents Tb_paramValueMod As TextBox
    Friend WithEvents Cmb_paramTypeMod As ComboBox
End Class
