using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.HelmRotation
{
    public class NoHelmRotation : IHelmRotation
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public NoHelmRotation(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(-90, 91), StandardFuzzySets.LambdaFunction(-10, 0, 10));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}