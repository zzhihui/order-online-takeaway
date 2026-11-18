var app = angular.module('webapp', ['webapp.controller', 'webapp.service', 'ngRoute']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    //Home 
    $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "/Demo/Home/Home"
    });


    //Order 
    $routeProvider.when("/order", {
        controller: "orderController",
        templateUrl: "/Demo/order/Index"
    });

    $routeProvider.when("/order/category", {
        controller: "orderController",
        templateUrl: "/Demo/order/category"
    });

    $routeProvider.when("/order/categoryitem", {
        controller: "orderController",
        templateUrl: "/Demo/order/categoryitem"
    });

    $routeProvider.when("/order/additemtobasket", {
        controller: "orderController",
        templateUrl: "/Demo/order/additemtobasket"
    });

    $routeProvider.when("/order/completeorder", {
        controller: "orderController",
        templateUrl: "/Demo/order/completeorder"
    });

    $routeProvider.when("/order/deliveryarea", {
        controller: "orderController",
        templateUrl: "/Demo/order/deliveryarea"
    });

    $routeProvider.when("/genericpayment", {
        controller: "orderController",
        templateUrl: "/Demo/order/genericpayment"
    });
    $routeProvider.when("/genericpaymentresult", {
        controller: "orderController",
        templateUrl: "/Demo/order/genericpaymentresult"
    });

    $routeProvider.when("/help", {
        controller: "orderController",
        templateUrl: "/Demo/order/help"
    });

    $routeProvider.when("/order/takepayment", {
        controller: "orderController",
        templateUrl: "/Demo/order/takepayment"
    });

    $routeProvider.when("/order/takepaymentresult", {
        controller: "orderController",
        templateUrl: "/Demo/order/takepaymentresult"
    });

    //Admin 
    $routeProvider.when("/admin/login", {
        controller: "adminController",
        templateUrl: "/Demo/account/login"
    });

    $routeProvider.when("/admin", {
        controller: "adminController",
        templateUrl: "/Demo/admin/index"
    });

    $routeProvider.when("/admin/killorderx7be", {
        controller: "adminController",
        templateUrl: "/Demo/admin/killorderx7be"
    });


    $routeProvider.when("/admin/menuitem", {
        controller: "menuitemController",
        templateUrl: "/Demo/menuitem/list"
    });


    $routeProvider.when("/admin/menuitem/category", {
        controller: "menuitemController",
        templateUrl: "/Demo/menuitem/category"
    });

    $routeProvider.when("/admin/menuitem/categoryitem", {
        controller: "menuitemController",
        templateUrl: "/Demo/menuitem/categoryitem"
    });

    $routeProvider.when("/admin/menuitem/categoryitemdelete", {
        controller: "menuitemController",
        templateUrl: "/Demo/menuitem/categoryitemdelete"
    });

    $routeProvider.when("/admin/menuitem/categoryitemedit", {
        controller: "menuitemController",
        templateUrl: "/Demo/menuitem/categoryitemedit"
    });


    $routeProvider.when("/admin/onlinedash", {
        controller: "onlinedashController",
        templateUrl: "/Demo/onlinedash"
    });


    $routeProvider.when("/admin/shopuser", {
        controller: "shopuserController",
        templateUrl: "/Demo/shopuser/list"
    });

    $routeProvider.when("/admin/shopuser/create", {
        controller: "shopuserController",
        templateUrl: "/Demo/shopuser/create"
    });

    $routeProvider.when("/admin/shopuser/edit", {
        controller: "shopuserController",
        templateUrl: "/Demo/shopuser/edit"
    });

    $routeProvider.when("/admin/shopuser/delete", {
        controller: "shopuserController",
        templateUrl: "/Demo/shopuser/delete"
    });

    $routeProvider.when("/admin/report", {
        controller: "reportController",
        templateUrl: "/Demo/report/list"
    });

    $routeProvider.when("/admin/report/dailyreport", {
        controller: "reportController",
        templateUrl: "/Demo/report/dailyreport"
    });

    $routeProvider.when("/admin/report/multiplereport", {
        controller: "reportController",
        templateUrl: "/Demo/report/multiplereport"
    });

    $routeProvider.when("/admin/report/nodatereport", {
        controller: "reportController",
        templateUrl: "/Demo/report/nodatereport"
    });
    

    //Error
    $routeProvider.when("/error", {
        controller: "errorController",
        templateUrl: "/Demo/error/index"
    });

    $routeProvider.when("/error404", {
        controller: "errorController",
        templateUrl: "/Demo/error/error404"
    });
   
    $routeProvider.otherwise({
        redirectTo: "/"
    });
    // Specify HTML5 mode (using the History APIs) or HashBang syntax.
    $locationProvider.html5Mode(false).hashPrefix('!');
}]);
