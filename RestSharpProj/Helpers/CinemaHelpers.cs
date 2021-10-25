using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TestUtilities.DataModels;
using TestUtilities.GenerateData;
using TestUtilities.Resources;

namespace RestSharpProj.Helpers
{
    public static class CinemaHelpers
    {
        /// <summary>
        /// Create Cinema via Post
        /// </summary>
        /// <param name="restClient">RestClient</param>
        /// <returns>Cinema data</returns>
        public static CinemaJSON CreateCinema(RestClient restClient) 
        {
            var request = new RestRequest(CinemaEndpoints.RequestUrl());
            var cinemaData = Cinema.GenerateCinema();
            request.AddJsonBody(cinemaData);
            var response = restClient.Post<CinemaJSON>(request);

            return response.Data;
        }

        /// <summary>
        /// Delete Cinema
        /// </summary>
        /// <param name="restClient">RestClient</param>
        /// <param name="id">cinema id to be deleted</param>
        public static void DeleteCinema(RestClient restClient, long id)
        {
            var request = new RestRequest(CinemaEndpoints.RequestUrlWithId(id));

            var response = restClient.Delete(request);
        }
    }
}
