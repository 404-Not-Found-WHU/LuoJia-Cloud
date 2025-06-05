using System;
using System.Data;
using System.Data.SqlClient;

namespace OnlineSupermarketTuto.Models
{
    public class Functions
    {
        private SqlConnection Con;
        private SqlCommand Cmd;
        private SqlDataAdapter sda;
        private string ConStr;

        public Functions()
        {

            ConStr = @"Server=IZL3WIW73ZUQIVZ;Database=SupermarketDb;User Id=sa;Password=Whucs051206;";


            Con = new SqlConnection(ConStr);

        
            Cmd = new SqlCommand();
            Cmd.Connection = Con;
        }

        public DataTable GetData(string Query)
        {
            DataTable dt = new DataTable();
            sda = new SqlDataAdapter(Query, ConStr);
            sda.Fill(dt);
            return dt;
        }

        public int SetData(string Query)
        {
            int cnt = 0;
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

                Cmd.CommandText = Query;
                cnt = Cmd.ExecuteNonQuery();
            }
            finally
            {
                Con.Close();
            }
            return cnt;
        }
    }
}
