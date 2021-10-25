using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpProj.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TestUtilities.DataModels;
using TestUtilities.Resources;

namespace RestSharpProj.Tests
{
    [TestClass]
    public class CinemaTest
    {
        #region variables
        public RestClient restClient { get; set; }
        private long _Id = 0;
        private CinemaJSON cinemaDetails = new CinemaJSON();
        #endregion

        #region Test initialive and clean-up
        [TestInitialize]
        public void TestInit()
        {
            restClient = new RestClient();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            if (_Id != 0)
            {
                CinemaHelpers.DeleteCinema(restClient, _Id);
            }
        }
        #endregion

        /// <summary>
        /// Create a test method that will get all the available cinemas
        /// </summary>
        [TestMethod]
        public void GetAllAvailableCinema() 
        {
            // Arrange
            cinemaDetails = CinemaHelpers.CreateCinema(restClient);
            var request = new RestRequest(CinemaEndpoints.RequestUrl());
            _Id = cinemaDetails.id;

            // Action
            var getAllResponse = restClient.Get<List<CinemaJSON>>(request);
            var cinemaList = getAllResponse.Data;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, getAllResponse.StatusCode);
            Assert.IsTrue(cinemaList.Count >= 1);

            Assert.IsTrue(cinemaList.Any(x => x.Name == cinemaDetails.Name),
                $"Expected cinema name: {cinemaDetails.Name} not found");
            Assert.IsTrue(cinemaList.Any(x => x.id == cinemaDetails.id),
                $"Expected cinema id: {cinemaDetails.id} not found");
        }

        /// <summary>
        /// Create a test method that deletes only the cinema that you created
        /// </summary>
        [TestMethod]
        public void DeleteCinema() 
        {
            // Arrange
            cinemaDetails = CinemaHelpers.CreateCinema(restClient);
            _Id = cinemaDetails.id;
            var request = new RestRequest(CinemaEndpoints.RequestUrlWithId(_Id));

            // Action
            var deleteResponse = restClient.Delete(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        /// <summary>
        /// Create a test method that will cover the negative test for invalid Cinema ID 
        /// </summary>
        [TestMethod]
        public void InvalidCinemaId() 
        {
            // Arrange
            var badId = 9999;
            var request = new RestRequest(CinemaEndpoints.RequestUrlWithId(badId));

            // Action
            var deleteResponse = restClient.Delete(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        }

    }
}
