/*cretes an array from a path using user-defined or default delimiter*/
var splitPath = function (path, delimiter) {
	delimiter = delimiter || '/';
	return path.split(delimiter);
};
/*returns true if string or false if array*/
var isArrayOrString = function (path) {
	return typeof path === 'string';
};

//var checkDiskPath = function (array) {
//	if (isArrayOrString(array)) {
//		if (array.length <= 3) {
//			return true;
//		}
//		else return false;
//	}
//	else {
//		if (array.length == 1) {
//			return true;
//		}
//		else return false;
//	}
//}

/*trims an array by slicing off last item if it`s delimiter or empty*/
var trimArray = function (array, delimiter) {
	delimiter = delimiter || '/';
	if (array[array.length - 1] === "" || array[array.length - 1] === delimiter) {
		array.pop();
		return array;
	}
	else return array;
};
/*trims a string by slicing off last item if it`s delimiter or empty*/
var trimString = function (string, delimiter) {
	delimiter = delimiter || '/';
	if (string[string.length - 1] === "" || string[string.length - 1] === delimiter) {
		return string.substring(0, string.length - 1);
	}
};

/*creates new path from input by slicing last directory name*/
var popPath = function (input, delimiter) {
	delimiter = delimiter || '/';
	var array = input.split(delimiter);
	var trimmed = trimArray(array);
	trimmed.pop();
	if (checkDiskPath) {
		return trimmed + '/';
	}
	var result = trimmed.join('/');
	return result;
};

/*replaces all backslashes in a path*/
var killBSlash = function (string) {
	return string.replace(/[\\]/g, "/");
};

/*returns slashes back :( */
var returnBSlash = function (string) {
	var result = string.replace(/[/]/g, "\\");
	return result;
};

var checkPath = function (string) {
	if (string[string.length - 1] != "\\") {
		return killBSlash(path + "\\");
	}
	else return killBSlash(string);
};


/*-------------------------------------------------------------------------------*/


/*checks if specified value is a logical disk path using disk colelction*/
var isDisk = function (e, disks) {
	return disks.indexOf(e) > -1;
};

/*tries to delete a delimiter at the end of the path if present*/
var tryTrimString = function (e, delimiter) {
	delimiter = delimiter || '/';
	if (e[e.length - 1] != delimiter) {
		return e;
	}
	do {
		e = e.substring(0, e.length - 1);
	} while (e[e.length - 1] == delimiter);
	return e;
};

/*pop the last folder in a string path*/
var popString = function (path, delimiter) {
	delimiter = delimiter || '/';
	var splited = path.split(delimiter);
	splited.pop();
	var result = splited.join(delimiter);
	return result + delimiter;
};

/*basic logic for processing path as inoput
  function checks if inout is a valid logical disk path
  or otherwise tries to pretify path.*/
var Process = function (e, collection) {
	switch (isDisk(e, collection)) {
		case true:
			return e; // < -- inplement session storage here?
			break;
		case false:
			var pretify = killBSlash(e);
			var pretify = tryTrimString(pretify);
			return pretify + '/';
			break;
	}
};

var tryMoveUp = function (e) {
	var pretify = killBSlash(e);
	var pretify = tryTrimString(pretify);
	return popString(pretify);
};

var checkSearch = function(e){
	if (e.length <= 3 && e.length > 0) {
		e = e[0] + ":\\"
		return e;
	}
	else return e;
}


