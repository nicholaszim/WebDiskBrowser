'use strict';

/* Application AngularJS Controllers*/

var fsAppControllers = angular.module('fsAppControllers', []);

//fsAppControllers.controller('FileSystemCtrl', ['$scope', 'FSREST',
//	function ($scope, FSREST) {
//		$scope.FileSysInfo = FSREST.query();
//	}]);
// defining controller as function for future minification.
function FileSystemCtrl($scope, $http) {
	/*do stuff with scopes here*/
}

FileSystemCtrl.$inject = ['$scope', '$http'];
fsAppControllers.controller('FileSystemCtrl', FileSystemCtrl);