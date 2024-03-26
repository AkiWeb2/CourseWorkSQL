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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace kur
{
    public partial class Form6 : Form
    {
        DB db = new DB();
        public Form6()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.open();

            var Id = textBox3.Text;
            var Prem = textBox2.Text;
            int price;

            if (int.TryParse(textBox1.Text, out price))
            {
                var addQ = $"insert into Prise (idUser, prise, Prem) values ('{Id}',  '{price}', '{Prem}')";

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
            Form4 form4 = new Form4();
            this.Close();
            form4.Show();
            
        }
    }
}
