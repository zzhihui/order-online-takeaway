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


