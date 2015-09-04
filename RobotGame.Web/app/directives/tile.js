app.directive('tile', function() {



    return {
        restrict: 'A',
        scope: {
            ngModel: '=',
            editing: '=',
            rotate: '='
        },
        templateUrl: 'app/templates/grid/tile.html'
    };
});