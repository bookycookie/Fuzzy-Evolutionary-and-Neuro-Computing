using Homework.Domain;

namespace Homework.Sets
{
    public class MutableFuzzySet : IFuzzySet
    {
        private readonly double[] _memberships;
        private IDomain Domain { get; }

        public MutableFuzzySet(IDomain domain)
        {
            Domain = domain;
            _memberships = new double[domain.GetCardinality()];
        }

        public IDomain GetDomain() => Domain;

        public double GetValueAt(DomainElement de) => _memberships[Domain.IndexOfElement(de)];

        public MutableFuzzySet Set(DomainElement de, double membership)
        {
            _memberships[Domain.IndexOfElement(de)] = membership;
            return this;
        }
    }
}