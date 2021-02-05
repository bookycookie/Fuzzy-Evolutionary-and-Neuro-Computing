using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.Distance
{
    public class ShortDistance : IDistance
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public ShortDistance(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(0, 1301), StandardFuzzySets.LFunction(40, 60));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}