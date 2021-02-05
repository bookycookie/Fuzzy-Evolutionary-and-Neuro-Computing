using System.Collections;
using System.Collections.Generic;

namespace Homework.Domain
{
    public class SimpleDomain : IDomain
    {
        private readonly int _first;
        private readonly int _last;


        public SimpleDomain(int first, int last)
        {
            _first = first;
            _last = last;
        }

        public IEnumerator<DomainElement> GetEnumerator()
        {
            return new SimpleDomainEnumerator(_first, _last);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int GetCardinality() => _last - _first;

        public IDomain GetComponent(int index) => this;

        public int GetNumberOfComponents() => 1;

        public int IndexOfElement(DomainElement de)
        {
            return de.GetComponentValue(0) - _first;
        }

        public DomainElement ElementForIndex(int index)
        {
            return new DomainElement(_first + index);
        }

        public int GetFirst() => _first;

        public int GetLast() => _last;
        
        private class SimpleDomainEnumerator : IEnumerator<DomainElement>
        {
            private int _currentIndex;
            private readonly int _first;
            private readonly int _last;

            public SimpleDomainEnumerator(int first, int last)
            {
                _first = first;
                _last = last;
                _currentIndex = first - 1;
            }
            
            public bool MoveNext() => ++_currentIndex < _last;

            public void Reset() => _currentIndex = _first;

            public DomainElement Current => new DomainElement(_currentIndex); 

            object? IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}