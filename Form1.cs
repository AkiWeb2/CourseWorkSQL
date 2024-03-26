using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace kur
{
    public partial class Form1 : Form
    {

        DB db = new DB();

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var logUser = LoginBox.Text;
            var pasUser = PasswordBox.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable Table = new DataTable();

            string query = $"select * from register where loginUser = '{logUser}' and passwordUser = '{pasUser}'";

            SqlCommand con = new SqlCommand(query, db.GetConnection());
            adapter.SelectCommand = con;
            adapter.Fill(Table);
            string[] mas = { "Admin", "Bugalteria", "Otpravka","register" };
           
            //for (int i =0; i <= mas.Length-1; i++)
            //{
            //    if (mas[i]==logUser)
            //    {
            //        string secondLine = File.ReadLines("Get.txt").Skip(i).First();
                    
            //    }

            //}
            if (Table.Rows.Count == 1 && mas[0] == logUser)
            {
                Form8 form8 = new Form8();
                this.Hide();
                form8.ShowDialog();
            }
            else if (Table.Rows.Count == 1 && mas[1] == logUser)
            {
                Form4 form4 = new Form4();
                this.Hide();
                form4.ShowDialog();
            }

            else if (Table.Rows.Count == 1 && mas[2] == logUser)
            {
                Form5 form5 = new Form5();
                this.Hide();
                form5.ShowDialog();
            }
            else if (Table.Rows.Count == 1 && mas[3] == logUser)
            {
                Form11 form11 = new Form11();
                this.Hide();
                form11.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }


        }

        private void LoginBox_TextChanged(object sender, EventArgs e)
        {
            
            pictureBox3.Visible = true;
            LoginBox.MaxLength = 50;


        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            PasswordBox.PasswordChar = '*';
            LoginBox.MaxLength = 50;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PasswordBox.UseSystemPasswordChar = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PasswordBox.UseSystemPasswordChar = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
