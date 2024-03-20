namespace System_For_AnalyzingProject
{
    public class TestingDefect
    {
        /// <summary> Тип тестирования </summary>
        private TestingTypes TestingType { set; get; }

        /// <summary> Тестируемый элемент </summary>
        private TestedElemnets TestedElemnet { set; get; }

        /// <summary> Имя тестируемого элемента </summary>
        private string NameTestedElemnet { set; get; }

        /// <summary> Имя формы, на которой находится тестируемый элемент </summary>
        private string FormName { set; get; }

        /// <summary> Конструктор </summary>
        /// <param name="NewTestingType">Тип тестирования</param>
        /// <param name="NewTestedElemnet">Тестируемый элемент</param>
        /// <param name="NewNameTestedElemnet">Имя тестируемого элемента</param>
        /// <param name="NewFormName">Имя формы, на которой находится тестируемый элемент</param>
        public TestingDefect
            (TestingTypes NewTestingType, TestedElemnets NewTestedElemnet, string NewNameTestedElemnet, string NewFormName)
        {
            TestingType = NewTestingType;
            TestedElemnet = NewTestedElemnet;
            NameTestedElemnet = NewNameTestedElemnet;
            FormName = NewFormName;
        }

        /// <summary> Расписание используемых кейсов данным тестируемым элементом </summary>
        public string Cases()
        {
            //switch (TestingDefect)
            //{
            //    case TestingTypes.
            //}
            return $"";
        }

        public override string ToString()
        {
            //switch (TestingDefect)
            //{
            //    case TestingTypes.
            //}
            return $"";
        }
    }
}