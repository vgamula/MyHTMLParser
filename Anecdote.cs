using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHtmlParser
{
    class Anecdote
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
