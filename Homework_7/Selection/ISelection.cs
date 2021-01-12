using System.Collections.Generic;

namespace Homework_7.Selection
{
    public interface ISelection
    {
        public Individual Select(List<Individual> population);
        
    }
}