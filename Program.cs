using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace YourNamespace // Replace with your actual namespace
{
    static class Program
    {
        public static string strConn = ""; // Declare global variable for connection string

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            strConn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();

            // Optionally, you can set the connection for other forms here
            Application.Run(new frmDeptEmp()); // Replace with your main form
            conn.Close();
        }
    }
}