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
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True");
        void populate()
        {
            try
            {
                con.Open();
                string MyQuery = "select * from CategoryTbl";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CategoriesGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into CategoryTbl values('" + txtCatID.Text + "','" + txtCatName.Text + "' )", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Added!");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCatID.Text == "")
            {
                MessageBox.Show("Enter the category Id: ");
            }
            else
            {
                con.Open();
                string myquery = "delete from CategoryTbl where CatId='" + txtCatID.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Deleted!");
                con.Close();
                populate();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update CategoryTbl set CatName= '" + txtCatName.Text + "' where CatId='" + txtCatID.Text + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Updated!");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCatID.Text = CategoriesGV.SelectedRows[0].Cells[0].Value.ToString();
            txtCatName.Text = CategoriesGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            populate();
        }
    }
}
