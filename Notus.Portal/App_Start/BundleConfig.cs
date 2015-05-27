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
                "~/Content/js/bootstrap.js",
                "~/Content/js/gsdk-checkbox.js",
                "~/Content/js/gsdk-radio.js",
                "~/Content/js/gsdk-bootstrapswitch.js",
                "~/Content/js/get-shit-done.js",
                "~/Content/js/fullcalendar.js",
                "~/Content/js/custom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/get-shit-done.css",
                "~/Content/css/ct-navbar.css",
                "~/Content/css/fullcalendar.css",
                "~/Content/css/fullcalendar.print.css",
                //Font awesome css
                "~/Content/css/font-awesome.css",
                "~/Content/css/select.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Content/js/jquery-2.1.4.min.js",
                "~/Content/js/jquery-ui.min.js",
                "~/Content/js/d3.min.js",
                "~/Content/js/topojson.min.js",
                "~/Scripts/charts/datamaps.all.js",
               "~/Content/js/angular.min.js",
               "~/Content/js/angular-sanitize.min.js",
               "~/Content/js/angular-animate.min.js",
               "~/Content/js/angular-sanitize.min.js",
               "~/Content/js/angular-route.min.js",
               "~/Content/js/select.min.js",
               "~/Content/js/angular-ui-router.min.js",
               "~/Content/js/slider.js",
                "~/Scripts/App/app.js",
                "~/Scripts/App/controllers.js"
                ));
        }
    }
}