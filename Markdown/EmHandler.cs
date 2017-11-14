using System;
using System.Linq;
using NUnit.Framework.Constraints;

namespace Markdown
{
    public class EmHandler : Handler
    {
        // _abs_ -> <em>abs</em>  OK
        // __abs__ -> __abs__
        // ab_23_1 -> ab_23_1  OK
        // ab \_dd\_ -> ab _dd_
        // _abc __d__ fg_ -> <em>abc __d__ fg</em>
        // fr_ -> fr_
        // 
        public string Handle(string markdown)
        {
            var lines = Split(markdown);
            var res = Replace(lines);
            return RemoveScreening(res);
        }

        public string Replace(string[] lines)
        {
            var tagLines = lines.Select(el =>
            {
                if (el.StartsWith("_") && el.EndsWith("_"))
                    return "<em>" + el.Trim('_') + "</em>";
                return el;
            }).ToArray();
            var res = String.Join("", tagLines);
            return res;
        }

        public int[] GetIndexesForSplit(string line) => base.GetIndexesForSplit(line, IsCorrectStart, IsCorrectFinish);

        public string RemoveScreening(string input) => base.RemoveScreening(input, "_");


        public string[] Split(string line)
        {
            var splitIndexes = GetIndexesForSplit(line);
            return SplitLineOnIndexes(line, splitIndexes.ToArray());
        }

        public bool IsCorrectStart(string line, int position)
        {
            var isCorrect = line[position] == '_';
            if (position > 0)
            {
                isCorrect = isCorrect && line[position - 1] == ' ';
            }
            isCorrect = isCorrect && 
                position < line.Length - 1 &&
                line[position + 1] != '_' &&
                line[position + 1] != ' ';
            return isCorrect;
        }

        public bool IsCorrectFinish(string line, int position)
        {
            var isCorrect = line[position] == '_';
            isCorrect = isCorrect && position > 0  && line[position - 1] != '_' && line[position - 1] != ' ';
            if (position < line.Length - 1)
            {
                isCorrect = isCorrect && line[position + 1] == ' ';
            }
            return isCorrect;
        }
    }
}