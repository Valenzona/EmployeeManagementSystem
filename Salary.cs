using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Drawing.Printing;

namespace EmployeeManagementSystem
{
    public partial class Salary : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\This-PC\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30");

        public Salary()
        {
            InitializeComponent();

            displayEmployees();
            disableFields();

        }
        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }

            displayEmployees();
            disableFields();
        }

        public void disableFields()
        {
           salarty_employeeID.Enabled = false;
            salary_name.Enabled = false;
            salary_position.Enabled = false;
        }

        public void displayEmployees()
        {
            SalaryData ed = new SalaryData();
            List<SalaryData> listData = ed.salaryEmployeeListData();


            dataGridView1.DataSource = listData;

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Salary_Load(object sender, EventArgs e)
        {

        }

        private void salary_updateBtn_Click(object sender, EventArgs e)
        {
            if (salarty_employeeID.Text == ""
                || salary_name.Text == ""
                || salary_position.Text == ""
                || salary_salary.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to UPDATE Employee ID: "
                    + salarty_employeeID.Text.Trim() + "?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (check == DialogResult.Yes)
                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        try
                        {
                            connect.Open();
                            DateTime today = DateTime.Today;

                            string updateData = "UPDATE employees SET salary = @salary" +
                                ", update_date = @updateData WHERE employee_id = @employeeID";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@salary", salary_salary.Text.Trim());
                                cmd.Parameters.AddWithValue("@updateData", today);
                                cmd.Parameters.AddWithValue("@employeeID", salarty_employeeID.Text.Trim());

                                cmd.ExecuteNonQuery();

                                displayEmployees();

                                MessageBox.Show("Updated successfully!"
                                    , "Information Message", MessageBoxButtons.OK
                                    , MessageBoxIcon.Information);

                                clearFields();

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex, "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }


                    }
                }
                else
                {
                    MessageBox.Show("Cancelled", "Information Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        public void clearFields()
        {
            salarty_employeeID.Text = "";
            salary_name.Text = "";
            salary_position.Text = "";
            salary_salary.Text = "";
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                salarty_employeeID.Text = row.Cells[0].Value.ToString();
                salary_name.Text = row.Cells[1].Value.ToString();
                salary_position.Text = row.Cells[4].Value.ToString();
                salary_salary.Text = row.Cells[5].Value.ToString();
            }
        }

        private void salary_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void salary_printBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                PrintDocument printDocument = new PrintDocument();

                printDialog.Document = printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                // Define the content to be printed
                string content = $"Employee ID: {salarty_employeeID.Text}\n" +
                                 $"Name: {salary_name.Text}\n" +
                                 $"Position: {salary_position.Text}\n" +
                                 $"Salary: {salary_salary.Text}\n";

                // Create a font and brush for the text
                Font font = new Font("Arial", 12);
                SolidBrush brush = new SolidBrush(Color.Black);

                // Set the position for printing
                PointF point = new PointF(10, 10);

                // Draw the content on the page
                e.Graphics.DrawString(content, font, brush, point);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
