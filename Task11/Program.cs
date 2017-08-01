using System;

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
            System.Threading.Thread.Sleep(t);
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

        const string Alphabet0 = " абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        const string Alphabet1 = " жгвбыршчонмлпьхфизцаяюэеутёъщкйдс";
        static string OldAlphabet = "";
        static string NewAlphabet = "";

        static void MakeAlphabet(out string oldAlph, out string newAlph)
        {
            oldAlph = " ";
            newAlph = " ";
            Clear();

            while (true)
            {
                ResetColor();
                WriteLine("Введите пары символов;\nПервый - Кодируемый\nВторой - Код");

                WriteLine("Было:  {0}", oldAlph);
                WriteLine("Стало: {0}", newAlph);
                string pair = ReadLine();
                if (pair == string.Empty)
                    break;

                pair = pair.Replace(" ","");
                if (pair.Length != 2)
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Введите два символа");
                    continue;
                }

                string newpair = string.Empty;
                foreach (char c in pair)
                    if (char.IsLetterOrDigit(c))
                        newpair += c;

                pair = newpair;
                if (pair.Length != 2)
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Введите буквы или цифры");
                    continue;
                }

                if (oldAlph.Contains(pair[0].ToString())
                    || newAlph.Contains(pair[1].ToString()))
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Нельзя иметь повторяющтеся символы");
                    continue;
                }

                oldAlph += pair[0];
                newAlph += pair[1];
                Clear();
            }

            ResetColor();
        }

        static char EncodeChar(char c, string alph0,string alph1)
        {
            int i = alph0.IndexOf(c);
            return i == -1? c: alph1[i];
        }
        static string Encode(string s)
        {
            string output = string.Empty;
            foreach (char c in s)
                output += EncodeChar(c,OldAlphabet,NewAlphabet);

            return output;
        }
        static string Decode(string s)
        {
            string output = string.Empty;
            foreach (char c in s)
                output += EncodeChar(c, NewAlphabet, OldAlphabet);

            return output;
        }
        
        static void Main(string[] args)
        {
            {
                string yn = "[Y/N] ";
                Write("Создать новую таблциу кодировки?\n{0}",yn);
                CursorVisible = false;

                while (true)
                {
                    var key = ReadKey(true);
                    if (key.Key == ConsoleKey.Y)
                    {
                        Write(key.Key.ToString());
                        MakeAlphabet(out OldAlphabet,out NewAlphabet);
                        break;
                    }

                    if (key.Key == ConsoleKey.N)
                    {
                        Write(key.Key.ToString());
                        OldAlphabet = Alphabet0;
                        NewAlphabet = Alphabet1;
                        break;
                    }

                    FlashingWarning(yn);
                }

                CursorVisible = true;
            }

            while (true)
            {
                Clear();
                string choice = "[1/2/3] ";
                {
                    WriteLine(
@"Кодировка:
Было:
 {0}
Стало:
 {1}
", OldAlphabet, NewAlphabet);

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
                    string msg = ReadLine();

                    string newmsg = string.Empty;
                    foreach (char c in msg)
                        if (OldAlphabet.Contains(c.ToString()))
                            newmsg += c;

                    if (newmsg != msg)
                    {
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine("Были удалены некторые символы");
                        ResetColor();
                    }
                    WriteLine("Было:\n{0}\nСтало:\n{1}",newmsg,Encode(newmsg));

                    ReadKey(true);
                    continue;
                }

                if (key.KeyChar == '2')
                {
                    WriteLine(key.KeyChar);

                    WriteLine("Введите декодируемое сообщение");
                    string msg = ReadLine();

                    string newmsg = string.Empty;
                    foreach (char c in msg)
                        if (NewAlphabet.Contains(c.ToString()))
                            newmsg += c;

                    if (newmsg != msg)
                    {
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine("Были удалены некторые символы");
                        ResetColor();
                    }
                    WriteLine("Было:\n{0}\nСтало:\n{1}", newmsg, Decode(newmsg));

                    ReadKey(true);
                    continue;
                }

                if (key.KeyChar == '3' || key.Key == ConsoleKey.Escape)
                {
                    WriteLine(key.KeyChar);
                    return;
                }

                FlashingWarning(choice);
            }
        }
    }
}
