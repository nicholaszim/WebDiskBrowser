'use strict';

/* Application AngularJS Controllers*/
var FSAControllers = angular.module('FSAControllers', []);

function mainController($scope, $http) {
	$scope.sysData = {
		searchPath: "",
		disks: "",
		entries: "",
		files: "",
		folders: "",
		files10mb: 1,
		files50mb: 1,
		files100mb: 1,
	};
	$scope.httpData = {
		//searchPath: '',
		url: {
			getDefault: "/api/FileSystem/default",
			getAll: '/api/FileSystem/new?path='
		}
	}
	$scope.errorData = {
		errorMessage: "",
		errorCode: 0
	}

	/*Helper to get scope for children in repeaters and stuff*/
	$scope.getCtrlScope = function () {
		return $scope;
	};

	/*all controller functions here*/
	$scope.functions = {
		getInit: function() {
			$http({ method: 'GET', url: $scope.httpData.url.getDefault }).
			success(function (data, status) {
				$scope.sysData.disks = data.Drives;
				$scope.sysData.entries = data.Entries;
				$scope.sysData.files = data.Files;
				$scope.sysData.folders = data.Folders;
				$scope.sysData.searchPath = data.DirectoryPath;
				$scope.sysData.files10mb = data.Count10mb;
				$scope.sysData.files50mb = data.Count50mb;
				$scope.sysData.files100mb = data.Count100mb;
			}).
			error(function (data, status) {
				$scope.errorData.errorCode = status;
				$scope.errorData.errorMessage = data;
				document.getElementById('alertMe').style.display = 'list-item';
			});
		},

		/*gets a data for ui using specified path as input.*/
		getContext: function (e) {
			document.getElementById('alertMe').style.display = 'none';
			$http({ method: "GET", url: e }).
				success(function (data, status) {
					$scope.sysData.entries = data.Entries;
					$scope.sysData.files = data.Files;
					$scope.sysData.folders = data.Folders
					$scope.sysData.files10mb = data.Count10mb;
					$scope.sysData.files50mb = data.Count50mb;
					$scope.sysData.files100mb = data.Count100mb;
					$scope.sysData.searchPath = returnBSlash(data.DirectoryPath);
				}).error(function (data, status) {
					$scope.errorData.errorCode = status;
					$scope.errorData.errorMessage = data;
					document.getElementById('alertMe').style.display = 'list-item';
				});
		},
		/*gets a data for ui using specified path as input.*/
		getSearchData: function (e){
			var result = Process(e, $scope.sysData.disks);
			var encode = encodeURIComponent(result);
			var path = $scope.httpData.url.getAll + encode; // !!!
			return $scope.functions.getContext(path);
		},

		/*function uses to retrieve entries for a disk drive*/
		getDiskEntries: function (e) {
			var path = $scope.httpData.url.getAll + e;
			return $scope.functions.getContext(path);
		},
		/*gets entries for higher folder*/
		moveUp: function (e) {
			if (isDisk(e, $scope.sysData.disks)) { return false };
			var upPath = tryMoveUp(e);
			var pretify = returnBSlash(upPath);
			pretify = encodeURIComponent(pretify);
			var path = $scope.httpData.url.getAll + pretify;
			return $scope.functions.getContext(path);
		},
		/*gets entries for specific folder*/
		getFolderEntries: function (e) {
			var checked = Process($scope.sysData.searchPath, $scope.sysData.disks)
			var combine = encodeURIComponent(checked + e);
			var path = $scope.httpData.url.getAll + combine;
			return $scope.functions.getContext(path);
		}
	}
	/*startup model initialization*/
	$scope.init = function () {
		return $scope.functions.getInit();
	}
	$scope.init();

}
/*Injection*/
mainController.$inject = ['$scope', '$http'];
/*Creation of controller*/
FSAControllers.controller('mainController',mainController);