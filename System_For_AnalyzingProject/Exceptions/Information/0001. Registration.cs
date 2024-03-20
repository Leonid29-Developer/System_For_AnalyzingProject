using System;

namespace System_For_AnalyzingProject.Exceptions.Information
{
    /// <summary> Класс-сообщение «Успешная регистрация» </summary>
    public class Registration : Exception
    {
        public Registration() : base($"Успешная регистрация")
            => Data.Add("Say", "Результат");
    }
}