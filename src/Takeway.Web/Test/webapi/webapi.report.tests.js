(function () {
    QUnit.config.testTimeout = 10000;

    module('Web API Result Tests');

    asyncTest('GetAllReportCategories return more at least 1 records',
            function () {
                $.ajax({
                    url: '/api/report/GetAllReportCategories',
                    dataType: 'json',
                    success: function (result) {
                        ok(result.length>0,true);
                        start();
                    },
                    error: function (result) {
                        ok(false, 'Failed with: ' + result.responseText);
                        start();
                    }
                });
            }
        );

}());
