using Homework.Domain;
using Homework.Sets;

namespace Homework.LinguisticVariable.Direction
{
    public class BadDirection : IDirection
    {
        public string Name { get; set; }
        public IFuzzySet FuzzySet { get; set; }

        public BadDirection(string name)
        {
            Name = name;
            FuzzySet = new MutableFuzzySet(IDomain.IntRange(0, 2))
                .Set(DomainElement.Of(0), 1.0)
                .Set(DomainElement.Of(1), 0.0);
        }
        
        public IDomain GetDomain() => FuzzySet.GetDomain();
        public double GetValueAt(DomainElement de) => FuzzySet.GetValueAt(de);
    }
}