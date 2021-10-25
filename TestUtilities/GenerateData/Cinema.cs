using System;
using System.Collections.Generic;
using System.Text;
using TestUtilities.DataModels;

namespace TestUtilities.GenerateData
{
    public static class Cinema
    {
        public static CinemaJSON GenerateCinema() 
        {
            return new CinemaJSON
            {
                Name = "Jaja Test",
                BranchId = 2, // use different branch, this is SM Bacoor
            };
        }
    }
}
