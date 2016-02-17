'use strict';
/*Application anularJS module*/

/*A main module with submodules it depends on*/
var mainModule = angular.module('FSApplication', [
	'angular-loading-bar',
	'FSAControllers',
	'FSAFilters',
	'ngSanitize',
	'FSAServices'
]);