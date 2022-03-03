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

namespace InventoryApp
{
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True");
        void populate()
        {
            try
            {
                con.Open();
                string MyQuery = "select * from CustomerTbl";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into CustomerTbl values('" + txtCustID.Text + "','" + txtCustName.Text + "','" + txtCustPhone.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Added!");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCustID.Text == "")
            {
                MessageBox.Show("Enter the customer Id: ");
            }
            else
            {
                con.Open();
                string myquery = "delete from CustomerTbl where CustId='" + txtCustID.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Deleted!");
                con.Close();
                populate();
            }
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = CustomersGV.CurrentCell.ColumnIndex;
            int selectedRow = e.RowIndex;
            txtCustID.Text = CustomersGV.Rows[selectedRow].Cells["CustId"].Value.ToString();
            txtCustName.Text = CustomersGV.Rows[selectedRow].Cells["CustName"].Value.ToString();
            txtCustPhone.Text = CustomersGV.Rows[selectedRow].Cells["custPhone"].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update CustomerTbl set CustName= '" + txtCustName.Text + "', CustPhone='" + txtCustPhone.Text + "' where CustId='" + txtCustID.Text + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Updated!");
                con.Close();
                populate();
            }
            catch
            {

            }
        }
    }
}
