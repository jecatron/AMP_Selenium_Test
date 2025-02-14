
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumProject.Pages;
using OpenQA.Selenium.Chrome;
using System;
using TestProject1;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using TestProject1.Reporting;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SeleniumProject.Tests
{

    [TestFixture]
    public class FlightTest : TestProject1.TestBase
    {
        private DriverSetup? _driverSetup;
        private IWebDriver? Driver;
        private DeltaFlightsPage? _testPage;
        private static ExtentReports _extent;
        private ExtentTest test;
 

        [OneTimeSetUp]
        public static void GlobalSetup()
        {
            // Initialize the ExtentReports object and attach a reporter
            string reportPath = "C:\\Users\\josep\\source\\repos\\Solution1\\TestProject1\\Reporting\\Reports\\\\ExtentReport.html";
            var sparkReporter = new ExtentSparkReporter(reportPath);
           
            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReporter);
        }

        [SetUp]
            public void Setup()
            {
                _driverSetup = new DriverSetup();
                Driver = _driverSetup.Driver;
                Driver.Navigate().GoToUrl("https://www.delta.com/");
                _testPage = new DeltaFlightsPage(Driver);
                test = _extent.CreateTest("Flight Booking Test").Info("Test Started");


        }

            [Test]
            public void FindRoundTripFlights()
            {
            //If language screen appears, select English (US)
            _testPage.LanguagePreferenceScreen();
            // Find 2 Random Cities from List
            var (origin, destination) = _testPage.SelectRandomCities();
            //Enter Origin City
            _testPage.EnterOrigin(origin);
            //Enter Destination City
            _testPage.EnterDestination(destination);
            //Select Trip Type
            _testPage.SelectRoundTrip();
            //Enter Trip Dates
            _testPage.EnterFlightDates();
            //Enter Passengers
            _testPage.EnterPassengers("1");
            //Advanced Search 
            _testPage.AdvancedSearch();
            //Search for Flights
            _testPage.ClickSearchButton();

            //Validate Flight page is displayed and Data matches input
            JObject tripData = _testPage.GetBookingPageData();
            string departureCity = tripData["originCity"].ToString();
            string arrivalCity = tripData["destinationCity"].ToString();

            Assert.That(departureCity, Is.EqualTo(origin));
            if (departureCity == origin)
                test.Log(Status.Pass, "Departure City Matches input Value");
            else
                test.Log(Status.Fail, "Departure City does not Match input Value");
            Assert.That(arrivalCity, Is.EqualTo(destination));

            if (arrivalCity == destination)
                test.Log(Status.Pass, "Arrival City Matches input Value");
            else
                test.Log(Status.Fail, "Arrival City does not Match input Value");
            string expectedDepartDate = DateTime.Now.AddDays(2).ToString("MMM dd");
            string expectedReturnDate = DateTime.Now.AddDays(5).ToString("MMM dd");
            Assert.That(tripData["departDate"].ToString(), Is.EqualTo(expectedDepartDate));

            if (tripData["departDate"].ToString() == expectedDepartDate)
                test.Log(Status.Pass, "Departure Date is 2 days from today");
            else
                test.Log(Status.Fail, "Departure Date is not 2 days from today");
            Assert.That(tripData["returnDate"].ToString(), Is.EqualTo(expectedReturnDate));

            if (tripData["returnDate"].ToString() == expectedReturnDate)
                test.Log(Status.Pass, "Return Date is 3 days from today");
            else
                test.Log(Status.Fail, "Return Date is not 3 days from today");
            Assert.That(tripData["tripType"].ToString(), Is.EqualTo("Round Trip"));

            if (tripData["tripType"].ToString() == "Round Trip")
                test.Log(Status.Pass, "Trip Type is Round Trip");
            else
                test.Log(Status.Fail, "Trip Type is not Round Trip");
            Assert.That(tripData["passengers"].ToString(), Is.EqualTo("1 Passenger"));

            if (tripData["passengers"].ToString() == "1 Passenger")
                test.Log(Status.Pass, "Passenger Count is 1");
            else
                test.Log(Status.Fail, "Passenger Count is not 1");



        }

        [TearDown]
            public void Teardown()
            {
                _driverSetup.CloseDriver();
                _extent.Flush();
            }

    }
       
    }

