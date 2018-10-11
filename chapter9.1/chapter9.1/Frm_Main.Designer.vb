<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Main))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_OpenFile = New System.Windows.Forms.Button()
        Me.Axpview_Main = New AxpviewLib.Axpview()
        Me.OFD = New System.Windows.Forms.OpenFileDialog()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.Axpview_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Btn_OpenFile, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Axpview_Main, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(800, 450)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Btn_OpenFile
        '
        Me.Btn_OpenFile.Dock = System.Windows.Forms.DockStyle.Right
        Me.Btn_OpenFile.Location = New System.Drawing.Point(691, 413)
        Me.Btn_OpenFile.Name = "Btn_OpenFile"
        Me.Btn_OpenFile.Size = New System.Drawing.Size(106, 34)
        Me.Btn_OpenFile.TabIndex = 0
        Me.Btn_OpenFile.Text = "选择文件"
        Me.Btn_OpenFile.UseVisualStyleBackColor = True
        '
        'Axpview_Main
        '
        Me.Axpview_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Axpview_Main.Enabled = True
        Me.Axpview_Main.Location = New System.Drawing.Point(3, 3)
        Me.Axpview_Main.Name = "Axpview_Main"
        Me.Axpview_Main.OcxState = CType(resources.GetObject("Axpview_Main.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Axpview_Main.Size = New System.Drawing.Size(794, 404)
        Me.Axpview_Main.TabIndex = 1
        '
        'Frm_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "Frm_Main"
        Me.Text = "预览Creo文件"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.Axpview_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Btn_OpenFile As Button
    Friend WithEvents Axpview_Main As AxpviewLib.Axpview
    Friend WithEvents OFD As OpenFileDialog
End Class
