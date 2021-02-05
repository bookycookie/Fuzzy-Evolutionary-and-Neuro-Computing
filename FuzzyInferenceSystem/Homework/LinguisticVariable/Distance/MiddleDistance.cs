using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.Distance
{
    public class MiddleDistance : IDistance
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public MiddleDistance(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(0, 1301),
                StandardFuzzySets.LambdaFunction(30, 90, 150));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}