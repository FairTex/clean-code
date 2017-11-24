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

            h.IsCorrectStart(line, position)
                .Should().Be(answer);
        }

        [TestCase("line __good__", 12, true)]
        [TestCase("line __bad___", 12, false)]
        public void IsCorrectEnd_Correct(string line, int position, bool answer)
        {
            var h = new StrongHandler();

            h.IsCorrectFinish(line, position)
                .Should().Be(answer);
        }

        [TestCase("line __good__", "line <strong>good</strong>")]
        [TestCase("line ___bad__", "line ___bad__")]
        [TestCase(@"line \__bad__", @"line __bad__")]
        public void StrongHandler_ShouldBeCorrect(string line, string result)
        {
            var h = new StrongHandler();

            h.Handle(line)
                .ShouldBeEquivalentTo(result);
        }
    }
}