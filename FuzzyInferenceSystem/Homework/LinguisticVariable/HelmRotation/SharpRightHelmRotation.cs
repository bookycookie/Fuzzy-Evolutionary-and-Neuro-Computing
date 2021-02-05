using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.HelmRotation
{
    public class SharpRightHelmRotation : IHelmRotation
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public SharpRightHelmRotation(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(-90, 91), StandardFuzzySets.LFunction(-90, -60));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}