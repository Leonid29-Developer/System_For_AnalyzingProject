using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using SpiceHorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Doc.Formatting;
using Spire.Doc.Fields.Shapes;
using Spire.Doc.Fields.Shapes.Charts;
using System.Reflection;
using System.Drawing.Imaging;
using System.Data.SqlClient;

namespace System_For_AnalyzingProject
{
    public partial class Main : Form
    {
        public Main() => InitializeComponent();

        private void Main_Load(object sender, EventArgs e)
            => Output();

        private void Output()
        {
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
            DocumentWord.LoadFromFile($@"{Application.StartupPath}\Resources\TT.docx");

            // Создание таблицы и добавление в документ
            {
                Section WordSection = DocumentWord.Sections[0];
                TextSelection WordSelection = DocumentWord.FindString("#DefectReport#", true, true);
                TextRange TRange = WordSelection.GetAsOneRange();
                Paragraph PG = TRange.OwnerParagraph;
                Body WordBody = PG.OwnerTextBody;
                int Index = WordBody.ChildObjects.IndexOf(PG);
                Table WordTable = WordSection.AddTable(true);
                WordTable.ResetCells(Cases.GetCount() + 1, 3);
                {
                    for (int I1 = 0; I1 < Cases.GetCount() + 1; I1++)
                    {
                        TableRow WordRow = WordTable.Rows[I1];
                        {
                            WordRow.Cells[0].SetCellWidth(48, CellWidthType.Point);
                            WordRow.Cells[1].SetCellWidth(304, CellWidthType.Point);
                            WordRow.Cells[2].SetCellWidth(112, CellWidthType.Point);
                        }

                        if (I1 == 0)
                        {
                            Paragraph Para = WordTable[I1, 0].AddParagraph();
                            Word(Para, SpiceHorizontalAlignment.Center, "№ кейса", true);

                            Para = WordTable[I1, 1].AddParagraph();
                            Word(Para, SpiceHorizontalAlignment.Center, "Описание", true);

                            Para = WordTable[I1, 2].AddParagraph();
                            Word(Para, SpiceHorizontalAlignment.Center, "Результат", true);
                        }
                        else for (int I2 = 0; I2 < 3; I2++)
                            {
                                Paragraph Para = WordTable[I1, I2].AddParagraph();

                                switch (I2)
                                {
                                    case 0:
                                        Word(Para, SpiceHorizontalAlignment.Center, I1.ToString(), false);
                                        break;

                                    case 1:
                                        Word(Para, SpiceHorizontalAlignment.Left, Cases[I1 - 1].ToStringWithNames(), false);
                                        break;

                                    case 2:
                                        if (Cases[I1 - 1].GetResult())
                                            Word(Para, SpiceHorizontalAlignment.Center, "Положительно", false);
                                        else
                                            Word(Para, SpiceHorizontalAlignment.Center, "Отрицательно", false);
                                        break;
                                }
                            }
                    }
                }
                WordBody.ChildObjects.Remove(PG); WordBody.ChildObjects.Insert(Index, WordTable);
            }

            // Создание круговой диаграммы и добавление в документ
            int Percentage = Cases.ImplementationPercentage();
            {
                TextSelection WordSelection = DocumentWord.FindString("#PieChart#", true, true);
                TextRange TRange = WordSelection.GetAsOneRange();
                Paragraph Para = TRange.OwnerParagraph;
                ShapeObject Shape = Para.AppendChart(ChartType.Pie, 420, 260);
                { Shape.FillColor = Color.White; }
                Chart WordChart = Shape.Chart;
                {
                    WordChart.Series.Clear();
                    WordChart.Series.Add("", new string[] { "Реализовано", "Не реализовано" }, new double[] { Percentage, 100 - Percentage });
                    WordChart.Legend.Position = LegendPosition.Right;
                    WordChart.Legend.Overlay = false;
                    WordChart.Title.Show = false;
                }
            }

            // Обработка данных
            string ProductReadiness, ProductReadiness2, TesterName = "";
            {
                if (Percentage > 85)
                { ProductReadiness = "готово"; ProductReadiness2 = "позволяет"; }
                else
                { ProductReadiness = "не готово"; ProductReadiness2 = "не позволяет"; }

                using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                {
                    Connect.Open();

                    using (SqlCommand Command = new SqlCommand("[System_For_AnalyzingProject].[dbo].[GetFIO]", Connect))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        bool[] Bools = { true, false, true, true };
                        Encryption Cod = new Encryption(Bools, "Gridobi52", 2);

                        bool[] Bools2 = { true, false, true, true };
                        Encryption Cod2 = new Encryption(Bools, "#Login#", 4);

                        bool[] Bools3 = { true, true, false, false };
                        Encryption Cod3 = new Encryption(Bools3, "Loridut", 1);

                        Command.Parameters.Add("@Login", SqlDbType.NVarChar, 32);
                        Command.Parameters["@Login"].Value = Cod.Encoding(Cod2.Decoding(Authorization.EncLogin));

                        SqlDataReader Reader = Command.ExecuteReader(); while (Reader.Read())
                        {
                            TesterName = $"{Cod3.Decoding((string)Reader.GetValue(0))} {Cod3.Decoding((string)Reader.GetValue(1))[0]}.";
                            if ((string)Reader.GetValue(2) != "") TesterName += $" {Cod3.Decoding((string)Reader.GetValue(2))[0]}.";
                        }

                    }

                    Connect.Close();
                }
            }

            // Сохранение старых строк — «заполнителей» и новых строк в словаре
            Dictionary<string, string> ReplaceDict = new Dictionary<string, string>
            {  
                { "#UsingCaseTesting#", Cases.UsingCaseTestingMethod() },
                { "#PieChart#", "" },
                { "#ProductReadiness#", ProductReadiness },
                { "#PercentageOfSales#", Percentage.ToString() },
                { "#PercentageNotImplementation#", (100-Percentage).ToString() },
                { "#ProductReadiness2#", ProductReadiness2 },
                { "#TesterName#", TesterName }
            };

            foreach (KeyValuePair<string, string> KVP in ReplaceDict) DocumentWord.Replace(KVP.Key, KVP.Value, true, true);

            // Сохранение файла документа
            DocumentWord.SaveToFile($@"{Application.StartupPath}\ReplacePlaceholders.docx", FileFormat.Docx); DocumentWord.Close();

            // Открытие файла документа
            System.Diagnostics.Process.Start($@"{Application.StartupPath}\ReplacePlaceholders.docx"); Close();
        }

        private void Word(Paragraph Para, SpiceHorizontalAlignment Alignment, string Text, bool TBold)
        {
            Para.Format.HorizontalAlignment = Alignment;
            TextRange TableRange = Para.AppendText(Text);
            TableRange.CharacterFormat.FontName = "Times New Roman";
            TableRange.CharacterFormat.FontSize = 14;
            TableRange.CharacterFormat.Bold = TBold;
        }
    }
}