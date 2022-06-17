using OpenXmlPowerTools;

namespace WebApplication3.Mechanics
{
    public class Generate
    {
        static string AnswerX;
        static string Answerx;
        static string AnswerInt;
        static long Value;
        String[] LettersX = new string[] {"Q","W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M"};
        String[] Lettersx = new string[] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" };
        int[] LettersInt = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Random random = new Random();

        public string GenerateRandomX(int length)
        {
            for(int i = 0; i < length; i++)
            {
                string rest;
                rest = LettersX[random.Next(0, 25)];
                AnswerX = AnswerX + rest;
            }
            return AnswerX;
        }

        public string GenerateRandomx(int length)
        {
            for (int i = 0; i < length; i++)
            {
                string rest;
                rest = Lettersx[random.Next(0, 25)];
                Answerx = Answerx + rest;
            }
            return Answerx;
        }

        public long GenerateRandomValue(int length)
        {
            for (int i = 0; i < length; i++)
            {
                string rest;
                rest = LettersInt[random.Next(0, 25)].ToString();
                AnswerInt = AnswerInt + rest;
            }
            Value = long.Parse(AnswerInt);
            return Value;
        }

    }
}
