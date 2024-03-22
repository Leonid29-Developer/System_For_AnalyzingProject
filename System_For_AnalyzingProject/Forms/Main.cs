using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Fields.Shapes.Charts;
using SpiceHorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BorderStyle = System.Windows.Forms.BorderStyle;
using TextBox = System.Windows.Forms.TextBox;

namespace System_For_AnalyzingProject
{
    public partial class Main : Form
    {
        public Main() => InitializeComponent();

        private void Main_Load(object sender, EventArgs e)
            => Creating_AnAddPanel();

        /// <summary> Cписок кейсов </summary>
        private ListСases Cases = new ListСases();

        /// <summary> Список элементов управления для быстрого обращения к ним </summary>
        /// TestingTypes_ComboBox     > Выбор типа тестирования           > 0
        /// TestedElemnets_ComboBox   > Выбор тестируемого элемента       > 1
        /// NameTestedElemnet_TextBox > Ввод имени тестируемого элемента  > 2-3
        /// FormName_TextBox          > Ввод имени формы                  > 4-5
        /// Other_TextBox             > Текст иного тестируемого элемента > 6-7
        /// Result_Button             > Выбор Результата тестирования     > 8
        private List<Control> List_Contorls = new List<Control>();

        /// <summary> Результат тестирования </summary>
        private bool TempTestResult { set; get; } = false;

        /// <summary> Вывод панелей списка кейсов </summary>
        private void Out_Cases()
        {
            AutoScroll = false;
            TableInterface.Controls.Clear();
            List_Contorls.Clear(); TempTestResult = false;
            for (int I1 = 0; I1 < Cases.GetCount(); I1++)
                Out_PanelCase(Cases[I1], I1);
            Creating_AnAddPanel();
            AutoScroll = true;
        }

