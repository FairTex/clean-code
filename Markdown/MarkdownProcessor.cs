using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdown
{
    public class MarkdownProcessor
    {
        public MarkdownProcessor()
        {
        }
        
        public string Process(string markdown)
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
                var splitted = strongHandler.Split(line);
                var strongHtml = strongHandler.Replace(splitted);
                return strongHandler.RemoveScreening(strongHtml);
            }).ToArray();
            return String.Join("", html);
        }
        
    }
}