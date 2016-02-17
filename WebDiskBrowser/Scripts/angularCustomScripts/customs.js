/*-------------------------------------------------------------------------------*/

/*replaces all backslashes in a path*/
var killBSlash = function (string) {
	return string.replace(/[\\]/g, "/");
};

/*returns slashes back :( */
var returnBSlash = function (string) {
	var result = string.replace(/[/]/g, "\\");
	return result;
};

/*checks if specified value is a logical disk path using disk collection*/
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

/*basic logic for processing path as input
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
/*Cuts off last directory from a path*/
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


