    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Windows.Forms;

    namespace WindowsFormsApp1
    {
        public partial class frmDeptEmp : Form
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);

            public frmDeptEmp()
            {
                InitializeComponent();
            }

            private void frmDeptEmp_Load(object sender, EventArgs e)
            {
                LoadDepartments();
            }

            private void LoadDepartments()
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Department", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridViewDepartment.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No Data");
                }
                adapter.Dispose();
            }

            private void LoadData()
            {
                try
                {
                    int index = dataGridViewDepartment.SelectedCells[0].RowIndex;
                    if (index < 0 || index >= dataGridViewDepartment.RowCount)
                    {
                        MessageBox.Show("Please select a department first");
                        return;
                    }
                    DataGridViewRow row = dataGridViewDepartment.Rows[index];
                    int iDNo = int.Parse(row.Cells["DNumber"].Value.ToString());

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE DNo = @DNo", conn);
                    cmd.Parameters.AddWithValue("@DNo", iDNo);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewEmployee.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No Data");
                    }
                    adapter.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message + " - " + ex.Source);
                }
            }

            private void dataGridViewDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                LoadData();
            }

            private void button1_Click(object sender, EventArgs e)
            {
                // Show the frmEmployee form
                frmEmployee employeeForm = new frmEmployee();
                employeeForm.Show(); // Use ShowDialog() if you want it to be modal
            }

            private void dataGridViewDepartment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {
                SaveDepartmentChanges(e.RowIndex);
            }

            private void dataGridViewEmployee_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {
                SaveEmployeeChanges(e.RowIndex);
            }

            private void SaveDepartmentChanges(int rowIndex)
            {
                try
                {
                    if (rowIndex < 0) return;

                    var row = dataGridViewDepartment.Rows[rowIndex];
                    int dNumber = Convert.ToInt32(row.Cells["DNumber"].Value);
                    string dName = row.Cells["DName"].Value.ToString();
                    string mgsn = row.Cells["Mgsn"].Value.ToString();
                    DateTime mgrStartDate = Convert.ToDateTime(row.Cells["MgrStartDate"].Value);

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Department SET DName = @DName, Mgsn = @Mgsn, MgrStartDate = @MgrStartDate WHERE DNumber = @DNumber", conn);
                    cmd.Parameters.AddWithValue("@DName", dName);
                    cmd.Parameters.AddWithValue("@Mgsn", mgsn);
                    cmd.Parameters.AddWithValue("@MgrStartDate", mgrStartDate);
                    cmd.Parameters.AddWithValue("@DNumber", dNumber);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating department: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            private void SaveEmployeeChanges(int rowIndex)
            {
                try
                {
                    if (rowIndex < 0) return;

                    var row = dataGridViewEmployee.Rows[rowIndex];
                    string ssn = row.Cells["SSN"].Value.ToString();
                    string lName = row.Cells["LName"].Value.ToString();
                    DateTime bDate = Convert.ToDateTime(row.Cells["BDate"].Value);
                    string address = row.Cells["Address"].Value.ToString();
                    decimal salary = Convert.ToDecimal(row.Cells["Salary"].Value);

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Employee SET LName = @LName, BDate = @BDate, Address = @Address, Salary = @Salary WHERE SSN = @SSN", conn);
                    cmd.Parameters.AddWithValue("@LName", lName);
                    cmd.Parameters.AddWithValue("@BDate", bDate);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    cmd.Parameters.AddWithValue("@SSN", ssn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating employee: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            private void button1_Click_1(object sender, EventArgs e)
            {

            }
    }
    }