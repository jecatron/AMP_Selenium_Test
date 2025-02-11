using System;
using System.Xml.Linq;
using OpenQA.Selenium;

namespace SeleniumProject
{
    public class ObjectRepository
    {
        private readonly XElement _root;

        // Constructor to load the XML file
        public ObjectRepository(string filePath)
        {
            _root = XElement.Load(filePath); // Load XML file containing the object repository
        }

        // Method to get the locator type (e.g., "id", "xpath", "css") for a given element
        public string GetLocatorType(string elementName)
        {
            var element = _root.Element("Element");
            if (element != null && element.Attribute("name")?.Value == elementName)
            {
                return element.Attribute("type")?.Value ?? "xpath"; // Default to "xpath" if no type is provided
            }
            throw new ArgumentException($"Element {elementName} not found in the object repository.");
        }

        // Method to get the locator value (e.g., "usernameField", ".login-button")
        public string GetLocator(string elementName)
        {
            var element = _root.Element("Element");
            if (element != null && element.Attribute("name")?.Value == elementName)
            {
                return element.Value;
            }
            throw new ArgumentException($"Element {elementName} not found in the object repository.");
        }

        // Method to return the correct By locator (e.g., By.Id, By.XPath, etc.)
        public By GetByLocator(string elementName)
        {
            try
            {
                Console.WriteLine($"🔍 Searching for element: {elementName}");

                var element = _root.Descendants("Element")
                                   .FirstOrDefault(e => e.Attribute("name")?.Value == elementName);

                if (element == null)
                {
                    Console.WriteLine($"🚨 ERROR: Element '{elementName}' not found in XML repository.");
                    throw new ArgumentException($"❌ ERROR: Element '{elementName}' not found in the XML repository.");
                }

                string locatorType = element.Attribute("type")?.Value ?? "XPath";
                string locatorValue = element.Value.Trim();

                Console.WriteLine($"✅ Found element → Name: {elementName}, Type: {locatorType}, Value: {locatorValue}");

                return locatorType.ToLower() switch
                {
                    "id" => By.Id(locatorValue),
                    "css" => By.CssSelector(locatorValue),
                    "name" => By.Name(locatorValue),
                    "xpath" => By.XPath(locatorValue),
                    "linktext" => By.LinkText(locatorValue),
                    "partiallinktext" => By.PartialLinkText(locatorValue),
                    "tagname" => By.TagName(locatorValue),
                    "classname" => By.ClassName(locatorValue),
                    _ => throw new ArgumentException($"❌ ERROR: Locator type '{locatorType}' is not supported.")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🚨 EXCEPTION in GetByLocator('{elementName}'): {ex.Message}");
                throw;
            }
        }

        public void DebugPrintElements()
        {
            Console.WriteLine("🔎 Elements Found in XML Repository:");
            foreach (var element in _root.Descendants("Element"))
            {
                Console.WriteLine($"➡ Name: {element.Attribute("name")?.Value}, " +
                                  $"Type: {element.Attribute("type")?.Value}, " +
                                  $"Value: {element.Value}");
            }
        }
    }

}
