using System;
using System.Collections.Generic;
using System.Text;

namespace Gmtl.HandyLib.Models.SEO
{
    /// <summary>
    /// Interface for SEO properties.
    /// </summary>
    public interface IWithSEO
    {
        /// <summary>
        /// The meta title of the page.
        /// <para>Max Length: 60 characters.</para>
        /// <para>Best Practice: Use a concise and descriptive title that includes primary keywords.</para>
        /// </summary>
        string MetaTitle { get; set; }

        /// <summary>
        /// The meta description of the page.
        /// <para>Max Length: 160 characters.</para>
        /// <para>Best Practice: Provide a brief summary of the page content with a call-to-action if applicable.</para>
        /// </summary>
        string MetaDescription { get; set; }

        /// <summary>
        /// The meta keywords for the page.
        /// <para>Max Length: 255 characters.</para>
        /// <para>Best Practice: Use a comma-separated list of relevant keywords. Avoid keyword stuffing.</para>
        /// </summary>
        string MetaKeywords { get; set; }

        /// <summary>
        /// The slug (URL-friendly identifier) for the page.
        /// <para>Best Practice: Use lowercase letters, hyphens instead of spaces, and avoid special characters.</para>
        /// </summary>
        string Slug { get; set; }

        /// <summary>
        /// The canonical URL for the page.
        /// <para>Best Practice: Use this to specify the preferred URL for duplicate or similar content.</para>
        /// </summary>
        string CanonicalUrl { get; set; }

        /// <summary>
        /// Indicates whether the page should not be indexed by search engines.
        /// <para>Best Practice: Set to true for pages that should remain private or are not useful for SEO.</para>
        /// </summary>
        bool NoIndex { get; set; }

        /// <summary>
        /// Indicates whether search engines should not follow links on the page.
        /// <para>Best Practice: Set to true for pages with untrusted or irrelevant links.</para>
        /// </summary>
        bool NoFollow { get; set; }
    }
}
