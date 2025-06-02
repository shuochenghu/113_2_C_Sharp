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
                if (openFileTeam.ShowDialog() == DialogResult.OK)
                {
                    team = new List<string>();
                    StreamReader inputFile = File.OpenText(openFileTeam.FileName);

                    // 逐行讀取檔案，並將每一行（球隊名稱）加入 listBox1 及 team 清單，並加上編號
                    string line;
                    int idx = 1;
                    listBox1.Items.Clear();
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        listBox1.Items.Add($"{idx}. {line}");
                        team.Add(line);
                        idx++;
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
                if (openFileWinner.ShowDialog() == DialogResult.OK)
                {
                    winner = new List<string>();
                    StreamReader inputFile = File.OpenText(openFileWinner.FileName);

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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || team == null || winner == null)
                return;

            // 取得選取的球隊名稱（去除前面的編號與點）
            string selectedItem = listBox1.SelectedItem.ToString();
            int dotIndex = selectedItem.IndexOf('.');
            string str = (dotIndex >= 0 && dotIndex + 2 < selectedItem.Length)
                ? selectedItem.Substring(dotIndex + 2)
                : selectedItem;

            int numWin = 0;
            List<int> winYears = new List<int>();

            // 計算 winner list 中每一筆對應的年份（排除1904、1994）
            int[] skipYears = { 1904, 1994 };
            int year = 1903;
            int winnerIndex = 0;
            List<int> allYears = new List<int>();

            while (winnerIndex < winner.Count)
            {
                // 跳過未舉辦的年份
                if (Array.IndexOf(skipYears, year) >= 0)
                {
                    year++;
                    continue;
                }
                allYears.Add(year);
                if (winner[winnerIndex] == str)
                {
                    numWin++;
                    winYears.Add(year);
                }
                winnerIndex++;
                year++;
            }

            // 計算最後一筆資料所代表的年份
            int lastYear = allYears.Count > 0 ? allYears[allYears.Count - 1] : year - 1;

            // 組合奪冠年份字串
            string yearsText = winYears.Count > 0 ? string.Join("、", winYears) : "無";
            label1.Text = $"{str} 自 1903 年至 {lastYear} 年共獲得世界大賽冠軍 {numWin} 次。\n奪冠年份：{yearsText}";
        }



        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // 1. 使用 openFileExtra 讓使用者選擇自2010年起之後各年的冠軍球隊名稱檔案
            if (openFileExtra.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<string> newWinners = new List<string>();
                    using (StreamReader inputFile = File.OpenText(openFileExtra.FileName))
                    {
                        string line;
                        while ((line = inputFile.ReadLine()) != null)
                        {
                            newWinners.Add(line);
                        }
                    }

                    // 2. 更新 winner list
                    if (winner == null)
                        winner = new List<string>();
                    // 將新冠軍隊伍逐一加入 winner 清單
                    foreach (var w in newWinners)
                    {
                        winner.Add(w);
                    }

                    // 3. 檢查新球隊名稱是否存在於 team list，若無則新增
                    AddNewTeamsToListBoxAndTeam(newWinners);

                    MessageBox.Show("資料已成功新增。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("新增資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("未選擇新增資料檔案。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 檢查新球隊名稱是否存在於 team list，若無則新增到 team 與 listBox1
        private void AddNewTeamsToListBoxAndTeam(List<string> newWinners)
        {
            if (team == null)
                team = new List<string>();

            int idx = team.Count + 1;
            // 清除 listBox1 中的舊項目，重新填充
            foreach (string teamName in newWinners) // 使用 Distinct() 確保不重複
            {
                if (!team.Contains(teamName))
                {
                    team.Add(teamName);
                    listBox1.Items.Add($"{idx}. {teamName}");
                    idx++;
                }
            }
        }


        private void buttonExit_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 將 team 與 winner 內容寫回一開始讀取的檔案
                // 假設 openFileTeam 與 openFileWinner 分別為球隊與冠軍的 OpenFileDialog
                if (openFileTeam.FileName != null && openFileTeam.FileName != "" && team != null)
                {
                    using (StreamWriter sw = new StreamWriter(openFileTeam.FileName, false, Encoding.UTF8))
                    {
                        foreach (string t in team)
                        {
                            sw.WriteLine(t);
                        }
                    }
                }

                if (openFileWinner.FileName != null && openFileWinner.FileName != "" && winner != null)
                {
                    using (StreamWriter sw = new StreamWriter(openFileWinner.FileName, false, Encoding.UTF8))
                    {
                        foreach (string w in winner)
                        {
                            sw.WriteLine(w);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存資料時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 2. 離開程式
            this.Close();
        }

    }
}
