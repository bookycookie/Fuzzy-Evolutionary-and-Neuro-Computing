using System.Collections.Generic;

namespace Homework_7.GA.Selection
{
    public interface ISelection
    {
        public Individual Select(List<Individual> population);
        
    }
}