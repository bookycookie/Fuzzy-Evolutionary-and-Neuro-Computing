using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.Velocity
{
    public class MediumVelocity : IVelocity
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public MediumVelocity(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(0, 100), StandardFuzzySets.LambdaFunction(10, 40, 70));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}