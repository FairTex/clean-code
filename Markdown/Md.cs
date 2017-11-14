using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Markdown
{
    public class Md
    {
        public string RenderToHtml(string markdown)
        {
            var emHandler = new EmHandler();
            var strongHandler = new StrongHandler();

            var splitedLine = emHandler.Split(markdown);
            var html = splitedLine.Select(line =>
            {
                if (line.StartsWith("_") && line.EndsWith("_"))
                {
                    var emHtml = "<em>" + line.Trim('_') + "</em>";
                    return emHandler.RemoveScreening(emHtml);
                }
                return strongHandler.Handle(line);
            }).ToArray();
            return String.Join("", html);
        }
    }


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