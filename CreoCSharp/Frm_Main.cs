using System;
using System.Windows.Forms;

namespace CreoCSharp
{
    public partial class Frm_Main : Form
    {
        private VBAPITool mytool;

        public Frm_Main()
        {
            InitializeComponent();
            mytool = new VBAPITool();
        }

        private void Btn_new_Click(object sender, EventArgs e)
        {
            mytool.StartCreo();
        }

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            mytool.ConnectCreo();
        }

        private void Btn_Open_Click(object sender, EventArgs e)
        {
            mytool.Openfile();
        }
    }
}