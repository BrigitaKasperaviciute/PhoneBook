using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        private readonly SqlConnection _connection;

        public Form1(string connectionString)
        {

            InitializeComponent();

            _connection = DatabaseConnection.Connect(connectionString);

            LoadData();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DatabaseConnection.Disconnect();
        }

        private void LoadData()
        {
            try
            {
                using (SqlCommand command = new SqlCommand("GetAllUsers", _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

                        dataGridView1.Columns["Full name"].FillWeight = 0.50F;
                        dataGridView1.Columns["Birth date"].FillWeight = 0.2F;
                        dataGridView1.Columns["Phone number"].FillWeight = 0.3F;

                        dataGridView1.Columns["Birth date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView1.Columns["Full name"].DefaultCellStyle.Padding = new Padding(40, 0, 0, 0);
                        dataGridView1.Columns["Phone number"].DefaultCellStyle.Padding = new Padding(83, 0, 0, 0);
                        dataGridView1.RowTemplate.Height = 40;

                        dataGridView1.Columns["Birth date"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView1.Columns["Phone number"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView1.Columns[0].HeaderCell.Style.Padding = new Padding(40, 0, 0, 0);
                        dataGridView1.ColumnHeadersHeight = 50;

                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(_connection);
            bool r = form2.ExecuteAdd();
            form2.Dispose(); form2 = null;
            if (r == false) { return; }

            LoadData();
        }
        private void EditButtonClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1) { return; }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string phoneNumber = selectedRow.Cells["Phone number"].Value.ToString();
            string fullName = selectedRow.Cells["Full name"].Value.ToString();
            string birthDate = selectedRow.Cells["Birth date"].Value.ToString();

            Form2 form2 = new Form2(_connection);
            bool r = false;// form2.ExecuteEdit(fullName, phoneNumber, birthDate);
            form2.Dispose(); form2 = null;
            if (r == false) { return; }

            LoadData();
        }
        private void DeleteButtonClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1) { return; }
            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) { return; }

            using (SqlCommand command = new SqlCommand("DeleteUser", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string phoneNumber = selectedRow.Cells["Phone number"].Value.ToString();
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.ExecuteNonQuery();
                // LoadData();

                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}