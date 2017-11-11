using System;
using System.Collections.Generic;

namespace Markdown
{
    public class MarkdownProcessor
    {
        private List<IHandler> Handlers { get; set; } 

        public void Process(char c)
        {
            foreach (var handler in Handlers)
            {
                handler.Handle(c);
            }
        }

        public string GetHtml()
        {
            return "";
        }

        public void RegisterHandler(IHandler handler)
        {
            Handlers.Add(handler);
        }
    }
}