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