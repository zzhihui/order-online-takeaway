using System;
using System.Linq;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoogleMapsApi.Entities.PlaceAutocomplete.Request;
using GoogleMapsApi.Entities.PlaceAutocomplete.Response;

namespace Takeaway.Wpf.IntegrationTests
{
    [TestClass]
    public class GoogleAddressSuggestionTest
    {

        private const string ApiKey = " REPLACE_ME";

        [TestMethod]
        public void TestMethod1()
        {
            var aSearch = "78 hunterswalk,Ireland";
            var request = new PlaceAutocompleteRequest
            {
                //ApiKey = "REPLACE_ME",
                Input = aSearch,
                Types = "geocode",
                ApiKey = ApiKey,
                Location = new GoogleMapsApi.Entities.Common.Location(52.659745, -8.616102),
                //Radius = 100
            };

            PlaceAutocompleteResponse result = GoogleMaps.PlaceAutocomplete.Query(request);

            if (result.Status == Status.OVER_QUERY_LIMIT)
                Assert.Inconclusive("Cannot run test since you have exceeded your Google API query limit.");
            Assert.AreNotEqual(Status.ZERO_RESULTS, result.Status);

            var a = result.Results;
        }

        [TestMethod]
        public void Directions_SumOfStepDistancesCorrect()
        {
            var request = new DirectionsRequest
            {
                Origin = "78 Hunters Walk, Hunters Wood, Dublin 24, Ireland",
                Destination = "Nutgrove Shopping Centre, Nutgrove Way, Dublin 14"
            };

            var result = GoogleMaps.Directions.Query(request);

            var distance = result.Routes.FirstOrDefault().Legs.FirstOrDefault().Distance;

            var a = distance.Text;
            var b = distance.Value;


            if (result.Status == DirectionsStatusCodes.OVER_QUERY_LIMIT)
                Assert.Inconclusive("Cannot run test since you have exceeded your Google API query limit.");
            Assert.AreEqual(DirectionsStatusCodes.OK, result.Status);


        }



    }
}
