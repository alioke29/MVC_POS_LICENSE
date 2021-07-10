using System.Web;
using System.Web.Optimization;

namespace RINOR_POS
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/smartadmin").IncludeDirectory("~/content/css", "*.min.css"));

            bundles.Add(new ScriptBundle("~/scripts/smartadmin").Include(
                "~/scripts/app.config.js",
                "~/scripts/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
                "~/scripts/bootstrap/bootstrap.min.js",
                "~/scripts/notification/SmartNotification.min.js",
                "~/scripts/smartwidgets/jarvis.widget.min.js",
                "~/scripts/plugin/jquery-validate/jquery.validate.min.js",
                "~/scripts/plugin/masked-input/jquery.maskedinput.min.js",
                "~/scripts/plugin/bootstrap-slider/bootstrap-slider.min.js",
                "~/scripts/plugin/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/scripts/plugin/msie-fix/jquery.mb.browser.min.js",
                "~/scripts/plugin/fastclick/fastclick.min.js",
                "~/scripts/plugin/jcrop/jquery.Jcrop.min.js",
                "~/scripts/plugin/clockpicker/clockpicker.min.js",
                "~/scripts/plugin/jcrop/jquery.color.min.js",
                "~/scripts/app.min.js",
                "~/scripts/demo.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/full-calendar").Include(
                "~/scripts/plugin/moment/moment.min.js",
                "~/scripts/plugin/fullcalendar/jquery.fullcalendar.min.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/charts").Include(
                "~/scripts/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
                "~/scripts/plugin/sparkline/jquery.sparkline.min.js",
                "~/scripts/plugin/morris/morris.min.js",
                "~/scripts/plugin/morris/raphael.min.js",
                "~/scripts/plugin/flot/jquery.flot.cust.min.js",
                "~/scripts/plugin/flot/jquery.flot.resize.min.js",
                "~/scripts/plugin/flot/jquery.flot.time.min.js",
                "~/scripts/plugin/flot/jquery.flot.fillbetween.min.js",
                "~/scripts/plugin/flot/jquery.flot.orderBar.min.js",
                "~/scripts/plugin/flot/jquery.flot.pie.min.js",
                "~/scripts/plugin/flot/jquery.flot.tooltip.min.js",
                "~/scripts/plugin/dygraphs/dygraph-combined.min.js",
                "~/scripts/plugin/chartjs/chart.min.js",
                "~/scripts/plugin/highChartCore/highcharts-custom.min.js",
                "~/scripts/plugin/highchartTable/jquery.highchartTable.min.js"
                ));

            //bundles.Add(new ScriptBundle("~/scripts/datatables").Include(
            //    "~/scripts/plugin/datatables/jquery.dataTables.min.js",
            //    "~/scripts/plugin/datatables/dataTables.colVis.min.js",
            //    "~/scripts/plugin/datatables/dataTables.tableTools.min.js",
            //    "~/scripts/plugin/datatables/dataTables.bootstrap.min.js",
            //    "~/scripts/plugin/datatable-responsive/datatables.responsive.min.js"
            //     ));

            bundles.Add(new ScriptBundle("~/scripts/datatables").Include(
                "~/scripts/plugin/datatables2/JSZip-2.5.0/jszip.min.js",
                "~/scripts/plugin/datatables2/pdfmake-0.1.36/pdfmake.min.js",
                "~/scripts/plugin/datatables2/scripts/plugin/datatables2/pdfmake-0.1.36/vfs_fonts.js",
                "~/scripts/plugin/datatables2/DataTables-1.10.20/js/jquery.dataTables.min.js",
                "~/scripts/plugin/datatables2/datatables.min.js",
                "~/scripts/plugin/datatables2/DataTables-1.10.20/js/dataTables.bootstrap.min.js",
                "~/scripts/plugin/datatables2/Responsive-2.2.3/js/dataTables.responsive.min.js",
                "~/scripts/plugin/datatables2/Responsive-2.2.3/js/responsive.bootstrap.min.js",
                "~/scripts/plugin/datatables2/RowGroup-1.1.1/js/dataTables.rowGroup.min.js",
                "~/scripts/plugin/datatables2/FixedColumns-3.3.0/js/dataTables.fixedColumns.min.js",
                "~/scripts/plugin/datatables2/Buttons-1.6.1/js/dataTables.buttons.min.js",
                "~/scripts/plugin/datatables2/Buttons-1.6.1/js/buttons.flash.min.js",
                "~/scripts/plugin/datatables2/Buttons-1.6.1/js/buttons.html5.min.js",
                "~/scripts/plugin/datatables2/Buttons-1.6.1/js/buttons.bootstrap4.min.js",
                "~/scripts/plugin/datatables2/Buttons-1.6.1/js/buttons.print.min.js",
                "~/scripts/plugin/datatables2/Buttons-1.6.1/js/buttons.colVis.min.js",
                "~/scripts/plugin/datatables2/Scroller-2.0.1/js/dataTables.scroller.min.js",
                "~/scripts/plugin/datatables2/Scroller-2.0.1/js/scroller.bootstrap.min.js",
                "~/scripts/plugin/datatables2/Scroller-2.0.1/js/scroller.dataTables.js",
                "~/scripts/plugin/datatables2/FixedHeader-3.1.6/js/dataTables.fixedHeader.min.js",
                "~/scripts/plugin/datatables2/FixedHeader-3.1.6/js/fixedHeader.bootstrap.min.js",
                "~/scripts/plugin/datatables2/FixedHeader-3.1.6/js/fixedHeader.dataTables.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/jq-grid").Include(
                "~/scripts/plugin/jqgrid/jquery.jqGrid.min.js",
                "~/scripts/plugin/jqgrid/grid.locale-en.min.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/forms").Include(
                "~/scripts/plugin/jquery-form/jquery-form.min.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/smart-chat").Include(
                "~/scripts/smart-chat-ui/smart.chat.ui.min.js",
                "~/scripts/smart-chat-ui/smart.chat.manager.min.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/vector-map").Include(
                "~/scripts/plugin/vectormap/jquery-jvectormap-1.2.2.min.js",
                "~/scripts/plugin/vectormap/jquery-jvectormap-world-mill-en.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/select2-new").Include(
                "~/scripts/plugin/select2-new/js/select2.min.js"
                ));

            bundles.Add(new StyleBundle("~/scripts/select2-new").Include(
                "~/scripts/plugin/select2-new/css/select2.min.css"
                ));
            bundles.Add(new ScriptBundle("~/scripts/jquery-clockpicker").Include(
             "~/scripts/jquery-clockpicker.min.js"
             ));
            bundles.Add(new ScriptBundle("~/scripts/sweetalert2").Include(
                "~/scripts/plugin/sweetalert2/js/sweetalert2.all.js"
                ));

            bundles.Add(new StyleBundle("~/scripts/sweetalert2").Include(
                "~/scripts/plugin/sweetalert2/css/sweetalert2.css"
                ));

            bundles.Add(new ScriptBundle("~/scripts/autoNumeric").Include(
                "~/scripts/plugin/autoNumeric/autoNumeric.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts/clockpicker").Include(
              "~/scripts/plugin/clockpicker/clockpicker.min.js"
              ));

            //custom js for global setting
            bundles.Add(new ScriptBundle("~/scripts/custom").Include(
                "~/scripts/plugin/custom/custom.js"
                ));

            BundleTable.EnableOptimizations = true;
        }

        //// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        //public static void RegisterBundles(BundleCollection bundles)
        //{
        //    bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
        //                "~/Scripts/jquery-{version}.js"));

        //    bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
        //                "~/Scripts/jquery.validate*"));

        //    // Use the development version of Modernizr to develop with and learn from. Then, when you're
        //    // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        //    bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
        //                "~/Scripts/modernizr-*"));

        //    bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
        //              "~/Scripts/bootstrap.js",
        //              "~/Scripts/respond.js"));

        //    bundles.Add(new StyleBundle("~/Content/css").Include(
        //              "~/Content/bootstrap.css",
        //              "~/Content/site.css"));
        //}
    }
}
