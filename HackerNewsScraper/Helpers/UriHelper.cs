namespace HackerNewsScraper.Helpers
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Web;
    using Flurl;

    /// <summary>
    /// A helper class for common Uri functions. This has not been added as an extension method as it operates on strings.
    /// We're using strings rather than Uri objects to make the API easier to consume.
    /// </summary>
    public static class UriHelper
    {
        /// <summary>
        /// Add or set a querystring parameter to a provided Uri.
        /// </summary>
        /// <param name="uri">The Uri to add or set the querystring parameter to.</param>
        /// <param name="key">The querystring parameter key.</param>
        /// <param name="value">The querystring parameter value.</param>
        /// <returns>The modified Uri with the querystring applied.</returns>
        public static string SetQuerystringParameter(string uri, string key, object value)
        {
            return UriHelper.SetQuerystringParameter(uri, key, value.ToString());
        }

        /// <summary>
        /// Add or set a querystring parameter to a provided Uri.
        /// </summary>
        /// <param name="uri">The Uri to add or set the querystring parameter to.</param>
        /// <param name="key">The querystring parameter key.</param>
        /// <param name="value">The querystring parameter value.</param>
        /// <returns>The modified Uri with the querystring applied.</returns>
        public static string SetQuerystringParameter(string uri, string key, string value)
        {
            UriBuilder uriBuilder = new UriBuilder(uri);

            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
            nameValueCollection[key] = value;

            uriBuilder.Query = nameValueCollection.ToString();

            return uriBuilder.ToString();
        }

        /// <summary>
        /// Returns a boolean value representing whether the provided Uri is a valid absolute http or https Uri.
        /// </summary>
        /// <param name="uri">The Uri to check for validity.</param>
        /// <returns>A boolean value representing whether the provided Uri is valid.</returns>
        public static bool IsValidHttpUri(string uri)
        {
            if (!Url.IsValid(uri))
            {
                return false;
            }

            if (Uri.TryCreate(uri, UriKind.Absolute, out Uri absoluteUri))
            {
                if (absoluteUri.Scheme != "http" && absoluteUri.Scheme != "https")
                {
                    return false;
                }
            }

            return true;

        }

        /// <summary>
        /// Takes a given uri and attempts to make it valid. Absolute uris are returned as are, relative uris are prepended with the fallback Uri, and invalid uris are replaced with the fallback uri.
        /// </summary>
        /// <param name="uri">The uri which must be absolute.</param>
        /// <param name="fallbackUri">The fallback Uri to use for invalid Uris. This value is also used as the base domain for relative uris.</param>
        /// <returns></returns>
        public static string EnsureUriAbsolute(string uri, string fallbackUri)
        {
            if (Uri.TryCreate(uri, UriKind.Absolute, out Uri absoluteUri))
            {
                return uri;
            }

            if (Uri.TryCreate(uri, UriKind.Relative, out Uri relativeUri))
            {
                return Url.Combine(fallbackUri, uri);
            }

            // Fallback URI
            return fallbackUri;
        }
    }
}
