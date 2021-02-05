using System.Collections.Generic;

namespace Homework.Domain
{
    public interface IDomain : IEnumerable<DomainElement>
    {

        public int GetCardinality();

        public IDomain GetComponent(int something);

        public int GetNumberOfComponents();

        public int IndexOfElement(DomainElement de);

        public DomainElement ElementForIndex(int index);

        public static IDomain Combine(IDomain d1, IDomain d2)
        {
            var domainComponents = d1.GetNumberOfComponents() + d2.GetNumberOfComponents();
            
            SimpleDomain[] domains = new SimpleDomain[domainComponents];
            var index = 0;
            for (var i = 0; i < d1.GetNumberOfComponents(); i++)
            {
                domains[i] = (d1.GetComponent(i) as SimpleDomain)!;
            }
            
            index += d1.GetNumberOfComponents();

            for (var i = 0; i < d2.GetNumberOfComponents(); i++)
            {
                domains[i + index] = (d2.GetComponent(i) as SimpleDomain)!;
            }
            
            return new CompositeDomain(domains);
        }

        public static IDomain IntRange(int first, int last)
        {
            return new SimpleDomain(first, last);
        }
    }
}