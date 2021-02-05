using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.Acceleration
{
    public class ZeroAcceleration : IAcceleration
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public ZeroAcceleration(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(-50, 51), StandardFuzzySets.LambdaFunction(-15, 0, 15));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();

        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}