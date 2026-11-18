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
