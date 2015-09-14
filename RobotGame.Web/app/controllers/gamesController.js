'use strict';
app.controller('gamesController', ['$scope', 'hotkeys', 'gamesService', function ($scope, hotkeys, gamesService) {

    $scope.grid = [
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,
        1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,
        1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,
        1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,
        1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,
        1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,
        1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
        1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
        1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
        1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
        1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
        1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,
        1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,
        1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,
        1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,
        1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,
        1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,
        1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
    ];

    $scope.loadBoardstate = function() {
        $scope.tiles = $scope.activeGame.states[$scope.activeGame.turn].robots;
        $scope.editing = $scope.activeGame.needsMove && $scope.activeGame.turn === $scope.activeGame.states.length - 1;
    };

    $scope.refresh = function() {

        $scope.activeGame = {
            turn: 0,
            states: [],
            ignoreControls: true
        };

        gamesService.getGamesList().then(function (results) {
        	$scope.games = results.data;

            for (var i in $scope.games) {
                var game = $scope.games[i];
                game.states.splice(0, 0, {});
                (function (g, ii) {
                    return function () {
                        $scope.$watch('games[' + ii + '].isOpen', function (isOpen) {
                            if (isOpen) {
                                $scope.loadGame(g);
                            }
                        });
                    }
                })(game, i)();
            }
        }, function (error) {
            //alert(error.data.message);
        });

        $scope.tiles = [];
        $scope.editing = false;
    }

    $scope.refresh();

    $scope.loadGame = function(game) {
        $scope.activeGame = game;
        game.turn = game.states.length - 1;
        game.myScore = 0;
        game.enemyScore = 0;
		for (var i in game.states[game.turn].robots) {
			var bot = game.states[game.turn].robots[i];
			if (bot.good) game.myScore++;
			else game.enemyScore++;
		}
        $scope.loadBoardstate();
    }

    $scope.next = function () {
        if ($scope.activeGame.ignoreControls) return;
        if ($scope.activeGame.turn === $scope.activeGame.states.length - 1) {
            return;
        }
        $scope.activeGame.turn = $scope.activeGame.turn + 1;
        $scope.loadBoardstate();
    }

    $scope.prev = function () {
        if ($scope.activeGame.ignoreControls) return;
        if ($scope.activeGame.turn === 1) {
            return;
        }
        $scope.activeGame.turn = $scope.activeGame.turn - 1;
        $scope.loadBoardstate();
    }

    $scope.last = function () {
        if ($scope.activeGame.ignoreControls) return;
        if ($scope.activeGame.turn === $scope.activeGame.states.length - 1) {
            return;
        }
        $scope.activeGame.turn = $scope.activeGame.states.length - 1;
        $scope.loadBoardstate();
    }

    function _rotate(action) {
        switch (action) {
            case 'AU':
                return 'AR';
            case 'AR':
                return 'AD';
            case 'AD':
                return 'AL';
            case 'AL':
                return 'MU';
            case 'MU':
                return 'MR';
            case 'MR':
                return 'MD';
            case 'MD':
                return 'ML';
            case 'ML':
                return 'EX';
            case 'EX':
                return 'BL';
            default:
                return 'AU';
        }
    }

    $scope.rotate = function(t) {
        t.action = _rotate(t.action);
    }

    $scope.submitWarning = function() {
        for (var i in $scope.activeGame.states[$scope.activeGame.states.length - 1].robots) {
            var bot = $scope.activeGame.states[$scope.activeGame.states.length - 1].robots[i];
            if (bot.good && !bot.action) return true;
        }
        return false;
    }

    $scope.waiting = function() {
        return $scope.activeGame.turn === $scope.activeGame.states.length - 1 && !$scope.activeGame.needsMove;
    }

    $scope.submit = function() {
        // POST

        $scope.activeGame.needsMove = false;
        $scope.editing = false;
    }

    hotkeys.bindTo($scope)
        .add({ combo: 'right', callback: $scope.next })
        .add({ combo: 'left', callback: $scope.prev })
        .add({ combo: 'end', callback: $scope.last });
}]);