using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TextStatistics
{
    class Program
    {
        static void PrintList(LinkedListNode<CntString> node)
        {
            if (node != null)
            {
                Console.Write(node.Value.ToString() + " ");
                if (node.Next != null)
                    PrintList(node.Next);
            }
        }
        static void Main(string[] args)
        {
            var textStat = new TextStatistics("   ll ll lp  abb z z z z 2ip.ru   abb abc abc lp word   world abc word");

            Console.WriteLine($"Кол-во слов в тексте: {textStat.Count}");
            Console.WriteLine($"\nВ тексте есть слово word: {textStat.Contains("word")}");
            Console.WriteLine($"В тексте есть слово rain: {textStat.Contains("rain")}");
            Console.WriteLine($"\nКол-во повторений слова word: {textStat["word"]}");
            Console.WriteLine($"Кол-во повторений слова rain: {textStat["rain"]}");
            Console.WriteLine($"\nСлово встречающееся мин. кол-во раз: {textStat.GetWordMinCount()}");
            Console.WriteLine($"Слово встречающееся мин. кол-во раз, но не менее 3: {textStat.GetWordMinCount(3)}");
            Console.WriteLine($"\nСлово встречающееся макс. кол-во раз: {textStat.GetWordMaxCount()}");
            Console.WriteLine($"Слово встречающееся макс. кол-во раз, но не более 3: {textStat.GetWordMaxCount(3)}");

            //Получим двусвязный список слов, начинающихся на "a"
            Console.Write("\nСписок слов на 'a': ");
            PrintList(textStat.WordsList("a").First);
            //Получим двусвязный список слов, начинающихся на "w"
            Console.Write("\nСписок слов на 'w': ");
            PrintList(textStat.WordsList("w").First);

            //Вывод всего БДП в инфиксном порядке (по алфавиту)
            Console.WriteLine("\n\nВывод всего БДП в инфиксном порядке (по алфавиту): ");
            textStat.PrintLn();
        }
    }
}
/*
Кол-во слов в тексте: 17

В тексте есть слово word: True
В тексте есть слово rain: False

Кол-во повторений слова word: 2
Кол-во повторений слова rain: 0

Слово встречающееся мин. кол-во раз: world
Слово встречающееся мин. кол-во раз, но не менее 3: abc

Слово встречающееся макс. кол-во раз: z
Слово встречающееся макс. кол-во раз, но не более 3: abc

Список слов на 'a': (abb, 2)  (abc, 3)
Список слов на 'w': (word, 2)  (world, 1)

Вывод всего БДП в инфиксном порядке (по алфавиту):
(2ip.ru, 1)  (abb, 2)  (abc, 3)  (ll, 2)  (lp, 2)  (word, 2)  (world, 1)  (z, 4)

*/