using System;
using Homework.Domain;
using Homework.Sets;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Homework
{
    public class Relations
    {
        public static bool IsUTimesURelation(IFuzzySet relation) =>
            relation.GetDomain().GetNumberOfComponents() == 2 && relation.GetDomain().GetComponent(0).Equals(relation.GetDomain().GetComponent(1));

        public static bool IsSymmetric(IFuzzySet relation)
        {
            if (!IsUTimesURelation(relation)) return false;

            var domain = relation.GetDomain();

            var firstComponent = domain.GetComponent(0);
            var secondComponent = domain.GetComponent(1);

            for (var i = 0; i < firstComponent.GetCardinality(); i++)
            for (var j = i + 1; j < secondComponent.GetCardinality(); j++)
            {
                var iElementIndex = firstComponent.ElementForIndex(i).GetComponentValue(0);
                var jElementIndex = secondComponent.ElementForIndex(j).GetComponentValue(0);

                var ijValue = relation.GetValueAt(new DomainElement(iElementIndex, jElementIndex));
                var jiValue = relation.GetValueAt(new DomainElement(jElementIndex, iElementIndex));

                if (ijValue != jiValue) return false;
            }
            return true;
        }

        public static bool IsReflexive(IFuzzySet relation)
        {
            if (!IsUTimesURelation(relation)) return false;

            var domain = relation.GetDomain();

            var firstComponent = domain.GetComponent(0);
            for (var i = 0; i < firstComponent.GetCardinality(); i++)
            {
                var diagonalValue = firstComponent.ElementForIndex(i).GetComponentValue(0);

                if (relation.GetValueAt(new DomainElement(diagonalValue, diagonalValue))  != 1)
                    return false;
            }
            return true;
        }

        public static bool IsMaxMinTransitive(IFuzzySet relation)
        {
            if (!IsUTimesURelation(relation)) return false;

            var universalComponent = relation.GetDomain().GetComponent(0);
            for (var x = 0; x < universalComponent.GetCardinality(); x++)
            for (var z = 0; z < universalComponent.GetCardinality(); z++)
            {
                var xValue = universalComponent.ElementForIndex(x).GetComponentValue(0);
                var zValue = universalComponent.ElementForIndex(z).GetComponentValue(0);
                var xzElementValue = relation.GetValueAt(new DomainElement(xValue, zValue));
                
                var maxYValue = 0.0;
                for (var y = 0; y < universalComponent.GetCardinality(); y++)
                {
                    var yValue = universalComponent.ElementForIndex(y).GetComponentValue(0);

                    var xyElementValue = relation.GetValueAt(new DomainElement(xValue, yValue));
                    var yzElementValue = relation.GetValueAt(new DomainElement(yValue, zValue));

                    var tmpMaxYValue = Math.Min(xyElementValue, yzElementValue);

                    if (tmpMaxYValue > maxYValue) maxYValue = tmpMaxYValue;
                }

                if (!(xzElementValue < maxYValue)) continue;
                return false;
            }
            return true;
        }

        public static IFuzzySet? CompositionOfBinaryRelations(IFuzzySet A, IFuzzySet B)
        {
            if (!AreRelationsMultiplicative(A, B)) return null;

            var rowAComponent = A.GetDomain().GetComponent(0);
            var columnAComponent = A.GetDomain().GetComponent(1);
            var rowBComponent = B.GetDomain().GetComponent(0);
            var columnBComponent = B.GetDomain().GetComponent(1);

            var rowsA = rowAComponent.GetCardinality();
            var colsA = columnAComponent.GetCardinality();
            
            var colsB = columnBComponent.GetCardinality();
            
            var fuzzySet = new MutableFuzzySet(new CompositeDomain(new SimpleDomain[]
            {
                (SimpleDomain) rowAComponent,
                (SimpleDomain) columnBComponent
            }));

            for (var i = 0; i < rowsA; i++)
            for (var j = 0; j < colsB; j++)
            {
                var maxValue = 0.0;
                for (var k = 0; k < colsA; k++)
                {
                    var iElement = rowAComponent.ElementForIndex(i).GetComponentValue(0);
                    var jElement = columnBComponent.ElementForIndex(j).GetComponentValue(0);
                    var kElement = rowAComponent.ElementForIndex(k).GetComponentValue(0);

                    var ikElement = new DomainElement(iElement, kElement);
                    var kjElement = new DomainElement(kElement, jElement);

                    var ikElementValue = A.GetValueAt(ikElement);
                    var kjElementValue = B.GetValueAt(kjElement);
                    var newElementValue = Math.Min(ikElementValue, kjElementValue);

                    if (maxValue < newElementValue) maxValue = newElementValue;
                }
                var iiElement = rowAComponent.ElementForIndex(i).GetComponentValue(0);
                var jjElement = columnBComponent.ElementForIndex(j).GetComponentValue(0);

                var ijElement = new DomainElement(iiElement, jjElement);
                fuzzySet.Set(ijElement, maxValue);
            }

            return fuzzySet;
        }
        private static bool AreRelationsMultiplicative(IFuzzySet r1, IFuzzySet r2) => r1.GetDomain().GetComponent(1).Equals(r2.GetDomain().GetComponent(0));

        public static bool IsFuzzyEquivalence(IFuzzySet relation) => IsReflexive(relation) && IsSymmetric(relation) && IsMaxMinTransitive(relation);
    }
}