using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyMemorizing.Common
{
    public class WordList
    {
        private readonly List<WordMemo> wordList;

        public WordList()
        {
            wordList = new List<WordMemo>();
        }

        public void AddWord(string[] wordInfo)
        {
            WordMemo wm = new WordMemo(wordInfo[0], wordInfo[1]);
            wm.IsCorrect = false;
            wm.IsPassed = false;
            wordList.Add(wm);
        }

        public string GetMeaning(int index)
        {
            return wordList[index].Word.Meaning;
        }

        public string GetHint(int index)
        {
            return wordList[index].Word.Content.Substring(0, 2);
        }

        public string GetContent(int index)
        {
            return wordList[index].Word.Content;
        }

        public bool IsPassed(int index)
        {
            return wordList[index].IsPassed;
        }

        public void SetPassed(int index, bool value)
        {
            wordList[index].IsPassed = value;
        }

        public bool IsCorrect(int index)
        {
            return wordList[index].IsCorrect;
        }

        public void SetCorrect(int index, bool value)
        {
            wordList[index].IsCorrect = value;
        }

        public int Count()
        {
            return wordList.Count;
        }

        public double CorrectRatio()
        {
            int correct_count = 0;
            for(int i = 0; i < wordList.Count; i++)
            {
                if (wordList[i].IsCorrect == true)
                    correct_count++;
            }
            return (double)(correct_count) / (double)Count();
        }

        public bool IsAllFinished()
        {
            for(int i = 0; i < wordList.Count; i++)
            {
                if (!wordList[i].IsPassed)
                    return false;
            }
            return true;
        }

        public void Clear()
        {
            wordList.Clear();
        }
    }
}
