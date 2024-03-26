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
using System.Data.SqlClient;

namespace kur
{
    enum RowState2
    {
        Existed,
        New,
        Modifind,
        ModifindNew,
        Deleted,

    }


    public partial class Form4 : Form
    {
        DB db = new DB();
        public Form4()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void ReatRows(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3), RowState2.ModifindNew);
        }
        private void ReatRows2(DataGridView dgv1, IDataRecord record1)
        {
            dgv1.Rows.Add(record1.GetInt32(0), record1.GetString(1), record1.GetString(2), record1.GetString(3), record1.GetString(4), record1.GetString(5), record1.GetString(6),  RowState2.ModifindNew);
        }

        private void CreatColums()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("idUser", "IDРабочиго");
            dataGridView1.Columns.Add("prise", "Зарплата");
            dataGridView1.Columns.Add("Prem", "Премия");
            dataGridView1.Columns.Add("IsNew", String.Empty);

        }
        private void CreatColums2()
        {
            dataGridView2.Columns.Add("id", "id");
            dataGridView2.Columns.Add("Names", "Имя");
            dataGridView2.Columns.Add("Fil", "Фамилия");
            dataGridView2.Columns.Add("Otl", "Отчество");
            dataGridView2.Columns.Add("job", "Должность");
            dataGridView2.Columns.Add("NumPhon", "Номер телефона");
            dataGridView2.Columns.Add("Hous", "Место Жительство");
            dataGridView2.Columns.Add("IsNew", String.Empty);

        }

        private void ClearFild()
        {
            textBox3.Text = " ";
            textBox4.Text = " ";
            textBox5.Text = " ";
            textBox6.Text = " ";
            textBox7.Text = " ";
            textBox8.Text = " ";
            textBox9.Text = " ";
        }
        private void Cen2()
        {
            var select = dataGridView2.CurrentCell.RowIndex;
            var id = textBox7.Text;
            var Name = textBox3.Text;
            var Fio = textBox4.Text;
            var Otl = textBox5.Text;
            var job = textBox6.Text;
            var NumPhon = textBox8.Text;
            var Hous = textBox9.Text;
            


            if (dataGridView2.Rows[select].Cells[0].Value.ToString() != string.Empty)
            {
                
               dataGridView2.Rows[select].SetValues(id, Name, Fio, Otl, job, NumPhon,Hous);
               dataGridView2.Rows[select].Cells[7].Value = RowState2.Modifind;
                
              
            }
        }

        private void ClearFild2()
        {
            textBox11.Text = " ";
            textBox12.Text = " ";
            textBox13.Text = " ";
            textBox14.Text = " ";
          
        }

        private void Cen()
        {
            var select = dataGridView2.CurrentCell.RowIndex;
            var id = textBox11.Text;
            var IdUser = textBox12.Text;
            var Prem = textBox14.Text;
            int price;


            if (dataGridView1.Rows[select].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBox13.Text, out price))
                {
                    dataGridView1.Rows[select].SetValues(id, IdUser, Prem, price);
                    dataGridView1.Rows[select].Cells[4].Value = RowState2.Modifind;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        private void RefData(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queru = $"select * from Prise";

            SqlCommand com = new SqlCommand(queru, db.GetConnection());

            db.open();

            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ReatRows(dgv, read);
            }
            read.Close();
        }
        private void RefData2(DataGridView dgv1)
        {
            dgv1.Rows.Clear();

            string queru = $"select * from WorkPepl";

            SqlCommand com = new SqlCommand(queru, db.GetConnection());

            db.open();

            SqlDataReader read2 = com.ExecuteReader();
            while (read2.Read())
            {
                ReatRows2(dgv1, read2);
            }
            read2.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            RefData(dataGridView1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefData2(dataGridView2);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            CreatColums();
            CreatColums2();
            RefData(dataGridView1);
            RefData2(dataGridView2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();
        }

        private void serd(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string seard = $"select * from Prise where concat(id, idUser, prise, Prem) like '%" + textBox2.Text + "%' ";
            SqlCommand command = new SqlCommand(seard, db.GetConnection());
            db.open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReatRows(dgv, reader);
            }

            db.closed();
        }
        private void serd1(DataGridView dgv1)
        {
            dgv1.Rows.Clear();
            string seard = $"select * from WorkPepl where concat(id, Names, Fil, Otl, job, NumPhon, Hous) like '%" + textBox1.Text + "%' ";
            SqlCommand command = new SqlCommand(seard, db.GetConnection());
            db.open();

            SqlDataReader reader1 = command.ExecuteReader();
            while (reader1.Read())
            {
                ReatRows2(dgv1, reader1);
            }

            db.closed();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            serd(dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            serd1(dataGridView2);
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
            string sql = "delete from Prise where id =" + id;
            ExecSQL(sql);
           
            
            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[4].Value = RowState2.Deleted;
                return;
            }
        }
        private void Deleted2()
        {
            string Message = "Вы точно хотите удалить запись";
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[index].Visible = false;
            if (MessageBox.Show(Message, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            var id = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
            string sql = "delete from WorkPepl where id =" + id;
            ExecSQL(sql);
            
            
            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[7].Value = RowState2.Deleted;
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Deleted();
            ClearFild2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Deleted2();
            ClearFild();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cen2();
            ClearFild();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Cen();
            ClearFild2();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[select];
                textBox11.Text = row.Cells[0].Value.ToString();
                textBox12.Text = row.Cells[1].Value.ToString();
                textBox13.Text = row.Cells[2].Value.ToString();
                textBox14.Text = row.Cells[3].Value.ToString();
              
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[select];
                textBox7.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
                textBox8.Text = row.Cells[5].Value.ToString();
                textBox9.Text = row.Cells[6].Value.ToString();
                
            }
        }

        private void UpdateBD()
        {
            db.open();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var row = (RowState2)dataGridView1.Rows[index].Cells[4].Value;
                if (row == RowState2.Existed)
                {
                    continue;
                }

                if (row == RowState2.Modifind)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var idUser = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var prise = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var Prem = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    

                    var chenQ = $"update Prise set idUser = '{idUser}', prise = '{prise}',Prem = '{Prem}' where id = '{id}'";

                    var com = new SqlCommand(chenQ, db.GetConnection());
                    com.ExecuteNonQuery();
                }
            }
            db.closed();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UpdateBD();
        }

        private void UpdateBD2()
        {
            db.open();
            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {
                var row = (RowState2)dataGridView2.Rows[index].Cells[7].Value;
                if (row == RowState2.Existed)
                {
                    continue;
                }


                if (row == RowState2.Modifind)
                {
                    var id = dataGridView2.Rows[index].Cells[0].Value.ToString();
                    var Names = dataGridView2.Rows[index].Cells[1].Value.ToString();
                    var Fil = dataGridView2.Rows[index].Cells[2].Value.ToString();
                    var Otl = dataGridView2.Rows[index].Cells[3].Value.ToString();
                    var job = dataGridView2.Rows[index].Cells[4].Value.ToString();
                    var NumPhon = dataGridView2.Rows[index].Cells[5].Value.ToString();
                    var Hous = dataGridView2.Rows[index].Cells[6].Value.ToString();
                  

                    var chenQ = $"update Prise set Names = '{Names}', Fil = '{Fil}', Otl = '{Otl}', job = '{job}', NumPhon = '{NumPhon}', Hous = '{Hous}' where id = '{id}'";

                    var com = new SqlCommand(chenQ, db.GetConnection());
                    com.ExecuteNonQuery();
                }
            }
            db.closed();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateBD2();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
        }

       
    }
}
