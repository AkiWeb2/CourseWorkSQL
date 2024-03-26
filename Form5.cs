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
using System.IO;

namespace kur
{
    enum RowState3
    {
        Existed,
        New,
        Modifind,
        ModifindNew,
        Deleted,

    }
    public partial class Form5 : Form
    {
        DB db = new DB();
        public Form5()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }


        private void ReatRows(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetString(4), record.GetString(5), RowState2.ModifindNew);
        }
        private void ReatRows2(DataGridView dgv1, IDataRecord record1)
        {
            dgv1.Rows.Add(record1.GetInt32(0), record1.GetString(1), record1.GetInt32(2), record1.GetString(3), record1.GetString(4), record1.GetString(5), RowState2.ModifindNew);
        }

        private void CreatColums()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("Names", "Тип товара");
            dataGridView1.Columns.Add("Counts", "Кол-Во");
            dataGridView1.Columns.Add("otdel", "Отдел, склад");
            dataGridView1.Columns.Add("zavod", "Завоcдчик");
            dataGridView1.Columns.Add("car", "Номер машины");
            dataGridView1.Columns.Add("IsNew", String.Empty);

        }
        private void CreatColums2()
        {
            dataGridView2.Columns.Add("id", "id");
            dataGridView2.Columns.Add("Names", "Тип товара");
            dataGridView2.Columns.Add("Count", "Кол-во");
            dataGridView2.Columns.Add("otdel", "Склад, отдел");
            dataGridView2.Columns.Add("car", "Номер машины");
            dataGridView2.Columns.Add("post", "Покупатель");
            dataGridView2.Columns.Add("IsNew", String.Empty);

        }

        private void ClearFild()
        {
            textBox14.Text = " ";
            textBox13.Text = " ";
            textBox12.Text = " ";
            textBox11.Text = " ";
            textBox10.Text = " ";
            textBox3.Text = " ";
           
        }

        private void Cen()
        {
            var select = dataGridView1.CurrentCell.RowIndex;
            var id = textBox14.Text;
            var Name = textBox13.Text;
            var counts = textBox12.Text;
            var Otl = textBox11.Text;
            var zavod = textBox10.Text;
            var car = textBox3.Text;
            



            if (dataGridView1.Rows[select].Cells[0].Value.ToString() != string.Empty)
            {

                dataGridView1.Rows[select].SetValues(id, Name, counts, Otl, zavod, car);
                dataGridView1.Rows[select].Cells[6].Value = RowState3.Modifind;


            }
        }

        private void ClearFild2()
        {
            textBox7.Text = " ";
            textBox6.Text = " ";
            textBox5.Text = " ";
            textBox4.Text = " ";
            textBox8.Text = " ";
            textBox9.Text = " ";
        }

        private void Cen2()
        {
            var select = dataGridView1.CurrentCell.RowIndex;
            var id = textBox7.Text;
            var Name = textBox6.Text;
            int counts;
            var Otl = textBox4.Text;
            var car = textBox8.Text;
            var post = textBox9.Text;


            if (dataGridView1.Rows[select].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBox5.Text, out counts))
                {
                    dataGridView1.Rows[select].SetValues(id, Name, counts, Otl, car, post);
                    dataGridView1.Rows[select].Cells[6].Value = RowState3.Modifind;
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

            string queru = $"select * from Otgruska";

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

            string queru = $"select * from Otpravka";

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

        private void Form5_Load(object sender, EventArgs e)
        {
            CreatColums();
            CreatColums2();
            RefData(dataGridView1);
            RefData2(dataGridView2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

    


        private void serd(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string seard = $"select * from Otgruska where concat(id, Names, Counts, otdel, zavod, car) like '%" + textBox2.Text + "%' ";
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
            string seard = $"select * from Otpravka where concat(id, Names, Counts, otdel, car, post) like '%" + textBox1.Text + "%' ";
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
            string sql = "delete from Otgruska where id =" + id;
            ExecSQL(sql);
            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[6].Value = RowState3.Deleted;
                return;
            }
        }
        private void Deleted2()
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[index].Visible = false;
            string Message = "Вы точно хотите удалить запись";
            if (MessageBox.Show(Message, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            var id = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
            string sql = "delete from Otpravka where id =" + id;
            ExecSQL(sql);
            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[6].Value = RowState3.Deleted;
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Deleted();
            ClearFild();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Deleted2();
            ClearFild2();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Cen();
            ClearFild();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cen2();
            ClearFild2();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[select];
                textBox14.Text = row.Cells[0].Value.ToString();
                textBox13.Text = row.Cells[1].Value.ToString();
                textBox12.Text = row.Cells[2].Value.ToString();
                textBox11.Text = row.Cells[3].Value.ToString();
                textBox10.Text = row.Cells[4].Value.ToString();
                textBox3.Text = row.Cells[5].Value.ToString();

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[select];
                textBox7.Text = row.Cells[0].Value.ToString();
                textBox6.Text = row.Cells[1].Value.ToString();
                textBox5.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox8.Text = row.Cells[4].Value.ToString();
                textBox9.Text = row.Cells[5].Value.ToString();

            }
        }

        private void UpdateBD()
        {
            db.open();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var row = (RowState3)dataGridView1.Rows[index].Cells[6].Value;
                if (row == RowState3.Existed)
                {
                    continue;
                }
                if (row == RowState3.Modifind)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var Names = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var Counts = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var Otdel = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var Zavod = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var car = dataGridView1.Rows[index].Cells[5].Value.ToString();


                    var chenQ = $"update Otgruska set Names = '{Names}', Counts = '{Counts}',otdel = '{Otdel}', zavod = '{Zavod}', car = '{car}' where id = '{id}'";

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
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var row = (RowState3)dataGridView1.Rows[index].Cells[6].Value;
                if (row == RowState3.Existed)
                {
                    continue;
                }

                if (row == RowState3.Modifind)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var Names = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var Counts = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var Otdel = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var car = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var post = dataGridView1.Rows[index].Cells[5].Value.ToString();


                    var chenQ = $"update Otpravka set Names = '{Names}', Counts = '{Counts}',otdel = '{Otdel}',  car = '{car}', post = '{post}' where id = '{id}'";

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
            Form9 form9 = new Form9();
            this.Close();
            form9.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            this.Close();
            form10.Show();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView1.Size.Width+10, dataGridView2.Size.Height+10 );
            dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
            e.Graphics.DrawImage(bmp,0,0);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView2.Size.Width + 10, dataGridView2.Size.Height + 10);
            dataGridView2.DrawToBitmap(bmp, dataGridView2.Bounds);
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            printDocument2.Print();
        }
    }
}
