using System.Web;
using System.Web.Optimization;

namespace StratusAccounting
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/plugins").IncludeDirectory("~/Scripts/plugins/bootstrap/css", "*.css", false).
                Include(
                "~/Scripts/plugins/uniform/css/uniform.default.css",
                "~/Content/css/style-metro.css",
                "~/Content/css/style.css",
                "~/Content/css/style-responsive.css",
                "~/Content/css/themes/default.css",
                "~/Scripts/plugins/select2/select2_metro.css",
                "~/Scripts/plugins/font-awesome/css/font-awesome.css",
                "~/Content/css/Validator.css"
                )
                );

            //pages bundle config - login
            bundles.Add(new StyleBundle("~/bundle/login").Include("~/Content/css/pages/login.css"));
            // dashboard
            bundles.Add(new StyleBundle("~/bundle/Dashboard").Include(
                "~/Scripts/plugins/data-tables/DT_bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/bundle/Datepicker").Include("~/Content/themes/base/jquery-ui.css"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/plugins/jquery-migrate-1.2.1.js",
                        "~/Scripts/plugins/jquery-ui/jquery-ui-1.10.1.custom.js",
                       "~/Scripts/plugins/bootstrap/js/bootstrap.js",
                        "~/Scripts/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.js",
                        "~/Scripts/plugins/jquery-slimscroll/jquery.slimscroll.js",
                        "~/Scripts/plugins/jquery.blockui.min.js",
                        "~/Scripts/plugins/jquery.cookie.js",
                        "~/Scripts/plugins/uniform/jquery.uniform.js",
                        "~/Scripts/plugins/jquery-validation/dist/jquery.validate.js",
                        "~/Scripts/plugins/select2/select2.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"
                       ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //           "~/Scripts/jquery.validate.unobtrusive.js",
            //           "~/Scripts/jquery.validate.js"));

            //page level scripts
            //login
            bundles.Add(new ScriptBundle("~/bundle/Script/login").Include(
                "~/Scripts/scripts/app.js",
                "~/Scripts/scripts/login.js"
                ));

            //dashboard
            bundles.Add(new ScriptBundle("~/bundle/Script/Dashboard").Include(
                "~/Scripts/scripts/app.js",
                "~/Scripts/scripts/form-components.js",
                 "~/Scripts/plugins/select2/select2.js",
                "~/Scripts/scripts/table-advanced.js"
                ));
        }
    }
}