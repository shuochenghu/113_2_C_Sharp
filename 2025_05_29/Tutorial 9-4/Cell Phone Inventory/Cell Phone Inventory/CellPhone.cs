using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell_Phone_Inventory
{
    // CellPhone 類別：用來表示一支手機的品牌、型號與售價
    class CellPhone
    {
        // 欄位
        private string _brand;   // 儲存手機品牌名稱
        private string _model;   // 儲存手機型號名稱
        private decimal _price;  // 儲存手機的零售價格（單位：新台幣）

        // 建構子：初始化手機物件，品牌與型號設為空字串，價格設為 0
        public CellPhone()
        {
            _brand = "";
            _model = "";
            _price = 0m;
        }

        // 品牌屬性：取得或設定手機品牌
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }

        // 型號屬性：取得或設定手機型號
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        // 價格屬性：取得或設定手機售價
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
    }
}
