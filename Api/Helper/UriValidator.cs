using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Api.Helper
{
    public class UriValidator
    {
        public bool Validate(string uri, string[] validationRules)
        {
            if (string.IsNullOrEmpty(uri))
                throw new System.ArgumentException("Url cannot be null");

            var rules = validationRules.Select(x => WildcardToRegex(x));

            foreach (string rule in rules)
            {
                if (Regex.IsMatch(uri, rule, RegexOptions.IgnoreCase))
                    return true;

            }
            return false;
        }

        private string WildcardToRegex(string pattern)
        {

            return Regex.Escape(pattern).
            Replace("\\*", ".*").
            Replace("\\?", ".");
        }
    }
}