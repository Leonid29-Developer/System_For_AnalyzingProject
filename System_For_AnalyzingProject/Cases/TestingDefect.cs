namespace System_For_AnalyzingProject
{
    /// <summary>
    /// 
    /// </summary>
    public class TestingDefect
    {
        /// <summary> Тип тестирования </summary>
        //private TestingTypes TestingType { set; get; }

        /// <summary> Текст иного тестируемого элемента </summary>
        private string TextAnotherElement { set; get; }

        /// <summary> Тестируемый элемент </summary>
        private TestedElemnets TestedElemnet { set; get; }

        /// <summary> Имя тестируемого элемента </summary>
        private string NameTestedElemnet { set; get; }

        /// <summary> Имя формы, на которой находится тестируемый элемент </summary>
        private string FormName { set; get; }

        /// <summary> Результат тестирования </summary>
        //private bool TestResult { set; get; }

        /// <summary> Конструктор </summary>
        /// <param name="NewTestingType">Тип тестирования</param>
        /// <param name="NewTestedElemnet">Тестируемый элемент</param>
        /// <param name="NewNameTestedElemnet">Имя тестируемого элемента</param>
        /// <param name="NewFormName">Имя формы, на которой находится тестируемый элемент</param>
        /// <param name="NewTextAnotherElement">Имя формы, на которой находится тестируемый элемент</param>
        public TestingDefect
            (TestedElemnets NewTestedElemnet, string NewNameTestedElemnet, string NewFormName, string NewTextAnotherElement)
        {
            FormName = NewFormName;
            TestedElemnet = NewTestedElemnet;
            NameTestedElemnet = NewNameTestedElemnet;
            TextAnotherElement = NewTextAnotherElement;
        }

        ///// <summary> Используемые кейсы данным тестируемым элементом </summary>
        //public string Cases()
        //{
        //    //switch (TestingDefect)
        //    //{
        //    //    case TestingTypes.
        //    //}
        //    return $"";
        //}

        /// <summary> Подробности кейса </summary>
        public override string ToString()
        {
            string Final;

            switch (TestedElemnet)
            {
                default:
                    Final = $"Проверка работоспособности {TestedElemnet} «{NameTestedElemnet}» в форме «{FormName}»";
                    break;

                case TestedElemnets.Form:
                    Final = $"Проверка работоспособности формы «{NameTestedElemnet}»";
                    break;

                case TestedElemnets.CorrectnessDataDisplayOnForms:
                    Final = $"Проверка корректности отображения данных на форме «{NameTestedElemnet}»";
                    break;

                case TestedElemnets.ConnectingDatabaseToForm:
                    Final = $"Проверка работоспособности подключения БД";
                    break;

                case TestedElemnets.System:
                    Final = $"Проверка полной работоспособности системы";
                    break;

                case TestedElemnets.OtherOnForm:
                    Final = $"{TextAnotherElement} на форме «{FormName}»";
                    break;

                case TestedElemnets.Other:
                    Final = $"{TextAnotherElement}";
                    break;
            }

            return Final;
        }
    }
}