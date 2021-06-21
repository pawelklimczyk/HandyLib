using System;

namespace Gmtl.HandyLib.Validators
{
    /// <summary>
    /// Validator for internet urls
    /// </summary>
    public static class HLUrlValidator
    {
        /// <summary>
        /// Validates if provided string is valid url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidUrl(string url)
        {
            Uri outUri;
            return Uri.TryCreate(url, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
