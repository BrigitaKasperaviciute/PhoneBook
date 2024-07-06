using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class Form2 : Form
    {
        private readonly SqlConnection _connection;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
     (
         int nLeftRect,
         int nTopRect,
         int nRightRect,
         int nBottomRect,
         int nWidthEllipse,
         int nHeightEllipse
     );
        public Form2(SqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private bool SaveData()
        {
            string phoneNumber = textBox2.Text;

            using (SqlCommand command = new SqlCommand(_user == null ? "AddUser" : "EditUser", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                SqlParameter[] parameters = new SqlParameter[]
                {
                        new SqlParameter("@FullName", textInfo.ToTitleCase(textBox1.Text.ToLower())),
                        new SqlParameter("@BirthDate", dateTimePicker1.Value.ToString("yyyy-MM-dd")),
                        new SqlParameter("@PhoneNumber",textBox2.Text),
                };
                if (_user != null)
                {
                    SqlParameter newParameter = new SqlParameter("@OriginalPhoneNumber", _user.PhoneNumber);
                    parameters = new SqlParameter[] { newParameter }.Concat(parameters).ToArray();
                }
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();

            }

            return true;
        }

        public bool ExecuteAdd(/* parametrus, ref string parametrus */)
        {
            /*
             * controls.text=parameters
             *
             **/

            bool r = ShowDialog() == DialogResult.OK;
            if (r)
            {
                /*
                 * ref parameters=controls.text
                 */
            }

            return r;
        }
        public bool ExecuteEdit(/* params */)
        {
            /*
             * controls.text=parameters
             *
             **/

            bool r = ShowDialog() == DialogResult.OK;
            if (r)
            {
                /*
                 * ref parameters=controls.text
                 */
            }

            return r;
        }

        private void OKClick(object sender, EventArgs e)
        {
        }

        private void CancelClick(object sender, EventArgs e)
        {
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private bool IsFullNameValid(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            string[] parts = fullName.Trim().Split(' ');
            if (parts.Length < 2)
                return false;

            foreach (string part in parts)
            {
                if (!part.All(char.IsLetter))
                    return false;
            }
            return true;
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            string pattern = @"^\+?[\d\s]+$";
            if (!Regex.IsMatch(phoneNumber, pattern))
                return false;

            return true;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (!IsFullNameValid(textBox1.Text))
                {
                    MessageBox.Show("Please enter your name and surname correctly! Only letters are allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                if (!IsPhoneNumberValid(textBox2.Text))
                {
                    MessageBox.Show("Please enter your phone number correctly! Phone numbers should contain digits and optionally begin with a plus sign (+).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                if (dateTimePicker1.Value > DateTime.Now)
                {
                    MessageBox.Show("Birth date cannot be in the future!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                bool r = SaveData();
                if (r == false)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}