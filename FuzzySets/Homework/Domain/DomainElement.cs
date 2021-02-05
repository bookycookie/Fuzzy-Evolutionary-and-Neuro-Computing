namespace Homework.Domain
{
    public class DomainElement
    {
        private readonly int[] _values;
        public DomainElement(params int[] values) => _values = values;

        public int GetNumberOfComponents() => _values.Length;

        public int GetComponentValue(int index) => _values[index];

        public static DomainElement Of(params int[] values) => new DomainElement(values);

        public override string ToString() => string.Join(",", _values);
    }
}