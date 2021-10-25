using System;
using System.Collections.Generic;
using System.Text;

namespace TestUtilities.Resources
{
    public class CinemaEndpoints
    {
        private const string BaseURL = "https://magenicmovies-api.azurewebsites.net/";

        /// <summary>
        /// Request Url for Cinema
        /// </summary>
        public static Uri RequestUrl() => new Uri ($"{BaseURL}cinemas");

        /// <summary>
        /// Request Url for Cinema with Id
        /// </summary>
        public static Uri RequestUrlWithId(long id) => new Uri($"{BaseURL}cinemas/{id}"); 

    }
}
