using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class StrongHandler_Should
    {
        [TestCase("line __good__", 5, true)]
        [TestCase("line ___bad__", 5, false)]
        public void IsCorrectStart_Correct(string line, int position, bool answer)
        {
            var h = new StrongHandler();
            h.IsCorrectStart(line, position).Should().Be(answer);
        }

        [TestCase("line __good__", 12, true)]
        [TestCase("line __bad___", 12, false)]
        public void IsCorrectEnd_Correct(string line, int position, bool answer)
        {
            var h = new StrongHandler();
            h.IsCorrectFinish(line, position).Should().Be(answer);
        }

        [TestCase("line __good__", "line <strong>good</strong>")]
        [TestCase("line ___bad__", "line ___bad__")]
        [TestCase(@"line \__bad__", @"line __bad__")]
        public void StrongHandler_ShouldBeCorrect(string line, string result)
        {
            var h = new StrongHandler();
            h.Handle(line).ShouldBeEquivalentTo(result);
        }
    }

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