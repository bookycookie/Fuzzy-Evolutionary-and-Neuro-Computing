using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.Acceleration
{
    public class NegativeAcceleration : IAcceleration
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public NegativeAcceleration(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(-50, 51), StandardFuzzySets.LFunction(-50, 0));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}