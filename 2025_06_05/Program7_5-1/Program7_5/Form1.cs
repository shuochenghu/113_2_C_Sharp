//專案名稱：Program7_5 - 1
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Program7_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 儲存所有球隊名稱的字串清單
        List<string> team;
        // 儲存所有世界大賽冠軍隊伍名稱的字串清單
        List<string> winner;

        // 表單載入時執行，讀取球隊與冠軍資料
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("請先選擇球隊資料檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            readTeams();
            MessageBox.Show("再選擇世界大賽冠軍資料檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            readWinner();
        }

        // 讀取 Teams.txt 檔案，將球隊名稱加入 listBox1 並存入 team 清單
        private void readTeams()
        {
            try
            {
                //OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Title = "請選擇球隊資料檔案";
                //openFileDialog.Filter = "文字檔案 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
                //openFileDialog.FileName = "Teams.txt";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    team = new List<string>();
                    StreamReader inputFile = File.OpenText(openFile.FileName);

                    // 逐行讀取檔案，並將每一行（球隊名稱）加入 listBox1 及 team 清單
                    string line;
                    listBox1.Items.Clear();
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                        team.Add(line);
                    }

                    inputFile.Close();
                }
                else
                {
                    MessageBox.Show("未選擇球隊資料檔案，無法載入球隊資料。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // 若發生例外，顯示錯誤訊息（繁體中文）
                MessageBox.Show("讀取球隊資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 讀取 WorldSeries.txt 檔案，將每年冠軍隊伍名稱存入 winner 清單
        private void readWinner()
        {
            try
            {
                //OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Title = "請選擇世界大賽冠軍資料檔案";
                //openFileDialog.Filter = "文字檔案 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
                //openFileDialog.FileName = "WorldSeries.txt";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    winner = new List<string>();
                    StreamReader inputFile = File.OpenText(openFile.FileName);

                    // 逐行讀取檔案，將每年冠軍隊伍名稱存入 winner 清單
                    string line;
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        winner.Add(line);
                    }
                    inputFile.Close();
                }
                else
                {
                    MessageBox.Show("未選擇世界大賽冠軍資料檔案，無法載入冠軍資料。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // 若發生例外，顯示錯誤訊息（繁體中文）
                MessageBox.Show("讀取冠軍資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 當使用者在 listBox1 選取不同球隊時，計算該球隊奪冠次數並顯示於 label1，並列出奪冠年份
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = listBox1.SelectedItem.ToString();
            int numWin = 0;
            List<int> winYears = new List<int>();

            // 世界大賽資料年份從1903年開始，winner[0]對應1903年
            int year = 1903;
            int winnerIndex = 0;

            while (winnerIndex < winner.Count && year <= 2009)
            {
                // 1904年與1994年未舉辦比賽，需跳過
                if (year == 1904 || year == 1994)
                {
                    year++;
                    continue;
                }

                if (winner[winnerIndex] == str)
                {
                    numWin++;
                    winYears.Add(year);
                }
                winnerIndex++;
                year++;
            }

            // 組合奪冠年份字串
            string yearsText = winYears.Count > 0 ? string.Join("、", winYears) : "無";
            // 顯示選取球隊自 1903 年至 2009 年的奪冠次數與年份（繁體中文）
            label1.Text = $"{str} 自 1903 年至 2009 年共獲得世界大賽冠軍 {numWin} 次。\n奪冠年份：{yearsText}";
        }
    }
}
