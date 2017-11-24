using System;
using System.Linq;

namespace Markdown
{
    public class StrongHandler : Handler
    {
        public string Handle(string markdown)
        {
            var lines = Split(markdown);
            var res = Replace(lines);
            return RemoveScreening(res);
        }

        public string RemoveScreening(string input) => base.RemoveScreening(input, "__");

        public string Replace(string[] lines)
        {
            var tagLines = lines.Select(el =>
            {
                if (el.StartsWith("__") && el.EndsWith("__"))
                    return "<strong>" + el.Trim(new[] {'_', '_'}) + "</strong>";
                return el;
            }).ToArray();
            var res = String.Join("", tagLines);
            return res;
        }

        public int[] GetIndexesForSplit(string line) => base.GetIndexesForSplit(line, IsCorrectStart, IsCorrectFinish);

        public string[] Split(string line)
        {
            var splitIndexes = GetIndexesForSplit(line, IsCorrectStart, IsCorrectFinish);
            return SplitLineOnIndexes(line, splitIndexes.ToArray());
        }

        public bool IsCorrectStart(string line, int position)
        {
            var isCorrect = line[position] == '_';
            if (position > 0)
            {
                isCorrect = isCorrect && line[position - 1] == ' ';
            }
            isCorrect = isCorrect && position < line.Length - 4 && line[position + 1] == '_' &&
                        line[position + 2] != ' ' && line[position + 2] != '_';

            return isCorrect;
        }

        public bool IsCorrectFinish(string line, int position)
        {
            var isCorrect = line[position] == '_';
            isCorrect = isCorrect && position > 3 && line[position - 1] == '_' &&
                        line[position - 2] != ' ' && line[position - 2] != '_';

            if (position < line.Length - 1)
            {
                isCorrect = isCorrect && line[position + 1] == ' ';
            }
            return isCorrect;
        }
    }
}