using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace todo
{
    public partial class Form_description : Form
    {
        private RichTextBox txtDescription;
        private string description;
        private FoldableTextManager foldableTextManager;

        private Stack<string> undoStack = new Stack<string>();
        private Stack<string> redoStack = new Stack<string>();

        public Form_description(string initialDescription) : this("任務描述", initialDescription) { }

        public Form_description(Form_edit form_Edit, string oldDescription) : this("任務詳細資訊", oldDescription) { }

        private Form_description(string title, string initialDescription)
        {
            InitializeComponent();
            InitializeForm(title, initialDescription);
        }

        private void InitializeForm(string title, string initialDescription)
        {
            description = initialDescription;
            foldableTextManager = new FoldableTextManager(initialDescription);

            this.Text = title;
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            InitializeControls();
        }

        private void InitializeControls()
        {
            txtDescription = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10),
                ScrollBars = RichTextBoxScrollBars.Vertical,
            };
            txtDescription.KeyDown += TxtDescription_KeyDown;
            txtDescription.TextChanged += TxtDescription_TextChanged;
            this.Controls.Add(txtDescription);

            MenuStrip menuStrip = new MenuStrip();
            this.MainMenuStrip = menuStrip;

            ToolStripMenuItem menuEdit = new ToolStripMenuItem("編輯");
            menuStrip.Items.Add(menuEdit);

            ToolStripMenuItem menuUndo = new ToolStripMenuItem("上一步", null, UndoAction_Click);
            menuEdit.DropDownItems.Add(menuUndo);

            ToolStripMenuItem menuRedo = new ToolStripMenuItem("下一步", null, RedoAction_Click);
            menuEdit.DropDownItems.Add(menuRedo);

            ToolStripMenuItem menuFormat = new ToolStripMenuItem("格式");
            menuStrip.Items.Add(menuFormat);

            ToolStripMenuItem menuFont = new ToolStripMenuItem("字體", null, MenuFont_Click);
            menuFormat.DropDownItems.Add(menuFont);

            ToolStripMenuItem menuColor = new ToolStripMenuItem("顏色", null, MenuColor_Click);
            menuFormat.DropDownItems.Add(menuColor);

            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;
        }

        private void ToggleFoldMode_Click(object sender, EventArgs e)
        {
            foldableTextManager.ToggleFoldMode(); // 呼叫 Manager 切換折疊模式
            ApplyFoldState(); // 更新顯示
        }

        private void ApplyFoldState()
        {
            txtDescription.SuspendLayout();
            string[] updatedLines = foldableTextManager.GetFoldedText();
            txtDescription.Lines = updatedLines; // 使用更新過的行來顯示
            txtDescription.ScrollToCaret();
            txtDescription.ResumeLayout();
        }

        private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) // 切換折疊模式
            {
                foldableTextManager.ToggleFoldMode();
                ApplyFoldState(); // 更新顯示
                e.Handled = true;
            }
        }

        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
            string currentText = txtDescription.Text;
            if (undoStack.Count == 0 || undoStack.Peek() != currentText)
            {
                undoStack.Push(description); // 儲存前狀態
                redoStack.Clear();
            }
            description = currentText;
        }

        private void MenuFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.Font = txtDescription.Font;
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDescription.Font = fontDialog.Font;
                }
            }
        }

        private void MenuColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = txtDescription.ForeColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDescription.ForeColor = colorDialog.Color;
                }
            }
        }

        private void UndoAction_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push(description);
                UpdateTextContent(undoStack.Pop(), false);
            }
        }

        private void RedoAction_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(description);
                UpdateTextContent(redoStack.Pop(), false);
            }
        }

        private void UpdateTextContent(string newText, bool pushUndo = true)
        {
            if (pushUndo && (undoStack.Count == 0 || undoStack.Peek() != description))
            {
                undoStack.Push(description);
            }
            description = newText;
            txtDescription.TextChanged -= TxtDescription_TextChanged;
            txtDescription.Text = description;
            txtDescription.TextChanged += TxtDescription_TextChanged;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            description = txtDescription.Text;
            base.OnFormClosing(e);
        }

        public string GetDescription()
        {
            return description;
        }
    }
}
