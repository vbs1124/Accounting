using System.Web;
using System.Web.Optimization;

namespace Vserv.Accounting.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // **************Section For ScriptBundle START **************
            bundles.UseCdn = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/bootbox.js")
                .Include("~/Scripts/respond.js")
                .Include("~/Scripts/bootstrap-select.js")
                .Include("~/scripts/bootstrap-datepicker.js")
                .Include("~/scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app").Include("~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/Scripts/base")
                .Include("~/Scripts/base/metisMenu.js")
                .Include("~/Scripts/base/raphael.js"));

            bundles.Add(new ScriptBundle("~/Scripts/toastr").Include("~/Scripts/toastr.js"));
            bundles.Add(new ScriptBundle("~/Scripts/moment").Include("~/Scripts/moment.js"));
            bundles.Add(new ScriptBundle("~/Scripts/views/employee").Include("~/Scripts/Views/employee.js"));
            bundles.Add(new ScriptBundle("~/Scripts/views/designation").Include("~/Scripts/Views/designation.js"));

            bundles.Add(new ScriptBundle("~/Scripts/DataTables")
                .Include("~/Scripts/DataTables/jquery.dataTables.js")
                .Include("~/Scripts/DataTables/dataTables.bootstrap.js"));


            bundles.Add(new ScriptBundle("~/Scripts/morris").Include("~/Scripts/base/morris.js").Include("~/Scripts/base/morris-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/places").Include("~/Scripts/base/google-places.js"));
            bundles.Add(new ScriptBundle("~/bundles/geocomplete").Include("~/Scripts/base/jquery.geocomplete.js"));


            bundles.Add(new ScriptBundle("~/scripts/app/angular")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/loading-bar.js")
                .Include("~/Scripts/angular-touch.js")
                .Include("~/Scripts/angular-animate.js")
                //.Include("~/Scripts/angular-ui/ui-bootstrap.js")
                .Include("~/Scripts/ui-bootstrap.js")
                .Include("~/Scripts/ui-grid.js")
                .Include("~/Scripts/xeditable.js")
                .Include("~/Scripts/App/app.js")
                .IncludeDirectory("~/Scripts/App/", "*.js", true)
                );
            // **************Section For ScriptBundle END **************

            // **************Section For StyleBundle START **************

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Styles/bootstrap")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-overrides.css")
                .Include("~/Content/bootstrap-select.css")
                .Include("~/Content/bootstrap-responsive.css")
                .Include("~/Content/bootstrap-datepicker.css")
                .Include("~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/toastr").Include("~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/DataTables")
                .Include("~/font-awesome/css/font-awesome.css")
                .Include("~/Content/DataTables/css/dataTables.bootstrap.css")
                .Include("~/Content/DataTables/css/dataTables.responsive.css"));

            bundles.Add(new StyleBundle("~/Content/base")
            .Include("~/Content/base/metisMenu.min.css")
            .Include("~/Content/base/timeline.css")
            .Include("~/Content/base/morris.css"));


            bundles.Add(new StyleBundle("~/content/angular")
            .Include("~/Content/ui-grid.css")
            .Include("~/Content/loading-bar.css")
            .Include("~/Content/xeditable.css")
            );

            // **************Section For StyleBundle  END **************
        }
    }
}
