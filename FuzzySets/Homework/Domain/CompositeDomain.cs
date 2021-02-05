using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Homework.Domain
{
    public class CompositeDomain : IDomain
    {
        private readonly SimpleDomain[] _simpleDomains;
        private readonly int _totalCardinality;

        public CompositeDomain(SimpleDomain[] simpleDomains)
        {
            _simpleDomains = simpleDomains;
            _totalCardinality = GetCardinality();
        }

        public int GetCardinality() => _simpleDomains.Aggregate(1, (current, simpleDomain) => current * simpleDomain.GetCardinality());

        public IDomain GetComponent(int index) => _simpleDomains[index];

        public int GetNumberOfComponents() => _simpleDomains.Length;

        public int IndexOfElement(DomainElement de)
        {
            var tmpTotalCardinality = 1;
            var numberOfSteps = 0;

            for (var i = _simpleDomains.Length - 1; i >= 0; i--)
            {
                var currentDomainElementValue = de.GetComponentValue(i);

                var index = currentDomainElementValue - _simpleDomains[i].GetFirst();
                
                numberOfSteps += index * tmpTotalCardinality;
                tmpTotalCardinality *= _simpleDomains[i].GetCardinality();

            }
            return numberOfSteps;
        }

        public DomainElement ElementForIndex(int index)
        {
            var arrayOfElementsForIndex = new int[GetNumberOfComponents()];
            
            var tmpTotalCardinality = _totalCardinality;

            for (var i = 0; i < _simpleDomains.Length; i++)
            {
                var domain = _simpleDomains[i];
                tmpTotalCardinality /= domain.GetCardinality();

                var currentDomainElementIndex = index / tmpTotalCardinality;
                index -= currentDomainElementIndex * tmpTotalCardinality;

                arrayOfElementsForIndex[i] = domain.ElementForIndex(currentDomainElementIndex).GetComponentValue(0);
            }

            return new DomainElement(arrayOfElementsForIndex);
        }

        public IEnumerator<DomainElement> GetEnumerator()
        {
            return new CompositeDomainEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        private class CompositeDomainEnumerator : IEnumerator<DomainElement>
        {
            private readonly CompositeDomain _domain;
            private int _currentIndex = -1;
            public CompositeDomainEnumerator(CompositeDomain domain) => _domain = domain;

            public bool MoveNext() => ++_currentIndex < _domain.GetCardinality();

            public void Reset() => _currentIndex = -1;

            public DomainElement Current => _domain.ElementForIndex(_currentIndex);

            object? IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}