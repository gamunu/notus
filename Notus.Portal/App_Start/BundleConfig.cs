using System.Web.Optimization;

namespace Notus.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/js/jquery-1.10.2.js",
                "~/Content/js/jquery-ui-1.10.4.custom.min.js",
                "~/Content/js/bootstrap.js",
                "~/Content/js/gsdk-checkbox.js",
                "~/Content/js/gsdk-radio.js",
                "~/Content/js/gsdk-bootstrapswitch.js",
                "~/Content/js/get-shit-done.js",
                "~/Content/js/custom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/get-shit-done.css",
                "~/Content/css/pe-icon-7-stroke.css",
                "~/Content/css/ct-navbar.css",
                //Font awesome css
                "~/Content/css/font-awesome.css"));
        }
    }
}