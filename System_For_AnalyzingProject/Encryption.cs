using System;
using System.Windows.Forms;

namespace System_For_AnalyzingProject
{
    public class Encryption
    {
        /// <summary> Допустимые наборы символов </summary>
        private const string
            ENG = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            RUS = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ",
            Numbers = "0123456789",
            Symbols = "!@#$%^&*()+=_-?:;~",
            Hieroglyphs = "用而不前博均解斯给它数その学究合何に私い降れス雨람을없아죄수러壘테个洒澜谜铝铜磨瘾瘸凝辨모" +
            "빛를슴용囤员金范围园且十丫三大币只田소낯과근니대火木上中口人先青山川森雲王外牛玉見工黒乖拧夷岁日以尘示吮我" +
            "含집검건패달품칠지비질隶車豸貝門鬯馬鼻齒龜龠隹非出書色心西雪千組竹町道年風百文米北名明侧殃品哪衍香侠网众阶";

        /// <summary> Активация или деактивация наборов символов </summary>
        private bool[] Bools = { false, false, false, false };

        /// <summary> Смещение символов </summary>
        private int Shift = 1;

        /// <summary> Ключ </summary>
        private string Key = "";

        /// <summary> Символы столбцов </summary>
        private string Headlines = "";

        /// <summary> Таблица кодирования </summary>
        private char[,] EncodingTable;

        /// <summary> Конструктор </summary>
        /// <param name="SetBools">Активация или деактивация наборов символов</param>
        /// <param name="SetKey">Назначение ключа</param>
        /// <param name="SetShift">Назначение смещения</param>
        public Encryption(bool[] SetBools, string SetKey, int SetShift)
        {
            try
            {
                Bools = SetBools; Key = SetKey; Shift = SetShift;

                if (Bools[0]) Headlines += ENG + ENG.ToLower();
                if (Bools[1]) Headlines += RUS + RUS.ToLower();
                if (Bools[2]) Headlines += Numbers;
                if (Bools[3]) Headlines += Symbols;

                EncodingTable = new char[Headlines.Length, Headlines.Length];

                for (int I1 = 0, Index = 0; I1 < EncodingTable.GetLength(0); I1++)
                {
                    for (int I2 = 0; I2 < EncodingTable.GetLength(1); I2++, Index += Shift)
                    {
                        if (Index > Hieroglyphs.Length - 1) Index -= Hieroglyphs.Length;
                        EncodingTable[I1, I2] = Hieroglyphs[Index];
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Encryption#Encryption\n{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary> Поиск индекса символа в массиве символов </summary>
        /// <param name="Li">Искомый символ</param>
        private int Indexing(char Li)
        {
            for (int I1 = 0; I1 < Headlines.Length; I1++)
                    if (Li == Headlines[I1]) return I1;
                throw new Exception();
        }

        /// <summary> Закодирование </summary>
        /// <param name="Text">Текст для закодирования</param>
        public string Encoding(string Text)
        {
            try
            {
                string Final = "", StringKey = "";

                for (int I1 = 0; StringKey.Length < Text.Length; I1++)
                {
                    if (I1 == Key.Length) I1 = 0;
                    StringKey += Key[I1];
                }

                for (int I1 = 0; I1 < Text.Length; I1++)
                {
                    int[] Index = { Indexing(Text[I1]), Indexing(StringKey[I1]) };
                    if (Index[0] != -1 & Index[1] != -1)
                        Final += EncodingTable[Index[0], Index[1]];
                }

                return Final;
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Encryption#Encoding\n{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return "ERROR";
            }
        }

        /// <summary> Раскодирование </summary>
        /// <param name="Text">Текст для раскодирование</param>
        public string Decoding(string Text)
        {
            try
            {
                string Final = "", StringKey = "";

                for (int I1 = 0; StringKey.Length < Text.Length; I1++)
                {
                    if (I1 == Key.Length) I1 = 0;
                    StringKey += Key[I1];
                }

                for (int I1 = 0; I1 < Text.Length; I1++)
                {
                    int Index = Indexing(StringKey[I1]);

                    for (int I2 = 0; I2 < Headlines.Length; I2++)
                        if (EncodingTable[I2, Index] == Text[I1]) Final += Headlines[I2];
                }

                return Final;
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Encryption#Decoding\n{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return "ERROR";
            }
        }
    }
}