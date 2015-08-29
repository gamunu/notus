using System.IO.Compression;
using System.Web.Mvc;

namespace Notus.Web.Core.ActionFilters
{
    public class CompressResponseAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            var acceptEncoding = request.Headers["Accept-Encoding"];

            if (!string.IsNullOrEmpty(acceptEncoding))
            {
                acceptEncoding = acceptEncoding.ToUpperInvariant();

                var response = filterContext.HttpContext.Response;

                if (acceptEncoding.Contains("GZIP"))
                {
                    response.AppendHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("DEFLATE"))
                {
                    response.AppendHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
            }
        }

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    if (filterContext.Exception != null)
        //    {
        //        filterContext.HttpContext.Response.Filter = null;
        //    }
        //}
    }
}