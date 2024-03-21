using Spire.Doc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System_For_AnalyzingProject
{
    public partial class Main : Form
    {
        public Main() => InitializeComponent();

        private void Main_Load(object sender, EventArgs e)
            => Output();

        private void Output()
        {
            ////tableLayoutPanel1.Controls.Clear();

            // Создание списка кейсов
            ListСases Cases = new ListСases();
            _ = Cases + new TestingDefect(TestingTypes.ManualTesting, TestedElemnets.TextBox, "Введите логин", "Авторизация", "", true);
            _ = Cases + new TestingDefect(TestingTypes.IntegrationTesting, TestedElemnets.Form, "PurchaseReceipt", "", "", true);
            _ = Cases + new TestingDefect(TestingTypes.WhiteBoxTesting, TestedElemnets.CorrectnessDataDisplayOnForms, "PurchaseReceipt", "", "", true);
            _ = Cases + new TestingDefect(TestingTypes.ManualTesting, TestedElemnets.TextBox, "Введите пароль", "Авторизация", "", true);
            _ = Cases + new TestingDefect(TestingTypes.ManualTesting, TestedElemnets.Button, "Войти", "Авторизация", "", true);
            _ = Cases + new TestingDefect(TestingTypes.ManualTesting, TestedElemnets.System, "", "", "", true);
            _ = Cases + new TestingDefect(TestingTypes.ManualTesting, TestedElemnets.Label, "Логин", "Авторизация", "", true);
            _ = Cases + new TestingDefect(TestingTypes.IntegrationTesting, TestedElemnets.Form, "RemovalOperations", "", "", true);
            _ = Cases + new TestingDefect(TestingTypes.WhiteBoxTesting, TestedElemnets.CorrectnessDataDisplayOnForms, "RemovalOperations", "", "", true);
            _ = Cases + new TestingDefect(TestingTypes.ManualTesting, TestedElemnets.Other, "", "", "Проверка работоспособности печати чек-листа", false);
            _ = Cases + new TestingDefect(TestingTypes.ManualTesting, TestedElemnets.Label, "Пароль", "Авторизация", "", true);
            _ = Cases + new TestingDefect(TestingTypes.ModularTesting, TestedElemnets.System, "", "", "", true);

            // Создание объекта Document
            Document DocumentWord = new Document();

            // Загрузка шаблона
            DocumentWord.LoadFromFile($@"{Application.StartupPath}\Resources\TT.docx"); //document.LoadFromFile($@"{Application.StartupPath}\..\\TT.docx");

            //Обработка данных
            //string[] DateString = Statement_Date.Text.Split('.');
            //DateTime StatementDate = new DateTime(Convert.ToInt32(DateString[2]), Convert.ToInt32(DateString[1]), Convert.ToInt32(DateString[0]));
            //DateString = Statement_Date.Text.Split('.');
            //DateTime TestReportDate = new DateTime(Convert.ToInt32(DateString[2]), Convert.ToInt32(DateString[1]), Convert.ToInt32(DateString[0]));
            //string[] KeyMonth = new string[]
            //{"Январь" ,"Февраль","Март","Апрель" ,"Май","Июнь" ,"Июль" ,"Август","Сентябрь","Октябрь","Ноябрь","Декабрь" };
            //string Deviations2 = "соответствует"; if (Deviations.SelectedIndex == 1) Deviations2 = "не соответствует";



            // Сохранение старых строк — «заполнителей» и новых строк в словаре
            Dictionary<string, string> ReplaceDict = new Dictionary<string, string>
                    {
                { "#EXPERTORGANIZATIONNAME#", Cases.UsingCaseTestingMethod() }
                        //{ "#EXPERTORGANIZATIONNAME#", ExpertOrganizationName.Text.ToUpper() },
                        //{ "#ExpertOrganizationAddress#", ExpertOrganizationAddress.Text },
                        //{ "#ExpertOrganizationNumber#", ExpertOrganizationNumber.Text },
                        //{ "#ExpertOrganization_Head_Position#", ExpertOrganization_Head_Position.Text },
                        //{
                        //    "#ExpertOrganization_Head_FIO#",
                        //    $"{ExpertOrganization_Head_Surname.Text} {ExpertOrganization_Head_Name.Text[0]}. {ExpertOrganization_Head_MiddleName.Text[0]}."
                        //},
                        //{ "#Statement_Number#", Statement_Number.Text },
                        //{
                        //    "#Statement_Date#",
                        //    $"«{StatementDate.Day}» {KeyMonth[StatementDate.Month]} {StatementDate.Year} г."
                        //},
                        //{ "#StatementOrganization_Name#", StatementOrganization_Name.Text },
                        //{ "#StatementOrganization_Address#", StatementOrganization_Address.Text },
                        //{ "#ManufacturerOrganization_Name#", ManufacturerOrganization_Name.Text },
                        //{ "#ManufacturerOrganization_Address#", ManufacturerOrganization_Address.Text },
                        //{ "#Statement_Name#", Statement_Name.Text },
                        //{ "#TestReport_Number#", TestReport_Number.Text },
                        //{
                        //    "#TestReport_Date#",
                        //    $"«{TestReportDate.Day}» {KeyMonth[TestReportDate.Month]} {TestReportDate.Year} г."
                        //},
                        //{ "#TestReport_Organization_Name#", TestReport_Organization_Name.Text },
                        //{ "#TestReport_Organization_RegistrationNumber#", TestReport_Organization_RegistrationNumber.Text },
                        //{ "#TestResults_Number1#", TestResults_Number1.Text },
                        //{ "#TestResults_Number2#", TestResults_Number2.Text },
                        //{ "#TestResults_Number3#", TestResults_Number3.Text },
                        //{ "#TestResults_Number4#", TestResults_Number4.Text },
                        //{ "#TestResults_Number5#", TestResults_Number5.Text },
                        //{ "#TestResults_Number6#", TestResults_Number6.Text },
                        //{ "#TestResults_Number7#", TestResults_Number7.Text },
                        //{ "#TestResults_Number8#", TestResults_Number8.Text },
                        //{ "#TestResults_Number9#", TestResults_Number9.Text },
                        //{ "#TestResults_Number10#", TestResults_Number10.Text },
                        //{ "#Deviations#", Deviations.Text.ToLower() },
                        //{ "#Deviations2#", Deviations2.ToLower() },
                        //{
                        //    "#ExpertOrganization_Expert#",
                        //    $"{ExpertOrganization_Expert_Surname.Text} {ExpertOrganization_Expert_Name.Text[0]}. {ExpertOrganization_Expert_MiddleName.Text[0]}."
                        //},
                    };

            foreach (KeyValuePair<string, string> KVP in ReplaceDict) DocumentWord.Replace(KVP.Key, KVP.Value, true, true);

            // Сохранение файла документа
            DocumentWord.SaveToFile($@"{Application.StartupPath}\..\ReplacePlaceholders.docx", FileFormat.Docx); DocumentWord.Close();

            // Открытие файла документа
            System.Diagnostics.Process.Start($@"{Application.StartupPath}\..\ReplacePlaceholders.docx"); Close();
        }
    }
}
