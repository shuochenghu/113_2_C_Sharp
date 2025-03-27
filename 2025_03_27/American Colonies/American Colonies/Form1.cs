using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace American_Colonies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // SequentialSearch 方法在字串陣列中搜尋指定的值。
        // 如果找到該值，則返回其位置。否則，返回 -1。
        private int SequentialSearch(string[] sArray, string value)
        {
            bool found = false;  // 標誌搜尋結果，初始為 false 表示尚未找到
            int index = 0;       // 用於遍歷陣列的索引
            int position = -1;   // 值的位置，如果找到則更新，初始為 -1 表示未找到

            // 使用 while 迴圈遍歷陣列，直到找到值或遍歷完陣列
            while (!found && index < sArray.Length)
            {
                // 如果當前索引的值等於搜尋值
                if (sArray[index] == value)
                {
                    found = true;      // 更新標誌為 true 表示找到
                    position = index;  // 設定 position 為當前索引
                }

                index++;  // 移動到下一個索引
            }

            // 返回找到的值的位置，或 -1 表示未找到
            return position;
        }

        // okButton_Click 方法在使用者點擊 "確定" 按鈕時觸發
        private void okButton_Click(object sender, EventArgs e)
        {
            string selection;   // 保存使用者的選擇

            // 建立包含殖民地名稱的陣列
            string[] colonies = {  "德拉瓦", "賓夕法尼亞", "新澤西",
                                            "喬治亞", "康乃狄克", "麻薩諸塞",
                                            "馬里蘭", "南卡羅來納", "新罕布夏",
                                            "維吉尼亞", "紐約", "北卡羅來納",
                                            "羅德島" };

            // 檢查是否有選擇項目
            if (selectionListBox.SelectedIndex != -1)
            {
                // 獲取選定的項目
                selection = selectionListBox.SelectedItem.ToString();

                // 確定該項目是否在殖民地陣列中
                if (SequentialSearch(colonies, selection) != -1)
                {
                    // 如果找到，顯示訊息框通知使用者
                    MessageBox.Show("是的，那是其中一個殖民地。");
                }
                else
                {
                    // 如果未找到，顯示訊息框通知使用者
                    MessageBox.Show("不，那不是其中一個殖民地。");
                }
            }
        }

        // exitButton_Click 方法在使用者點擊 "退出" 按鈕時觸發
        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉表單
            this.Close();
        }
    }
}
