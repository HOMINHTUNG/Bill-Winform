using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;


namespace XuatBill
{
    class KetNoiCSDL
    {
        static string ConnectStr = @"Data Source=DESKTOP-A1BMAOT\HOMINHTUNG;Initial Catalog=SLEEPINTENT;Integrated Security=True";
        // @"Data Source=DESKTOP-A1BMAOT\HOMINHTUNG;AttachDbFilename=|DataDirectory|\SLEEPINTENT.mdf;Integrated Security=TrueConnect Timeout=30;User Instance=True";
        static SqlConnection Connect;

        static public DataTable LoadCSDL(string Sql)
        {
            DataTable Data = new DataTable();
            Connect = new SqlConnection(ConnectStr);
            SqlCommand Cmd = new SqlCommand(Sql, Connect);
            SqlDataAdapter DA = new SqlDataAdapter(Cmd);
            DA.Fill(Data);
            return Data;
        }

        static public int Change(string Sql)
        {
            Connect = new SqlConnection(ConnectStr);
            if (Connect.State == ConnectionState.Closed)
            {
                Connect.Open();
            }
            SqlCommand Cmd = new SqlCommand(Sql, Connect);
            int kq = Cmd.ExecuteNonQuery();
            Connect.Close();
            return kq;
        }

    }
}
