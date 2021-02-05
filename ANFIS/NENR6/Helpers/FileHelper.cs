using System.Collections.Generic;
using System.IO;
using NENR6.ANFIS;

namespace NENR6.Helpers
{
    public static class FileHelper
    {
        public static void WriteToFile(IEnumerable<Sample> samples, string path)
        {
            using var file = new StreamWriter(path);
            foreach (var sample in samples)
                file.WriteLine(
                    $"{sample.X.ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture)} " +
                    $"{sample.Y.ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture)} " +
                    $"{sample.Z.ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture)} ");
        }
    }
}