using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocalbularyMemorizing
{
    public class WordMemo
    {
        /// <summary>
        /// 单词
        /// </summary>
        public Word Word { get; set; }

        /// <summary>
        /// 是否已遍历过
        /// </summary>
        public bool IsPassed { get; set; }

        /// <summary>
        /// 是否正确回答
        /// </summary>
        public bool IsCorrect { get; set; }

        public WordMemo(string content, string meaning)
        {
            Word = new Word
                {
                    Content = content,
                    Meaning = meaning
                };
            IsPassed = false;
            IsCorrect = false;
        }
    }
}
