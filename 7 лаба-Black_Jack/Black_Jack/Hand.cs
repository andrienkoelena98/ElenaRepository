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

        public int countOfAces()
        {
            int c = 0;
            for (int i = 0; i < cardList.Count; i++)
                if (cardList[i].value == 0) c += 1;
            return c;

        }

        public int Check()
        {
            int Sum = 0;
            foreach (Card c in cardList)//для каждого карты из списка 
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
