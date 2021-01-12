using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Homework_7
{
    public class Dataset
    {
        private List<Sample> Samples { get; set; }

        private const string Root =
            "C:/git/Fuzzy-Evolutionary-and-Neuro-Computing/Homework_7/Files/Dataset/zad7-dataset.txt";

        public IEnumerator<Sample> GetEnumerator() => Samples.GetEnumerator();
        public Dataset() => Samples = ParseFile();

        private static List<Sample> ParseFile() => (from row in File.ReadAllLines(Root)
            select row.Split('\t')
            into split
            let x = double.Parse(split[0])
            let y = double.Parse(split[1])
            let a = double.Parse(split[2])
            let b = double.Parse(split[3])
            let c = double.Parse(split[4])
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