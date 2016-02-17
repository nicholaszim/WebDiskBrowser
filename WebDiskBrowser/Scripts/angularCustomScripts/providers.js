'use strict';

var FSAProviders = angular.module('FSAProviders', ['angular-loading-bar']).
	config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
		cfpLoadingBarProvider.includeSpinner = true;
	}]);