using System;
using System.Drawing;
using System.Windows.Forms;

namespace todo
{
    public partial class Form_description : Form
    {
        private Task currentTask;
        private TextBox txtName;

        public Form_description(Task curtask)
        {
            InitializeComponent();
            currentTask = curtask;

            this.Text = "任務詳細資訊";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            InitializeControls();
        }

        private void InitializeControls()
        {
            Label lblName = new Label
            {
                Text = "任務名稱：",
                Location = new Point(20, 20),
                AutoSize = true
            };
            txtName = new TextBox
            {
                Location = new Point(100, 20),
                Width = 250,
                Text = currentTask.name
            };

            Label lblDescription = new Label
            {
                Text = "任務描述：",
                Location = new Point(20, 140),
                AutoSize = true
            };


            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
        }



    }
}
