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


