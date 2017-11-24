using FluentAssertions;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class EmHandler_Should
    {
        [Test]
        public void Handle_ReplaceSimpleTag()
        {
            var handler = new EmHandler();
            var markdown = "_abs_";
            var html = "<em>abs</em>";

            handler.Handle(markdown)
                .ShouldBeEquivalentTo(html);
        }

        [Test]
        public void Handle_ReplaceHard()
        {
            var handler = new EmHandler();
            var markdown = "_abs_";
            var html = "<em>abs</em>";

            handler.Handle(markdown)
                .ShouldBeEquivalentTo(html);
        }

        [Test]
        public void Split_ShouldSplit_OnUnderscore()
        {
            var handler = new EmHandler();
            var markdown = "ab _abs_ de";
            var result = new [] {"ab ", "_abs_", " de"};

            handler.Split(markdown)
                .ShouldBeEquivalentTo(result);
        }
        [Test]
        public void Split_ShouldSplit_OnUnderscore2()
        {
            var handler = new EmHandler();
            var markdown = "ab _abs __fr__ e_ de";
            var result = new[] { "ab ", "_abs __fr__ e_", " de" };

            handler.Split(markdown)
                .ShouldBeEquivalentTo(result);
        }

        [Test]
        public void SplitLineOnIndexes_ShouldSplitCorrect()
        {
            var h = new EmHandler();

            h.SplitLineOnIndexes("123 _6789_ ", new []{3, 7})
                .ShouldBeEquivalentTo(new [] {"123", " _67", "89_ "});
        }

        [Test]
        public void Split()
        {
            var h = new Handler();
            var em = new EmHandler();

            h.GetIndexesForSplit("ab_23_1", em.IsCorrectStart, em.IsCorrectFinish)
                .ShouldBeEquivalentTo(new int[0]);
        }

        [Test]
        public void Split_IsCorrect()
        {
            var h = new Handler();
            var em = new EmHandler();

            h.GetIndexesForSplit(@"ab \_dd\_ ff", em.IsCorrectStart, em.IsCorrectFinish)
                .ShouldBeEquivalentTo(new int[0]);
        }

        [Test]
        public void RemoveScreening_IsCorrect()
        {
            var h = new Handler();

            h.RemoveScreening(@"fr \_ddd\_ dd", "_")
                .ShouldBeEquivalentTo("fr _ddd_ dd");
        }

        [Test]
        public void Handle_HandleTheScreening()
        {
            var h = new EmHandler();

            h.Handle(@"fr \_ddd\_ _dd_")
                .ShouldBeEquivalentTo("fr _ddd_ <em>dd</em>");
        }

        [Test]
        public void RemoveScreening_IsCorrect2()
        {
            var h = new Handler();

            h.RemoveScreening(@"fr \__ddd\__ dd", "__")
                .ShouldBeEquivalentTo("fr __ddd__ dd");
        }
    }
}