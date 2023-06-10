using AS.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using ChartJSCore.Plugins.Zoom;
using AS.DATA.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AS.UI.MVC.Controllers
{
    [Authorize(Roles = "HR, Admin, Accounting")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ASContext _context;

        public HomeController(ILogger<HomeController> logger, ASContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {


            #region Retrieve Sales Data

            //Retrieve all Sales records (and associated info) from the past 6 months
            var salesData = _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Customer.Country)
                .Include(s => s.Customer.Country.Region)
                .Include(s => s.Salesperson)
                .Where(s => s.SaleDate >= (DateTime.Today.AddMonths(-6)))
                .OrderBy(s => s.SaleDate)
                .ToList();

            #endregion

            #region Bar Chart - Sales Over Last 6 Months

            //Create the Chart object
            Chart salesBarChart = new Chart();

            //Assign the type using the enum
            salesBarChart.Type = Enums.ChartType.Bar;

            //Create the chart Data object
            ChartJSCore.Models.Data barChartData = new ChartJSCore.Models.Data();

            //Retrieve the distinct, abbreviated month names present in the data
            List<string> monthNames = salesData
                                        .Select(sale => sale.SaleDate.ToString("MMM"))
                                        .Distinct()
                                        .ToList();

            //Retrieve the total sales for each month
            List<decimal?> monthlyTotalSales = salesData
                                                .GroupBy(s => new { s.SaleDate.Year, s.SaleDate.Month })
                                                .Select(group => group.Sum(s => s.SaleTotal))
                                                .ToList();

            //Convert the total sales data into doubles, as required by BarDataset
            List<double?> monthlySalesAsDoubles = monthlyTotalSales.ConvertAll(s => (double?)s);

            //Assign the month names to the label
            barChartData.Labels = monthNames;

            //Create the BarDataset object and assign values
            BarDataset barDataset = new BarDataset()
            {
                Label = "Sales",
                Data = monthlySalesAsDoubles,
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(28, 200, 138)
                },
                BorderColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(28, 200, 138)
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = 1,
                BarThickness = 20,
                MaxBarThickness = 30,
                MinBarLength = 2
            };

            //Create a List<Dataset> to store our Chart data
            barChartData.Datasets = new List<Dataset>();

            //Add the customized BarDataset to the list
            barChartData.Datasets.Add(barDataset);

            //Assign the List<Dataset> to the Data property of our Chart object
            salesBarChart.Data = barChartData;

            //Create and customize formatting options to be used with our chart
            var options = new Options
            {
                Responsive = true,
                MaintainAspectRatio = false
            };

            //Store the completed chart in the ViewData
            ViewData["BarChart"] = salesBarChart;

            #endregion

            #region Pie Chart - Sales by Region Over Last 6 Months

            //Create the Chart object
            Chart regionalPieChart = new Chart();

            //Assign the type using the enum
            regionalPieChart.Type = Enums.ChartType.Pie;

            //Create the chart Data object
            ChartJSCore.Models.Data pieChartData = new ChartJSCore.Models.Data();

            //Retrieve the distinct RegionName values from the salesData
            List<string> salesRegions = salesData
                                         .Where(s => s.Customer.Country.RegionId != null
                                                && !String.IsNullOrEmpty(s.Customer.Country.Region.RegionName))
                                         .OrderBy(s => s.Customer.Country.Region.RegionName)
                                         .Select(s => s.Customer.Country.Region.RegionName)
                                         .Distinct()
                                         .ToList();

            //Retrieve the Count of sales records, grouped by region
            List<int> salesPerRegion = salesData
                                        .Where(s => s.Customer.Country.RegionId != null
                                                && !String.IsNullOrEmpty(s.Customer.Country.Region.RegionName))
                                        .OrderBy(s => s.Customer.Country.Region.RegionName)
                                        .GroupBy(s => s.Customer.Country.Region.RegionName)
                                        .Select(group => group.Count())
                                        .ToList();

            //Convert total sales per region to doubles, as reqiored by PieDataset 
            List<double?> salesPerRegionAsDoubles = salesPerRegion.ConvertAll(s => (double?)s);

            //Assign the region names to the labels
            pieChartData.Labels = salesRegions;

            //Create a list of colors, from which we can create ChartColors
            List<string> colors = new List<string>()
            {
                "#4e73df", //Primary
                "#f6c23e", //Warning
                "#1cc88a", //Success
                "#e74a3b", //Danger
                "#36b9cc", //Info
                "#858796", //Secondary
                "#5a5c69", //Dark

                //NOTE: The values above were included based on the Bootstrap
                //color palette. Additional values may be needed for larger
                //datasets.
            };

            //Create a list to store ChartColors
            List<ChartColor> chartColors = new List<ChartColor>();

            //Loop through the sales regions
            for (int i = 0; i < salesRegions.Count; i++)
            {
                //Create a ChartColor using the current index value of colors
                ChartColor chartColor = ChartColor.FromHexString(colors[i]);

                //Add the ChartColor to the list
                chartColors.Add(chartColor);
            }

            //Create the PieDataset object and assign values
            PieDataset pieDataset = new PieDataset()
            {
                Label = "Sales by Region",
                BackgroundColor = chartColors,
                HoverBackgroundColor = chartColors,
                Data = salesPerRegionAsDoubles
            };

            //Create a List<Dataset> to store our Chart data
            pieChartData.Datasets = new List<Dataset>();

            //Add the customized PieDataset to the list
            pieChartData.Datasets.Add(pieDataset);

            //Assign the List<Dataset> to the Data property of our Chart object
            regionalPieChart.Data = pieChartData;

            //Store the completed chart in the ViewData
            ViewData["PieChart"] = regionalPieChart;

            #endregion

            #region Bar Chart - Sales Rep Totals

            //Create the Chart object
            Chart salesRepBarChart = new Chart();

            //Assign the type using the enum
            salesRepBarChart.Type = Enums.ChartType.Bar;

            //Create the chart Data object
            ChartJSCore.Models.Data repChartData = new ChartJSCore.Models.Data();

            //Retrieve the salesperson names
            List<string> repNames = salesData
                                        .GroupBy(s => s.Salesperson.FullName)
                                        .Where(s => s.Sum(b => b.SaleTotal) >= 200000)
                                        .Select(group => group.Key)
                                        .ToList();

            //Retrieve the total sales for each salesperson
            List<decimal?> repTotalSales = salesData
                                                .GroupBy(s => s.Salesperson.FullName)
                                                .Where(s => s.Sum(a => a.SaleTotal) >= 200000)
                                                .Select(group => group.Sum(s => s.SaleTotal))
                                                .ToList();

            //Convert the total sales data into doubles, as required by BarDataset
            List<double?> repSalesAsDoubles = repTotalSales.ConvertAll(s => (double?)s);

            //Assign the salesperson names to the label
            repChartData.Labels = repNames;

            //Create the BarDataset object and assign values
            BarDataset repDataset = new BarDataset()
            {
                Label = "Sales Representative",
                Data = repSalesAsDoubles,
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(54, 185, 204)
                },
                BorderColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(54, 185, 204)
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = 1,
                BarThickness = 20,
                MaxBarThickness = 30,
                MinBarLength = 2
            };

            //Create a List<Dataset> to store our Chart data
            repChartData.Datasets = new List<Dataset>();

            //Add the customized BarDataset to the list
            repChartData.Datasets.Add(repDataset);

            //Assign the List<Dataset> to the Data property of our Chart object
            salesRepBarChart.Data = repChartData;

            //Create and customize formatting options to be used with our chart
            var repOptions = new Options
            {
                Responsive = true,
                MaintainAspectRatio = false
            };

            //Store the completed chart in the ViewData
            ViewData["SalesRepBarChart"] = salesRepBarChart;

            #endregion

            #region Bar Chart - Sales YTD

            //Retrieve all Sales records (and associated info) for YTD
            var ytdData = _context.Sales
                    .Include(s => s.Customer)
                    .Include(s => s.Salesperson)
                    .ToList();

            //filtered ytdData
            var filteredYtd = ytdData
                    .Where(s => s.SaleDate >= new DateTime(DateTime.Today.Year, 1, 1) && s.SaleDate <= DateTime.Today)
                    .OrderBy(s => s.SaleDate)
                    .ToList();

            //Create the Chart object
            Chart ytdSalesChart = new Chart();

            //Assign the type using the enum
            ytdSalesChart.Type = Enums.ChartType.Line;

            //Create the chart Data object
            ChartJSCore.Models.Data lineData = new ChartJSCore.Models.Data();

            //Retrieve the distinct, abbreviated month names present in the data
            List<string> ytdmonthNames = filteredYtd
                                        .Select(sale => sale.SaleDate.ToString("MMM"))
                                        .Distinct()
                                        .ToList();

            //Retrieve the total sales for each month
            List<decimal?> ytdTotalSales = filteredYtd
                                                .GroupBy(s => new { s.SaleDate.Year, s.SaleDate.Month })
                                                .Select(group => group.Sum(s => s.SaleTotal))
                                                .ToList();

            //Convert the total sales data into doubles, as required by BarDataset
            List<double?> ytdSalesAsDoubles = ytdTotalSales.ConvertAll(s => (double?)s);

            //Assign the month names to the label
            lineData.Labels = ytdmonthNames;

            //Create the BarDataset object and assign values


            LineDataset ytdDataset = new LineDataset()
            {
                Label = "Sales",
                Data = ytdSalesAsDoubles,
                Fill = "true",
                Tension = .01,
                BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(75, 192, 192, 0.4) },
                BorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                BorderCapStyle = "butt",
                BorderDash = new List<int>(),
                BorderDashOffset = 0.0,
                BorderJoinStyle = "miter",
                PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                PointBorderWidth = new List<int> { 1 },
                PointHoverRadius = new List<int> { 5 },
                PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                PointHoverBorderWidth = new List<int> { 2 },
                PointRadius = new List<int> { 1 },
                PointHitRadius = new List<int> { 10 },
                SpanGaps = false
            };



            //Create a List<Dataset> to store our Chart data
            lineData.Datasets = new List<Dataset>();

            //Add the customized BarDataset to the list
            lineData.Datasets.Add(ytdDataset);

            //Assign the List<Dataset> to the Data property of our Chart object
            ytdSalesChart.Data = lineData;

            //Create and customize formatting options to be used with our chart

            ZoomOptions zoomOptions = new ZoomOptions
            {
                Zoom = new Zoom
                {
                    Wheel = new Wheel
                    {
                        Enabled = true
                    },
                    Pinch = new Pinch
                    {
                        Enabled = true
                    },
                    Drag = new Drag
                    {
                        Enabled = true,
                        ModifierKey = Enums.ModifierKey.alt
                    }
                },
                Pan = new Pan
                {
                    Enabled = true,
                    Mode = "xy"
                }
            };

            ytdSalesChart.Options.Plugins = new ChartJSCore.Models.Plugins
            {
                PluginDynamic = new Dictionary<string, object> { { "zoom", zoomOptions } }
            };

            //var options = new Options
            //{
            //    Responsive = true,
            //    MaintainAspectRatio = false
            //};

            //Store the completed chart in the ViewData
            ViewData["ytdLineChart"] = ytdSalesChart;

            #endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}