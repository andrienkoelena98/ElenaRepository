using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    public class Hand: Cards
    {
        public int Count
        {
            get
            {
                return cardList.Count;
            }
        }

        
        

        public int Check()
        {
            int Sum = 0;
            foreach (Card c in cardList) 
            {
                Sum += c.points;//сумируем очки
            }
            return Sum;
        }

        public Hand() : base()
        {
        }
    }
}
