using System.Web;
using System.Web.Optimization;

namespace WebDiskBrowser
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/w3").Include(
						"~/Content/W3/w3.css"));

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
						"~/Scripts/angular.js",
						"~/Scripts/angular-*"));
			bundles.Add(new ScriptBundle("~/bundles/angular-custom").IncludeDirectory("~/Scripts/angularCustomScripts","*.js",false));

			bundles.UseCdn = true;

			var FontAwesomeCdn = "http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css";
			var AngularJSCDN = "https://ajax.googleapis.com/ajax/libs/angularjs/1.0.6/angular.min.js";
			var AngularSanitizeCDN = "https://ajax.googleapis.com/ajax/libs/angularjs/1.0.6/angular-sanitize.js";

			//bundles.Add(new StyleBundle("~/Content/fontAw", FontAwesomeCdn).Include(
			//			FontAwesomeCdn));
		}
	}
}
