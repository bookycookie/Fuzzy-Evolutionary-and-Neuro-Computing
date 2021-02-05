using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Homework_7.ANN
{
    public class Dataset
    {
        private List<Sample> Samples { get; set; }

        private const string Root =
            "./Files/Dataset/zad7-dataset.txt";

        public IEnumerator<Sample> GetEnumerator() => Samples.GetEnumerator();
        public Dataset() => Samples = ParseFile();

        private static List<Sample> ParseFile() => (from row in File.ReadAllLines(Root)
            select row.Split('\t')
            into split
            let x = double.Parse(split[0], CultureInfo.InvariantCulture)
            let y = double.Parse(split[1], CultureInfo.InvariantCulture)
            let a = int.Parse(split[2], CultureInfo.InvariantCulture)
            let b = int.Parse(split[3], CultureInfo.InvariantCulture)
            let c = int.Parse(split[4], CultureInfo.InvariantCulture)
            select new Sample
            {
                X = x,
                Y = y,
                A = a,
                B = b,
                C = c
            }).ToList();

        public int DatasetCount() => Samples.Count;
    }
}