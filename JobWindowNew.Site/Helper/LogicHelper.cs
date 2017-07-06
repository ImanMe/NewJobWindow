using System.Linq;

namespace JobWindowNew.Site.Helper
{
    public static class LogicHelper
    {
        public static bool ContainsOneOrMore(string str, params string[] values)
        {
            if (string.IsNullOrEmpty(str) && values.Length <= 0) return false;
            return str != null && values.Any(str.Contains);
        }
    }
}