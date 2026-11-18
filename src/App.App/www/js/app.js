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
        .state('tab.category', {
            url: '/category',
            views: {
                'tab-category': {
                    templateUrl: 'templates/category.html',
                    controller: 'CategoryCtrl'
                }
            }
        })
        .state('tab.categoryitem', {
            url: '/category/:categoryid',
            views: {
                'tab-category': {
                    templateUrl: 'templates/categoryitem.html',
                    controller: 'CategoryItemCtrl'
                }
            }
        })
        .state('tab.categoryitemselection', {
            cache: false,
            url: '/categoryitem/:menuitemid',
            views: {

                'tab-category': {
                    templateUrl: 'templates/categoryitemselection.html',
                    controller: 'CategoryItemSelectionCtrl'
                }
            }
        })
        .state('tab.basket', {
            url: '/basket',
            views: {
                'tab-basket': {
                    cache: false,
                    templateUrl: 'templates/basket.html',
                    controller: 'BasketCtrl'
                }
            }
        })
        .state('tab.more', {
            url: '/more',
            views: {
                'tab-more': {
                    templateUrl: 'templates/more.html',
                    controller: 'MoreCtrl'
                }
            }
        })
        .state('tab.help', {
            url: '/help',
            views: {
                'tab-more': {
                    templateUrl: 'templates/help.html',
                    controller: 'HelpCtrl'
                }
            }
        })
        .state('tab.aboutus', {
                url: '/aboutus',
                views: {
                    'tab-more': {
                        templateUrl: 'templates/aboutus.html',
                        controller: 'AboutUsCtrl'
                    }
                }
            })
        .state('tab.deliveryarea', {
            url: '/deliveryarea',
            views: {
                'tab-more': {
                    templateUrl: 'templates/deliveryarea.html',
                    controller: 'DeliveryareaCtrl'
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
        .state('tab.payment', {
            url: '/payment',
            views: {
                'tab-basket': {
                    templateUrl: 'templates/payment.html',
                    controller: 'PaymentCtrl'
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

        ;

        // if none of the above states are matched, use this as the fallback
        $urlRouterProvider.otherwise('/tab/dash');

    });


})();
(function () {
    'use strict';

    angular.module('starter.controllers', [])


    .controller('TabCtrl', function ($scope, $state, $rootScope, utilService) {

        $scope.$on('basket-added', function (event, args) {

            if (!utilService.isUndefinedOrNull($rootScope.myOrder) && !utilService.isUndefinedOrNull($rootScope.myOrder.orderItems)) {
                $rootScope.number = $rootScope.myOrder.orderItems.length;
            } else {
                $rootScope.number = null;
            }
            
        });

       


        $scope.goToDash = function () {
            $state.go('tab.dash');
        }

        $scope.goToMenu = function () {
            $state.go('tab.category');
        }

        $scope.goToBasket = function () {
            $state.go('tab.basket');
        }

        $scope.goToMore = function () {
            $state.go('tab.more');
        }

    })

    //home
    .controller('DashCtrl', function ($scope, wsShopService, $sce) {



        wsShopService.getShopInfo().then(function (response) {
            $scope.shopInfo = response.data;
            $scope.shopNews = $scope.shopInfo.shopNews;
            $scope.googleMap = $sce.trustAsResourceUrl($scope.shopInfo.googleMap);
        });

    })

    //Category
    .controller('CategoryCtrl', function ($scope, wsShopService, $filter, $rootScope) {

        $scope.toggleGroup = function (group) {
            group.show = !group.show;
        };
        $scope.isGroupShown = function (group) {
            return group.show;
        };
        
        wsShopService.getCategories().then(function (response) {
            $scope.categories = $filter('filter')(response.data, { isInternal: '!true' });
            wsShopService.getMenuItems().then(function (response) {
                $scope.categoryItems = $filter('filter')(response.data, { excludeOnline: '!true' });
                $rootScope.categoryItems = $scope.categoryItems;

                $scope.groups = [];
                for (var i = 0; i < $scope.categories.length; i++) {
                    $scope.groups[i] = {
                        id: $scope.categories[i].id,
                        name: $scope.categories[i].name,
                        webImageUrl: $scope.categories[i].webImageUrl,
                        description: $scope.categories[i].description,
                        items: []
                    };
                    //console.error($scope.groups[i]);

                    $scope.groups[i].items = $filter('filter')($scope.categoryItems, { menuItemCategoryId: $scope.groups[i].id });
                    //console.error($scope.groups[i].items);
                    
                }



            
            });
        });
    })

    //CategoryItem
    .controller('CategoryItemCtrl', function ($rootScope, $scope, $stateParams, wsShopService, $filter) {

        wsShopService.getMenuItemByCategory($stateParams.categoryid).then(function (response) {
            $scope.categoryItems = $filter('filter')(response.data, { excludeOnline: '!true' });
            $rootScope.categoryItems = $scope.categoryItems;
        });

    })

    //Select Item
    .controller('CategoryItemSelectionCtrl', function ($rootScope, $scope, $stateParams, $location, $ionicHistory, orderDataService, wsShopService, utilService, $state) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });

        $scope.pickercount = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];
        $scope.dishCount = 0;

        wsShopService.getMenuItembyid($stateParams.menuitemid).then(function (response) {
            $scope.menuItem = response.data;
        });

        $scope.addItemToBasket = function () {

            //Create or Update New Order
            if (utilService.isUndefinedOrNull($rootScope.myOrder)) {
                $rootScope.myOrder = orderDataService.getOrder();
            }

            //Save Option Extra(by Default)
            //Add Item to Basket
            if (utilService.isUndefinedOrNull($rootScope.myOrder.orderItems)) {
                $rootScope.myOrder.orderItems = [];
            }

            for (var counter = 0; counter < $scope.dishCount; counter++) {
                var newItem = angular.copy($scope.menuItem);
                newItem.guid = utilService.newGuid();

                //Calculate Cost Extra
                angular.forEach(newItem.subItemSelections, function (item) {
                    newItem.price += item.selected.price;
                });

                $rootScope.myOrder.orderItems.push(newItem);
            }


            //            $state.go('tab.basket');
            $ionicHistory.goBack();

            $rootScope.$broadcast('basket-added');

        }

    })

    //Basket
    .controller('BasketCtrl', function ($q, $rootScope, $scope, $ionicHistory, wsShopService, utilService, orderDataService, $location, $state) {

        wsShopService.getDeliveryOption().then(function (response) {
            $scope.deliveryOptions = response.data;
            $scope.deliveryOptionSelected = $scope.deliveryOptions[0];
            wsShopService.getDeliveryArea().then(function (response) {
                $scope.deliveryAreas = response.data;
                $scope.dareaSelected = $scope.deliveryAreas[0];
            });
        });

        //get delivery area
        $scope.$on('$ionicView.enter', function () {

            $scope.cacheCustomer = utilService.loadLocalStorage("cacheCustomer");

            if (!utilService.isUndefinedOrNull($scope.cacheCustomer) && !utilService.isUndefinedOrNull($rootScope.myOrder)) {
                $rootScope.myOrder.contactName = $scope.cacheCustomer.name;
                $rootScope.myOrder.phoneNumber = $scope.cacheCustomer.phone;
                $rootScope.myOrder.email = $scope.cacheCustomer.email;
                $rootScope.myOrder.address = $scope.cacheCustomer.address;
            }


            wsShopService.getShopStatus().then(function (response) {
                $scope.shopInfo = response.data;
                if (utilService.isUndefinedOrNull($rootScope.myOrder)) {
                    $scope.hasMinOrder = !utilService.isUndefinedOrNull($scope.shopInfo.minOrder) && $scope.shopInfo.minOrder > 0;
                    $scope.subtotal = orderDataService.calculateTotal($rootScope.myOrder);
                    $scope.discountApplied = !utilService.isUndefinedOrNull($scope.discountAmount) && $scope.discountAmount > 0;
                } else {

                    $scope.subtotal = orderDataService.calculateTotal($rootScope.myOrder);
                    wsShopService.discountOrder($rootScope.myOrder).then(
                        function (response) {

                            $rootScope.myOrder = response.data;
                            $scope.hasMinOrder = !utilService.isUndefinedOrNull($scope.shopInfo.minOrder) && $scope.shopInfo.minOrder > 0;
                            $scope.subtotal = $rootScope.myOrder.orderTotal;
                            $scope.discountAmount = $rootScope.myOrder.orderOriginalTotal - $rootScope.myOrder.orderTotal;
                            $scope.discountApplied = !utilService.isUndefinedOrNull($scope.discountAmount) && $scope.discountAmount > 0;
                        });
                }
            });
        });

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });

        $scope.orderSubmit = function () {

            var customer = {
                name: "",
                phone: "",
                email: "",
                address: ""
            }

            customer.name = $rootScope.myOrder.contactName;
            customer.phone = $rootScope.myOrder.phoneNumber;
            customer.email = $rootScope.myOrder.email;
            customer.address = $rootScope.myOrder.address;

            utilService.storeToLocalStorage("cacheCustomer", customer);

            $rootScope.myOrder.deliveryType = $scope.deliveryOptionSelected;

            //App Order
            $rootScope.myOrder.orderSource = 2;

            if ($scope.deliveryOptionSelected.name == 'For Delivery') {
                if (utilService.isUndefinedOrNull($rootScope.myOrder.deliveryCharge) || $rootScope.myOrder.deliveryCharge == 0) {
                    $rootScope.myOrder.deliveryCharge = $scope.dareaSelected.deliveryCost;
                }

            } else {
                $rootScope.myOrder.deliveryCharge = 0;
            }

            $rootScope.myOrder.orderRef = utilService.newRef();

            if ($scope.shopInfo.takePayment) {
                $state.go("tab.payment");
            } else {
                wsShopService.submitOrder($rootScope.myOrder).then(
                    function (response) {
                        $rootScope.lastOrder = $rootScope.myOrder;
                        $rootScope.myOrder = null;
                        $state.go("tab.completeorder");
                    });

            }

        }

        $scope.removeItemFromBasket = function (index) {

            $rootScope.myOrder.orderItems.splice(index, 1);
            $scope.subtotal = orderDataService.calculateTotal($rootScope.myOrder);
            wsShopService.discountOrder($rootScope.myOrder).then(
                function (response) {

                    $rootScope.myOrder = response.data;
                    $scope.subtotal = $rootScope.myOrder.orderTotal;
                    $scope.discountAmount = $rootScope.myOrder.orderOriginalTotal - $rootScope.myOrder.orderTotal;
                    $scope.discountApplied = !utilService.isUndefinedOrNull($scope.discountAmount) && $scope.discountAmount > 0;
                });

        }

        $scope.notvalidToSubmit = function () {

            return !$scope.shopInfo.isOpen || ($scope.hasMinOrder && $scope.shopInfo.minOrder > $scope.subtotal);
        }



    })

    //More Item
    .controller('MoreCtrl', function ($scope, wsShopService) {

        wsShopService.getShopInfo().then(function (response) {
            $scope.shopInfo = response.data;
        });

    })

    //About us 
    .controller('AboutUsCtrl', function ($scope, $cordovaAppVersion, $cordovaInAppBrowser, $ionicHistory) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });

        document.addEventListener("deviceready", function () {

            $cordovaAppVersion.getVersionNumber().then(function (version) {
                $scope.appVersion = version;
            });
            $cordovaAppVersion.getVersionCode().then(function (build) {
                $scope.appBuild = build;
            });
        });

        $scope.doWeb = function () {

            var options = {
                location: 'yes',
                clearcache: 'yes',
                toolbar: 'yes',
                closebuttoncaption: 'DONE?'
            };

            $cordovaInAppBrowser.open('http://www.indasoft.ie', '_blank', options)
            .then(function (event) {
                // success
            })
            .catch(function (event) {
                // error
            });
        }

    })

    .controller('HelpCtrl', function ($scope, wsShopService, $sce, $ionicHistory) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });

        $scope.weburl = $sce.trustAsResourceUrl(wsShopService.getHelpUrl());
    })

    //Delivery Area
    .controller('DeliveryareaCtrl', function ($scope, wsShopService, $ionicHistory) {

        wsShopService.getDeliveryArea().then(function (response) {
            $scope.deliveryAreas = response.data;
        });

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });


    })

    //Complete Order
    .controller('CompleteorderCtrl', function ($rootScope, $scope, $ionicHistory, $filter) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });
        $scope.$on('$ionicView.enter', function () {

            $scope.subTotal = $rootScope.lastOrder.orderTotal + $rootScope.lastOrder.deliveryCharge;
            $scope.deliveryTime = $filter('date')(getDeliveryTime(), 'HH:mm');
        });


        function getDeliveryTime() {
            var curDate = new Date();
            if ($rootScope.lastOrder.deliveryType.name == 'For Delivery') {
                curDate.setMinutes(curDate.getMinutes() + 45);
            } else {
                curDate.setMinutes(curDate.getMinutes() + 15);
            }

            return curDate;
        }



    })

    //Take payment
    .controller('PaymentCtrl', function ($scope, $ionicHistory, $rootScope, utilService, wsShopService, $state) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });

        $scope.$on('$ionicView.enter', function () {

            $scope.payment = {};
            $scope.orderTotal = $rootScope.myOrder.orderTotal;
            $scope.deliveryTotal = $rootScope.myOrder.deliveryCharge;
            $scope.amount = $rootScope.myOrder.orderTotal + $rootScope.myOrder.deliveryCharge;

        });

        $scope.paylater = function () {

            wsShopService.submitOrder($rootScope.myOrder).then(
                    function (response) {
                        $rootScope.lastOrder = $rootScope.myOrder;
                        $rootScope.myOrder = null;
                        $rootScope.$broadcast('basket-added');
                        $state.go("tab.completeorder");
                    });

        }



    })


    //Take payment
    .controller('PaymentCcCtrl', function ($scope, $ionicHistory, $rootScope, utilService, wsShopService, $state) {

        $scope.$on('$ionicView.leave', function () {
            $ionicHistory.clearHistory();
        });
        $scope.$on('$ionicView.enter', function () {

            $scope.payment = {};
            
            $scope.orderTotal = $rootScope.myOrder.orderTotal;
            $scope.deliveryTotal = $rootScope.myOrder.deliveryCharge;
            $scope.amount = $rootScope.myOrder.orderTotal + $rootScope.myOrder.deliveryCharge;

            $scope.cacheCards = utilService.loadLocalStorage("cacheCard");
            if (utilService.isUndefinedOrNull($scope.cacheCards)) {
                $scope.noCard = true;
            } else {
                $scope.noCard = false;
            }

        });

        wsShopService.getPaymentMonths().then(function (response) {
            $scope.paymentMonths = response.data;
            $scope.monthSelected = $scope.paymentMonths[0];
            wsShopService.getPaymentYears().then(function (response) {
                $scope.paymentYears = response.data;
                $scope.yearSelected = $scope.paymentYears[0];
            });

        });

        $scope.useothercard = function () {
            $scope.noCard = true;
            utilService.removeToLocalStorage("cacheCard");
        }


        $scope.takestoredpayment = function () {

            var card = utilService.loadLocalStorage("cacheCard");
            $scope.payment = {
                payment:
                {
                    cardName: "",
                    cardNumber: "",
                    cardCvc: "",
                    cardExpirationMonth: "",
                    cardExpirationYear: ""
                }
            };
            
            $scope.payment.payment.cardName = card.name;
            $scope.payment.payment.cardNumber = card.number;
            $scope.payment.payment.cardCvc = card.cvv;
            $scope.payment.payment.cardExpirationMonth = card.expiredate;
            $scope.payment.payment.cardExpirationYear = card.expireyear;
            
            $scope.payment.orderTotal = $rootScope.myOrder.orderTotal;
            $scope.payment.deliveryCharge = $rootScope.myOrder.deliveryCharge;
            $scope.payment.payment.amount = $scope.amount;

            wsShopService.takePayment($scope.payment.payment).then(
               function (response) {

                   var paymentResult = response.data;
                   //console.log(paymentResult);
                   if (paymentResult.isSuccess) {

                       $scope.showError = false;
                       $rootScope.myOrder.paymentRef = paymentResult.paymentRef;
                       wsShopService.submitOrder($rootScope.myOrder).then(
                       function (response) {
                           $rootScope.lastOrder = $rootScope.myOrder;
                           $rootScope.myOrder = null;
                           $rootScope.$broadcast('basket-added');
                           $state.go("tab.completeorder");
                       });
                   }
                   else {

                       $scope.showError = true;
                   }
               }
           );


        }


        $scope.takepayment = function () {

            $scope.payment.payment.cardExpirationMonth = $scope.monthSelected.id;
            $scope.payment.payment.cardExpirationYear = $scope.yearSelected.id;
            $scope.payment.orderTotal = $rootScope.myOrder.orderTotal;
            $scope.payment.deliveryCharge = $rootScope.myOrder.deliveryCharge;
            $scope.payment.payment.amount = $scope.amount;

            wsShopService.takePayment($scope.payment.payment).then(
                function (response) {

                    var paymentResult = response.data;
                    //console.log(paymentResult);
                    if (paymentResult.isSuccess) {

                        $scope.showError = false;
                        $rootScope.myOrder.paymentRef = paymentResult.paymentRef;
                        wsShopService.submitOrder($rootScope.myOrder).then(
                        function (response) {
                            $rootScope.lastOrder = $rootScope.myOrder;
                            $rootScope.myOrder = null;

                            var card = {
                                name: "",
                                number: "",
                                cvv: "",
                                expireyear: "",
                                expiredate: ""
                            }

                            card.name = $scope.payment.payment.cardName;
                            card.number = $scope.payment.payment.cardNumber;
                            card.expiredate = $scope.monthSelected.id;
                            card.expireyear = $scope.yearSelected.id;
                            card.cvv = $scope.payment.payment.cardCvc;
                            utilService.storeToLocalStorage("cacheCard", card);
                            $rootScope.$broadcast('basket-added');
                            $state.go("tab.completeorder");
                        });
                    }
                    else {

                        $scope.showError = true;
                    }
                }
            );




        }

    });


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

    var websiteUrl = 'https://indasoftkingfoodapi.azurewebsites.net';
    //var websiteUrl = 'http://localhost:49041';

    //TODO: localisation
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
                getShopStatus: function () {
                    return rest({method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetShopStatus?shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getCategories: function () {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetMenuItemCategoryByGroupId?groupId=0&shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getMenuItems: function () {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetMenuItemByCategoryGroupId?groupId=0&shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getMenuItembyid: function (menuItemId) {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetMenuItemById?id=").concat(menuItemId).concat("&shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getMenuItemByCategory: function (categoryId) {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetMenuItemsByCategory?categoryId=").concat(categoryId).concat("&shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                discountOrder: function (myOrder) {
                    return rest({ method: 'POST', url: websiteUrl.concat("/api/ShopApi/DiscountCustomerOrder?shopId=").concat(shopIndaSoftId), data: myOrder, headers: config.headers });
                },
                getDeliveryArea: function () {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetDeliveryArea?deliveryGroupId=1&shopId=").concat(shopIndaSoftId), headers: config.headers });
                },
                getDeliveryOption: function () {
                    return rest({ cache: true, method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetDeliveryOptionsByTime?shopId=").concat(shopIndaSoftId), headers: config.headers });
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


            function newGuid() {
                function s4() {
                    return Math.floor((1 + Math.random()) * 0x10000)
                        .toString(16)
                        .substring(1);
                }

                return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                    s4() + '-' + s4() + s4() + s4();
            }

            return {
                getOrder: function () {
                    var order = {
                        id: 0,
                        guid: newGuid()
                    };
                    return angular.copy(order);
                },
                calculateTotal: function (order) {

                    //Calculate Orginal Total
                    var totalValue = 0;
                    if (angular.isUndefined(order) || order == null)
                        return totalValue;

                    angular.forEach(order.orderItems, function (item) {
                        totalValue += item.price;
                    });

                    order.orderOriginalTotal = totalValue;
                    order.orderTotal = totalValue;

                    return totalValue;
                }

            };

        });





})();