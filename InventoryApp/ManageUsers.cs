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
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True");


        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void populate()
        {
            try
            {
                con.Open();
                string MyQuery = "select * from userTbl";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UserGV.DataSource = ds.Tables[0];
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
                SqlCommand cmd = new SqlCommand("insert into UserTbl values('" + txtUsername.Text + "','" + txtFullname.Text + "','" + txtPassword.Text + "', '" + txtPhone.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Added!");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtPhone.Text == "")
            {
                MessageBox.Show("Enter user's phone number: ");
            }
            else
            {
                con.Open();
                string myquery = "delete from UserTbl where Upassword='" + txtPhone.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted!");
                con.Close();
                populate();
            }
        }

        private void UserGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*txtUsername.Text = UserGV.SelectedRows[0].Cells[0].Value.ToString();
            txtFullname.Text = UserGV.SelectedRows[0].Cells[1].Value.ToString();
            txtPassword.Text = UserGV.SelectedRows[0].Cells[2].Value.ToString();
            txtPhone.Text = UserGV.SelectedRows[0].Cells[3].Value.ToString();*/

            //new code refrenced by sir
            int index = UserGV.CurrentCell.ColumnIndex;
            
            int selectedRow = e.RowIndex;

                txtUsername.Text = UserGV.Rows[selectedRow].Cells["Uname"].Value.ToString();
                txtFullname.Text = UserGV.Rows[selectedRow].Cells["UfullName"].Value.ToString();
                txtPassword.Text = UserGV.Rows[selectedRow].Cells["Upassword"].Value.ToString();
                txtPhone.Text = UserGV.Rows[selectedRow].Cells["Uphone"].Value.ToString();
            //cmbSemester.Text = dgvList.Rows[selectedRow].Cells["Semester"].Value.ToString();

            /*string gender = dgvList.Rows[selectedRow].Cells["Sex"].Value.ToString();
                if (gender == "Male")
                    rdMale.Checked = true;
                else if (gender == "Female")
                    rdFemale.Checked = true;
                else
                    rdOthers.Checked = true;

                string lang1 = dgvList.Rows[selectedRow].Cells["Lang1"].Value.ToString();
                if (!string.IsNullOrEmpty(lang1))
                {
                    chkC.Checked = true;
                }
                else
                    chkC.Checked = false;

                string lang2 = dgvList.Rows[selectedRow].Cells["Lang2"].Value.ToString();
                if (!string.IsNullOrEmpty(lang2))
                {
                    chkAsp.Checked = true;
                }
                else
                    chkAsp.Checked = false;

                button1.Text = "Update";
                updateRow = selectedRow;*/
            }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update UserTbl set Uname= '"+txtUsername.Text+"', Ufullname='"+txtFullname.Text+"', Upassword='"+txtPassword.Text+"' where Uphone='"+txtPhone.Text+"' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated!");
                con.Close();
                populate();
            }
            catch
            {

            }
        }
    }
}
