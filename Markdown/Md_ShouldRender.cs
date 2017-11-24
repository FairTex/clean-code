using FluentAssertions;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class Md_ShouldRender
    {
        [Test]
        public void RenderToHtml_ShouldBeCorrect()
        {
            var md = new Md();

            md.RenderToHtml("_em_ __strong__ _not __strong__ easy_  __of _italic_ anyway__")
                .ShouldBeEquivalentTo(
                    "<em>em</em> <strong>strong</strong> <em>not __strong__ easy</em>  __of <em>italic</em> anyway__");
        }
    }
}