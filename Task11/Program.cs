using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static System.Console;

namespace Task11
{
    class Program
    {
        static void WriteSleep(string s, ConsoleColor fore = ConsoleColor.Gray, int t = 200)
        {
            CursorLeft = 0;
            ForegroundColor = fore;
            Write(s);
            Thread.Sleep(t);
        }
        static void FlashingWarning(string s)
        {
            WriteSleep(s, ConsoleColor.Red);
            WriteSleep(s, ConsoleColor.Green);
            WriteSleep(s, ConsoleColor.Red);
            WriteSleep(s, ConsoleColor.Green);
            WriteSleep(s, ConsoleColor.Red);
            WriteSleep(s,t:0);
            ResetColor();
        }

        static readonly string Alphabet0 = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        static readonly string Alphabet1 = "жгвбыршчонмлпьхфизцаяюэеутёъщкйдс";
        static string NewAlphabet = "";

        static char EncodeChar(char c)
        {
            int i = Alphabet0.IndexOf(c);
            return i == -1? c: NewAlphabet[i];
        }

        static string Encode(string s)
        {
            string output = string.Empty;
            foreach (char c in s)
                output += EncodeChar(c);

            return output;
        }

        static string MakeAlphabet()
        {
            return string.Empty;
        }

        static void Main(string[] args)
        {
            WriteLine("Создать новую таблциу кодировки? ");
            string yn = "[Y/N] ";
            CursorVisible = false;

            while (true)
            {
                var key = ReadKey(true);
                if (key.Key == ConsoleKey.N)
                {
                    Write(key.KeyChar);
                    NewAlphabet = MakeAlphabet();
                    break;
                }

                if (key.Key == ConsoleKey.Y)
                {
                    Write(key.KeyChar);
                    NewAlphabet = Alphabet1;
                    break;
                }
                
                FlashingWarning(yn);
            }

            CursorVisible = true;
            Clear();

            WriteLine("Кодировка:\n{0}\n{1}\n",Alphabet0,NewAlphabet);













            ReadKey(true);
        }
    }
}
