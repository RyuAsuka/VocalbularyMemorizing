using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace VocalbularyMemorizing
{
    class Program
    {
        const string inFilePath = @"WordList.txt";
        const string outFilePath = @"statics.txt";
        static readonly List<WordMemo> wordList = new List<WordMemo>();
        static int training_times = GetTrainingTimes();

        static void Main(string[] args)
        {
            Console.Clear();
            using (StreamReader sr = new StreamReader(inFilePath))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] ss = s.Split(':');
                    WordMemo wm = new WordMemo(ss[0].Trim(), ss[1].Trim());
                    wordList.Add(wm);
                }
            }

            Random rd = new Random();
            int i = 1;
            int count_correct = 0;
            while (i <= wordList.Count)
            {
                int rdm = rd.Next(wordList.Count);
                WordMemo word = wordList[rdm];
                if (word.IsPassed == true)
                    ;
                else
                {
                    Console.WriteLine("{0}: {1}", i, word.Word.Meaning);
                    Console.Write(">" + word.Word.Content.Substring(0,2));
                    string ans = Console.ReadLine();
                    if (ans == word.Word.Content.Substring(2))
                    {
                        Console.WriteLine("Correct!");
                        word.IsCorrect = true;
                        count_correct++;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect! The correct answer is: {0}", word.Word.Content);
                        word.IsCorrect = false;
                    }
                    word.IsPassed = true;
                    i++;
                    Console.WriteLine();
                }
            }

            double correct_ratio = (double)count_correct / (double)wordList.Count;
            int count_incorrect = wordList.Count - count_correct;

            Console.WriteLine("Correct Ratio = {0:P}", correct_ratio);



            using (StreamWriter sw = new StreamWriter(outFilePath, true))
            {
                sw.WriteLine("{0}: correct {1}, incorrect {2}, correct ratio = {3:P}", training_times, count_correct, count_incorrect, correct_ratio);
            }

        }

        static int GetTrainingTimes()
        {
            try
            {
                int count = 1;
                using (StreamReader sr = new StreamReader(outFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        sr.ReadLine();
                        count++;
                    }
                    return count;
                }
            }
            catch (FileNotFoundException ex)
            {
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
