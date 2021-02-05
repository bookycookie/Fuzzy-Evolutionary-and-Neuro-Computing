using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.Velocity
{
    public class SlowVelocity : IVelocity
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public SlowVelocity(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(0, 100), StandardFuzzySets.LFunction(0, 100));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}