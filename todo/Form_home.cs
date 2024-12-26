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
        string User;
        public Form_home(string user)
        {
            InitializeComponent();
            Text = user;
            User = user;
        }

        private void 切換使用者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* change mainForm to Form_login and exit Form_home */
            Program.mainForm = new Form_login();
            Close();
        }

        private void Form_home_Load(object sender, EventArgs e)
        {

        }

        private void Form_home_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Form_edit form = new Form_edit(this, User);
                form.ShowDialog();
            }
        }

        private void Form_home_Checkbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CheckBox clickedCheckBox = sender as CheckBox;
                if (clickedCheckBox != null)
                {
                    // 儲存文字
                    string taskText = clickedCheckBox.Text;
                    Form_edit form = new Form_edit(this, User,taskText);
                    form.ShowDialog();
                }
            }
        }
        public void AddCheckBox(string text)
        {
            // 動態新增 CheckBox
            CheckBox checkBox = new CheckBox
            {
                Text = text,
                Location = new Point(200,200),
                AutoSize = true
            };

            // 綁定右鍵點擊事件
            checkBox.MouseDown += Form_home_Checkbox_MouseDown;

            // 新增到 Form2 的控制項
            this.Controls.Add(checkBox);
            MessageBox.Show("nice", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
