using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocalbularyMemorizing
{
    /// <summary>
    /// 单词类
    /// </summary>
    public class Word
    {
        /// <summary>
        /// 单词本身
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 含义（英文）
        /// </summary>
        public string Meaning { get; set; }
    }
}
