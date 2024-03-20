using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace System_For_AnalyzingProject
{
    public partial class Registration : Form
    {
        public Registration() => InitializeComponent();

        /// <summary> Обработка события. Загрузка формы </summary>
        private void Registration_Load(object sender, EventArgs e)
        {
            Location = new Point
                            (Screen.PrimaryScreen.Bounds.Size.Width / 2 - Size.Width / 2, Screen.PrimaryScreen.Bounds.Size.Height / 2 - Size.Height / 2);
        }

        /// <summary> Допустимые наборы символов </summary>
        private const string
            ENG = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            RUS = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ",
            Numbers = "0123456789",
            Symbols = "!@#$%^&*()+=_-?:;~";

        /// <summary>  Показан или спрятан пароль </summary>
        private bool HideOrShow = true;

        /// <summary> Показать или спрятать ввод пароля </summary>
        private void Password_HideOrShow_Click(object sender, EventArgs e)
        {
            if (HideOrShow)
            {
                HideOrShow = false;
                TB_Password.PasswordChar = TB_Login.PasswordChar;
                TB_PasswordAgain.PasswordChar = TB_Login.PasswordChar;
                Password_HideOrShow.BackgroundImage = Properties.Resources.EyeOpen;
            }
            else
            {
                HideOrShow = true;
                TB_Password.PasswordChar = '✵';
                TB_PasswordAgain.PasswordChar = '✵';
                Password_HideOrShow.BackgroundImage = Properties.Resources.EyeClose;
            }
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Регистрация
        /// </summary>
        private void Button_Register_Click(object sender, EventArgs e)
        {
            try
            {
                bool[] T = { true, true, true };

                // Логин
                {
                    PanelError_Login_1.ForeColor = Color.Black;
                    PanelError_Login_2.ForeColor = Color.Black;
                    PanelError_Login_3.ForeColor = Color.Black;

                    // Длина от 8 до 32 символов
                    if (TB_Login.Text.Length < 8 | TB_Login.Text.Length > 32)
                    {
                        T[0] = false;
                        PanelError_Login_1.ForeColor = Color.Red;
                    }

                    // Допустимые символы: Английский алфавит, цифры и спецсимволы: !@#$%^&*()+=_-?:;~
                    if (ValidCharacters((ENG + ENG.ToLower() + Numbers + Symbols), TB_Login.Text))
                    {
                        T[0] = false;
                        PanelError_Login_2.ForeColor = Color.Red;
                    }

                    // Должен быть минимум один символ каждого вида: Нижнего и верхнего регистра, спецсимволы и цифры
                    if (OneCharacter(TB_Login.Text))
                    {
                        T[0] = false;
                        PanelError_Login_3.ForeColor = Color.Red;
                    }

                    // Проверка доступности Логина
                    {
                        using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                        {
                            Connect.Open();

                            using (SqlCommand Command = new SqlCommand("[System_For_AnalyzingProject].[dbo].[CheckingAvailabilityLogin]", Connect))
                            {
                                Command.CommandType = CommandType.StoredProcedure;
                                bool[] Bools = { true, false, true, true };
                                Encryption Cod = new Encryption(Bools, "Gridobi52", 2);

                                Command.Parameters.Add("@Login", SqlDbType.NVarChar, 32); Command.Parameters["@Login"].Value = Cod.Encoding(TB_Login.Text);

                                SqlDataReader Reader = Command.ExecuteReader(); if (Reader.HasRows)
                                {
                                    T[0] = false;
                                    PanelError_Login_4.ForeColor = Color.Red;
                                }
                            }

                            Connect.Close();
                        }
                    }

                    if (!T[0]) PanelError_Login.Visible = true;
                    else PanelError_Login.Visible = false;
                }

                // Пароль
                {
                    PanelError_Password_1.ForeColor = Color.Black;
                    PanelError_Password_2.ForeColor = Color.Black;
                    PanelError_Password_3.ForeColor = Color.Black;
                    PanelError_Password_4.ForeColor = Color.Black;

                    // Пароль
                    {
                        // Длина от 8 до 32 символов
                        if (TB_Password.Text.Length < 8 | TB_Password.Text.Length > 32)
                        {
                            T[1] = false;
                            PanelError_Password_1.ForeColor = Color.Red;
                        }

                        // Допустимые символы: Английский алфавит, цифры и спецсимволы: !@#$%^&*()+=_-?:;~
                        if (ValidCharacters(ENG + ENG.ToLower() + Numbers + Symbols, TB_Password.Text))
                        {
                            T[1] = false;
                            PanelError_Password_2.ForeColor = Color.Red;
                        }

                        // Должен быть минимум один символ каждого вида: Нижнего и верхнего регистра, спецсимволы и цифры
                        if (OneCharacter(TB_Password.Text))
                        {
                            T[1] = false;
                            PanelError_Password_3.ForeColor = Color.Red;
                        }
                    }

                    // Пароль повторно
                    {
                        // Длина от 8 до 32 символов
                        if (TB_PasswordAgain.Text.Length < 8 | TB_PasswordAgain.Text.Length > 32)
                        {
                            T[1] = false;
                            PanelError_Password_1.ForeColor = Color.Red;
                        }

                        // Допустимые символы: Английский алфавит, цифры и спецсимволы: !@#$%^&*()+=_-?:;~
                        if (ValidCharacters(ENG + ENG.ToLower() + Numbers + Symbols, TB_PasswordAgain.Text))
                        {
                            T[1] = false;
                            PanelError_Password_2.ForeColor = Color.Red;
                        }

                        // Должен быть минимум один символ каждого вида: Нижнего и верхнего регистра, спецсимволы и цифры
                        if (OneCharacter(TB_PasswordAgain.Text))
                        {
                            T[1] = false;
                            PanelError_Password_3.ForeColor = Color.Red;
                        }
                    }

                    // Пароли должны совпадать
                    if (TB_Password.Text != TB_PasswordAgain.Text)
                    {
                        T[1] = false;
                        PanelError_Password_4.ForeColor = Color.Red;
                    }

                    if (!T[1]) PanelError_Password.Visible = true;
                    else PanelError_Password.Visible = false;
                }

                // ФИО
                {
                    PanelError_FIO_1.ForeColor = Color.Black;
                    PanelError_FIO_2.ForeColor = Color.Black;
                    PanelError_FIO_3.ForeColor = Color.Black;

                    // Фамилия
                    {
                        // Длина от 2 до 30 символов
                        if (TB_Surname.Text.Length < 2 | TB_Surname.Text.Length > 30)
                        {
                            T[2] = false;
                            PanelError_FIO_1.ForeColor = Color.Red;
                        }

                        // Допустимые символы: Английский и Русский алфавит
                        if (ValidCharacters((ENG + ENG.ToLower() + RUS + RUS.ToLower()), TB_Surname.Text))
                        {
                            T[2] = false;
                            PanelError_FIO_2.ForeColor = Color.Red;
                        }
                    }

                    // Имя
                    {
                        // Длина от 2 до 30 символов
                        if (TB_Name.Text.Length < 2 | TB_Name.Text.Length > 30)
                        {
                            T[2] = false;
                            PanelError_FIO_1.ForeColor = Color.Red;
                        }

                        // Допустимые символы: Английский и Русский алфавит
                        if (ValidCharacters((ENG + ENG.ToLower() + RUS + RUS.ToLower()), TB_Name.Text))
                        {
                            T[2] = false;
                            PanelError_FIO_2.ForeColor = Color.Red;
                        }
                    }

                    // Отчество
                    {
                        // Длина до 30 символов
                        if (TB_MiddleName.Text.Length > 30)
                        {
                            T[2] = false;
                            PanelError_FIO_1.ForeColor = Color.Red;
                        }

                        // Допустимые символы: Английский и Русский алфавит
                        if (ValidCharacters((ENG + ENG.ToLower() + RUS + RUS.ToLower()), TB_MiddleName.Text))
                        {
                            T[2] = false;
                            PanelError_FIO_2.ForeColor = Color.Red;
                        }
                    }

                    if (TB_Surname.Text == "" | TB_Name.Text == "")
                    {
                        T[2] = false;
                        PanelError_FIO_3.ForeColor = Color.Red;
                    }

                    if (!T[2]) PanelError_FIO.Visible = true;
                    else PanelError_FIO.Visible = false;
                }

                if (T[0] == true & T[1] == true & T[2] == true)
                {
                    using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                    {
                        Connect.Open();

                        using (SqlCommand Command = new SqlCommand("[System_For_AnalyzingProject].[dbo].[Registration]", Connect))
                        {
                            Command.CommandType = CommandType.StoredProcedure;

                            bool[] Bools = { true, false, true, true };
                            Encryption Cod = new Encryption(Bools, "Gridobi52", 2);

                            bool[] Bools2 = { true, true, false, false };
                            Encryption Cod2 = new Encryption(Bools2, "Loridut", 1);

                            Command.Parameters.Add("@Login", SqlDbType.NVarChar, 32); Command.Parameters["@Login"].Value = Cod.Encoding(TB_Login.Text);
                            Command.Parameters.Add("@Password", SqlDbType.NVarChar, 32); Command.Parameters["@Password"].Value = Cod.Encoding(TB_Password.Text);
                            Command.Parameters.Add("@Surname", SqlDbType.NVarChar, 30); Command.Parameters["@Surname"].Value = Cod2.Encoding(TB_Surname.Text);
                            Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30); Command.Parameters["@FirstName"].Value = Cod2.Encoding(TB_Name.Text);
                            Command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 30); Command.Parameters["@MiddleName"].Value = Cod2.Encoding(TB_MiddleName.Text);

                            Command.ExecuteNonQuery();
                        }

                        Connect.Close();
                    }

                    throw new Exceptions.Information.Registration();
                }
                else throw new Exceptions.Errors.Registration();
            }
            catch (Exceptions.Information.Registration Ex)
            {
                Authorization.LoG.Info($"Button_Register_Click|{Ex.Data["Say"]}. {Ex.Message}");
                MessageBox.Show(Ex.Message, Ex.Data["Say"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exceptions.Errors.Registration Ex)
            {
                Authorization.LoG.Info($"Button_Register_Click|{Ex.Data["Kod"]}. {Ex.Message}");
                MessageBox.Show($"{Ex.Message}", Ex.Data["Kod"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                Authorization.LoG.Info($"Button_Register_Click|Неизвестная ошибка. {Ex.Message}");
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary> Проверка на допустимые символы </summary>
        /// <param name="StringChar">Допустимые символы</param>
        /// <param name="Test">Проверяемый текст</param>
        private bool ValidCharacters(string StringChar, string Text)
        {
            foreach (char LI in Text)
                if (StringChar.Contains(LI) == false)
                    return true;
            return false;
        }

        /// <summary> Проверка на допустимые символы </summary>
        /// <param name="Test">Проверяемый текст</param>
        private bool OneCharacter(string Text)
        {
            int[] Ti = { 0, 0, 0, 0 };
            foreach (char LI in Text)
            {
                if (ENG.Contains(LI) == true) Ti[0]++;
                if (ENG.ToLower().Contains(LI) == true) Ti[1]++;
                if (Numbers.Contains(LI) == true) Ti[2]++;
                if (Symbols.Contains(LI) == true) Ti[3]++;
            }

            if (Ti[0] == 0 | Ti[1] == 0 | Ti[2] == 0 | Ti[3] == 0)
                return true;
            return false;
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            Control Send = (Control)sender;

            switch (Send.Tag)
            {
                case "Login":
                    PanelError_Login.Visible = false;
                    break;

                case "Password":
                    PanelError_Password.Visible = false;
                    break;

                case "FIO":
                    PanelError_FIO.Visible = false;
                    break;
            }
        }
    }
}