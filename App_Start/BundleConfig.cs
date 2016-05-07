using System.Web;
using System.Web.Optimization;

namespace MemberLoginSSRS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.signalR-{version}.js",
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                        "~/Scripts/angular-drag-and-drop-lists/angular-drag-and-drop-lists.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
            "~/Scripts/chosen.jquery.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/offcanvas.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                        "~/Scripts/d3/d3.js",
                        "~/Scripts/d3/nv.d3.js",
                        "~/Scripts/angular-chart/chart.js",
                        "~/Scripts/angular-chart/angular-chart.js",
                        "~/Scripts/d3/angular-nvd3.js",
                        "~/Scripts/angular-toastr.tpls.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/appjs").Include(
                        "~/app/app.js",
                        "~/app/services.js",
                        "~/app/directives.js",
                        "~/app/controllers.js"
                        ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/chosen.css",
                      "~/Content/site.css",
                      "~/Content/offcanvas.css",
                      "~/Content/nv.d3.css",
                      "~/Content/angular-chart.css",
                      "~/Content/angular-drag-and-drop-lists/demo-framework.css",
                      "~/Content/angular-drag-and-drop-lists/nested.css",
                      "~/Content/angular-toastr.css"
                      ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
#if DEBUG

            BundleTable.EnableOptimizations = false;

#else

            BundleTable.EnableOptimizations = true;

#endif

        }
    }
}
