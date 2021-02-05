using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.Distance
{
    public class LongDistance : IDistance
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public LongDistance(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(0, 1301), StandardFuzzySets.GammaFunction(100, 200));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();

        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}