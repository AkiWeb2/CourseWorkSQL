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

    enum RowState{
        Existed,
        New,
        Modifind,
        ModifindNew,
        Deleted,

    }

    public partial class Form2 : Form
    {
        DB db = new DB();
        int select;

        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }


        private void CreatColums()
        {
            dataGridView1.Columns.Add("id","id");
            dataGridView1.Columns.Add("type_off", "Тип товара");
            dataGridView1.Columns.Add("count_off", "Количество");
            dataGridView1.Columns.Add("postavka", "Поставщик");
            dataGridView1.Columns.Add("sclad", "Номер поддона");
            
            dataGridView1.Columns.Add("IsNew", String.Empty);

        }

        private void ClearFild()
        {
            textBox2.Text = " ";
            textBox3.Text = " ";
            textBox4.Text = " ";
            textBox5.Text = " ";
            textBox6.Text = " ";
        }

        private void ReatRows(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetString(4), RowState.ModifindNew );
        }

        private void RefData(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queru = $"select * from sclads";

            SqlCommand com = new SqlCommand(queru, db.GetConnection());

            db.open();

            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ReatRows(dgv, read);
            }
            read.Close();
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
            string sql = "delete from sclads where id =" + id;
            ExecSQL(sql);
            
            
            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
                return;
            }
        }

        private void UpdateBD()
        {
            db.open();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var row = (RowState)dataGridView1.Rows[index].Cells[5].Value;
                if (row == RowState.Existed)
                {
                    continue;
                }

              

                if (row == RowState.Modifind)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var type = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var count = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var pos = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var sclad = dataGridView1.Rows[index].Cells[4].Value.ToString();

                    var chenQ = $"update sclads set type_off = '{type}', count_off = '{count}',postavka = '{pos}', sclad = '{sclad}' where id = '{id}'";

                    var com = new SqlCommand (chenQ,db.GetConnection());
                    com.ExecuteNonQuery();
                }
            }
            db.closed();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Deleted();
            ClearFild();
        }

        private void Cen()
        {
            var select = dataGridView1.CurrentCell.RowIndex;
            var id = textBox2.Text;
            var typeOf = textBox3.Text;
            var count = textBox4.Text;
            var pos = textBox5.Text;
            var sclad = textBox6.Text;


            if (dataGridView1.Rows[select].Cells[0].Value.ToString() != string.Empty)
            {
                
                dataGridView1.Rows[select].SetValues(id, typeOf, count, pos, sclad);
                dataGridView1.Rows[select].Cells[5].Value = RowState.Modifind;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cen();
            ClearFild();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateBD();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Close();
            form3.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CreatColums();
            RefData(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            select = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[select];
                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefData(dataGridView1);
        }


        private void serd(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string seard = $"select * from sclads where concat(id, type_off, count_off, postavka, sclad) like '%" + textBox1.Text + "%' ";
            SqlCommand command = new SqlCommand(seard, db.GetConnection());
            db.open();

            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read()){
                ReatRows(dgv,reader);
            }

            db.closed();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            serd(dataGridView1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.ShowDialog();
            Application.Exit();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView1.Size.Width + 10, dataGridView1.Size.Height + 10);
            dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
