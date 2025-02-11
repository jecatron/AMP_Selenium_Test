using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.IO;

namespace TestProject1.Reporting
{
    public static class ExtentManager
    {
        private static ExtentReports _extent;

        public static ExtentReports GetInstance()
        {
            if (_extent == null)
            {
                InitializeReport();
            }
            return _extent;
        }

        private static void InitializeReport()
        {
            // Determine the project directory (assumes tests run from bin\Debug or bin\Release)
            string currentDir = Directory.GetCurrentDirectory();
            // Navigate up to the project folder. Adjust the number of Parent calls if needed.
            DirectoryInfo? parent = Directory.GetParent(currentDir).Parent;
            string projectDir = parent.FullName;

            // Define the folder for reports within your solution
            string reportsDirectory = Path.Combine(projectDir, "Reports");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(reportsDirectory))
            {
                Directory.CreateDirectory(reportsDirectory);
            }

            // Define the full path to the report file
            string reportPath = Path.Combine(reportsDirectory, "ExtentReports.html");

            // Use ExtentSparkReporter for ExtentReports 4.x (or later)
            var sparkReporter = new ExtentSparkReporter(reportPath);
            sparkReporter.Config.DocumentTitle = "Automation Test Report";
            sparkReporter.Config.ReportName = "Selenium Test Report";

            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReporter);

            // Optionally, add system information
            _extent.AddSystemInfo("Environment", "QA");
            _extent.AddSystemInfo("User", "Chester Tester");
        }
    }
}
