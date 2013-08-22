using System.Web;
using System.Web.Optimization;

namespace _250ml_MVC4_2
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=254725".
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bundling trotz Debug aktivieren
            //BundleTable.EnableOptimizations = true;

            // Bundle für TimePicker
            bundles.Add(
                new ScriptBundle("~/bundles/timepicker")
                    .Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery-ui-timepicker-addon.js",
                        "~/Scripts/config/timepicker-addon.js",
                        "~/Content/bootstrap/js/bootstrap.min.js"
                    )
            );

            IItemTransform cssFixer = new CssRewriteUrlTransform();

            bundles.Add(
                new StyleBundle("~/Content/bootstrap/css")
                    .Include(
                        "~/Content/bootstrap/css/bootstrap.css",
                        cssFixer
                    )
                    .Include(
                        "~/Content/250ml.css"
                    )
            );

            bundles.Add(
               new ScriptBundle("~/bundles/bootstrap/js")
                   .Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Content/bootstrap/js/bootstrap.js"
                   )
            );















            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css", "~/Content/jquery-ui-timepicker-addon.css"));





           
        }
    }
}