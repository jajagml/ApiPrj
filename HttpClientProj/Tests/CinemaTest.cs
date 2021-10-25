using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using TestUtilities.DataModels;
using TestUtilities.GenerateData;
using TestUtilities.Resources;

namespace HttpClientProj.Tests
{
    [TestClass]
    public class CinemaTest
    {
        #region private variables
        public HttpClient httpClient { get; set; }
        private long _Id = 0;
        #endregion

        #region Test initialive and clean-up
        [TestInitialize]
        public void TestInit() 
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            if (_Id != 0) 
            {
                var deleteResponse = httpClient.DeleteAsync(CinemaEndpoints.RequestUrlWithId(_Id));
                var deleteResponseMessage = deleteResponse.Result; 
                
            }
        }
        #endregion

        /// <summary>
        /// Create a test method that creates cinema under SM Mall of Asia Branch (10 pts)
        /// </summary>
        [TestMethod]
        public void CreateCinema() 
        {
            // Arrange
            var cinemaData = Cinema.GenerateCinema();
            var cinemaJson = JsonConvert.SerializeObject(cinemaData);
            var postCinemaRequest = new StringContent(cinemaJson, Encoding.UTF8, "application/json");

            // Action
            var postResponse = httpClient.PostAsync(CinemaEndpoints.RequestUrl(), postCinemaRequest);
            var postResponseMessage = postResponse.Result;
            var responseData = postResponseMessage.Content.ReadAsStringAsync();
            var cinema = JsonConvert.DeserializeObject<CinemaJSON>(responseData.Result);
            _Id = cinema.id; // for clean-up

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, postResponseMessage.StatusCode);
        }

        /// <summary>
        /// Create a test method that updates the name of the cinema that you created
        /// </summary>
        [TestMethod]
        public void UpdateCinema()
        {
            #region Part 1
            // Arrange
            var cinemaData = Cinema.GenerateCinema();
            var cinemaJson = JsonConvert.SerializeObject(cinemaData);
            var postCinemaRequest = new StringContent(cinemaJson, Encoding.UTF8, "application/json");
            var postResponse = httpClient.PostAsync(CinemaEndpoints.RequestUrl(), postCinemaRequest);
            var postResponseMessage = postResponse.Result;
            var responseData = postResponseMessage.Content.ReadAsStringAsync();
            var cinema = JsonConvert.DeserializeObject<CinemaJSON>(responseData.Result);
            _Id = cinema.id; // for clean-up

            cinemaData.Name = "UpdatedJaja Cinema";
            var putCinemaJson = JsonConvert.SerializeObject(cinemaData);
            var putCinemaRequest = new StringContent(putCinemaJson, Encoding.UTF8, "application/json");

            // Action
            var putResponse = httpClient.PutAsync(CinemaEndpoints.RequestUrlWithId(_Id), putCinemaRequest);
            var putResponseMessage = putResponse.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, putResponseMessage.StatusCode);
            #endregion

            #region Part 2
            // Action
            var getAllResponse = httpClient.GetAsync(CinemaEndpoints.RequestUrl());
            var getAllResponseMessage = getAllResponse.Result;
            var getAllResponseData = getAllResponseMessage.Content.ReadAsStringAsync();
            var cinemaList = JsonConvert.DeserializeObject<List<CinemaJSON>>(getAllResponseData.Result);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, getAllResponseMessage.StatusCode);
            Assert.IsTrue(cinemaList.Any(x => x.Name == cinemaData.Name),$"Expected cinema {cinemaData.Name} not found");
            #endregion

        }

        /// <summary>
        /// Create a test method that will cover the negative test for invalid Cinema ID
        /// </summary>
        [TestMethod]
        public void InvalidCinemaId()
        {
            // Arrange
            var badId = 9999;

            // Action
            var getResponse = httpClient.GetAsync(CinemaEndpoints.RequestUrlWithId(badId));
            var getResponseMessage = getResponse.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, getResponseMessage.StatusCode);

        }
    }
}
