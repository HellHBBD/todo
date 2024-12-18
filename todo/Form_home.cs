using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace todo
{
    public partial class Form_home : Form
    {
        public Form_home(string user)
        {
            InitializeComponent();
            Text = user;
        }

        private void 切換使用者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* change mainForm to Form_login and exit Form_home */
            Program.mainForm = new Form_login();
            Close();
        }
    }
}
