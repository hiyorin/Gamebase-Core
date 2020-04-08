using System.Text;

namespace Gamebase
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendLineFormat(this StringBuilder self, string format, params object[] args)
        {
            return self.AppendLine(string.Format(format, args));
        }
    }
}