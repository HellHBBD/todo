using System;
using System.Collections.Generic;

namespace todo
{
    public class FoldableTextManager
    {
        public bool IsFoldableMode { get; private set; }
        private List<FoldableLine> FoldableLines;

        public FoldableTextManager(string initialText)
        {
            FoldableLines = ParseTextToFoldableLines(initialText);
            IsFoldableMode = false; // 默認為非折疊模式
        }

        public void ToggleFoldMode()
        {
            IsFoldableMode = !IsFoldableMode;
            UpdateVisibility();
        }

        public string[] GetFoldedText()
        {
            List<string> foldedLines = new List<string>();

            foreach (var line in FoldableLines)
            {
                string lineText = new string(' ', line.Level * 4) + (line.IsFolded ? "► " : "▼ ") + line.Text; // 使用三角形符號
                if (line.IsVisible) // 只顯示可見的行
                {
                    foldedLines.Add(lineText);
                }
            }

            return foldedLines.ToArray(); // 返回折疊後的行文字
        }

        public void ToggleFoldState(int lineIndex)
        {
            if (lineIndex >= 0 && lineIndex < FoldableLines.Count)
            {
                var line = FoldableLines[lineIndex];
                line.IsFolded = !line.IsFolded; // 切換摺疊狀態
                UpdateVisibility();
            }
        }

        private void UpdateVisibility()
        {
            foreach (var line in FoldableLines)
            {
                if (line.IsFolded)
                {
                    line.IsVisible = false; // 如果折疊則隱藏子節點
                }
                else
                {
                    line.IsVisible = true; // 顯示
                }
            }
        }

        private List<FoldableLine> ParseTextToFoldableLines(string text)
        {
            var lines = text.Split('\n');
            var foldableLines = new List<FoldableLine>();
            int currentLevel = 0;

            foreach (var line in lines)
            {
                // 確定層級
                int level = GetTabLevel(line);
                string trimmedLine = line.TrimStart(); // 去除前導空白

                // 更新層級關係
                if (level > currentLevel)
                {
                    // 新增子節點
                    currentLevel = level;
                }
                else if (level < currentLevel)
                {
                    // 回退到對應層級
                    currentLevel = level;
                }

                // 將行新增到節點列表
                foldableLines.Add(new FoldableLine
                {
                    Text = trimmedLine,
                    IsVisible = true,
                    IsFolded = false,
                    Level = level
                });
            }

            return foldableLines;
        }


        private int GetTabLevel(string line)
        {
            int tabCount = 0;
            while (line.StartsWith("\t"))
            {
                tabCount++;
                line = line.Substring(1); // 去除一個 tab 字符
            }
            return tabCount;
        }
    }

    public class FoldableLine
    {
        public string Text = "";
        public bool IsVisible { get; set; }
        public bool IsFolded { get; set; }
        public int Level { get; set; } // 縮排層級，根據 tab 數量確定
    }
}
