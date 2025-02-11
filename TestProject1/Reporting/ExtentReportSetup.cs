using AventStack.ExtentReports;
using NUnit.Framework;

namespace TestProject1.Reporting
{
    [SetUpFixture]
    public class ExtentReportSetup
    {
        public static ExtentReports Extent;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            Extent = ExtentManager.GetInstance();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            Extent.Flush();
        }
    }
}
