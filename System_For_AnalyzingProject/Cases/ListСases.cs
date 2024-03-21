using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace System_For_AnalyzingProject
{
    /// <summary> Класс для работы с информацией кейсов </summary>
    public class ListСases
    {
        /// <summary> Список с кейсами </summary>
        private List<TestingDefect> Cases = new List<TestingDefect>();

        /// <summary> Конструктор </summary>
        public ListСases() { }

        /// <summary> Получение количества кейсов в списке </summary>
        public int GetCount() => Cases.Count;

        /// <summary> Добавление кейса в список </summary>
        /// <param name="List">Список с кейсами</param>
        /// <param name="Case">Кейс тестирования</param>
        public static int operator +(ListСases List, TestingDefect Case)
        {
            try
            {
                List.Cases.Add(Case);
                List.Cases.Sort
                    (new Comparison<TestingDefect>((X1, X2) => X1.GetTestedElemnet().CompareTo(X2.GetTestedElemnet())));
                return 0;
            }
            catch (Exception Ex)
            {
                Authorization.LoG.Info($"Button_Enter_Click|Неизвестная ошибка. {Ex.Message}");
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        /// <summary> Удаления кейса из списка </summary>
        /// <param name="List">Список с кейсами</param>
        /// <param name="Case">Кейс тестирования</param>
        public static int operator -(ListСases List, TestingDefect Case)
        {
            try
            {
                List.Cases.Remove(Case); return 0;
            }
            catch (Exception Ex)
            {
                Authorization.LoG.Info($"Button_Enter_Click|Неизвестная ошибка. {Ex.Message}");
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        /// <summary>  Вычисления процента реализации </summary>
        public int ImplementationPercentage()
        {
            int Count = 0;

            foreach (TestingDefect Case in Cases)
                if (Case.GetResult()) Count++;

            return (int)((float)Count / (float)Cases.Count * 100);
        }

        /// <summary> Описание использованных методов тестирования какими кейсами </summary>
        public string UsingCaseTestingMethod()
        {
            string Final = "";
            List<TestedElemnets> Elemnets = new List<TestedElemnets>();
            List<ElementsWithType> Types = new List<ElementsWithType>();

            foreach (TestingDefect Case in Cases)
            {
                bool[] T = { true, true };

                foreach (TestedElemnets Elemnet in Elemnets)
                    if (Case.GetTestedElemnet() == Elemnet) T[0] = false;

                foreach (ElementsWithType Type in Types)
                    if (Case.GetTestedElemnet() == Type.Elemnet & Case.GetTestingType() == Type.Type) T[1] = false;

                if (T[0]) Elemnets.Add(Case.GetTestedElemnet());
                if (T[1])
                {
                    Types.Add(new ElementsWithType(Case.GetTestedElemnet(), Case.GetTestingType()));
                    CountElemnetAdd(Types, Case.GetTestedElemnet());
                }
            }

            // Описание типа тестирования, тестируемого элемента и какие номера кейсов использовались
            foreach (TestedElemnets Elemnet in Elemnets)
            {
                List<int> Indexes = new List<int>(); 

                if (Elemnet == TestedElemnets.OtherOnForm | Elemnet == TestedElemnets.Other)
                {
                    foreach (TestingDefect Case in Cases)
                        if (Case.GetTestedElemnet() == Elemnet)
                        { Indexes.Add(Cases.IndexOf(Case) + 1); break; }

                    Final += $"{Cases[Indexes[0] - 1]} ";
                }
                else
                {
                    foreach (TestingDefect Case in Cases)
                        if (Case.GetTestedElemnet() == Elemnet)
                            Indexes.Add(Cases.IndexOf(Case) + 1);

                    // Описание тестируемого элемента
                    switch (Elemnet)
                    {
                        default:
                            Final += $"Проверка работоспособности {Elemnet} ";
                            break;

                        case TestedElemnets.Form:
                            Final += $"Проверка работоспособности форм программы ";
                            break;

                        case TestedElemnets.CorrectnessDataDisplayOnForms:
                            Final += $"Проверка корректности отображения данных на формах ";
                            break;

                        case TestedElemnets.ConnectingDatabaseToForm:
                            Final += $"Проверка работоспособности подключения БД ";
                            break;

                        case TestedElemnets.DBTable:
                            Final += $"Проверка работоспособности таблицы БД ";
                            break;

                        case TestedElemnets.System:
                            Final += $"Проверка полной работоспособности системы ";
                            break;
                    }
                }

                // Номера кейсов, использованные данным тестируемым элементом 
                Final += $"({(Indexes.Count == 1 ? "кейс" : "кейсы")}" +
                    $" {ConvertNumbers(Indexes)}) производилась ";

                // Описание типов тестирования
                int CountType = 0;
                foreach (ElementsWithType Type in Types)
                    if (Type.Elemnet == Elemnet)
                    {
                        CountType++;

                        switch (Type.Type)
                        {
                            case TestingTypes.ManualTesting:
                                Final += $"ручным";
                                break;

                            case TestingTypes.IntegrationTesting:
                                Final += $"интеграционным";
                                break;

                            case TestingTypes.DescendingTesting:
                                Final += $"нисходящим";
                                break;

                            case TestingTypes.LoadTesting:
                                Final += $"нагрузочным";
                                break;

                            case TestingTypes.ModularTesting:
                                Final += $"модульным";
                                break;

                            case TestingTypes.WhiteBoxTesting:
                                Final += $"тестированием белым ящиком\n";
                                break;

                            case TestingTypes.AcceptanceTesting:
                                Final += $"приемочным";
                                break;
                        }

                        if (CountType == Type.CountElemnet)
                        {
                            if (Type.Type != TestingTypes.WhiteBoxTesting)
                                Final += $" тестированием\n";
                        }
                        else Final += $" и ";
                    }
            }

            if (Final[Final.Length-1] == '\n') Final = Final.Remove(Final.Length-1,1);
            return Final;
        }

        /// <summary> Конвертирование вида перечисления чисел </summary>
        /// <param name="Numbers">Список чисел</param>
        private string ConvertNumbers(List<int> Numbers)
        {
            string ConvertedNumbers = "";
            IEnumerable<int> Remains = Numbers.AsEnumerable();

            while (Remains.Any())
            {
                int FirstNumber = Remains.First();
                int EndNumber = Remains.TakeWhile((x, i) => x - FirstNumber == i).Last();
                Remains = Remains.Skip(EndNumber - FirstNumber + 1);
                ConvertedNumbers += $"{FirstNumber}{(FirstNumber == EndNumber ? "" : "-" + EndNumber)}" +
                    $"{(Remains.Any() ? "," : "")}";
            }

            return ConvertedNumbers;
        }

        /// <summary> Увеличение количества повторений тестируемого элемента </summary>
        private void CountElemnetAdd(List<ElementsWithType> List, TestedElemnets DesiredElemnet)
        {
            int Count = 0;
            foreach (ElementsWithType EWT in List)
                if (DesiredElemnet == EWT.Elemnet) Count++;

            foreach (ElementsWithType EWT in List)
                if (DesiredElemnet == EWT.Elemnet) EWT.CountElemnet = Count;
        }

        /// <summary> Индексатор </summary>
        public TestingDefect this[int Index]
        {
            get
            {
                return Cases[Index];
            }
        }
    }
}