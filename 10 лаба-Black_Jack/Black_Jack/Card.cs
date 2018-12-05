using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    public class Card
    {
        public static string[] ColourName = { "Крести", "Пики", "Черви", "Буби" };
        public static string[] ValueName = { "Туз", "", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Валет", "Дама", "Король" };

        public string title;
        public int value;
        public int colour;
        public int points;

        public Card(int value, int colour)
        {
            this.value = value;
            this.colour = colour;
            title = ValueName[value] + " " + ColourName[colour];
            if (value >= 11) points = 10;
            else points = value;

        }

        public override string ToString()
        {
            return title;
        }
    }
}
