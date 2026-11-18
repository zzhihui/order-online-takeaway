using System;
using System.Collections.Generic;
using System.Linq;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.PlaceAutocomplete.Request;
using GoogleMapsApi.Entities.PlaceAutocomplete.Response;

namespace Takeaway.Admin.Wpf.Utility
{
    public class GoogleAddress
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("Address");


        public static List<string> SuggestAddress(string input)
        {
            var list = new List<string>();

            var inputSearch = string.Format("{0}", input);
            var request = new PlaceAutocompleteRequest
            {
                ApiKey = "REPLACE_ME",
                Input = inputSearch,
                Types = "address",
                Components = "country:ie"

            };

            var result = GoogleMaps.PlaceAutocomplete.Query(request);

            if (result.Status == Status.OK)
            {
                list.AddRange(result.Results.Select(suggestion => suggestion.Description));
            }

            return list;
        }



        public static bool GetDirection(string originalAddress, string findAddress, out string distanceText, out int distanceValue)
        {
            var request = new DirectionsRequest
            {
                Origin = originalAddress,
                Destination = findAddress
            };

            var result = GoogleMaps.Directions.Query(request);


            try
            {

                if (result.Status == DirectionsStatusCodes.OVER_QUERY_LIMIT)
                    throw new Exception("Cannot run test since you have exceeded your Google API query limit.");

                var distance = result.Routes.FirstOrDefault().Legs.FirstOrDefault().Distance;
                distanceText = distance.Text;
                distanceValue = distance.Value;

               
            }
            catch (Exception ex)
            {
                Logger.WarnFormat("[GetDirection] {0}", ex.Message);
                distanceValue = 0;
                distanceText= "UnKnown Distance";
            }

            return true;
        }



    }
}