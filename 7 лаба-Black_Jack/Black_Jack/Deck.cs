using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    public class Deck : Cards
    {
        public int Count
        {
            get { return cardList.Count; }
        }

        public Card Get()//когда вызывается метод то выбрасивается рандомная число от 1 до 52
        {
            Random R = new Random();
            int NumOfCard = R.Next(Count - 1); //выдает рандомное целое число, максимум число карт
            Card c = cardList[NumOfCard];
            cardList.Remove(c);//удаляем карт из колоды
            return c;
        }

        public Deck()//при вызовы дек состовляется карты 
        {

            for (int i = 0; i < 4; i++)
            {
                Add(new Card(0, i));//заполняет карты 
                for (int j = 2; j < 14; j++)//колтчество карт
                    Add(new Card(j, i));

            }

        }
    }
}
