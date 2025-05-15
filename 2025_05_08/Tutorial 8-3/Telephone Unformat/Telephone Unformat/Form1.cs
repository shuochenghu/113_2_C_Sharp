using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Telephone_Unformat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // IsValidFormat 方法接受一個字串參數，並判斷該字串是否正確地格式化為
        // 台灣電話號碼的格式：(XX)XXXX-XXXX。
        // 如果字串符合此格式，則方法返回 true；否則返回 false。
        private bool IsValidFormat(string str)
        {
            // 使用一般字串處理方法檢查格式是否為 (XX)XXXX-XXXX
            if (str.Length == 13 &&
                str[0] == '(' &&
                str[3] == ')' &&
                str[8] == '-')
            {
                string areaCode = str.Substring(1, 2);
                string firstPart = str.Substring(4, 4);
                string secondPart = str.Substring(9, 4);

                // 確保括號內和破折號分隔的部分都是數字
                if (IsAllDigits(areaCode) &&
                    IsAllDigits(firstPart) &&
                    IsAllDigits(secondPart))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        // IsAllDigits 方法接受一個字串參數，並檢查該字串是否僅包含數字字符。
        // 如果字串僅包含數字字符，則方法返回 true；否則返回 false。
        private bool IsAllDigits(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        // Unformat 方法接受一個字串參數（以引用方式傳遞），
        // 假設該字串包含一個格式化的電話號碼，格式為：(XX)XXXX-XXXX。
        // 此方法會將字串中的括號和連字符移除，從而去除格式。
        private void Unformat(ref string str)
        {
            // 使用 Remove 方法移除括號和連字符
            str = str.Remove(0, 1); // 移除左括號 '('
            str = str.Remove(2, 1); // 移除右括號 ')'
            str = str.Remove(6, 1); // 移除連字符 '-'

            //str = str.Remove(0, 1).Remove(2, 1).Remove(6, 1); // 一行完成所有移除操作

            //str = str.Replace("(", "").Replace(")", "").Replace("-", ""); // 另一種方法
        }

        // 當使用者按下「去格式化」按鈕時觸發此事件。
        // 此方法會從 numberTextBox 控制項中取得使用者輸入的電話號碼，
        // 並進一步處理該輸入。
        private void unformatButton_Click(object sender, EventArgs e)
        {
            string input = numberTextBox.Text; // 取得使用者輸入的電話號碼

            if (IsValidFormat(input))
            {
                Unformat(ref input); // 去除格式
                MessageBox.Show("去格式化後的電話號碼為：" + input, "結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("請輸入正確格式的電話號碼！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 清空輸入框。
                numberTextBox.Text = string.Empty;
                // 將焦點設置到輸入框。
                numberTextBox.Focus();
            }


        }

        // 當使用者按下「離開」按鈕時觸發此事件。
        // 此方法會關閉目前的表單，結束應用程式。
        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉表單。
            this.Close();
        }

        private void instructionLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
