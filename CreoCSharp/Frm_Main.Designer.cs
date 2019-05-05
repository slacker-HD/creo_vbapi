namespace CreoCSharp
{
    partial class Frm_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Btn_new = new System.Windows.Forms.Button();
            this.Btn_Connect = new System.Windows.Forms.Button();
            this.Btn_Open = new System.Windows.Forms.Button();
            this.TableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel2
            // 
            this.TableLayoutPanel2.ColumnCount = 1;
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel2.Controls.Add(this.Btn_new, 0, 0);
            this.TableLayoutPanel2.Controls.Add(this.Btn_Connect, 0, 1);
            this.TableLayoutPanel2.Controls.Add(this.Btn_Open, 0, 2);
            this.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.TableLayoutPanel2.Name = "TableLayoutPanel2";
            this.TableLayoutPanel2.RowCount = 3;
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanel2.Size = new System.Drawing.Size(251, 303);
            this.TableLayoutPanel2.TabIndex = 4;
            // 
            // Btn_new
            // 
            this.Btn_new.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_new.Location = new System.Drawing.Point(4, 4);
            this.Btn_new.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_new.Name = "Btn_new";
            this.Btn_new.Size = new System.Drawing.Size(243, 92);
            this.Btn_new.TabIndex = 0;
            this.Btn_new.Text = "启动新会话";
            this.Btn_new.UseVisualStyleBackColor = true;
            this.Btn_new.Click += new System.EventHandler(this.Btn_new_Click);
            // 
            // Btn_Connect
            // 
            this.Btn_Connect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_Connect.Location = new System.Drawing.Point(4, 104);
            this.Btn_Connect.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(243, 92);
            this.Btn_Connect.TabIndex = 1;
            this.Btn_Connect.Text = "连接现有会话";
            this.Btn_Connect.UseVisualStyleBackColor = true;
            this.Btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // Btn_Open
            // 
            this.Btn_Open.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_Open.Location = new System.Drawing.Point(3, 203);
            this.Btn_Open.Name = "Btn_Open";
            this.Btn_Open.Size = new System.Drawing.Size(245, 97);
            this.Btn_Open.TabIndex = 2;
            this.Btn_Open.Text = "打开一个模型\r\n（确保当前已有会话）";
            this.Btn_Open.UseVisualStyleBackColor = true;
            this.Btn_Open.Click += new System.EventHandler(this.Btn_Open_Click);
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 303);
            this.Controls.Add(this.TableLayoutPanel2);
            this.MaximizeBox = false;
            this.Name = "Frm_Main";
            this.TableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
        internal System.Windows.Forms.Button Btn_new;
        internal System.Windows.Forms.Button Btn_Connect;
        private System.Windows.Forms.Button Btn_Open;
    }
}

