namespace System_For_AnalyzingProject
{
    /// <summary> Класс для хранения типа тестирования для конкретного тестового элемента </summary>
    public class ElementsWithType
    {
        /// <summary> Тестируемый элемент </summary>
        public TestedElemnets Elemnet { private set; get; }

        /// <summary> Количество повторений тестируемого элемента </summary>
        public int CountElemnet { set; get; } = 0;

        /// <summary> Тип тестирования </summary>
        public TestingTypes Type { private set; get; }

        /// <summary> Конструктор </summary>
        /// <param name="NewElemnet">Тестируемый элемент</param>
        /// <param name="NewType">Тип тестирования</param>
        public ElementsWithType(TestedElemnets NewElemnet, TestingTypes NewType)
        { Elemnet = NewElemnet; Type = NewType; }
    }
}