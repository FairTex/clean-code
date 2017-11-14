using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public class TokenReader
    {
        public int Position { get; set; } = 0;
        public string Line { get; set; }

        public TokenReader(string line)
        {
            Line = line;
        }

        public void ReadUntil(params char[] stopChars)
        {
            
        }

        public void ReadWhile(params char[] acceptableChars)
        {
            
        }
    }
}
