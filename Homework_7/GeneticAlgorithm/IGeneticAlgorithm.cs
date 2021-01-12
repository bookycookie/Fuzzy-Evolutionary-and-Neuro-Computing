using System.Collections;
using Homework_7.Selection;

namespace Homework_7.GeneticAlgorithm
{
    public interface IGeneticAlgorithm<T> where T : IEnumerable
    {
        public Individual FindBestIndividual(bool speedRun, ISelection selection, double epsilon = 10E-7);
    }
}