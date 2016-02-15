'use strict'
/*Application Angular Services*/

//var fsAppServices = angular.module('fsAppServices', ['ngResource']);

//fsAppServices.factory('FSREST', ['$resource',
//	function ($resource) {
//		return $resource('/////', {}, {
//			query: {method: 'GET', }
//		});
//	}]);


var FSAServices = angular.module('FSAServices', []);
function mainService($resource) {

}

mainService.$inject = ['$resource'];

FSAServices.service('mainService',mainService);