//專案名稱： Program7_5 - 3

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
    struct TeamData
    {
        public string Name; // 球隊名稱
        public int Wins; // 獲勝次數
        public List<int> WinYears; // 獲勝年份列表
    }

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
        // 儲存所有球隊與冠軍資料的清單
        List<TeamData> teamDataList = new List<TeamData>();

        // 表單載入時執行，讀取球隊與冠軍資料
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("請先選擇球隊資料檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            readTeams();
            MessageBox.Show("再選擇世界大賽冠軍資料檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            readWinner();

            getTeamDataList();
        }

        // 取得每支球隊的冠軍資料，並存入 teamDataList
        private void getTeamDataList()
        {
            teamDataList.Clear();
            if (team == null || winner == null)
                return;

            // 計算原始冠軍資料的筆數（1903~2009，排除1904、1994）
            int[] skipYears = { 1904, 1994 };
            int baseStartYear = 1903;
            int baseEndYear = 2009;
            int baseCount = 0;
            for (int y = baseStartYear; y <= baseEndYear; y++)
            {
                if (Array.IndexOf(skipYears, y) < 0)
                    baseCount++;
            }

            foreach (string teamName in team)
            {
                TeamData data = new TeamData();
                data.Name = teamName;
                data.Wins = 0;
                data.WinYears = new List<int>();

                // 1. 處理原始 winner（1903~2009，排除1904、1994）
                int year = baseStartYear;
                int winnerIndex = 0;
                while (winnerIndex < winner.Count && winnerIndex < baseCount)
                {
                    if (year == 1904 || year == 1994)
                    {
                        year++;
                        continue;
                    }
                    if (winner[winnerIndex] == teamName)
                    {
                        data.Wins++;
                        data.WinYears.Add(year);
                    }
                    winnerIndex++;
                    year++;
                }

                // 2. 處理新增的 winner（2010年起）
                int extraYear = 2010;
                for (int i = winnerIndex; i < winner.Count; i++)
                {
                    if (winner[i] == teamName)
                    {
                        data.Wins++;
                        data.WinYears.Add(extraYear);
                    }
                    extraYear++;
                }

                teamDataList.Add(data);
            }
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
            if (listBox1.SelectedIndex < 0 || teamDataList == null || teamDataList.Count == 0)
                return;

            // 取得選取的球隊名稱（去除前面的編號與點）
            string selectedItem = listBox1.SelectedItem.ToString();
            int dotIndex = selectedItem.IndexOf('.');
            string str = (dotIndex >= 0 && dotIndex + 2 < selectedItem.Length)
                ? selectedItem.Substring(dotIndex + 2)
                : selectedItem;

            // 用基本迴圈尋找對應球隊資料
            //TeamData? foundData = null;
            bool found = false;
            TeamData foundData = new TeamData();
            for (int i = 0; i < teamDataList.Count; i++)
            {
                if (teamDataList[i].Name == str)
                {
                    foundData = teamDataList[i];
                    found = true;
                    break;
                }
            }

            // 計算最後一筆資料所代表的年份（排除1904與1994）
            int[] skipYears = { 1904, 1994 };
            int startYear = 1903;
            int winnerCount = winner != null ? winner.Count : 0;
            int year = startYear;
            int count = 0;
            int lastYear = startYear;

            while (count < winnerCount)
            {
                if (Array.IndexOf(skipYears, year) < 0)
                {
                    lastYear = year;
                    count++;
                }
                year++;
            }

            if (found)
            {
                string yearsText = foundData.WinYears.Count > 0 ? string.Join("、", foundData.WinYears) : "無";
                label1.Text = $"{foundData.Name} 自 1903 年至 {lastYear} 年共獲得世界大賽冠軍 {foundData.Wins} 次。\n奪冠年份：{yearsText}";
            }
            else
            {
                label1.Text = $"{str} 尚未獲得世界大賽冠軍。";
            }
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

                    getTeamDataList(); // 更新 teamDataList

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
