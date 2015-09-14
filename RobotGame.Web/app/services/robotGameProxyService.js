'use strict';
app.factory('robotGameProxyService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    return {
        getRobotPlayersList : function() {
        	return $http.get(serviceBase + 'api/robotGameProxy/robots').then(function (results) {
                return results;
            });
        }
    }
}]);


