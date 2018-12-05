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
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

       
        public string user
        {
            get
            {
                return result;
            }
        }
        public static string result;

        static string connectionString = @"Data Source=LAPTOP-O8RC8S5Q;Initial Catalog=Black;Integrated Security=True";

        private static void AddUser(string name, string password)
        {
            // название процедуры
            string sqlExpression = "AddUsers1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@user",
                    Value = name
                };
                // добавляем параметр
                command.Parameters.Add(nameParam);
                // параметр для ввода 
                SqlParameter pasParam = new SqlParameter
                {
                    ParameterName = "@Password",
                    Value = password
                };
                command.Parameters.Add(pasParam);

                SqlParameter schetParam = new SqlParameter
                {
                    ParameterName = "@Schet",
                    Value = 1000
                };
                command.Parameters.Add(schetParam);


                 result = command.ExecuteScalar().ToString();
                
              


            }
        }



        private static void Prowuser(string name, string password)
        {
            string sqlExpression = "Prowuser1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@user",
                    Value = name
                };
                command.Parameters.Add(nameParam);

                SqlParameter pasParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = password
                };
                command.Parameters.Add(pasParam);

                // определяем первый выходной параметр
                SqlParameter id = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = SqlDbType.Int // тип параметра
                };
                // указываем, что параметр будет выходным
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);

                // определяем второй выходной параметр
                SqlParameter schet = new SqlParameter
                {
                    ParameterName = "@schet",
                    SqlDbType = SqlDbType.Float
                };
                schet.Direction = ParameterDirection.Output;
                command.Parameters.Add(schet);

                
                result=command.ExecuteScalar().ToString();
                
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                Prowuser(textBox1.Text, textBox2.Text);

                GameForm f = new GameForm();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Заполните поля!");
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text != "")
            {
                AddUser(textBox1.Text, textBox2.Text);
                GameForm f = new GameForm();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Заполните поля!");
            }
            
        }
    }
}
