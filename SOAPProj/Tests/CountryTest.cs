using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace SOAPProj.Tests
{
    [TestClass]
    public class CountryTest
    {
        #region private variables
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryInfo =
            new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        private Random randomValue = new Random();
        #endregion

        /// <summary>
        /// SOAP Problem 1
        /// </summary>
        [TestMethod]
        public void VerifyFullCountryInfo() 
        {
            // Arrange
            var listOfCountryNamesbyCode = this.ListOfCountryNamesByCode();
            var randomCountry = this.RandomCountryRecord(listOfCountryNamesbyCode);

            // Action
            var fullCountryInfo = countryInfo.FullCountryInfo(randomCountry.sISOCode);

            // Assert
            Assert.AreEqual(randomCountry.sName, fullCountryInfo.sName);
            Assert.AreEqual(randomCountry.sISOCode, fullCountryInfo.sISOCode);
        }

        [TestMethod]
        public void VerifyCountryCode() 
        {
            // Arrange
            var listOfCountryNamesbyCode = this.ListOfCountryNamesByCode();
            var countryRecords = listOfCountryNamesbyCode.Take(5).ToList();
          
            foreach (var record in countryRecords)
            {
                // Action
                var countryISOCode = countryInfo.CountryISOCode(record.sName);

                // Assert
                Assert.AreEqual(record.sISOCode, countryISOCode);
            }
       }

        #region private helper methods
        /// <summary>
        /// Get the list of Country Name By Code
        /// </summary>
        /// <returns>List of country records</returns>
        private ServiceReference1.tCountryCodeAndName[] ListOfCountryNamesByCode() 
        {
            return countryInfo.ListOfCountryNamesByCode();
        }

        /// <summary>
        /// Get random Country Record
        /// </summary>
        /// <returns>random country record</returns>
        private ServiceReference1.tCountryCodeAndName RandomCountryRecord(ServiceReference1.tCountryCodeAndName[] countryList)
        {
            var value = randomValue.Next(0, countryList.Length);
            return countryList[value];
        }
        #endregion
    }
}
