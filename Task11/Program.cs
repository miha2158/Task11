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

        const string Alphabet0 = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        const string Alphabet1 = "жгвбыршчонмлпьхфизцаяюэеутёъщкйдс";
        static string NewAlphabet = "";

        static char EncodeChar(char c, string alph0,string alph1)
        {
            int i = alph0.IndexOf(c);
            return i == -1? c: alph1[i];
        }
        static string Encode(string s)
        {
            string output = string.Empty;
            foreach (char c in s)
                output += EncodeChar(c,Alphabet0,NewAlphabet);

            return output;
        }
        static string Decode(string s)
        {
            string output = string.Empty;
            foreach (char c in s)
                output += EncodeChar(c, NewAlphabet, Alphabet0);

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

            while (true)
            {
                string choice = "[1/2/3] ";
                {
                    WriteLine(
@"Кодировка:
Было:
 {0}
Стало:
 {1}
", Alphabet0, NewAlphabet);

                    Write(
@"Выберите действие
1. Закодировать
2. Раскодировать
3. Выход
{0}", choice);
                }

                bool done = false;
                var key = ReadKey(true);

                if (key.KeyChar == '1')
                {
                    WriteLine(key.KeyChar);

                    WriteLine("Введите кодируемое сообщение");
                    WriteLine("Закодированное:");
                    WriteLine(Encode(ReadLine()));

                    ReadKey(true);
                    Clear();
                }

                if (key.KeyChar == '2')
                {
                    Write(key.KeyChar);

                    WriteLine("Введите декодируемое сообщение");
                    WriteLine("Раскодированное:");
                    WriteLine(Decode(ReadLine()));

                    ReadKey(true);
                    Clear();
                }

                if (key.KeyChar == '3')
                    break;

                FlashingWarning(choice);

            }
            









            ReadKey(true);
        }
    }
}
