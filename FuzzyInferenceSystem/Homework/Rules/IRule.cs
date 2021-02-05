using System.Collections.Generic;
using Homework.Sets;

namespace Homework.Rules
{
    public interface IRule
    {
        public List<IFuzzySet> Antecedent { get; set; }
        
        public IFuzzySet Consequent { get; set; }
        
        public List<string> Variables { get; set; }
    }
}