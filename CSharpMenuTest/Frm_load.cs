using System;
using System.Windows.Forms;

namespace CSharpMenuTest
{
    public partial class Frm_load : Form
    {
        CreoFunction mycreoFunction = new CreoFunction();
        public Frm_load()
        {
            InitializeComponent();
        }

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            mycreoFunction.ConnectCreo();
        }
    }
}
