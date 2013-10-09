﻿using System.Web;
using System.Web.Optimization;

namespace ChecklistManager
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/checklistManager").Include(
            //            "~/Scripts/underscore.js",
            //            "~/Scripts/toastr.js",
            //            "~/Scripts/angular.js",
            //            "~/Scripts/angular-resource.js",
            //            "~/Scripts/angular-route.js",
            //            "~/Scripts/bootstrap.js"));

            //bundles.Add(new Bundle("~/bundles/checklistManager-No-Min").Include(
            //            "~/app/controllers/checklist-definition.controllers.js",
            //            "~/app/controllers/checklist.controllers.js",
            //            "~/app/controllers/user.controllers.js",
            //            "~/app/controllers/start.controllers.js",
            //            "~/app/factories/httpInterceptor.factory.js",
            //            "~/app/factories/checklist.factory.js",
            //            "~/app/directives/checklist.directives.js",
            //            "~/app/app.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/toastr.css",
                "~//Content/checklistManager.css"));


            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}