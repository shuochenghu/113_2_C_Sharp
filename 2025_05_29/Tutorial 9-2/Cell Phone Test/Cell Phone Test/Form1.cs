using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell_Phone_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // GetPhoneData 方法會接收一個 CellPhone 物件作為參數。
        // 此方法會將使用者輸入的資料指派給 CellPhone 物件的屬性。
        private void GetPhoneData(CellPhone phone)
        {
            decimal price;

            phone.Brand = brandTextBox.Text; // 從品牌文字框取得品牌名稱
            phone.Model = modelTextBox.Text; // 從型號文字框取得型號名稱

            // 嘗試將價格文字框中的文字轉換為 decimal 類型。
            if (decimal.TryParse(priceTextBox.Text, out price))
            {
                phone.Price = price; // 如果轉換成功，將價格指派給手機物件
            }
            else
            {
                // 如果轉換失敗，顯示錯誤訊息並清空價格文字框。
                MessageBox.Show("請輸入有效的價格。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                priceTextBox.Clear();
            }

        }

        private void createObjectButton_Click(object sender, EventArgs e)
        {
            // 建立一個新的 CellPhone 物件，並將其指派給 myPhone 變數。
            CellPhone myPhone = new CellPhone();

            // 呼叫 GetPhoneData 方法，將 myPhone 作為參數傳入。
            GetPhoneData(myPhone);

            // 將 myPhone 的資料顯示在標籤中。
            brandLabel.Text = myPhone.Brand;
            modelLabel.Text = myPhone.Model;
            priceLabel.Text = myPhone.Price.ToString("C2"); // 格式化為貨幣格式
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // 關閉表單，結束程式執行。
            this.Close();
        }
    }
}
