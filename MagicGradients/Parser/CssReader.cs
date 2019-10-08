using System;

namespace MagicGradients.Parser
{
    public class CssReader
    {
        private readonly string[] _tokens;
        private int _cursor;

        public bool CanRead => _cursor < _tokens.Length;

        public bool HasMoreElements => _cursor + 1 < _tokens.Length;

        public CssReader(string css)
        {
            _tokens = css
                .Replace("\r\n", "")
                .Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public CssReader(string css, char[] separator)
        {
            _tokens = css
                .Replace("\r\n", "")
                .Split(separator, StringSplitOptions.RemoveEmptyEntries);
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
            if (HasMoreElements)
            {
                MoveNext();
                return Read();
            }

            return string.Empty;
        }
    }
}
