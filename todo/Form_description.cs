using System;
using System.Drawing;
using System.Windows.Forms;

namespace todo
{
    public partial class Form_description : Form
    {
        private TextBox txtDescription;
        private string description;

        public Form_description(string initialDescription)
        {
            InitializeComponent();
            description = initialDescription;

            this.Text = "任務描述";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            InitializeControls();
        }

        private void InitializeControls()
        {
            txtDescription = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Font = new Font("Arial", 10),
                ScrollBars = ScrollBars.Vertical,
                Text = description // 載入初始描述
            };
            this.Controls.Add(txtDescription);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 自動保存描述
            description = txtDescription.Text;
            base.OnFormClosing(e);
        }

        public string GetDescription()
        {
            return description;
        }
    }
}
