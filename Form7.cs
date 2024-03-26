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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace kur
{
    public partial class Form7 : Form
    {
        DB db = new DB();
        public Form7()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            db.open();


            var Name = textBox3.Text;
            var Fio = textBox1.Text;
            var Otl = textBox2.Text;
            var job = textBox4.Text;
            var NumPhon = textBox5.Text;
            var Hous = textBox6.Text;

           
                var addQ = $"insert into WorkPepl (Names, Fil, Otl, job, NumPhon, Hous) values ('{Name}', '{Fio}', '{Otl}', '{job}', '{NumPhon}', '{Hous}')";

                var com = new SqlCommand(addQ, db.GetConnection());
                com.ExecuteNonQuery();
                MessageBox.Show("Запись созданна");
            
          

            db.closed();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            this.Close();
            form4.Show();
            
        }
}   }

