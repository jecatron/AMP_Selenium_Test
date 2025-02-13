using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using RazorEngine;
using System.Globalization;


namespace SeleniumProject.Pages
{
    public class DeltaFlightsPage
    {
        private readonly IWebDriver Driver;
        private readonly ObjectRepository _repo;

        // Element locators
        private readonly By Destination;
        private readonly By Origin;
        private readonly By AirportSearch;
        private readonly By Search;
        private readonly By DepartureDate;
        private readonly By ReturnDate;
        private readonly By Passengers;
        private readonly By TripType;
        private readonly By LanguagePreference;
        private readonly By RoundTrip;
        private readonly By FlightCalendar;
        private readonly By CalendarDone;
        private readonly By OnePassenger;
        private readonly By advancedSearch;
        private readonly By BestFares;
        private readonly By MainCabin;

        // Validation locators
        private readonly By DestinationTab;
        private readonly By departureCity;
        private readonly By arrivalCity;
        private readonly By ValDepartDate;
        private readonly By ValReturnDate;
        private readonly By ValTripType;
        private readonly By ValTripPassemgers;
        

        public DeltaFlightsPage(IWebDriver driver)
        {
            Driver = driver;
            _repo = new ObjectRepository("C:\\Users\\josep\\source\\repos\\Solution1\\TestProject1\\ObjectRepository\\DeltaRepo.xml");

            // Load element locators from XML and assign them to By objects
            Origin = _repo.GetByLocator("Origin");
            AirportSearch = _repo.GetByLocator("AirportSearch");
            Destination = _repo.GetByLocator("Destination");
            Search = _repo.GetByLocator("Search");
            DepartureDate = _repo.GetByLocator("DepartureDate");
            FlightCalendar = _repo.GetByLocator("Calendar");
            Passengers = _repo.GetByLocator("Passengers");
            OnePassenger = _repo.GetByLocator("OnePassenger");
            TripType = _repo.GetByLocator("TripType");
            LanguagePreference = _repo.GetByLocator("Language");
            RoundTrip = _repo.GetByLocator("RoundTrip");
            CalendarDone = _repo.GetByLocator("CalendarDone");
           // RefundableFlightsOnly = _repo.GetByLocator("RefundableFlightsOnly");
            advancedSearch = _repo.GetByLocator("AdvancedSearch");
            BestFares = _repo.GetByLocator("BestFares");
            MainCabin = _repo.GetByLocator("MainCabin");

            //Validation locators
            DestinationTab = _repo.GetByLocator("DestinationTab");
            departureCity = _repo.GetByLocator("departureCity");
            arrivalCity = _repo.GetByLocator("arrivalCity");
            ValTripType = _repo.GetByLocator("ValTripType");
            ValDepartDate = _repo.GetByLocator("ValDepartDate");
            ValReturnDate = _repo.GetByLocator("ValReturnDate");
            ValTripPassemgers = _repo.GetByLocator("ValTripPassengers");
            
        }

        public void WaitForElementToBeVisible(By locator)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public void LanguagePreferenceScreen()
        {
            if (Driver.FindElement(LanguagePreference).Displayed)
            {
                Driver.FindElement(LanguagePreference).Click();
            }
            
        }


        // Find random cities to search for flights
        public (string, string) SelectRandomCities()
        {
            List<string> cities = new List<string> { "ATL", "JFK", "BOS", "MIA", "DAL"};
            Random random = new Random();

            string origin = cities[random.Next(cities.Count)];
            string destination;

            do
            {
                destination = cities[random.Next(cities.Count)];
            } while (origin == destination);

            return (origin, destination);
        }

        // Encapsulated methods for flight search actions
        public void EnterOrigin(string origin)
        {
            Driver.FindElement(Origin).SendKeys(origin);
            Driver.FindElement(AirportSearch).Click();   
            Driver.FindElement(AirportSearch).SendKeys(Keys.Enter);
        }
        public void EnterDestination(string destination)
        {
            Thread.Sleep(1000);
            Driver.FindElement(Destination).SendKeys(destination);
            Driver.FindElement(AirportSearch).Click();
            Driver.FindElement(AirportSearch).SendKeys(Keys.Enter);


        }

        public void SelectRoundTrip()
        {
            Driver.FindElement(TripType).Click();   
            Driver.FindElement(RoundTrip).Click();
        }

        public void EnterFlightDates()
        {
            Driver.FindElement(DepartureDate).Click();
            //Depart Date is 3 days from today
            Driver.FindElement(FlightCalendar).SendKeys(Keys.Right + Keys.Right + Keys.Right + Keys.Enter);
            //Return Date is 6 days from today
            Driver.FindElement(FlightCalendar).SendKeys(Keys.Right + Keys.Right + Keys.Right + Keys.Enter);
            //CLick Done
            Driver.FindElement(CalendarDone).Click();
        }

        public void EnterPassengers(string PassengerNumber)
        {
            Driver.FindElement(Passengers).Click();
            Driver.FindElement(OnePassenger).Click();
        }

        public void AdvancedSearch()
        {
            //Click Advanced Search
            Driver.FindElement(advancedSearch).Click();
            //Click Best Fares
            Driver.FindElement(BestFares).Click();
            //Click Main Cabin
            Driver.FindElement(MainCabin).Click();
        }
        public void ClickSearchButton()
        {
            Driver.FindElement(Search).Click();
        }

        //Validation Objects

        public JObject GetBookingPageData()
        {
            JObject pageData = new JObject();
            Thread.Sleep(3000); 
            Driver.FindElement(DestinationTab).Click();
            string originCity = Driver.FindElement(departureCity).Text;
            string destinationCity = Driver.FindElement(arrivalCity).Text;
            string tripType = Driver.FindElement(ValTripType).Text;
            string departDate = Driver.FindElement(ValDepartDate).Text;
            string returnDate = Driver.FindElement(ValReturnDate).Text;
            string passengers = Driver.FindElement(ValTripPassemgers).Text;

            // Store the extracted value
            pageData["originCity"] = originCity; 
            pageData["destinationCity"] = destinationCity;
            pageData["tripType"] = tripType;
            pageData["departDate"] = departDate;
            pageData["returnDate"] = returnDate;
            pageData["passengers"] = passengers;

            return pageData;
        }


    }
}
