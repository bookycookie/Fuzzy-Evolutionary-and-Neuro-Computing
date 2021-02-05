using NENR6.ANFIS;
using NENR6.Helpers;

namespace NENR6
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const int numberOfRules = 10;
            var anfis = new ANFIS.ANFIS(numberOfRules, 10_000);
            var gradSus = anfis.Run(useBatch: false, eta: 0.001, etaZ: 0.01, verbose: false);
            // var anfisBatch = new ANFIS(numberOfRules, 10_000);
            // var gradSus = anfisBatch.Run(useBatch: true, batchSize: 4, eta: 0.0001, etaZ: 0.01, verbose: false);

            // var samples = ANFIS.GenerateSamples();
            // const string root = "C:/Faks/NENR/NENR6/NENR6/Data/";
            // var testRoot = root + $"{numberOfRules}.txt";
            // var testRoot = root + "sampled.txt";
            // FileHelper.WriteToFile(samples, testRoot);

            // WriteDifferencesToFile();

        }

        public static void WriteDifferencesToFile()
        {
            const int numberOfRules = 10;
            var anfis = new ANFIS.ANFIS(numberOfRules, 10_000);
            var gradSus = anfis.Run(useBatch: false, eta: 0.001, etaZ: 0.01, verbose: false);
            // var anfisBatch = new ANFIS(numberOfRules, 10_000);
            // var gradSus = anfisBatch.Run(useBatch: true, batchSize: 4, eta: 0.0001, etaZ: 0.01, verbose: false);
            
            var samples = ANFIS.ANFIS.GenerateSamples();
            const string root = "C:/Faks/NENR/NENR6/NENR6/Data/SGD/Differences/";
            var testRoot = root + $"{numberOfRules}.txt";

            var sampleDifferences = samples;
            
            for (var i = 0; i < samples.Count; i++)
                sampleDifferences[i] = new Sample(samples[i].X, samples[i].Y, samples[i].Z - gradSus[i].Z);
            
            FileHelper.WriteToFile(sampleDifferences, testRoot);
        }

        
    }
}