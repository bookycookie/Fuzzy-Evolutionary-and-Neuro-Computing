using Homework.Domain;

namespace Homework.Sets
{
    public interface IFuzzySet
    {
        public IDomain GetDomain();
        public double GetValueAt(DomainElement de);
    }
}