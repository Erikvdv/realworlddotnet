using System;
using System.Text.RegularExpressions;

namespace Realworlddotnet.Infrastructure.Utils;

// https://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
public static class Slug
{
    public static string GenerateSlug(this string phrase)
    {
        var shortGuid = CreateShortGuid();
        var slug = CreateSlug(phrase);
        return slug + "-" + shortGuid;
    }

    private static string CreateSlug(string input)
    {
        var str = input.ToLowerInvariant();

        // invalid chars
        str = Regex.Replace(str, @"[^a-z0-9\s-]", string.Empty);

        // convert multiple spaces into one space
        str = Regex.Replace(str, @"\s+", " ").Trim();

        // cut and trim
        str = str[..(str.Length <= 45 ? str.Length : 45)].Trim();
        str = Regex.Replace(str, @"\s", "-"); // hyphens
        return str;
    }

    public static string CreateShortGuid()
    {
        var guid = Guid.NewGuid();
        var enc = Convert.ToBase64String(guid.ToByteArray());
        enc = enc.Replace("/", "_");
        enc = enc.Replace("+", "-");
        return enc.Substring(0, 22);
    }
}
