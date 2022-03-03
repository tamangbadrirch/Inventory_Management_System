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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }

       SqlConnection con = new SqlConnection(@"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True");

        void fillcategory()
        {
            string query = "select * from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                cmbProdcutCategory.ValueMember = "CatName";
                cmbProdcutCategory.DataSource = dt;
                cmbSearch.ValueMember = "CatName";
                cmbSearch.DataSource = dt;
                con.Close();
            }
            catch
            {

            }
        }

        void fillSearchcombo()   //To Search Products
        {
            string query = "select * from CategoryTbl where CatName= '"+cmbSearch.SelectedValue.ToString()+"' ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                cmbProdcutCategory.ValueMember = "CatName";
                cmbProdcutCategory.DataSource = dt;
                con.Close();
            }
            catch
            {

            }
        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillcategory();
            populate();
        }
        void populate()
        {
            try
            {
                con.Open();
                string MyQuery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }

        void filterByCategory() //To Filter Products CategoryWise
        {
            try
            {
                con.Open();
                string MyQuery = "select * from ProductTbl where ProdCat = '"+cmbSearch.SelectedValue.ToString()+"'";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
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
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values('" + txtProdId.Text + "','" + txtProdName.Text + "','" + txtProdQty.Text + "','" + txtProdPrice.Text + "','" + txtProdDesc.Text + "','" + cmbProdcutCategory.SelectedValue.ToString() + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Added!");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtProdId.Text == "")
            {
                MessageBox.Show("Enter the product Id: ");
            }
            else
            {
                con.Open();
                string myquery = "delete from ProductTbl where ProdId='" + txtProdId.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Deleted!");
                con.Close();
                populate();
            }
        }

        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = ProductsGV.CurrentCell.ColumnIndex;
            int selectedRow = e.RowIndex;
            txtProdId.Text = ProductsGV.Rows[selectedRow].Cells["ProdId"].Value.ToString();
            txtProdName.Text = ProductsGV.Rows[selectedRow].Cells["ProdName"].Value.ToString();
            txtProdQty.Text = ProductsGV.Rows[selectedRow].Cells["ProdQty"].Value.ToString();
            txtProdPrice.Text = ProductsGV.Rows[selectedRow].Cells["ProdPrice"].Value.ToString();
            txtProdDesc.Text = ProductsGV.Rows[selectedRow].Cells["ProdDesc"].Value.ToString();
            cmbProdcutCategory.SelectedValue = ProductsGV.Rows[selectedRow].Cells["ProdPrice"].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTbl set ProdName= '" + txtProdName.Text + "', ProdQty='" + txtProdQty.Text + "', ProdPrice='" + txtProdPrice.Text + "', ProdDesc='" + txtProdDesc.Text + "', ProdCat='" + cmbProdcutCategory.SelectedValue + "' where ProdId='" + txtProdId.Text + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Updated!");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           filterByCategory();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
