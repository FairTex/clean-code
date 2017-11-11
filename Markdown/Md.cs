using NUnit.Framework;

namespace Markdown
{
	public class Md
	{
		public string RenderToHtml(string markdown)
		{
            var processor = new MarkdownProcessor();
            for (int i = 0; i < markdown.Length; i++)
            {
                processor.Process(markdown[i]);
            }
            return processor.GetHtml();
		}
	}

    public class MarkdownProcessor
    {
        public void Process(char c)
        {

        }

        public string GetHtml()
        {
            return "";
        }

        public void RegisterHandler()
        {

        }
    }



	[TestFixture]
	public class Md_ShouldRender
	{
	}
}