        /// <summary> Вывод панели кейса </summary>
        private void Out_PanelCase(TestingDefect Case, int Index)
        {
            try
            {
                Panel AddPanel = new Panel
                {
                    Size = new Size(TableInterface.Width - 12, 160),
                    BorderStyle = BorderStyle.FixedSingle,
                };
                {
                    // Вывод данных кейса
                    Label Testing_Label = new Label
                    {
                        AutoSize = false,
                        Font = new Font("Times New Roman", 14),
                        Text = Case.ToStringWithNames(),
                        Size = new Size(AddPanel.Width - 100, AddPanel.Height),
                        Location = new Point(0, 0),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    AddPanel.Controls.Add(Testing_Label);

                    Panel DopPanel = new Panel
                    {
                        Size = new Size(100, AddPanel.Height),
                        Location = new Point(AddPanel.Width - 100, -1),
                        BorderStyle = BorderStyle.FixedSingle,
                    };
                    {
                        // Кнопка сохранения
                        {
                            PictureBox Delete_Button = new PictureBox()
                            {
                                Tag = $"{Index}",
                                BackgroundImage = Properties.Resources.Trashcan,
                                Size = new Size(44, 44),
                                Location = new Point(28, 20),
                                BorderStyle = BorderStyle.FixedSingle,
                                BackgroundImageLayout = ImageLayout.Stretch
                            };
                            {
                                Delete_Button.Click += Button_Delete_Click;
                            }
                            DopPanel.Controls.Add(Delete_Button);

                            Label Save_Label = new Label
                            {
                                AutoSize = false,
                                Font = new Font("Times New Roman", 8),
                                Text = $"Удалить",
                                Size = new Size(60, 20),
                                Location = new Point(20, 65),
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            DopPanel.Controls.Add(Save_Label);
                        }

                        // Результат тестирования
                        {
                            Label Result_Label = new Label
                            {
                                AutoSize = false,
                                Font = new Font("Times New Roman", 12),
                                Text = $"Результат",
                                Size = new Size(DopPanel.Width, 22),
                                Location = new Point(-1, DopPanel.Height - 72),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BorderStyle = BorderStyle.FixedSingle,
                            };
                            DopPanel.Controls.Add(Result_Label);

                            Label Result_Button = new Label
                            {
                                AutoSize = false,
                                Font = new Font("Times New Roman", 9, FontStyle.Bold),
                                Size = new Size(DopPanel.Width, 50),
                                Location = new Point(-1, DopPanel.Height - 51),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BorderStyle = BorderStyle.FixedSingle,
                            };
                            {
                                if (Case.GetResult()) Result_Button.Text = "Положительно";
                                else Result_Button.Text = "Отрицательно";
                            }
                            DopPanel.Controls.Add(Result_Button);
                            Result_Button.BringToFront();
                        }
                    }
                    AddPanel.Controls.Add(DopPanel);
                }
                TableInterface.Controls.Add(AddPanel);
            }
            catch (Exception Ex)
            {
                Authorization.LoG.Info($"Out_PanelCase|Неизвестная ошибка. {Ex.Message}");
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary> Создание панели добавления </summary>
        private void Creating_AnAddPanel()
        {
            try
            {
                Panel AddPanel = new Panel
                {
                    Size = new Size(TableInterface.Width - 12, 200),
                    BorderStyle = BorderStyle.FixedSingle,
                };
                {
                    // Выбор типа тестирования
                    {
                        Label TestingTypes_Label = new Label
                        {
                            AutoSize = false,
                            Font = new Font("Times New Roman", 12),
                            Text = $"Выберите тип тестирования",
                            Size = new Size(240, 22),
                            Location = new Point(30, 15),
                            TextAlign = ContentAlignment.BottomCenter
                        };
                        AddPanel.Controls.Add(TestingTypes_Label);

                        ComboBox TestingTypes_ComboBox = new ComboBox()
                        {
                            Name = $"CB_TestingTypes",
                            Font = new Font("Times New Roman", 12),
                            Size = TestingTypes_Label.Size,
                            Location = new Point(TestingTypes_Label.Location.X, 25 + TestingTypes_Label.Height)
                        };
                        {
                            List_Contorls.Add(TestingTypes_ComboBox);
                            TestingTypes_ComboBox.Items.Add("Ручное тестирование");
                            TestingTypes_ComboBox.Items.Add("Интеграционное тестирование");
                            TestingTypes_ComboBox.Items.Add("Тестирование белым ящиком");
                            TestingTypes_ComboBox.Items.Add("Нисходящее тестирование");
                            TestingTypes_ComboBox.Items.Add("Модульное тестирование");
                            TestingTypes_ComboBox.Items.Add("Приемочное тестирование");
                            TestingTypes_ComboBox.Items.Add("Нагрузочное тестирование");
                            TestingTypes_ComboBox.SelectedIndex = 0;
                        }
                        AddPanel.Controls.Add(TestingTypes_ComboBox);
                    }

                    // Выбор тестируемого элемента
                    {
                        Label TestedElemnets_Label = new Label
                        {
                            AutoSize = false,
                            Font = new Font("Times New Roman", 12),
                            Text = $"Выберите тестируемый элемент программы",
                            Size = new Size(340, 22),
                            Location = new Point(300, 15),
                            TextAlign = ContentAlignment.BottomCenter
                        };
                        AddPanel.Controls.Add(TestedElemnets_Label);

                        ComboBox TestedElemnets_ComboBox = new ComboBox()
                        {
                            Name = $"CB_TestedElemnets",
                            Font = new Font("Times New Roman", 12),
                            Size = TestedElemnets_Label.Size,
                            Location = new Point(TestedElemnets_Label.Location.X, 25 + TestedElemnets_Label.Height)
                        };
                        {
                            List_Contorls.Add(TestedElemnets_ComboBox);
                            TestedElemnets_ComboBox.Items.Add("Button");
                            TestedElemnets_ComboBox.Items.Add("PiсtureBox");
                            TestedElemnets_ComboBox.Items.Add("Label");
                            TestedElemnets_ComboBox.Items.Add("LinkLabel");
                            TestedElemnets_ComboBox.Items.Add("TextBox");
                            TestedElemnets_ComboBox.Items.Add("DataTable");
                            TestedElemnets_ComboBox.Items.Add("TableLayoutPanel");
                            TestedElemnets_ComboBox.Items.Add("Panel");
                            TestedElemnets_ComboBox.Items.Add("Форма");
                            TestedElemnets_ComboBox.Items.Add("Корректность отображения данных на форме");
                            TestedElemnets_ComboBox.Items.Add("Подключение Базы данных");
                            TestedElemnets_ComboBox.Items.Add("Таблица Базы данных");
                            TestedElemnets_ComboBox.Items.Add("Система");
                            TestedElemnets_ComboBox.Items.Add("Другое, с указанием формы");
                            TestedElemnets_ComboBox.Items.Add("Другое, без указания формы");
                            TestedElemnets_ComboBox.SelectedIndex = 0;
                            TestedElemnets_ComboBox.SelectedIndexChanged += new EventHandler(ComboBox_TestedElemnets_SelectedIndexChanged);
                        }
                        AddPanel.Controls.Add(TestedElemnets_ComboBox);
                    }

                    // Ввод имени тестируемого элемента 
                    {
                        Label NameTestedElemnet_Label = new Label
                        {
                            AutoSize = false,
                            Font = new Font("Times New Roman", 12),
                            Text = $"Ведение имя тестируемого элемента",
                            Size = new Size(260, 22),
                            Location = new Point(20, 122),
                            TextAlign = ContentAlignment.BottomCenter
                        };
                        AddPanel.Controls.Add(NameTestedElemnet_Label);

                        TextBox NameTestedElemnet_TextBox = new TextBox
                        {
                            Name = $"TB_NameTestedElemnet",
                            Font = new Font("Times New Roman", 12),
                            Size = NameTestedElemnet_Label.Size,
                            Location = new Point(NameTestedElemnet_Label.Location.X, 128 + NameTestedElemnet_Label.Height)
                        };
                        {
                            List_Contorls.Add(NameTestedElemnet_Label);
                            List_Contorls.Add(NameTestedElemnet_TextBox);
                        }
                        AddPanel.Controls.Add(NameTestedElemnet_TextBox);
                    }

                    // Ввод имени формы, на которой находится тестируемый элемент
                    {
                        Label FormName_Label = new Label
                        {
                            AutoSize = false,
                            Font = new Font("Times New Roman", 12),
                            Text = $"Ведение имя формы, на которой находится тестируемый элемент",
                            Size = new Size(320, 44),
                            Location = new Point(310, 100),
                            TextAlign = ContentAlignment.BottomCenter
                        };
                        AddPanel.Controls.Add(FormName_Label);

                        TextBox FormName_TextBox = new TextBox
                        {
                            Name = $"TB_NameTestedElemnet",
                            Font = new Font("Times New Roman", 12),
                            Size = FormName_Label.Size,
                            Location = new Point(FormName_Label.Location.X, 106 + FormName_Label.Height)
                        };
                        {
                            List_Contorls.Add(FormName_Label);
                            List_Contorls.Add(FormName_TextBox);
                        }
                        AddPanel.Controls.Add(FormName_TextBox);
                    }

                    // Текст иного тестируемого элемента
                    {
                        Label Other_Label = new Label
                        {
                            Visible = false,
                            AutoSize = false,
                            Font = new Font("Times New Roman", 12),
                            Text = $"Введите, что тестируется",
                            Size = new Size(320, 22),
                            Location = new Point(200, 122),
                            TextAlign = ContentAlignment.BottomCenter
                        };
                        AddPanel.Controls.Add(Other_Label);

                        TextBox Other_TextBox = new TextBox
                        {
                            Visible = false,
                            Name = $"TB_NameTestedElemnet",
                            Font = new Font("Times New Roman", 12),
                            Size = Other_Label.Size,
                            Location = new Point(Other_Label.Location.X, 128 + Other_Label.Height)
                        };
                        {
                            List_Contorls.Add(Other_Label);
                            List_Contorls.Add(Other_TextBox);
                        }
                        AddPanel.Controls.Add(Other_TextBox);
                    }

                    Panel DopPanel = new Panel
                    {
                        Size = new Size(100, AddPanel.Height),
                        Location = new Point(AddPanel.Width - 100, -1),
                        BorderStyle = BorderStyle.FixedSingle,
                    };
                    {
                        // Кнопка сохранения
                        {
                            PictureBox Save_Button = new PictureBox()
                            {
                                BackgroundImage = Properties.Resources.Save,
                                Size = new Size(44, 44),
                                Location = new Point(28, 20),
                                BorderStyle = BorderStyle.FixedSingle,
                                BackgroundImageLayout = ImageLayout.Stretch
                            };
                            {
                                Save_Button.Click += Button_Save_Click;
                            }
                            DopPanel.Controls.Add(Save_Button);

                            Label Save_Label = new Label
                            {
                                AutoSize = false,
                                Font = new Font("Times New Roman", 8),
                                Text = $"Сохранить",
                                Size = new Size(60, 20),
                                Location = new Point(20, 65),
                                TextAlign = ContentAlignment.MiddleCenter
                            };

                            DopPanel.Controls.Add(Save_Label);
                        }

                        // Выбор Результата тестирования
                        {
                            Label Result_Label = new Label
                            {
                                AutoSize = false,
                                Font = new Font("Times New Roman", 12),
                                Text = $"Выберите результат",
                                Size = new Size(DopPanel.Width, 44),
                                Location = new Point(-1, DopPanel.Height - 104),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BorderStyle = BorderStyle.FixedSingle,
                            };
                            DopPanel.Controls.Add(Result_Label);

                            Label Result_Иackground = new Label
                            {
                                AutoSize = false,
                                BackColor = Color.Gray,
                                Size = new Size(DopPanel.Width, 60),
                                Location = new Point(-1, DopPanel.Height - 60),
                                BorderStyle = BorderStyle.FixedSingle,
                            };
                            DopPanel.Controls.Add(Result_Иackground);

                            Label Result_Button = new Label
                            {
                                AutoSize = false,
                                Font = new Font("Times New Roman", 9, FontStyle.Bold),
                                Text = "Отрицательно",
                                Size = new Size(DopPanel.Width, 60),
                                Location = new Point(-1, DopPanel.Height - 61),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BorderStyle = BorderStyle.FixedSingle,
                            };
                            {
                                Result_Button.Click += Button_Result_Click;
                                List_Contorls.Add(Result_Button);
                            }
                            DopPanel.Controls.Add(Result_Button);
                            Result_Button.BringToFront();
                        }
                    }
                    AddPanel.Controls.Add(DopPanel);
                }
                TableInterface.Controls.Add(AddPanel);
            }
            catch (Exception Ex)
            {
                Authorization.LoG.Info($"Creating_AnAddPanel|Неизвестная ошибка. {Ex.Message}");
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Обработка события «ComboBox» </br> 
        ///  Изменение выбора тестируемого элемента
        /// </summary>
        private void ComboBox_TestedElemnets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox SelectedComboBox = (ComboBox)sender;
            int Index = SelectedComboBox.SelectedIndex;

            if (0 >= Index & Index <= 7)
            {
                List_Contorls[2].Visible = true; List_Contorls[2].Location = new Point(20, 122);
                List_Contorls[3].Visible = true; List_Contorls[3].Location = new Point(20, 150);
                List_Contorls[4].Visible = true; List_Contorls[5].Visible = true;
                List_Contorls[6].Visible = false; List_Contorls[7].Visible = false;
            }

            if (Index == 8 | Index == 9 | Index == 11)
            {
                List_Contorls[2].Visible = true; List_Contorls[2].Location = new Point(220, 122);
                List_Contorls[3].Visible = true; List_Contorls[3].Location = new Point(220, 150);
                List_Contorls[4].Visible = false; List_Contorls[5].Visible = false;
                List_Contorls[6].Visible = false; List_Contorls[7].Visible = false;
            }

            if (Index == 10 | Index == 12)
            {
                List_Contorls[2].Visible = false; List_Contorls[3].Visible = false;
                List_Contorls[4].Visible = false; List_Contorls[5].Visible = false;
                List_Contorls[6].Visible = false; List_Contorls[7].Visible = false;
            }

            if (Index == 13)
            {
                List_Contorls[2].Visible = false; List_Contorls[3].Visible = false;
                List_Contorls[4].Visible = true; List_Contorls[5].Visible = true;
                List_Contorls[6].Visible = true; List_Contorls[7].Visible = true;
                List_Contorls[6].Location = new Point(20, 122); List_Contorls[7].Location = new Point(20, 150);
                List_Contorls[6].Size = new Size(280, 22); List_Contorls[7].Size = new Size(280, 22);
            }

            if (Index == 14)
            {
                List_Contorls[2].Visible = false; List_Contorls[3].Visible = false;
                List_Contorls[4].Visible = false; List_Contorls[5].Visible = false;
                List_Contorls[6].Visible = true; List_Contorls[7].Visible = true;
                List_Contorls[6].Location = new Point(220, 122); List_Contorls[7].Location = new Point(220, 150);
                List_Contorls[6].Size = new Size(320, 22); List_Contorls[7].Size = new Size(320, 22);
            }
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Сохранение параметров кейса и добавление его в список
        /// </summary>
        private void Button_Save_Click(object sender, EventArgs e)
        {
            DialogResult Result =
                MessageBox.Show("Уверены, что хотите сохранить параметры кейса и добавить его в список?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (Result == DialogResult.Yes)
            {
                // Тип тестирования
                TestingTypes TestType = TestingTypes.ManualTesting; ;

                switch (List_Contorls[0].Text)
                {
                    case "Интеграционное тестирование":
                        TestType = TestingTypes.IntegrationTesting;
                        break;

                    case "Тестирование белым ящиком":
                        TestType = TestingTypes.WhiteBoxTesting;
                        break;

                    case "Нисходящее тестирование":
                        TestType = TestingTypes.DescendingTesting;
                        break;

                    case "Модульное тестирование":
                        TestType = TestingTypes.ModularTesting;
                        break;

                    case "Приемочное тестирование":
                        TestType = TestingTypes.AcceptanceTesting;
                        break;

                    case "Нагрузочное тестирование":
                        TestType = TestingTypes.LoadTesting;
                        break;
                }

                // Тестируемый элемент
                TestedElemnets TestElemnet = TestedElemnets.Button;
                ComboBox SelectedComboBox = (ComboBox)List_Contorls[1];

                switch (SelectedComboBox.SelectedIndex)
                {
                    case 1:
                        TestElemnet = TestedElemnets.PiсtureBox;
                        break;

                    case 2:
                        TestElemnet = TestedElemnets.Label;
                        break;

                    case 3:
                        TestElemnet = TestedElemnets.LinkLabel;
                        break;

                    case 4:
                        TestElemnet = TestedElemnets.TextBox;
                        break;

                    case 5:
                        TestElemnet = TestedElemnets.DataTable;
                        break;

                    case 6:
                        TestElemnet = TestedElemnets.TableLayoutPanel;
                        break;

                    case 7:
                        TestElemnet = TestedElemnets.Panel;
                        break;

                    case 8:
                        TestElemnet = TestedElemnets.Form;
                        break;

                    case 9:
                        TestElemnet = TestedElemnets.CorrectnessDataDisplayOnForms;
                        break;

                    case 10:
                        TestElemnet = TestedElemnets.ConnectingDatabaseToForm;
                        break;

                    case 11:
                        TestElemnet = TestedElemnets.DBTable;
                        break;

                    case 12:
                        TestElemnet = TestedElemnets.System;
                        break;

                    case 13:
                        TestElemnet = TestedElemnets.OtherOnForm;
                        break;

                    case 14:
                        TestElemnet = TestedElemnets.Other;
                        break;
                }

                _ = Cases + new TestingDefect(TestType, TestElemnet, List_Contorls[3].Text, List_Contorls[5].Text, List_Contorls[7].Text, TempTestResult);
            }

            Out_Cases();
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Удаление кейса из списка
        /// </summary>
        private void Button_Delete_Click(object sender, EventArgs e)
        {
            DialogResult Result =
                MessageBox.Show("Уверены, что хотите удалить кейс из списка?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (Result == DialogResult.Yes)
            {
                PictureBox Picture = (PictureBox)sender;
                int Index = Convert.ToInt32(Picture.Tag);
                _ = Cases - Cases[Index];
            }

            Out_Cases();
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Изменение значения результата тестирования
        /// </summary>
        private void Button_Result_Click(object sender, EventArgs e)
        {
            Label TempLabel = (Label)List_Contorls[8];

            if (TempTestResult)
            {
                TempTestResult = false; TempLabel.Text = "Отрицательно";
                TempLabel.Size = new Size(TempLabel.Size.Width + 6, TempLabel.Size.Height + 6);
                TempLabel.Location = new Point(TempLabel.Location.X - 3, TempLabel.Location.Y - 3);
            }
            else
            {
                TempTestResult = true; TempLabel.Text = "Положительно";
                TempLabel.Size = new Size(TempLabel.Size.Width - 6, TempLabel.Size.Height - 6);
                TempLabel.Location = new Point(TempLabel.Location.X + 3, TempLabel.Location.Y + 3);
            }

            TempLabel.BringToFront();
        }

        private void Button_Create_Click(object sender, EventArgs e)
         => Output();

        /// <summary> Вывод данных и генерация документа </summary>
        private void Output()
        {
            try
            {
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
            catch (Exception Ex)
            {
                Authorization.LoG.Info($"Output|Неизвестная ошибка. {Ex.Message}");
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Word(Paragraph Para, SpiceHorizontalAlignment Alignment, string Text, bool TBold)
        {
            Para.Format.HorizontalAlignment = Alignment;
            TextRange TableRange = Para.AppendText(Text);
            TableRange.CharacterFormat.FontName = "Times New Roman";
            TableRange.CharacterFormat.FontSize = 14;
            TableRange.CharacterFormat.Bold = TBold;
        }

        private void Main_Resize(object sender, EventArgs e) => Out_Cases();
    }
}