using System;
using Homework.Domain;
using Homework.Sets;
using Homework.Sets.Operations;

namespace Homework
{
    class Program
    {
        private static void Main(string[] args)
        {
            SimpleDomain sd = new SimpleDomain(-4, 5);
            PrintDomain(sd, "sd: ");

            IDomain cd = IDomain.Combine(sd, sd);
            PrintDomain(cd, "cd: ");
            
            IDomain d = IDomain.IntRange(0, 11);
            IFuzzySet set1 = new MutableFuzzySet(d).Set(DomainElement.Of(0), 1.0);
            PrintFuzzySet(set1, "set1: ");
            
            IDomain d2 = IDomain.IntRange(-5, 6);
            IFuzzySet set2 = new CalculatedFuzzySet(d2,
                StandardFuzzySets.LambdaFunction(d2.IndexOfElement(DomainElement.Of(-4)),
                    d2.IndexOfElement(DomainElement.Of(0)),
                    d2.IndexOfElement(DomainElement.Of(4))));
            PrintFuzzySet(set2, "set2: ");
            
            
            IDomain d7 = IDomain.IntRange(0, 11);
            
            IFuzzySet set3 = new MutableFuzzySet(d7).Set(DomainElement.Of(0), 1.0)
                .Set(DomainElement.Of(1), 0.8)
                .Set(DomainElement.Of(2), 0.6)
                .Set(DomainElement.Of(3), 0.4)
                .Set(DomainElement.Of(4), 0.2);
            
            PrintFuzzySet(set3, "set3: ");

            IFuzzySet notSet3 = Operations.UnaryOperation(set3, Operations.ZadehNot());
            PrintFuzzySet(notSet3, "notSet3: ");

            IFuzzySet union = Operations.BinaryOperation(set3, notSet3, Operations.ZadehOr());
            PrintFuzzySet(union, "Set3 U notSet3: ");

            IFuzzySet hinters = Operations.BinaryOperation(set3, notSet3, Operations.HamacherTNorm(1.0));
            PrintFuzzySet(hinters, "Set3 Intersection with notSet3 using parameterised Hamacher T norm with parameter 1.0: ");
        }

        private static void PrintDomain(IDomain domain, string headingText = "")
        {
            if (!string.IsNullOrEmpty(headingText)) Console.WriteLine(headingText);

            foreach (DomainElement de in domain)
                Console.WriteLine($"Element of domain: ({de})");

            Console.WriteLine($"Domain cardinality is: {domain.GetCardinality()}");
            Console.WriteLine();
        }

        private static void PrintFuzzySet(IFuzzySet set, string headingText = "") {
            if (!string.IsNullOrEmpty(headingText)) Console.WriteLine(headingText);
            
            foreach (var de in set.GetDomain()) Console.WriteLine($"d({de}) = {set.GetValueAt(de):0.000000}");
            Console.WriteLine();

        }
    }
}