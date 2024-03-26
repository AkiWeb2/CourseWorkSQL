using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kur
{
    enum RowState4
    {
        Existed,
        New,
        Modifind,
        ModifindNew,
        Deleted,

    }

    public partial class Form11 : Form
    {
        DB db = new DB();
        public Form11()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void ReatRows(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RowState4.ModifindNew);
        }

        private void CreatColums()
        {
            dataGridView1.Columns.Add("idUser", "id");
            dataGridView1.Columns.Add("loginUser", "Login");
            dataGridView1.Columns.Add("passwordUser", "Password");
            dataGridView1.Columns.Add("IsNew", String.Empty);

        }

        private void ClearFild()
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
        }

        void ExecSQL(string sql)
        {
            db.open();
            var com = new SqlCommand(sql, db.GetConnection());
            com.CommandText = sql;
            com.ExecuteNonQuery();
            db.closed();
        }
        private void Deleted()
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;
            string Message = "Вы точно хотите удалить запись";

            if (MessageBox.Show(Message, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
            string sql = "delete from register where idUser =" + id;
            ExecSQL(sql);


            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[2].Value = RowState4.Deleted;
                return;
            }
        }
        private void Cen()
        {
            var select = dataGridView1.CurrentCell.RowIndex;
            var id = textBox3.Text;
            var Login = textBox1.Text;
            var Password = textBox2.Text;


            if (dataGridView1.Rows[select].Cells[0].Value.ToString() != string.Empty)
            {
                
                dataGridView1.Rows[select].SetValues(id, Login, Password);
                dataGridView1.Rows[select].Cells[3].Value = RowState4.Modifind;
               
            }
        }
        private void RefData(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queru = $"select * from register";

            SqlCommand com = new SqlCommand(queru, db.GetConnection());

            db.open();

            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ReatRows(dgv, read);
            }
            read.Close();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            CreatColums();
            RefData(dataGridView1);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[select];
                textBox3.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Deleted();
            ClearFild();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cen();
            ClearFild();
        }
        private void UpdateBD()
        {
            db.open();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var row = (RowState4)dataGridView1.Rows[index].Cells[3].Value;
                if (row == RowState4.Existed)
                {
                    continue;
                }

                if (row == RowState4.Modifind)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var Login = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var Password = dataGridView1.Rows[index].Cells[2].Value.ToString();
  
                    var chenQ = $"update register set loginUser = '{Login}', passwordUser = '{Password}' where idUser = '{id}'";

                    var com = new SqlCommand(chenQ, db.GetConnection());
                    com.ExecuteNonQuery();
                }
            }
            db.closed();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateBD();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            db.open();

            var Login = textBox1.Text;
            int Password;
           

            if (int.TryParse(textBox2.Text, out Password))
            {
                var addQ = $"insert into register (loginUser, passwordUser) values ('{Login}', '{Password}')";

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefData(dataGridView1);
        }
    }
}
