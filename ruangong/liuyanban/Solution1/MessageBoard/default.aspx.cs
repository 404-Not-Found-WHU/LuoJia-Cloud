using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace MessageBoard
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) 
            {
                showData();
            }
        }

        public void showData()
        {
            string connStr = @"server = IZL3WIW73ZUQIVZ;uid=sa;pwd=Whucs051206;database=message;";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string sql = "select * from MessageBoard";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            rep.DataSource = dt;
            rep.DataBind();
            conn.Close();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string user = username.Text;
            string body = txtBody.Text;

            string connStr = @"server = IZL3WIW73ZUQIVZ;uid=sa;pwd=Whucs051206;database=message;";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string sql = "insert into MessageBoard(username,body,ip) values('"+user+"','"+body+"','"+Request.UserHostAddress+"')";
            SqlCommand cmd = new SqlCommand(sql,conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            litres.Text = "<span style = 'color:blue;font-size:16px;'>留言发送成功！</span>";

            username.Text = "";
            txtBody.Text = "";

            showData();
        }


        protected void btnDel_Click(object sender, EventArgs e)
        {
            Button btnDel = sender as Button;
            string id = btnDel.CommandArgument;

            string connStr = @"server = IZL3WIW73ZUQIVZ;uid=sa;pwd=Whucs051206;database=message;";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string sql = "delete MessageBoard where id = "+id;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            litres.Text = "<span style = 'color:red;font-size:16px;'>留言删除成功！</span>";

            showData();
        }
    }
}