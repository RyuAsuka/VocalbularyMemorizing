using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyMemorizing.Common
{
    /// <summary>
    /// 用于正确率的统计信息显示的只读类，不应往该类中写入数据。
    /// </summary>
    public class CorrectRate
    {
        public int PassTime { get; private set; }
        public double CorrectRateValue { get; private set; }

        public CorrectRate(int passTime, double correctRate)
        {
            PassTime = passTime;
            CorrectRateValue = correctRate;
        }
    }
}
