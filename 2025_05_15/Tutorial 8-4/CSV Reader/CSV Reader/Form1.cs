using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CSV_Reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 當使用者點擊「取得分數」按鈕時觸發此事件。
        /// 此方法會顯示檔案選擇對話方塊，讓使用者選擇要開啟的 CSV 檔案。
        /// 若選擇檔案，則開啟該檔案進行後續處理；若未選擇，則顯示提示訊息。
        /// 若發生例外，則顯示錯誤訊息。
        /// </summary>
        private void getScoresButton_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader inputFile; // 宣告 StreamReader 物件以讀取檔案
                string line; // 用來儲存讀取的每一列資料
                int count = 0; // 計數器，用來計算讀取的列數
                int total = 0; // 總分數，用來計算所有分數的總和
                double average; // 平均分數，用來計算所有分數的平均值

                char[] delim = {',', ' '}; // 定義分隔符號，這裡使用逗號和空格

                // 顯示檔案選擇對話方塊，讓使用者選擇要開啟的檔案
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    // 使用者已選擇檔案，開啟該檔案以供讀取
                    inputFile = File.OpenText(openFile.FileName);

                    while (!inputFile.EndOfStream)
                    {
                        // 讀取每一列資料
                        line = inputFile.ReadLine();
                        line = line.Trim(); // 去除前後空白
                        string[] tokens = line.Split(delim); // 使用分隔符號將資料分割成陣列
                        total = 0; // 每次讀取新列時，重置總分數為 0
                        // 將分數轉換為整數並累加到總分數中
                        for (int i = 0; i < tokens.Length; i++)
                        {
                            // 將字串轉換為整數，並累加到總分數中
                            total += int.Parse(tokens[i]);
                        }
                        // 計算平均分數
                        average = (double)total / tokens.Length; // 計算平均值
                        // 將平均分數加入 ListBox 中
                        averagesListBox.Items.Add("第 " + (++count) + " 位同學的平均分數為：" + average.ToString("F2")); // 顯示平均值，保留兩位小數
                    }
                }
                else
                {
                    // 使用者未選擇任何檔案，顯示提示訊息
                    MessageBox.Show("未選擇任何檔案。");
                }
            }
            catch (Exception ex)
            {
                // 發生例外時，顯示錯誤訊息與詳細原因
                MessageBox.Show("發生錯誤：" + ex.Message);
            }
        }

        /// <summary>
        /// 當使用者點擊「離開」按鈕時觸發此事件。
        /// 此方法會關閉目前的表單，結束應用程式。
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉目前的表單，結束應用程式
            this.Close();
        }
    }
}
