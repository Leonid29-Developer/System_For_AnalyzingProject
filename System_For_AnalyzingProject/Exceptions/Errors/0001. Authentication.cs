using System;

namespace System_For_AnalyzingProject.Exceptions.Errors
{
    /// <summary> Error #0001 <br/> 
    /// Класс-исключение «Неверные данные аутентификации» </summary>
    public class Authentication : Exception
    {
        public Authentication() : base($"Неверные данные аутентификации")
            => Data.Add("Kod", "Ошибка #0001");
    }
}