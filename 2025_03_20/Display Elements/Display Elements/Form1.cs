using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Display_Elements
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void getNamesButton_Click(object sender, EventArgs e)
        {
            // 建立一個陣列來存放三個字串。
            // 這裡定義了一個常數 SIZE，表示陣列的大小為 3。
            const int SIZE = 3;
            string[] names = new string[SIZE];

            // 從三個 TextBox 控制項中獲取名字並存入陣列。
            // 將 name1TextBox 的文字內容存入 names 陣列的第一個位置。
            names[0] = name1TextBox.Text;
            // 將 name2TextBox 的文字內容存入 names 陣列的第二個位置。
            names[1] = name2TextBox.Text;
            // 將 name3TextBox 的文字內容存入 names 陣列的第三個位置。
            names[2] = name3TextBox.Text;

            // 顯示名字。
            // 使用 MessageBox 顯示 names 陣列中的每個名字。
            MessageBox.Show(names[0]);
            MessageBox.Show(names[1]);
            MessageBox.Show(names[2]);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉表單。
            // 當使用者點擊 exitButton 時，關閉當前的表單。
            this.Close();
        }
    }
}
