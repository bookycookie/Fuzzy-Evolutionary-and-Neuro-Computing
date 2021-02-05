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

            PrintExample1();
            PrintExample2();
            PrintExample3();
            PrintExample4();
            PrintExample5();
            PrintExample6();
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

        private static void PrintExample1()
        {
            Console.WriteLine("EXAMPLE 1");
            SimpleDomain d1 = new SimpleDomain(0, 5);
            PrintDomain(d1, "sd: ");
            
            SimpleDomain d2 = new SimpleDomain(0, 3);
            PrintDomain(d2, "sd: ");

            IDomain d3 = IDomain.Combine(d1, d2);
            PrintDomain(d3, "cd: ");

            Console.WriteLine($"({d3.ElementForIndex(0)})");
            Console.WriteLine($"({d3.ElementForIndex(5)})");
            Console.WriteLine($"({d3.ElementForIndex(13)})");
            Console.WriteLine(d3.IndexOfElement(DomainElement.Of(4, 1)));
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }

        private static void PrintExample2()
        {
            Console.WriteLine("EXAMPLE 2");
            IDomain d = IDomain.IntRange(0, 11);
            IFuzzySet set1 = new MutableFuzzySet(d)
                .Set(DomainElement.Of(0), 1.0)
                .Set(DomainElement.Of(1), 0.8)
                .Set(DomainElement.Of(2), 0.6)
                .Set(DomainElement.Of(3), 0.4)
                .Set(DomainElement.Of(4), 0.2);

            PrintFuzzySet(set1, "Set1: ");
            
            IDomain d2 = IDomain.IntRange(-5, 6);
            IFuzzySet set2 = new CalculatedFuzzySet(d2,
                StandardFuzzySets.LambdaFunction(d2.IndexOfElement(DomainElement.Of(-4)),
                    d2.IndexOfElement(DomainElement.Of(0)),
                    d2.IndexOfElement(DomainElement.Of(4))));
            PrintFuzzySet(set2, "Set2: ");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }

        private static void PrintExample3()
        {
            Console.WriteLine("EXAMPLE 3");
            IDomain d = IDomain.IntRange(0, 11);
            
            IFuzzySet set1 = new MutableFuzzySet(d).Set(DomainElement.Of(0), 1.0)
                .Set(DomainElement.Of(1), 0.8)
                .Set(DomainElement.Of(2), 0.6)
                .Set(DomainElement.Of(3), 0.4)
                .Set(DomainElement.Of(4), 0.2);
            
            PrintFuzzySet(set1, "Set1: ");

            IFuzzySet notSet1 = Operations.UnaryOperation(set1, Operations.ZadehNot());
            PrintFuzzySet(notSet1, "notSet1: ");

            IFuzzySet union = Operations.BinaryOperation(set1, notSet1, Operations.ZadehOr());
            PrintFuzzySet(union, "Set1 U notSet1: ");

            IFuzzySet hinters = Operations.BinaryOperation(set1, notSet1, Operations.HamacherTNorm(1.0));
            PrintFuzzySet(hinters, "Set1 Intersection with notSet1 using parameterised Hamacher T norm with parameter 1.0: ");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }

        private static void PrintExample4()
        {
            Console.WriteLine("EXAMPLE 4");
            IDomain u = IDomain.IntRange(1, 6); // {1,2,3,4,5}
            IDomain u2 = IDomain.Combine(u, u);
            IFuzzySet r1 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1,1), 1)
                .Set(DomainElement.Of(2,2), 1)
                .Set(DomainElement.Of(3,3), 1)
                .Set(DomainElement.Of(4,4), 1)
                .Set(DomainElement.Of(5,5), 1)
                .Set(DomainElement.Of(3,1), 0.5)
                .Set(DomainElement.Of(1,3), 0.5);
            
            IFuzzySet r2 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1,1), 1)
                .Set(DomainElement.Of(2,2), 1)
                .Set(DomainElement.Of(3,3), 1)
                .Set(DomainElement.Of(4,4), 1)
                .Set(DomainElement.Of(5,5), 1)
                .Set(DomainElement.Of(3,1), 0.5)
                .Set(DomainElement.Of(1,3), 0.1);
            
            IFuzzySet r3 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1,1), 1)
                .Set(DomainElement.Of(2,2), 1)
                .Set(DomainElement.Of(3,3), 0.3)
                .Set(DomainElement.Of(4,4), 1)
                .Set(DomainElement.Of(5,5), 1)
                .Set(DomainElement.Of(1,2), 0.6)
                .Set(DomainElement.Of(2,1), 0.6)
                .Set(DomainElement.Of(2,3), 0.7)
                .Set(DomainElement.Of(3,2), 0.7)
                .Set(DomainElement.Of(3,1), 0.5)
                .Set(DomainElement.Of(1,3), 0.5);
            
            IFuzzySet r4 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1,1), 1)
                .Set(DomainElement.Of(2,2), 1)
                .Set(DomainElement.Of(3,3), 1)
                .Set(DomainElement.Of(4,4), 1)
                .Set(DomainElement.Of(5,5), 1)
                .Set(DomainElement.Of(1,2), 0.4)
                .Set(DomainElement.Of(2,1), 0.4)
                .Set(DomainElement.Of(2,3), 0.5)
                .Set(DomainElement.Of(3,2), 0.5)
                .Set(DomainElement.Of(1,3), 0.4)
                .Set(DomainElement.Of(3,1), 0.4);

            var test1 = Relations.IsUTimesURelation(r1);
            Console.WriteLine($"r1 je definiran nad UxU? {test1}");
            
            var test2 = Relations.IsSymmetric(r1);
            Console.WriteLine($"r1 je simetrična? {test2}");
            
            var test3 = Relations.IsSymmetric(r2);
            Console.WriteLine($"r2 je simetrična? {test3}");
            
            var test4 = Relations.IsReflexive(r1);
            Console.WriteLine($"r1 je refleksivna? {test4}");
            
            var test5 = Relations.IsReflexive(r3);
            Console.WriteLine($"r3 je refleksivna? {test5}");
            
            var test6 = Relations.IsMaxMinTransitive(r3);
            Console.WriteLine($"r3 je max-min tranzitivna? {test6}");
            
            var test7 = Relations.IsMaxMinTransitive(r4);
            Console.WriteLine($"r4 je max-min tranzitivna? {test7}");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }

        private static void PrintExample5()
        {
            Console.WriteLine("EXAMPLE 5");
            IDomain u1 = IDomain.IntRange(1, 5); // {1,2,3,4}
            IDomain u2 = IDomain.IntRange(1, 4); // {1,2,3}
            IDomain u3 = IDomain.IntRange(1, 5); // {1,2,3,4}
            IFuzzySet r1 = new MutableFuzzySet(IDomain.Combine(u1, u2))
                .Set(DomainElement.Of(1,1), 0.3)
                .Set(DomainElement.Of(1,2), 1)
                .Set(DomainElement.Of(3,3), 0.5)
                .Set(DomainElement.Of(4,3), 0.5);
            IFuzzySet r2 = new MutableFuzzySet(IDomain.Combine(u2, u3))
                .Set(DomainElement.Of(1,1), 1)
                .Set(DomainElement.Of(2,1), 0.5)
                .Set(DomainElement.Of(2,2), 0.7)
                .Set(DomainElement.Of(3,3), 1)
                .Set(DomainElement.Of(3,4), 0.4);
            IFuzzySet r1r2 = Relations.CompositionOfBinaryRelations(r1, r2)!;

            foreach (var element in r1r2.GetDomain()) Console.WriteLine($"mu({element}) = {r1r2.GetValueAt(element)}");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }

        private static void PrintExample6()
        {
            Console.WriteLine("EXAMPLE 6");
            IDomain uFinal = IDomain.IntRange(1, 5); // {1,2,3,4}
            IFuzzySet r = new MutableFuzzySet(IDomain.Combine(uFinal, uFinal))
                .Set(DomainElement.Of(1,1), 1)
                .Set(DomainElement.Of(2,2), 1)
                .Set(DomainElement.Of(3,3), 1)
                .Set(DomainElement.Of(4,4), 1)
                .Set(DomainElement.Of(1,2), 0.3)
                .Set(DomainElement.Of(2,1), 0.3)
                .Set(DomainElement.Of(2,3), 0.5)
                .Set(DomainElement.Of(3,2), 0.5)
                .Set(DomainElement.Of(3,4), 0.2)
                .Set(DomainElement.Of(4,3), 0.2);
            IFuzzySet r2 = r;

            Console.WriteLine($"Pocetna relacija je neizrazita relacija ekvivalencija? {Relations.IsFuzzyEquivalence(r2)}");
            Console.WriteLine();

            for (var i = 0; i < 3; i++)
            {
                r2 = Relations.CompositionOfBinaryRelations(r2, r)!;

                Console.WriteLine($"Broj odradenih kompozicija je: {i + 1}. Relacija je: ");

                foreach (var element in r2.GetDomain())
                {
                    Console.WriteLine($"mu({element}) = {r2.GetValueAt(element)}");
                }

                Console.WriteLine($"Ova relacija je neizrazita relacija ekvivalencije? {Relations.IsFuzzyEquivalence(r2)}");
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }
    }
}