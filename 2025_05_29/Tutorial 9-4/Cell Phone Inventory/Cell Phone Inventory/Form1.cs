using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell_Phone_Inventory
{
    public partial class Form1 : Form
    {
        // 用來儲存 CellPhone 物件的清單，每個物件代表一支手機的資料
        List<CellPhone> phoneList = new List<CellPhone>();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 取得使用者輸入的手機資料，並設定到傳入的 CellPhone 物件屬性中
        /// </summary>
        /// <param name="phone">要設定資料的 CellPhone 物件</param>
        private void GetPhoneData(CellPhone phone)
        {
            // 用來暫存價格的變數
            decimal price;

            // 取得品牌輸入欄位的文字，並設定到手機物件的品牌屬性
            phone.Brand = brandTextBox.Text;

            // 取得型號輸入欄位的文字，並設定到手機物件的型號屬性
            phone.Model = modelTextBox.Text;

            // 嘗試將價格輸入欄位的文字轉換為 decimal 型別
            if (decimal.TryParse(priceTextBox.Text, out price))
            {
                phone.Price = price;
            }
            else
            {
                // 顯示錯誤訊息，提醒使用者價格格式錯誤
                MessageBox.Show("價格格式無效，請輸入正確的數字。");
            }
        }

        /// <summary>
        /// 當使用者點擊「新增手機」按鈕時執行
        /// 本方法已依需求設定為不執行任何新增資料的動作
        /// </summary>
        private void addPhoneButton_Click(object sender, EventArgs e)
        {
            CellPhone myPhone = new CellPhone(); // 建立新的 CellPhone 物件

            GetPhoneData(myPhone); // 取得使用者輸入的手機資料

            // 將新的手機物件加入到手機清單中
            phoneList.Add(myPhone);

            //將新增手機的品牌與型號組合成字串，並加入到 ListBox 中
            phoneListBox.Items.Add( myPhone.Brand + " " + myPhone.Model);

            // 清空輸入欄位，準備下一次輸入
            brandTextBox.Text = "";
            modelTextBox.Text = "";
            priceTextBox.Text = "";
            // 將焦點設回品牌輸入欄位，方便使用者繼續輸入
            brandTextBox.Focus();
        }

        /// <summary>
        /// 當使用者在手機清單 ListBox 選取不同手機時執行
        /// 本方法已依需求設定為不執行任何動作
        /// </summary>
        private void phoneListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = phoneListBox.SelectedIndex; // 取得選取的手機在清單中的索引

            MessageBox.Show(phoneList[index].Price.ToString("C")); // 顯示選取手機的售價，格式化為貨幣顯示
        }

        /// <summary>
        /// 當使用者點擊「離開」按鈕時，關閉視窗並結束程式
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉表單（結束程式）
            this.Close();
        }
    }
}
