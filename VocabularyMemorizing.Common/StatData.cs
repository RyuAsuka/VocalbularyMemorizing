using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyMemorizing.Common
{
    public class StatData
    {
        Dictionary<string, int> sdl = new Dictionary<string,int>();

        /// <summary>
        /// 新建数据表时使用此构造函数
        /// </summary>
        /// <param name="l"></param>
        public StatData(WordList l)
        {
            for(int i = 0; i < l.Count(); i++)
            {
                sdl.Add(l.GetContent(i), 0);
            }
        }

        /// <summary>
        /// 从文件读入时使用此构造函数
        /// </summary>
        /// <param name="fileData"></param>
        public StatData(List<string[]> fileData)
        {
            for(int i = 0; i < fileData.Count; i++)
            {
                sdl.Add(fileData[i][0], int.Parse(fileData[i][1]));
            }
        }

        public void SelfAdd(string word)
        {
            sdl[word]++;
        }

        public KeyValuePair<string, int> GetItem(int index)
        {
            return new KeyValuePair<string, int>(sdl.ElementAt(index).Key, sdl.ElementAt(index).Value);
        }

        public int Count
        {
            get
            {
                return sdl.Count;
            }
        }

    }
}
