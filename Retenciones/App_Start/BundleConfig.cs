using System.Web;
using System.Web.Optimization;

namespace Retenciones
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.min.js", "~/Scripts/jquery-ui-timepicker-addon.js", "~/Scripts/datepicker.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryuicss").Include(
                        "~/Content/jquery-ui.min.css", "~/Content/jquery-ui-timepicker-addon.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/gestioncreate").Include(
                    "~/Scripts/GestionCreate.js"));

            bundles.Add(new ScriptBundle("~/bundles/gestionprueba").Include(
                    "~/Scripts/GestionPrueba.js"));

            bundles.Add(new ScriptBundle("~/bundles/gestioncarruselesedit").Include(
                    "~/Scripts/GestionCarruselesEdit.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard2").Include(
                    "~/Scripts/Dashboard2.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/multiselect").Include(
                        "~/Scripts/bootstrap-multiselect.js"));

            bundles.Add(new StyleBundle("~/Content/multiselectcss").Include(
                        "~/Content/bootstrap-multiselect.css"));

            bundles.Add(new ScriptBundle("~/bundles/accountindex").Include(
                        "~/Scripts/AccountIndex.js"));
        }
    }
}
