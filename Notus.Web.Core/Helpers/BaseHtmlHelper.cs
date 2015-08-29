using System.Web.Mvc;

namespace Notus.Web.Core.Helpers
{
    public class BaseHtmlHelper
    {
        public BaseHtmlHelper(HtmlHelper html, UrlHelper url)
        {
            Html = html;
            Url = url;
        }

        protected HtmlHelper Html { get; }
        protected UrlHelper Url { get; }
    }
}