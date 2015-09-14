'use strict';
app.factory('gamesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    return {
        getGamesList : function() {
            return $http.get(serviceBase + 'api/games').then(function (results) {
                return results;
            });
        },

		startHumanGame : function(name, success) {
			return $http.post(serviceBase + 'api/games/startHumanGame', { name: name })
				.then(success);
		},

    	getHumanOpponents : function() {
		    return $http.get(serviceBase + 'api/games/getHumanOpponents').then(function(results) {
			    return results;
		    });
	    },

    	setAccepting : function(acc, after) {
		    return $http.post(serviceBase + 'api/games/setReady', { ready: acc })
			    .then(after, after);
	    }
    }
}]);


