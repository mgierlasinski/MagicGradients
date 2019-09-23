using System;

namespace MagicGradients.Parser
{
    public class CssReader
    {
        protected string[] Tokens;
        private int _cursor;

        public bool CanRead => _cursor < Tokens.Length;

        protected CssReader() { }

        public CssReader(string css)
        {
            Tokens = css
                .Replace(" ", "")
                .Replace("\r\n", "")
                .Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string Read()
        {
            return Tokens[_cursor];
        }

        public void MoveNext()
        {
            _cursor++;
        }

        public void Rollback()
        {
            _cursor--;
        }

        public string ReadNext()
        {
            MoveNext();

            if (!CanRead)
            {
                return string.Empty;
            }

            return Read();
        }
    }
}
