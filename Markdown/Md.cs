using System;
using System.Linq;

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
}