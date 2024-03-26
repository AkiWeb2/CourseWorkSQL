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
    
    public partial class Form3 : Form
    {
        DB db = new DB();
        public Form3()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            db.open();

            var typeOf = textBox1.Text;
            var pos =  textBox2.Text;
            var count  = textBox3.Text;
            var sclad = textBox4.Text;

           
            var addQ = $"insert into sclads (type_off, count_off, postavka, sclad) values ('{typeOf}', '{count}', '{pos}', '{sclad}')";

            var com = new SqlCommand(addQ, db.GetConnection());
            com.ExecuteNonQuery();
            MessageBox.Show("Запись созданна");
           
           
          

            db.closed();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Close();
            
            form2.Show();
            //Application.Exit();
        }

      
    }
}
