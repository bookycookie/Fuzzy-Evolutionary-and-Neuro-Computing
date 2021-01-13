
namespace Homework_7.GA
{
    public class Individual
    {
        public Individual(int dimension) => Representation = new double[dimension];
        public double[] Representation { get; set; }
        
        public double Fitness { get; set; } = double.NegativeInfinity;
        public override string ToString() => "Parameters: [" + string.Join(" ", Representation) + $"]\nFitness: {Fitness:F4}";
    }
}