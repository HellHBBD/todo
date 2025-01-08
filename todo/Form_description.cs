using System;
using System.Drawing;
using System.Windows.Forms;

namespace todo
{
    public partial class Form_description : Form
    {
        Form_edit formedit;
        //private Task currentTask; // 當前任務
        private TextBox txtName;
        private DateTimePicker dtpDate;
        private NumericUpDown numImportance;
        private TextBox txtDescription;
        private string txtDescriptionText;
        private Button btnSave;
        private Button btnCancel;

        //public Form_description(Form_edit form_Edit,Task curtask)
        public Form_description(Form_edit form_Edit)
        {
            InitializeComponent();
            //currentTask = curtask;
            formedit = form_Edit;

            this.Text = "任務詳細資訊";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            InitializeControls();
        }
        public Form_description(Form_edit form_Edit, string oldDescription)
        {
            InitializeComponent();
            //currentTask = curtask;
            formedit = form_Edit;

            this.Text = "任務詳細資訊";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            txtDescriptionText = oldDescription;
            InitializeControls();
        }

        private void InitializeControls()
        {
            Label lblDescription = new Label
            {
                Text = "任務描述：",
                Location = new Point(20, 140),
                AutoSize = true
            };
            txtDescription = new TextBox
            {
                Location = new Point(100, 140),
                Width = 250,
                Height = 60,
                Multiline = true,
                Text = txtDescriptionText
            };

            btnSave = new Button
            {
                Text = "保存",
                Location = new Point(100, 220),
                Width = 80
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "取消",
                Location = new Point(200, 220),
                Width = 80
            };
            btnCancel.Click += BtnCancel_Click;

            
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtDescription);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 保存修改
            formedit.tempDescrip = txtDescription.Text;
            //currentTask.name = txtName.Text;
            //currentTask.date = dtpDate.Value;
            //currentTask.important = (int)numImportance.Value;
            //currentTask.description = txtDescription.Text;

            // 關閉視窗
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // 不保存修改，直接關閉
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
