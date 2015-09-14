'use strict';
app.controller('newgameController', ['$scope', 'robotGameProxyService', 'gamesService', '$location', 'authService',
	function ($scope, robotGameProxyService, gamesService, $location, authService) {

		$scope.userName = authService.authentication.userName;

		gamesService.getHumanOpponents().then(function(response) {
			$scope.humans = _.map(response.data, function (r) { return { name: r, isSelf: r == $scope.userName } });
		});

		robotGameProxyService.getRobotPlayersList().then(function (response) {
			$scope.robots = response.data;
		});

		$scope.accepting = {
			value: false,
			prevValue: false,
			blocking: false
		};

		$scope.$watch('accepting.value', function (acc) {
			if (acc == $scope.accepting.prevValue) return;

			if ($scope.accepting.blocking) {
				$scope.accepting.value = $scope.accepting.prevValue;
				return;
			}

			$scope.accepting.blocking = true;

			gamesService.setAccepting(acc, function (response) {
				if (response.status != 200) {
					$scope.accepting.value = $scope.accepting.prevValue;
				}
				$scope.accepting.blocking = false;
				$scope.accepting.prevValue = $scope.accepting.value;

			});
		});

		$scope.filterData = {
			minRating: 2500,
			maxRating: 4000,
			name: ''
		};

		$scope.ratingFilter = function(robot) {
			if ($scope.filterData.minRating && robot.rating < $scope.filterData.minRating) return false;
			if ($scope.filterData.maxRating && robot.rating > $scope.filterData.maxRating) return false;
			return true;
		}

		$scope.startHumanGame = function(humanName) {
			gamesService.startHumanGame(humanName, function(response) {
				$location.path('/games');
			});
		}

		$scope.startRobotGame = function(robotId) {
		
		}
	}
]);