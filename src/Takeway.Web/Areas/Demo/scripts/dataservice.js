
angular.module('webapp.core', [])
        .factory('dataservice', dataservice);

var configHeader = {
    //TODO: localisation
    'IndaSoftAuthToken': '107,45,80,39,163,116,145,5,117,10,40,179,104,2,227,157,137,127,156,230,114,108,62,251,126,240,27,245,101,185,253,135,49,9,99,3,241,89,222,148,125,247,92,196,85,173,194,116,220,228,121,28,116,155,161,228,29,38,35,21,210,54,213,228,125,133,141,56,30,242,19,107,11,135,220,223,182,77,167,239,209,247,177,111,173,74,61,132,182,146,241,43,46,162,55,225,72,109,3,169,99,37,40,214,228,229,77,169,52,203,222,4,172,32,55,237,9,65,221,222,9,93,76,242,198,47,113,186',
    'Content-Type': 'application/json'
};

var websiteUrl = 'https://indasoftstaging.azurewebsites.net';
//var websiteUrl = 'http://localhost:49041';



/* @ngInject */
function dataservice($http, $location, $q) {

    //Service Functions
    var service = {
        getShopInfo: getShopInfo
    };
    return service;


    //Support function
    function showBusy() {
        //TODO: Busy indicator
    }
    function hideBusy() {

        //TODO: Busy indicator
    }

    function rest(request) {

        var deferred = $q.defer();
        showBusy();
        return $http(request).
           success(function (data) {
               hideBusy();
               deferred.resolve(data);
               return deferred.promise;
           }).
           error(function (data) {
               hideBusy();
               console.log("failure message: " + JSON.stringify({ data: data }));
               deferred.reject();
               $location.url('/');
           });
    }


    function getShopInfo() {
        return rest({ method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetShopInfo"), headers: configHeader });
    }



}
