namespace Homework.Sets.Operations
{
    public static class Operations
    {
        public static IFuzzySet UnaryOperation(IFuzzySet set, IUnaryFunction function)
        {
            var newSet = new MutableFuzzySet(set.GetDomain());
            foreach (var element in newSet.GetDomain())
                newSet.Set(element, function.ValueAt(set.GetValueAt(element)));

            return newSet;
        }

        public static IFuzzySet BinaryOperation(IFuzzySet set1, IFuzzySet set2, IBinaryFunction function)
        {
            var newSet = new MutableFuzzySet(set1.GetDomain());

            foreach (var element in set1.GetDomain())
                newSet.Set(element, function.ValueAt(set1.GetValueAt(element), set2.GetValueAt(element)));

            return newSet;
        }

        public static IUnaryFunction ZadehNot() => new ZadehNot();

        public static IBinaryFunction ZadehAnd() => new ZadehAnd();

        public static IBinaryFunction ZadehOr() => new ZadehOr();

        public static IBinaryFunction HamacherTNorm(double nu) => new HamacherTNorm(nu);

        public static IBinaryFunction HamacherSNorm(double nu) => new HamacherSNorm(nu);
    }
}