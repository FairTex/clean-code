using NUnit.Framework;

namespace Markdown
{
    public class Md
    {
        public string RenderToHtml(string markdown)
        {
            var processor = new MarkdownProcessor();

            var emHandler = new EmHandler();
            var strongHandler = new StrongHandler();
            strongHandler.ExcludeProcessIn(emHandler);

            processor.RegisterHandler(emHandler);
            processor.RegisterHandler(strongHandler);

            for (int i = 0; i < markdown.Length; i++)
            {
                processor.Process(markdown[i]);
            }
            return processor.GetHtml();
        }
    }


    [TestFixture]
    public class Md_ShouldRender
    {
    }
}