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
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 檔案ToolStripMenuItem, 查看月曆ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(622, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            檔案ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 切換使用者ToolStripMenuItem });
            檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            檔案ToolStripMenuItem.Size = new Size(43, 20);
            檔案ToolStripMenuItem.Text = "檔案";
            // 
            // 切換使用者ToolStripMenuItem
            // 
            切換使用者ToolStripMenuItem.Name = "切換使用者ToolStripMenuItem";
            切換使用者ToolStripMenuItem.Size = new Size(180, 22);
            切換使用者ToolStripMenuItem.Text = "切換使用者";
            切換使用者ToolStripMenuItem.Click += 切換使用者ToolStripMenuItem_Click;
            // 
            // 查看月曆ToolStripMenuItem
            // 
            查看月曆ToolStripMenuItem.Name = "查看月曆ToolStripMenuItem";
            查看月曆ToolStripMenuItem.Size = new Size(67, 20);
            查看月曆ToolStripMenuItem.Text = "查看月曆";
            查看月曆ToolStripMenuItem.Click += 查看月曆ToolStripMenuItem_Click;
            // 
            // Form_home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 355);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form_home";
            Text = "Form_home";
            FormClosing += Form_home_FormClosing;
            MouseDown += Form_home_MouseDown;
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
    }
}