using System.Collections.Generic;
using System.Linq;

namespace Gmtl.HandyLib
{
    public static class HLFileContentHelper
    {
        private static readonly ICollection<KeyValuePair<string, string>> _contentTypeMappings =
            new List<KeyValuePair<string, string>>
            {
            new KeyValuePair<string, string>("pdf", "application/pdf"),
            new KeyValuePair<string, string>("png", "image/png"),
            new KeyValuePair<string, string>("jpg", "image/jpg"),
            new KeyValuePair<string, string>("jpeg", "image/jpeg"),
            new KeyValuePair<string, string>("webp", "image/webp"),
            new KeyValuePair<string, string>("gif", "image/gif"),
            new KeyValuePair<string, string>("svg", "image/svg+xml"),
            new KeyValuePair<string, string>("mp4", "video/mp4"),
            new KeyValuePair<string, string>("css", "text/css"),
            new KeyValuePair<string, string>("woff", "application/font-woff"),
            new KeyValuePair<string, string>("doc", "application/msword"),
            new KeyValuePair<string, string>("dot", "application/msword"),
            new KeyValuePair<string, string>(
                "docx",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            ),
            new KeyValuePair<string, string>(
                "dotx",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.template"
            ),
            new KeyValuePair<string, string>("xls", "application/vnd.ms-excel"),
            new KeyValuePair<string, string>(
                "xlsx",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            ),
            new KeyValuePair<string, string>("ppt", "application/vnd.ms-powerpoint"),
            new KeyValuePair<string, string>(
                "pptx",
                "application/vnd.openxmlformats-officedocument.presentationml.presentation"
            ),
            new KeyValuePair<string, string>("odt", "application/vnd.oasis.opendocument.text"),
            new KeyValuePair<string, string>(
                "ods",
                "application/vnd.oasis.opendocument.spreadsheet"
            ),
            new KeyValuePair<string, string>(
                "odp",
                "application/vnd.oasis.opendocument.presentation"
            )};

        public static string GetFileExtFromContentType(this string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType)) return null;

            var mapping = _contentTypeMappings.FirstOrDefault(kv => contentType.ToLowerInvariant() == kv.Value);

            return !mapping.Equals(default(KeyValuePair<string, string>)) ? mapping.Key : null;
        }

        public static string GetContentTypeFromFilename(this string filename)
        {
            if (string.IsNullOrWhiteSpace(filename)) return null;

            var mapping = _contentTypeMappings.SingleOrDefault(kv => filename.EndsWith(kv.Key));

            return !mapping.Equals(default(KeyValuePair<string, string>))
                ? mapping.Value
                : "application/octet-stream";
        }
    }

}
