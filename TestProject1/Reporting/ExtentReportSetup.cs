using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace FlightBookingTest.Reports
{
    public static class ExtentReportSetup
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _sparkReporter;
        private static readonly string reportPath = Path.Combine(Directory.GetCurrentDirectory(),
            $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");

        public static void InitializeReport()
        {
            if (_extent == null)
            {
                _sparkReporter = new ExtentSparkReporter(reportPath);
                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
            }
        }

        public static ExtentReports Extent
        {
            get
            {
                if (_extent == null)
                    throw new NullReferenceException("ExtentReports is not initialized. Call InitializeReport() first.");

                return _extent;
            }
        }

        public static void FlushReport()
        {
            _extent?.Flush();
        }
    }
}
