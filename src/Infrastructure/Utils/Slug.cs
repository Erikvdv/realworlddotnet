using System.Text.RegularExpressions;

namespace Realworlddotnet.Infrastructure.Utils
{
    // https://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
    public static class Slug
    {
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.ToLowerInvariant();

            // invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", string.Empty);

            // convert multiple spaces into one space
            str = Regex.Replace(str, @"\s+", " ").Trim();

            // cut and trim
            str = str[..(str.Length <= 45 ? str.Length : 45)].Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens
            return str;
        }
    }
}
