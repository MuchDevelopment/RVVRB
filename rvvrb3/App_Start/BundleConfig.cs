using System.Web;
using System.Web.Optimization;

namespace rvvrb3
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/angular-upload-shim.js",
                "~/Scripts/angular.js",
                //"~/Scripts/angular-route.min.js",
                //"~/Scripts/angular-cookies.min.js",
                "~/Scripts/angular-resource.min.js",
                "~/Scripts/angular-sanitize.min.js",
                //"~/Scripts/decoder.js",
                //"~/Scripts/demuxer.js",
                "~/Scripts/angular-ui-router.min.js",
                "~/Scripts/angular-file-upload.js",
                "~/Scripts/ui-bootstrap-tpls-0.13.0.min.js",
                "~/App/Directives/angular-audio-playlist.js",
                "~/App/app.js",
                "~/App/Directives/commentsDirective.js",
                "~/App/Controllers/fileUpload.js",
                "~/App/Controllers/appPlaylist.js",
                "~/App/Directives/addToPlaylist.js",
                "~/App/Controllers/SongFactory.js",
                "~/App/Controllers/SongController.js"
                //"~/App/Directives/audioPlayerDirective.js"
                //"~/App/Controllers/MainController.js",
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
