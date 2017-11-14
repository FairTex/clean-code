using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Markdown
{
    public class Handler : IHandler
    {
        public string Handle(string markdown)
        {
            throw new NotImplementedException();
        }

        public string RemoveScreening(string input, string sequence)
        {
            var regex = new Regex(@"\\" + sequence);
            return regex.Replace(input, sequence);
        }
            
        public int[] GetIndexesForSplit(string line, Func<string, int, bool> isCorrectStart, Func<string, int, bool> isCorrectFinish)
        {
            var splitIndexes = new List<int>();
            var start = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (!start && isCorrectStart(line, i))
                {
                    splitIndexes.Add(i);
                    start = true;
                }
                if (start && isCorrectFinish(line, i))
                {
                    splitIndexes.Add(i + 1);
                    start = false;
                }
            }
            return splitIndexes.ToArray();
        }

        public string[] SplitLineOnIndexes(string line, int[] indexes)
        {
            var result = new List<string>();
            if (indexes.Length > 0)
            {
                result.Add(line.Substring(0, indexes[0]));
                for (var i = 1; i < indexes.Length; i++)
                {
                    var from = indexes[i - 1];
                    var length = indexes[i] - from;
                    result.Add(line.Substring(from, length));
                }
                result.Add(line.Substring(indexes[indexes.Length - 1]));
            }
            else
            {
                result.Add(line);
            }
            return result.ToArray();
        }

    }
}