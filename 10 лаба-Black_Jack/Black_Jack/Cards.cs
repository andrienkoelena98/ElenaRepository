using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    public class Cards
    {

        public Card this[int index]     
        {
            get { return cardList[index]; }

        }

        public List<Card> cardList;

        public Cards()
        {
            cardList = new List<Card>();
        }


        public void Add(Card c)
        {
            cardList.Add(c);
        }

    }
}
