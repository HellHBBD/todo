using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace todo
{
    public partial class Form_description : Form
    {
        private RichTextBox txtDescription;
        private string description;

        private Stack<string> undoStack = new Stack<string>();
        private Stack<string> redoStack = new Stack<string>();

        private Dictionary<int, bool> collapseStates = new Dictionary<int, bool>(); // 每段摺疊狀態

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

        public Form_description(Form_edit form_Edit, string oldDescription)
        {
            InitializeComponent();
            this.Text = "任務詳細資訊";
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
                Text = description // 載入初始描述
            };
            txtDescription.KeyDown += TxtDescription_KeyDown; // 註冊快捷鍵事件
            txtDescription.TextChanged += TxtDescription_TextChanged;
            this.Controls.Add(txtDescription);

            MenuStrip menuStrip = new MenuStrip();
            this.MainMenuStrip = menuStrip;

            ToolStripMenuItem menuEdit = new ToolStripMenuItem("編輯");
            menuStrip.Items.Add(menuEdit);

            ToolStripMenuItem menuUndo = new ToolStripMenuItem("上一步", null, MenuUndo_Click);
            menuEdit.DropDownItems.Add(menuUndo);

            ToolStripMenuItem menuRedo = new ToolStripMenuItem("下一步", null, MenuRedo_Click);
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

        private void InitializeCollapseStates()
        {
            string[] lines = txtDescription.Text.Split(new[] { '\n' }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                if (!collapseStates.ContainsKey(i)) // 初始化不存在的鍵
                {
                    collapseStates[i] = false; // 預設所有段落未摺疊
                }
            }

            // 移除多餘的鍵
            foreach (var key in new List<int>(collapseStates.Keys))
            {
                if (key >= lines.Length)
                {
                    collapseStates.Remove(key);
                }
            }

            UpdateRichTextWithTriangles();
        }

        private void UpdateRichTextWithTriangles()
        {
            StringBuilder updatedText = new StringBuilder();
            string[] lines = description.Split(new[] { '\n' }, StringSplitOptions.None);

            // 每行是否已經處理過，避免重複添加三角形
            for (int i = 0; i < lines.Length; i++)
            {
                bool isCollapsed = collapseStates.ContainsKey(i) && collapseStates[i];
                string prefix = isCollapsed ? "▶ " : "▼ ";

                string content = lines[i].TrimStart(); // 移除行首的空格
                if (content.StartsWith("▶ ") || content.StartsWith("▼ "))
                {
                    // 行首已經有三角形符號，保持不變
                    updatedText.AppendLine(lines[i]);
                }
                else
                {
                    // 沒有三角形符號時，根據摺疊狀態加上三角形符號
                    updatedText.AppendLine($"{prefix}{lines[i]}");
                }
            }

            int caretPosition = txtDescription.SelectionStart;

            // 暫時移除文本變化事件，避免在更新文本時引發循環
            txtDescription.TextChanged -= TxtDescription_TextChanged;
            txtDescription.Text = updatedText.ToString();
            txtDescription.TextChanged += TxtDescription_TextChanged;

            // 保證光標位置不會錯位
            txtDescription.SelectionStart = caretPosition < txtDescription.Text.Length ? caretPosition : txtDescription.Text.Length;
            txtDescription.ScrollToCaret();
        }

        private void RichDescription_MouseClick(object sender, MouseEventArgs e)
        {
            // 計算點擊的行號
            int charIndex = txtDescription.GetCharIndexFromPosition(e.Location);
            int lineIndex = txtDescription.GetLineFromCharIndex(charIndex);

            // 檢查點擊範圍是否有效
            if (lineIndex >= 0 && lineIndex < collapseStates.Count)
            {
                int lineStartIndex = txtDescription.GetFirstCharIndexFromLine(lineIndex);
                int relativeIndex = charIndex - lineStartIndex;

                // 只有在小三角區域點擊時才切換摺疊狀態
                if (relativeIndex >= 0 && relativeIndex <= 2) // 假設三角形占用前兩個字符位置
                {
                    collapseStates[lineIndex] = !collapseStates[lineIndex];
                    UpdateRichTextWithTriangles();
                }
            }
        }

        private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) // F1 用於呼叫反白區域的三角形
            {
                HighlightCurrentSelection();
                e.Handled = true;
            }
        }

        private void HighlightCurrentSelection()
        {
            int caretLineStart = txtDescription.SelectionStart;
            int caretLineEnd = caretLineStart + txtDescription.SelectionLength;

            // 取得選中的行範圍
            int startLine = txtDescription.GetLineFromCharIndex(caretLineStart);
            int endLine = txtDescription.GetLineFromCharIndex(caretLineEnd);

            // 初始化並設定選中範圍的摺疊狀態
            InitializeCollapseStatesForRange(startLine, endLine);

            // 更新摺疊狀態並顯示對應三角形
            for (int i = startLine; i <= endLine; i++)
            {
                collapseStates[i] = !collapseStates[i];
            }

            UpdateRichTextWithTriangles();
        }

        private void InitializeCollapseStatesForRange(int startLine, int endLine)
        {
            string[] lines = txtDescription.Text.Split(new[] { '\n' }, StringSplitOptions.None);

            for (int i = startLine; i <= endLine; i++)
            {
                if (!collapseStates.ContainsKey(i)) // 初始化不存在的鍵
                {
                    collapseStates[i] = false; // 預設所有段落未摺疊
                }
            }

            // 移除多餘的鍵（僅保留選中範圍內的摺疊狀態）
            foreach (var key in new List<int>(collapseStates.Keys))
            {
                if (key < startLine || key > endLine)
                {
                    collapseStates.Remove(key);
                }
            }

            UpdateRichTextWithTriangles();
        }

        private int GetCaretLine()
        {
            int index = txtDescription.SelectionStart;
            return txtDescription.GetLineFromCharIndex(index);
        }

        private void SetCaretToLine(int line)
        {
            int startIndex = txtDescription.GetFirstCharIndexFromLine(line);
            txtDescription.SelectionStart = startIndex >= 0 ? startIndex : txtDescription.Text.Length;
            txtDescription.SelectionLength = 0;
        }

        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
            // 每次文本變化時，記錄到撤銷棧，清空恢復棧
            undoStack.Push(description);
            description = txtDescription.Text;
            redoStack.Clear();
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

        private void MenuUndo_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                // 從撤銷棧取出最後一個狀態，保存到恢復棧
                redoStack.Push(description);
                description = undoStack.Pop();
                txtDescription.TextChanged -= TxtDescription_TextChanged; // 暫時取消事件避免循環
                txtDescription.Text = description;
                txtDescription.TextChanged += TxtDescription_TextChanged;
            }
        }

        private void MenuRedo_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                // 從恢復棧取出最後一個狀態，保存到撤銷棧
                undoStack.Push(description);
                description = redoStack.Pop();
                txtDescription.TextChanged -= TxtDescription_TextChanged; // 暫時取消事件避免循環
                txtDescription.Text = description;
                txtDescription.TextChanged += TxtDescription_TextChanged;
            }
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
