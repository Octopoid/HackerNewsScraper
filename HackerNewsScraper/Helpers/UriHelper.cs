namespace HackerNewsScraper.Helpers
{
    using System;
    using System.Collections.Specialized;
    using System.Web;

    public static class UriHelper
    {
        public static string SetQuerystringParameter(string uri, string key, object value)
        {
            return UriHelper.SetQuerystringParameter(uri, key, value.ToString());
        }

        public static string SetQuerystringParameter(string uri, string key, string value)
        {
            UriBuilder uriBuilder = new UriBuilder(uri);

            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
            nameValueCollection[key] = value;

            uriBuilder.Query = nameValueCollection.ToString();

            return uriBuilder.ToString();
        }

        public static string ValidateUri(string uri, string sourceUri)
        {
            if (Uri.TryCreate(uri, UriKind.Absolute, out Uri absoluteUri))
            {
                return uri;
            }

            if (Uri.TryCreate(uri, UriKind.Relative, out Uri relativeUri))
            {
                string baseUri = new Uri(sourceUri).GetLeftPart(UriPartial.Authority);
                return $"{baseUri}/{relativeUri}";
            }

            // Fallback URI
            return sourceUri;
        }

    }
}
