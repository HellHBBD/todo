﻿namespace todo
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
            月曆ToolStripMenuItem = new ToolStripMenuItem();
            象限圖ToolStripMenuItem = new ToolStripMenuItem();
            順序ToolStripMenuItem = new ToolStripMenuItem();
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
            menuStrip1.Size = new Size(977, 34);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            檔案ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 切換使用者ToolStripMenuItem });
            檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            檔案ToolStripMenuItem.Size = new Size(62, 28);
            檔案ToolStripMenuItem.Text = "檔案";
            // 
            // 切換使用者ToolStripMenuItem
            // 
            切換使用者ToolStripMenuItem.Name = "切換使用者ToolStripMenuItem";
            切換使用者ToolStripMenuItem.Size = new Size(200, 34);
            切換使用者ToolStripMenuItem.Text = "切換使用者";
            切換使用者ToolStripMenuItem.Click += 切換使用者ToolStripMenuItem_Click;
            // 
            // 查看月曆ToolStripMenuItem
            // 
            查看月曆ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 月曆ToolStripMenuItem, 象限圖ToolStripMenuItem, 順序ToolStripMenuItem });
            查看月曆ToolStripMenuItem.Name = "查看月曆ToolStripMenuItem";
            查看月曆ToolStripMenuItem.Size = new Size(62, 28);
            查看月曆ToolStripMenuItem.Text = "排版";
            // 
            // 月曆ToolStripMenuItem
            // 
            月曆ToolStripMenuItem.Name = "月曆ToolStripMenuItem";
            月曆ToolStripMenuItem.Size = new Size(270, 34);
            月曆ToolStripMenuItem.Text = "以月曆檢視";
            月曆ToolStripMenuItem.Click += 月曆ToolStripMenuItem_Click;
            // 
            // 象限圖ToolStripMenuItem
            // 
            象限圖ToolStripMenuItem.Name = "象限圖ToolStripMenuItem";
            象限圖ToolStripMenuItem.Size = new Size(270, 34);
            象限圖ToolStripMenuItem.Text = "查看任務程度";
            // 
            // 順序ToolStripMenuItem
            // 
            順序ToolStripMenuItem.Name = "順序ToolStripMenuItem";
            順序ToolStripMenuItem.Size = new Size(270, 34);
            順序ToolStripMenuItem.Text = "以先後順序排序";
            // 
            // Form_home
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 544);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
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
        private ToolStripMenuItem 月曆ToolStripMenuItem;
        private ToolStripMenuItem 象限圖ToolStripMenuItem;
        private ToolStripMenuItem 順序ToolStripMenuItem;
    }
}