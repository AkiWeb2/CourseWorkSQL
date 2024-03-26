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

namespace kur
{
    public partial class Form9 : Form
    {
        DB db = new DB();
        public Form9()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.open();

            var Names = textBox3.Text;
            int Count;
            var Otl = textBox2.Text;
            var zavod = textBox4.Text;
            var car = textBox5.Text;
           


            if (int.TryParse(textBox1.Text, out Count))
            {
                var addQ = $"insert into Otgruska (Names, Counts, otdel, zavod, car) values ('{Names}',  '{Count}', '{Otl}', '{zavod}', '{car}')";

                var com = new SqlCommand(addQ, db.GetConnection());
                com.ExecuteNonQuery();
                MessageBox.Show("Запись созданна");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }

            db.closed();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.Close();
            form5.Show();
            
        }
    }
}
