using Homework.Domain;

namespace Homework.Sets
{
    public class CalculatedFuzzySet : IFuzzySet
    {
        private IDomain Domain { get; set; }
        private IIntUnaryFunction Function { get; set; }        
        
        public CalculatedFuzzySet(IDomain domain, IIntUnaryFunction function)
        {
            Domain = domain;
            Function = function;
        }

        public IDomain GetDomain() => Domain;

        public double GetValueAt(DomainElement de) => Function.ValueAt(Domain.IndexOfElement(de));
    }
}