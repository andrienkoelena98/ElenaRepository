using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    public class Game
    {
        public Deck d;
        public Hand h, dealer;
        public bool state;

        public Game()
        {
            d = new Deck();
            h = new Hand();
            dealer = new Hand();
            state = false;
        }

        public void getCard()
        {
            h.Add(d.Get()); 
        }

        public void getCardDealer()
        {
            dealer.Add(d.Get());
        }

        public int Finish()
        {
            int sum = h.Check();
           
            return sum;
        }

        public int FinishDealer()
        {
            int sum = dealer.Check();
           
            return sum;
        }

        public void Turn()
        {
            getCard();
           
        }

       

        public void Start()
        {
            Turn();
            Turn();
            getCardDealer();
            getCardDealer();
        }
    }
}
