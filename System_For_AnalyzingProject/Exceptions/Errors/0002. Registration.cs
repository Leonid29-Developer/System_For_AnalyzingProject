using System;

namespace System_For_AnalyzingProject.Exceptions.Errors
{
    /// <summary> Error #0002 <br/> 
    /// Класс-исключение «Неудачная попытка регистрации» </summary>
    public class Registration : Exception
    {
        public Registration() : base($"Неудачная попытка регистрации")
            => Data.Add("Kod", "Ошибка #0002");
    }
}