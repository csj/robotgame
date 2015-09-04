app.directive('overlay', function () {
    return {
        restrict: 'A',
        scope: {
            ngModel: '='
        },
        templateUrl: 'app/templates/grid/overlay.html'
    };
});