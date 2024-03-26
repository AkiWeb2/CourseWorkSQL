using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace kur
{
    internal class DB
    {
        SqlConnection con = new SqlConnection(@"Data Source =DESKTOP-A9EGE1A; Initial Catalog = TestBD; integrated Security = True ");

        public void open()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void closed()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return con;
        }

    }
}
