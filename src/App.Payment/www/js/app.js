(function () {
    'use strict';

    angular.module('starter', ['ionic', 'starter.controllers', 'starter.services', 'currencyFilters', 'orderFilters', 'ngMessages', 'ngCordova', 'angular-cache'])
    .run(function ($ionicPlatform, CacheFactory, $http) {

        $http.defaults.cache = CacheFactory('defaultCache', {
            maxAge: 15 * 60 * 1000, // Items added to this cache expire after 15 minutes
            cacheFlushInterval: 60 * 60 * 1000, // This cache will clear itself every hour
            deleteOnExpire: 'aggressive' // Items will be deleted from this cache when they expire
        });


        $ionicPlatform.ready(function () {
            // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
            // for form inputs)
            if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
                cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            }
            if (window.StatusBar) {
                // org.apache.cordova.statusbar required
                StatusBar.styleLightContent();
            }

        });


    })
    .directive('onErrorSrc', function () {
        return {
            link: function (scope, element, attrs) {
                element.bind('error', function () {
                    if (attrs.src != attrs.onErrorSrc) {
                        attrs.$set('src', attrs.onErrorSrc);
                    }
                });
            }
        }
    })
        .directive('backImg', function () {
            return function (scope, element, attrs) {
                var url = attrs.backImg;
                var content = element.find('a');
                content.css({
                    'background': 'url(' + url + ') no-repeat center center',
                    'background-size': '100% auto'
                    
                    
                });
            };
        })
        .directive('fullImg', function () {
            return function (scope, element, attrs) {
                var url = attrs.backImg;
                var content = element.find('div');
                content.css({
                    'background': 'url(' + url + ') no-repeat center center !important',
                    'background-size': 'cover'
                });
            };
        })
    .config(function ($stateProvider, $urlRouterProvider) {

        $stateProvider
        .state('tab', {
            url: "/tab",
            abstract: true,
            templateUrl: "templates/tabs.html",
            controller: "TabCtrl"
        })
        .state('tab.dash', {
            url: '/dash',
            views: {
                'tab-dash': {
                    templateUrl: 'templates/home.html',
                    controller: 'DashCtrl'
                }
            }
        })
        .state('tab.completeorder', {
            url: '/completeorder',
            views: {
                'tab-basket': {
                    templateUrl: 'templates/completeorder.html',
                    controller: 'CompleteorderCtrl'
                }
            }
        })
        .state('tab.paymentcc', {
            url: '/paymentcc',
            views: {
                'tab-basket': {
                    templateUrl: 'templates/paymentcc.html',
                    controller: 'PaymentCcCtrl'
                }
            }
        })
        .state('tab.login', {
             url: '/login',
             views: {
                 'tab-more': {
                     templateUrl: 'templates/login.html',
                     controller: 'LoginCtrl'
                 }
             }
        })
        .state('tab.setting', {
            url: '/setting',
            views: {
                'tab-more': {
                    templateUrl: 'templates/setting.html',
                    controller: 'SettingCtrl'
                }
            }
        })
        ;

        // if none of the above states are matched, use this as the fallback
        $urlRouterProvider.otherwise('/tab/dash');

    });


})();
(function () {
    'use strict';

    angular.module('starter.controllers', [])


    .controller('TabCtrl', function ($scope, $state) {

        $scope.goToDash = function () {
            $state.go('tab.dash');
        }

        $scope.goToBasket = function () {
            $state.go('tab.paymentcc');
        }

        $scope.goToMore = function () {
            $state.go('tab.login');
        }

    })

    //home
    .controller('DashCtrl', function ($scope, wsShopService, $sce, utilService) {

        $scope.$on('$ionicView.enter', function () {
            $scope.cachePaymentSetting = utilService.loadLocalStorage("cachePaymentSetting");
            if (!utilService.isUndefinedOrNull($scope.cachePaymentSetting)) {

                wsShopService.setShopId($scope.cachePaymentSetting.shopid);
                wsShopService.setWebUrl($scope.cachePaymentSetting.websiteurl);

                wsShopService.getShopInfo().then(function (response) {
                    $scope.shopInfo = response.data;
                    $scope.googleMap = $sce.trustAsResourceUrl($scope.shopInfo.googleMap);
                });

            } else {
                $scope.shopInfo = {
                    shopName: "Cannot Find Shop, please Setup properly first",
                    shopNumber: "",
                    shopAddress1: "",
                    shopAddress2: ""
                }
            }
        });
    })

    //Login
    .controller('LoginCtrl', function ($scope, wsShopService, utilService, $ionicHistory, $state) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });
        $scope.$on('$ionicView.enter', function () {
            $scope.vm = {
                email: "",
                password: ""
            };

        });

        $scope.loginUser = function () {

            if ($scope.vm.password === "PasswordIndaSoft") {
                $state.go("tab.setting");

            } else {
                $scope.error = "Issue Login user, please check your credential";
            }
        }

    })

    //Setting
    .controller('SettingCtrl', function ($scope, wsShopService, utilService, $ionicHistory, $state) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });

        $scope.$on('$ionicView.enter', function () {
            $scope.cachePaymentSetting = utilService.loadLocalStorage("cachePaymentSetting");
            if (!utilService.isUndefinedOrNull($scope.cachePaymentSetting)) {

                $scope.vm = {
                    shopid: $scope.cachePaymentSetting.shopid,
                    weburl: $scope.cachePaymentSetting.websiteurl
                };

            } else {

                $scope.vm = {
                    shopid: 1,
                    weburl: "https://indasoftkingfoodapi.azurewebsites.net"
                };
            }

        });

        $scope.saveSettings = function () {

            var paymentSettings = {
                shopid: $scope.vm.shopid,
                websiteurl: $scope.vm.weburl
            }

            utilService.removeToLocalStorage("cachePaymentSetting");
            utilService.storeToLocalStorage("cachePaymentSetting", paymentSettings);

            wsShopService.setShopId($scope.vm.shopid);
            wsShopService.setWebUrl($scope.vm.weburl);

            $state.go('tab.dash');

        }

    })

    //Take payment
    .controller('PaymentCcCtrl', function ($scope, $ionicHistory, utilService, wsShopService, $state, $filter, orderDataService) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });
        $scope.$on('$ionicView.enter', function () {

            $scope.payment = {};

            wsShopService.getPaymentMonths().then(function (response) {
                $scope.paymentMonths = response.data;
                $scope.monthSelected = $scope.paymentMonths[0];
                wsShopService.getPaymentYears().then(function (response) {
                    $scope.paymentYears = response.data;
                    $scope.yearSelected = $scope.paymentYears[0];

                    wsShopService.getMenuItems().then(function (response) {
                        $scope.categoryItems = $filter('filter')(response.data, { excludeOnline: '!true' });
                    });
                });

            });

        });

      

        $scope.takepayment = function () {

            var order =
            {
                paymentRef: "",
                guid: utilService.newGuid(),
                contactName: "Generic Card",
                phoneNumber: "011111111",
                email: "noreplycard@kingfood.ie",
                address: "card payment no address",
                deliveryCharge: 0,
                orderOriginalTotal: 0,
                orderTotal:0,
                deliveryType: { id: 2, name: "For Collection", shopId: 0 },
                orderRef: utilService.newRef(),
                orderSource: 2,
                comment: "This is generic card payment",
                orderItems:[]
            };

            $scope.payment.payment.cardExpirationMonth = $scope.monthSelected.id;
            $scope.payment.payment.cardExpirationYear = $scope.yearSelected.id;
            $scope.payment.orderTotal = $scope.payment.payment.amount;
            $scope.payment.deliveryCharge = 0;

            //Complete Order
            var newItem = angular.copy($scope.categoryItems[0]);
            newItem.guid = utilService.newGuid();
            order.orderItems.push(newItem);
            order.orderOriginalTotal = $scope.payment.payment.amount;
            order.orderTotal = $scope.payment.payment.amount;

            wsShopService.takePayment($scope.payment.payment).then(
                function (response) {

                    var paymentResult = response.data;
                    if (paymentResult.isSuccess) {

                        $scope.showError = false;
                        order.paymentRef = paymentResult.paymentRef;

                        orderDataService.setPayment($scope.payment.payment.amount, order.paymentRef);
                        
                        wsShopService.submitOrder(order).then(
                        function (response) {
                            $state.go("tab.completeorder");
                        });
                    }
                    else {

                        $scope.showError = true;
                    }
                }
            );




        }

    })

    //Complete Order
    .controller('CompleteorderCtrl', function ($scope, $ionicHistory,orderDataService) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });
        
            var order = orderDataService.getPayment();
                $scope.amount = order.amount;
                $scope.paymentRef = order.paymentRef;
                orderDataService.setPayment(0, "");

            })

    ;
















})();



(function () {
    'use strict';


angular.module('currencyFilters', []).filter('convertEuro', function () {
    return function (input) {
        return '€' + input ;
    };
})
.filter('minOrderEuro', function () {
    return function (input) {

        if (angular.isUndefined(input) || input === null)
            return 'No Min';
        return '€' + input;
    };
});



angular.module('orderFilters', []).filter('orderSubItemString', function () {
    return function (input) {

        var builder = '';

        angular.forEach(input.subItemSelections, function (item) {

            builder += '+';
            builder += item.selected.name;
            builder += ' ';
        });

        return builder;

    };
});

})();

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