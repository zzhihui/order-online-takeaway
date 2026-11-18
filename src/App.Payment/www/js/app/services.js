(function () {
    'use strict';

    var config = {
        headers: {
            'IndaSoftAuthToken': '45,203,48,87,27,34,217,218,4,13,37,35,159,226,194,14,156,34,32,236,183,36,26,168,65,212,55,181,212,196,48,175,124,90,216,180,226,85,4,106,120,114,109,153,37,7,110,149,228,103,87,104,108,254,58,15,182,196,141,97,213,56,167,27,148,94,224,31,185,65,61,167,246,226,201,216,83,121,149,177,160,206,206,157,171,228,67,221,215,100,186,234,128,103,215,227,193,155,251,71,52,158,122,178,244,255,177,46,134,119,35,252,146,169,8,230,71,146,47,32,65,214,30,70,0,207,153,128',
            'Content-Type': 'application/json'
        }
    };

    
    var websiteUrl = 'http://localhost:49041';
    var shopIndaSoftId = 1;

    angular.module('starter.services', [])
        .factory('wsShopService', function ($http, $location, $q, $ionicLoading) {
            //Support Functions
            function showBusy() {

                $ionicLoading.show({
                    content: 'Loading',
                    animation: 'fade-in',
                    showBackdrop: true,
                    maxWidth: 200,
                    showDelay: 0
                });
            }

            function hideBusy() {

                $ionicLoading.hide();
            }

            function rest(request) {
                var deferred = $q.defer();
                showBusy();
                return $http(request).
                    success(function (data, status, headers, config) {
                        hideBusy();
                        deferred.resolve(data);
                        return deferred.promise;
                    }).
                    error(function (data, status, headers, config) {
                        hideBusy();
                        console.error("failure message: " + JSON.stringify({ data: data }));
                        deferred.reject();
                        $location.url('/');
                    });
            }

            return {
                getShopInfo: function () {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetShopInfo?shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getCategories: function () {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetMenuItemCategoryByGroupId?groupId=0&shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getMenuItems: function () {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetMenuItemByCategoryGroupId?groupId=0&shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getMenuItemByCategory: function (categoryId) {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetMenuItemsByCategory?categoryId=").concat(categoryId).concat("&shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getPaymentMonths: function () {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetPaymentMonths?shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getPaymentYears: function () {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetPaymentYears?shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                submitOrder: function (myOrder) {
                    return rest({ method: 'POST', url: websiteUrl.concat("/api/ShopApi/SubmitCustomerOrder?shopId=").concat(shopIndaSoftId), data: myOrder, headers: config.headers });
                },
                takePayment: function (payment) {
                    return rest({ method: 'POST', url: websiteUrl.concat("/api/ShopApi/TakePayment?shopId=").concat(shopIndaSoftId), data: payment, headers: config.headers });
                },
                getHelpUrl: function () {
                    return websiteUrl.concat("/home/helpclient");

                },
              
                setShopId: function (shopId) {
                    shopIndaSoftId = shopId;
                },
                setWebUrl: function (webUrl) {
                    websiteUrl = webUrl;
                }


            }

        })
        .factory('utilService', function () {

            return {
                isUndefinedOrNull: function (val) {
                    return angular.isUndefined(val) || val === null;
                },
                newGuid: function () {
                    function s4() {
                        return Math.floor((1 + Math.random()) * 0x10000)
                            .toString(16)
                            .substring(1);
                    }

                    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                        s4() + '-' + s4() + s4() + s4();
                },
                newRef: function () {
                    function s4() {
                        return Math.floor((1 + Math.random()) * 0x10000)
                            .toString(16)
                            .substring(1);
                    }

                    return s4() + s4();
                },

                storeToLocalStorage: function (storekey, storeObject) {
                    try {
                        var storage = window.localStorage;
                        storage.setItem(storekey, angular.toJson(storeObject));
                    } catch (e) {
                        console.log(e);
                    }

                },
                removeToLocalStorage: function (storekey) {
                    try {
                        var storage = window.localStorage;
                        storage.removeItem(storekey);
                    } catch (e) {
                        console.log(e);
                    }

                },
                loadLocalStorage: function (storekey) {
                    try {
                        var storage = window.localStorage;
                        var value = storage.getItem(storekey);
                        return angular.fromJson(value);
                    } catch (e) {
                        console.log(e);
                        return {};
                    }
                }

            };

        })
        .factory('orderDataService', function ($q, $ionicPlatform) {
            var paymentItem = {
                    amount: 0,
                    paymentRef:""
                }

          

            return {
                getPayment: function () {
                    return paymentItem;
                },
                setPayment: function (setamount, setpaymentref) {

                    paymentItem.amount = setamount;
                    paymentItem.paymentRef = setpaymentref;
                }

            };

        });


})();

