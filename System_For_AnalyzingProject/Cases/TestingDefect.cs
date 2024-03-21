namespace System_For_AnalyzingProject
{
    /// <summary> Данные о кейсе тестирования </summary>
    public class TestingDefect //: ICases
    {
        /// <summary> Тип тестирования </summary>
        private TestingTypes TestingType { set; get; }

        /// <summary> Текст иного тестируемого элемента </summary>
        private string TextAnotherElement { set; get; }

        /// <summary> Тип тестируемого элемента </summary>
        private TestedElemnets TestedElemnet { set; get; }

        /// <summary> Имя тестируемого элемента </summary>
        private string NameTestedElemnet { set; get; }

        /// <summary> Имя формы, на которой находится тестируемый элемент </summary>
        private string FormName { set; get; }

        /// <summary> Результат тестирования </summary>
        private bool TestResult { set; get; }

        /// <summary> Конструктор </summary>
        /// <param name="NewTestingType">Тип тестирования</param>
        /// <param name="NewTestedElemnet">Тестируемый элемент</param>
        /// <param name="NewNameTestedElemnet">Имя тестируемого элемента</param>
        /// <param name="NewFormName">Имя формы, на которой находится тестируемый элемент</param>
        /// <param name="NewTextAnotherElement">Имя формы, на которой находится тестируемый элемент</param>
        public TestingDefect
            (TestingTypes NewTestingType, TestedElemnets NewTestedElemnet, string NewNameTestedElemnet, string NewFormName, string NewTextAnotherElement, bool Result)
        {
            TestResult = Result;
            FormName = NewFormName;
            TestingType = NewTestingType;
            TestedElemnet = NewTestedElemnet;
            NameTestedElemnet = NewNameTestedElemnet;
            TextAnotherElement = NewTextAnotherElement;
        }

        /// <summary> Получения типа тестирования </summary>
        public TestingTypes GetTestingType() => TestingType;

        /// <summary> Получения типа nестируемого элемента </summary>
        public TestedElemnets GetTestedElemnet() => TestedElemnet;

        /// <summary> Получения результата тестирования </summary>
        public bool GetResult() => TestResult;

        /// <summary> Подробности кейса без уточнения имени формы, на которой находится тестируемый элемент </summary>
        public override string ToString()
        {
            string Final;

            switch (TestedElemnet)
            {
                default:
                    Final = $"Проверка работоспособности {TestedElemnet}";
                    break;

                case TestedElemnets.Form:
                    Final = $"Проверка работоспособности формы «{NameTestedElemnet}»";
                    break;

                case TestedElemnets.CorrectnessDataDisplayOnForms:
                    Final = $"Проверка корректности отображения данных";
                    break;

                case TestedElemnets.ConnectingDatabaseToForm:
                    Final = $"Проверка работоспособности подключения БД";
                    break;

                case TestedElemnets.DBTable:
                    Final = $"Проверка работоспособности таблицы «{NameTestedElemnet}» БД";
                    break;

                case TestedElemnets.System:
                    Final = $"Проверка полной работоспособности системы";
                    break;

                case TestedElemnets.OtherOnForm:
                    Final = $"{TextAnotherElement}";
                    break;

                case TestedElemnets.Other:
                    Final = $"{TextAnotherElement}";
                    break;
            }

            return Final;
        }

        /// <summary> Подробности кейса c уточнением имени формы, на которой находится тестируемый элемент </summary>
        public string ToStringWithNames()
        {
            string Final;

            switch (TestedElemnet)
            {
                default:
                    Final = $"{ToString()} «{NameTestedElemnet}» в форме «{FormName}»";
                    break;

                case TestedElemnets.Form:
                    Final = $"Проверка работоспособности формы «{NameTestedElemnet}»";
                    break;

                case TestedElemnets.CorrectnessDataDisplayOnForms:
                    Final = $"{ToString()} на форме «{NameTestedElemnet}»";
                    break;

                case TestedElemnets.OtherOnForm:
                    Final = $"{TextAnotherElement} на форме «{FormName}»";
                    break;

                case TestedElemnets.Other:
                    Final = $"{ToString()}";
                    break;

                case TestedElemnets.System:
                    Final = $"{ToString()}";
                    break;
            }

            return Final;
        }
    }
}