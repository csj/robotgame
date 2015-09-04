'use strict';
app.factory('gamesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    return {
        getGamesList : function() {
            return $http.get(serviceBase + 'api/games').then(function (results) {
                return results;
            });

            return [
                {
                    enemy: "Peetee",
                    myScore: 5,
                    enemyScore: 5,
                    needsMove: true,
                    states: [
                        {},
                        {
                            robots: [
                                {
                                    good: true,
                                    health: 50,
                                    row: 1,
                                    col: 11,
                                    action: 'ML'
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 2,
                                    col: 12,
                                    action: 'ML'
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 3,
                                    col: 12,
                                    action: 'AR'
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 4,
                                    col: 11,
                                    action: 'MD'
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 4,
                                    col: 13,
                                    action: 'EX'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 3,
                                    col: 13,
                                    action: 'AL'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 3,
                                    col: 14,
                                    action: 'MD'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 5,
                                    col: 13,
                                    action: 'EX'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 5,
                                    col: 15,
                                    action: 'BL'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 6,
                                    col: 14,
                                    action: 'AL'
                                }
                            ]
                        },
                        {
                            robots: [
                                {
                                    good: true,
                                    health: 50,
                                    row: 1,
                                    col: 10
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 2,
                                    col: 11
                                },
                                {
                                    good: true,
                                    health: 42,
                                    row: 3,
                                    col: 12
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 5,
                                    col: 11
                                },
                                {
                                    good: false,
                                    health: 41,
                                    row: 3,
                                    col: 13
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 4,
                                    col: 14
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 5,
                                    col: 15
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 6,
                                    col: 14
                                }
                            ]
                        }
                    ]
                },
                {
                    name: "Game 2",
                    needsMove: false,
                    enemy: "WhiteHalmos",
                    states: [
                        { robots: [] }, {
                            robots: [
                                {
                                    good: false,
                                    health: 50,
                                    row: 6,
                                    col: 14
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 1,
                                    col: 10,
                                    action: 'AD'
                                }
                            ]
                        }
                    ]
                }
            ];
        }
    }
}]);


