using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyMemorizing.Common
{
    public class CorrectCount
    {
        public string Word { get; private set; }

        public int Count { get; private set; }

        public CorrectCount(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}
