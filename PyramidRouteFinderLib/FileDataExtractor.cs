using System.Collections.Generic;

namespace PyramidRouteFinderLib
{
    internal class FileDataExtractor : IFileDataExtractor
    {
        public IEnumerable<string> ExtractLines(string filePath)
        {
            return System.IO.File.ReadAllLines(filePath);
        }
    }
}