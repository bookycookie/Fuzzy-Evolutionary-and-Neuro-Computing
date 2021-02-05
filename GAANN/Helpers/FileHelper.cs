using System.Collections.Generic;
using System.IO;

namespace Homework_7.Helpers
{
    public static class FileHelper
    {
        public static void WriteToFile(List<double> x, List<double> y, string path)
        {
            using var file = new StreamWriter(path);
            for (var i = 0; i < x.Count; i++)
            {
                file.WriteLine(
                    $"{x[i].ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture)} " +
                    $"{y[i].ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture)}");
            }
                
        }
    }
}