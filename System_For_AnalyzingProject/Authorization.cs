using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace System_For_AnalyzingProject
{
    public partial class Authorization : Form
    {
        public Authorization() => InitializeComponent();

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Авторизация
        /// </summary>
        private void Button_Enter_Click(object sender, EventArgs e)
        {
            try
            {
                bool T = false;
                bool[] Bools = { true, false, true, true };
                Encryption Cod = new Encryption(Bools, "Gridobi52", 2); // Gulding28@kur - Sp(9hv*3pr&O

                textBox1.Text = Cod.Encoding(TB_Login.Text);
                textBox2.Text = Cod.Encoding(TB_Password.Text);

                using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                {
                    Connect.Open();

                    using (SqlCommand Command = new SqlCommand("[System_For_AnalyzingProject].[dbo].[Authentication]", Connect))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.Add("@Login", SqlDbType.NVarChar, 32); Command.Parameters["@Login"].Value = Cod.Encoding(TB_Login.Text);
                        Command.Parameters.Add("@Password", SqlDbType.NVarChar, 32); Command.Parameters["@Password"].Value = Cod.Encoding(TB_Password.Text);

                        SqlDataReader Reader = Command.ExecuteReader(); T = Reader.HasRows;
                    }

                    Connect.Close();
                }

                label4.Text = Cod.Decoding(textBox1.Text);

                if (T) MessageBox.Show("GOOD");
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Authorization#Button_Enter_Click\n{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}