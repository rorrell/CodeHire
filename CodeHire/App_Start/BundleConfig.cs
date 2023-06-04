using System.Web;
using System.Web.Optimization;

namespace CodeHire
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/Dependencies/jquery-{version}.js",
                        "~/Scripts/Dependenciesbootstrap.js",
                        "~/Scripts/Dependencies/bootbox.js",
                        "~/Scripts/Dependencies/Datatables/jquery.datatables.js",
                        "~/Scripts/Dependencies/Datatables/datatables.bootstrap.js",
                        "~/Scripts/Dependencies/typeahead.bundle.js",
                        "~/Scripts/Dependencies/toastr.js"
        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Dependencies/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Dependencies/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Dependencies/bootstrap.css",
                      "~/Content/Dependencies/Datatables/css/datatables.bootstrap.css",
                      "~/Content/Dependencies/typeahead.css",
                      "~/Content/Dependencies/toastr.css",
                      "~/Content/site.css"));
        }
    }
}
