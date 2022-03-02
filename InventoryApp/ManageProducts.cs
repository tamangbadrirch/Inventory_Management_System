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

   
      
       private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values('" + txtProdId.Text + "','" + txtProdName.Text + "','" + txtProdQty.Text + "','" + txtProdPrice.Text + "',,'" + txtProdDesc.Text + "',,'" + cmbProdcutCategory.SelectedValue.ToString() + "',)", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Added!");
                con.Close();
                populate();
            }
            catch
            {

            }
        }
    }
}
