using System.Collections.Generic;
using Homework.Rules;

namespace Homework.FuzzyEngine
{
    public interface IFuzzyEngine
    {
        public List<IRule> RuleBase { get; set; }

        int Conclude(int l, int d, int lk, int dk, int v, int s);
    }
}