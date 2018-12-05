using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Black_Jack
{
    public partial class GameForm : Form
    {


        Game game;
        public void StartGame()
        {
            game = new Game();
            game.Start();
            
            result -= stavka;
            label4.Text = result.ToString();
            fillListBoxes();
        }

        List<PictureBox> playerList, dealerList;

        void fillListBoxes()
        {
            
            int width = 86,
                height = 120,
                space = 20;

           

            if (playerList != null)
                foreach (PictureBox aa in playerList)
                    Controls.Remove(aa);

            playerList = new List<PictureBox>();

            for (int i = 0; i < game.h.Count; i++)
            {
                PictureBox pb = new PictureBox();
                playerList.Add(pb);
                pb.Image = GetCardImage(game.h[i]);
                pb.Width = width;
                pb.Height = height;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Left = 300 + i * (width + space);
                pb.Top = 250;
                Controls.Add(pb);
            }

            label2.Text = "Игрок: " + game.Finish();

         

            if (dealerList != null)
                foreach (PictureBox pb in dealerList)
                    Controls.Remove(pb);

            dealerList = new List<PictureBox>();
            for (int i = 0; i < game.dealer.Count; i++) 
            {
                PictureBox pb = new PictureBox();
                playerList.Add(pb);
                pb.Image = GetCardImage(game.dealer[i]);
                pb.Width = width;
                pb.Height = height;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Left = 300 + i * (width + space);
                pb.Top = 100;
                Controls.Add(pb);
            }

            label1.Text = "Дилер: " + game.FinishDealer();
        }

        Image GetCardImage(Card crd)
        {
            string num = "", mast = "";

            switch (crd.value)
            {
                case 0: num = "A"; break;
                case 2: num = "2"; break;
                case 3: num = "3"; break;
                case 4: num = "4"; break;
                case 5: num = "5"; break;
                case 6: num = "6"; break;
                case 7: num = "7"; break;
                case 8: num = "8"; break;
                case 9: num = "9"; break;
                case 10: num = "10"; break;
                case 11: num = "J"; break;
                case 12: num = "Q"; break;
                case 13: num = "K"; break;
            }
            switch (crd.colour)
            {
                case 0: mast = "c"; break;
                case 1: mast = "s"; break;
                case 2: mast = "h"; break;
                case 3: mast = "d"; break;
            }

            return Image.FromFile($"images/{num}{mast}.jpg");
        }





        public GameForm()
        {
            InitializeComponent();
        }
        public static int id; // текущий игрок
        public static int stavka;
        private void GameForm_Load(object sender, EventArgs e)
        {

            Form1 f = new Form1();
           
            MessageBox.Show(f.user);
            id = Convert.ToInt32(f.user);
            Schet();
            label4.Text = result.ToString();
        }
        public static int result = 0; // счет игрока


        static string connectionString = @"Data Source=LAPTOP-O8RC8S5Q;Initial Catalog=Black;Integrated Security=True";
        private static void Schet()
        {
            string sqlExpression = "SchetReturn";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = id
                };
                command.Parameters.Add(nameParam);
                result = Convert.ToInt32(command.ExecuteScalar().ToString());

            }
        }

        private static void SchetUpdate()
        {
            string sqlExpression = "SchetUpdate";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = id
                };
                command.Parameters.Add(nameParam);
                SqlParameter schetParam = new SqlParameter
                {
                    ParameterName = "@Schet",
                    Value = result
                };
                command.Parameters.Add(schetParam);
               command.ExecuteScalar();

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            game.Turn();
            GetDealerCard();

            fillListBoxes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                stavka = Convert.ToInt32(textBox1.Text);
                StartGame();
            }
            else MessageBox.Show("Введите сумму ставки!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            while (game.FinishDealer() < 17)
                GetDealerCard();
            fillListBoxes();

            string result2 = "";
            int score = game.Finish(), scoreDealer = game.FinishDealer();
            if ((score > 21 && scoreDealer > 21) || (score == scoreDealer)) { result2 = "Ничья"; result += stavka; }
            else
            {
                if (score > 21 || (score < scoreDealer && scoreDealer<=21))
                {
                    result2 = "Проигрыш";
                }
                else
                {
                    result2 = "Выигрыш";
                    stavka *= 2;
                    result += stavka;
                    stavka = 0;
                }
                SchetUpdate();
                label4.Text = result.ToString();
            }

            MessageBox.Show($"Ваши очки: {game.Finish()}\nОчки дилера:{game.FinishDealer()}\nРезультат: {result2}");
           
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        
        }

        void GetDealerCard()
        {
            if (game.FinishDealer() < 17)
                game.getCardDealer();
        }



    }
}
