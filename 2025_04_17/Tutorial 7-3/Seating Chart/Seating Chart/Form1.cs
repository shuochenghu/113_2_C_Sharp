using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seating_Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // 初始化表單元件
        }

        /// <summary>
        /// 當使用者按下「顯示價格」按鈕時觸發的事件處理函式。
        /// 根據使用者輸入的行號與列號，顯示對應座位的價格。
        /// </summary>
        private void displayPriceButton_Click(object sender, EventArgs e)
        {
            // 定義座位的行數與列數
            const int ROWS = 6; // 總共有 6 行
            const int COLS = 4; // 總共有 4 列

            int row; // 儲存使用者輸入的行號
            int col; // 儲存使用者輸入的列號

            // 定義座位價格的二維陣列，每個元素代表對應座位的價格
            decimal[,] seatPrices =  {
                                        {450m, 450m, 450m, 450m}, // 第一行的座位價格
                                        {425m, 425m, 425m, 425m}, // 第二行的座位價格
                                        {400m, 400m, 400m, 400m}, // 第三行的座位價格
                                        {375m, 375m, 375m, 375m}, // 第四行的座位價格
                                        {375m, 375m, 375m, 375m}, // 第五行的座位價格
                                        {350m, 350m, 350m, 350m}  // 第六行的座位價格
                                     };

            // 嘗試將使用者輸入的行號轉換為整數
            if (int.TryParse(rowTextBox.Text, out row))
            {
                // 嘗試將使用者輸入的列號轉換為整數
                if (int.TryParse(colTextBox.Text, out col))
                {
                    // 檢查行號是否在有效範圍內
                    if (row >= 0 && row < seatPrices.GetLength(0))
                    {
                        // 檢查列號是否在有效範圍內
                        if (col >= 0 && col < seatPrices.GetLength(1))
                        {
                            // 顯示對應座位的價格，格式化為美元貨幣格式
                            priceLabel.Text = seatPrices[row, col].ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                        }
                        else
                        {
                            // 列號超出範圍，顯示錯誤訊息
                            MessageBox.Show("行編號必須在 0 到 3 之間!");
                            colTextBox.Focus(); // 將焦點移回列號輸入框
                            return;
                        }
                    }
                    else
                    {
                        // 行號超出範圍，顯示錯誤訊息
                        MessageBox.Show("列編號必須在 0 到 5 之間!");
                        rowTextBox.Focus(); // 將焦點移回行號輸入框
                        return;
                    }
                }
                else
                {
                    // 列號輸入無效，顯示錯誤訊息
                    MessageBox.Show("請輸入有效的行號。");
                    colTextBox.Focus(); // 將焦點移回列號輸入框
                    return;
                }
            }
            else
            {
                // 行號輸入無效，顯示錯誤訊息
                MessageBox.Show("請輸入有效的列號。");
                rowTextBox.Focus(); // 將焦點移回行號輸入框
                return;
            }
        }

        /// <summary>
        /// 當使用者按下「離開」按鈕時觸發的事件處理函式。
        /// 關閉表單並結束應用程式。
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close(); // 關閉表單
        }
    }
}
