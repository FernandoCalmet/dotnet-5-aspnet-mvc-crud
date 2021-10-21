using System.Web;
using System.Web.Optimization;

namespace cs_aspnet_mvc_crud
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            //// para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/DefaultTheme/site.css"));

            bundles.Add(new StyleBundle("~/bundles/auth/css").Include("~/Content/DefaultTheme/auth.css"));

            bundles.Add(new StyleBundle("~/bundles/carousel/css").Include("~/Content/DefaultTheme/carousel.css"));

            bundles.Add(new StyleBundle("~/bundles/headers/css").Include("~/Content/DefaultTheme/headers.css"));

            bundles.Add(new StyleBundle("~/bundles/siderbars/css").Include("~/Content/DefaultTheme/siderbars.css"));
        }
    }
}
