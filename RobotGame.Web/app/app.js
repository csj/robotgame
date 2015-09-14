
var app = angular.module('AngularAuthApp', ['ui.bootstrap', 'ui.bootstrap.tpls', 'ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'cfp.hotkeys']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.when("/games", {
        controller: "gamesController",
        templateUrl: "/app/views/games.html"
    });

    $routeProvider.when("/newgame", {
        controller: "newgameController",
        templateUrl: "/app/views/newgame.html"
    });


    $routeProvider.otherwise({ redirectTo: "/home" });

});

var serviceBase = 'http://robotgame-api.azurewebsites.net/';
//var serviceBase = 'http://localhost:26264/';

app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


