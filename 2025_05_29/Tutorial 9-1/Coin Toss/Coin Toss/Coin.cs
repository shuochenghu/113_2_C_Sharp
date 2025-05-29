using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin_Toss
{
    /// <summary>
    /// Coin 類別用於表示一枚硬幣，未實作任何功能。
    /// </summary>
    internal class Coin
    {
        private string sideUp; // 硬幣正面朝上的一面
        Random rand = new Random();  // 隨機數生成器

        public Coin()
        {
            sideUp = "正面"; // 預設為正面
        }

        public void Toss()
        {            
            // 模擬擲硬幣，隨機選擇正面或反面
            if (rand.Next(2) == 0)
            {
                sideUp = "正面";
            }
            else
            {
                sideUp = "反面";
            }
        }

        public string GetSideUp() 
        {
            // 返回當前正面朝上的一面
            return sideUp;
        }

    }
}
