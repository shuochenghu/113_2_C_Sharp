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

namespace Phonebook
{
    // 結構：用來儲存電話簿的每一筆資料，包括姓名與電話號碼
    struct PhoneBookEntry
    {
        public string name;   // 姓名
        public string phone;  // 電話號碼
    }

    public partial class Form1 : Form
    {
        // 欄位：用來儲存所有電話簿資料的清單
        private List<PhoneBookEntry> phoneList = new List<PhoneBookEntry>();

        public Form1()
        {
            InitializeComponent();
        }

        // ReadFile 方法：
        // 讀取 PhoneList.txt 檔案內容，將每一筆資料轉換成 PhoneBookEntry 結構，
        // 並加入 phoneList 清單中。
        // 若檔案不存在或格式錯誤，會顯示錯誤訊息。
        private void ReadFile()
        {
            StreamReader inputFile; // 開啟檔案的 StreamReader 物件
            if (openFile.ShowDialog() == DialogResult.OK) // 選擇檔案
            {
                try // 嘗試開啟檔案
                {
                    inputFile = File.OpenText(openFile.FileName);  // 開啟檔案
                    string line;
                    while ( !inputFile.EndOfStream)  // 讀取檔案直到結尾
                    {
                        // 讀取一行資料，並去除前後空白
                        line = inputFile.ReadLine().Trim();
                        // 將資料以逗號分隔，並檢查是否有兩個部分
                        string[] parts = line.Split(',');
                        if (parts.Length == 2) // 檔案格式正確
                        {
                            PhoneBookEntry entry; // 宣告一個 PhoneBookEntry 結構
                            entry.name = parts[0].Trim(); // 姓名
                            entry.phone = parts[1].Trim(); // 電話號碼
                            phoneList.Add(entry); // 將資料加入清單中
                        }
                        else // 檔案格式錯誤
                        {
                            MessageBox.Show("檔案格式錯誤");
                        }
                    }
                    inputFile.Close();
                }
                catch (Exception ex)  // 嘗試開啟檔案時發生錯誤
                {
                    MessageBox.Show("讀取檔案時發生錯誤：" + ex.Message);
                }
            }

        }

        // DisplayNames 方法：
        // 將 phoneList 清單中的所有姓名顯示在 nameListBox 控制項中，
        // 讓使用者可以從清單中選擇要查詢的姓名。
        private void DisplayNames()
        {
            foreach (PhoneBookEntry entry in phoneList) // 遍歷所有電話簿資料
            {
                nameListBox.Items.Add(entry.name); // 將姓名加入 nameListBox 控制項中
            }
        }

        // Form1_Load 事件處理函式：
        // 當表單載入時自動執行，
        // 會呼叫 ReadFile 方法讀取電話簿資料，
        // 並呼叫 DisplayNames 方法顯示所有姓名。
        private void Form1_Load(object sender, EventArgs e)
        {
            ReadFile(); // 讀取電話簿資料

            DisplayNames(); // 顯示所有姓名在 nameListBox 控制項中
        }

        // nameListBox_SelectedIndexChanged 事件處理函式：
        // 當使用者在 nameListBox 控制項中選擇不同的姓名時，
        // 會顯示對應的電話號碼在 phoneLabel 控制項上。
        private void nameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = nameListBox.SelectedIndex; // 取得選擇的索引
            if (index != -1)
            {
                // 顯示對應的電話號碼在 phoneLabel 控制項上
                phoneLabel.Text = phoneList[index].phone;
            }
            else
            {
                // 如果沒有選擇任何姓名，則清空 phoneLabel 控制項的文字
                phoneLabel.Text = "無資料";
            }
        }

        // exitButton_Click 事件處理函式：
        // 當使用者按下「離開」按鈕時，會將 phoneList 清單內容寫回原本開啟的檔案，然後關閉整個表單。
        private void exitButton_Click(object sender, EventArgs e)
        {
            // 檢查是否有開啟過檔案
            if (!string.IsNullOrEmpty(openFile.FileName) && File.Exists(openFile.FileName))
            {
                StreamWriter writer = null;
                try
                {
                    // 使用 File.CreateText 以覆寫方式開啟檔案
                    writer = File.CreateText(openFile.FileName);
                    foreach (PhoneBookEntry entry in phoneList)
                    {
                        writer.WriteLine($"{entry.name},{entry.phone}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("儲存檔案時發生錯誤：" + ex.Message);
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }
            else
            {
                MessageBox.Show("找不到原始檔案，無法儲存資料。");
            }
            // 關閉表單
            this.Close();
        }

        //讀取textBoxName與textBoxPhone的資料，加入phoneList清單中
        //更新nameListBox顯示的內容
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            PhoneBookEntry entry; // 宣告一個 PhoneBookEntry 結構
            entry.name = textBoxName.Text.Trim(); // 姓名
            entry.phone = textBoxPhone.Text.Trim(); // 電話號碼
            // 檢查姓名與電話號碼是否為空
            if (string.IsNullOrEmpty(entry.name) || string.IsNullOrEmpty(entry.phone))
            {
                MessageBox.Show("姓名或電話號碼不能為空");
                return;
            }
            // 檢查姓名是否已存在
            foreach (PhoneBookEntry existingEntry in phoneList)
            {
                if (existingEntry.name.Equals(entry.name, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("姓名已存在");
                    return;
                }
            }
            // 將資料加入清單
            phoneList.Add(entry); // 將資料加入清單中
            nameListBox.Items.Add(entry.name); // 將姓名加入 nameListBox 控制項中
            // 清空 textBoxName 與 textBoxPhone 控制項的文字
            textBoxName.Clear();
            textBoxPhone.Clear();
            // 清空 phoneLabel 控制項的文字
            phoneLabel.Text = "";
        }
    }
}
