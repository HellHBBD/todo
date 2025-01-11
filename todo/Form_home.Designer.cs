namespace todo
{
    partial class Form_home
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
            menuStrip1 = new MenuStrip();
            檔案ToolStripMenuItem = new ToolStripMenuItem();
            切換使用者ToolStripMenuItem = new ToolStripMenuItem();
            查看月曆ToolStripMenuItem = new ToolStripMenuItem();
            以新增順序排序預設ToolStripMenuItem = new ToolStripMenuItem();
            月曆ToolStripMenuItem = new ToolStripMenuItem();
            象限圖ToolStripMenuItem = new ToolStripMenuItem();
            順序ToolStripMenuItem = new ToolStripMenuItem();
            以緊急重要程度排序ToolStripMenuItem = new ToolStripMenuItem();
            存檔ToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 檔案ToolStripMenuItem, 查看月曆ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 3, 0, 3);
            menuStrip1.Size = new Size(977, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            檔案ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 切換使用者ToolStripMenuItem, 存檔ToolStripMenuItem });
            檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            檔案ToolStripMenuItem.Size = new Size(53, 24);
            檔案ToolStripMenuItem.Text = "檔案";
            // 
            // 切換使用者ToolStripMenuItem
            // 
            切換使用者ToolStripMenuItem.Name = "切換使用者ToolStripMenuItem";
            切換使用者ToolStripMenuItem.Size = new Size(224, 26);
            切換使用者ToolStripMenuItem.Text = "切換使用者";
            切換使用者ToolStripMenuItem.Click += 切換使用者ToolStripMenuItem_Click;
            // 
            // 查看月曆ToolStripMenuItem
            // 
            查看月曆ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 以新增順序排序預設ToolStripMenuItem, 月曆ToolStripMenuItem, 象限圖ToolStripMenuItem, 順序ToolStripMenuItem, 以緊急重要程度排序ToolStripMenuItem });
            查看月曆ToolStripMenuItem.Name = "查看月曆ToolStripMenuItem";
            查看月曆ToolStripMenuItem.Size = new Size(53, 24);
            查看月曆ToolStripMenuItem.Text = "排版";
            // 
            // 以新增順序排序預設ToolStripMenuItem
            // 
            以新增順序排序預設ToolStripMenuItem.Name = "以新增順序排序預設ToolStripMenuItem";
            以新增順序排序預設ToolStripMenuItem.Size = new Size(237, 26);
            以新增順序排序預設ToolStripMenuItem.Text = "依新增順序排序(預設)";
            以新增順序排序預設ToolStripMenuItem.Click += 以新增順序排序預設ToolStripMenuItem_Click;
            // 
            // 月曆ToolStripMenuItem
            // 
            月曆ToolStripMenuItem.Name = "月曆ToolStripMenuItem";
            月曆ToolStripMenuItem.Size = new Size(237, 26);
            月曆ToolStripMenuItem.Text = "以月曆檢視";
            月曆ToolStripMenuItem.Click += 月曆ToolStripMenuItem_Click;
            // 
            // 象限圖ToolStripMenuItem
            // 
            象限圖ToolStripMenuItem.Name = "象限圖ToolStripMenuItem";
            象限圖ToolStripMenuItem.Size = new Size(237, 26);
            象限圖ToolStripMenuItem.Text = "查看四象限圖";
            象限圖ToolStripMenuItem.Click += 象限圖ToolStripMenuItem_Click;
            // 
            // 順序ToolStripMenuItem
            // 
            順序ToolStripMenuItem.Name = "順序ToolStripMenuItem";
            順序ToolStripMenuItem.Size = new Size(237, 26);
            順序ToolStripMenuItem.Text = "依先後順序排序";
            順序ToolStripMenuItem.Click += 順序ToolStripMenuItem_Click;
            // 
            // 以緊急重要程度排序ToolStripMenuItem
            // 
            以緊急重要程度排序ToolStripMenuItem.Name = "以緊急重要程度排序ToolStripMenuItem";
            以緊急重要程度排序ToolStripMenuItem.Size = new Size(237, 26);
            以緊急重要程度排序ToolStripMenuItem.Text = "依緊急重要程度排序";
            以緊急重要程度排序ToolStripMenuItem.Click += 以緊急重要程度排序ToolStripMenuItem_Click;
            // 
            // 存檔ToolStripMenuItem
            // 
            存檔ToolStripMenuItem.Name = "存檔ToolStripMenuItem";
            存檔ToolStripMenuItem.Size = new Size(224, 26);
            存檔ToolStripMenuItem.Text = "存檔";
            存檔ToolStripMenuItem.Click += 存檔ToolStripMenuItem_Click;
            // 
            // Form_home
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 544);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            Name = "Form_home";
            Text = "Form_home";
            FormClosing += Form_home_FormClosing;
            MouseDoubleClick += Form_home_MouseDoubleClick;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 檔案ToolStripMenuItem;
        private ToolStripMenuItem 切換使用者ToolStripMenuItem;
        private ToolStripMenuItem 查看月曆ToolStripMenuItem;
        private ToolStripMenuItem 月曆ToolStripMenuItem;
        private ToolStripMenuItem 象限圖ToolStripMenuItem;
        private ToolStripMenuItem 順序ToolStripMenuItem;
        private ToolStripMenuItem 以緊急重要程度排序ToolStripMenuItem;
        private ToolStripMenuItem 以新增順序排序預設ToolStripMenuItem;
        private ToolStripMenuItem 存檔ToolStripMenuItem;
    }
}
