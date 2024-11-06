using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmEmployee : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Department", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    comboBoxDepartment.DataSource = dt;
                    comboBoxDepartment.DisplayMember = "DName";
                    comboBoxDepartment.ValueMember = "DNumber";
                    comboBoxDepartment.SelectedIndexChanged += comboBoxDepartment_SelectedIndexChanged;
                    LoadData(); // Load initial data for the first department
                }
                else
                {
                    MessageBox.Show("No Departments Found");
                }
                adapter.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading departments: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void LoadData()
        {
            if (comboBoxDepartment.SelectedValue == null) return;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE DNo = @DNo", conn);
                cmd.Parameters.AddWithValue("@DNo", comboBoxDepartment.SelectedValue);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridViewEmployee.DataSource = dt;
                }
                else
                {
                    dataGridViewEmployee.DataSource = null; // Clear previous data
                    MessageBox.Show("No Employees Found");
                }
                adapter.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}