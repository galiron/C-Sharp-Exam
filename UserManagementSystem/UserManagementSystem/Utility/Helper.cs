using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Utility
{
    public static class Helper
    {
        // this method is not written by me but taken from https://stackoverflow.com/questions/1857513/get-substring-everything-before-certain-char
        public static string GetUntilOrEmpty(this string text, string stopAt = ",")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }
    }
}
