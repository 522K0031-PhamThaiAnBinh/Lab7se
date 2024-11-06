using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        OleDbCommand cmd;
        OleDbDataAdapter da;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dt = new DataTable();
        String strConn = ConfigurationManager.ConnectionStrings["MyConnOleDb"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DataBind();
        }        private void btnSave_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            try
            {
                da.Update(dt);
                MessageBox.Show("Updated !");
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataBind()
        {
            dataGridView1.DataSource = null;
            dt.Clear();
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            String sSQL = "SELECT * FROM Employee";
            cmd = conn.CreateCommand();
            cmd.CommandText = sSQL;
            try
            {
                da = new OleDbDataAdapter(sSQL, conn);
                oleCommandBuilder = new OleDbCommandBuilder(da);
                da.Fill(dt);
                bindingSource = new BindingSource { DataSource = dt };
                dataGridView1.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }
    }
}
