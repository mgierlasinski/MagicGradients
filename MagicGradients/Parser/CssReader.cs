using System;

namespace MagicGradients.Parser
{
    public class CssReader
    {
        private readonly string[] _tokens;
        private int _cursor;

        public bool CanRead => _cursor < _tokens.Length;

        public CssReader(string css)
        {
            _tokens = css
                .Replace("\r\n", "")
                .Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string Read()
        {
            return _tokens[_cursor];
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
