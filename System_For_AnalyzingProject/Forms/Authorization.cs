using NLog;
using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace System_For_AnalyzingProject
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            LoG = LogManager.GetCurrentClassLogger(); InitializeComponent();
        }

        /// <summary> Логгер </summary>
        public static Logger LoG;

        /// <summary> Обработка события. Загрузка формы </summary>
        private void Authorization_Load(object sender, EventArgs e)
        {
            Location = new Point
                (Screen.PrimaryScreen.Bounds.Size.Width / 2 - Size.Width / 2, Screen.PrimaryScreen.Bounds.Size.Height / 2 - Size.Height / 2);
        }

        /// <summary>  Показан или спрятан пароль </summary>
        private bool HideOrShow = true;

        /// <summary>  Зашифрованный логин пользователя, находящегося в данной сессии </summary>
        public static string EncLogin { private set; get; }

        /// <summary> Показать или спрятать ввод пароля </summary>
        private void Password_HideOrShow_Click(object sender, EventArgs e)
        {
            if (HideOrShow)
            {
                HideOrShow = false;
                TB_Password.PasswordChar = TB_Login.PasswordChar;
                Password_HideOrShow.BackgroundImage = Properties.Resources.EyeOpen;
            }
            else
            {
                HideOrShow = true;
                TB_Password.PasswordChar = '✵';
                Password_HideOrShow.BackgroundImage = Properties.Resources.EyeClose;
            }
        }

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
                Encryption Cod = new Encryption(Bools, "Gridobi52", 2);

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

                if (T)
                {
                    bool[] Bools2 = { true, false, true, true };
                    Encryption Cod2 = new Encryption(Bools2, "#Login#", 4);
                    EncLogin = Cod2.Encoding(TB_Login.Text);

                    LoG.Info($"Button_Enter_Click|Успешная аутентификации");
                    Hide(); new Main().ShowDialog(); Show();
                }
                else throw new Exceptions.Errors.Authentication();
            }
            catch (Exceptions.Errors.Authentication Ex)
            {
                LoG.Info($"Button_Enter_Click|{Ex.Data["Kod"]}. {Ex.Message}");
                MessageBox.Show($"{Ex.Message}", Ex.Data["Kod"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                LoG.Info($"Button_Enter_Click|Неизвестная ошибка. {Ex.Message}");
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Регистрация
        /// </summary>
        private void Button_Register_Click(object sender, EventArgs e)
        { Hide(); new Registration().ShowDialog(); Show(); }

        /// <summary>
        /// Быстрая авторизация </br> 
        /// !!! Удалить при производственной эксплуатации !!!
        /// </summary>
        private void panel1_Click(object sender, EventArgs e)
        {
            TB_Login.Text = "GuldinGRg28@kuri"; TB_Password.Text = "Sp(9HGED*3pr&O";
            //bool[] Bools = { true, false, true, true };
            //Encryption Cod = new Encryption(Bools, "Gridobi52", 2);
            //TB_Password.Text = Cod.Decoding(TB_Login.Text);
        }
    }
}