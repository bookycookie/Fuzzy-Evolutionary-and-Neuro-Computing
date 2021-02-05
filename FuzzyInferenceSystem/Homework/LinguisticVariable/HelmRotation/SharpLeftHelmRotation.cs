using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.HelmRotation
{
    public class SharpLeftHelmRotation : IHelmRotation

    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public SharpLeftHelmRotation(string name)
        {
            Name = name;
            FuzzySet = new CalculatedFuzzySet(IDomain.IntRange(-90, 91), StandardFuzzySets.GammaFunction(60, 90));
        }

        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}